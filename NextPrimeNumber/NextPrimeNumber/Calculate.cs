using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextPrimeNumber
{
    class Calculate
    {
        public int CalculateNextPrime(int userInput)
        {
            int number = (userInput + 1);

            for (int i = 2; i <= Math.Sqrt(number);)
            {
                if (number % i == 0)
                {
                    number++;
                    i = 2;
                }
                else if (number % i != 0)
                {
                    i++;
                }
            }
            return number;
        }
    }
}
