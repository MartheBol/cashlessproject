using nmct.ba.cashlessproject.api.Helper;
using nmct.ba.cashlessproject.model.Klanten;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.Claims;
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
                product.Id = Convert.ToInt32(reader["ID"]);
                product.ProductName = reader["ProductName"].ToString();
                product.Price = Convert.ToDouble(reader["Price"]);
                list.Add(product);
            }

            reader.Close();
            return list;

        }

 
        //verwijderen van een product
        public static void DeleteProduct(int id)
        {
            string sql = "DELETE FROM Products WHERE ID = @ID";
            DbParameter par = Database.AddParameter(CONNECTIONSTRING, "@ID", id);
            Database.ModifyData(CONNECTIONSTRING, sql, par);
        }

        //wijzigen van een product
        public static void UpdateProduct(long id, Products prod)
        {
            string sql = "UPDATE Products SET ProductName=@ProductName,Price=@Price WHERE ID=@ID;";

            if (prod.ProductName == null) prod.ProductName = "";

            DbParameter parName = Database.AddParameter(CONNECTIONSTRING,"@ProductName", prod.ProductName);
            DbParameter parPrice = Database.AddParameter(CONNECTIONSTRING, "@Price", prod.Price);
            DbParameter parId = Database.AddParameter(CONNECTIONSTRING, "@ID", id);

            Database.ModifyData(CONNECTIONSTRING, sql, parName, parPrice, parId);
        }


        //Toevoegen van een product 
        public static void AddProduct(Products product)
        {
            if(product != null)
            {
                string productName = product.ProductName;
                double productPrice = product.Price;
                DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@ProductName", productName);
                DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@Price", productPrice);
                string sql = "INSERT INTO Products VALUES (@ProductName, @Price)";

                Database.InsertData(CONNECTIONSTRING, sql, par1, par2);
            }
           
        }


        public static int InsertProduct(Products Product)
        {
            string sql = "INSERT INTO Products(ProductName,Price) VALUES(@ProductName,@Price)";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@ProductName", Product.ProductName);
            DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@Price", Product.Price);

            return Database.InsertData(CONNECTIONSTRING, sql, par1, par2);
        }



  
       

       

    }
}