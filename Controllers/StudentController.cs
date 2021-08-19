using Microsoft.AspNetCore.Mvc;
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
            return View();

        }

        [HttpGet("/student/cpc/")]
        public IActionResult CoursePLOComparison()
        {
            return View();

        }

        [HttpGet("/student/ppc/")]
        public IActionResult ProgramPLOComparison()
        {
            return View();

        }

        [HttpGet("/student/pat/")]
        public IActionResult PLOAchievementTable()
        {
            return View();

        }




    }
}
