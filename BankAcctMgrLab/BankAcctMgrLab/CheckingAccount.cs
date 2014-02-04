using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAcctMgrLab
{
    public class CheckingAccount:IAccount
    {
        public decimal Balance
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string Deposit()
        {
            throw new NotImplementedException();
        }

        public string Withdraw()
        {
            throw new NotImplementedException();
        }
    }
}
