using NumberInformation.Services.Core;
using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;
using System.Linq;

namespace NumberInformation.Tests
{

    [TestFixture]
    public class PrimeServiceTest
    {
        private PrimeService _primeService;

        [SetUp]
        public void SetUp()
        {
            _primeService = new ();
        }

        [TestCase(new long[] { 1, 2, 4, 5, 10, 20, 25, 50, 100 }, new long[] { 1, 2, 5})]
        [TestCase(new long[] { 1, 13 }, new long[] { 1, 13 })]
        [TestCase(new long[] { 1, 2, 3, 4, 6, 8, 12, 24 }, new long[] { 1, 2, 3 })]
        [TestCase(new long[] { 1, 999983 }, new long[] { 1, 999983 })]
        
        public void PrimeNumbers_TestCase(long[] dividingNumbers, long[] primeNumbers)
        {
            var result = dividingNumbers.Where(_primeService.IsPrime).ToList();

            result.ShouldBe(new List<long>(primeNumbers));
        }

    }
}
