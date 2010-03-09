using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web_App_101.Models;
using System.Data.SqlServerCe;

namespace BankAccountRepository
{
    public class CustomerRepository : ICustomerRepository
    {
        public Customer GetCustomerById(int id)
        {
            var db = new SqlCeConnection("DataSource=\"C:/Users/Kyle Rader/Documents/Visual Studio 2010/Projects/Web App 101/MyDatabase1.sdf\"");
            db.Open();
            var cmdString = "SELECT * FROM Customer WHERE _id = " + id;
            var cmd = new SqlCeCommand(cmdString,db);
            var resultSet = cmd.ExecuteResultSet(ResultSetOptions.Scrollable);
            resultSet.ReadAbsolute(0);
            var ficoScore = resultSet.GetInt32(resultSet.GetOrdinal("_ficoScore"));
            var firstName = resultSet.GetString(resultSet.GetOrdinal("_firstName"));
            var lastName = resultSet.GetString(resultSet.GetOrdinal("_lastName"));
            db.Close();
            return new Customer(id, firstName, lastName, ficoScore);
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
