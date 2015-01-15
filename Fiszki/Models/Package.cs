using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fiszki.Models
{
    public class Package
    {
        public int ID { get; set; }
        public int AuthorID { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string Icon { get; set; }

        public virtual ICollection<Card> Cards { get; set; }
    }
}
