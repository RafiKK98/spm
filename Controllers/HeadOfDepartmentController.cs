using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpmsApp.Controllers
{
    public class HeadOfDepartmentController : Controller
    {
        [HttpGet("/department/")]
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet("/department/SPCC")]
        public IActionResult StudentPLOComparisonCourseWise()
        {
            return View();
        }
    }
}
