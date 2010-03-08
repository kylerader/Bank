using System.Collections.ObjectModel;

namespace Web_App_101.Models
{
    public class AuditLog : Collection<Audit>
    {
        private readonly Bank _bank;

        public AuditLog(Bank bank)
        {
            _bank = bank;
            _bank.LargeWithdrawAlert += (amount, customer) => Add(new Audit(customer.Id, amount));
        }

        public Bank Bank
        {
            get { return _bank; }
        }
    }
}