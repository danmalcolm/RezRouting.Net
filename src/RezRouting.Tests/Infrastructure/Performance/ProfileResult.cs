namespace RezRouting.Tests.Infrastructure.Performance
{
    public class ProfileResult
    {
        public ProfileResult(double totalMilliseconds, int iterations)
        {
            TotalMilliseconds = totalMilliseconds;
            Iterations = iterations;
        }

        public double TotalMilliseconds { get; private set; }

        public int Iterations { get; private set; }

        public double MeanIterationTime { get { return TotalMilliseconds / Iterations; } }
    }
}