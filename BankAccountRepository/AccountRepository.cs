using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web_App_101.Models;

namespace BankAccountRepository
{
    public class AccountRepository : IAccountRepository
    {
        public Account GetAccountByCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public Account CreateAccount(Account accountToSave)
        {
            throw new NotImplementedException();
        }

        public List<Account> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
