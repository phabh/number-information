using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberInformation.Services.Core
{
    public class PrimeService
    {
        public bool IsPrime(int number)
        {
            var dividing = 2;
            while(Math.Abs(number) > dividing)
            {
                if (number % dividing == 0) return false;
                dividing++;
            }
            return true;
        }


    }
}
