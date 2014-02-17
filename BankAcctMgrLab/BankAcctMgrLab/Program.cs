using System;
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

                ui.DisplayUserMenu(userNameEntry);
            }
        }
    }
}
