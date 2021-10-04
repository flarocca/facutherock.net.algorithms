using System;
using System.Collections;

namespace FacuTheRock.Net.Algorithms.BloomFilter.Implementation.BloomFilter
{
    public class SpecializedBloomFilter : IBloomFilter
    {
        private readonly int _hashFunctionCount;
        private readonly BitArray _hashBits;
        private readonly IHashCalculator _hashCalculator;

        /// <summary>
        /// Creates a new Specialized Bloom filter.
        /// </summary>
        /// <param name="bloomFilterCalculator">Calculates K and M values according to capacity and error rate.</param>
        /// <param name="hashCalculator">Computes hashes of the ids provided.</param>
        /// <param name="capacity">The anticipated number of items to be added to the filter. More than this number of items can be added, but the error rate will exceed what is expected.</param>
        /// <param name="errorRate">The accepable false-positive rate (e.g., 0.01F = 1%)</param>
        public SpecializedBloomFilter(
            IBloomFilterCalculator bloomFilterCalculator,
            IHashCalculator hashCalculator,
            int capacity,
            float errorRate)
        {
            if (capacity < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity), capacity, "capacity must be > 0");
            }

            if (errorRate >= 1 || errorRate <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(errorRate), errorRate, $"errorRate must be between 0 and 1, exclusive. Was {errorRate}");
            }

            var m = bloomFilterCalculator.CalculateM(capacity, errorRate);
            _hashFunctionCount = bloomFilterCalculator.CalculateK(capacity, m);
            _hashBits = new BitArray(m);
            _hashCalculator = hashCalculator;

            _hashCalculator.Module = _hashBits.Count;
        }

        public void Add(int id)
        {
            ComputeHash(id, hash =>
            {
                _hashBits[hash] = true;

                return null;
            });
        }

        public bool Contains(int id)
        {
            return ComputeHash(id, hash =>
            {
                if (_hashBits[hash] == false)
                {
                    return false;
                }

                return null;
            });
        }

        private bool ComputeHash(int id, Func<int, bool?> onHashComputed)
        {
            var primaryHash = id;
            var secondaryHash = _hashCalculator.HashId(id);

            for (int i = 0; i < _hashFunctionCount; i++)
            {
                var hash = _hashCalculator.ComputeHash(primaryHash, secondaryHash, i);
                var result = onHashComputed(hash);
                if (result.HasValue)
                    return result.Value;
            }

            return true;
        }
    }
}
