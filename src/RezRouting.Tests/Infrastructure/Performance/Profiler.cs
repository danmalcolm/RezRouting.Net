using System;
using System.Diagnostics;

namespace RezRouting.Tests.Infrastructure.Performance
{
    // Based on solution here: http://stackoverflow.com/a/1048708/146280

    /// <summary>
    /// Provides simple execution time profiling
    /// </summary>
    public class Profiler
    {
        public static ProfileResult Profile(string description, int iterations, Action func)
        {
            // warm up 
            func();

            var watch = new Stopwatch();

            // clean up
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            watch.Start();
            for (int i = 0; i < iterations; i++)
            {
                func();
            }
            watch.Stop();
            var result = new ProfileResult(watch.Elapsed.TotalMilliseconds, iterations);
            
            Console.Write(description);
            Console.WriteLine(" Time Elapsed {0} ms - Average time per iteration {1} ms", result.TotalMilliseconds, result.MeanIterationTime);

            return result;
        }
    }
}