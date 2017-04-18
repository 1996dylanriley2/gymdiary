using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [DisplayName("Exercise")]
        public ExerciseStats ExerciseStats1 { get; set; }
        [DisplayName("Exercise")]
        public ExerciseStats ExerciseStats2 { get; set; }
        [DisplayName("Exercise")]
        public ExerciseStats ExerciseStats3 { get; set; }
        [DisplayName("Exercise")]
        public ExerciseStats ExerciseStats4 { get; set; }
        [DisplayName("Exercise")]
        public ExerciseStats ExerciseStats5 { get; set; }
        [DisplayName("Exercise")]
        public ExerciseStats ExerciseStats6 { get; set; }
        [DisplayName("Exercise")]
        public ExerciseStats ExerciseStats7 { get; set; }
        [DisplayName("Exercise")]
        public ExerciseStats ExerciseStats8 { get; set; }
        [DisplayName("Exercise")]
        public ExerciseStats ExerciseStats9 { get; set; }
        [DisplayName("Exercise")]
        public ExerciseStats ExerciseStats10 { get; set; }
        [DisplayName("Exercise")]
        public ExerciseStats ExerciseStats11 { get; set; }
        [DisplayName("Exercise")]
        public ExerciseStats ExerciseStats12 { get; set; }
        [DisplayName("Exercise")]
        public ExerciseStats ExerciseStats13 { get; set; }
        [DisplayName("Exercise")]
        public ExerciseStats ExerciseStats14 { get; set; }
        [DisplayName("Exercise")]
        public ExerciseStats ExerciseStats15 { get; set; }
    }
}