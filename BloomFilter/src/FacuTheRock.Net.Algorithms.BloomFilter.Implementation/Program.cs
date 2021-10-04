using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Linq;
using System.Threading.Tasks;

namespace FacuTheRock.Net.Algorithms.BloomFilter.Implementation
{
    public class Program
    {
        public static void Main(string[] args) =>
            _ = BenchmarkRunner.Run<Benchy>();

        [MemoryDiagnoser]
        public class Benchy
        {
            private readonly int[] ids = Enumerable.Range(1, 1_000_000).ToArray();

            [Benchmark]
            public void CheckServiceBenchV6()
            {
                var capacity = ids.Length;
                var errorRate = 0.01f;
                var service = new HighlyOptimizedThreadSafeDuplicateCheckService(capacity, errorRate);
                Parallel.ForEach(
                    ids,
                    currentId => service.IsThisTheFirstTimeWeHaveSeen(currentId));
            }
        }
    }
}
