using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using nmct.ba.cashlessproject.model;
using nmct.ba.cashlessproject.api.Helper;
using System.Data.Common;
using System.Data;


namespace nmct.ba.cashlessproject.api.Models
{
    public class OrganisationsDA
    {
        public static List<Organisations> GetOrganistations()
        {
            List<Organisations> list = new List<Organisations>();

            string sql = "SELECT Id, Login, Password, DbName, DbLogin, OrganisationName, Address, Email, Phone FROM Organisations";
            DbDataReader reader = Database.GetData("ConnectionStringAdmin", sql);
            
            while(reader.Read())
            {
                list.Add(Create(reader));
            }

            reader.Close();
            return list;

        }

        
        private static Organisations Create(IDataRecord record)
        {
            return new Organisations()
            {
                Id = Int32.Parse(record["ID"].ToString()),
                Login = record["Login"].ToString(),
                Password = record["Password"].ToString(),
                DbName = record["DbName"].ToString(),
                DbLogin = record["DbLogin"].ToString(),
                OrganistationName = record["OrganisationName"].ToString(),
                Address = record["Address"].ToString(),
                Email = record["Email"].ToString(),
                Phone = Int32.Parse(record["Phone"].ToString())



               

            };
        }



       
    }
}