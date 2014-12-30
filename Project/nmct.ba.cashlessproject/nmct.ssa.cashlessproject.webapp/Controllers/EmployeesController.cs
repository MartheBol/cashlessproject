using nmct.ba.cashlessproject.model.Klanten;
using nmct.ssa.cashlessproject.webapp.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace nmct.ssa.cashlessproject.webapp.Controllers
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

        public HttpStatusCode Delete(int id)
        {
            EmployeesDA.DeleteEmployee(id);
            return HttpStatusCode.OK;
        }

        public Employee Post(Employee emp)
        {
            int id = EmployeesDA.InsertEmployee(emp);
            emp.Id = id;
            return emp;
        }

        public HttpStatusCode Put(long id, Employee emp)
        {
            EmployeesDA.UpdateEmployee(id, emp);
            return HttpStatusCode.OK;
        }
    }
}