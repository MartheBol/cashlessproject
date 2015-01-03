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
    public class KassaController : Controller
    {
        // GET: Kassa
        [HttpGet]
        public ActionResult Index()
        {
            List<Registers> alleRegisters = new List<Registers>();
            alleRegisters = KassaDA.GetRegisters();

            //kassas die aanwezig zijn en zijn toegekend aan een organisatie
            List<Registers> ToegekendeKassas = new List<Registers>();
            //kassas die aanwezig zijn maar niet zijn toegekend aan een organisatie
            List<Registers> BeschikbareKassas = new List<Registers>();

            List<string> organisations = new List<string>();

            foreach(Registers reg in alleRegisters)
            {
                bool exist = KassaDA.CheckIfRegisterGotOrganisation(reg.Id);

                if(exist == false)
                {
                    BeschikbareKassas.Add(reg);
                }

                else if (exist == true)
                {
                    int oId = KassaDA.GetOrIDFromRegisterID(reg.Id);
                    Organisations org = VerenigingenDA.GetOrganisationByID(oId);
                    organisations.Add(org.OrganisationName);
                    reg.OrganisationName = org.OrganisationName;

                    ToegekendeKassas.Add(reg);
                }
            }

            ViewBag.Registers = alleRegisters;
            ViewBag.BeschikbareKassas = BeschikbareKassas;
            ViewBag.ToegekendeKassas = ToegekendeKassas;

            return View();
        }

        //Kassas per vereniging tonen
        [HttpGet]
        public ActionResult Search(int? id)
        {
            List<Registers> allRegistersPerOrganisation = new List<Registers>();

            if (id.HasValue)
            {
                allRegistersPerOrganisation = KassaDA.GetAllRegistersForSelectedOrganisation(id.Value);
                ViewBag.SelectedOrganisation = id.Value;

                if (allRegistersPerOrganisation.Count > 1)
                    ViewBag.Title = allRegistersPerOrganisation.Count + " kassa's gevonden van " + VerenigingenDA.GetOrganisationByID(id.Value).OrganisationName;
                else if (allRegistersPerOrganisation.Count == 1)
                    ViewBag.Title = allRegistersPerOrganisation.Count + " kassa gevonden van " + VerenigingenDA.GetOrganisationByID(id.Value).OrganisationName;
                else
                    ViewBag.Title = "Geen kassa's gevonden van de organisatie " + VerenigingenDA.GetOrganisationByID(id.Value).OrganisationName;
            }

            ViewBag.RegistersPerOrganisation = allRegistersPerOrganisation;
            ViewBag.Organisations = VerenigingenDA.GetOrganisations();

            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        //kassa toevoegen
        public ActionResult Create(Registers reg)
        {
            if(ModelState.IsValid)
            {
                KassaDA.InsertRegister(reg);
                return RedirectToAction("Index");
            }

            else
            {
                return View(reg);
            }
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            Registers reg = KassaDA.GetRegisterByID(id);
            ViewBag.Register = reg.RegisterName;

            //Vervaldatum & aankoopdatum van de kassa ophalen
            reg.PurchaseDate = KassaDA.GetPurchaseDate(id);
            reg.ExpiresDate = KassaDA.GetExpiresDate(id);
            //reg.RegisterName = KassaDA.GetRegisterName(id);


            ViewBag.PuchaseDate = reg.PurchaseDate;
            ViewBag.ExpiresDate = reg.ExpiresDate;
            ViewBag.RegisterName = reg.RegisterName;

            int orgId = KassaDA.GetOrIDFromRegisterID(id);

            if(orgId != 0)
            {
                Organisations organisation = VerenigingenDA.GetOrganisationByID(orgId);
                reg.OrganisationName = organisation.OrganisationName;
                ViewBag.Organisatie = reg.OrganisationName;
            }

            else
            {
                ViewBag.Organisatie = "Geen vereniging";
            }

            ViewBag.Organisaties = VerenigingenDA.GetOrganisations();

            return View();
        }

        
        }
}