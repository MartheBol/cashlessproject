using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.model.Klanten
{
    class Registers
    {
        private string _id;
        private string _registerName;
        private string _device;

        public string Id
        {
            get { return _id; }
            set { _id = value;  }
        }

        public string RegisterName
        {
            get { return _registerName; }
            set { _registerName = value; }
        }

        public string Device
        {
            get { return _device; }
            set { _device = value; }
        }
    }
}
