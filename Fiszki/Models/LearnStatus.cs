using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Fiszki.Models
{
    public class LearnStatus
    {
        public string UserID { get; set; }
        [Key]
        [ForeignKey("Card")]
        public int CardID { get; set; }
        public int Progress { get; set; }
        public int Views { get; set; }
        [DataType(DataType.Date)]
        public DateTime NextOccurrence { get; set; }

        public virtual Card Card { get; set; }
    }
}