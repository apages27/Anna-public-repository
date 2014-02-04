using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAcctMgrLab
{
    public interface IAccount
    {
        decimal Balance { get; set; }

        string Deposit();
        string Withdraw();
    }
}
