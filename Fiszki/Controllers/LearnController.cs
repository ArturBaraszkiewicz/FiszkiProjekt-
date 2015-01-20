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

        // GET: Learn
        public ActionResult Index()
        {
            
            return View(NewLearnStatus());
        }

        public List<LearnStatus> NewLearnStatus()
        {
            var LearnStatuses = new List<LearnStatus>();
            /* TODO zapytanie linq wołające wszystkie karty z danej paczki 
            List<Card> cards = db.Cards.ToList();

            foreach (Package package in db.Packages.ToList())
            {
                package.Cards.ToList();
                
            }*/
            string userID = User.Identity.GetUserId();

            var karty = db.Cards.ToList().FindAll(obj => obj.Package.AuthorID == userID);
            LearnStatus x = new LearnStatus();
            foreach (Card karta in karty)
            {
                LearnStatuses.Add(new LearnStatus { Card = karta });
            }
            return LearnStatuses;
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LearnStatus learnStatus = db.LearnStatuss.Find(id);
            if (learnStatus == null)
            {
                return HttpNotFound();
            }
            return View(learnStatus);
        }

        // GET: Learn/Create
        public string Create(int? id, string answer)
        {
            //TODO: Zapisywanie do bazy informacji o umiem/nieUmiem danej karty.
            string x = "ID: " + id + "Answer: " + answer;
            return x;
        }

        // POST: LearnStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id, answer")] String s)
        {
            LearnStatus learnStatus = new LearnStatus();
            if (ModelState.IsValid)
            {
                db.LearnStatuss.Add(learnStatus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            s = s + "";
            ViewBag.CardID = new SelectList(db.Cards, "ID", "PlWord", learnStatus.CardID);
            return View(learnStatus);
        }

        // GET: Learn/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LearnStatus learnStatus = db.LearnStatuss.Find(id);
            if (learnStatus == null)
            {
                return HttpNotFound();
            }
            ViewBag.CardID = new SelectList(db.Cards, "ID", "PlWord", learnStatus.CardID);
            return View(learnStatus);
        }

        // POST: LearnStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CardID,UserID,Progress,Views,NextOccurrence")] LearnStatus learnStatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(learnStatus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CardID = new SelectList(db.Cards, "ID", "PlWord", learnStatus.CardID);
            return View(learnStatus);
        }

        // GET: Learn/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LearnStatus learnStatus = db.LearnStatuss.Find(id);
            if (learnStatus == null)
            {
                return HttpNotFound();
            }
            return View(learnStatus);
        }

        // POST: LearnStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LearnStatus learnStatus = db.LearnStatuss.Find(id);
            db.LearnStatuss.Remove(learnStatus);
            db.SaveChanges();
            return RedirectToAction("Index");
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
