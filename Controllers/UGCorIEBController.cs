using Microsoft.AspNetCore.Mvc;
using SpmsApp.ViewModels;
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
            return View(new TopbarViewModel() {Name = "No Name Set", ID = 0000});
        }

        [HttpGet("/ugc/pafap")]
        public IActionResult PLOAchievementForAProgram()
        {
            return View(new TopbarViewModel() {Name = "No Name Set", ID = 0000});
        }
        [HttpGet("/ugc/pwcp")]
        public IActionResult PLOwiseCoursePerformance()
        {
            return View(new TopbarViewModel() {Name = "No Name Set", ID = 0000});
        }
          [HttpGet("/ugc/sogaap")]
        public IActionResult StudentsOrGraduatesAchievingAllPLOs()
        {
            return View(new TopbarViewModel() {Name = "No Name Set", ID = 0000});
        }
       



    }
}