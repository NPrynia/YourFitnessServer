using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using YourFitnessServer.Models;

namespace YourFitnessServer.Controllers
{
    public class WorkoutController : ApiController
    {
        public IEnumerable<Workout> Get()
        {

            Appdata.refreshChanges();
            List<Models.Workout> workouts = new List<Models.Workout>();
            workouts = Appdata.Context.Workout.ToList();
            return workouts;
        }


        [System.Web.Http.Route("api/Workout/GetImage/{fileName}")]
        public HttpResponseMessage Get(string fileName)
        {
            fileName = fileName.Replace('-', '.');
            if (!string.IsNullOrEmpty(fileName))
            {
                string fullPath = $"D:\\YourFitness\\WorkoutImage\\{fileName}";
                if (File.Exists(fullPath))
                {

                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    var fileStream = new FileStream(fullPath, FileMode.Open);
                    response.Content = new StreamContent(fileStream);
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                    response.Content.Headers.ContentDisposition.FileName = $"{fileName}";
                    return response;
                }
            }

            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }




        public HttpResponseMessage Post([FromBody] Workout workout)
        {
            Appdata.refreshChanges();
            try
            {
                Appdata.Context.Workout.Add(workout);
                
                if (workout.ExerciseInWorkout.Last().IDWorkout != workout.ID)
                {

                    workout.ExerciseInWorkout.ForEach(x => x.IDWorkout = workout.ID);
                    Appdata.Context.SaveChanges();
                }
                workout.ExerciseWorkout = workout.ExerciseInWorkout;
                Appdata.Context.SaveChanges();
                if (workout.ImagePath != null)
                {
                    workout.ImagePath = ("D:\\YourFitness\\WorkoutImage\\" + workout.ID + ".jpg");
                    Appdata.Context.SaveChanges();
                }


                var request = Request.CreateResponse(HttpStatusCode.Created, workout);
                request.Headers.Location = new Uri(Request.RequestUri + "%" +
                    workout.ID.ToString() + "%");
                var httpRequest = HttpContext.Current.Request;

               


                return request;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        [System.Web.Http.Route("api/Workout/PostImage/{idWorkout}")]
        public HttpResponseMessage Post(int idWorkout)
        {
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                foreach (string fileName in httpRequest.Files.Keys)
                {
                    var file = httpRequest.Files[fileName];
                    file.SaveAs("D:\\YourFitness\\WorkoutImage\\" + idWorkout +".jpg");
                }

                return Request.CreateResponse(HttpStatusCode.Created);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }



        [System.Web.Http.Route("api/Workout/GetTypeWorkout")]
        public IEnumerable<WorkoutType> GetTypeWorkout()
        {
            try
            {
                Appdata.refreshChanges();
                List<Models.WorkoutType> workoutType = new List<Models.WorkoutType>();
                workoutType = Appdata.Context.WorkoutType.ToList();
                return workoutType;
            }
            catch
            {
                return null;
            }
          
        }

    }
}
