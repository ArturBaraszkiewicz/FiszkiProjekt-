using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Fiszki.DAL;
using Fiszki.Models;
using Microsoft.AspNet.Identity;
using System.Web.Services;

namespace Fiszki.Controllers
{
    public class LearnController : Controller
    {
        private FiszkiContext db = new FiszkiContext();

        // GET: Learn/Package/5
        [Authorize]
        public ActionResult Package(int? id)
        {
            if (id == null)
            {
                return RedirectPermanent("/Packages/");
            }
            Package package = db.Packages.Find(id);
            if (package == null)
            {
                return HttpNotFound();
            }
            int PackageID = id.GetValueOrDefault();

            if (IsLearnStatusExist(PackageID))
            {
                return View(TodayCards(PackageID));
            }
            else
            {
                return View(GenerateCardsNew(PackageID));
            }
        }
        public List<LearnStatus> TodayCards(int PackageID)
        {
            string userID = User.Identity.GetUserId();

            //TODO porównywanie daty (nie obsługiwane w linq for entity)
            // && (learn.NextOccurrence <= DateTime.Now.Date)
            // learnQuery = learnQuery.Where(l => l.NextOccurrence.Date <= DateTime.Now.Date);

            var learnQuery =
                from package in db.Packages
                join card in db.Cards on package.ID equals card.PackageID
                join learn in db.LearnStatuss on card.ID equals learn.CardID
                where (learn.UserID == userID) && (package.ID == PackageID) 
                select learn;

            return learnQuery.ToList();
        }
        public bool IsLearnStatusExist(int PackageID)
        {
            string userId = User.Identity.GetUserId();

            var learnQuery = 
                from learn in db.LearnStatuss
                where (learn.UserID == userId) && (learn.Card.PackageID==PackageID)
                select learn.CardID;

            if (learnQuery.Count() == 0)
            {
                return false;
            }
            else {
                return true;
            }
        }
        public List<LearnStatus> GenerateCardsNew(int PackageID)
        {
            List<LearnStatus> learnList = new List<LearnStatus>();

            var cardsFromPackage =
                from package in db.Packages
                join card in db.Cards on package.ID equals card.PackageID
                where package.ID == PackageID
                select card;

            foreach (Card card in cardsFromPackage)
            {
                learnList.Add(new LearnStatus
                {
                    Card = card,
                    Views = 0,
                    Progress = card.Difficult,
                    UserID = User.Identity.GetUserId(),
                    NextOccurrence = DateTime.Now.Date
                });
            }

            db.LearnStatuss.AddRange(learnList);
            db.SaveChanges();

            return learnList.ToList();
        }
        // GET: Learn/Update
        public void Update(int id, string answer)
        {
            var userID = User.Identity.GetUserId();

            var learnQuery =
                from learn in db.LearnStatuss
                where (learn.CardID == id) && (learn.UserID == userID)
                select learn;

            if (learnQuery.Count() == 1)
            {
                var learnStatus = learnQuery.First();

                if (answer == "true")
                {
                    learnStatus.Progress -= 1;
                    learnStatus.NextOccurrence = DateTime.Now.Date.AddDays(3);
                }
                else if (answer == "false")
                {
                    learnStatus.Progress += 1;
                    learnStatus.NextOccurrence = DateTime.Now.Date.AddDays(1);
                }
                else
                {
                    return; //error
                }

                learnStatus.Views += 1;

                if (ModelState.IsValid)
                {
                    db.Entry(learnStatus).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return; //TODO zwracanie succes/fail
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
