using nmct.ba.cashlessproject.model;
using nmct.ba.cashlessproject.model.Klanten;
using nmct.ssa.cashlessproject.webapp.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Security.Claims;
using System.Web;


namespace nmct.ssa.cashlessproject.webapp.DataAccess
{
    public class CustomersDA
    {
        private const string CONNECTIONSTRING = "ConnectionStringKlanten";

        private static ConnectionStringSettings CreateConnectionString(IEnumerable<Claim> claims)
        {
            string dblogin = claims.FirstOrDefault(c => c.Type == "dblogin").Value;
            string dbpass = claims.FirstOrDefault(c => c.Type == "dbpass").Value;
            string dbname = claims.FirstOrDefault(c => c.Type == "dbname").Value;


            return Database.CreateConnectionString("System.Data.SqlClient", @"MARTHEBOLCF6F", Cryptography.Decrypt(dbname), Cryptography.Decrypt(dblogin), Cryptography.Decrypt(dbpass));

        }

        //CUSTOMERS TOEVOEGEN
        public static List<Customers> GetCustomers(IEnumerable<Claim> claims)
        {
            List<Customers> list = new List<Customers>();
            string sql = "SELECT ID,CustomerName, Address, Picture, Balance FROM Customer";
            DbDataReader reader = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql);

            while (reader.Read())
            {
                Customers c = new Customers();

                c.Id = Int32.Parse(reader["ID"].ToString());
                c.CustomerName = reader["CustomerName"].ToString();
                c.Address = reader["Address"].ToString();
                if (!DBNull.Value.Equals(reader["Picture"]))
                    c.Picture = (byte[])reader["Picture"];
                else
                    c.Picture = new byte[0];
                c.Balance = Double.Parse(reader["Balance"].ToString());
                //c.Sex = reader["Sex"].ToString();

                list.Add(c);
            }

            reader.Close();
            return list;

        }


        //CUSTOMER WIJZIGEN
        public static void UpdateCustomer(Customers c, IEnumerable<Claim> claims)
        {
            
                string sql = "UPDATE Customer SET CustomerName=@CustomerName,Address=@Address, Balance=@Balance, Picture=@Picture WHERE ID=@ID;";
                DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@CustomerName", c.CustomerName);
                DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@Address", c.Address);
                DbParameter par3 = Database.AddParameter(CONNECTIONSTRING, "@Picture", c.Picture);
                DbParameter par4 = Database.AddParameter(CONNECTIONSTRING, "@Balance", c.Balance);
                DbParameter par5 = Database.AddParameter(CONNECTIONSTRING, "@ID", c.Id);


                Database.ModifyData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2, par3, par4, par5);
            
        }


        public static Customers GetKlantenByID(int id)
        {
            Customers c = new Customers();
            string sql = "SELECT ID, CustomerName, Address, Balance, Picture, Sex, NationalNumber FROM Customers where ID = @ID";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@ID", id);
            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql, par1);
            while (reader.Read())
            {
                c = Create(reader);
            }
            reader.Close();
            return c;
        }

        private static Customers Create(IDataRecord record)
        {
            Customers c = new Customers();
            c.Id = Int32.Parse(record["ID"].ToString());
            c.CustomerName = record["CustomerName"].ToString();
            c.Balance = Convert.ToInt32(record["Balance"].ToString());
            c.Sex = record["Sex"].ToString();
            c.Address = record["Address"].ToString();
            if (!DBNull.Value.Equals(record["Picture"]))
                c.Picture = (byte[])record["Picture"];
            else
                c.Picture = new byte[0];
            c.CardNumber = record["NationalNumber"].ToString();
            return c;
        }



        public static void UpdateCustomerSale(Customers c)
        {
            string sql = "UPDATE Customers SET CustomerName=@CustomerName,Address=@Address, Balance=@Balance, Picture=@Picture WHERE ID=@ID;";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@CustomerName", c.CustomerName);
            DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@Address", c.Address);
            DbParameter par3 = Database.AddParameter(CONNECTIONSTRING, "@Picture", c.Picture);
            DbParameter par4 = Database.AddParameter(CONNECTIONSTRING, "@Balance", c.Balance);
            DbParameter par5 = Database.AddParameter(CONNECTIONSTRING, "@ID", c.Id);


            Database.ModifyData(CONNECTIONSTRING, sql, par1, par2, par3, par4, par5);
        }

    }
}