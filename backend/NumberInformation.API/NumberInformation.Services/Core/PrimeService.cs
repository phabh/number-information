using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberInformation.Services.Core
{
    public class PrimeService
    {
        public bool IsPrime(long number)
        {
            var dividing = 2L;
            while(Math.Abs(number) > dividing)
            {
                if (number % dividing == 0L) return false;
                dividing++;
            }
            return true;
        }


    }
}
