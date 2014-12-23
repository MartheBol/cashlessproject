using nmct.ba.cashlessproject.api.Helper;
using nmct.ba.cashlessproject.model.Klanten;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace nmct.ba.cashlessproject.api.Models
{
    public class ProductenDA
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
                product.Id = reader["ID"].ToString();
                product.ProductName = reader["ProductName"].ToString();
                product.Price = Convert.ToDouble(reader["Price"]);
                list.Add(product);
            }

            reader.Close();
            return list;

        }

        //product toevoegen
        public static void UpdateProduct (long id, Products prod )
        {
            string sql = "UPDATE Products SET ProductName = @ProductName, Price = @Price WHERE ID = @ID";

            DbParameter parName = Database.AddParameter(CONNECTIONSTRING, "@ProductName", prod.ProductName);
            DbParameter parPrice = Database.AddParameter(CONNECTIONSTRING, "@ProductName", prod.Price);
            DbParameter parId = Database.AddParameter(CONNECTIONSTRING, "@ProductName", prod.Id);

            Database.ModifyData(CONNECTIONSTRING, sql, parName, parPrice, parId);
        
        }


        //verwijderen van een product
        public static void DeleteProduct(int id)
        {
            string sql = "DELETE FROM Products WHERE ID = @ID";
            DbParameter par = Database.AddParameter(CONNECTIONSTRING, "@ID", id);
            Database.ModifyData(CONNECTIONSTRING, sql, par);
        }

       


  
       

       

    }
}