using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberInformation.Services.Core
{
    public class DividingNumberService
    {
        public IEnumerable<long> GetDividingNumbers(long inputNumber)
        {
            var dividingNumbers = new List<long>();
            var tempNumber = inputNumber;

            dividingNumbers.Add(1);
            var dividing = 2L;
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
