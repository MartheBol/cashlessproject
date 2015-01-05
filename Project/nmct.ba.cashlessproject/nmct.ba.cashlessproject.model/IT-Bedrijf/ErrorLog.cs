using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.model.IT_Bedrijf
{
    public class ErrorLog
    {
        private int _registerId;

        [Required]
        public int RegisterID
        {
            get { return _registerId; }
            set { _registerId = value; }
        }

        private DateTime _timestamp;

        [Required]
        [DataType(DataType.Date)]
        public DateTime Timestamp
        {
            get { return _timestamp; }
            set { _timestamp = value; }
        }


        private string _message;

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Het bericht moet tussen de 2 & 50 tekens bevatten")]
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        private string _stacktrace;

        [Required]
        public string Stacktrace
        {
            get { return _stacktrace; }
            set { _stacktrace = value; }
        }
    }
}