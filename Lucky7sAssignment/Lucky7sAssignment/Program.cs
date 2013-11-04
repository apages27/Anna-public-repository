using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucky7sAssignment
{
    class Program
    {
        private static void Main(string[] args)
        {
            RollDice rd = new RollDice();
            bool isOver = false;

            while (!isOver)
            {
                Console.WriteLine("\n{0} dice rolls until broke.", rd.RolltheDice());
                Console.Write("\nPlay again? Y/N ");

                if (Console.ReadLine().ToUpper() == "N")
                {
                    isOver = true;
                }
            }
        }
    }
}
