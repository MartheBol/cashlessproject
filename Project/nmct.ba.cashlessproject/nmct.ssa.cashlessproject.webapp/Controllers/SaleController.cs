using nmct.ba.cashlessproject.model.Klanten;
using nmct.ssa.cashlessproject.webapp.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace nmct.ssa.cashlessproject.webapp.Controllers
{
    public class SaleController : ApiController
    {
        // GET: Sale
        public HttpResponseMessage Put(Customers customer)
        {
            CustomersDA.UpdateCustomerSale(customer);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        public Sales Post(Sales sal)
        {

           
            int id = SalesDA.AddSalesBestelling(sal);
            sal.ID = id;
            return sal;
        }



    }
}