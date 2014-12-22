using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using nmct.ba.cashlessproject.api.Models;
using nmct.ba.cashlessproject.model.Klanten;
using System.Web.Http;

namespace nmct.ba.cashlessproject.api.Controllers
{
    public class EmployeesController : ApiController
    {
        // GET: Employees
        /*public ActionResult Index()
        {
            return View();
        }*/

        public List<Employee> Get()
        {
            return EmployeesDA.GetEmployees();
        }

    }
}