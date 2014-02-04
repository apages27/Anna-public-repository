using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAcctMgrLab
{
    public class Operations
    {
        public void DisplayUserMenu()
        {
            Console.Write("Please select an account (\"c\" for checking or \"s\" for savings): ");
        }

        public string GetUserFile()
        {
            string userFile = File.ReadAllLines(@"C:\Users\anna.pages\Documents\Visual Studio 2013\Projects\BankAcctMgrLab\BankAcctMgrLab\bin\Debug\UserFile.txt").ToString();

            return userFile;
        }

        public void AddUserToFile(User user)
        {
            string userString = user.UserName + "," + user.PIN;
            
            File.AppendAllText(
                @"C:\Users\anna.pages\Documents\Visual Studio 2013\Projects\BankAcctMgrLab\BankAcctMgrLab\bin\Debug\UserFile.txt", userString + Environment.NewLine);
        }

        public int ValidateAndReturnPINInput(string pinEntry)
        {
            int result = 0;
            bool isInt = false;

            while (!isInt)
            {
                isInt = int.TryParse(pinEntry, out result);
                Console.Write("That was not a valid entry.  Please try again: ");
                pinEntry = Console.ReadLine();
            }

            return result;
        }
    }
}
