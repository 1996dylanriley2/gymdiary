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
        public string UserId { get; set; }
        //public ApplicationUser User { get; set; }
        [Display(Name = "Workout Name")]
        public string Name { get; set; }
        public DateTime? Date { get; set; }
        public List<ExerciseStats> Exercises { get; set; }
        
    }
}