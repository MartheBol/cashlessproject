using nmct.ba.cashlessproject.api.Models;
using nmct.ba.cashlessproject.model.Klanten;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace nmct.ba.cashlessproject.api.Controllers
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