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
            var workout = PopulateWorkout.PopulateEntireWorkout(id);
            return View(workout);
        }

        [HttpPost]
        public ActionResult Index(Workout workoutCompletedByUser)
        {
            //This method creates a copy of the baseworkout before filling in the blanks to make it a complete workout then saves to db.
            //Cloning the baseworkout using workoutID
            var baseWorkout = PopulateWorkout.PopulateEntireWorkout(workoutCompletedByUser.WorkoutId);
            workoutCompletedByUser.Date = DateTime.Now;
            workoutCompletedByUser.WorkoutId = default(int);
            workoutCompletedByUser.IsBaseWorkout = false;
            workoutCompletedByUser.Name = baseWorkout.Name;
            workoutCompletedByUser.UserId = baseWorkout.UserId;

            foreach(var exerciseCompletedByUser in workoutCompletedByUser.Exercises)
            {
                foreach(var baseExercise in baseWorkout.Exercises)
                {
                    //Note that in the workoutCompletedByUser the exerciseStatId that gets posted back is the one from the baseExercise.
                    //This provides an if which enables us to copy over each piece of data correctly. This needs removing before saving to db though
                    //otherwise it will over-write data in the baseworkout
                    if(baseExercise.ExerciseStatsId == exerciseCompletedByUser.ExerciseStatsId)
                    {
                        exerciseCompletedByUser.DesiredSet = baseExercise.DesiredSet;
                        exerciseCompletedByUser.ExerciseId = baseExercise.ExerciseId;
                        exerciseCompletedByUser.DesiredSetId = baseExercise.DesiredSetId;
                        exerciseCompletedByUser.DesiredSetCount = baseExercise.DesiredSetCount;
                        exerciseCompletedByUser.ExerciseStatsId = default(int);
                    }
                }
                
            }

            db.Workouts.Add(workoutCompletedByUser);
            db.SaveChanges();
            
            foreach(var e in workoutCompletedByUser.Exercises)
            {
                e.Exercise = PopulateWorkout.PopulateExerciseType(e.ExerciseId);
            }

            return View("CompletedWorkout", workoutCompletedByUser.Exercises);
        }
    }
}