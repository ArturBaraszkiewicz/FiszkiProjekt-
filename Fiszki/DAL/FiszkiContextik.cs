using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Fiszki.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Fiszki.DAL
{
    public class FiszkiContextik : DbContext
    {
        public FiszkiContextik() : base("FiszkiContextik")
        {
        }

        public DbSet<Card> Cards { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<LearnStatus> LearnStatuss { get; set; }

    }
}