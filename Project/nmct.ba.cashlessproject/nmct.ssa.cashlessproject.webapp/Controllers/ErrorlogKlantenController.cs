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
    public class ErrorlogKlantenController : ApiController
    {
        // GET: ErrorlogKlanten
       public HttpResponseMessage Post(Errorlog error)
        {
            ErrorlogKlantenDA.AddError(error);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }
    }
}