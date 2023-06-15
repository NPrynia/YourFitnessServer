using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using YourFitnessServer.Models;

namespace YourFitnessServer.Controllers
{
    public class LikeWorkoutController : ApiController
    {
        public HttpResponseMessage Post([FromBody] LikeWorkout likeWorkout)
        {
            Appdata.refreshChanges();
            try
            {
                var existingtLike = Appdata.Context.LikeWorkout.Where(s => s.IDUser == likeWorkout.IDUser & s.IDWorkout == likeWorkout.IDWorkout).FirstOrDefault();

                if (existingtLike != null)
                {
                    existingtLike.IsLike = likeWorkout.IsLike;

                    Appdata.Context.SaveChanges();
                }
                else
                {
                    Appdata.Context.LikeWorkout.Add(likeWorkout);

                    Appdata.Context.SaveChanges();

                }

                var message = Request.CreateResponse(HttpStatusCode.Created, likeWorkout);
                message.Headers.Location = new Uri(Request.RequestUri +
                    likeWorkout.IDWorkout.ToString());
                return message;

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
