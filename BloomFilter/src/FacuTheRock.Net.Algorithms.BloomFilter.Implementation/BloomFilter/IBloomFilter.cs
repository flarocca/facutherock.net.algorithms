namespace FacuTheRock.Net.Algorithms.BloomFilter.Implementation.BloomFilter
{
    public interface IBloomFilter
    {
        void Add(int id);

        bool Contains(int id);
    }
}
