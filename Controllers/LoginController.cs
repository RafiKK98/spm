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
                return Redirect("/student/");
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
                return Redirect("/dean/");
            }
            else if (cred.UserType == UserType.Head)
            {
                return Redirect("/department/");
            }
            else if (cred.UserType == UserType.UGCIEB)
            {
                return Redirect("/ugc/");
            }

            else if (cred.UserType == UserType.Gaurdian)
            {
                return Redirect("/guardian/");
            }

            else if (cred.UserType == UserType.VC)
            {
                return Redirect("/vc/");

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