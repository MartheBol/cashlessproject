using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.model
{
    public class Organisations
    {
        private int _id;
        private string _login;
        private string _password;
        private string _dbName;
        private string _dbLogin;
        private string _organistationName;
        private string _address;
        private string _email;
        private int _phone;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Login
        {
            get { return _login; }
            set { _login = value; }
        }
        public string Password
        {
            get { return _password; }
            set { _password= value; }
        }
        public string DbName
        {
            get { return _dbName; }
            set { _dbName = value; }
        }
        public string DbLogin
        {
            get { return _dbLogin; }
            set { _dbLogin = value; }
        }
        public string OrganistationName
        {
            get { return _organistationName; }
            set { _organistationName = value; }
        }
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        public int Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }
    }
}
