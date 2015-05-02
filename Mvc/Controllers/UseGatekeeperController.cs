using System;
using System.Web.Mvc;
using Mvc.Extras.Gatekeeper;

namespace Mvc.Controllers
{
    public class UseGatekeeperController : Controller
    {
        private readonly IGatekeeperExample _gatekeeperExample;

        public UseGatekeeperController(IGatekeeperExample gatekeeperExample)
        {
            _gatekeeperExample = gatekeeperExample;
        }

        public UseGatekeeperController()
            :this(new GatekeeperExample())
        {
        }

        // GET: /Index/
        public ActionResult Index(int dividend, int divisor)
        {
            var result = GetResult(dividend, divisor);

            return RedirectToAction("Index", "Home", new { Output = result });
        }


        private string GetResult(int dividend, int divisor)
        {
            try
            {
                #pragma warning disable 618
                //justification: I have a really, really good reason not to implement
                //async end-to-end right now
                var quotient = _gatekeeperExample.DivideSlowly(dividend, divisor);
                #pragma warning restore 618
                return "Success. Quotient = " + quotient;
            }
            catch (DivideByZeroException)
            {
                return "Failure. Division by zero.";
            }
        }


     }
}
