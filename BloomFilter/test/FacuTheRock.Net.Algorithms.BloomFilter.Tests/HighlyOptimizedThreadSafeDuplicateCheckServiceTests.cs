using FacuTheRock.Net.Algorithms.BloomFilter.Implementation;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FacuTheRock.Net.Algorithms.BloomFilter.Tests
{
    public class HighlyOptimizedThreadSafeDuplicateCheckServiceTests
    {
        [Fact]
        public void IsThisTheFirstTimeWeHaveSeenTest_NoRepetitions()
        {
            //Arrange
            var idCount = 1_000_000;
            var errorRate = 0.01f;
            var service = new HighlyOptimizedThreadSafeDuplicateCheckService(idCount, errorRate);
            var maxDegreeOfParalellism = 1000;
            var successfulCalls = 0;
            var failedCalls = 0;
            var ids = Enumerable.Range(1, idCount).ToArray();

            var testDuplicateCheckService = new Action<int>(id =>
            {
                var firstTimeSeen = service.IsThisTheFirstTimeWeHaveSeen(id);
                if (firstTimeSeen)
                {
                    Interlocked.Increment(ref successfulCalls);
                }
                else
                {
                    Interlocked.Increment(ref failedCalls);
                }
            });

            // Act
            Parallel.ForEach(
                ids,
                new ParallelOptions
                {
                    MaxDegreeOfParallelism = maxDegreeOfParalellism
                },
                currentId => testDuplicateCheckService(currentId));

            // Assert
            Assert.True(successfulCalls >= idCount - 10_000);
            Assert.True(successfulCalls <= idCount);
            Assert.True(failedCalls < 10_000);
        }

        [Fact]
        public void IsThisTheFirstTimeWeHaveSeenTest_AllExceptOneRepeated()
        {
            //Arrange
            var idCount = 1_000_000;
            var errorRate = 0.01f;
            var service = new HighlyOptimizedThreadSafeDuplicateCheckService(idCount, errorRate);
            var maxDegreeOfParalellism = 1000;
            var successfulCalls = 0;
            var failedCalls = 0;
            var ids = Enumerable.Range(1, idCount);

            var testDuplicateCheckService = new Action<int>(id =>
            {
                var firstTimeSeen = service.IsThisTheFirstTimeWeHaveSeen(id);
                if (firstTimeSeen)
                {
                    Interlocked.Increment(ref successfulCalls);
                }
                else
                {
                    Interlocked.Increment(ref failedCalls);
                }
            });

            // Act
            Parallel.ForEach(
                ids,
                new ParallelOptions
                {
                    MaxDegreeOfParallelism = maxDegreeOfParalellism
                },
                currentId => testDuplicateCheckService(1));

            // Assert
            Assert.True(successfulCalls >= 1);
            Assert.True(successfulCalls <= 10_000);
            Assert.True(failedCalls > 990_000);
            Assert.True(failedCalls < 1_000_000);
        }

        [Fact]
        public void IsThisTheFirstTimeWeHaveSeenTest_HalfRepeated()
        {
            //Arrange
            var idCount = 1_000_000;
            var errorRate = 0.01f;
            var service = new HighlyOptimizedThreadSafeDuplicateCheckService(idCount, errorRate);
            var maxDegreeOfParalellism = 1000;
            var successfulCalls = 0;
            var failedCalls = 0;
            var ids = Enumerable.Range(1, idCount)
                .Concat(Enumerable.Range(1, idCount));

            service.IsThisTheFirstTimeWeHaveSeen(1);

            var testDuplicateCheckService = new Action<int>(id =>
            {
                var firstTimeSeen = service.IsThisTheFirstTimeWeHaveSeen(id);
                if (firstTimeSeen)
                {
                    Interlocked.Increment(ref successfulCalls);
                }
                else
                {
                    Interlocked.Increment(ref failedCalls);
                }
            });

            // Act
            Parallel.ForEach(
                ids,
                new ParallelOptions
                {
                    MaxDegreeOfParallelism = maxDegreeOfParalellism
                },
                currentId => testDuplicateCheckService(currentId));

            // Assert
            Assert.True(successfulCalls >= idCount - 10_000);
            Assert.True(successfulCalls <= idCount + 10_000);
            Assert.True(failedCalls >= idCount - 10_000);
            Assert.True(failedCalls <= idCount + 10_000);
        }
    }
}
