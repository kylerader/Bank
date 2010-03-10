using System;
using System.Collections.Generic;
using System.Text;

namespace Web_App_101.Models
{
    public static class CustomerNumberProvisioningService
    {
        public static int NewCustomerNumber()
        {
            var randomGenerator = new Random(Guid.NewGuid().GetHashCode());
            return randomGenerator.Next();
        }
    }

    public interface ICustomerRepository
    {
        Customer GetCustomerById(int id);
        //Customer CreateCustomer(Customer newCustomerToSave);
        Customer CreateCustomer(int ficoScore, string lastName, string firstName);
        void Save(Customer newCustomer);
        List<Customer> GetAll();
    }

    public interface IBankRepository
    {
        Bank GetBank();
    }

    public interface IAccountRepository
    {
        Account GetAccountByCustomer(Customer customer);
        Account CreateAccount(Account accountToSave);
        List<Account> GetAll();
    }

    public interface IAuditLogRepository
    {
        void WriteEntries(AuditLog auditLogToSave);
        IEnumerable<Audit> GetAll();
    }
}
