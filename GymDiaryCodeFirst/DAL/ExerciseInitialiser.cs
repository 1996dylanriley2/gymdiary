using GymDiaryCodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GymDiaryCodeFirst.DAL
{
    public class ExerciseInitialiser : System.Data.Entity.DropCreateDatabaseIfModelChanges<GymDiaryContext>
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
            new Exercise{Name="squat", PrimaryMuscleId=3, SecondaryMuscleId=null},
            new Exercise{Name="leg press", PrimaryMuscleId=3, SecondaryMuscleId=null}

            };

            exercises.ForEach(e => context.Exercises.Add(e));
            context.SaveChanges();

            var x = new Workout { Date = DateTime.Now, Name = "Test Workout", UserId = "1a", Exercises = new List<ExerciseStats> { new ExerciseStats { ExerciseId = 1, DesiredSet = new Set() { Reps = 2, WeightInKg = 5 }, DesiredSetCount = 3} } };
            var workouts = new List<Workout>
            {
                x

            };

            workouts.ForEach(e => context.Workouts.Add(e));
            context.SaveChanges();

        }
    }
}