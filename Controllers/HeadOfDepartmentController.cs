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
        [HttpGet("/department/SPCP")]
        public IActionResult StudentPLOComparisonProgramWise()
        {
            return View();
        }
        

        [HttpGet("/department/SPAT")]
        public IActionResult StudentPLOAchievementTable()
        {
            return View();
        }

        [HttpGet("/department/CPPF")]
        public IActionResult CoursePLOPerformanceFacultyWise()
        {
            return View();
        }


        [HttpGet("/department/PWCP")]
        public IActionResult PLOWiseCoursePerformance()
        {
            return View();
        }

        

        [HttpGet("/department/AAC")]
        public IActionResult AttemptedVsAchievedComparison()
        {
            return View();
        }       

        [HttpGet("/department/AFPP")]
        public IActionResult AchievedVsFailedPLOPerformance()
        {
            return View();
        }
        

        [HttpGet("/department/PAP")]
        public IActionResult PLOAchievementForAProgram()
        {
            return View();
        }


        [HttpGet("/department/SGAP")]
        public IActionResult StudentsOrGraduatesAchievingAllPLOS()
        {
            return View();
        }
  

        [HttpGet("/department/IPPC")]
        public IActionResult InstructorWisePLOPerformanceComparison()
        {
            return View();
        }
    }
}
