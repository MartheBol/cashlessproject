using nmct.ba.cashlessproject.model;
using nmct.ba.cashlessproject.model.IT_Bedrijf;
using nmct.ssa.cashlessproject.webapp.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace nmct.ssa.cashlessproject.webapp.Controllers
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