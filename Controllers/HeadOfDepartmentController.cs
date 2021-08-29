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
        [HttpGet("/department/ISPSCC")]
        public IActionResult IndividualStudentPLOScoreComparisonCourseWise()
        {
            var viewModel = new IndividualStudentPLOScoreComparisonCourseWiseViewModel()
            {
                TopbarViewModel = new TopbarViewModel()
                {  
                Name = ActiveHead.FullName,
                ID = ActiveHead.DepartmentHeadID
                },
                Courses = ds.courses.Where(c => c.Program.Department == ActiveHead.Department&& c.CoofferedCourse == null).ToList()

            };
            return View(viewModel);
        }


        [HttpGet("/department/ISPSCC/{studentID}/{courseID}")]
        public IActionResult IndividualStudentPLOScoreComparisonCourseWise(int studentID, int courseID)
        {
            var student = ds.students.Find(s => s.StudentID == studentID && s.Program.Department == ActiveHead.Department);
            var course = ds.courses.Find(c => c.CourseID == courseID);
            if (student == null) return NotFound();


            var courseRegistrations = ds.courseRegistrations.Where(cr => cr.Student == student && cr.Section.Course == course).ToList();

            if (courseRegistrations.Count <= 0) return NotFound();

            var sections = courseRegistrations.Select((cr, idx) => cr.Section).ToList();

            var section = sections.First();

            for (int i = 1; i < sections.Count; i++)
            {
                if (section.Semester.CompareTo(sections[i].Semester) > 0)
                {
                    section = sections[i];
                }
            }

            var semester = section.Semester;

            var studentEvaluation = ds.evaluations.Where(e => e.Assessment.Section == section && e.Student == student);

            var studentEvalGroup = studentEvaluation.GroupBy(se => se.Assessment.CourseOutcome.PLO.PloName);

            List<string> studentPloList = new List<string>();
            List<float> studentScoreList = new List<float>();

            foreach (var evalGroup in studentEvalGroup)
            {
                studentPloList.Add(evalGroup.Key);

                float score = 0;

                foreach (var eval in evalGroup)
                {
                    score += eval.TotalObtainedMark;
                }

                studentScoreList.Add(score);
            }

            var courseEval = ds.evaluations.Where(ev => ev.Assessment.Section.Semester.Equals(semester) && ev.Assessment.Section.Course == course && ev.Assessment.CourseOutcome.PLO.Program == student.Program);

            var courseEvalGroupbyPlo = courseEval.GroupBy(ce => ce.Assessment.CourseOutcome.PLO.PloName);

            var coursePloList = new List<string>();
            var courseAvgScoreList = new List<float>();

            foreach (var evalGroup in courseEvalGroupbyPlo)
            {
                coursePloList.Add(evalGroup.Key);

                var evalGroupbySt = evalGroup.GroupBy(eg => eg.Student);
                var stCount = evalGroupbySt.Count();

                float score = 0;

                foreach (var eval in evalGroup)
                {
                    score += eval.TotalObtainedMark;
                }

                courseAvgScoreList.Add(score / stCount);
            }

            return Json(new { StData = studentScoreList, StLabel = studentPloList, CourseData = courseAvgScoreList });



        }




        [HttpGet("/department/ISPSCP")]
        public IActionResult IndividualStudentPLOScoreComparisonProgramWise()
        {
            var viewModel = new IndividualStudentPLOScoreComparisonProgramWiseViewModel()
            {
                TopbarViewModel = new TopbarViewModel()
                {
                    Name = ActiveHead.FullName,
                    ID = ActiveHead.DepartmentHeadID
                },
                Programs = ds.programs.Where(p => p.Department == ActiveHead.Department).ToList()
            };

            return View(viewModel);
        }
        [HttpGet("/department/ISPSCP/{studentID}/{programID}")]
         public IActionResult IndividualStudentPLOScoreComparisonProgramWise(int studentID, int programID)
        {
            var student = ds.students.Find(s => s.StudentID == studentID);

            if (student == null) return NotFound();

            var program = ds.programs.Find(p => p.ProgramID == programID);
            var programPlos = ds.plos.Where(plo => plo.Program == program);

            var evaluations = ds.evaluations.Where(ev => ev.Assessment.CourseOutcome.PLO.Program == program);

            List<float> programScores = new List<float>();
            List<float> studentScores = new List<float>();

            foreach (var plo in programPlos)
            {
                var ploScoreProgram = evaluations.Where(ev => ev.Assessment.CourseOutcome.PLO == plo);
                var stCount = ploScoreProgram.GroupBy(psp => psp.Student).Count();
                // var ploScoreStudent = ploScoreProgram.Where(ev => ev.Student == student);

                float programScore = 0;
                float studentScore = 0;

                foreach (var p in ploScoreProgram)
                {
                    programScore += p.TotalObtainedMark;

                    if (p.Student == student)
                    {
                        studentScore += p.TotalObtainedMark;
                    }
                }

                programScores.Add(programScore / stCount);
                studentScores.Add(studentScore);
            }

            var data = new { PloList = programPlos.Select(p => p.PloName), StudentScores = studentScores, ProgramScores = programScores };

            return Json(data);



        }






    }
}
