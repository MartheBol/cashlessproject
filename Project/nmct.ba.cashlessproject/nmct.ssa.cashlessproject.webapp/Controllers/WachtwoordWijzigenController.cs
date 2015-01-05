using nmct.ba.cashlessproject.model.IT_Bedrijf;
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
    
    public class WachtwoordWijzigenController : ApiController
    {
    
        // GET: WachtwoordWijzigen
        public HttpResponseMessage Put(NewPassword np )
        {
            PaswoordWijzigenDA.WijzigeWachtwoord(np);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        public HttpResponseMessage Get(NewPassword np)
        {
            PaswoordWijzigenDA.WijzigeWachtwoord(np);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        
    }
}