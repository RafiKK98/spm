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
<<<<<<< HEAD
            else if (cred.UserType == UserType.Gaurdian)
            {
                return Redirect("/guardian/");
=======
            else if (cred.UserType == UserType.VC)
            {
                return Redirect("/vc/");
>>>>>>> 4e1c7a060e424ef2d6454440341603c1e0384176
            }
            else
            {
                throw new NotImplementedException("Please implement");
            }
        }
    }
}