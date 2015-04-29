using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Mvc.Controllers
{
    public class AsyncNotConcurrentController : Controller
    {
        // GET: /Index/
        public async Task<ActionResult> Index()
        {
            var startTime = DateTime.Now;

            var doSomething = DoSomethingAsync();
            Thread.Sleep(1000);
            await doSomething;
            
            var timeTaken = DateTime.Now.Subtract(startTime);
            var output = timeTaken.TotalSeconds + " seconds";

            return RedirectToAction("Index", "Home", new { Output = output });
        }

        private async Task DoSomethingAsync()
        {
            //When the following command runs, the calling method (Index) will
            //be able to continue execution.
            await Task.Delay(1);
            //With the the async Delay completed, this method will now wait for the 
            //'captured' thread (the thread it started on) to resume execution. 
            //Unfortunately, that thread is doing work (*cough* sleeping *cough*).
            Thread.Sleep(1000);
        }


     }
}
