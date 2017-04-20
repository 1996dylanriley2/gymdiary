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
    public class ExercisesController : Controller
    {
        private GymDiaryContext db = new GymDiaryContext();

        // GET: Exercises
        public ActionResult Index()
        {
            var exercises = db.Exercises.Include(e => e.PrimaryMuscle).Include(e => e.SecondaryMuscle);
            return View(exercises.ToList());
        }

        // GET: Exercises/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exercise exercise = db.Exercises.Find(id);
            if (exercise == null)
            {
                return HttpNotFound();
            }
            return View(exercise);
        }

        // GET: Exercises/Create
        public ActionResult Create()
        {
            ViewBag.PrimaryMuscleId = new SelectList(db.Muscles, "Id", "Name");
            ViewBag.SecondaryMuscleId = new SelectList(db.Muscles, "Id", "Name");
            return View();
        }

        // POST: Exercises/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,PrimaryMuscleId,SecondaryMuscleId")] Exercise exercise)
        {
            if (ModelState.IsValid)
            {
                db.Exercises.Add(exercise);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PrimaryMuscleId = new SelectList(db.Muscles, "Id", "Name", exercise.PrimaryMuscleId);
            ViewBag.SecondaryMuscleId = new SelectList(db.Muscles, "Id", "Name", exercise.SecondaryMuscleId);
            return View(exercise);
        }

        // GET: Exercises/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exercise exercise = db.Exercises.Find(id);
            if (exercise == null)
            {
                return HttpNotFound();
            }
            ViewBag.PrimaryMuscleId = new SelectList(db.Muscles, "Id", "Name", exercise.PrimaryMuscleId);
            ViewBag.SecondaryMuscleId = new SelectList(db.Muscles, "Id", "Name", exercise.SecondaryMuscleId);
            return View(exercise);
        }

        // POST: Exercises/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,PrimaryMuscleId,SecondaryMuscleId")] Exercise exercise)
        {
            if (ModelState.IsValid)
            {
                db.Entry(exercise).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PrimaryMuscleId = new SelectList(db.Muscles, "Id", "Name", exercise.PrimaryMuscleId);
            ViewBag.SecondaryMuscleId = new SelectList(db.Muscles, "Id", "Name", exercise.SecondaryMuscleId);
            return View(exercise);
        }

        // GET: Exercises/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exercise exercise = db.Exercises.Find(id);
            if (exercise == null)
            {
                return HttpNotFound();
            }
            return View(exercise);
        }

        // POST: Exercises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Exercise exercise = db.Exercises.Find(id);
            db.Exercises.Remove(exercise);
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
