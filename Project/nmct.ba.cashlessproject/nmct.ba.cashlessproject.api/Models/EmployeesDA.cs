using nmct.ba.cashlessproject.model.Klanten;
using nmct.ba.cashlessproject.api.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;
using System.Data;

namespace nmct.ba.cashlessproject.api.Models
{
    public class EmployeesDA
    {
        private const string CONNECTIONSTRING = "ConnectionStringKlanten";

        //GET EMPLOYEES
        public static List<Employee> GetEmployees()
        {
            List<Employee> list = new List<Employee>();

            string sql = "SELECT ID, EmployeeName, Address, Email, Phone FROM Employee";
            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql);

            while(reader.Read())
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
        public static void DeleteEmployee(int id)
        {
            string sql = "DELETE FROM Employee WHERE ID = @ID";
            DbParameter par = Database.AddParameter(CONNECTIONSTRING, "@ID", id);
            Database.ModifyData(CONNECTIONSTRING, sql, par);
        }

        //INSERT EMPLOYEES
        public static int InsertEmployee(Employee emp)
        {

                DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@EmployeeName", emp.EmployeeName);
                DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@Address", emp.Address);
                DbParameter par3 = Database.AddParameter(CONNECTIONSTRING, "@Email", emp.Email);
                DbParameter par4 = Database.AddParameter(CONNECTIONSTRING, "@Phone", emp.Phone);
                string sql = "INSERT INTO Employee(EmployeeName, Address, Email, Phone) VALUES(@EmployeeName, @Address, @Email, @Phone)";

                return Database.InsertData(CONNECTIONSTRING, sql, par1, par2, par3, par4);
        
 
          
        }

        //UPDATE EMPLOYEES
        public static void UpdateEmployee(long id, Employee emp)
        {
            string sql = "UPDATE employee SET EmployeeName=@EmployeeName,Address=@Address,Email=@Email,Phone=@Phone WHERE ID=@ID;";

            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@EmployeeName", emp.EmployeeName);
            DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@Address", emp.Address);
            DbParameter par3 = Database.AddParameter(CONNECTIONSTRING, "@Email", emp.Email);
            DbParameter par4 = Database.AddParameter(CONNECTIONSTRING, "@Phone", emp.Phone);

            DbParameter par5 = Database.AddParameter(CONNECTIONSTRING, "@ID", emp.Id);

            Database.ModifyData(CONNECTIONSTRING, sql, par1, par2, par3, par4, par5);
        
        }
        


    }
}