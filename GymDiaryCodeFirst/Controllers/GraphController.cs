using GymDiaryCodeFirst.DAL;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

namespace GymDiaryCodeFirst.Controllers
{
    public class GraphController : ApiController
    {
        private GymDiaryContext db = new GymDiaryContext();
        public string Get()
        {
            //This for now will provide us with the past 8 workouts for that muscle group. I will work out an avg for reps/set and use that as the data point
            //then it will give the point info in the hover box: this includes date,reps,sets,weight goal achieved or not.
            //the axis will be strength,time.
            var data = new {x = new List<int>(), y = new List<int>() };
            var userID = User.Identity.GetUserId();
            var userWorkout = db.Workouts.Where(w => w.UserId == userID && w.IsBaseWorkout == false).ToList();
            foreach (var workout in userWorkout)
            {
                var exercisesInWorkout = db.ExerciseStats.Where(j => j.WorkoutId == workout.WorkoutId);
                foreach (var exercise in exercisesInWorkout)
                {
                    //data.x.Add((int)exercise.Reps);
                    //data.y.Add(workout.Date.Value.Month);
                }
                

            }
            var json = JsonConvert.SerializeObject(data);

            
            return json;
        }
        public string Get(int i)
        {
            return "success";
        }
    }
}
