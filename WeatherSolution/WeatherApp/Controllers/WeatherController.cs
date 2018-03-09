using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WeatherAPI;
using WeatherApp.Models;
using static WeatherApp.Models.WeatherData;

namespace WeatherApp.Controllers
{
    [Authorize]
    public class WeatherController : Controller
    {
        [HttpGet, Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet, Authorize]
        public ActionResult AsyncSearchCityName(string cityName) {
            
            ApplicationConfiguration configuration = new ApplicationConfiguration();
            configuration.Load();
            try
            {
                IRestResponse response = OpenWeatherAPI.Get(configuration.BaseAddressAPI, cityName, configuration.Query, configuration.APIKey);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    ContentResult content = new ContentResult();
                    content = Content(response.Content, "application/json");
                    return content;
                }
                else
                {                    
                    return Json(null, JsonRequestBehavior.AllowGet);
                }
            }

            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return Content(null, "application/json; charset=utf-8");
            }
          

        }      
    }

}
