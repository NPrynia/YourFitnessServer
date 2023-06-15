using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using YourFitnessServer.Models;

namespace YourFitnessServer.Controllers
{
    public class WorkoutReactionController : ApiController
    {
        public HttpResponseMessage Post([FromBody] WorkoutReaction workoutReaction)
        {
            Appdata.refreshChanges();
            try
            {
                workoutReaction.User = null;
                workoutReaction.Workout = null;
                Appdata.Context.WorkoutReaction.Add(workoutReaction);
                Appdata.Context.SaveChanges();
                var request = Request.CreateResponse(HttpStatusCode.Created, workoutReaction);
                request.Headers.Location = new Uri(Request.RequestUri +
                    workoutReaction.IDUser.ToString());
                return request;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        public HttpResponseMessage Put([FromBody] WorkoutReaction workoutReaction)
        {
            try
            {
                var existingtworkoutReaction = Appdata.Context.WorkoutReaction.Where(s => s.IDUser == workoutReaction.IDUser && s.IDWorkout == workoutReaction.IDWorkout ).FirstOrDefault();
                if (existingtworkoutReaction != null)
                {

                    existingtworkoutReaction.Review = workoutReaction.Review;
                    existingtworkoutReaction.Rating = workoutReaction.Rating;
                    existingtworkoutReaction.TimeSent = workoutReaction.TimeSent;
                    Appdata.Context.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, workoutReaction);
                    message.Headers.Location = new Uri(Request.RequestUri +
                        workoutReaction.IDUser.ToString());
                    return message;
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "not found");
                }

             

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }


        }


        public HttpResponseMessage Delete(int IDUser , int IDWorkout )
        {

            try
            {
                var existingtworkoutReaction = Appdata.Context.WorkoutReaction.Where(s => s.IDUser == IDUser && s.IDWorkout == IDWorkout).FirstOrDefault();
                if (existingtworkoutReaction != null)
                {
                    Appdata.Context.WorkoutReaction.Remove(existingtworkoutReaction);

                    Appdata.Context.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, IDUser);
                    message.Headers.Location = new Uri(Request.RequestUri +
                        IDUser.ToString());
                    return message;
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest,"not found");
                }
               

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
