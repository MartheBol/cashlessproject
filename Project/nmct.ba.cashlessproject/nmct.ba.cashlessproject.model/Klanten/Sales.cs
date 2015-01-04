using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.model.Klanten
{
    public class Sales
    {
        private int _id;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private Int32 _timestamp;

        public Int32 Timestamp
        {
            get { return _timestamp; }
            set { _timestamp = value; }
        }

        private int _customerId;

        public int CustomerID
        {
            get { return _customerId; }
            set { _customerId = value; }
        }

        private int _registerId;

        public int RegisterID
        {
            get { return _registerId; }
            set { _registerId = value; }
        }

        private int _productId;

        public int ProductID
        {
            get { return _productId; }
            set { _productId = value; }
        }

        private int _amount;

        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        private double _totalPrice;

        public double TotalPrice
        {
            get { return _totalPrice; }
            set { _totalPrice = value; }
        }

    }
}
