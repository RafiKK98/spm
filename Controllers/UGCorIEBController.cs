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
    public class UGCorIEBController : Controller
    {
        public static DataServices ds= DataServices.dataServices;
         public static UGCIEB ActiveUgcieb= ds.uGCIEBs.First();

       

        [HttpGet("/ugc/")]
        public IActionResult Index()
        {
            return View(new TopbarViewModel() {Name = ActiveUgcieb.FullName, ID =ActiveUgcieb.UgciebID});
        }

       [HttpGet("/ugc/upasp/")]
        public IActionResult UniversityPloAchievementSelectProgram()
        {
            var viewModel = new UniversityPloAchievementSelectProgram()
            {
                TopbarViewModel = new TopbarViewModel()
                {
                    Name = ActiveUgcieb.FullName,
                    ID = ActiveUgcieb.UgciebID
                },
                Programs = ds.programs
            };

            return View(viewModel);
              
        }
            [HttpPost("/ugc/upasp/")]
        public IActionResult UniversityPloAchievementSelectProgram([FromBody] UniversityPloAchievementSelectProgram viewModel)
        {
            viewModel.TopbarViewModel = new TopbarViewModel()
            {
                Name = ActiveUgcieb.FullName,
                ID = ActiveUgcieb.UgciebID
            };
            viewModel.Programs = ds.programs;

            var program = ds.programs.Find(p => p.ProgramID == viewModel.SelectedProgram);

            var programEval = ds.evaluations.Where(ev => ev.Assessment.CourseOutcome.PLO.Program == program);
            var evalPloGroups = programEval.GroupBy(ev => ev.Assessment.CourseOutcome.PLO.PloName);

            List<string> labels = new List<string>();
            List<int> passCount = new List<int>();

            foreach (var evalPloGroup in evalPloGroups)
            {
                labels.Add(evalPloGroup.Key);

                int passed = 0;

                foreach (var eval in evalPloGroup)
                {
                    float percent = (float)eval.TotalObtainedMark / eval.Assessment.TotalMark * 100;

                    if (percent >= eval.Assessment.Section.PassMark)
                    {
                        passed++;
                    }
                }

                passCount.Add(passed);
            }

            Dataset dset = new Dataset()
            {
                Data = passCount,
                Label = "No. of Students",
                BackgroundColor = "rgba(255, 99, 132, 0.2)",
                BorderColor = "rgb(255, 99, 132)",
                PointBackgroundColor = "rgb(255, 99, 132)",
                PointBorderColor = "#fff",
                PointHoverBackgroundColor = "#fff",
                PointHoverBorderColor = "rgb(255, 99, 132)"
            };

            ViewModels.Data data = new ViewModels.Data()
            {
                Labels = labels,
                Datasets = new List<Dataset>() { dset }
            };

            viewModel.Data = data;

            return Json(viewModel);
        }

        [HttpGet("/ugc/pwcp")]
        public IActionResult PLOwiseCoursePerformance()
        {
            return View(new TopbarViewModel() {Name = "No Name Set", ID = 0000});
        }
          [HttpGet("/ugc/sogaap")]
        public IActionResult StudentsOrGraduatesAchievingAllPLOs()
        {
            return View(new TopbarViewModel() {Name = "No Name Set", ID = 0000});
        }
       

        [HttpGet("/ugc/logout")]
        public IActionResult Logout()
        {
            return Redirect("/");
        }

    }
}