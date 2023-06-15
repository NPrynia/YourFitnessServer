using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using YourFitnessServer.Models;

namespace YourFitnessServer.Controllers
{
    public class HistoryChangeController : ApiController
    {

        [System.Web.Http.HttpGet]
        public IEnumerable<HistoryChange> Get()
        {
            List<Models.HistoryChange> historyChange = new List<Models.HistoryChange>();
            Appdata.refreshChanges();
            historyChange = Appdata.Context.HistoryChange.ToList();
            return historyChange;
        }
        public List<HistoryChange> Get(int idUser)
        {
            Appdata.refreshChanges();
            return Appdata.Context.HistoryChange.Where(e => e.IDUser == idUser).ToList();
        }


        public HttpResponseMessage Post([FromBody] HistoryChange historyChange)
        {
            Appdata.refreshChanges();
            try
            {
                Appdata.Context.HistoryChange.Add(historyChange);
                Appdata.Context.SaveChanges();
                var request = Request.CreateResponse(HttpStatusCode.Created, historyChange);
                request.Headers.Location = new Uri(Request.RequestUri +
                    historyChange.ID.ToString());
                return request;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }


        }
    }
}
