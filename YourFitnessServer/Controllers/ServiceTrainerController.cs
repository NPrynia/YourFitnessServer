using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using YourFitnessServer.Models;

namespace YourFitnessServer.Controllers
{
    public class ServiceTrainerController : ApiController
    {
        public List<Service> Get(int idUser)
        {
            Appdata.refreshChanges();
            return Appdata.Context.Service.Where(e => e.IDUser == idUser).ToList();
        }

        public HttpResponseMessage Post(Service service)
        {
            Appdata.refreshChanges();
            try
            {
                Appdata.Context.Service.Add(service);
                Appdata.Context.SaveChanges();
                var request = Request.CreateResponse(HttpStatusCode.Created, service);
                request.Headers.Location = new Uri(Request.RequestUri + "%" +
                    service.ID.ToString() + "%");
                return request;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        public HttpResponseMessage Put([FromBody] Service service)
        {
            try
            {
                var existingService = Appdata.Context.Service.Where(s => s.ID == service.ID).FirstOrDefault();
                if (existingService != null)
                {

                    existingService.Price = service.Price;
                    existingService.Name = service.Name;
                    existingService.Description = service.Description;
                    Appdata.Context.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, service);
                    message.Headers.Location = new Uri(Request.RequestUri +
                        service.ID.ToString());
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




        public HttpResponseMessage Delete(int IdService)
        {

            try
            {
                var existingService = Appdata.Context.Service.Where(s => s.ID == IdService).FirstOrDefault();
                if (existingService != null)
                {
                    Appdata.Context.Service.Remove(existingService);

                    Appdata.Context.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, IdService);
                    message.Headers.Location = new Uri(Request.RequestUri +
                        IdService.ToString());
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

    }
}