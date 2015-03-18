using System.Threading.Tasks;
using System.Web.Mvc;

namespace Mvc.Controllers
{
    public class DeadLockFixedController : Controller
    {
        // GET: /Index/
        public ActionResult Index()
        {
            Task.Run(() => DoSomethingAsync()).Wait();

            var output = "Success";

            return RedirectToAction("Index", "Home", new { Output = output });
        }

        private async Task DoSomethingAsync()
        {
            await Task.Delay(1000)
                      .ConfigureAwait(continueOnCapturedContext: false);
        }


     }
}
