using nmct.ba.cashlessproject.model.Klanten;
using nmct.ssa.cashlessproject.webapp.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
      
                if (cus != null)
                {
                    ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
                    CustomersDA.UpdateCustomer(cus, p.Claims);
                    return HttpStatusCode.OK;
                }
                else{
                        throw new HttpResponseException(AddRequest(HttpStatusCode.BadRequest, "parameter is leeg"));
                
                }

          
        }

        public Customers Get(int id)
        {
            // ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
            return CustomersDA.GetKlantenByID(id);
        }

        private HttpResponseMessage AddRequest(HttpStatusCode code, string message)
        {
            return Request.CreateErrorResponse(code, message);
        }
    }
}