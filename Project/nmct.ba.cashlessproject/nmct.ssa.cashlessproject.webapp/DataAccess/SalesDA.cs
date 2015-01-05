using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Web;
using nmct.ba.cashlessproject.model.Klanten;
using nmct.ssa.cashlessproject.webapp.Helper;
using System.Data.Common;
using nmct.ba.cashlessproject.model;

namespace nmct.ssa.cashlessproject.webapp.DataAccess
{
    public class SalesDA
    {
        private const string CONNECTIONSTRING = "ConnectionStringKlanten";

        private static ConnectionStringSettings CreateConnectionString(IEnumerable<Claim> claims)
        {
            string dblogin = claims.FirstOrDefault(c => c.Type == "dblogin").Value;
            string dbpass = claims.FirstOrDefault(c => c.Type == "dbpass").Value;
            string dbname = claims.FirstOrDefault(c => c.Type == "dbname").Value;

            return Database.CreateConnectionString("System.Data.SqlClient", @"MARTHEBOLCF6F", Cryptography.Decrypt(dbname), Cryptography.Decrypt(dblogin), Cryptography.Decrypt(dbpass));

        }

        public static int AddSales(Sales sale, IEnumerable<Claim> claims)
        {
            string sql = "INSERT INTO Sale VALUES(@Timestamp, @CustomerID, @RegisterID, @ProductID, @Amount, @TotalPrice)";
            DbParameter par1 = Database.AddParameter(CreateConnectionString(claims), "@Timestamp", sale.Timestamp);
            DbParameter par2 = Database.AddParameter(CreateConnectionString(claims), "@CustomerID", sale.CustomerID);
            DbParameter par3 = Database.AddParameter(CreateConnectionString(claims), "@RegisterID", sale.RegisterID);
            DbParameter par4 = Database.AddParameter(CreateConnectionString(claims), "@ProductID", sale.ProductID);
            DbParameter par5 = Database.AddParameter(CreateConnectionString(claims), "@Amount", sale.Amount);
            DbParameter par6 = Database.AddParameter(CreateConnectionString(claims), "@TotalPrice", sale.TotalPrice);

            return Database.ModifyData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2, par3, par4, par5, par6);
        }

        public static int AddSalesBestelling(Sales sale)
        {
            string sql = "INSERT INTO Sale VALUES(@Timestamp, @CustomerID, @RegisterID, @ProductID, @Amount, @TotalPrice)";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@Timestamp", sale.Timestamp);
            DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@CustomerID", sale.CustomerID);
            DbParameter par3 = Database.AddParameter(CONNECTIONSTRING, "@RegisterID", sale.RegisterID);
            DbParameter par4 = Database.AddParameter(CONNECTIONSTRING, "@ProductID", sale.ProductID);
            DbParameter par5 = Database.AddParameter(CONNECTIONSTRING, "@Amount", sale.Amount);
            DbParameter par6 = Database.AddParameter(CONNECTIONSTRING, "@TotalPrice", sale.TotalPrice);

            return Database.ModifyData(CONNECTIONSTRING, sql, par1, par2, par3, par4, par5, par6);
        }

      
    }
}