using NumberInformation.Services.Core;
using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;

namespace NumberInformation.Tests
{

    [TestFixture]
    public class DivindingNumbersTest
    {
        private DividingNumberService _dividingNumberService;

        [SetUp]
        public void SetUp()
        {
            _dividingNumberService = new DividingNumberService();
        }

        [TestCase(10,new int[] { 1, 2, 5, 10 })]
        [TestCase(100, new int[] { 1, 2, 4, 5, 10, 20, 25, 50, 100 })]
        [TestCase(13, new int[] { 1, 13 })]
        [TestCase(15, new int[] { 1, 3, 5, 15 })]
        [TestCase(24, new int[] { 1, 2, 3, 4, 6,  8, 12, 24  })]
        [TestCase(999983, new int[] { 1, 999983 })]
        
        public void DividingNumber_Cases( int inputNumber, int[] dividingNumbers)
        {
            var result = _dividingNumberService.GetDividingNumbers(inputNumber);

            result.ShouldBe(new List<int>(dividingNumbers));
        }

    }
}
