using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Omni.Agence.DAL;

namespace Omni.Agence.WEB.Controllers
{    
    public class PaiementLoyersController : ControllerAuth
    {
        private AgenceEntities db = new AgenceEntities();
        private static int codeLocataire=0;
        // GET: PaiementLoyers
        public ActionResult Index(int? idp,int? idc)
        {
            ViewBag.codePersLocataire = idp;
            ViewBag.CodeContrat = idc;
            ViewBag.CodeLocal = new SelectList(db.Locals, "CodeLocal", "TitreLocal");
            ViewBag.CodePers = new SelectList(db.Personnes, "CodePers", "nom");

            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codeOp,CodePers,codeType,montant,dateOp,Commentaire")] Operation operation,
            [Bind(Include = "codeOp,CodePers,CodeLocal,CodeContrat,MoyenPaiement,Reference,DteDebut,DteFin,NbrQuittanceImprime")] PaiementLoyer paiementLoyer, int? idc)
        {
            if (ModelState.IsValid)
            {

                operation.dateOp = DateTime.Now;
                operation.codeType = 1;
                operation.CodePers = codeLocataire;
                db.Operations.Add(operation);
                paiementLoyer.CodePers = codeLocataire;
                //paiementLoyer.CodeLocal = CodeLocal;

                paiementLoyer.codeOp = operation.codeOp;
                paiementLoyer.CodeContrat = idc;
                ContratLocation contrat = db.ContratLocations.Where(c => c.CodeContrat == idc).First();
                paiementLoyer.CodeLocal = contrat.CodeLocal;

                if (contrat.DateFinContrat <= paiementLoyer.DteFin)
                {

                    return RedirectToAction("Create", new { idp=paiementLoyer.CodePers,idc = contrat.CodeContrat });

                }
                else
                {

                    contrat.LoyerNet = operation.montant;
                    db.Entry(contrat).State = EntityState.Modified;

                    db.PaiementLoyers.Add(paiementLoyer);
                    db.SaveChanges();

                    return RedirectToAction("print", new { id = operation.codeOp });
                }

            }

            ViewBag.codeOp = new SelectList(db.Depenses, "codeOp", "Description", operation.codeOp);
            ViewBag.CodePers = new SelectList(db.Personnes, "CodePers", "nom", operation.CodePers);
            ViewBag.codeType = new SelectList(db.TypeOps, "codeType", "libelle", operation.codeType);
            ViewBag.codeOp = new SelectList(db.PaiementLoyers, "codeOp", "MoyenPaiement", operation.codeOp);
            return View(operation);
        }
        public ActionResult print(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Operation op = db.Operations.Find(id);

            ViewBag.NomPre = op.Personne.nom + " " + op.Personne.prenom;
            ViewBag.CodePre = op.Personne.CodePers;

            return View(op);
        }

        public ActionResult SelectLocataire(int? id)
        {

            codeLocataire = id.Value;
            //            ViewBag.codePersLocataire = codePersLocataire;
            return RedirectToAction("Create", new { idp = id });
        }
        public ActionResult SelectContrat(int? id)
        {
            //            ViewBag.codePersLocataire = codePersLocataire;
            return RedirectToAction("Create", new { idp = codeLocataire, idc = id });
        }
        // GET: PaiementLoyers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaiementLoyer paiementLoyer = db.PaiementLoyers.Find(id);
            if (paiementLoyer == null)
            {
                return HttpNotFound();
            }
            return View(paiementLoyer);
        }

        // GET: PaiementLoyers/Create
        public ActionResult Create(int? idp, int? idc)
        {
            ViewBag.codePersLocataire = idp;
            if (idp != null)
            {
                var refCodeLoc = db.Personnes.Where(p => p.CodePers == idp).FirstOrDefault().RefCodePers;
                ViewBag.RefCodeLoc = refCodeLoc;
            }
           
            ViewBag.CodeContrat = idc;
            ViewBag.CodeLocal = new SelectList(db.Locals, "CodeLocal", "TitreLocal");
            ViewBag.CodePers = new SelectList(db.Personnes, "CodePers", "nom");

            return View();
        }

        // POST: PaiementLoyers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "codeOp,CodePers,CodeLocal,CodeContrat,MoyenPaiement,Reference,DteDebut,DteFin,NbrQuittanceImprime")] PaiementLoyer paiementLoyer)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.PaiementLoyers.Add(paiementLoyer);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.CodePers = new SelectList(db.ContratLocations, "CodePers", "CodePers", paiementLoyer.CodePers);
        //    ViewBag.codeOp = new SelectList(db.Operations, "codeOp", "codeOp", paiementLoyer.codeOp);
        //    return View(paiementLoyer);
        //}

        // GET: PaiementLoyers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaiementLoyer paiementLoyer = db.PaiementLoyers.Find(id);
            if (paiementLoyer == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodePers = new SelectList(db.ContratLocations, "CodePers", "CodePers", paiementLoyer.CodePers);
            ViewBag.codeOp = new SelectList(db.Operations, "codeOp", "codeOp", paiementLoyer.codeOp);
            return View(paiementLoyer);
        }

        // POST: PaiementLoyers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codeOp,CodePers,CodeLocal,CodeContrat,MoyenPaiement,Reference,DteDebut,DteFin,NbrQuittanceImprime")] PaiementLoyer paiementLoyer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paiementLoyer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CodePers = new SelectList(db.ContratLocations, "CodePers", "CodePers", paiementLoyer.CodePers);
            ViewBag.codeOp = new SelectList(db.Operations, "codeOp", "codeOp", paiementLoyer.codeOp);
            return View(paiementLoyer);
        }

        // GET: PaiementLoyers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaiementLoyer paiementLoyer = db.PaiementLoyers.Find(id);
            if (paiementLoyer == null)
            {
                return HttpNotFound();
            }
            return View(paiementLoyer);
        }

        // POST: PaiementLoyers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PaiementLoyer paiementLoyer = db.PaiementLoyers.Find(id);
            db.PaiementLoyers.Remove(paiementLoyer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
