using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GymDiaryCodeFirst.Models
{
    public class Set
    {
        //how to get exercisestatidid to automatically populate?
        [Key]
        public int SetId { get; set; }
        
        public int? ExerciseStatId { get; set; }
        [ForeignKey("ExerciseStatId")]
        public ExerciseStats ExerciseStat { get; set; }
        public float? WeightInKg { get; set; }
        public int? Reps { get; set; }
        public float? Minutes { get; set; }
    }
}