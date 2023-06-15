using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using YourFitnessServer.Models;

namespace YourFitnessServer.Controllers
{
    public class HistoryWorkoutUserController : ApiController
    {

        public List<HistoryWorkoutUser> Get(int idUser)
        {
            Appdata.refreshChanges();
            return Appdata.Context.HistoryWorkoutUser.Where(e => e.IDUser == idUser).ToList();
        }


        public HttpResponseMessage Post([FromBody] HistoryWorkoutUser hwu)
        {
            Appdata.refreshChanges();
            try
            {
                Appdata.Context.HistoryWorkoutUser.Add(hwu);
                Appdata.Context.SaveChanges();
                var request = Request.CreateResponse(HttpStatusCode.Created, hwu);
                request.Headers.Location = new Uri(Request.RequestUri +
                    hwu.ID.ToString());
                return request;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
