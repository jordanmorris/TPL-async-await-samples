﻿using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Mvc.Controllers
{
    public class AynscParallelController : Controller
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
            await Task.Delay(1)
                // *** difference ***
                      .ConfigureAwait(continueOnCapturedContext: false);
            //Lets go of context here
            
            Thread.Sleep(1000);
        }


     }
}
