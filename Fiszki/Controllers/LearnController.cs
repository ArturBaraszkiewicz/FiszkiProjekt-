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

            var karty = db.Cards.ToList().FindAll(obj => obj.Package.AuthorID == "ddbde702-2a30-4aae-96b9-3aba7afa2cbd");
            LearnStatus x = new LearnStatus();
            foreach (Card karta in karty)
            {
                LearnStatuses.Add(new LearnStatus { Card = karta });
            }
            return LearnStatuses;
        }

        // GET: Learn/Details/5
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
        public ActionResult Create()
        {
            ViewBag.CardID = new SelectList(db.Cards, "ID", "PlWord");
            return View();
        }

        // POST: LearnStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CardID,UserID,Progress,Views,NextOccurrence")] LearnStatus learnStatus)
        {
            if (ModelState.IsValid)
            {
                db.LearnStatuss.Add(learnStatus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

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
