using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GymDiaryCodeFirst.Models
{
    public class Exercise
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ExerciseType Type { get; set; }
        [Required]
        public int PrimaryMuscleId { get; set; }
        [ForeignKey("PrimaryMuscleId")]
        public Muscle PrimaryMuscle { get; set; }
        public int? SecondaryMuscleId { get; set; }
        [ForeignKey("SecondaryMuscleId")]
        public Muscle SecondaryMuscle { get; set; }
    }
}