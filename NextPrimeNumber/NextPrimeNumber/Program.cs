using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextPrimeNumber
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isQuit = false;

            while (!isQuit)
            {
                Console.Clear();
                GetInput getter = new GetInput();
                Calculate calc = new Calculate();

                Console.WriteLine("\nThe next prime number after your entry is: {0} ", calc.CalculateNextPrime(getter.GetUserInput()));
                Console.Write("\nTry another number?  Y/N ");

                if (Console.ReadLine().ToUpper() == "N")
                {
                    isQuit = true;
                }
            }
        }
    }
}
