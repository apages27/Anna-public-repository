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
        public string[] GetUserFile()
        {
            string[] userFile = new string[0];

            if (File.Exists(@".\UserFile.txt"))
            {
                userFile = File.ReadAllLines(@".\UserFile.txt");
            }

            return userFile;
        }

        public void AddUserToFile(User user)
        {
            string userString = user.UserName + "," + user.PIN + "," + 0.00M + "," + 0.00M;

            File.AppendAllText(
                @".\UserFile.txt", userString + Environment.NewLine);
        }

        public int ValidateAndReturnPINInput(string pinEntry)
        {
            int result = 0;
            bool isInt = false;

            while (!isInt)
            {
                isInt = int.TryParse(pinEntry, out result);

                if (!isInt)
                {
                    pinEntry = Program.ui.GetIntInputFromUser("validInt");
                }
            }

            return result;
        }

        public bool DecideIfNewOrCurrentUser(string[] userFile, string userNameEntry)
        {
            bool currentUser = false;

            foreach (string user in userFile)
            {
                if (!user.Contains(userNameEntry))
                {
                    GetPINForNewUser(userNameEntry);
                    break;
                }
                else
                {
                    GetPINForCurrentUser(userNameEntry);
                    currentUser = true;
                }
            }

            return currentUser;
        }

        public void GetPINForNewUser(string userNameEntry)
        {
            User newUser = new User();
            newUser.UserName = userNameEntry;

            newUser.PIN = ValidateAndReturnPINInput(Program.ui.GetIntInputFromUser("newPIN"));

            AddUserToFile(newUser);
        }

        public bool GetPINForCurrentUser(string userNameEntry)
        {
            bool correctPIN = false;

            int pinEntry = ValidateAndReturnPINInput(Program.ui.GetIntInputFromUser("currentPIN"));

            while (!correctPIN)
            {
                correctPIN = CheckIfCorrectPIN(pinEntry, userNameEntry);
                if (!correctPIN)
                {
                    pinEntry = ValidateAndReturnPINInput(Program.ui.GetIntInputFromUser("correctPIN")); 
                }
            }

            return correctPIN;
        }

        public bool CheckIfCorrectPIN(int pinEntry, string userNameEntry)
        {
            bool correctPIN = false;

            string[] userFile = GetUserFile();

            foreach (string user in userFile)
            {
                string[] splitUserFile = user.Split(',');

                if (splitUserFile[0] == userNameEntry && splitUserFile[1] == pinEntry.ToString())
                {
                    correctPIN = true;
                }
            }

            return correctPIN;
        }

        public decimal GetCheckingBalance(string userName)
        {
            string[] userFile = GetUserFile();

            decimal checkingBalance = 0.00M;

            foreach (string user in userFile)
            {
                string[] splitUserFile = user.Split(',');

                if (splitUserFile[0] == userName && splitUserFile.Length > 2)
                {
                    checkingBalance = decimal.Parse(splitUserFile[2]);
                }
            }
            return checkingBalance;
        }

        public decimal GetSavingsBalance(string userName)
        {
            string[] userFile = GetUserFile();

            decimal savingsBalance = 0.00M;

            foreach (string user in userFile)
            {
                string[] splitUserFile = user.Split(',');

                if (splitUserFile[0] == userName && splitUserFile.Length > 2)
                {
                    savingsBalance = decimal.Parse(splitUserFile[3]);
                }
            }
            return savingsBalance;
        }
    }
}
