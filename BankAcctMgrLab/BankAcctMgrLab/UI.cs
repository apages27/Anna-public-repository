using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAcctMgrLab
{
    public class UI
    {
        private Operations oper = new Operations();

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

        public void DisplayUserMenu(string userName)
        {
            Console.Clear();
            Console.Write("Please select an account (\"C\" for checking or \"S\" for savings): ");

            string userChoice = Console.ReadLine().ToLower();

            if (userChoice == "c")
            {
                DisplayCheckingAccount(userName);
            }
            else if (userChoice == "s")
            {
                DisplaySavingsAccount(userName);
            }
        }

        public void DisplaySavingsAccount(string userName)
        {
            decimal savingsBalance = oper.GetSavingsBalance(userName);

            Console.Clear();
            Console.WriteLine("The current balance in your savings account is: {0}", savingsBalance);

            if (savingsBalance > 0)
            {
                Console.WriteLine("Please choose \"D\" to make a deposit or \"W\" to make a wthdrawal: ");
            }
            else
            {
                Console.Write("Would you like to make a deposit? Y/N ");
            }
        }

        public void DisplayCheckingAccount(string userName)
        {
            decimal checkingBalance = oper.GetCheckingBalance(userName);

            Console.Clear();
            Console.WriteLine("The current balance in your checking account is: {0}", checkingBalance);

            if (checkingBalance > 0)
            {
                Console.WriteLine("Please choose \"D\" to make a deposit or \"W\" to make a wthdrawal: " );
            }
            else
            {
                Console.Write("Would you like to make a deposit? Y/N ");
            }
        }
    }
}
