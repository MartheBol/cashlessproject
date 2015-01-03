using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.model.Klanten
{
    public class Customers
    {
        private int _id;

        private string _customerName;
        private string _address;
        private string _sex;
        private double _balance;
        private byte[] _picture;

    
        #region PROPERTIES
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string CustomerName
        {
            get { return _customerName; }
            set { _customerName = value; }
        }
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        public byte[] Picture
        {
            get { return _picture; }
            set { _picture = value; }
        }
        
        public double Balance
        {
            get { return _balance; }
            set { _balance = value; }
        }

        
        public string Sex
        {
            get { return _sex; }
            set { _sex = value; }
        }

#endregion

        #region optionele properties
        private string _birthDate;

        public string BirthDate
        {
            get { return _birthDate; }
            set { _birthDate = value; }
        }

        private string _cardNumber;

        public string CardNumber
        {
            get { return _cardNumber; }
            set { _cardNumber = value; }
        }

        private string _country;

        public string Country
        {
            get { return _country; }
            set { _country = value; }
        }

        #endregion
        public string CustomerSummary
        {
            get { return string.Format("{0} | {1} ({2} {3})", Id, CustomerName, "huidig saldo: €", Balance); }
        }

    }
}