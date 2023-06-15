using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using YourFitnessServer.Models;

namespace YourFitnessServer.Controllers
{
    public class LikeNewsUserController : ApiController
    {

        public HttpResponseMessage Post([FromBody] LikeNewsUser likeNewsUser)
        {
            Appdata.refreshChanges();
            try
            {
                var existingtLike = Appdata.Context.LikeNewsUser.Where(s => s.IDUser == likeNewsUser.IDUser & s.IDNews == likeNewsUser.IDNews ).FirstOrDefault();

                if (existingtLike != null)
                {
                    existingtLike.IsLike = likeNewsUser.IsLike;

                    Appdata.Context.SaveChanges();
                }
                else
                {
                    Appdata.Context.LikeNewsUser.Add(likeNewsUser);

                    Appdata.Context.SaveChanges();

                }

                var message = Request.CreateResponse(HttpStatusCode.Created, likeNewsUser);
                message.Headers.Location = new Uri(Request.RequestUri +
                    likeNewsUser.IDNews.ToString());
                return message;

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

    }
}
