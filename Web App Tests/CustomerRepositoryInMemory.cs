using System;
using System.Collections.Generic;
using System.Linq;
using Web_App_101.Models;

namespace Web_App_Tests
{
    public class CustomerRepositoryInMemory : ICustomerRepository
    {
        private readonly List<Customer> _customers = new List<Customer>();

        public Customer GetCustomerById(int id)
        {
            var matchingCustomers = from c in _customers where c.Id == id select c;
            if (matchingCustomers.Any()) return matchingCustomers.First();
            throw new NotSupportedException();
        }

        public Customer CreateCustomer(Customer newCustomerToSave)
        {
            _customers.Add(newCustomerToSave);
            return newCustomerToSave;
        }

        public void Save(Customer newCustomer)
        {
            throw new NotImplementedException();
        }

        public List<Customer> GetAll()
        {
            return _customers;
        }
    }
}