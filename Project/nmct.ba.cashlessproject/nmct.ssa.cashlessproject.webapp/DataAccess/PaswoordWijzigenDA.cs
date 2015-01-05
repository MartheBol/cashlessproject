using nmct.ba.cashlessproject.model;
using nmct.ba.cashlessproject.model.IT_Bedrijf;
using nmct.ssa.cashlessproject.webapp.Helper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace nmct.ssa.cashlessproject.webapp.DataAccess
{
    public class PaswoordWijzigenDA
    {
        private const string CONNECTIONSTRING = "Admin";

        public static int WijzigeWachtwoord(NewPassword np)
        {
            //wachtwoord & username zitten geëncrypteerd in database
            string Login = Cryptography.Encrypt(np.Username);
            string Password = Cryptography.Encrypt(np.NewPass);

            string sql = "UPDATE Organisations SET Password=@Password WHERE Login=@Login";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@Password", Password);
            DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@Login", Login);

            return Database.ModifyData(CONNECTIONSTRING, sql, par1, par2);




        }


    }
}