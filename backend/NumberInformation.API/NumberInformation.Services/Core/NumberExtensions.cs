using NumberInformation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberInformation.Services.Core
{
    public static class NumberExtensions
    {
        private static readonly DividingNumberService _dividingNumberService = new();
        private static readonly PrimeService _primeService = new(); 

        public static NIResponse GetDividingInformations(this int inputNumber)
        {
            return GetInformations(inputNumber);
        }

        public static NIResponse GetDividingInformations(this long inputNumber)
        {
            return GetInformations(inputNumber);
        }

        private static NIResponse GetInformations(long inputNumber)
        {
            var dividingNumbers = _dividingNumberService.GetDividingNumbers(inputNumber);
            var dividingPrime = dividingNumbers.Where(_primeService.IsPrime).ToList();

            return new NIResponse()
            {
                Input = inputNumber,
                DividingNumbers = dividingNumbers,
                DividingPrime = dividingPrime
            };
        }
    }
}
