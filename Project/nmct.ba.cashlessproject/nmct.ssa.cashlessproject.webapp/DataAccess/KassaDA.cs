using nmct.ba.cashlessproject.model;
using nmct.ba.cashlessproject.model.IT_Bedrijf;
using nmct.ssa.cashlessproject.webapp.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
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
                    RegisterName = reader["RegisterName"].ToString(),
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
                return true;
            }
            catch (Exception)
            {
                reader.Close();
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
            string sql = "SELECT Registers.ID as registersID, Registers.RegisterName, Registers.Device, Registers.PurchaseDate, Registers.ExpiresDate, Organisations.OrganisationName as naamOrganisatie FROM Registers INNER JOIN Organisation_Register ON Organisation_Register.RegisterID = Registers.ID INNER JOIN Organisations ON Organisation_Register.OrganisationID = Organisations.ID WHERE Organisation_Register.OrganisationID=@ID";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@ID", id);

            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql, par1);

            while (reader.Read())
            {
                Registers reg = new Registers()
                {
                    Id = Int32.Parse(reader["registersID"].ToString()),
                    RegisterName = reader["RegisterName"].ToString(),
                    Device = reader["Device"].ToString(),
                    PurchaseDate = DateTime.Parse(reader["PurchaseDate"].ToString()),
                    ExpiresDate = DateTime.Parse(reader["ExpiresDate"].ToString()),
                    OrganisationName = organisationName
        
                };
                        lijst.Add(reg);
          
            }

            reader.Close();
            return lijst;
        }

        public static int InsertRegister(Registers reg)
        {
            string sql = "INSERT INTO Registers(RegisterName,Device,PurchaseDate,ExpiresDate) VALUES(@RegisterName,@Device,@PurchaseDate,@ExpiresDate)";
            

            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@RegisterName", reg.RegisterName);
            DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@Device", reg.Device);
            DbParameter par3 = Database.AddParameter(CONNECTIONSTRING, "@PurchaseDate", reg.PurchaseDate);
            DbParameter par4 = Database.AddParameter(CONNECTIONSTRING, "@ExpiresDate", reg.ExpiresDate);
            //DbParameter par5 = Database.AddParameter(CONNECTIONSTRING, "@OrganisationName", reg.OrganisationName);


            return Database.InsertData(CONNECTIONSTRING, sql, par1, par2, par3, par4);
        }

        public static int InsertRegisterwWithOrganisation(Registers reg)
        {
            string sql = "INSERT INTO Registers(RegisterName,Device,PurchaseDate,ExpiresDate,OrganisationName ) VALUES(@RegisterName,@Device,@PurchaseDate,@ExpiresDate,@OrganisationName)";


            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@RegisterName", reg.RegisterName);
            DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@Device", reg.Device);
            DbParameter par3 = Database.AddParameter(CONNECTIONSTRING, "@PurchaseDate", reg.PurchaseDate);
            DbParameter par4 = Database.AddParameter(CONNECTIONSTRING, "@ExpiresDate", reg.ExpiresDate);
            DbParameter par5 = Database.AddParameter(CONNECTIONSTRING, "@OrganisationName", reg.OrganisationName);


            return Database.InsertData(CONNECTIONSTRING, sql, par1, par2, par3, par4,par5);
        }


        public static Registers GetRegisterByID(int id)
        {
            string sql = "SELECT ID, RegisterName, Device, PurchaseDate, ExpiresDate FROM Registers WHERE ID=@ID";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@ID", id);

            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql, par1);

            reader.Read();

            Registers r = new Registers();
            r.Id = Int32.Parse(reader["ID"].ToString());
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

        public static void SaveUpdate(Registers r, int organisationID)
        {
            DateTime fromDate = DateTime.MaxValue;
            DateTime untilDate = DateTime.MaxValue;

            string sql = "INSERT INTO Organisation_Register VALUES(@OrganisationID, @RegisterID, @FromDate, @UntilDate)";

            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@OrganisationID", organisationID);
            DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@RegisterID", r.Id);
            DbParameter par3 = Database.AddParameter(CONNECTIONSTRING, "@FromDate", r.PurchaseDate);
            DbParameter par4 = Database.AddParameter(CONNECTIONSTRING, "@UntilDate", r.ExpiresDate);

            Database.InsertData(CONNECTIONSTRING, sql, par1, par2, par3, par4);
        }

        public static int GetById(string OrganisationName)
        {
            int id = 0;
            string sql = "SELECT * FROM Organisations WHERE OrganisationName = @OrganisationName";

            DbParameter parLogin = Database.AddParameter(CONNECTIONSTRING, "@OrganisationName", OrganisationName);

            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql, parLogin);

            while (reader.Read())
            {
                Organisations org = new Organisations()
                {
                    Id = Int32.Parse(reader["ID"].ToString()),
                    Login = Cryptography.Decrypt(reader["Login"].ToString()),
                    Password = Cryptography.Decrypt(reader["Password"].ToString()),
                    DbName = Cryptography.Decrypt(reader["DbName"].ToString()),
                    DbLogin = Cryptography.Decrypt(reader["DbLogin"].ToString()),
                    DbPassword = Cryptography.Decrypt(reader["DbPassword"].ToString()),
                    OrganisationName = reader["OrganisationName"].ToString(),
                    Address = reader["Address"].ToString(),
                    Email = reader["Email"].ToString(),
                    Phone = Int32.Parse(reader["Phone"].ToString())

                };

                id = org.Id;

            }

            return id;


        }

        public static int GetIdFromRegister(string RegisterName)
        {
            
            string sql = "SELECT ID, RegisterName, Device, PurchaseDate, ExpiresDate FROM Registers WHERE RegisterName=@RegisterName";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@RegisterName",RegisterName);

            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql, par1);

            reader.Read();

            Registers r = new Registers();
            r.Id = Int32.Parse(reader["ID"].ToString());
            r.RegisterName = reader["RegisterName"].ToString();
            r.Device = reader["Device"].ToString();
            r.PurchaseDate = DateTime.Parse(reader["PurchaseDate"].ToString());
            r.ExpiresDate = DateTime.Parse(reader["ExpiresDate"].ToString());

            reader.Close();
            return r.Id;
        }
        public static int GetOrganisationByID(string organisationName)
        {
            string sql = "SELECT ID, Login, Password, DbName, DbLogin, DbPassword, OrganisationName, Address, Email, Phone FROM Organisations WHERE OrganisationName=@OrganisationName";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@OrganisationName", organisationName);

            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql, par1);
            //DbDataReader reader = Database.GetData(Database.GetConnection(CONNECTIONSTRING), sql, par1);

            reader.Read();

            Organisations org = new Organisations();
                org.Id = Convert.ToInt32(reader["ID"]);
                org.Login = reader["Login"].ToString();
                org.Password = reader["Password"].ToString();
                org.DbName = reader["DbName"].ToString();
                org.DbLogin = reader["DbLogin"].ToString();
                org.DbPassword = reader["DbPassword"].ToString();
                org.OrganisationName = reader["OrganisationName"].ToString();
                org.Address = reader["Address"].ToString();
                org.Email = reader["Email"].ToString();
                org.Phone = long.Parse(reader["Phone"].ToString());
            

            

            reader.Close();
            return org.Id;
        }

       public static void SaveInRegisterOrganisations(int regID, int orgID, DateTime ExpiresDate, DateTime PurchaseDate )
        {
            string sql = "INSERT INTO Organisation_Register(RegisterID, OrganisationID, FromData,UntilData) VALUES (@RegisterID, @OrganisationID, @FromData,@UntilData)";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@RegisterID", regID );
            DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@OrganisationID", orgID);
            DbParameter par3 = Database.AddParameter(CONNECTIONSTRING, "@FromData", PurchaseDate);
            DbParameter par4 = Database.AddParameter(CONNECTIONSTRING, "@UntilData", ExpiresDate);

            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql, par1,par2,par3,par4);

        }

       public static void SaveRegisterName(Registers reg)
       {
           string sql = "UPDATE Registers SET  OrganisationName = @OrganisationName WHERE ID =@ID";
           DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@ID", reg.Id);
           DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@OrganisationName", reg.OrganisationName);

           Database.ModifyData(CONNECTIONSTRING, sql, par1, par2);

       }

       public static void SaveInRegisterOrganisations(int regID, int orgID)
       {
           string sql = "UPDATE Organisation_Register SET  OrganisationID = @OrganisationID WHERE RegisterID = @RegisterID";
           DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@RegisterID", regID);
           DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@OrganisationID", orgID);


           DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql, par1, par2);
       }

       
 
       
    }

}