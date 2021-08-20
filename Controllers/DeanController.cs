using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpmsApp.ViewModels;
using SpmsApp.Models;

namespace SpmsApp.Controllers
{
    
    
    public class DeanController : Controller
    {

        static CoursePloPerformanceOwnViewModel viewModel = new CoursePloPerformanceOwnViewModel();
        
        [HttpGet("/dean/")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/dean/spcc/")]
        public IActionResult StudentPLOComparisonByCourse()
        {
            return View();
        }

        [HttpGet("/dean/spcp/")]
        public IActionResult StudentPLOComparisonByProgram()
        {
            return View();
        }
        [HttpGet("/dean/spat/")]
        public IActionResult StudentPLOAchievementTable()
        {
            return View();
        }
        [HttpGet("/dean/cppf/")]
        public IActionResult CoursePLOPerformanceByFaculty()
        {
            viewModel.Courses = new List<Course>()
            {
                new Course()
                {
                    CourseID = 0,
                    CourseName = "Abc"
                },
                new Course()
                {
                    CourseID = 1,
                    CourseName = "Def"
                }
            };

            return View(viewModel);
        }

        [HttpGet("/dean/pcp/")]
        public IActionResult PLOwiseCoursePerformance()
        {
            return View();
        }

        [HttpGet("/dean/afpp/")]
        public IActionResult AchievedVsFailedPLOPerformance()
        {
            return View();
        }

        [HttpGet("/dean/aac/")]
        public IActionResult AttemptedVsAchievedComparison()
        {
            return View();
        }

        [HttpGet("/dean/pafap/")]
        public IActionResult PLOAchievementForAProgram()
        {
            //
            return View();
        }

        [HttpGet("/dean/sgaap/")]
        public IActionResult StudentsGraduatesAchievingAllPLOS()
        {
            return View();
        }

        [HttpGet("/dean/apa/")]
        public IActionResult AveragePLOAchievement()
        {
            return View();
        }

        [HttpGet("/dean/ippc/")]
        public IActionResult InstructorwisePLOPerformanceComparison()
        {
            return View();
        }




    }
}
