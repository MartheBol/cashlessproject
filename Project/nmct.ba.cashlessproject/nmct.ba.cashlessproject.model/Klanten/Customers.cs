using System;
using System.Collections.Generic;
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
        private byte[] _picture;
        private DateTime _birthDate;
        private double _balance;
       


       
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
            set { value = _picture; }
        }
        public double Balance
        {
            get{return _balance;}
            set{_balance = value;}
        }

        public DateTime BirthDate
        {
            get { return _birthDate; }
            set { value = _birthDate; }
        }

        public string Sex
        {
            get { return _sex; }
            set { _sex = value; }
        }

        public string CustomerSummary
        {
            get { return string.Format("{0} | {1} ({2} {3})", Id, CustomerName, "huidig saldo: €", Balance); }
        }
        
        
    }
}
