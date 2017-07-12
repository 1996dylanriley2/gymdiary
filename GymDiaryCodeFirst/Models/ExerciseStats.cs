using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GymDiaryCodeFirst.Models
{
    public class ExerciseStats
    {
        [Key]
        public int ExerciseStatsId { get; set; }
        [Required]
        public int ExerciseId { get; set; }
        
        public Exercise Exercise { get; set; }
        [Required]
        public int WorkoutId { get; set; }
        [ForeignKey("WorkoutId")]
        public Workout Workout { get; set; }
        public float? WeightInKg { get; set; }
       
        public int Sets { get; set; }
        public int? Reps { get; set; }
        public int? Minutes { get; set; }
    }
}