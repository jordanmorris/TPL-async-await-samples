using System;
using System.Threading.Tasks;

namespace Mvc.Extras.Gatekeeper
{
    public class GatekeeperExample : IGatekeeperExample
    {
        /// <summary>
        /// Divides one number by another.
        /// </summary>
        /// <exception cref="DivideByZeroException"></exception>
        /// <returns></returns>
        public async Task<int> DivideSlowlyAsync(int dividend, int divisor)
        {
            await Task.Delay(2000)
                .ConfigureAwait(continueOnCapturedContext: false);
            return dividend/divisor;
        }

        /// <summary>
        /// Synchronous equivalent of DivideSlowlyAsync. Divides one number by another. 
        /// </summary>
        /// <exception cref="DivideByZeroException"></exception>
        /// <returns></returns>
        [Obsolete("Wherever possible, use DivideSlowlyAsync and refactor for " +
                  "end-to-end asynchronicity. Otherwise, if you must have " +
                  "partially asnyc code, use this Gate-keeper Method convert.")]
        public int DivideSlowly(int dividend, int divisor)
        {
            try
            {
                //Start the async method on the TPL's ThreadPool
                return Task.Run(() => DivideSlowlyAsync(dividend, divisor)).Result;
            }
            catch (AggregateException ae)
            {
                //Flatten the InnerExceptions, so you don't need to loop through
                //them recursively (some of the inner exceptions may themselves be
                //AggregateExceptions)
                var flatAe = ae.Flatten();

                //re-throw any exception which may be handled specifically by the client
                foreach (var e in flatAe.InnerExceptions)
                {
                    if (e is DivideByZeroException) throw e;
                }

                foreach (var e in flatAe.InnerExceptions)
                {
                    //todo: log each of the InnerExceptions
                }

                //Throw some regular kind of exception which normal error-handling
                //infrastructure can handle
                throw new Exception("Stopped an AggregateException", ae);
            }
        }
    }
}