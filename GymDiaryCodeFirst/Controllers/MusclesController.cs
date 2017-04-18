using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GymDiaryCodeFirst.DAL;
using GymDiaryCodeFirst.Models;

namespace GymDiaryCodeFirst.Views
{
    public class MusclesController : Controller
    {
        private GymDiaryContext db = new GymDiaryContext();

        // GET: Muscles
        public ActionResult Index()
        {
            return View(db.Muscles.ToList());
        }

        // GET: Muscles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Muscle muscle = db.Muscles.Find(id);
            if (muscle == null)
            {
                return HttpNotFound();
            }
            return View(muscle);
        }

        // GET: Muscles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Muscles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Muscle muscle)
        {
            if (ModelState.IsValid)
            {
                db.Muscles.Add(muscle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(muscle);
        }

        // GET: Muscles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Muscle muscle = db.Muscles.Find(id);
            if (muscle == null)
            {
                return HttpNotFound();
            }
            return View(muscle);
        }

        // POST: Muscles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Muscle muscle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(muscle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(muscle);
        }

        // GET: Muscles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Muscle muscle = db.Muscles.Find(id);
            if (muscle == null)
            {
                return HttpNotFound();
            }
            return View(muscle);
        }

        // POST: Muscles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Muscle muscle = db.Muscles.Find(id);
            db.Muscles.Remove(muscle);
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
