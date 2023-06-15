using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using YourFitnessServer.Models;

namespace YourFitnessServer.Controllers
{
    public class ExerciseController : ApiController
    {
        [System.Web.Http.HttpGet]
        public IEnumerable<Exercise> Get()
        {
            Appdata.refreshChanges();
            List<Models.Exercise> exercise = new List<Models.Exercise>();
            Appdata.refreshChanges();
            exercise = Appdata.Context.Exercise.ToList();
            return exercise;
        }
        public Exercise Get(int id)
        {
            Appdata.refreshChanges();
            return Appdata.Context.Exercise.FirstOrDefault(e => e.ID == id);
        }


        //public HttpResponseMessage Post([FromBody] Message message)
        //{
        //    try
        //    {
        //        Appdata.Context.Message.Add(message);
        //        Appdata.Context.SaveChanges();
        //        var request = Request.CreateResponse(HttpStatusCode.Created, message);
        //        request.Headers.Location = new Uri(Request.RequestUri +
        //            message.ID.ToString());
        //        return request;
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
        //    }


        //}
    }
}
