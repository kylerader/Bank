using System;

namespace Web_App_101.Models
{
    public class Account
    {
        private readonly Customer _holder;
        public event Action<decimal, Customer> LargeWithdrawAlert;
        private readonly int _id;

        public Account(Customer customer)
        {
            _holder = customer;
            _id = CustomerNumberProvisioningService.NewCustomerNumber(); ;
            Balance = 0;
        }

        public Account(Customer customer, int id)
        {
            _holder = customer;
            _id = id;
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