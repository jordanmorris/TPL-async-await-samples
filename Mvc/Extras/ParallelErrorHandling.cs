using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Mvc.Extras
{
    /// <summary>
    /// From MSDN's How to: Handle Exceptions in Parallel Loops
    /// https://msdn.microsoft.com/en-us/library/dd460695%28v=vs.110%29.aspx
    /// </summary>
    public class ParallelErrorHandling
    {
        static void Main(string[] args)
        {
            // Create some random data to process in parallel. 
            // There is a good probability this data will cause some exceptions to be thrown. 
            byte[] data = new byte[5000];
            Random r = new Random();
            r.NextBytes(data);

            try
            {
                ProcessDataInParallel(data);
            }

            catch (AggregateException ae)
            {
                // This is where you can choose which exceptions to handle. 
                foreach (var ex in ae.InnerExceptions)
                {
                    if (ex is ArgumentException)
                        Console.WriteLine(ex.Message);
                    else
                        throw ex;
                }
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        private static void ProcessDataInParallel(byte[] data)
        {
            // Use ConcurrentQueue to enable safe enqueueing from multiple threads. 
            var exceptions = new ConcurrentQueue<Exception>();

            // Execute the complete loop and capture all exceptions.
            Parallel.ForEach(data, d =>
            {
                try
                {
                    // Cause a few exceptions, but not too many. 
                    if (d < 0x3)
                        throw new ArgumentException(String.Format("value is {0:x}. Elements must be greater than 0x3.", d));
                    else
                        Console.Write(d + " ");
                }
                // Store the exception and continue with the loop.                     
                catch (Exception e) { exceptions.Enqueue(e); }
            });

            // Throw the exceptions here after the loop completes. 
            if (exceptions.Count > 0) throw new AggregateException(exceptions);
        }
    }
}