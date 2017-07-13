using GymDiaryCodeFirst.DAL;
using GymDiaryCodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GymDiaryCodeFirst.Utils
{
    public static class PopulateWorkout
    {
        private static GymDiaryContext db = new GymDiaryContext();
        public static Workout PopulateEntireWorkout(int id)
        {
            var workout = db.Workouts.Single(x => x.WorkoutId == id);
            workout.Exercises = PopulateExerciseStats(id);
            foreach(var exercise in workout.Exercises)
            {
                exercise.Exercise = PopulateExerciseType(exercise.ExerciseId);
            }
            return workout;
        }

        public static List<ExerciseStats> PopulateExerciseStats(int id)
        {

            var exercises = db.ExerciseStats.Where(x => x.WorkoutId == id).ToList();
            foreach(var e in exercises)
            {
                e.DesiredSet = PopulateDesiredSet(e.DesiredSetId);
                //e.ActualSets = PopulateActualSets(e.ExerciseStatsId);
            }
            return exercises;
        }

        public static Set PopulateDesiredSet(int setId)
        {
            return db.Sets.Single(x => x.SetId == setId);
        }
        //public static List<Set> PopulateActualSets(int exerciseStatsId)
        //{

        //}
        public static Exercise PopulateExerciseType(int id)
        {
            return db.Exercises.Single(x => x.Id == id);
        }
    }
}