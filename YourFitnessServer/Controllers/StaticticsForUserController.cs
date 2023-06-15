using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using YourFitnessServer.Models;

namespace YourFitnessServer.Controllers
{
    public class StaticticsForUserController : ApiController
    {

        [Route("api/StaticticsForUserController/StatsMuscle/{idUser?}")]
        public List<qtyHourOnMuscleForUser_Result> GetStatsMuscle(int? idUser)
        {
            Appdata.refreshChanges();
            return Appdata.Context.qtyHourOnMuscleForUser(idUser).ToList();
        }

        [Route("api/StaticticsForUserController/StatsWeek/{idUser?}")]
        public List<qtyHourWorkoutUser_Result> GetStatsForWeek(int? idUser)
        {
            Appdata.refreshChanges();
            return Appdata.Context.qtyHourWorkoutUser(idUser, DateTime.Today.AddDays(-30), DateTime.Today.AddDays(1)).ToList();
        }
    }
}
