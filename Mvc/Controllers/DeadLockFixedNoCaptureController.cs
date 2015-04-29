using System.Threading.Tasks;
using System.Web.Mvc;

namespace Mvc.Controllers
{
    public class DeadLockFixedNoCaptureController : Controller
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
            await Task.Delay(1000)
                //this protects from deadlocks by electing
                //to continue on the a thread from the ThreadPool
                //after completing the asynchronous operation
                      .ConfigureAwait(continueOnCapturedContext: false);
        }


     }
}
