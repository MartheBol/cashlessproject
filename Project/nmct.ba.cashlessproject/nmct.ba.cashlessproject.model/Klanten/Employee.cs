﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.model.Klanten
{
    public class Employee
    {
        private int _id;
        private string _employeeName;
        private string _address;
        private string _email;
        private long _phone;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string EmployeeName
        {
            get { return _employeeName; }
            set { _employeeName = value; }
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
        public long Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }
    }

}
