using GymDiaryCodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GymDiaryCodeFirst.DAL
{
    public class ExerciseInitialiser : System.Data.Entity.DropCreateDatabaseAlways<GymDiaryContext>
    {
        protected override void Seed(GymDiaryContext context)
        {
            var muscles = new List<Muscle>
            {
            new Muscle{Name="Bieceip"},
            new Muscle{Name="triceip"},
            new Muscle{Name="Quads"}

            };

            muscles.ForEach(e => context.Muscles.Add(e));
            context.SaveChanges();

            var exercises = new List<Exercise>
            {
            new Exercise{Name="Biceip Curl", PrimaryMuscleId=1, SecondaryMuscleId=2},
            new Exercise{Name="Reverse Biceip Curl", PrimaryMuscleId=1, SecondaryMuscleId=2},
            new Exercise{Name="Bench Press", PrimaryMuscleId=1, SecondaryMuscleId=2},

            };

            exercises.ForEach(e => context.Exercises.Add(e));
            context.SaveChanges();

            var y = new ExerciseStats() { ExerciseId = 1 , Reps = "2", Sets = 10, WeightInKg = 17.5f, WorkoutId = 1 };
            var x = new Workout { Date = DateTime.Now, UserId = 1, ExerciseStats1 = y };
            var workouts = new List<Workout>
            {
                x

            };

            workouts.ForEach(e => context.Workouts.Add(e));
            context.SaveChanges();
        }
    }
}