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
                throw new NotImplementedException("Please implement");
            }
            else if (cred.UserType == UserType.Faculty)
            {
                return Redirect("/faculty/");
            }
            else if (cred.UserType == UserType.Dean)
            {
                return Redirect("/dean/");
            }
            else
            {
                throw new NotImplementedException("Please implement");
            }
        }
    }
}