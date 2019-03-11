using System.Web.Mvc;
using WebApplication1.Application;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataStoreHelper _helper;

        public HomeController(DataStoreHelper helper)
        {
            _helper = helper;
        }

        public ActionResult Index()
        {
            _helper.CreateCookie(Request, Response);

            var items = _helper.FetchCharacters(Request);
            return View(items);
        }

        public ActionResult Details(int id)
        {
            var details = _helper.FetCharacterDetails(Request, id);

            return View(details);
        }

        public ActionResult SetAsFavourite(CharacterDetails characterDetails)
        {
            _helper.SaveFavourite(Request, Response, characterDetails);

            return RedirectToAction("Index");
        }
    }
}