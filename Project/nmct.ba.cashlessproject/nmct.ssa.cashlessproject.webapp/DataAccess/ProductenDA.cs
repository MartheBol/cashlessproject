using nmct.ssa.cashlessproject.webapp.Helper;
using nmct.ba.cashlessproject.model.Klanten;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Configuration;
using nmct.ba.cashlessproject.model;

namespace nmct.ssa.cashlessproject.webapp.DataAccess
{
    public class ProductenDA
    {
      

            private const string CONNECTIONSTRING = "ConnectionStringKlanten";

            private static ConnectionStringSettings CreateConnectionString(IEnumerable<Claim> claims)
            {
                string dblogin = claims.FirstOrDefault(c => c.Type == "dblogin").Value;
                string dbpass = claims.FirstOrDefault(c => c.Type == "dbpass").Value;
                string dbname = claims.FirstOrDefault(c => c.Type == "dbname").Value;

                return Database.CreateConnectionString("System.Data.SqlClient", @"MARTHEBOLCF6F", Cryptography.Decrypt(dbname), Cryptography.Decrypt(dblogin), dbpass);

            }

            public static List<Products> GetProducts(IEnumerable<Claim> claims)
            {
                List<Products> list = new List<Products>();

                string sql = "SELECT ID, ProductName, Price FROM Product";
                DbDataReader reader = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql);

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
            public static void DeleteProduct(int id, IEnumerable<Claim> claims)
            {
                string sql = "DELETE FROM Product WHERE ID = @ID";
                DbParameter par = Database.AddParameter(CONNECTIONSTRING, "@ID", id);
                Database.ModifyData(Database.GetConnection(CreateConnectionString(claims)), sql, par);
            }

            //wijzigen van een product
            public static void UpdateProduct(long id, Products prod, IEnumerable<Claim> claims )
            {
                string sql = "UPDATE Product SET ProductName=@ProductName,Price=@Price WHERE ID=@ID;";

                if (prod.ProductName == null) prod.ProductName = "";

                DbParameter parName = Database.AddParameter(CONNECTIONSTRING, "@ProductName", prod.ProductName);
                DbParameter parPrice = Database.AddParameter(CONNECTIONSTRING, "@Price", prod.Price);
                DbParameter parId = Database.AddParameter(CONNECTIONSTRING, "@ID", id);

                Database.ModifyData(Database.GetConnection(CreateConnectionString(claims)), sql, parName, parPrice, parId);
            }


            //Toevoegen van een product 
            /*public static void AddProduct(Products product)
            {
            
                    string productName = product.ProductName;
                    double productPrice = product.Price;
                    DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@ProductName", productName);
                    DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@Price", productPrice);
                    string sql = "INSERT INTO Products VALUES (@ProductName, @Price)";

                    Database.InsertData(CONNECTIONSTRING, sql, par1, par2);
                }
           
            }*/


            public static int InsertProduct(Products Product, IEnumerable<Claim> claims)
            {
                string sql = "INSERT INTO Product(ProductName,Price) VALUES(@ProductName,@Price)";
                DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@ProductName", Product.ProductName);
                DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@Price", Product.Price);

                return Database.InsertData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2);

            }








        }
    }
