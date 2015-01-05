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
    public class ErrorLogITBedrijfController : Controller
    {
        // GET: ErrorLogITBedrijf
        public ActionResult Index()
        {
            List<ErrorLog> list = new List<ErrorLog>();
            list = ErrorLogDA.GetErrors();
            ViewBag.Errors = list;
            return View();
        }
    }
}