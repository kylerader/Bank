using System;
using NUnit.Framework;
using Web_App_101.Models;

namespace Web_App_Tests
{
    [TestFixture]
    public class CustomerTests
    {
        
        [Test]
        public void CustomerCanBeCreated()
        {
            var customer = new Customer(1,"Milton", "Waddams", 720);
            Assert.AreEqual("Milton", customer.FirstName);
            Assert.AreEqual("Waddams", customer.LastName);
            Assert.AreEqual(720, customer.FicoScore);
        }

        [Test]
        public void GoodFicoScoreCreditCheck()
        {
            var customer = new Customer(1, "Milton", "Waddams", 720);
            var bank = new Bank("Initech Bank", 680);
            var canOpenAccount = bank.CheckCredit(customer.FicoScore);
            Assert.AreEqual(true, canOpenAccount);
        }

        [Test]
        public void BadFicoScoreCreditCheck()
        {
            var customer = new Customer(1, "Peter", "Gibbons", 600);
            var bank = new Bank("Initech Bank", 680);
            var canOpenAccount = bank.CheckCredit(customer.FicoScore);
            Assert.AreEqual(false, canOpenAccount);
        }

        [Test]
        public void OpeningAccountWithBadCreditThrowsException()
        {
            var customer = new Customer(1, "Peter", "Gibbons", 600);
            var bank = new Bank("Initech Bank", 680);
            Assert.Throws<BadCreditException>(() => bank.OpenAccount(customer));
        }

        [Test]
        public void OpeningAccountWithNoCustomerThrowsException()
        {
            var bank = new Bank("Initech Bank", 680);
            Customer customer = null;
            Assert.Throws<ArgumentNullException>(() => bank.OpenAccount(customer));
        }

        [Test]
        public void OpeningAccountWithGoodCreditResultsInAccount()
        {
            var customer = new Customer(1, "Milton", "Waddams", 720);
            var bank = new Bank("Initech Bank", 680);
            var newAccount = bank.OpenAccount(customer);
            Assert.AreSame(customer, newAccount.Holder);
        }
    }


    [TestFixture]
    public class AccountTests
    {
        [Test]
        public void DepositCashIntoAccountWorks()
        {
            var customer = new Customer(1, "Milton", "Waddams", 720);
            var bank = new Bank("Initech Bank", 680);
            var account = bank.OpenAccount(customer);
            account.Deposit(42m);
            account.Deposit(37m);
            Assert.AreEqual(79m, account.Balance);
        }

        [Test]
        public void WithdrawCashFromAccountWorks()
        {
            var customer = new Customer(1, "Milton", "Waddams", 720);
            var bank = new Bank("Initech Bank", 680);
            var account = bank.OpenAccount(customer);
            account.Deposit(50m);
            account.Withdraw(10m);
            Assert.AreEqual(40m, account.Balance);
        }

        [Test]
        public void OverdrawingAccountThrowsException()
        {
            var customer = new Customer(1, "Milton", "Waddams", 720);
            var bank = new Bank("Initech Bank", 680);
            var account = bank.OpenAccount(customer);
            Assert.Throws<AccountOverdrawnException>(() => account.Withdraw(10m));
        }

        [Test]
        public void WithdrawingOver10KCausesBigWithdrawAlert()
        {
            var customer = new Customer(1, "Milton", "Waddams", 720);
            var bank = new Bank("Initech Bank", 680);
            var account = bank.OpenAccount(customer);
            var alerted = false;
            Action<decimal, Customer> handleAccountAlert = (amount, cust) => alerted = true;
            account.LargeWithdrawAlert += handleAccountAlert;
            account.Deposit(10000m);
            account.Withdraw(10000m);
            Assert.AreEqual(true, alerted);
        }

        [Test]
        public void WithdrawingOver10KCreatesAnAuditEntry()
        {
            var customer = new Customer(1, "Milton", "Waddams", 720);
            var bank = new Bank("Initech Bank", 680);

            var account = bank.OpenAccount(customer);
            var auditlog = new AuditLog(bank);
            account.Deposit(10000m);
            account.Withdraw(10000m);
            Assert.AreEqual(1, auditlog.Count);

        }
    }
}