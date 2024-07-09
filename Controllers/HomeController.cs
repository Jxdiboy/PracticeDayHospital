using DayHospital.Data.Static;
using DayHospital.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DayHospital.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //HttpContext.Session.SetString("id", "0109105229081");
            //HttpContext.Session.SetString("role", Roles.Nurse);
            //HttpContext.Session.SetString("name", "Jade");
            //HttpContext.Session.SetString("fname", "Jade Appels");
            //return View();

            HttpContext.Session.SetString("id", "12345678912358");
            HttpContext.Session.SetString("role", Roles.Surgeon);
            HttpContext.Session.SetString("name", "Andile");
            HttpContext.Session.SetString("fname", "Andile Banda");
            return View();

            HttpContext.Session.SetString("id", "12345678912359");
            HttpContext.Session.SetString("role", Roles.Admin);
            HttpContext.Session.SetString("name", "Admin");
            HttpContext.Session.SetString("fname", "Admin");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

      
    }
}
