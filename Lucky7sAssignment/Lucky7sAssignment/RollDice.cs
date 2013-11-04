using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucky7sAssignment
{
    class RollDice
    {
        public int RolltheDice()
        {
            int rolls = 0;
            int bankRoll = 100;
            Random r = new Random();

            while (bankRoll > 0)
            {
                int rollDie1 = r.Next(1, 7);
                int rollDie2 = r.Next(1, 7);
                rolls++;

                if (rollDie1 + rollDie2 == 7)
                {
                    bankRoll = bankRoll + 3;
                }
                else
                {
                    bankRoll = bankRoll - 1;
                }
            }

            return rolls;
        }
    }
}
