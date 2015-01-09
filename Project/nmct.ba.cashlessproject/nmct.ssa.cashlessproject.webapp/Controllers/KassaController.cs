using nmct.ba.cashlessproject.model.IT_Bedrijf;
using nmct.ssa.cashlessproject.webapp.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
            //Lijst voor alle registers aanmaken
            List<Registers> alleRegisters = new List<Registers>();
            alleRegisters = KassaDA.GetRegisters();

            //lijst voor kassas die aanwezig zijn en zijn toegekend aan een organisatie
            List<Registers> ToegekendeKassas = new List<Registers>();
            //lijst voor kassas die aanwezig zijn maar NIET TOEGEKEND aan een organisatie
            List<Registers> BeschikbareKassas = new List<Registers>();

            List<string> organisations = new List<string>();

            foreach(Registers reg in alleRegisters)
            {
                //Controleren of de register een organisatie heeft.
                bool exist = KassaDA.CheckIfRegisterGotOrganisation(reg.Id);

                //Zo nee
                if(exist == false)
                {
                    //kassa toevoegen aan de lijst beschikbare kassa's
                    BeschikbareKassas.Add(reg);
                }

                //zo ja 
                else if (exist == true)
                {
                    //id ophalen van de kassa en daarna toevoegen aan lijst kassa's met organisatie
                    int oId = KassaDA.GetOrIDFromRegisterID(reg.Id);
                    Organisations org = OrganisationsDA.GetById(oId);
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
            //Als alles is ingevuld
            if(ModelState.IsValid)
            {
               //controleren of de organisation name is ingevuld
               if(reg.OrganisationName == null)
               {
                   KassaDA.InsertRegister(reg);
               }

               else if(ModelState.Count == 5)
               {
                   //nieuwe register aanmaken op basis van de ingevulde waarden
                   var newRegister = new Registers
                   {
                       RegisterName = reg.RegisterName,
                       Device = reg.Device,
                       PurchaseDate = reg.PurchaseDate,
                       ExpiresDate = reg.ExpiresDate,
                       OrganisationName = reg.OrganisationName
                   };

                   //opslaan in database verenigingen
                   int id = KassaDA.InsertRegisterwWithOrganisation(newRegister);
                   //organisatie opslaan 
                   int registerId = KassaDA.GetIdFromRegister(newRegister.RegisterName);
                   int organisationId = KassaDA.GetById(newRegister.OrganisationName);
                   DateTime expiresDate = newRegister.ExpiresDate;
                   DateTime purchaseDate = newRegister.PurchaseDate;
                   KassaDA.SaveInRegisterOrganisations(registerId, organisationId, expiresDate, purchaseDate);
               }



                //als dit niet zo is
               
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
                ViewBag.Device = reg.Device;

                int orgId = KassaDA.GetOrIDFromRegisterID(id);

                if (orgId != 0)
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
        
        [HttpPost]
        public ActionResult Update(Registers r)
        {
            r.PurchaseDate = KassaDA.GetPurchaseDate(r.Id);
            r.ExpiresDate = KassaDA.GetExpiresDate(r.Id);

            bool exists = KassaDA.CheckIfRegisterGotOrganisation(r.Id);

            if (exists == false) //kassa nog niet toegekend aan vereniging
            {
                int newOrganisationID = OrganisationsDA.GetById(r.OrganisationName);
                KassaDA.SaveUpdate(r, newOrganisationID);
                KassaDA.SaveInRegisterOrganisations(r.Id, newOrganisationID, r.ExpiresDate, r.ExpiresDate);
                KassaDA.SaveRegisterName(r);

            }
            else if (exists == true)
            {
                int oldOrganisationID = KassaDA.GetOrIDFromRegisterID(r.Id); //oldOrganisationID
                int newOrganisationID = KassaDA.GetOrganisationByID(r.OrganisationName); //newOrganisationID
                KassaDA.UpdateRegister(r, newOrganisationID);
                KassaDA.SaveInRegisterOrganisations(r.Id, newOrganisationID);
            }

            return RedirectToAction("Index");
        }
    }
    
}

       