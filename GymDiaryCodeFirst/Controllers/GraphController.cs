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
            //This for now will provide us with the past 8 workouts for that muscle group. I will work out an avg for reps/set and use that as the data point
            //then it will give the point info in the hover box: this includes date,reps,sets,weight goal achieved or not.
            //the axis will be strength/duration,time.

            var userID = User.Identity.GetUserId();
            var possibleExercises = db.Exercises.Where(x => x.PrimaryMuscleId == primaryMuscleId).ToList();
            List<List<GraphApiViewModel>> listsToConvertToJson = new List<List<GraphApiViewModel>>();

            //This loop gets the 8 most recent competed exercisedstats for the current user for all exercises within the correct  muscle group.
            for (var exercise = 0; exercise < possibleExercises.Count; ++exercise)
            {
                var exerciseId = possibleExercises[exercise].Id;
                //Need to now split the exercises up
                var get8MostRecentForMuscleId = @"select top 8 result1.Date, result1.DesiredSetCount, result1.Minutes, result1.Name,result1.Reps,result1.WeightInKg,result1.Id
                                from (select result.*
                                from (select exerciseStats.*, Exercises.*,workouts.Date, [Sets].Minutes,[Sets].Reps,[Sets].WeightInKg
	                                from Workouts as workouts
	                                 full join ExerciseStats as exerciseStats on exercisestats.workoutId = workouts.WorkoutId
	                                 full join Exercises on Exercises.Id = $$$
	                                 full join [Sets] on [Sets].ExerciseStats_ExerciseStatsId = ExerciseStats.ExerciseStatsId
	                                where workouts.UserId = '***' and PrimaryMuscleId = ### and Workouts.IsBaseWorkout = 0) as result

                                where result.PrimaryMuscleId = ### ) as result1
                                order by result1.Date DESC";

                get8MostRecentForMuscleId = get8MostRecentForMuscleId.Replace("$$$", exerciseId.ToString());
                get8MostRecentForMuscleId = get8MostRecentForMuscleId.Replace("###", primaryMuscleId.ToString());
                get8MostRecentForMuscleId = get8MostRecentForMuscleId.Replace("***", userID.ToString());

                List<GraphApiViewModel> latest8ExerciseStats = db.ApiViewModel.SqlQuery(get8MostRecentForMuscleId).ToList();

                listsToConvertToJson.Add(latest8ExerciseStats);

                //if((exercise == 0 && possibleExercises.Count == 1) || possibleExercises.Count == 1 || exercise == possibleExercises.Count - 1)
                //    json += JsonConvert.SerializeObject(latest8ExerciseStats, Formatting.Indented);
                //else
                //    json += JsonConvert.SerializeObject(latest8ExerciseStats, Formatting.Indented) + ",";
            }

            var json = JsonConvert.SerializeObject(listsToConvertToJson);

            return json;
        }
    }
}
