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

        /// <summary>
        /// This methods job is to save the workout to the database and return the saved data to the view.
        /// </summary>
        /// <param name="workoutCompletedByUser"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(Workout workoutCompletedByUser)
        {
            SaveNewCompletedWorkout(workoutCompletedByUser);

            var populatedWorkoutForView = workoutCompletedByUser;
            foreach (var e in populatedWorkoutForView.Exercises)
            {
                e.Exercise = PopulateWorkout.PopulateExerciseType(e.ExerciseId);
            }

            return View("CompletedWorkout", populatedWorkoutForView.Exercises);
        }

        public Workout CreateAndPopulateACopyOfBaseWorkout(Workout childWorkout)
        {
            var baseWorkout = PopulateWorkout.PopulateEntireWorkout(childWorkout.WorkoutId);
            childWorkout.Date = DateTime.Now;
            childWorkout.WorkoutId = default(int);
            childWorkout.IsBaseWorkout = false;
            childWorkout.Name = baseWorkout.Name;
            childWorkout.UserId = baseWorkout.UserId;

            foreach (var exerciseCompletedByUser in childWorkout.Exercises)
            {
                foreach (var baseExercise in baseWorkout.Exercises)
                {
                    //Note that in the childWorkout the exerciseStatId that gets posted back is the one from the baseExercise.
                    //This provides an if which enables us to copy over each piece of data correctly. This needs removing before saving to db though
                    //otherwise it will over-write data in the baseworkout
                    if (baseExercise.ExerciseStatsId == exerciseCompletedByUser.ExerciseStatsId)
                    {
                        exerciseCompletedByUser.DesiredSet = baseExercise.DesiredSet;
                        exerciseCompletedByUser.ExerciseId = baseExercise.ExerciseId;
                        exerciseCompletedByUser.DesiredSetId = baseExercise.DesiredSetId;
                        exerciseCompletedByUser.DesiredSetCount = baseExercise.DesiredSetCount;
                        exerciseCompletedByUser.ExerciseStatsId = default(int);
                    }
                }

            }

            return childWorkout;
        }

        public void SaveNewCompletedWorkout(Workout workout)
        {
            var workoutForDb = CreateAndPopulateACopyOfBaseWorkout(workout);

            //desiredset is causing sql error "unable to determin valid ordering"
            //Because its tryinig to update this item in db which isn't nessesary as we are not modifying it.
            //Therefor we only post back relivant info which in this case is the fk desiredsetid.
            foreach (var x in workoutForDb.Exercises)
            {
                x.DesiredSet = null;
            }

            db.Workouts.Add(workoutForDb);
            db.SaveChanges();
        }
    }
}