using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace InmetaTimeReg.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TimeList()
        {
            ViewData["Message"] = "Blank";

            return View();
        }

        public IActionResult TimeListCopy()
        {
            ViewData["Message"] = "Copy from last week";

            return View();
        }

        public IActionResult DeliveryCompleted()
        {
            ViewData["Message"] = "Timelisten er levert!";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
