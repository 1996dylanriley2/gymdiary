using GymDiaryCodeFirst.DAL;
using GymDiaryCodeFirst.Models;
using GymDiaryCodeFirst.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GymDiaryCodeFirst.Controllers
{
    public class DoWorkoutController : Controller
    {
        //get workout from db
        private GymDiaryContext db = new GymDiaryContext();

        // GET: DoWorkout
        [HttpGet]
        public ActionResult Index(int id)
        {
            var workout = GetWorkout.Get(id);
            return View(workout);
        }

        [HttpPost]
        public ActionResult Index(Workout workout)
        {
            var completedWorkout = GetWorkout.Get(workout.WorkoutId);
            completedWorkout.Date = DateTime.Now;
            completedWorkout.Exercises = ResetStats.Reset(completedWorkout.Exercises);
            workout.WorkoutId = default(int);

            foreach(var exercise in completedWorkout.Exercises)
            {
                foreach(var exercisePosted in workout.Exercises)
                if(exercise.ExerciseId == exercisePosted.ExerciseId)
                    {
                        exercise.Reps = exercisePosted.Reps;
                        exercise.Sets = exercisePosted.Sets;
                        exercise.Minutes = exercisePosted.Minutes;
                    }
            }

            db.Workouts.Add(completedWorkout);
            db.SaveChanges();
            
            return View("CompletedWorkout", completedWorkout.Exercises);
        }
    }
}