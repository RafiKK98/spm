using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SpmsApp.Models;
using SpmsApp.Services;

namespace SpmsApp.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet("/")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("/login")]
        public IActionResult Login(LoginCredentials cred)
        {
            DataServices service = DataServices.dataServices;

            if (cred.UserType == UserType.Admin)
            {
                throw new NotImplementedException("Please implement");
            }
            else if (cred.UserType == UserType.Student)
            {
                if (service.GetStudent(cred.Username, cred.Password) is Student s)
                {
                    StudentController.activestudent = s;
                    return Redirect("/student/"); 
                }

                return Redirect("/login/wrong");
            }
            else if (cred.UserType == UserType.Faculty)
            {
                if (service.GetFaculty(cred.Username, cred.Password) is Faculty f)
                {
                    FacultyController.activeFaculty = f;
                    return Redirect("/faculty/"); 
                }

                return Redirect("/login/wrong");
            }
            else if (cred.UserType == UserType.Dean)
            {
                if (service.GetDean(cred.Username, cred.Password) is SchoolDean d)
                {
                    DeanController.ActiveDean = d;
                    return Redirect("/dean/"); 
                }

                return Redirect("/login/wrong");
            }
            else if (cred.UserType == UserType.Head)
            {
                if (service.GetHead(cred.Username, cred.Password) is DepartmentHead h)
                {
                HeadOfDepartmentController.ActiveHead=h;
                return Redirect("/department/");
                }
                return Redirect("/login/wrong");
            }
            else if (cred.UserType == UserType.UGCIEB)
            {
                 if (service.GetUGCIEB(cred.Username, cred.Password) is UGCIEB u)
                {
                     UGCorIEBController.ActiveUgcieb=u;
                    return Redirect("/ugc/");
                }
                  return Redirect("/login/wrong");
             
            }

            else if (cred.UserType == UserType.Guardian)
            {
                 if (service.GetGuardian(cred.Username, cred.Password) is Guardian g)
                {
                    GuardianController.Activeguardian = g;
                    return Redirect("/guardian/"); 
                }

                return Redirect("/login/wrong");
            }
            

            else if (cred.UserType == UserType.VC)
            {
                //return Redirect("/vc/");
                if (service.GetVC(cred.Username, cred.Password) is VC v)
                {
                    VCController.ActiveVC = v;
                    return Redirect("/vc/"); 
                }

                return Redirect("/login/wrong");
            }
            else
            {
                return Redirect("/login/wrong");
            }
        }

        [HttpGet("/login/wrong")]
        public IActionResult InvalidLogin()
        {
            return View();
        }
    }
}