using nmct.ba.cashlessproject.model.Klanten;
using nmct.ssa.cashlessproject.webapp.Helper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace nmct.ssa.cashlessproject.webapp.DataAccess
{
    public class CustomersDA
    {
        private const string CONNECTIONSTRING = "ConnectionStringKlanten";
        //CUSTOMERS TOEVOEGEN
        public static List<Customers> GetCustomers()
        {
            List<Customers> list = new List<Customers>();

            string sql = "SELECT ID,CustomerName, Address, Picture, Balance, Birthdate, Sex FROM Customers";
            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql);

            while (reader.Read())
            {
                Customers c = new Customers();

                c.Id = Int32.Parse(reader["ID"].ToString());
                c.CustomerName = reader["CustomerName"].ToString();
                c.Address = reader["Address"].ToString();
                c.Balance = Double.Parse(reader["Balance"].ToString());
                c.BirthDate = DateTime.Parse(reader["Birthdate"].ToString());
                c.Sex = reader["Sex"].ToString();

                list.Add(c);
            }

            reader.Close();
            return list;

        }


        //CUSTOMER WIJZIGEN
        public static void UpdateCustomer(long id, Customers customer)
        {
            string sql = "UPDATE Customers SET CustomerName=@CustomerName,Address=@Address, Balance=@Balance, Birthdate=@Birthdate, Sex=@Sex WHERE ID=@ID;";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@CustomerName", customer.CustomerName);


        }



    }
}