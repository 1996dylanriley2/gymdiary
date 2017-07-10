using GymDiaryCodeFirst.DAL;
using GymDiaryCodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GymDiaryCodeFirst.Utils
{
    public static class GetWorkout
    {
        private static GymDiaryContext db = new GymDiaryContext();
        public static Workout Get(int id)
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
            return db.ExerciseStats.Where(x => x.WorkoutId == id).ToList();
            
        }

        private static Exercise PopulateExerciseType(int id)
        {
            return db.Exercises.Single(x => x.Id == id);
        }
    }
}