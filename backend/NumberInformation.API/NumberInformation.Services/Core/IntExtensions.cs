using NumberInformation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberInformation.Services.Core
{
    public static class IntExtensions
    {
        private static readonly DividingNumberService _dividingNumberService = new();
        private static readonly PrimeService _primeService = new(); 

        public static NIResponse GetDividingInformations(this int inputNumber)
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
