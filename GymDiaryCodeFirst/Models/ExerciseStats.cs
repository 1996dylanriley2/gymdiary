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
        [ForeignKey("ExerciseId")]
        public Exercise Exercise { get; set; }
        [Required]
        public int WorkoutId { get; set; }
        [ForeignKey("WorkoutId")]
        public Workout Workout { get; set; }
        public int? DesiredSetId { get; set; }
        [ForeignKey("DesiredSetId")]
        public Set DesiredSet { get; set; }
        public int DesiredSetCount { get; set; }
        //This attribute ensures that the same navigational property is used for both desireset and actualsets. Otherwise another fk collum is created so tht fks are not reused.
        [InverseProperty("ExerciseStat")]
        public List<Set> ActualSets { get; set; }
        

    }
}