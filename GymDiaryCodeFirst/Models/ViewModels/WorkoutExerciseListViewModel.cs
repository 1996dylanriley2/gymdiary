using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GymDiaryCodeFirst.Models.ViewModels
{
    public class WorkoutExerciseListViewModel
    {
        public Workout Workout { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
    }
}