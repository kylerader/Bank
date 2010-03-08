using System;
using System.Collections.Generic;

namespace Web_App_101.Models
{
    public class Bank
    {
        private readonly string _bankName;
        private readonly int _minimumCreditScore;
        private readonly List<Account> _accounts = new List<Account>();

        public Bank(string bankName, int minimumCreditScore)
        {
            _bankName = bankName;
            _minimumCreditScore = minimumCreditScore;
        }

        public bool CheckCredit(Customer customer)
        {
            return customer.FicoScore >= _minimumCreditScore;
        }

        public Account OpenAccount(Customer customer)
        {
            if(customer==null)
            {
                throw new ArgumentNullException();
            }

            if(!CheckCredit(customer))
            {
                throw new BadCreditException();
            }

            var account = new Account(customer);
            _accounts.Add(account);
            return account;
        }

        private void AttachAllAccountsToLargeWithdrawAlert(Action<decimal, Customer> newSubscriber)
        {
            _accounts.ForEach(a => a.LargeWithdrawAlert += newSubscriber);
        }

        public event Action<decimal, Customer> LargeWithdrawAlert
        {
            add { AttachAllAccountsToLargeWithdrawAlert(value); }
            remove { throw new NotImplementedException(); }
        }
    }
}