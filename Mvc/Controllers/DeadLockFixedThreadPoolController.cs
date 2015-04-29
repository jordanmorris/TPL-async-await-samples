using System.Threading.Tasks;
using System.Web.Mvc;

namespace Mvc.Controllers
{
    public class DeadLockFixedThreadPoolController : Controller
    {
        // GET: /Index/
        public ActionResult Index()
        {
            //this protects from deadlocks by starting the async method
            //on any free thread from the ThreadPool (rather than this thread)
            Task.Run(() => DoSomethingAsync()).Wait();

            var output = "Success";

            return RedirectToAction("Index", "Home", new { Output = output });
        }

        private async Task DoSomethingAsync()
        {
            await Task.Delay(1000);
        }


     }
}
