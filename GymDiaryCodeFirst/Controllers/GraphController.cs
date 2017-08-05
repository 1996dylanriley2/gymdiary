using GymDiaryCodeFirst.DAL;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using GymDiaryCodeFirst.Models;
using System.ComponentModel.DataAnnotations;
using GymDiaryCodeFirst.Models.ViewModels;

namespace GymDiaryCodeFirst.Controllers
{
    public class GraphController : ApiController
    {
        private GymDiaryContext db = new GymDiaryContext();

        public string Get([FromUri]int primaryMuscleId)
        {
            var listOfDataForEachLine = new List<List<Tuple<int, DateTime, string>>>();
            var exercises = GetPossibleExercisesIds(primaryMuscleId);
            foreach(var exerciseId in exercises)
            {
               var lineData = FormatData(GetSetsData(8, exerciseId));
            }
            return JsonConvert.SerializeObject(listOfDataForEachLine, Formatting.Indented);
        }
        private List<int> GetPossibleExercisesIds(int primaryMuscleId)
        {
            var query = @"SELECT distinct *
                          FROM Exercises
                          full join ExerciseStats on ExerciseStats.ExerciseId = Exercises.Id
                          full join Workouts on Workouts.WorkoutId = ExerciseStats.WorkoutId
                          full join AspNetUsers on AspNetUsers.Id = Workouts.UserId
                          where Exercises.PrimaryMuscleId = $$$ and Workouts.IsBaseWorkout = 0 and Workouts.UserId = '***'";

            query = query.Replace("$$$", primaryMuscleId.ToString());
            query = query.Replace("***", User.Identity.GetUserId());

            return db.Exercises.SqlQuery(query).Select(x => x.Id).Distinct().ToList();
        }
       
        //This method gets X most recent exerciseStatsId for the specified exercise with the date.
        public List<int> GetXMostRecentExerciseStatsIdsWithCompletionDate (int x, int exerciseId)
        {
            var query = @"select top $$$ *
                        from Workouts
                        full join ExerciseStats on Workouts.WorkoutId = ExerciseStats.WorkoutId
                        where workouts.UserId = '***' and ExerciseStats.ExerciseId = ###
                        order by Workouts.Date DESC";

            query = query.Replace("$$$", x.ToString());
            query = query.Replace("***", User.Identity.GetUserId());
            query = query.Replace("###", exerciseId.ToString());

            return db.ExerciseStats.SqlQuery(query).Select(e => e.ExerciseStatsId).ToList();
        }

        public List<List<Set>> GetSetsData(int countOfPreviousWorkoutsToShow, int exerciseId)
        {
            var exerciseStatsIds = GetXMostRecentExerciseStatsIdsWithCompletionDate(countOfPreviousWorkoutsToShow, exerciseId);
            var setsDataPerExercise = new List<List<Set>>();

            //adds new list of set data for each exercise for user.
            foreach (var id in exerciseStatsIds)
            {
                var p = db.Sets.Where(x => x.ExerciseStat.ExerciseStatsId == id).ToList();
                setsDataPerExercise.Add(p);
            }


            return setsDataPerExercise;
        }

        public List<Tuple<int, DateTime, string>> FormatData(List<List<Set>> ExerciseSets)
        {
            //int will be the average set/rep count (x axis)
            //datetime is when the workout was completed
            //string will be the info that appears in the tooltip on the graph
            var datapoints = new List<Tuple<int, DateTime, string>>();

            foreach(var exercise in ExerciseSets)
            {
                int sum = 0;
                DateTime completionDate = new DateTime();
                int exerciseStatIdForCompletionDate = 0;
                string tooltipInfo = "Reps/Weight in Kgs";
                foreach(var set in exercise)
                {
                    //tool tip info
                    tooltipInfo += String.Format("\n{0} reps on {1}kg", set.Reps, set.WeightInKg);

                    sum += set.Reps.Value;
                    exerciseStatIdForCompletionDate = (int)set.ExerciseStat.ExerciseStatsId;
                }
                //the avg
                sum = sum / exercise.Count + 1;

                //the datetime
                if (exerciseStatIdForCompletionDate > 0)
                {
                    var workoutId = db.ExerciseStats.Single(x => x.ExerciseStatsId == exerciseStatIdForCompletionDate).WorkoutId;
                    completionDate = (DateTime)db.Workouts.Single(x => x.WorkoutId == workoutId).Date;
                }

                //construct the data to form a datapoint and add it to list
                datapoints.Add(new Tuple<int, DateTime, string>(sum, completionDate, tooltipInfo));

            }
            return datapoints;
        }
        
    }
}
