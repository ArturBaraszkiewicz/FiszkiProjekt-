using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fiszki.Models
{
    public enum Category
    {
        //rzeczownik, przymiotnik, czasownik, inne
        noun, adjective, verb, other
    }
    public class Card
    {
        public int ID { get; set; }
        public string PlWord { get; set; }
        public string EngWord { get; set; }
        [Range(1, 10)]
        public int Difficult { get; set; }
        public Category Category { get; set; }

        public virtual Package Package { get; set; }
    }
}