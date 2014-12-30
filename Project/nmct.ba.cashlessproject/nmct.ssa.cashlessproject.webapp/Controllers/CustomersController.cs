using nmct.ba.cashlessproject.model.Klanten;
using nmct.ssa.cashlessproject.webapp.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace nmct.ssa.cashlessproject.webapp.Controllers
{
    public class CustomersController : ApiController
    {
        // GET: Customer
        /*public ActionResult Index()
        {
            return View();
        }*/

        public List<Customers> Get()
        {
            return CustomersDA.GetCustomers();
        }

    }
}