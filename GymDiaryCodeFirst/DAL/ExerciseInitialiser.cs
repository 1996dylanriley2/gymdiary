using GymDiaryCodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GymDiaryCodeFirst.DAL
{
    public class ExerciseInitialiser : System.Data.Entity.DropCreateDatabaseIfModelChanges<ExerciseContext>
    {
        protected override void Seed(ExerciseContext context)
        {
            var exercises = new List<Exercise>
            {
            new Exercise{Name="Biceip Curl", PrimaryMuscleId=1, SecondaryMuscleId=2},
            new Exercise{Name="Reverse Biceip Curl", PrimaryMuscleId=1, SecondaryMuscleId=2},
            new Exercise{Name="Bench Press", PrimaryMuscleId=5, SecondaryMuscleId=2},

            };

            exercises.ForEach(e => context.Exercises.Add(e));
            context.SaveChanges();
        }
    }
}