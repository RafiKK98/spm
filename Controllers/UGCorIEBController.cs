using Microsoft.AspNetCore.Mvc;
using SpmsApp.ViewModels;
using SpmsApp.Models;
using SpmsApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpmsApp.Controllers
{
    public class UGCorIEBController : Controller
    {
        public static DataServices ds= DataServices.dataServices;

        public static UGCIEB ActiveUGCIEB = new Ugcieb()
        {
            ID = 15000,
            FirstName = "Kazi",
            LastName = "Shahidullah",
            ContactNumber = "01853463458",
            EmailAddress = "kazi@iub.edu.bd",
            Address = "Mirpur Dhaka",
            UgciebID= 402,
            University= ds.universities.First()

        }; 

     

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