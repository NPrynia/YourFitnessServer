using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using YourFitnessServer.Models;

namespace YourFitnessServer.Controllers
{
    public class NewsController : ApiController
    {



        public IEnumerable<News> Get()
        {

            Appdata.refreshChanges();
            List<Models.News> news = new List<Models.News>();
            news = Appdata.Context.News.ToList();
            return news.OrderByDescending(i => i.timeCreate);
        }

        public News Get(int id)
        {
            Appdata.refreshChanges();
            return Appdata.Context.News.FirstOrDefault(e => e.IDUser == id);
        }

        [System.Web.Http.Route("api/News/GetImage/{fileName}")]
        public HttpResponseMessage Get(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                string fullPath = $"D:\\{fileName}.jpg";
                if (File.Exists(fullPath))
                {

                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    var fileStream = new FileStream(fullPath, FileMode.Open);
                    response.Content = new StreamContent(fileStream);
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                    response.Content.Headers.ContentDisposition.FileName = $"{fileName}.jpg";
                    return response;
                }
            }

            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }

       

        public HttpResponseMessage Post(News news)
        {
            Appdata.refreshChanges();
            try
            {
                
                news.User = null;
                news.Workout = null;
                Appdata.Context.News.Add(news);
                Appdata.Context.SaveChanges();
                var request = Request.CreateResponse(HttpStatusCode.Created, news);
                request.Headers.Location = new Uri(Request.RequestUri +"%"+
                    news.ID.ToString()+"%");
                return request;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

            
              
           
        }




        public HttpResponseMessage Delete(int idNews)
        {

            try
            {
                var existingNews = Appdata.Context.News.Where(s => s.ID == idNews).FirstOrDefault();
                if (existingNews != null)
                {
                    Appdata.Context.News.Remove(existingNews);

                    Appdata.Context.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, idNews);
                    message.Headers.Location = new Uri(Request.RequestUri +
                        idNews.ToString());
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


        //public HttpResponseMessage Put([FromBody] News news)
        //{
        //    try
        //    {
        //        var existingtNews = Appdata.Context.News.Where(s => s.ID == news.ID).FirstOrDefault();

        //        if (existingtNews != null)
        //        {
        //            existingtNews.QtyLike = news.Name;

        //            Appdata.Context.SaveChanges();
        //        }

        //        var message = Request.CreateResponse(HttpStatusCode.Created, user);
        //        message.Headers.Location = new Uri(Request.RequestUri +
        //            user.ID.ToString());
        //        return message;

        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
        //    }
        //}
    }
}
