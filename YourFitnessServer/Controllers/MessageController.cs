using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using YourFitnessServer.Models;

namespace YourFitnessServer.Controllers
{
    public class MessageController : ApiController
    {

        public List<Message> Get(int idUserGet, int idUserSent, int qtyMessage)
        {
            try
            {
                var listMessage = Appdata.Context.Message.Where(e => (e.IDUserGet == idUserGet && e.IDUserSent == idUserSent) || (e.IDUserSent == idUserGet && e.IDUserGet == idUserSent)).ToList();

                if (listMessage.Count() != qtyMessage)
                {

                    Appdata.refreshChanges();
                    return listMessage;
                }
                else
                {
                    return null;
                }
            }
            catch 
            {
                return null;
            }
            
        }

        [Route("api/Message/listIdUser/{idUser?}")]
        public List<int> GetIdUserWithMessage(int idUser)
        {
            try
            {
                var listIdUser = Appdata.Context.Message.Where(e => e.IDUserSent == idUser).Select(x => new { x.IDUserGet }).ToList();
                return (from i in listIdUser
                        select i.IDUserGet).Distinct().ToList();


            }
            catch 
            {
                return null;
            }
        }


        public HttpResponseMessage Post(Message message)
        {
            Appdata.refreshChanges();
            try
            {
                Appdata.Context.Message.Add(message);
                Appdata.Context.SaveChanges();
                var request = Request.CreateResponse(HttpStatusCode.Created, message);
                request.Headers.Location = new Uri(Request.RequestUri + "%" +
                    message.ID.ToString() + "%");
                return request;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }




        }


    }



}
