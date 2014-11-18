using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.model.Klanten
{
    class Customers
    {
        private string _id;
        private string _customerName;
        private string _address;
        private BitmapImage _picture;
        private string _balance;
        

        public string Id
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
        public BitmapImage Picture 
        {
            get{return _picture;}
            set{_picture = value;}
        }
        public string Balance
        {
            get{return _balance;}
            set{_balance = value;}
        }
        
    }
}
