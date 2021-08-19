using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpmsApp.Controllers
{
    public class GuardianController : Controller
    {
        [HttpGet("/guardian/")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("/guardian/spat")]
        public IActionResult StudentPLOAchievementTable()
        {
            return View();
        }

    }
}
