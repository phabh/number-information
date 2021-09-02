using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberInformation.Services.Core
{
    public class DividingNumberService
    {
        public IEnumerable<int> GetDividingNumbers(int inputNumber)
        {
            var dividingNumbers = new List<int>();
            var tempNumber = inputNumber;

            dividingNumbers.Add(1);
            var dividing = 2;
            while ( Math.Abs(tempNumber) > dividing)
            {
                if( tempNumber % dividing == 0)
                {
                    dividingNumbers.Add(dividing);
                }

                dividing++;
            }

            dividingNumbers.Add(inputNumber);

            return dividingNumbers;
        }
    }
}
