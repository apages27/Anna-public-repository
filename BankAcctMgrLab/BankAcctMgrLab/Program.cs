using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAcctMgrLab
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isStarted = true;
            string userNameEntry = "";
            Operations oper = new Operations();

            while (isStarted)
            {

                Console.WriteLine("----------------------------------------");
                Console.WriteLine("Welcome to the ATM");
                Console.WriteLine("----------------------------------------");
                Console.WriteLine();
                Console.Write("Please enter your user name: ");
                userNameEntry = Console.ReadLine();

                string userFile = oper.GetUserFile();

                if (!userFile.Contains(userNameEntry))
                {
                    User newUser = new User();
                    newUser.UserName = userNameEntry;

                    Console.Write("Please choose a 4-digit PIN for your account: ");
                    newUser.PIN = oper.ValidateAndReturnPINInput(Console.ReadLine());

                    oper.AddUserToFile(newUser);
                }
                else
                {
                    Console.Write("Please enter your PIN to access the menu: ");
                    int pinEntry = oper.ValidateAndReturnPINInput(Console.ReadLine()); 
                }

                //if (pinEntry == user.PIN)
                //{
                //    oper.DisplayUserMenu();
                //}


            }
        }
    }
}
