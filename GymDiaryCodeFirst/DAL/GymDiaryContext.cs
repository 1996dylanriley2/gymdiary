using GymDiaryCodeFirst.Models;
using GymDiaryCodeFirst.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace GymDiaryCodeFirst.DAL
{
    public class GymDiaryContext:DbContext
    {
        public GymDiaryContext():base("GymDiaryCF")
        {

        }
        
        public DbSet<Muscle> Muscles { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Workout> Workouts{ get; set; }
        public DbSet<ExerciseStats> ExerciseStats{ get; set; }
        public DbSet<Set> Sets { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //}


    }
}