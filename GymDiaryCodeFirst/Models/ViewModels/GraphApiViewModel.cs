using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GymDiaryCodeFirst.Models.ViewModels
{
    public class GraphApiViewModel
    {
        public int Id { get; set; }
        public int DesiredSetCount { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public float? Minutes { get; set; }
        public int Reps { get; set; }
        public float? WeightInKg { get; set; }

    }
}