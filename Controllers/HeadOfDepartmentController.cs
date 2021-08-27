using Microsoft.AspNetCore.Mvc;
using SpmsApp.ViewModels;
using SpmsApp.ViewModels.DepartmentHead;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpmsApp.Models;
using SpmsApp.Services;
using System.Collections;


namespace SpmsApp.Controllers
{
    public class HeadOfDepartmentController : Controller
    {

        public static DataServices ds = DataServices.dataServices;
        
        static StudentwisePloComparisonCourseViewModel viewStudentModel = new StudentwisePloComparisonCourseViewModel();
        public static DepartmentHead ActiveHead = ds.departmentHeads.First();
       
        

        [HttpGet("/department/")]
        public IActionResult Index()
        {
            return View(new TopbarViewModel() {Name = ActiveHead.FullName, ID=ActiveHead.DepartmentHeadID});
        }
        
        [HttpGet("/department/SPCC")]
        public IActionResult StudentPLOComparisonCourseWise()
        {

            int DeptId = 0;

            StudentPLOComparisonCourseWiseViewModel studentPLOComparisonCourseWiseViewModel = new StudentPLOComparisonCourseWiseViewModel();
            List<int> progID = new List<int>();
            List<Course> cou = new List<Course>();

                foreach(DepartmentHead d in ds.departmentHeads){
                    if(d.DepartmentHeadID == ActiveHead.DepartmentHeadID)
                    {
                        DeptId= d.Department.DepartmentID;
                    }
                }

                foreach(Program p in ds.programs){
                        if(DeptId == p.Department.DepartmentID)
                        {
                            progID.Add(p.ProgramID);
                        }
                }

                foreach(Course c in ds.courses){
                    for(int i=0; i< progID.Count;i++)
                    {
                        if(progID[i] == c.Program.ProgramID)
                        {
                            cou.Add(c);
                        }
                    }
                }
            
            studentPLOComparisonCourseWiseViewModel.Courses = cou;
            studentPLOComparisonCourseWiseViewModel.TopbarViewModel = new TopbarViewModel()
            {
                Name = ActiveHead.FullName,
                ID = ActiveHead.DepartmentHeadID
            };

            return View(studentPLOComparisonCourseWiseViewModel);

        }
        [HttpGet("/department/SPCP")]
        public IActionResult StudentPLOComparisonProgramWise()
        {
            return View(new TopbarViewModel() {Name = "No Name Set", ID = 0000});
        }
        


        [HttpGet("/department/SPAT")]
        public IActionResult PloAchievementTable()
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
