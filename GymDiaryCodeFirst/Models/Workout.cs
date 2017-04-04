using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GymDiaryCodeFirst.Models
{
    public class Workout
    {
        [Key]
        public int WorkoutId { get; set; }
        public int? UserId { get; set; }
        //public ApplicationUser User { get; set; }
        public DateTime? Date { get; set; }
        public virtual ExerciseStats ExerciseStats1 { get; set; }
        public ExerciseStats ExerciseStats2 { get; set; }
    }
}