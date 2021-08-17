using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpmsApp.Controllers
{
    public class DeanController : Controller
    {
        [HttpGet("/dean/")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/dean/spcc/")]
        public IActionResult StudentPLOComparisonByCourse()
        {
            return Content("Hi, It is for course");
        }

        [HttpGet("/dean/spcp/")]
        public IActionResult StudentPLOComparisonByProgram()
        {
            return Content("Hi, It is for Program");
        }



    }
}
