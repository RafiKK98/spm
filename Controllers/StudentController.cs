using Microsoft.AspNetCore.Mvc;
using SpmsApp.Models;
using SpmsApp.Services;
using SpmsApp.ViewModels;
using SpmsApp.ViewModels.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpmsApp.Controllers
{
    
        public class StudentController : Controller
    {
        public static DataServices ds=DataServices.dataServices;
        public static Student activestudent=ds.students.First();

        [HttpGet("/student/ispscc")]
        public IActionResult IndividualStudentPloScoreComparisonCourse() // 1
        {
            // return Content("HELLO WORLD");

            var viewModel = new IndividualStudentPLOScoreComparisonCourseViewModel()
            {
                TopbarViewModel = new TopbarViewModel()
                {
                    Name = activestudent.FullName,
                    ID = activestudent.StudentID
                },
                Courses = ds.courses.Where(c => c.Program == activestudent.Program ).ToList()
            };

            return View(viewModel);
        }

        [HttpGet("/student/ispscc/{courseID}")]
        public IActionResult IndividualStudentPloScoreComparisonCourse( int courseID) // 1 continued
        {
            
            var course = ds.courses.Find(c => c.CourseID == courseID);

            

            var courseRegistrations = ds.courseRegistrations.Where(cr => cr.Student == activestudent && cr.Section.Course == course.CoofferedCourse).ToList();

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

            var studentEvaluation = ds.evaluations.Where(e => e.Assessment.Section == section && e.Student == activestudent);

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

            var courseEval = ds.evaluations.Where(ev => ev.Assessment.Section.Semester.Equals(semester) && ev.Assessment.Section.Course == course && ev.Assessment.CourseOutcome.PLO.Program == activestudent.Program);

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


[HttpGet("/student/ispscp")]
        public IActionResult IndividualStudentPloScoreComparisonProgram() // 2
        {
            var viewModel = new IndividualStudentPLOScoreComparisonProgramViewModel()
            {
                TopbarViewModel = new TopbarViewModel()
                {
                    Name = activestudent.FullName,
                    ID = activestudent.ID
                },
                Programs = ds.programs.Where(p => p == activestudent.Program).ToList()
            };

            return View(viewModel);
        }

        [HttpGet("/student/ispscp/{studentID}/{programID}")]
        public IActionResult IndividualStudentPloScoreComparisonProgram(int studentID, int programID) // 2 continued
        {
            var student = ds.students.Find(s => s.StudentID == studentID);

            //if (student == null) return NotFound();

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




        [HttpGet("/student/")]
        public IActionResult Index()
        {
            return View(new TopbarViewModel(){Name = activestudent.FullName, ID = activestudent.ID});

        }

        [HttpGet("/student/cpc/")]
        public IActionResult CoursePLOComparison()
        {
            // List<Course> cou=new List<Course>();
            // foreach(Student s in ds.students)
            // {
            //     foreach
            // }
        return Content("ssss");

        }




    }
}
