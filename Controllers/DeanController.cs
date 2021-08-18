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
            return View();
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
