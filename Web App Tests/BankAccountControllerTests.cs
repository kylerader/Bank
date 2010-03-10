using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using NUnit.Framework;
using Web_App_101.Controllers;
using Web_App_101.Models;

namespace Web_App_Tests
{
    [TestFixture]
    public class BankAccountControllerTests
    {
        readonly StandardKernel _kernel = new StandardKernel(new UnitTestModule());
        
        
        [Test]
        public void CanCreateBankAccountController()
        {
            var bankAccountController = new BankAccountController();
            Assert.Pass();
        }

        [Test]
        public void CreateAccountResultsInHavingANewCustomer()
        {
            //this has been changed AGAIN
            var bankAccountController = _kernel.Get<BankAccountController>();
            var viewResult = bankAccountController.Create("Joe", "Blow", 800);
            var firstCustomer = bankAccountController.CustomerRepository.GetAll().Last();
            Assert.AreEqual("Joe", firstCustomer.FirstName);
            Assert.AreEqual("Blow", firstCustomer.LastName);
        }

        [Test]
        public void CreateAccountResultsInHavingANewAccount()
        {
            var bankAccountController = _kernel.Get<BankAccountController>();
            var viewResult = bankAccountController.Create("Joe", "Blow", 800);
            var firstAccount = bankAccountController.AccountRepository.GetAll().Last();
            Assert.AreEqual("Joe", firstAccount.Holder.FirstName);
            Assert.AreEqual("Blow", firstAccount.Holder.LastName);
        }

        [Test]
        public void DepositResultsInHavingMoney()
        {
            var bankAccountController = _kernel.Get<BankAccountController>();
            var viewResult = bankAccountController.Create("Joe", "Blow", 800);
            var firstCustomer = bankAccountController.CustomerRepository.GetAll().First();
            bankAccountController.Deposit(firstCustomer.Id, 500m);
            var firstAccount = bankAccountController.AccountRepository.GetAll().First();
            Assert.AreEqual(500, firstAccount.Balance);
        }


        [Test]
        public void WithdrawResultsInLosingMoney()
        {
            var bankAccountController = _kernel.Get<BankAccountController>();
            var viewResult = bankAccountController.Create("Joe", "Blow", 800);
            var firstCustomer = bankAccountController.CustomerRepository.GetAll().First();
            bankAccountController.Deposit(firstCustomer.Id, 500m);
            bankAccountController.Withdraw(firstCustomer.Id, 200m);
            var firstAccount = bankAccountController.AccountRepository.GetAll().First();
            Assert.AreEqual(300, firstAccount.Balance);
        }

        [Test]
        public void LargeWithdrawResultsInAudit()
        {
            var bankAccountController = _kernel.Get<BankAccountController>();
            var viewResult = bankAccountController.Create("Joe", "Blow", 800);
            var firstCustomer = bankAccountController.CustomerRepository.GetAll().First();
            bankAccountController.Deposit(firstCustomer.Id, 30000m);
            bankAccountController.Withdraw(firstCustomer.Id, 11000m);
            bankAccountController.Withdraw(firstCustomer.Id, 10000m);
            var auditLog = bankAccountController.AuditLogRepository.GetAll();
            Assert.AreEqual(2, auditLog.Count());
        }
    }
}
