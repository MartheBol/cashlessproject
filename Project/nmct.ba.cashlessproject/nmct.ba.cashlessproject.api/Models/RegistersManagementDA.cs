using nmct.ba.cashlessproject.api.Helper;
using nmct.ba.cashlessproject.model.Klanten;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace nmct.ba.cashlessproject.api.Models
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