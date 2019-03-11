using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebApplication1.Application;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private DataStoreHelper _helper;

        public HomeController(DataStoreHelper helper)
        {
            _helper = helper;
        }
        public HomeController()
        {
           
        }

        public ActionResult Index()
        {
            _helper.CreateCookie(Request,Response);
           
           var items = _helper.FetchCharacters(Request);
      

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
            CharacterDetails details= new CharacterDetails();
           
            using (var http = new HttpClient())
            {
                http.BaseAddress = new Uri("https://swapi.co/api/");
                var data = http.GetStringAsync($"people/{id}").Result;
                details = JsonConvert.DeserializeObject<CharacterDetails>(data);
                ViewBag.Title = details.Name;
            }
      
            return View(details);
        }

        public ActionResult SetAsFavourite(CharacterDetails characterDetails)
        {
           _helper.SaveFavourite(Request,Response,characterDetails);
            List<Character> list = _helper.FetchCharacters(Request);
            return View("Index",list);
        }

       

    }
}