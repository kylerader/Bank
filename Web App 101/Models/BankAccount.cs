using System;

namespace Web_App_101.Models
{
    public class Account
    {
        private readonly Customer _holder;
        public event Action<decimal, Customer> LargeWithdrawAlert;

        public Account(Customer customer)
        {
            _holder = customer;
            Balance = 0;
        }

        public Customer Holder
        {
            get { return _holder; }
        }

        public decimal Balance { get; set; }

        public void Deposit(decimal amount)
        {
            Balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            Balance -= amount;
            if(Balance<0)
            {
                throw new AccountOverdrawnException();
            }

            if(amount>=10000m)
            {
               LargeWithdrawAlert(amount, Holder);
            }
        }

    }

    public class BadCreditException : Exception
    {
    }

    public class AccountOverdrawnException : Exception
    {
    }
}