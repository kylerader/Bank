using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web_App_101.Models;

namespace BankAccountRepository
{
    public class AuditLogRepository : IAuditLogRepository
    {
        public void WriteEntries(AuditLog auditLogToSave)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Audit> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
