using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<Character> items;
            using (var http= new HttpClient())
            { http.BaseAddress=new Uri("https://swapi.co/api/");
              var data=  http.GetStringAsync("people").Result;
              items = JObject.Parse(data)["results"].ToList().Select( t =>new Character()
              {
                  Url = t["url"].ToString(),
                  Gender = t["gender"].ToString(),
                  Name = t["name"].ToString(),
                  Height = Convert.ToInt32(t["height"].ToString()),
                  
              }).ToList();
              
            }
        

         

            return View(items);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Details(string id)
        {
            return View();
        }
    }
}