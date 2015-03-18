using System.Web.Mvc;

namespace Mvc.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index(string output)
        {
            ViewBag.Output = output;
            return View();
        }


     }
}
