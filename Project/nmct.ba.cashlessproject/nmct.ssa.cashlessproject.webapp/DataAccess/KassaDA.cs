using nmct.ba.cashlessproject.model.IT_Bedrijf;
using nmct.ssa.cashlessproject.webapp.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nmct.ssa.cashlessproject.webapp.DataAccess
{
    [Authorize]
    public class KassaDA
    {
        private const string CONNECTIONSTRING = "Admin";

        public static List<Registers> GetRegisters()
        {
            List<Registers> list = new List<Registers>();
            string sql = "SELECT ID, RegisterName, Device, PurchaseDate, ExpiresDate FROM Registers";
            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql);
            while (reader.Read())
            {
                Registers reg = new Registers()
                {
                    Id = Convert.ToInt32(reader["ID"]),
                    Device = reader["Device"].ToString(),
                    PurchaseDate = Convert.ToDateTime(reader["PurchaseDate"].ToString()),
                    ExpiresDate = Convert.ToDateTime(reader["ExpiresDate"].ToString())
                };

                list.Add(reg);
            }

            reader.Close();
            return list;
        }

        public static bool CheckIfRegisterGotOrganisation(int registerID)
        {
            string sql = "SELECT OrganisationID FROM Organisation_Register WHERE RegisterID=@RegisterID";

            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@RegisterID", registerID);

            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql, par1);
            reader.Read();

            try
            {
                Organisations o = new Organisations();
                o.Id = Int32.Parse(reader["OrganisationID"].ToString());
                reader.Close();
                //gebruiker bestaat al in database
                return true;
            }
            catch (Exception)
            {
                reader.Close();
                //gebruiker bestaat NIET in database
                return false;
            }
        }

        public static int GetOrIDFromRegisterID(int regID)
        {
            int id = 0;

            string sql = "SELECT OrganisationID FROM Organisation_Register WHERE RegisterID=@RegisterID";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@RegisterID", regID);

            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql, par1);

            while (reader.Read())
            {
                Organisations or = new Organisations();
                or.Id = Int32.Parse(reader["OrganisationID"].ToString());
                id = or.Id;
            }

            reader.Close();
            return id;
        }

        public static List<Registers> GetAllRegistersForSelectedOrganisation(int id)
        {
            Organisations o = VerenigingenDA.GetOrganisationByID(id);
            string organisationName = o.OrganisationName;

            List<Registers> lijst = new List<Registers>();
            string sql = "SELECT Registers.ID as registersID, Registers.RegisterName, Registers.Device, Registers.PurchaseDate, Registers.ExpiresDate, Organisations.OrganisationName as naamOrganisatie FROM Registers INNER JOIN Organisation_Register ON Organisation_Register.RegisterID = Registers.ID INNER JOIN Organisations ON Organisation_Register.OrganisationID = Organisations.ID WHERE Registers.OrganisationName=@OrganisationName";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@OrganisationName", organisationName);

            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql, par1);

            while (reader.Read())
            {
                Registers reg = new Registers();
                reg.Id = Int32.Parse(reader["registersID"].ToString());
                reg.RegisterName = reader["RegisterName"].ToString();
                reg.Device = reader["Device"].ToString();
                reg.PurchaseDate = DateTime.Parse(reader["PurchaseDate"].ToString());
                reg.ExpiresDate =  DateTime.Parse(reader["ExpiresDate"].ToString());
                reg.OrganisationName = organisationName;
                lijst.Add(reg);
            }

            reader.Close();
            return lijst;
        }

        public static int InsertRegister(Registers reg)
        {
            string sql = "INSERT INTO Registers(RegisterName,Device,PurchaseDate,ExpiresDate,OrganisationName) VALUES(@RegisterName,@Device,@PurchaseDate,@ExpiresDate,@OrganisationName)";
            if (reg.OrganisationName == null)
                reg.OrganisationName = "";

            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@RegisterName", reg.RegisterName);
            DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@Device", reg.Device);
            DbParameter par3 = Database.AddParameter(CONNECTIONSTRING, "@PurchaseDate", reg.PurchaseDate);
            DbParameter par4 = Database.AddParameter(CONNECTIONSTRING, "@ExpiresDate", reg.ExpiresDate);
            DbParameter par5 = Database.AddParameter(CONNECTIONSTRING, "@OrganisationName", reg.OrganisationName);

            return Database.InsertData(CONNECTIONSTRING, sql, par1, par2, par3, par4, par5);
        }

        public static Registers GetRegisterByID(int id)
        {
            string sql = "SELECT RegisterName, Device, PurchaseDate, ExpiresDate FROM Registers WHERE ID=@ID";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@ID", id);

            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql, par1);

            reader.Read();

            Registers r = new Registers();
            r.RegisterName = reader["RegisterName"].ToString();
            r.Device = reader["Device"].ToString();
            r.PurchaseDate = DateTime.Parse(reader["PurchaseDate"].ToString());
            r.ExpiresDate = DateTime.Parse(reader["ExpiresDate"].ToString());

            reader.Close();
            return r;
        }

        public static int UpdateRegister(Registers reg, int organisationId)
        {
            

            string sql = "UPDATE Organisation_Register SET  OrganisationID = @OrganisationID WHERE RegisterID=@RegisterID";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@OrganisationID", organisationId);
            DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@RegisterID", reg.Id);

            return Database.ModifyData(CONNECTIONSTRING, sql, par1, par2);


        }

        public static DateTime GetPurchaseDate(int id)
        {
            DateTime purchaseDate = new DateTime();
            string sql = "SELECT PurchaseDate FROM Registers WHERE ID=@ID";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@ID", id);
            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql, par1);

            while (reader.Read())
            {
                purchaseDate = DateTime.Parse(reader["PurchaseDate"].ToString());
            }
            reader.Close();
            return purchaseDate;
        }

        public static DateTime GetExpiresDate(int id)
        {
            DateTime expiresDate = new DateTime();
            string sql = "SELECT ExpiresDate FROM Registers WHERE ID=@ID";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@ID", id);
            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql, par1);

            while (reader.Read())
            {
                expiresDate = DateTime.Parse(reader["ExpiresDate"].ToString());
            }
            reader.Close();
            return expiresDate;
        }

        /*public static string GetRegisterName(int id)
        {
            var registerName = "" ;
            string sql = "SELECT RegisterName FROM Registers WHERE ID=@ID";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING,"@ID",id);
            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql, par1);

            while(reader.Read())
            {
                registerName = reader["RegisterName"].ToString();
            }

            reader.Close();
            return registerName;

        }*/
    }

}