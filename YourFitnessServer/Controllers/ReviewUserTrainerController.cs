using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using YourFitnessServer.Models;

namespace YourFitnessServer.Controllers
{
    public class ReviewUserTrainerController : ApiController
    {
        public List<ReviewUserTrainer> GetReviewUserTrainers(int idUser)
        {
            Appdata.refreshChanges();
            return Appdata.Context.ReviewUserTrainer.Where(e => e.IDUserTrainer == idUser).ToList();

        }

        public HttpResponseMessage PostReviewUserTrainers(ReviewUserTrainer review)
        {
            Appdata.refreshChanges();
            review.User1 = null;
            try
            {
                Appdata.Context.ReviewUserTrainer.Add(review);
                Appdata.Context.SaveChanges();
                var request = Request.CreateResponse(HttpStatusCode.Created, review);
                request.Headers.Location = new Uri(Request.RequestUri + "%" +
                    review.IDUserReview.ToString() + "%");
                return request;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage PutReview([FromBody] ReviewUserTrainer reviewUserTrainer)
        {
            try
            {
                var existingtTrainerReaction = Appdata.Context.ReviewUserTrainer.Where(s => s.IDUserTrainer == reviewUserTrainer.IDUserTrainer && s.IDUserReview == reviewUserTrainer.IDUserReview).FirstOrDefault();
                if (existingtTrainerReaction != null)
                {

                    existingtTrainerReaction.Review = reviewUserTrainer.Review;
                    existingtTrainerReaction.Rating = reviewUserTrainer.Rating;
                    Appdata.Context.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, existingtTrainerReaction);
                    message.Headers.Location = new Uri(Request.RequestUri +
                        reviewUserTrainer.IDUserTrainer.ToString());
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

        public HttpResponseMessage Delete(int IDUserReview, int IDTrainer)
        {

            try
            {
                var existingReview = Appdata.Context.ReviewUserTrainer.Where(s => s.IDUserReview == IDUserReview && s.IDUserTrainer == IDTrainer).FirstOrDefault();
                if (existingReview != null)
                {
                    Appdata.Context.ReviewUserTrainer.Remove(existingReview);

                    Appdata.Context.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, IDUserReview);
                    message.Headers.Location = new Uri(Request.RequestUri +
                        IDUserReview.ToString());
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
