using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpmsApp.Controllers
{
    public class DeanController : Controller
    {
        [HttpGet("/dean/")]
        public IActionResult Index()
        {
            return View();
        }

        
}
}
