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
using Microsoft.AspNet.Identity;

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
                //use workout class and use addexercisesfromdb()
                item.Exercise = db.Exercises.Find(item.ExerciseId);
                item.Workout = db.Workouts.Find(item.WorkoutId);
                item.DesiredSet = db.Sets.Find(item.DesiredSetId);
            }
            
            return View(workout);
        }

        // GET: Workouts/Create
        public ActionResult Create()
        {
            //TODO: refactor this controller as its duplicates alot of the code in edit
            var workout = new Workout() {UserId = User.Identity.GetUserId(), Date = DateTime.Now, };

            workout.Exercises = new List<ExerciseStats>();
            workout.Exercises = AddEmptyExercises(workout.Exercises);

            var viewModel = new WorkoutExerciseDropdown();

            viewModel.Exercises = GetListOfExercisesForDropDown();
            viewModel.Workout = workout;

            return View("Edit",viewModel);
        }

        // POST: Workouts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Workout workout)
        {
            var newWorkout = new Workout() {
                IsBaseWorkout = true,
                Date = DateTime.Now,
                Name = workout.Name,
                UserId = workout.UserId,
                Exercises = new List<ExerciseStats>(),
                };
            foreach (var e in workout.Exercises)
            {
                if (e.ExerciseId != 0)
                {
                    newWorkout.Exercises.Add(e);
                }    
            }
            db.Workouts.Add(newWorkout);
            db.SaveChanges();
            return RedirectToAction("Index");
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

            workout.Exercises = AddEmptyExercises(workout.Exercises);

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
            //setid is null
            var workoutInDb = db.Workouts.Single(x => x.WorkoutId == workout.WorkoutId);
            
            workoutInDb.Name = workout.Name;


            foreach (var e in workout.Exercises)
            {
                //Modifies any existing exerciseStats
                if (e.ExerciseStatsId != 0)
                {
                    var eInDb = db.ExerciseStats.Single(x => x.ExerciseStatsId == e.ExerciseStatsId);
                    var desiredSetInDb = db.Sets.Single(x => x.SetId == e.DesiredSetId);
                    if (e.ExerciseId == 0)
                        db.ExerciseStats.Remove(eInDb);
                    else
                    {
                        eInDb.ExerciseId = e.ExerciseId;
                        desiredSetInDb.WeightInKg = e.DesiredSet.WeightInKg;
                        eInDb.DesiredSetCount = e.DesiredSetCount;
                        desiredSetInDb.Reps = e.DesiredSet.Reps;
                        desiredSetInDb.Minutes = e.DesiredSet.Minutes;
                    }
                    db.SaveChanges();
                }
                else if (e.ExerciseId != 0)
                {
                    workoutInDb.Exercises.Add(e);
                }
                db.SaveChanges();
            }

            return RedirectToAction("Index");
            // if modelstate is invalid then => return View(new WorkoutExerciseDropdown() { Exercises = GetListOfExercisesForDropDown(), Workout = workout });

        }

        public Workout AddExercisesToWorkoutFromDB(Workout workout)
        {
            //TODO: use the untils instead of this class
            workout.Exercises = db.ExerciseStats.Where(e => e.WorkoutId == workout.WorkoutId).ToList();

            foreach (var item in workout.Exercises)
            {
                item.Exercise = db.Exercises.Find(item.ExerciseId);
                item.Workout = db.Workouts.Find(item.WorkoutId);
                item.DesiredSet = db.Sets.Find(item.DesiredSetId);
            }
            return workout;

        }
        public IEnumerable<SelectListItem> GetListOfExercisesForDropDown()
        {
            return db.Exercises.Select(i => new SelectListItem
            {
                Value = i.Id.ToString(),
                Text = i.Name
            });
        }

        //This method adds extra exercises to the model incase the user wants to add many exercises.
        //Adding items in the view was difficult so this seems eaiser.
        //Also empty exercises are just removed when form is submitted
        public List<ExerciseStats> AddEmptyExercises(List<ExerciseStats> listToExtend)
        {
            for (var i = 0; i < 60; i++)
            {
                listToExtend.Add(new ExerciseStats());
            }

            return listToExtend;
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
