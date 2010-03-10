using System;
using System.Collections.Generic;
using System.Linq;
using Web_App_101.Models;

namespace Web_App_Tests
{
    public class AccountRepositoryInMemory : IAccountRepository
    {
        private readonly List<Account> _accounts = new List<Account>();


        public Account GetAccountByCustomer(Customer customer)
        {
            var matchingAccounts = from a in _accounts where a.Holder == customer select a;
            if (matchingAccounts.Any()) return matchingAccounts.First();
            throw new NotSupportedException();
        }

        public void UpdateAccountBalance(Account account)
        {
            return;
        }

        public Account CreateAccount(Customer customer)
        {
            var accountToSave = new Account(customer);
            _accounts.Add(accountToSave);
            return accountToSave;
        }

        public List<Account> GetAll()
        {
            return _accounts;
        }

       /* public decimal Withdraw(decimal amount)
        {
            
        }

        public decimal Deposit(decimal amount){}*/
    }
}