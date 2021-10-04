using System;

namespace FacuTheRock.Net.Algorithms.BloomFilter.Implementation.BloomFilter
{
    public class HashCalculator : IHashCalculator
    {
        public int Module { get; set; }

        /// <summary>
        /// Performs Dillinger and Manolios double hashing. 
        /// </summary>
        public int ComputeHash(int primaryHash, int secondaryHash, int index)
        {
            int resultingHash = (primaryHash + (index * secondaryHash)) % Module;
            return Math.Abs(resultingHash);
        }

        /// <summary>
        /// Hashes a 32-bit signed int using Thomas Wang's method v3.1 (http://www.concentric.net/~Ttwang/tech/inthash.htm).
        /// Runtime is suggested to be 11 cycles. 
        /// </summary>
        public int HashId(int id)
        {
            uint x = (uint)id;
            unchecked
            {
                x = ~x + (x << 15);
                x ^= (x >> 12);
                x += (x << 2);
                x ^= (x >> 4);
                x *= 2057;
                x ^= (x >> 16);

                return (int)x;
            }
        }
    }
}
