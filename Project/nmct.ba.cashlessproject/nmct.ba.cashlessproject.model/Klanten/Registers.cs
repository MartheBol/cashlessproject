using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.model.Klanten
{
    public class Registers
    {
        private int _id;
        private string _registerName;
        private string _device;
    
        private DateTime _from;
        private DateTime _until;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
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

       
        public DateTime From
        { 
            get { return _from; }
            set { value = _from; }
        }

        public DateTime Until
        {
            get { return _until; }
            set { value = _until; }
        }

        private int _employeeId;

        public int EmployeeId
        {
            get { return _employeeId; }
            set { _employeeId = value; }
        }


        private string _employeeName;

        public string EmployeeName
        {
            get { return _employeeName; }
            set { _employeeName = value; }
        }

        public string EmployeeFromTo
        {
            get { return string.Format("{0} \n ({1} - {2})", EmployeeName, From, Until); }
        }
    }
}
