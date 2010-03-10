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

        public Account CreateAccount(Account accountToSave)
        {
            var db = new SqlCeConnection("DataSource=\"..\\..\\..\\MyDatabase1.sdf\"");
            db.Open();
            var cmdString = "INSERT INTO Accounts (_customerId,_balance) VALUES (" + accountToSave.Holder.Id + ", " +
                accountToSave.Balance + ")";
            var cmd = new SqlCeCommand(cmdString, db);
            var resultSet = cmd.ExecuteResultSet(ResultSetOptions.Scrollable);
            cmdString = "SELECT * FROM Accounts WHERE _customerId = " + accountToSave.Holder.Id + "";
            cmd = new SqlCeCommand(cmdString, db);
            resultSet = cmd.ExecuteResultSet(ResultSetOptions.Scrollable);
            resultSet.ReadLast();
            var id = resultSet.GetInt32(resultSet.GetOrdinal("_id"));
            db.Close();
            var account = new Account(accountToSave.Holder);
            return customer;
        }

        public List<Account> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
