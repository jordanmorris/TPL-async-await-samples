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

            var output = "Success. Http verb used: " +
                         System.Web.HttpContext.Current.Request.HttpMethod;

            return RedirectToAction("Index", "Home", new { Output = output });
        }

        private async Task DoSomethingAsync()
        {
            await Task.Delay(1000)
                      .ConfigureAwait(continueOnCapturedContext: false);
        }


     }
}
