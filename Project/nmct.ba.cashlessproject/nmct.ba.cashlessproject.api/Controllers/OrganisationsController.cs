using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using nmct.ba.cashlessproject.api.Models;
using nmct.ba.cashlessproject.model;

namespace nmct.ba.cashlessproject.api.Controllers
{
    public class OrganisationsController : ApiController
    {
        // GET: Organisations
     
        public List<Organisations> Get()
        {
            return OrganisationsDA.GetOrganistations();
        }


        /*
        public int Post(Bankaccount ba)
        {
            return BankaccountDA.InsertAccount(ba);
        }*/

        
    }
}