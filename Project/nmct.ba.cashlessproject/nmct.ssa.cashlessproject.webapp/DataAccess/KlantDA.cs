using Newtonsoft.Json;
using nmct.ba.cashlessproject.model.Klanten;
using nmct.ssa.cashlessproject.webapp.Helper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace nmct.ssa.cashlessproject.webapp.DataAccess
{
    public class KlantDA
    {
        private const string CONNECTIONSTRING = "ConnectionStringKlanten";

        public static void AddCustomer(Customers cus)
        {
            string sql = "INSERT INTO customers(CustomerName, Address, Picture, Balance, Sex, NationalNumber) VALUES(@CustomerName, @Address,@Picture, @Balance, @Sex, @NationalNumber)";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@CustomerName", cus.CustomerName);
            DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@Address", cus.Address);
            DbParameter par3 = Database.AddParameter(CONNECTIONSTRING, "@Picture", cus.Picture);
            DbParameter par4 = Database.AddParameter(CONNECTIONSTRING, "@Balance", cus.Balance);
            DbParameter par5 = Database.AddParameter(CONNECTIONSTRING, "@Sex", cus.Sex);
            DbParameter par6 = Database.AddParameter(CONNECTIONSTRING, "@NationalNumber", cus.CardNumber);

            Database.InsertData(CONNECTIONSTRING, sql, par1, par2, par3, par4, par5, par6);
        }


      
        public static Customers GetCustomer(string kaartNummer)
        {
            string sql = "SELECT ID, CustomerName, Address, Picture, Balance, Sex, NationalNumber From Customers WHERE NationalNumber=@NationalNumber ";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@NationalNumber", kaartNummer);
            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql, par1);

            Customers c = new Customers();
            if (reader.Read())
            {
                c.Id = Convert.ToInt32(reader["Id"]);
                c.CustomerName = reader["CustomerName"].ToString();
                c.Sex = reader["Sex"].ToString();
                c.Address = reader["Address"].ToString();
                c.CardNumber = reader["NationalNumber"].ToString(); ;
                if (!DBNull.Value.Equals(reader["Picture"]))
                    c.Picture = (byte[])reader["Picture"];
                else
                    c.Picture = new byte[0];
                c.Balance = Double.Parse(reader["Balance"].ToString());
            }
            return c;
        }


        public static int UpdateCustomer(Customers cus)
        {
            string sql = "UPDATE Customers SET Balance=@Balance WHERE ID =@ID";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@Balance", cus.Balance);
            DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@ID", cus.Id);

            return Database.ModifyData(CONNECTIONSTRING, sql, par1, par2);
        }

      
       

    }
}