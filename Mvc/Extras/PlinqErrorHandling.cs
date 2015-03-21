using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Mvc.Extras
{
    public class PlinqErrorHandling
    {
        // Paste into PLINQDataSample class. 
        static void PLINQExceptions_1()
        {
            // Using the raw string array here. See PLINQ Data Sample. 
            string[] customers = GetCustomersAsStrings().ToArray();


            // First, we must simulate some currupt input.
            customers[54] = "###";

            var parallelQuery = from cust in customers.AsParallel()
                                let fields = cust.Split(',')
                                where fields[3].StartsWith("C") //throw indexoutofrange 
                                select new { city = fields[3], thread = Thread.CurrentThread.ManagedThreadId };
            try
            {
                // We use ForAll although it doesn't really improve performance 
                // since all output is serialized through the Console.
                parallelQuery.ForAll(e => Console.WriteLine("City: {0}, Thread:{1}", e.city, e.thread));
            }

            // In this design, we stop query processing when the exception occurs. 
            catch (AggregateException e)
            {
                foreach (var ex in e.InnerExceptions)
                {
                    Console.WriteLine(ex.Message);
                    if (ex is IndexOutOfRangeException)
                        Console.WriteLine("The data source is corrupt. Query stopped.");
                }
            }
        }





        private static List<string> GetCustomersAsStrings()
        {
            throw new NotImplementedException();
        }
    }
}