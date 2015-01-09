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

        public static Organisations CheckCredentials(string username, string password)
        {
            string sql = "SELECT * FROM Organisations WHERE Login=@Login AND Password=@Password";
            DbParameter parUser = Database.AddParameter(CONNECTIONSTRING, "@Login", Cryptography.Encrypt(username));
            DbParameter parPass = Database.AddParameter(CONNECTIONSTRING, "@Password", Cryptography.Encrypt(password));

            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql, parUser, parPass);

            Organisations res;

            if (!reader.Read())
                res = null;
            else
                res = Create(reader);

            reader.Close();
            return res;


        }

        public static Organisations GetOrganisationByID(int id)
        {
            string sql = "SELECT Login, Password, DbName, DbLogin, DbPassword, OrganisationName, Address, Email, Phone FROM Organisations WHERE ID=@ID";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@ID", id);

            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql, par1);
            //DbDataReader reader = Database.GetData(Database.GetConnection(CONNECTIONSTRING), sql, par1);

            reader.Read();

            Organisations organisation = CreateForOrganisationById(reader);

            reader.Close();

            return organisation;
        }

        private static Organisations CreateForOrganisationById(IDataRecord record)
        {
            return new Organisations()
            {
                Login = record["Login"].ToString(),
                Password = record["Password"].ToString(),
                DbName = record["DbName"].ToString(),
                DbLogin = record["DbLogin"].ToString(),
                DbPassword = record["DbPassword"].ToString(),
                OrganisationName = record["OrganisationName"].ToString(),
                Address = record["Address"].ToString(),
                Email = record["Email"].ToString(),
                Phone = long.Parse(record["Phone"].ToString()),
            };
        }

        public static List<Organisations> GetOrganisations()
        {
            List<Organisations> lijst = new List<Organisations>();

            string sql = "SELECT ID, Login, Password, DbName, DbLogin, DbPassword, OrganisationName, Address, Email, Phone FROM Organisations";

            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql);
            //DbDataReader reader = Database.GetData(Database.GetConnection(CONNECTIONSTRING), sql);

            while (reader.Read())
            {
                lijst.Add(Create(reader));
            }

            reader.Close();
            return lijst;
        }

        private static Organisations Create(IDataRecord record)
        {
            return new Organisations()
            {
                Id = Convert.ToInt32(record["ID"]),
                Login = record["Login"].ToString(),
                Password = record["Password"].ToString(),
                DbName = record["DbName"].ToString(),
                DbLogin = record["DbLogin"].ToString(),
                DbPassword = record["DbPassword"].ToString(),
                OrganisationName = record["OrganisationName"].ToString(),
                Address = record["Address"].ToString(),
                Email = record["Email"].ToString(),
                Phone = long.Parse(record["Phone"].ToString()),
            };
        }
    }
}