namespace FacuTheRock.Net.Algorithms.BloomFilter.Implementation
{
    public interface IBloomFilterCalculator
    {
        /// <summary>
        /// The best k.
        /// k = round((m / capacity) * log(2));
        /// </summary>
        /// <param name="capacity"> The capacity. </param>
        /// <param name="m"> The m value. </param>
        /// <returns> The <see cref="int"/>. </returns>
        int CalculateK(int capacity, float m);

        /// <summary>
        /// The best m. 
        /// m = ceil((capacity * log(errorRate)) / log(1 / pow(2, log(2))));
        /// </summary>
        /// <param name="capacity"> The capacity. </param>
        /// <param name="errorRate"> The error rate. </param>
        /// <returns> The <see cref="int"/>. </returns>
        int CalculateM(int capacity, float errorRate);
    }
}
