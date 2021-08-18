using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpmsApp.Controllers
{
    public class VCController : Controller
    {
        [HttpGet("/vc/")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/vc/spcc/")]
        public IActionResult StudentPLOComparisonByCourse()
        {
            return View();
        }

        [HttpGet("/vc/spcp/")]
        public IActionResult StudentPLOComparisonByProgram()
        {
            return View();
        }
        [HttpGet("/vc/spat/")]
        public IActionResult StudentPLOAchievementTable()
        {
            return View();
        }
        [HttpGet("/vc/cppf/")]
        public IActionResult CoursePLOPerformanceByFaculty()
        {
            return View();
        }

        [HttpGet("/vc/pcp/")]
        public IActionResult PLOwiseCoursePerformance()
        {
            return View();
        }

        [HttpGet("/vc/afpp/")]
        public IActionResult AchievedVsFailedPLOPerformance()
        {
            return View();
        }

        [HttpGet("/vc/aac/")]
        public IActionResult AttemptedVsAchievedComparison()
        {
            return View();
        }

        [HttpGet("/vc/pafap/")]
        public IActionResult PLOAchievementForAProgram()
        {
            return View();
        }

        [HttpGet("/vc/sgaap/")]
        public IActionResult StudentsGraduatesAchievingAllPLOS()
        {
            return View();
        }

        [HttpGet("/vc/apa/")]
        public IActionResult AveragePLOAchievement()
        {
            return View();
        }

        [HttpGet("/vc/ippc/")]
        public IActionResult InstructorwisePLOPerformanceComparison()
        {
            return View();
        }
    }
}
