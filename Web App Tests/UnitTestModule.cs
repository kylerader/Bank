using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Modules;
using Web_App_101.Models;

namespace Web_App_Tests
{
    class UnitTestModule : NinjectModule
    {
        public override void Load()
        {
            //BOOM!!
            Bind<ICustomerRepository>().To<CustomerRepositoryInMemory>();
            Bind<IBankRepository>().To<BankRepositoryInMemory>();
            Bind<IAccountRepository>().To<AccountRepositoryInMemory>();
            Bind<IAuditLogRepository>().To<AuditLogRepositoryInMemory>();
        }
    }
}
