using NumberInformation.Models;
using NumberInformation.Services.Core;
using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;

namespace NumberInformation.Tests
{

    [TestFixture]
    public class ProcessBackgroundTest
    {
        private IProcessBackground<NIResponse> _processBackground;

        [SetUp]
        public void SetUp()
        {
            _processBackground = new MemoryProcessBackground<NIResponse>(); 
        }

        [TestCase(10,new long[] { 1, 2, 5, 10 }, new long[] { 1, 2, 5 }) ]
        [TestCase(100, new long[] { 1, 2, 4, 5, 10, 20, 25, 50, 100 }, new long[] { 1, 2, 5 })]
        [TestCase(13, new long[] { 1, 13 }, new long[] { 1, 13 })]
        [TestCase(15, new long[] { 1, 3, 5, 15 }, new long[] { 1, 3, 5 })]
        [TestCase(24, new long[] { 1, 2, 3, 4, 6,  8, 12, 24  }, new long[] { 1, 2, 3})]
        [TestCase(999983, new long[] { 1, 999983 }, new long[] { 1, 999983 })]
        
        public void DividingNumber_Cases(long inputNumber, long[] dividingNumbers, long[] primeNumbers)
        {
            var taskId = _processBackground.ExecuteInBackground(() => inputNumber.GetDividingInformations());
            var result = _processBackground.GetResultBackgroundTask(taskId);
            while (result?.Input == null)
            {
                result = _processBackground.GetResultBackgroundTask(taskId);
            }

            result.Input.ShouldBe(inputNumber);
            result.DividingNumbers.ShouldBe(new List<long>(dividingNumbers));
            result.DividingPrime.ShouldBe(new List<long>(primeNumbers));
        }

    }
}
