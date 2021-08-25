using Microsoft.AspNetCore.Mvc;
using SpmsApp.ViewModels;
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
            return View(new TopbarViewModel() {Name = "No Name Set", ID = 0000});
        }
        
        [HttpGet("/department/SPCC")]
        public IActionResult StudentPLOComparisonCourseWise()
        {
            return View(new TopbarViewModel() {Name = "No Name Set", ID = 0000});
        }
        [HttpGet("/department/SPCP")]
        public IActionResult StudentPLOComparisonProgramWise()
        {
            return View(new TopbarViewModel() {Name = "No Name Set", ID = 0000});
        }
        

        [HttpGet("/department/SPAT")]
        public IActionResult StudentPLOAchievementTable()
        {
            return View(new TopbarViewModel() {Name = "No Name Set", ID = 0000});
        }

        [HttpGet("/department/CPPF")]
        public IActionResult CoursePLOPerformanceFacultyWise()
        {
            return View(new TopbarViewModel() {Name = "No Name Set", ID = 0000});
        }


        [HttpGet("/department/PWCP")]
        public IActionResult PLOWiseCoursePerformance()
        {
            return View(new TopbarViewModel() {Name = "No Name Set", ID = 0000});
        }

        

        [HttpGet("/department/AAC")]
        public IActionResult AttemptedVsAchievedComparison()
        {
            return View(new TopbarViewModel() {Name = "No Name Set", ID = 0000});
        }       

        [HttpGet("/department/AFPP")]
        public IActionResult AchievedVsFailedPLOPerformance()
        {
            return View(new TopbarViewModel() {Name = "No Name Set", ID = 0000});
        }
        

        [HttpGet("/department/PAP")]
        public IActionResult PLOAchievementForAProgram()
        {
            return View(new TopbarViewModel() {Name = "No Name Set", ID = 0000});
        }


        [HttpGet("/department/SGAP")]
        public IActionResult StudentsOrGraduatesAchievingAllPLOS()
        {
            return View(new TopbarViewModel() {Name = "No Name Set", ID = 0000});
        }
  

        [HttpGet("/department/IPPC")]
        public IActionResult InstructorWisePLOPerformanceComparison()
        {
            return View(new TopbarViewModel() {Name = "No Name Set", ID = 0000});
        }
    }
}
