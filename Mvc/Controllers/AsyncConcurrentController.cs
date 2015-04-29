using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Mvc.Controllers
{
    public class AsyncConcurrentController : Controller
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
            await Task.Delay(1)
                // *** difference ***
                      .ConfigureAwait(continueOnCapturedContext: false);
            //This method did not 'capture' the thread on which it was started,
            //so it will happily resume on any available thread from TPL's ThreadPool
            
            Thread.Sleep(1000);
        }


     }
}
