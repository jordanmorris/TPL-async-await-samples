using System.Threading.Tasks;
using System.Web.Mvc;

namespace Mvc.Controllers
{
    public class DeadLockController : Controller
    {
        // GET: /Index/
        public ActionResult Index()
        {
            DoSomethingAsync().Wait();

            var output = "Success";

            return RedirectToAction("Index", "Home", new { Output = output });
        }

        private async Task DoSomethingAsync()
        {
            await Task.Delay(1000);
        }


     }
}
