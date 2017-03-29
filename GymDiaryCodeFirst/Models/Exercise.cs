using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GymDiaryCodeFirst.Models
{
    public class Exercise
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int PrimaryMuscleId { get; set; }
        public int SecondaryMuscleId { get; set; }
    }
}