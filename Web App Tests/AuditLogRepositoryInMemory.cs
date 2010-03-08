using System;
using System.Collections.Generic;
using System.Linq;
using Web_App_101.Models;

namespace Web_App_Tests
{
    public class AuditLogRepositoryInMemory : IAuditLogRepository
    {
        private readonly List<Audit> _auditLog = new List<Audit>();

        public void WriteEntries(AuditLog auditLogToSave)
        {
            foreach(var a in auditLogToSave) _auditLog.Add(a);
        }

        public IEnumerable<Audit> GetAll()
        {
            return _auditLog;
        }
    }
}