namespace FacuTheRock.Net.Algorithms.BloomFilter.Implementation.BloomFilter
{
    public interface IHashCalculator
    {
        int Module { get; set; }

        int ComputeHash(int primaryHash, int secondaryHash, int index);

        int HashId(int id);
    }
}
