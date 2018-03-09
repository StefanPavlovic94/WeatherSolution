using System;
using System.Web;
using System.Web.Mvc;

namespace WeatherApp.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet, AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
    }
}