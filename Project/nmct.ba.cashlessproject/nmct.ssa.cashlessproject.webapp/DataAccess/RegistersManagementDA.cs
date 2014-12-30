using nmct.ba.cashlessproject.model.Klanten;
using nmct.ssa.cashlessproject.webapp.Helper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace nmct.ssa.cashlessproject.webapp.DataAccess
{
    public class RegistersManagementDA
    {
        private const string CONNECTIONSTRING = "ConnectionStringKlanten";
        public static List<Registers> GetRegisters()
        {
            List<Registers> list = new List<Registers>();

            string sql = "SELECT ID, RegisterName, Device FROM Registers";
            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql);

            while (reader.Read())
            {
                Registers kassa = new Registers();
                kassa.Id = Convert.ToInt32(reader["ID"]);
                kassa.RegisterName = reader["RegisterName"].ToString();
                kassa.Device = reader["Device"].ToString();
                list.Add(kassa);
            }

            reader.Close();
            return list;
        }
    }
}