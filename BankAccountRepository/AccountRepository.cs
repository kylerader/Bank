using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
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

        public Account CreateAccount(Customer customer)
        {
            var db = new SqlCeConnection("DataSource=\"..\\..\\..\\MyDatabase1.sdf\"");
            db.Open();
            var cmdString = "INSERT INTO Accounts (_customerId,_balance) VALUES (" + customer.Id + ", 0)";
            var cmd = new SqlCeCommand(cmdString, db);
            var resultSet = cmd.ExecuteResultSet(ResultSetOptions.Scrollable);
            cmdString = "SELECT * FROM Accounts WHERE _customerId = " + customer.Id + "";
            cmd = new SqlCeCommand(cmdString, db);
            resultSet = cmd.ExecuteResultSet(ResultSetOptions.Scrollable);
            resultSet.ReadLast();
            var id = resultSet.GetInt32(resultSet.GetOrdinal("_id"));
            db.Close();
            var account = new Account(customer, id);
            return account;
        }

        public List<Account> GetAll()
        {
            var db = new SqlCeConnection("DataSource=\"..\\..\\..\\MyDatabase1.sdf\"");
            db.Open();
            const string cmdString = "SELECT * FROM Accounts";
            var cmd = new SqlCeCommand(cmdString, db);
            var resultSet = cmd.ExecuteResultSet(ResultSetOptions.Scrollable);
            var accounts = new List<Account>();


            for (var i = 0; resultSet.ReadAbsolute(i); i++)
            {
                var customerId = resultSet.GetInt32(resultSet.GetOrdinal("_customerId"));
                var balance = resultSet.GetDecimal(resultSet.GetOrdinal("_balance"));
                var id = resultSet.GetInt32(resultSet.GetOrdinal("_id"));
                var customer = CustomerRepository.GetCustomerById(customerId);
                var account = new Account(customer, id);
                accounts.Add(account);
            }

            db.Close();
            return accounts;
        }
    }
}
