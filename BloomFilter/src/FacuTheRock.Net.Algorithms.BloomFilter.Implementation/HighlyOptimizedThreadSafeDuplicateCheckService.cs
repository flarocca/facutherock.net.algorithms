using FacuTheRock.Net.Algorithms.BloomFilter.Implementation.BloomFilter;
using System.Collections.Generic;
using System.Threading;

namespace FacuTheRock.Net.Algorithms.BloomFilter.Implementation
{
    public class HighlyOptimizedThreadSafeDuplicateCheckService : IDuplicateCheckService
    {
        private readonly IBloomFilter _itemsSeen;
        private readonly SpinLock _lock = new(false);

        public HighlyOptimizedThreadSafeDuplicateCheckService(
            int capacity,
            float errorRate)
            : this(new SpecializedBloomFilter(
                new BloomFilterCalculator(),
                new HashCalculator(),
                capacity, 
                errorRate)) 
            {}

        public HighlyOptimizedThreadSafeDuplicateCheckService(
            IBloomFilter bloomFilter) =>
                _itemsSeen = bloomFilter;

        public bool IsThisTheFirstTimeWeHaveSeen(int id)
        {
            var lockTaken = false;
            var result = false;

            _lock.Enter(ref lockTaken);

            try
            {
                if (lockTaken)
                {
                    if (_itemsSeen.Contains(id) == false)
                    {
                        _itemsSeen.Add(id);
                        result = true;
                    }
                }
            }
            finally
            {
                if (lockTaken)
                    _lock.Exit();
            }

            return result;
        }
    }
}
