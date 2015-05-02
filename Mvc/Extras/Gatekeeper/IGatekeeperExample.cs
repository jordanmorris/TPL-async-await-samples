using System;
using System.Threading.Tasks;

namespace Mvc.Extras.Gatekeeper
{
    public interface IGatekeeperExample
    {
        /// <summary>
        /// Divides one number by another.
        /// </summary>
        /// <exception cref="DivideByZeroException"></exception>
        /// <returns></returns>
        Task<int> DivideSlowlyAsync(int dividend, int divisor);

        /// <summary>
        /// Synchronous equivalent of DivideSlowlyAsync. Divides one number by another. 
        /// </summary>
        /// <exception cref="DivideByZeroException"></exception>
        /// <returns></returns>
        [Obsolete("Wherever possible, use DivideSlowlyAsync and refactor for " +
                  "end-to-end asynchronicity. Otherwise, if you must have " +
                  "partially asnyc code, use this Gate-keeper Method convert.")]
        int DivideSlowly(int dividend, int divisor);
    }
}
