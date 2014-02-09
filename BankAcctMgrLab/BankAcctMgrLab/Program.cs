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
            UI ui = new UI();

            while (isStarted)
            {
                ui.Initialize();

                userNameEntry = ui.GetStringInputFromUser("userName");

                string[] userFile = oper.GetUserFile();



                //if (pinEntry == user.PIN)
                //{
                //    oper.DisplayUserMenu();
                //}


            }
        }
    }
}
