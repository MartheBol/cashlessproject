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
    public class KlantController : ApiController
    {
        // GET: Klant
  
        public Customers Get(string kaartNummer)
        {
            return KlantDA.GetCustomer(kaartNummer);
        }
        public HttpResponseMessage Post(Customers cus)
        {
            KlantDA.AddCustomer(cus);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        public HttpResponseMessage Put(Customers cus)
        {
            KlantDA.UpdateCustomer(cus);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}