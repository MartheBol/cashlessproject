using nmct.ba.cashlessproject.model;
using nmct.ba.cashlessproject.model.IT_Bedrijf;
using nmct.ssa.cashlessproject.webapp.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace nmct.ssa.cashlessproject.webapp.DataAccess
{
    public class OrganisationsDA
    {
        private const string CONNECTIONSTRING = "Admin";
        public static List<Organisations> GetOrganistations()
        {
            List<Organisations> list = new List<Organisations>();

            string sql = "SELECT Id, Login, Password, DbName, DbLogin, DbPassword, OrganisationName, Address, Email, Phone FROM Organisations";
            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql);

            while (reader.Read())
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
                DbPassword = record["DbPassword"].ToString(),
                OrganisationName = record["OrganisationName"].ToString(),
                Address = record["Address"].ToString(),
                Email = record["Email"].ToString(),
                Phone = Int32.Parse(record["Phone"].ToString())

            };
        }

        public static void Update(Organisations org)
        {
            string sql = "UPDATE Organisations SET Login=@Login, Password=@Password, DbName=@DbName, DbLogin=@DbLogin, DbPassword=@DbPassword, OrganisationName=@OrganisationName, Address=@Address, Email=@Email, Phone=@Phone WHERE ID=@ID";

            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@Login", org.Login);
            DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@Password", org.Password);
            DbParameter par3 = Database.AddParameter(CONNECTIONSTRING, "@DbName", org.DbName);
            DbParameter par4 = Database.AddParameter(CONNECTIONSTRING, "@DbLogin", org.DbLogin);
            DbParameter par5 = Database.AddParameter(CONNECTIONSTRING, "@OrganisationName", org.OrganisationName);
            DbParameter par6 = Database.AddParameter(CONNECTIONSTRING, "@Address", org.Address);
            DbParameter par7 = Database.AddParameter(CONNECTIONSTRING, "@Email", org.Email);
            DbParameter par8 = Database.AddParameter(CONNECTIONSTRING, "@Phone", org.Phone);
            DbParameter par9 = Database.AddParameter(CONNECTIONSTRING,"@DbPassword",org.DbPassword);
            DbParameter par10 = Database.AddParameter(CONNECTIONSTRING,"@ID",org.Id);

            Database.ModifyData(CONNECTIONSTRING, sql, par1, par2, par3, par4, par5, par6, par7, par8, par9, par10);
            
        }

        public static Organisations GetById(int id)
        {
            string sql = "SELECT * FROM Organisations WHERE ID=@ID";

            DbParameter parLogin = Database.AddParameter(CONNECTIONSTRING, "@ID", id);

            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql, parLogin);

            Organisations res;

            if (!reader.Read())
                res = null;
            else
                res = Create(reader);

            reader.Close();
            return res;

        }

       
    }
}