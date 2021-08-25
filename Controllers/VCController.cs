using Microsoft.AspNetCore.Mvc;
using SpmsApp.ViewModels;
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
            return View(new TopbarViewModel() {Name = "No Name Set", ID = 0000});
        }

        [HttpGet("/vc/spcc/")]
        public IActionResult StudentPLOComparisonByCourse()
        {
            return View(new TopbarViewModel() {Name = "No Name Set", ID = 0000});
        }

        [HttpGet("/vc/spcp/")]
        public IActionResult StudentPLOComparisonByProgram()
        {
            return View(new TopbarViewModel() {Name = "No Name Set", ID = 0000});
        }
        [HttpGet("/vc/spat/")]
        public IActionResult StudentPLOAchievementTable()
        {
            return View(new TopbarViewModel() {Name = "No Name Set", ID = 0000});
        }
        [HttpGet("/vc/cppf/")]
        public IActionResult CoursePLOPerformanceByFaculty()
        {
            return View(new TopbarViewModel() {Name = "No Name Set", ID = 0000});
        }

        [HttpGet("/vc/pcp/")]
        public IActionResult PLOwiseCoursePerformance()
        {
            return View(new TopbarViewModel() {Name = "No Name Set", ID = 0000});
        }

        [HttpGet("/vc/afpp/")]
        public IActionResult AchievedVsFailedPLOPerformance()
        {
            return View(new TopbarViewModel() {Name = "No Name Set", ID = 0000});
        }

        [HttpGet("/vc/aac/")]
        public IActionResult AttemptedVsAchievedComparison()
        {
            return View(new TopbarViewModel() {Name = "No Name Set", ID = 0000});
        }

        [HttpGet("/vc/pafap/")]
        public IActionResult PLOAchievementForAProgram()
        {
            return View(new TopbarViewModel() {Name = "No Name Set", ID = 0000});
        }

        [HttpGet("/vc/sgaap/")]
        public IActionResult StudentsGraduatesAchievingAllPLOS()
        {
            return View(new TopbarViewModel() {Name = "No Name Set", ID = 0000});
        }

        [HttpGet("/vc/apa/")]
        public IActionResult AveragePLOAchievement()
        {
            return View(new TopbarViewModel() {Name = "No Name Set", ID = 0000});
        }

        [HttpGet("/vc/ippc/")]
        public IActionResult InstructorwisePLOPerformanceComparison()
        {
            return View(new TopbarViewModel() {Name = "No Name Set", ID = 0000});
        }
    }
}
