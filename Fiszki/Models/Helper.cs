using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Fiszki.Models;

namespace Fiszki
{
    public class Helper
    {
        public static MethodResult SubmitPost(string url, string postValues, out string answer)
        {
            MethodResult result = new MethodResult();
            answer = string.Empty;

            try
            {
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] data = encoding.GetBytes(postValues);

                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.Timeout = System.Threading.Timeout.Infinite;

                webRequest.ContentLength = data.Length;

                Stream requestStream = webRequest.GetRequestStream();
                requestStream.Write(data, 0, data.Length);
                requestStream.Close();
                requestStream.Flush();

                WebResponse webResponse = webRequest.GetResponse();
                StreamReader reader = new StreamReader(webResponse.GetResponseStream());

                var dataReceived = reader.ReadToEnd();

                webResponse.Close();
                reader.Close();
                webRequest.Abort();

                result.returnCode = MethodResult.ReturnCode.Success;
                answer = dataReceived;
            }

            catch (Exception ex)
            {
                result.returnCode = MethodResult.ReturnCode.Failure;
                result.errorMessage = ex.Message;
            }

            return result;
        }


        public static string BuildPost(Dictionary<string, string> zawartosc)
        {
            StringBuilder builder = new StringBuilder();

            foreach (KeyValuePair<string, string> value in zawartosc)
            {
                if (builder.Length > 0) builder.Append("&");

                builder.Append(string.Format("{0}={1}", value.Key, value.Value));
            }

            return builder.ToString();
        }
    }
}