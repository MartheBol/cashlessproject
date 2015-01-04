using nmct.ba.cashlessproject.model.IT_Bedrijf;
using nmct.ssa.cashlessproject.webapp.Helper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace nmct.ssa.cashlessproject.webapp.DataAccess
{
    public class ErrorLogDA
    {
        private const string CONNECTIONSTRING = "Admin";

        public static List<ErrorLog> GetErrors()
        {
            List<ErrorLog> list = new List<ErrorLog>();
            string sql = "SELECT RegisterID, TimeStamp, Message, Stacktrace FROM Errorlog";
            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql);

            while(reader.Read())
            {
                ErrorLog error = new ErrorLog()
                {
                    RegisterID = Int32.Parse(reader["RegisterID"].ToString()),
                    Timestamp = DateTime.Parse(reader["TimeStamp"].ToString()),
                    Message = reader["Message"].ToString(),
                    Stacktrace = reader["Stacktrace"].ToString()
                };
                list.Add(error);
            }
            reader.Close();
            return list;
        }
    }
}