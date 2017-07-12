using GymDiaryCodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GymDiaryCodeFirst.Utils
{
    public static class ResetStats
    {
        public static List<ExerciseStats> Reset(List<ExerciseStats> exercises)
        {
            foreach(var exercise in exercises)
            {
                exercise.WorkoutId = 0;
                exercise.Minutes = 0;
                exercise.Reps = 0;
                exercise.Sets = 0;
            }
            return exercises;
        }
    }
}