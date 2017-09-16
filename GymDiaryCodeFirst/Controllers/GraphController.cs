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
    public class DataPoint
    {
        public float Y;
        public string X;
        public string ExerciseName;
        public string tooltipMessage;

    }
    public class GraphController : ApiController
    {
        private GymDiaryContext db = new GymDiaryContext();

        public string Get([FromUri]int primaryMuscleId)
        {
            var listOfDataForEachLine = new List<List<DataPoint>>();
            var exercises = GetPossibleExercisesIds(primaryMuscleId);
            foreach(var exerciseId in exercises)
            {
               var lineData = FormatData(GetSetsData(8, exerciseId), exerciseId);
               listOfDataForEachLine.Add(lineData);
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
            var setsPerExercisePerWorkout = new List<List<Set>>();

            //adds new list of set data for each exercise for user.
            foreach (var id in exerciseStatsIds)
            {
                var set = db.Sets.Where(x => x.ExerciseStat.ExerciseStatsId == id).ToList();
                setsPerExercisePerWorkout.Add(set);
            }


            return setsPerExercisePerWorkout;
        }

        public string GetExerciseName(int exerciseId)
        {
           return db.Exercises.Single(x => x.Id == exerciseId).Name;
        }
        public List<DataPoint> FormatData(List<List<Set>> ExerciseSets, int exerciseId)
        {
            //int will be the average set/rep count (x axis)
            //datetime is when the workout was completed
            //string will be the info that appears in the tooltip on the graph
            var datapoints = new List<DataPoint>();
            var exerciseName = GetExerciseName(exerciseId);
            foreach (var exercise in ExerciseSets)
            {
                int totalOverallReps = 0;
                float totalWeightLifted = 0;
                float avgStrength;
                DateTime completionDate = new DateTime();
                int exerciseStatIdForCompletionDate = 0;
                string tooltipInfo = "";
                foreach(var set in exercise)
                {
                    //tool tip info
                    tooltipInfo += String.Format("Weight:{1}kg Reps:{0}<br />", set.Reps, set.WeightInKg);

                    totalOverallReps += set.Reps.Value;
                    totalWeightLifted += (set.WeightInKg.Value * (float)set.Reps.Value);
                    exerciseStatIdForCompletionDate = (int)set.ExerciseStat.ExerciseStatsId;
                }
                //the avg
                avgStrength = totalWeightLifted;
                
                //the datetime
                if (exerciseStatIdForCompletionDate > 0)
                {
                    var workoutId = db.ExerciseStats.Single(x => x.ExerciseStatsId == exerciseStatIdForCompletionDate).WorkoutId;
                    completionDate = (DateTime)db.Workouts.Single(x => x.WorkoutId == workoutId).Date;
                }

                //construct the data to form a datapoint and add it to list
                datapoints.Add(new DataPoint() {
                    Y = avgStrength,
                    X = completionDate.ToString("yyyy,MM,dd,HH,mm,ss"),
                    tooltipMessage = tooltipInfo,
                    ExerciseName = exerciseName
                });
            }
            return datapoints;
        }
        
    }
}
