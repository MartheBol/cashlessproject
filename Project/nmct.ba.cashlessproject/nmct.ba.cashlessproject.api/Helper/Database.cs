using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace nmct.ba.cashlessproject.api.Helper
{
    public class Database
    {
        //1 = connectie
        //2 = commando
        //3 = uitvoeren

        public static ConnectionStringSettings CreateConnectionString(string provider, string server, string database, string username, string password)
        {
            ConnectionStringSettings settings = new ConnectionStringSettings();
            settings.ProviderName = provider;
            settings.ConnectionString = "Data Source=" + server + ";Initial Catalog=" + database + ";User ID=" + username + ";Password=" + password;
            return settings;
        }

        #region 1// Code voor opstellen van connectie

        //private : wordt nooit buiten dit hier (=deze klasse) gebruikt !
        private static DbConnection GetConnection(string conString)
        {
            //via ConfMang zoekt in Web.config : zoeken naar connectionstring met de naam "constring" , en haal deze op
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[conString];

            //maak een nieuwe variabele van type DbConnection > haal de factory op voor de provider die je uit de settings haalt (hier SQLConnection)
            DbConnection con = DbProviderFactories.GetFactory(settings.ProviderName).CreateConnection();

            //Niet vergeten: welke connectionstring!
            con.ConnectionString = settings.ConnectionString;

            //openen van connectie
            con.Open();

            //return van connectie
            return con;
        }


        //public : als we met transacties werken moet deze buiten de klasse kunnen afgesloten worden
        public static void ReleaseConnection(DbConnection dbCon)
        {
            //connectie sluiten
            if (dbCon != null)
            {
                dbCon.Close();
                dbCon = null;
            }
        }

        #endregion

        #region 2// Code voor opstellen van commando

        //code voor een commando op te stellen
        private static DbCommand BuildCommand(string constring, string sql, params DbParameter[] parameters)
        {
            DbCommand command = GetConnection(constring).CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = sql; // = SQL-statement

            if (parameters != null)
            {
                //
                command.Parameters.AddRange(parameters);
            }


            return command;
        }

        #endregion

        #region 3// GetData

        public static DbDataReader GetData(string constring, string sql, params DbParameter[] parameters)
        {
            DbCommand command = null;
            DbDataReader reader = null;

            try
            {
                command = BuildCommand(constring, sql, parameters);
                //maken van de reader
                reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection); //moment dat het klaar is met uitvoeren > sluit hij de connectie
                return reader;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (reader != null)
                {
                    reader.Close();
                }
                if (command != null)
                {
                    ReleaseConnection(command.Connection);
                }
                throw;
            }
        }

        #endregion

        #region 4// ModifyData

        public static int ModifyData(string constring, string sql, params DbParameter[] parameters)
        {
            DbCommand command = null;

            try
            {
                command = BuildCommand(constring, sql, parameters);
                //ExecuteNonQuery() = geeft terug hoeveel rijen er aangepast werden
                return command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (command != null)
                {
                    ReleaseConnection(command.Connection);
                }
                throw;
            }
        }

        #endregion

        #region 5// InsertData

        public static int InsertData(string constring, string sql, params DbParameter[] parameters)
        {
            DbCommand command = null;

            try
            {
                command = BuildCommand(constring, sql, parameters);
                //ExecuteNonQuery() = geeft terug hoeveel rijen er aangepast werden
                command.ExecuteNonQuery();

                command.Parameters.Clear();
                //Geeft laatst aangemaakte ID terug!
                command.CommandText = "SELECT @@IDENTITY";

                int id = Convert.ToInt32(command.ExecuteScalar());
                command.Connection.Close();

                return id;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (command != null)
                {
                    ReleaseConnection(command.Connection);
                }
                throw;
            }
        }

        #endregion

        #region 5.1// InsertDataWithoutID

        public static void InsertDataWithoutID(string constring, string sql, params DbParameter[] parameters)
        {
            DbCommand command = null;

            try
            {
                command = BuildCommand(constring, sql, parameters);
                //ExecuteNonQuery() = geeft terug hoeveel rijen er aangepast werden
                command.ExecuteNonQuery();

                command.Parameters.Clear();
                //Geeft laatst aangemaakte ID terug!
                //command.CommandText = "SELECT @@IDENTITY";

                //int id = Convert.ToInt32(command.ExecuteScalar());
                command.Connection.Close();

                //return id;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (command != null)
                {
                    ReleaseConnection(command.Connection);
                }
                throw;
            }
        }

        #endregion

        #region 6// AddParameter

        public static DbParameter AddParameter(string constring, string name, object value)
        {
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[constring];
            //parameter aanmaken voor dit type van provider
            DbParameter dbPar = DbProviderFactories.GetFactory(settings.ProviderName).CreateParameter();
            dbPar.ParameterName = name;
            dbPar.Value = value;

            return dbPar;
        }

        #endregion

        #region TRANSACTIE - CODE

        public static DbTransaction BeginTransaction(string constring)
        {
            DbConnection dbCon = null;
            try
            {
                dbCon = GetConnection(constring);
                return dbCon.BeginTransaction();
            }
            catch (Exception ex)
            {
                ReleaseConnection(dbCon);
                throw;
            }
        }

        private static DbCommand BuildCommand(DbTransaction trans, string sql, params DbParameter[] parameters)
        {
            DbCommand command = trans.Connection.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = sql; // = SQL-statement
            command.Transaction = trans;

            if (parameters != null)
            {
                //
                command.Parameters.AddRange(parameters);
            }


            return command;
        }

        public static DbDataReader GetData(DbTransaction trans, string sql, params DbParameter[] parameters)
        {
            DbCommand command = null;
            DbDataReader reader = null;

            try
            {
                command = BuildCommand(trans, sql, parameters);
                //maken van de reader
                reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection); //moment dat het klaar is met uitvoeren > sluit hij de connectie
                return reader;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (reader != null)
                {
                    reader.Close();
                }
                if (command != null)
                {
                    ReleaseConnection(command.Connection);
                }
                throw;
            }
        }

        public static int ModifyData(DbTransaction trans, string sql, params DbParameter[] parameters)
        {
            DbCommand command = null;

            try
            {
                command = BuildCommand(trans, sql, parameters);
                //ExecuteNonQuery() = geeft terug hoeveel rijen er aangepast werden
                return command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (command != null)
                {
                    ReleaseConnection(command.Connection);
                }
                throw;
            }
        }

        public static int InsertData(DbTransaction trans, string sql, params DbParameter[] parameters)
        {
            DbCommand command = null;

            try
            {
                command = BuildCommand(trans, sql, parameters);
                //ExecuteNonQuery() = geeft terug hoeveel rijen er aangepast werden
                command.ExecuteNonQuery();

                command.Parameters.Clear();
                //Geeft laatst aangemaakte ID terug!
                command.CommandText = "SELECT @@IDENTITY";

                int id = Convert.ToInt32(command.ExecuteScalar());
                command.Connection.Close();

                return id;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (command != null)
                {
                    ReleaseConnection(command.Connection);
                }
                throw;
            }
        }


        #endregion
    }
}