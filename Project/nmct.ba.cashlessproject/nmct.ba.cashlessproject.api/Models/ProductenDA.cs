using nmct.ba.cashlessproject.api.Helper;
using nmct.ba.cashlessproject.model.Klanten;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using nmct.ba.cashlessproject.api.Models;


namespace nmct.ba.cashlessproject.api.Models
{
    public class ProductenDA
    {

        private const string CONNECTIONSTRING = "ConnectionStringKlanten";

        public static List<Products> GetProducts()
        {
            List<Products> list = new List<Products>();

            string sql = "SELECT ID, ProductName, Price FROM Products";
            DbDataReader reader = Database.GetData("ConnectionStringKlanten", sql);

            while (reader.Read())
            {
                list.Add(Create(reader));
            }

            reader.Close();
            return list;

        }


        private static Products Create(IDataRecord record)
        {
            return new Products()
            {
                Id = record["ID"].ToString(),
                ProductName= record["ProductName"].ToString(),
                Price = double.Parse(record["Price"].ToString())

            };
        }

       

    }
}