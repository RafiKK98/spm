using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpmsApp.Controllers
{
    public class UGCorIEBController : Controller
    {
     

        [HttpGet("/ugc/")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/ugc/pafap")]
        public IActionResult PLOAchievementForAProgram()
        {
            return View();
        }
        [HttpGet("/ugc/pwcp")]
        public IActionResult PLOwiseCoursePerformance()
        {
            return View();
        }
        [HttpGet("/ugc/sogaap")]
        public IActionResult StudentsOrGraduatesAchievingAllPLOs()
        {
            return View();
        }



    }
}