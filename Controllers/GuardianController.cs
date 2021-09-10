using Microsoft.AspNetCore.Mvc;
using SpmsApp.ViewModels;
using SpmsApp.Models;
using SpmsApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SpmsApp.Controllers
{
    public class GuardianController : Controller
    {
        public static DataServices ds = DataServices.dataServices;
        public static Guardian Activeguardian = ds.guardians.First();
        [HttpGet("/guardian/")]
        public IActionResult Index()
        {
            return View(new TopbarViewModel() {Name = Activeguardian.FullName, ID = Activeguardian.guardianID});
        }
       [HttpGet("/guardian/spat")]
        public IActionResult StudentPLOAchievementTable()
        {
             PloAchievementTableViewModel ploAchievementTableViewModel = new PloAchievementTableViewModel();
            ploAchievementTableViewModel.TopbarViewModel = new TopbarViewModel()
            {
                Name = "No Name Set", ID = 0000
            };

            return View(ploAchievementTableViewModel);
        }


    }
}
