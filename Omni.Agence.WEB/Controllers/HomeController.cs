using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Omni.Agence.DAL;
namespace Omni.Agence.WEB.Controllers
{    
    public class HomeController : ControllerAuth
    {
        private AgenceEntities db = new AgenceEntities();
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.nbrLocalCount = db.Locals.Count();
            ViewBag.nbrprpCount = db.Personnes.Where(p => p.CodeTypePersonne == 1).Count();
            ViewBag.nbrlocCount = db.Personnes.Where(p => p.CodeTypePersonne == 2).Count();
            ViewBag.nbrImmCount = db.Immeubles.Count();
            ViewBag.nbrLocCount = db.Locals.Count();
            ViewBag.nbrContratCount = db.ContratLocations.Count();

            DateTime datePDM = DateTime.Now.AddMonths(-1);
            ViewBag.PaiementDernierMois = db.PaiementLoyers.Where(c => c.Operation.dateOp > datePDM).Count();
            ViewBag.DepensesDernierMois = db.Depenses.Where(c => c.Operation.dateOp > datePDM).Count(); ;
            double? revenumoisp = db.PaiementLoyers.Where(c => c.Operation.dateOp > datePDM).Select(c=>c.Operation.montant).Sum();
            ViewBag.RevenuePaiementDernierMois = revenumoisp;
            double? revenumoisd = db.Depenses.Where(c => c.Operation.dateOp > datePDM).Select(c => c.Operation.montant).Sum();
            ViewBag.RevenueDepensesDernierMois = revenumoisd;
            ViewBag.TotalDernierMois = revenumoisp - revenumoisd;


            int year = DateTime.Now.Year;
            ViewBag.PaiementAnnee = db.PaiementLoyers.Where(c => c.Operation.dateOp.Value.Year == year).Count();
            ViewBag.DepensesAnnee = db.Depenses.Where(c => c.Operation.dateOp.Value.Year == year).Count(); 
            double? revenuanneep = db.PaiementLoyers.Where(c => c.Operation.dateOp.Value.Year == year).Select(c => c.Operation.montant).Sum();
            ViewBag.RevenuePaiementAnnee = revenuanneep;
            double? revenuanneed = db.Depenses.Where(c => c.Operation.dateOp.Value.Year == year).Select(c => c.Operation.montant).Sum();
            ViewBag.RevenueDepensesAnnee = revenuanneed;
            ViewBag.TotalAnnee = revenuanneep - revenuanneed;

            DateTime datePD12 = DateTime.Now.AddYears(-1);
            ViewBag.Paiement12 = db.PaiementLoyers.Where(c => c.Operation.dateOp > datePD12).Count();
            ViewBag.Depenses12 = db.Depenses.Where(c => c.Operation.dateOp > datePD12).Count(); ;
            double? revenu12p = db.PaiementLoyers.Where(c => c.Operation.dateOp > datePD12).Select(c => c.Operation.montant).Sum();
            ViewBag.RevenuePaiement12 = revenu12p;
            double? revenu12d = db.Depenses.Where(c => c.Operation.dateOp > datePD12).Select(c => c.Operation.montant).Sum();
            ViewBag.RevenueDepenses12 = revenu12d;
            ViewBag.Total12 = revenu12p - revenu12d;


            List<ContratLocation> ListContrat  = db.ContratLocations.ToList();

            var query = db.ContratLocations.Where(c => c.Local.DateDDispo != null && c.Local.DateFDispo != null);


            ViewBag.ListContrat = query.ToList();
            List<ContratLocation> ListOP = new List<ContratLocation>();
            for (int i = 0; i < ListContrat.Count(); i++)
            {
                if (ListContrat[i].PaiementLoyers.Max(c=>c.DateFact) < DateTime.Now.AddMonths(-1))
                {
                    ListOP.Add(ListContrat[i]);
                }
            }
            ViewBag.ListOP = ListOP;
            return View();
        }

        public ActionResult IndexTime() {
            return View();
        }
        public ActionResult Blank()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


        public ActionResult Error()
        {
            ViewBag.Message = "Error Page";
            return View("~/Views/Shared/Error.cshtml");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}