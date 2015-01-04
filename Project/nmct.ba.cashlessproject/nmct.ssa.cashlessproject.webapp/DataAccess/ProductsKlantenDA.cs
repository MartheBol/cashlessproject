using nmct.ba.cashlessproject.model.Klanten;
using nmct.ssa.cashlessproject.webapp.Helper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace nmct.ssa.cashlessproject.webapp.DataAccess
{
    public class ProductsKlantenDA
    {
        private const string CONNECTIONSTRING = "ConnectionStringKlanten";

      

        public static List<Products> GetProducts()
        {
            List<Products> list = new List<Products>();

            string sql = "SELECT ID, ProductName, Price FROM Products";
            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql);

            while (reader.Read())
            {
                Products product = new Products();
                product.Id = Convert.ToInt32(reader["ID"]);
                product.ProductName = reader["ProductName"].ToString();
                product.Price = Convert.ToDouble(reader["Price"]);
                list.Add(product);
            }

            reader.Close();
            return list;

        }
    }

}