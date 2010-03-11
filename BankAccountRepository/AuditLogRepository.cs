using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;
using Web_App_101.Models;

namespace BankAccountRepository
{
    public class AuditLogRepository : IAuditLogRepository
    {
        public void WriteEntries(AuditLog auditLogToSave)
        {
            if (auditLogToSave.Count == 0) return;
            var db = new SqlCeConnection("DataSource=\"..\\..\\..\\MyDatabase1.sdf\"");
            db.Open();
            var cmdString = "INSERT INTO AuditLog (_accountID,_amount,_timeStamp) VALUES (" + auditLogToSave.Last().Id + ", \'" +
                auditLogToSave.Last().Amount + "\', \'" + DateTime.Now + "\')";
            var cmd = new SqlCeCommand(cmdString, db);
            var resultSet = cmd.ExecuteResultSet(ResultSetOptions.Scrollable);
            db.Close();
        }

        public IEnumerable<Audit> GetAll()
        {
            var db = new SqlCeConnection("DataSource=\"..\\..\\..\\MyDatabase1.sdf\"");
            db.Open();
            const string cmdString = "SELECT * FROM AuditLog";
            var cmd = new SqlCeCommand(cmdString, db);
            var resultSet = cmd.ExecuteResultSet(ResultSetOptions.Scrollable);
            var auditLogs = new List<Audit>();
            for (var i = 0; resultSet.ReadAbsolute(i); i++)
            {
                var amount = resultSet.GetDecimal(resultSet.GetOrdinal("_amount"));
                var accountId = resultSet.GetInt32(resultSet.GetOrdinal("_accountId"));
                var audit = new Audit(accountId, amount);
                auditLogs.Add(audit);
            }

            db.Close();
            return auditLogs;
        }
    }
}
