using BenchmarkDotNet.Running;

namespace OrderSortingBenchmark
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<OrderToolBenchmark>();
        }
    }
}
