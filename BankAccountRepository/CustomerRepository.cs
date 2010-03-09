using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web_App_101.Models;

namespace BankAccountRepository
{
    public class CustomerRepository : ICustomerRepository
    {
        public Customer GetCustomerById(int id)
        {
            throw new NotImplementedException();
        }

        public Customer CreateCustomer(Customer newCustomerToSave)
        {
            throw new NotImplementedException();
        }

        public void Save(Customer newCustomer)
        {
            throw new NotImplementedException();
        }

        public List<Customer> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
