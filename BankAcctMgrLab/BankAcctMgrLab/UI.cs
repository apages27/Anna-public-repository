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
                case "validChoice":
                    Console.Write("That was not a valid choice.  Please try again: ");
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

        public string GetDecimalInputFromUser(string inputType)
        {
            switch (inputType)
            {
                case "validDec":
                    Console.Write("That was not a valid entry.  Please try again: ");
                    break;
            }

            return Console.ReadLine();
        }

        public decimal DisplayUserMenu(string userName)
        {
            Console.Clear();
            Console.Write("Please select an account (\"C\" for checking or \"S\" for savings): ");

            string userChoice = Console.ReadLine().ToLower();

            while (userChoice != "c" && userChoice != "s")
            {
                userChoice = GetStringInputFromUser("validChoice").ToLower();
            }

            decimal accountBalance = DisplayAccount(userChoice, userName);

            return accountBalance;
        }

        public decimal DisplayAccount(string userChoice, string userName)
        {
            decimal balance;

            if (userChoice == "c")
            {
                balance = oper.GetCheckingBalance(userName);
                userChoice = "checking";
            }
            else
            {
                balance = oper.GetSavingsBalance(userName);
                userChoice = "savings";
            }

            Console.Clear();
            Console.WriteLine("The current balance in your {0} account is: {1:c}", userChoice, balance);
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine();

            return balance;
        }

        public string GetUserAccountActionChoice(decimal balance)
        {
            string userChoice;

            if (balance > 0)
            {
                Console.WriteLine("Please choose \"D\" to make a deposit or \"W\" to make a withdrawal: ");
                userChoice = Console.ReadLine().ToLower();
            }
            else
            {
                Console.Write("Would you like to make a deposit? Y/N ");
                userChoice = Console.ReadLine().ToLower();
            }

            return userChoice;
        }

        public decimal DisplayAccountActionScreen(string action)
        {
            decimal amount = 0.00m;

            if (action == "d" || action == "y")
            {
                Console.Write("Please enter the amount you wish to deposit: ");
                bool validDecimal = decimal.TryParse(Console.ReadLine(), out amount);

                while (!validDecimal)
                {
                    validDecimal = decimal.TryParse(GetDecimalInputFromUser("validDec"), out amount);
                }
            }
            else if (action == "w")
            {
                Console.Write("Please enter the amount you wish to deposit: ");
                bool validDecimal = decimal.TryParse(Console.ReadLine(), out amount);

                while (!validDecimal)
                {
                    validDecimal = decimal.TryParse(GetDecimalInputFromUser("validDec"), out amount);
                }
            }

            return amount;
        }

        public string OfferMainMenuOrQuit()
        {
            Console.Write("Would you like to return to the Main Menu (\"M\"), or quit (\"Q\")?");
            string choice = Console.ReadLine().ToLower();

            while (choice != "m" && choice != "q")
            {
                choice = GetStringInputFromUser("validChoice");
            }

            return choice;
        }
    }
}
