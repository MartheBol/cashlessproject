﻿using nmct.ba.cashlessproject.model;
using nmct.ba.cashlessproject.model.IT_Bedrijf;
using nmct.ssa.cashlessproject.webapp.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace nmct.ssa.cashlessproject.webapp.DataAccess
{
    [Authorize]
    public class OrganisationsDA
    {
        private const string CONNECTIONSTRING = "Admin";

        #region INDEX VIEW
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
                Login = Cryptography.Decrypt(record["Login"].ToString()),
                Password = Cryptography.Decrypt(record["Password"].ToString()),
                DbName =Cryptography.Decrypt(record["DbName"].ToString()),
                DbLogin = Cryptography.Decrypt(record["DbLogin"].ToString()),
                DbPassword = Cryptography.Decrypt(record["DbPassword"].ToString()),
                OrganisationName = record["OrganisationName"].ToString(),
                Address = record["Address"].ToString(),
                Email = record["Email"].ToString(),
                Phone = Int32.Parse(record["Phone"].ToString())

            };
        }

        #endregion

        public static int InsertOrganisations(Organisations organisation)
        {
            string sql = "INSERT INTO Organisations(Login, Password, DbName, DbLogin, DbPassword, OrganisationName, Address, Email, Phone) VALUES(@Login, @Password, @DbName, @DbLogin, @DbPassword, @OrganisationName, @Address, @Email, @Phone)";

            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@Login", Cryptography.Encrypt(organisation.Login));
            DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@Password", Cryptography.Encrypt(organisation.Password));
            DbParameter par3 = Database.AddParameter(CONNECTIONSTRING, "@DbName", Cryptography.Encrypt(organisation.DbName));
            DbParameter par4 = Database.AddParameter(CONNECTIONSTRING, "@DbLogin", Cryptography.Encrypt(organisation.DbLogin));
            DbParameter par5 = Database.AddParameter(CONNECTIONSTRING, "@DbPassword", Cryptography.Encrypt(organisation.DbPassword));
            DbParameter par6 = Database.AddParameter(CONNECTIONSTRING, "@OrganisationName", organisation.OrganisationName);
            DbParameter par7 = Database.AddParameter(CONNECTIONSTRING, "@Address", organisation.Address);
            DbParameter par8 = Database.AddParameter(CONNECTIONSTRING, "@Email", organisation.Email);
            DbParameter par9 = Database.AddParameter(CONNECTIONSTRING, "@Phone", organisation.Phone);
            int rowsaffectet =  Database.InsertData(CONNECTIONSTRING, sql, par1, par2, par3, par4, par5, par6, par7, par8, par9);
            
            CreateDatabase(organisation);

            return rowsaffectet;
              
        }

        private static void CreateDatabase(Organisations o)
        {
            // create the actual database
            string create = File.ReadAllText(HostingEnvironment.MapPath(@"~/App_Data/create.txt"));
            string sql = create.Replace("@@DbName", o.DbName).Replace("@@DbLogin", o.DbLogin).Replace("@@DbPassword", o.DbPassword);
            foreach (string commandText in RemoveGo(sql))
            {
                Database.ModifyData(CONNECTIONSTRING, commandText);
            }

            // create login, user and tables
            DbTransaction trans = null;
            try
            {
                trans = Database.BeginTransaction(CONNECTIONSTRING);

                string fill = File.ReadAllText(HostingEnvironment.MapPath(@"~/App_Data/fill.txt")); // only for the web
                string sql2 = fill.Replace("@@DbName", o.DbName).Replace("@@DbLogin", o.DbLogin).Replace("@@DbPassword", o.DbPassword);

                foreach (string commandText in RemoveGo(sql2))
                {
                    Database.ModifyData(trans, commandText);
                }

                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                Console.WriteLine(ex.Message);
            }
        }

        private static string[] RemoveGo(string input)
        {
            //split the script on "GO" commands
            string[] splitter = new string[] { "\r\nGO\r\n" };
            string[] commandTexts = input.Split(splitter, StringSplitOptions.RemoveEmptyEntries);
            return commandTexts;
        }

        public static void Update(Organisations org)
        {
            string sql = "UPDATE Organisations SET Login=@Login, Password=@Password, DbName=@DbName, DbLogin=@DbLogin, DbPassword=@DbPassword, OrganisationName=@OrganisationName, Address=@Address, Email=@Email, Phone=@Phone WHERE ID=@ID";

            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@Login", Cryptography.Encrypt(org.Login));
            DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@Password", Cryptography.Encrypt(org.Password));
            DbParameter par3 = Database.AddParameter(CONNECTIONSTRING, "@DbName", Cryptography.Encrypt(org.DbName));
            DbParameter par4 = Database.AddParameter(CONNECTIONSTRING, "@DbLogin", Cryptography.Encrypt(org.DbLogin));
            DbParameter par5 = Database.AddParameter(CONNECTIONSTRING, "@OrganisationName", org.OrganisationName);
            DbParameter par6 = Database.AddParameter(CONNECTIONSTRING, "@Address", org.Address);
            DbParameter par7 = Database.AddParameter(CONNECTIONSTRING, "@Email", org.Email);
            DbParameter par8 = Database.AddParameter(CONNECTIONSTRING, "@Phone", org.Phone);
            DbParameter par9 = Database.AddParameter(CONNECTIONSTRING,"@DbPassword", Cryptography.Encrypt(org.DbPassword));
            DbParameter par10 = Database.AddParameter(CONNECTIONSTRING,"@ID",org.Id);

            Database.ModifyData(CONNECTIONSTRING, sql, par1, par2, par3, par4, par5, par6, par7, par8, par9, par10);
            
        }

        #region Edit & Details
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
        #endregion


        public static int GetById(string OrganisationName)
        {
            int id = 0;
            string sql = "SELECT * FROM Organisations WHERE OrganisationName = @OrganisationName" ;

            DbParameter parLogin = Database.AddParameter(CONNECTIONSTRING, "@OrganisationName", OrganisationName);

            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql, parLogin);

            while (reader.Read())
            {
                Organisations org = new  Organisations()
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
    }
}