using System.Diagnostics;
using AdbMe.CLI;
using AdbMe.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdbMe.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Your Device page";
            ViewData["Message"] = "Devices are loading";

            var model = new DevicesViewModel(new Scrcpy());
            return View("Index", model);
        }

        [HttpPost]
        public IActionResult Index(string? serial)
        {
            ViewData["Title"] = "Your Device page";
            ViewData["Message"] = "A device is in launch attempt";
            
            var model = new DevicesViewModel(new Scrcpy(), serial);
            return View("Index", model);
        }


        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Test()
        {
            ViewData["Message"] = "Your Test page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
