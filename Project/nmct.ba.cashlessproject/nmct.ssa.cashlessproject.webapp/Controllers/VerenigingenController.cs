using nmct.ba.cashlessproject.model.IT_Bedrijf;
using nmct.ssa.cashlessproject.webapp.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nmct.ssa.cashlessproject.webapp.Controllers
{
    [Authorize]
    public class VerenigingenController : Controller
    {
        // GET: Verenigingen
        [HttpGet]
        public ActionResult Index()
        {
            var ver = OrganisationsDA.GetOrganistations();
            ViewBag.Ver = ver;
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Organisations o)
        {
            //controleren of de lijn wel degelijk werd toegevoegd aan de database
            int validInsert = OrganisationsDA.InsertOrganisations(o);
            if (validInsert != 0)
                {
                    return RedirectToAction("Index");
                }
            
            return View();
        }


       [HttpGet]
       public ActionResult Edit(int? id)
        {
            //al de gebruiker er naar toe surf zonder id op te geven
            if (id.HasValue)
            {
                Organisations org = OrganisationsDA.GetById(id.Value);
                if (org == null) return new HttpNotFoundResult();
                return View(org);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

       [HttpPost]
       public ActionResult Edit(Organisations org)
       {
           if (org != null)
           {
               //organisatie ophalen
               Organisations control = OrganisationsDA.GetById(org.Id);
               if (control == null) return new HttpNotFoundResult();

               OrganisationsDA.Update(org);
               return RedirectToAction("Details", new { id = org.Id });
           }

           else
           {
               return RedirectToAction("Index");
           }
       }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                Organisations org = OrganisationsDA.GetById(id.Value);
                if (org == null) return new HttpNotFoundResult();
                return View(org);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }



    }
}