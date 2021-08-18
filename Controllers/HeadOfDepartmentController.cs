using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpmsApp.Controllers
{
    public class HeadOfDepartmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
