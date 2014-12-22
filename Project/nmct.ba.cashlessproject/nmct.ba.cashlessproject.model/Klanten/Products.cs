using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.model.Klanten
{
    public class Products
    {
        private string _id;
        private string _productName;
        private double _price;

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string ProductName
        {
            get { return _productName; }
            set { _productName= value; }
        }

        public double Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public string ProductNamePrice
        {
            get { return Id + "   |   " + ProductName + "   |   " + Price; }
        }



    }
}
