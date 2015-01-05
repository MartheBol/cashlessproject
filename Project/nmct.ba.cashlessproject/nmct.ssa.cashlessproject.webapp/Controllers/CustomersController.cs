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
    public class CustomersController : ApiController
    {
        // GET: Customer
        /*public ActionResult Index()
        {
            return View();
        }*/

        public List<Customers> Get()
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            return CustomersDA.GetCustomers(p.Claims);
        }

        public HttpStatusCode Put(Customers cus)
        {
            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            CustomersDA.UpdateCustomer(cus,p.Claims);
            return HttpStatusCode.OK;
        }

        public Customers Get(int id)
        {
            // ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            return CustomersDA.GetKlantenByID(id);
        }
    }
}