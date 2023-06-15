using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace YourFitnessServer.Controllers
{
    public class ImageUserController : ApiController
    {

        public String Get()
        {
            byte[] bytes = File.ReadAllBytes("D:\\1.jpg");
            return  Convert.ToBase64String(bytes);
        }


        public HttpResponseMessage Post()
        {
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                foreach (string fileName in httpRequest.Files.Keys)
                {
                    var file = httpRequest.Files[fileName];
                    file.SaveAs("D:\\userProfileFile\\" + file.FileName);
                }

                return Request.CreateResponse(HttpStatusCode.Created);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
