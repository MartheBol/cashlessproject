using nmct.ba.cashlessproject.model.Klanten;
using nmct.ssa.cashlessproject.webapp.Helper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace nmct.ssa.cashlessproject.webapp.DataAccess
{
    public class ErrorlogKlantenDA
    {
        private const string CONNECTIONSTRING = "ConnectionStringKlanten";

        public static int AddError(Errorlog error)
        {
            int rowsaffected = 0;

            string sql = "INSERT INTO Errorlog VALUES(@RegisterID, @Timestamp, @Message, @StackTrace)";

            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@RegisterID", error.RegisterId);
            DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@Timestamp", error.Timestamp);
            DbParameter par3 = Database.AddParameter(CONNECTIONSTRING, "Message", error.Message);
            DbParameter par4 = Database.AddParameter(CONNECTIONSTRING, "@StackTrace", error.StackTrace);

            rowsaffected += Database.ModifyData(CONNECTIONSTRING, sql, par1, par2, par3, par4);

            return rowsaffected;
        }
    }
}