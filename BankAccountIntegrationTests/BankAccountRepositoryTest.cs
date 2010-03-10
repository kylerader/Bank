using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using Ninject.Modules;
using NUnit.Framework;
using Web_App_101.Controllers;
using Web_App_101.Models;
using BankAccountRepository;

namespace BankAccountIntegrationTests
{
    [TestFixture]
    class BankAccountRepositoryTest
    {
        readonly StandardKernel _kernel = new StandardKernel(new UnitTestModule());

        [Test]
        public void GetAccountNumber1()
        {
            var bankAccountController = _kernel.Get<BankAccountController>();
            var customer = bankAccountController.CustomerRepository.GetCustomerById(1);
            Assert.AreEqual(customer.LastName,"Lumberg");
        }

        [Test]
        public void InsertNewCustomer()
        {
            var bankAccountController = _kernel.Get<BankAccountController>();
            var viewResult = bankAccountController.Create("Joe2", "Blow2", 800);
            var firstCustomer = bankAccountController.CustomerRepository.GetAll().Last();
            Assert.AreEqual("Joe2", firstCustomer.FirstName);
            Assert.AreEqual("Blow2", firstCustomer.LastName);
        }


    }

    class UnitTestModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICustomerRepository>().To<CustomerRepository>();
            Bind<IBankRepository>().To<BankRepository>();
            Bind<IAccountRepository>().To<AccountRepository>();
            Bind<IAuditLogRepository>().To<AuditLogRepository>();
        }
    }
}
