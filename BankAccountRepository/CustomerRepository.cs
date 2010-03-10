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
            var db = new SqlCeConnection("DataSource=\"..\\..\\..\\MyDatabase1.sdf\"");
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

        public Customer CreateCustomer(int ficoScore, string lastName, string firstName)
        {
            var db = new SqlCeConnection("DataSource=\"..\\..\\..\\MyDatabase1.sdf\"");
            db.Open();
            var cmdString = "INSERT INTO Customer (_ficoScore,_lastName,_firstName) VALUES (" + ficoScore + ", \'" +
                lastName + "\', \'" + firstName + "\')";
            var cmd = new SqlCeCommand(cmdString, db);
            var resultSet=cmd.ExecuteResultSet(ResultSetOptions.Scrollable);
            cmdString = "SELECT * FROM Customer WHERE _lastName = \'" + lastName + "\'";
            cmd = new SqlCeCommand(cmdString, db);
            resultSet = cmd.ExecuteResultSet(ResultSetOptions.Scrollable);
            resultSet.ReadLast();
            var id = resultSet.GetInt32(resultSet.GetOrdinal("_id"));
            db.Close();
            var customer = new Customer(id, firstName, lastName, ficoScore);
            return customer;
        }

        public void Save(Customer newCustomer)
        {
            throw new NotImplementedException();
        }

        public List<Customer> GetAll()
        {
            var db = new SqlCeConnection("DataSource=\"..\\..\\..\\MyDatabase1.sdf\"");
            db.Open();
            const string cmdString = "SELECT * FROM Customer";
            var cmd = new SqlCeCommand(cmdString, db);
            var resultSet = cmd.ExecuteResultSet(ResultSetOptions.Scrollable);
            var customers = new List<Customer>();
            for (var i = 0; resultSet.ReadAbsolute(i);i++ )
            {
                var ficoScore = resultSet.GetInt32(resultSet.GetOrdinal("_ficoScore"));
                var firstName = resultSet.GetString(resultSet.GetOrdinal("_firstName"));
                var lastName = resultSet.GetString(resultSet.GetOrdinal("_lastName"));
                var id = resultSet.GetInt32(resultSet.GetOrdinal("_id"));
                var customer = new Customer(id, firstName, lastName, ficoScore);
                customers.Add(customer);
            }
           
            db.Close();
            return customers;
        }
    }
}
