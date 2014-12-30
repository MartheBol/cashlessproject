using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.model.IT_Bedrijf
{
    public class Organisations
    {
          private int _id;
        private string _login;
        private string _password;
        private string _dbName;
        private string _dbLogin;
        private string _dbPassword;
        private string _organisationName;
        private string _address;
        private string _email;
        private long _phone;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        [DisplayName("Gebruikersnaam")]
        public string Login
        {
            get { return _login; }
            set { _login = value; }
        }


        [DisplayName("Wachtwoord")]
        public string Password
        {
            get { return _password; }
            set { _password= value; }
        }

        [DisplayName("Databasenaam")]
        public string DbName
        {
            get { return _dbName; }
            set { _dbName = value; }
        }

        [DisplayName("Database gebruikersnaam")]
        public string DbLogin
        {
            get { return _dbLogin; }
            set { _dbLogin = value; }
        }
        [DisplayName("Database wachtwoord")]
        public string DbPassword
        {
            get { return _dbPassword; }
            set { _dbPassword = value; }
        }

        [DisplayName("Naam organisatie")]
        public string OrganisationName
        {
            get { return _organisationName; }
            set { _organisationName = value; }
        }

        [DisplayName("Adres")]
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        [DisplayName("Email")]
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        [DisplayName("Telefoonnummer")]
        public long Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }
    }
}

    
