using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web_App_101.Models;

namespace BankAccountRepository
{
    public class BankRepository : IBankRepository
    {
        private static readonly Bank ShittyBank = new Bank("ShittyBank", 720);

        public Bank GetBank()
        {
            return ShittyBank;
        }
    }
}
