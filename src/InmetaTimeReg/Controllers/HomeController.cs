using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;

namespace InmetaTimeReg.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //Example login request
            //"https://mtimereg.inmeta.com/timeregserver/login?username=inmeta&password=infeta"
            var loginURL = "https://mtimereg.inmeta.com/timeregserver/login";

            string urlParameters = "?username=inmeta&password=infeta";

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(loginURL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
               //Parse the response body. Blocking!

            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            return View();
        }

        public IActionResult TimeList()
        {
            var startDate = DateTime.Today;
            startDate = startDate.AddDays(-(((startDate.DayOfWeek - DayOfWeek.Monday) + 7) % 7));
            var endDate = startDate.AddDays(7);
  
            var numDays = (int)((endDate - startDate).TotalDays);
            List<DateTime> daysOfWeek = Enumerable
                       .Range(0, numDays)
                       .Select(x => startDate.AddDays(x))
                       .ToList();

            ViewData["Message"] = "Blank";
            ViewBag.DaysOfWeek = daysOfWeek;

            ViewBag.CurrentWeekNumber = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(startDate, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            return View();
        }

        public IActionResult TimeListCopy()
        {
            ViewData["Message"] = "Copy from last week";


            List<TimeRegItem> CalendarItems = new List<TimeRegItem>();
            CalendarItems.Add(new TimeRegItem()
            {
                ID = 1,
                CustomerName = "Inmarsat",
                CustomerProject = "Intranet",
                CustomerActivity = "Development and delivery",
                Description = "Doing stuff and cool things",
                Duration = 6.5
            });
            CalendarItems.Add(new TimeRegItem()
            {
                ID = 2,
                CustomerName = "KLP",
                CustomerProject = "OFFICE addin",
                CustomerActivity = "Development",
                Description = "another brick on the wall",
                Duration = 3.5
            });


            ViewBag.CalendarItems = CalendarItems;

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

    public class TimeRegItem
    {
        public int ID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerProject { get; set; }
        public string CustomerActivity { get; set; }
        public string Description { get; set; }
        public double Duration { get; set; }


    }
}
