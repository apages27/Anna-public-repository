using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAcctMgrLab
{
    public class UI
    {
        public void Initialize()
        {
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Welcome to the ATM");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine();
        }

        public string GetStringInputFromUser(string inputType)
        {
            switch (inputType)
            {
                case "userName":
                    Console.Write("Please enter your user name: ");
                    break;
            }

            return Console.ReadLine();
        }

        public string GetIntInputFromUser(string inputType)
        {
            switch (inputType)
            {
                case "newPIN":
                    Console.Write("Please choose a 4-digit PIN for your account: ");
                    break;
                case "currentPIN":
                    Console.Write("Please enter your PIN to access the menu: ");
                    break;
                case "validInt":
                    Console.Write("That was not a valid entry.  Please try again: ");
                    break;
                case "correctPIN":
                    Console.Write("That was not the correct PIN.  Please try again: ");
                    break;
            }

            return Console.ReadLine();
        }

        public void DisplayUserMenu()
        {
            Console.Write("Please select an account (\"c\" for checking or \"s\" for savings): ");
        }
    }
}
