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
         public static UGCIEB ActiveUgcieb= ds.uGCIEBs.First();

       

        [HttpGet("/ugc/")]
        public IActionResult Index()
        {
            return View(new TopbarViewModel() {Name = ActiveUgcieb.FullName, ID =ActiveUgcieb.UgciebID});
        }

       [HttpGet("/ugc/upasp/")]
        public IActionResult UniversityPloAchievementSelectProgram()
        {
            var viewModel = new UniversityPloAchievementSelectProgram()
            {
                TopbarViewModel = new TopbarViewModel()
                {
                    Name = ActiveUgcieb.FullName,
                    ID = ActiveUgcieb.UgciebID
                },
                Programs = ds.programs
            };

            return View(viewModel);
              
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
       

        [HttpGet("/ugc/logout")]
        public IActionResult Logout()
        {
            return Redirect("/");
        }

    }
}