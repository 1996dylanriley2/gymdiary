using GymDiaryCodeFirst.DAL;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GymDiaryCodeFirst.Controllers.api
{
    public class GraphController1 : ApiController
    {
        private GymDiaryContext db = new GymDiaryContext();
        // GET api/<controller>
        public Dictionary<string, string> Get()
        {
            var data = new Dictionary<string, string>();
            var userID = User.Identity.GetUserId();
            var userWorkout = db.Workouts.Where(p => p.UserId == userID);
            foreach(var workout in userWorkout)
            {
                var exercisesInWorkout = db.ExerciseStats.Where(j => j.WorkoutId == workout.WorkoutId);
                foreach(var exercise in exercisesInWorkout)
                {
                    data.Add(exercise.Sets.ToString(), workout.Date.ToString());
                }
                
            }
            
            return data;
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}