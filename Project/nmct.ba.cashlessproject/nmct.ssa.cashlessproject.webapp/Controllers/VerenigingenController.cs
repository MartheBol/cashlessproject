using nmct.ba.cashlessproject.model.IT_Bedrijf;
using nmct.ssa.cashlessproject.webapp.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nmct.ssa.cashlessproject.webapp.Controllers
{
    public class VerenigingenController : Controller
    {
        // GET: Verenigingen
        [HttpGet]
        public ActionResult Index()
        {
            var vers = OrganisationsDA.GetOrganistations();
            ViewBag.Vers = vers;
            return View();
        }

        [HttpGet]
       public ActionResult Edit(int id)
        {
            Organisations org = OrganisationsDA.GetById(id);
            if (org == null) return new HttpNotFoundResult();
            return View(org);
        }

        [HttpPost]
        public ActionResult Edit(Organisations org)
        {
            Organisations control = OrganisationsDA.GetById(org.Id);
            if(control == null) return new HttpNotFoundResult();

            OrganisationsDA.Update(org);
            return RedirectToAction("Details", new {id = org.Id});
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            Organisations org = OrganisationsDA.GetById(id);
            if (org == null) return new HttpNotFoundResult();
            return View(org);
        }
    }
}