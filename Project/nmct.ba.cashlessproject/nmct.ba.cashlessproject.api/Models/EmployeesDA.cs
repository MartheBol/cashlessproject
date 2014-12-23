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

        public static List<Employee> GetEmployees()
        {
            List<Employee> list = new List<Employee>();

            string sql = "SELECT ID, EmployeeName, Address, Email, Phone, RekeningNr FROM Employee";
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
                Phone = Int32.Parse(record["Phone"].ToString())

            };
        }

        public static void DeleteEmployee(int id)
        {
            string sql = "DELETE FROM Employee WHERE ID = @ID";
            DbParameter par = Database.AddParameter(CONNECTIONSTRING, "@ID", id);
            Database.ModifyData(CONNECTIONSTRING, sql, par);
        }


    }
}