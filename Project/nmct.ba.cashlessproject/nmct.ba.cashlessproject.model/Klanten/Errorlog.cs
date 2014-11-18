using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.model.Klanten
{
    class Errorlog
    {
        private string _registerId;
        private DateTime _timestamp;
        private string _message;
        private string _stackTrace;

        public string RegisterId
        {
            get { return _registerId; }
            set { _registerId = value; }
        }

        public DateTime Timestamp
        {
            get { return _timestamp; }
            set { _timestamp = value; }
        }

        public string Message
        {
            get { return _message;}
            set { _message = value; }
        }

        public string StackTrace
        {
            get { return _stackTrace;}
            set { _stackTrace = value;}
        }
        
    }
}
