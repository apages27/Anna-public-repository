using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAcctMgrLab
{
    class Program
    {
        public static UI ui;

        static void Main(string[] args)
        {
            bool isStarted = true;
            string userNameEntry = "";
            Operations oper = new Operations();
            ui = new UI();

            while (isStarted)
            {
                ui.Initialize();

                userNameEntry = ui.GetStringInputFromUser("userName");

                string[] userFile = oper.GetUserFile();

                oper.DecideIfNewOrCurrentUser(userFile, userNameEntry);

                decimal userAccountBalance = ui.DisplayUserMenu(userNameEntry);

                string accountAction = ui.GetUserAccountActionChoice(userAccountBalance);

                switch (accountAction)
                {
                    case "n":
                        string userChoice = ui.OfferMainMenuOrQuit();

                        if (userChoice == "m")
                        {
                            ui.DisplayUserMenu(userNameEntry);
                        }
                        else
                        {
                            isStarted = false;
                        }
                        break;
                    case "y":
                    case "d":
                        decimal userAmount = ui.DisplayAccountActionScreen(accountAction);

                        break;
                    case "w":
                        userAmount = ui.DisplayAccountActionScreen(accountAction);
                        break;
                }


            }
        }
    }
}
