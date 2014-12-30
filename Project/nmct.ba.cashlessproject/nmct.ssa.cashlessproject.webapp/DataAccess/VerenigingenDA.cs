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
    public class VerenigingenDA
    {
        private const string CONNECTIONSTRING = "Admin";



        private static Organisations Create(IDataRecord record)
        {
            return new Organisations()
            {
                Id = Int32.Parse(record["ID"].ToString()),
                Login = record["Login"].ToString(),
                Password = record["Password"].ToString(),
                DbName = record["DbName"].ToString(),
                //DbPassword = record["DbPassword"].ToString(),
                OrganisationName = record["OrganisationName"].ToString(),
                Address = record["Address"].ToString(),
                Email = record["Email"].ToString(),
                Phone = long.Parse(record["Phone"].ToString()),
                DbLogin = record["DbLogin"].ToString()
            };
        }


        public static Organisations CheckCredentials(string username, string password)
        {
            string sql = "SELECT * FROM Organisations WHERE Login=@Login AND Password=@Password";
            DbParameter parUser = Database.AddParameter(CONNECTIONSTRING, "@Login", username);
            DbParameter parPass = Database.AddParameter(CONNECTIONSTRING, "@Password", password);

            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql, parUser, parPass);

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