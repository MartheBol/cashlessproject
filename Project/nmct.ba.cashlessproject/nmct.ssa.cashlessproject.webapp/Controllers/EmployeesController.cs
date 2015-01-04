using nmct.ba.cashlessproject.model.Klanten;
using nmct.ssa.cashlessproject.webapp.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
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
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;

            return EmployeesDA.GetEmployees(p.Claims);
        }

     

        public Employee Post(Employee emp)
        {

            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            int id = EmployeesDA.InsertEmployee(emp, p.Claims);
            emp.Id = id;
            return emp;
        }

        public HttpStatusCode Delete(int id)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            EmployeesDA.DeleteEmployee(id, p.Claims);
            return HttpStatusCode.OK;
        }

    

        public HttpStatusCode Put(long id, Employee emp)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;

            EmployeesDA.UpdateEmployee(id, emp, p.Claims);
            return HttpStatusCode.OK;
        }
    }
}