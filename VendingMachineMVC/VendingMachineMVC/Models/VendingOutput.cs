using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendingMachineMVC.Models
{
    public class VendingOutput
    {
        public decimal Change { get; set; }
        public decimal Quarters { get; set; }
        public decimal Dimes { get; set; }
        public decimal Nickels { get; set; }
        public decimal Pennies { get; set; }

        public VendingOutput()
        {

        }

        public VendingOutput(UserInput input, VendingItem chosenItem)
        {
             
            Change = (input.UserPaid.Value - chosenItem.Price);

            if (Change > 0.00M)
            {
                Quarters = Change/0.25M;
                if (Quarters < 1)
                {
                    Quarters = 0;
                }
                decimal remainder = (Change%0.25M);
                Quarters = Math.Floor(Quarters);
                if (remainder == 0)
                {
                    Dimes = 0;
                    Nickels = 0;
                    Pennies = 0;
                }
                else
                    Dimes = remainder/0.10M;
                if (Dimes < 1)
                {
                    Dimes = 0;
                }
                remainder = (remainder%0.10M);
                Dimes = Math.Floor(Dimes);
                if (remainder == 0)
                {
                    Nickels = 0;
                    Pennies = 0;
                }
                else
                    Nickels = remainder/0.05M;
                if (Nickels < 1)
                {
                    Nickels = 0;
                }
                remainder = (remainder%0.05M);
                Nickels = Math.Floor(Nickels);
                if (remainder == 0)
                {
                    Pennies = 0;
                }
                else
                    Pennies = remainder/0.01M;
            }
        }
    }
}