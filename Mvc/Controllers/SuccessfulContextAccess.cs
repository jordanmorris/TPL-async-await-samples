using System.Threading.Tasks;
using System.Web.Mvc;

namespace Mvc.Controllers
{
    public class SuccessfulContextAccessController : Controller
    {
        // GET: /Index/
        public async Task<ActionResult> Index()
        {
            await DoSomethingAsync();
            // *** difference ***
            // We allow the default behaviour, which is to resume on the 
            // thread (captured context) we started with.

            var output = "Success. Http verb used: " +
                         System.Web.HttpContext.Current.Request.HttpMethod;

            return RedirectToAction("Index", "Home", new { Output = output });
        }

        private async Task DoSomethingAsync()
        {
            await Task.Delay(1000)
                //Note that it doesn't matter if async calls further down the stack
                //let go of the context. ConfigureAwait has no upstream effect.
                      .ConfigureAwait(continueOnCapturedContext: false);
        }


     }
}
