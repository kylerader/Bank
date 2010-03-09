using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Web_App_101.Models;

namespace Web_App_101.Controllers
{
    public class BankAccountController : Controller
    {
        //now this is also here HAHA
        //and this is also here more blah
        [Inject]
        public ICustomerRepository CustomerRepository { get; set; }
        [Inject]
        public IBankRepository BankRepository { get; set; }
        [Inject]
        public IAccountRepository AccountRepository { get; set; }
        [Inject]
        public IAuditLogRepository AuditLogRepository { get; set; }


        public ActionResult Index()
        {
            ViewData["Message"] = "Open an account with IniTech!";
            return View();
        }

        //
        // GET: /BankAccount/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /BankAccount/Create
        
        [HttpPost]
        public ActionResult Create(string firstName, string lastName, int? ficoScore)
        {
            var bank = BankRepository.GetBank();
            var auditLog = new AuditLog(bank);
            if (ficoScore == null) throw new NotSupportedException();
            var customer = new Customer(firstName, lastName, ficoScore.Value);
            var creditPassed = bank.CheckCredit(customer);
            if (creditPassed)
            {
                var newCustomer = CustomerRepository.CreateCustomer(customer);
                var account = bank.OpenAccount(newCustomer);
                var newAccount = AccountRepository.CreateAccount(account);
                return RedirectToAction("View", "BankAccount", newCustomer.Id);
            }
            AuditLogRepository.WriteEntries(auditLog);
            return View();
        }

        //Im putting coments all over!
        [HttpPost]
        public ActionResult Deposit(int id, decimal amount)
        {
            var bank = BankRepository.GetBank();
            var auditLog = new AuditLog(bank);
            var customer = CustomerRepository.GetCustomerById(id);
            if (customer == null) throw new NotSupportedException(); 
            
            var account = AccountRepository.GetAccountByCustomer(customer);
            if (account == null) throw new NotSupportedException(); 
    
            account.Deposit(amount);
            AuditLogRepository.WriteEntries(auditLog);
            return RedirectToAction("View", "BankAccount", customer.Id);
        }

        [HttpPost]
        public ActionResult AuditLog()
        {
            var bank = BankRepository.GetBank();
            var auditLog = new AuditLog(bank);

            var audits = AuditLogRepository.GetAll();
            return View();
        }

        public ActionResult View(int accountId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public ActionResult Withdraw(int id, decimal amount)
        {
            var bank = BankRepository.GetBank();
            var auditLog = new AuditLog(bank);
            var customer = CustomerRepository.GetCustomerById(id);
            if (customer == null) throw new NotSupportedException();

            var account = AccountRepository.GetAccountByCustomer(customer);
            if (account == null) throw new NotSupportedException();

            account.Withdraw(amount);
            AuditLogRepository.WriteEntries(auditLog);
            return RedirectToAction("View", "BankAccount", customer.Id);
        }
    }
}
