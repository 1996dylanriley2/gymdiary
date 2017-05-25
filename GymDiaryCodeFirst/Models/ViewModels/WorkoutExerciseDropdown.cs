using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GymDiaryCodeFirst.Models.ViewModels
{
    public class WorkoutExerciseDropdown
    {
        public Workout Workout { get; set; }
        public IEnumerable<SelectListItem> Exercises { get; set; }
    }
}