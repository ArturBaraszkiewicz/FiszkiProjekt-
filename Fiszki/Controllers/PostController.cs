using System.Collections.Generic;
using System.Web.Mvc;
using Fiszki.Models;

namespace Fiszki.Controllers
{
    public class PostController : Controller
    {
        //
        // GET: /Post/

        public ActionResult Index()
        {
            return View();
        }


        //
        // POST: /Create
        [HttpPost]
        public ActionResult Create(PostModel model)
        {
            try
            {
                var post = new Dictionary<string, string>();

               
                post.Add("access_token", Session["accessToken"].ToString());
                post.Add("message", model.message);
                post.Add("URl aplikacji",model.link);

                string Masage = string.Empty;
                string answer;
                MethodResult header = Helper.SubmitPost(string.Format("https://graph.facebook.com/{0}/feed", Session["uid"].ToString()),
                                                            Helper.BuildPost(post),
                                                            out answer);

                if (header.returnCode == MethodResult.ReturnCode.Success)
                {
                    var deserialised =
                        Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(answer);
                    Masage = deserialised["id"];
                    return RedirectToAction("CreatedSuccessfully");
                }
                
            }
            catch
            {
               
            }

            return RedirectToAction("PostError");
        }


        public ActionResult CreatedSuccessfully()
        {
            return View();
        }


        public ActionResult PostError()
        {
            return View();
        }

    }
}
