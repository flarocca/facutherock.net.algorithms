namespace FacuTheRock.Net.Algorithms.BloomFilter.Implementation
{
    public interface IDuplicateCheckService
    {
        //checks the given id and returns if it is the first time we have seen it
        //IT IS CRITICAL that duplicates are not allowed through this system but false
        //positives can be tolerated at a maximum error rate of less than 1%
        bool IsThisTheFirstTimeWeHaveSeen(int id);
    }
}
