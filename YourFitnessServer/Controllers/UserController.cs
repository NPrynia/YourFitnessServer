using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using YourFitnessServer.Models;

namespace YourFitnessServer.Controllers
{
    public class UserController : ApiController
    {

       
        public User Get(int id)
        {
            Appdata.refreshChanges();
            return Appdata.Context.User.FirstOrDefault(e => e.ID == id);
        }

        public IEnumerable<User> Get()
        {
            Appdata.refreshChanges();
            List<Models.User> user = new List<Models.User>();
            user = Appdata.Context.User.ToList();
            return user;
        }
        public HttpResponseMessage Post([FromBody] User user)
        {
            Appdata.refreshChanges();
            try
            {
                Appdata.Context.User.Add(user);
                Appdata.Context.SaveChanges();
                var request = Request.CreateResponse(HttpStatusCode.Created, user);
                request.Headers.Location = new Uri(Request.RequestUri +
                    user.ID.ToString());
                return request;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put([FromBody] User user)
        {
            try
            {
                var existingtUser = Appdata.Context.User.Where(s => s.ID == user.ID).FirstOrDefault();
                if (existingtUser != null)
                {
                    existingtUser.FirstName = user.FirstName;
                    existingtUser.SecondName = user.SecondName;
                    existingtUser.Description = user.Description;
                    existingtUser.ImageProfile = user.ImageProfile;
                    Appdata.Context.SaveChanges();
                }

                var message = Request.CreateResponse(HttpStatusCode.Created, user);
                message.Headers.Location = new Uri(Request.RequestUri +
                    user.ID.ToString());
                return message;

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }





    }
     
}
