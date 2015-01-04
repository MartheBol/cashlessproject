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
    public class EmployeesDA
    {
        private const string CONNECTIONSTRING = "ConnectionStringKlanten";

        private static ConnectionStringSettings CreateConnectionString(IEnumerable<Claim> claims)
        {
            string dblogin = claims.FirstOrDefault(c => c.Type == "dblogin").Value;
            string dbpass = claims.FirstOrDefault(c => c.Type == "dbpass").Value;
            string dbname = claims.FirstOrDefault(c => c.Type == "dbname").Value;

            return Database.CreateConnectionString("System.Data.SqlClient", @"MARTHEBOLCF6F", Cryptography.Decrypt(dbname), Cryptography.Decrypt(dblogin), Cryptography.Decrypt(dbpass));

        }
        //GET EMPLOYEES
        public static List<Employee> GetEmployees(IEnumerable<Claim> claims)
        {
            List<Employee> list = new List<Employee>();

            string sql = "SELECT ID, EmployeeName, Address, Email, Phone FROM Employee";
            DbDataReader reader = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql);

            while (reader.Read())
            {
                list.Add(Create(reader));
            }

            reader.Close();
            return list;
        }


        private static Employee Create(IDataRecord record)
        {
            return new Employee()
            {
                Id = Int32.Parse(record["ID"].ToString()),
                EmployeeName = record["EmployeeName"].ToString(),
                Address = record["Address"].ToString(),
                Email = record["Email"].ToString(),
                Phone = Int64.Parse(record["Phone"].ToString())

            };
        }

        //DELETE EMPLOYEES
        public static void DeleteEmployee(int id, IEnumerable<Claim> claims)
        {
            string sql = "DELETE FROM Employee WHERE ID = @ID";
            DbParameter par = Database.AddParameter(CONNECTIONSTRING, "@ID", id);
            Database.ModifyData(Database.GetConnection(CreateConnectionString(claims)), sql, par);
        }

        //INSERT EMPLOYEES
        public static int InsertEmployee(Employee emp, IEnumerable<Claim> claims)
        {
            string sql = "INSERT INTO Employee(EmployeeName, Address, Email, Phone) VALUES(@EmployeeName, @Address, @Email, @Phone)";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@EmployeeName", emp.EmployeeName);
            DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@Address", emp.Address);
            DbParameter par3 = Database.AddParameter(CONNECTIONSTRING, "@Email", emp.Email);
            DbParameter par4 = Database.AddParameter(CONNECTIONSTRING, "@Phone", emp.Phone);


            return Database.InsertData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2, par3, par4);



        }

        //UPDATE EMPLOYEES
        public static void UpdateEmployee(long id, Employee emp, IEnumerable<Claim> claims)
        {
            string sql = "UPDATE employee SET EmployeeName=@EmployeeName,Address=@Address,Email=@Email,Phone=@Phone WHERE ID=@ID;";

            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@EmployeeName", emp.EmployeeName);
            DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@Address", emp.Address);
            DbParameter par3 = Database.AddParameter(CONNECTIONSTRING, "@Email", emp.Email);
            DbParameter par4 = Database.AddParameter(CONNECTIONSTRING, "@Phone", emp.Phone);

            DbParameter par5 = Database.AddParameter(CONNECTIONSTRING, "@ID", emp.Id);

            Database.ModifyData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2, par3, par4, par5);

        }




    }
}
