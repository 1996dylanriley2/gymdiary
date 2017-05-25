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
using GymDiaryCodeFirst.Models.ViewModels;

namespace GymDiaryCodeFirst.Views
{
    public class WorkoutsController : Controller
    {
        private GymDiaryContext db = new GymDiaryContext();

        // GET: Workouts
        public ActionResult Index()
        {
            return View(db.Workouts.ToList());
        }

        // GET: Workouts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<ExerciseStats> workout = db.ExerciseStats.Where(w => w.WorkoutId == id).ToList();
            if (workout == null)
            {
                return HttpNotFound();
            }
            foreach(var item in workout)
            {
                item.Exercise = db.Exercises.Find(item.ExerciseId);
                item.Workout = db.Workouts.Find(item.WorkoutId);
            }
            return View(workout);
        }

        // GET: Workouts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Workouts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WorkoutId,UserId,Name,Date")] Workout workout)
        {
            if (ModelState.IsValid)
            {
                db.Workouts.Add(workout);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(workout);
        }

        [HttpGet]
        public ActionResult AddExercise(int id)
        {
            Workout workout = db.Workouts.Find(id);
            AddExercisesToWorkoutFromDB(workout);

            workout.Exercises.Add(new ExerciseStats() { WorkoutId = 100, ExerciseStatsId = 100 });
            ViewData["dropdownData"] = GetListOfExercisesForDropDown();
            ViewBag.id = id;

            return PartialView("_AddExerciseList", workout.Exercises);
        }
        public IEnumerable<SelectListItem> GetListOfExercisesForDropDown()
        {
            return db.Exercises.Select(i => new SelectListItem
            {
                Value = i.Id.ToString(),
                Text = i.Name
            });
        }
        public ActionResult LoadExercises(int id)
        {
            Workout workout = db.Workouts.Find(id);
            AddExercisesToWorkoutFromDB(workout);
            ViewData["dropdownData"] = GetListOfExercisesForDropDown();
            ViewBag.id = id;

            return PartialView("_AddExerciseList", workout.Exercises);
        }
        //This method adds 20 extra exercises to the model incase the user wants to add many exercises.
        //Adding items in the view was difficult so this seems eaiser.
        //Also empty exercises are just removed when form is submitted
        public List<ExerciseStats> Add20Exercises(List<ExerciseStats> listToExtend)
        {
            for(var i = 0; i < 20; i++)
            {
                listToExtend.Add(new ExerciseStats());
            }

            return listToExtend;
        }

        public ActionResult Edit(int? id)
         {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Workout workout = db.Workouts.Find(id);

            if (workout == null)
            {
                return HttpNotFound();
            }

            AddExercisesToWorkoutFromDB(workout);

            workout.Exercises = Add20Exercises(workout.Exercises);

            var viewModel = new WorkoutExerciseDropdown();

            viewModel.Exercises = GetListOfExercisesForDropDown();
            viewModel.Workout = workout;
            ViewBag.id = id;
            

            return View(viewModel);
        }

        // POST: Workouts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Workout workout)
        {

            if (ModelState.IsValid)
            {
                var workoutInDb = db.Workouts.Single(x => x.WorkoutId == workout.WorkoutId);
                workoutInDb.Name = workout.Name;

               
                foreach (var e in workout.Exercises)
                { 
                    //Modifies any existing exerciseStats
                    if (e.ExerciseStatsId != 0)
                    {
                        var eInDb = db.ExerciseStats.Single(x => x.ExerciseStatsId == e.ExerciseStatsId);
                        eInDb.ExerciseId = e.ExerciseId;
                        eInDb.WeightInKg = e.WeightInKg;
                        eInDb.Sets = e.Sets;
                        eInDb.Reps = e.Reps;
                        eInDb.Minutes = e.Minutes;
                        db.SaveChanges(); 
                    }        
                    else if (e.ExerciseId != 0)
                    {
                        workoutInDb.Exercises.Add(e);
                    }          
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(workout);

        }

        public Workout AddExercisesToWorkoutFromDB(Workout workout)
        {
            workout.Exercises = db.ExerciseStats.Where(e => e.WorkoutId == workout.WorkoutId).ToList();

            foreach (var item in workout.Exercises)
            {
                item.Exercise = db.Exercises.Find(item.ExerciseId);
                item.Workout = db.Workouts.Find(item.WorkoutId);
            }
            return workout;
        }

        // GET: Workouts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workout workout = db.Workouts.Find(id);
            if (workout == null)
            {
                return HttpNotFound();
            }
            return View(workout);
        }

        // POST: Workouts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Workout workout = db.Workouts.Find(id);
            db.Workouts.Remove(workout);
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
