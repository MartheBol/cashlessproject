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
    public class RegistersManagementDA
    {
        private const string CONNECTIONSTRING = "ConnectionStringKlanten";
        private static ConnectionStringSettings CreateConnectionString(IEnumerable<Claim> claims)
        {
            string dblogin = claims.FirstOrDefault(c => c.Type == "dblogin").Value;
            string dbpass = claims.FirstOrDefault(c => c.Type == "dbpass").Value;
            string dbname = claims.FirstOrDefault(c => c.Type == "dbname").Value;


            return Database.CreateConnectionString("System.Data.SqlClient", @"MARTHEBOLCF6F", Cryptography.Decrypt(dbname), Cryptography.Decrypt(dblogin), Cryptography.Decrypt(dbpass));

        }

        public static List<Registers> GetRegisters(IEnumerable<Claim> claims)
        {
            List<Registers> list = new List<Registers>();

            string sql = "SELECT RegisterId, EmployeeID, EmployeeName, FromTime, UntilTime, RegisterName, Device FROM Register_Employee Inner join Employee	on Employee.Id = Register_Employee.EmployeeId INNER JOIN Register ON Register.Id = Register_Employee.RegisterId";
            DbDataReader reader = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql);

            while (reader.Read())
            {
                Registers kassa = new Registers();
                kassa.EmployeeName = reader["EmployeeName"].ToString();
                kassa.EmployeeId = Convert.ToInt32(reader["EmployeeID"]);
                kassa.Id = Convert.ToInt32(reader["RegisterId"]);
                kassa.RegisterName = reader["RegisterName"].ToString();
                kassa.Device = reader["Device"].ToString();
                kassa.From = Convert.ToDateTime(reader["FromTime"].ToString());
                kassa.From = Convert.ToDateTime(reader["UntilTime"].ToString());
               
                list.Add(kassa);
            }

            reader.Close();
            return list;
        }

       

    }
}