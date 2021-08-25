using Microsoft.AspNetCore.Mvc;
using SpmsApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpmsApp.Controllers
{
        public class StudentController : Controller
    {
        [HttpGet("/student/")]
        public IActionResult Index()
        {
            return View(new TopbarViewModel() {Name = "No Name Set", ID = 0000});

        }

        [HttpGet("/student/cpc/")]
        public IActionResult CoursePLOComparison()
        {
            return View(new TopbarViewModel() {Name = "No Name Set", ID = 0000});

        }

        [HttpGet("/student/ppc/")]
        public IActionResult ProgramPLOComparison()
        {
            return View(new TopbarViewModel() {Name = "No Name Set", ID = 0000});

        }

        [HttpGet("/student/pat/")]
        public IActionResult PLOAchievementTable()
        {
            return View(new TopbarViewModel() {Name = "No Name Set", ID = 0000});

        }




    }
}
