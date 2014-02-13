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
        UI ui = new UI();

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
            string userString = user.UserName + "," + user.PIN;

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
                    pinEntry = ui.GetIntInputFromUser("validInt");
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

            newUser.PIN = ValidateAndReturnPINInput(ui.GetIntInputFromUser("newPIN"));

            AddUserToFile(newUser);
        }

        public bool GetPINForCurrentUser(string userNameEntry)
        {
            bool correctPIN = false;

            int pinEntry = ValidateAndReturnPINInput(ui.GetIntInputFromUser("currentPIN"));

            while (!correctPIN)
            {
                correctPIN = CheckIfCorrectPIN(pinEntry, userNameEntry);
                pinEntry = ValidateAndReturnPINInput(ui.GetIntInputFromUser("correctPIN"));
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

                foreach (string s in splitUserFile)
                {
                    if (s == userNameEntry && (s + 1) == pinEntry.ToString())
                    {
                        correctPIN = true;
                    }
                }
            }

            return correctPIN;
        }
    }
}
