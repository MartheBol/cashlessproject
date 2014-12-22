using System;
using nmct.ba.cashlessproject.api.Helper;
using nmct.ba.cashlessproject.model.Klanten;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;
using System.Data;

namespace nmct.ba.cashlessproject.api.Models
{
    public class CustomersDA
    {
        public static List<Customers> GetCustomers()
        {
            List<Customers> list = new List<Customers>();

            string sql = "SELECT Id,CustomerName, Address, Picture, Balance FROM Customers";
            DbDataReader reader = Database.GetData("ConnectionStringKlanten", sql);

            while (reader.Read())
            {
                list.Add(Create(reader));
            }

            reader.Close();
            return list;

        }


        private static Customers Create(IDataRecord record)
        {
            return new Customers()
            {
                Id = int.Parse(record["ID"].ToString()),
                CustomerName = record["CustomerName"].ToString(),
                Address = record["Address"].ToString(),
                Balance = record["Balance"].ToString()

            };
        }
    }
}