using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using YourFitnessServer.Models;

namespace YourFitnessServer.Controllers
{
    public class NewsCommentController : ApiController
    {

        public HttpResponseMessage Post([FromBody] NewsComment newsComment)
        {
            Appdata.refreshChanges();
            try
            {
                newsComment.User = null;
                Appdata.Context.NewsComment.Add(newsComment);
                Appdata.Context.SaveChanges();
                var request = Request.CreateResponse(HttpStatusCode.Created, newsComment);
                request.Headers.Location = new Uri(Request.RequestUri +
                    newsComment.ID.ToString());
                return request;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

    }
}
