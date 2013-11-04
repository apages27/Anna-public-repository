using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextPrimeNumber
{
    class GetInput
    {
        public int GetUserInput()
        {
            bool isNumber = false;
            int input = 0;

            while (!isNumber)
            {
                Console.Write("Please enter a number: ");
                isNumber = int.TryParse(Console.ReadLine(), out input);

                if (!isNumber)
                {
                    Console.WriteLine("\nThat is not a number!  Please try again.\n");
                }
            }
            return input;
        }
    }
}
