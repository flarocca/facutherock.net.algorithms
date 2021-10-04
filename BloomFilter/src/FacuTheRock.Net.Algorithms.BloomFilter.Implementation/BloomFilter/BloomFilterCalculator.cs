using System;

namespace FacuTheRock.Net.Algorithms.BloomFilter.Implementation
{
    public class BloomFilterCalculator : IBloomFilterCalculator
    {
        private static readonly double Log2 = Math.Log(2);

        public int CalculateK(int capacity, float m) =>
            (int)Math.Round(Log2 * m / capacity);

        public int CalculateM(int capacity, float errorRate) =>
            (int)Math.Ceiling(capacity * Math.Log(errorRate, 1 / Math.Pow(2, Log2)));
    }
}
