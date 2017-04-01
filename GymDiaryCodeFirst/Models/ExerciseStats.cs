using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GymDiaryCodeFirst.Models
{
    public class ExerciseStats
    {
        public Exercise ExerciseId { get; set; }
        public int WorkoutId { get; set; }
        public string Reps { get; set; }
        public int Sets { get; set; }
    }
}