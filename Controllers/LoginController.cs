using System;
using Microsoft.AspNetCore.Mvc;
using SpmsApp.Models;

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
                return Redirect("/faculty/");
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
            else if (cred.UserType == UserType.VC)
            {
                return Redirect("/vc/");
            }
            else
            {
                throw new NotImplementedException("Please implement");
            }
        }
    }
}