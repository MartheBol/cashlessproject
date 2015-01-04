using nmct.ba.cashlessproject.model.Klanten;
using nmct.ssa.cashlessproject.webapp.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
namespace nmct.ssa.cashlessproject.webapp.Controllers
{
    public class SalesController : ApiController
    {
        // GET: Sales
        public Sales Post(Sales sal)
        {

            ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            int id = SalesDA.AddSales(sal, p.Claims);
            sal.ID = id;
            return sal;
        }



    }
}