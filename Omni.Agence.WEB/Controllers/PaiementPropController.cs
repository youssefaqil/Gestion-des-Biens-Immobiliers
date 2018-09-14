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
    public class PaiementPropController : ControllerAuth
    {
        private AgenceEntities db = new AgenceEntities();
        private static int codeProp = 0;
        // GET: PaiementProp
        public ActionResult Index(int? idp)
        {
            ViewBag.CodePersProp = idp;
            if (idp != null)
            {
                var refCodeProp = db.Personnes.Where(p => p.CodePers == idp).FirstOrDefault().RefCodePers;
                ViewBag.RefCodeProp = refCodeProp;
                var operations = db.Operations.Include(o => o.Depense).Include(o => o.Personne).Include(o => o.TypeOp).Include(o => o.PaiementLoyer).
                    Where(d => d.Depense.Local.CodePers == idp || d.Depense.Immeuble.CodePers == idp  || 
                        d.PaiementLoyer.ContratLocation.Local.CodePers==idp).
                        Where(d=>d.Depense.DateFact == null && d.PaiementLoyer.DateFact == null);
                List<Operation> lOP = operations.ToList();
                foreach (Operation o in lOP)
                {
                    if(o.codeType==1){
                        o.montant = o.montant *
                    (100 - (o.PaiementLoyer.ContratLocation.TaxeDedilite +
                    o.PaiementLoyer.ContratLocation.Charge +
                    o.PaiementLoyer.ContratLocation.Local.ServiceLocal)) / 100;
                    }
                }
                double total = 0;
                double totalPL = 0;
                double totalD = 0;
                codeProp = idp.Value;
                for(int i=0; i< lOP.Count(); i++){
                    if (lOP[i].codeType == 1)
                    {
                        totalPL = totalPL + lOP[i].montant.Value;
                    }
                    else if (lOP[i].codeType == 3)
                    {
                        totalD = totalD + lOP[i].montant.Value;
                    }
                }
                
                total = totalPL - totalD;
                ViewBag.listProp = lOP;
                ViewBag.totalPL = totalPL;
                ViewBag.totalD = totalD;
                ViewBag.Total = total;
            }
            

            ViewBag.codeOp = new SelectList(db.Depenses, "codeOp", "Description");
            ViewBag.CodePers = new SelectList(db.Personnes, "CodePers", "nom");
            ViewBag.codeType = new SelectList(db.TypeOps, "codeType", "libelle");
            ViewBag.codeOp = new SelectList(db.PaiementLoyers, "codeOp", "MoyenPaiement");
            return View();
        }

        public ActionResult test()
        {
            
            return View();
        }
    [HttpPost]
        public ActionResult testAff(String[] Tab)
        {
            List<String> ListTab = new List<String>();
            foreach (String i in Tab)
            {
                ListTab.Add(i);
            }
            ViewBag.ListTab = ListTab;
            return View();
        }

        //[HttpPost]
        //public ActionResult Index(int? CodePers,int? x)
        //{
        //    var operations = db.Operations.Include(o => o.Depense).Include(o => o.Personne).Include(o => o.TypeOp).Include(o => o.PaiementLoyer).
        //            Where(d => d.Depense.Local.CodePers == codeProp || d.Depense.Immeuble.CodePers == codeProp ||
        //                d.PaiementLoyer.ContratLocation.Local.CodePers == codeProp &&
        //                d.Depense.DateFact == null && d.PaiementLoyer.DateFact == null);
        //    List<Operation> lOP = operations.ToList();
        //    ViewBag.listProp = lOP;

        //    return RedirectToAction("Edit", "Proprietaires", new {id=CodePers });
        //}
        [HttpPost]
        public ActionResult Print(int[] Tab)
        {
            List<Operation> ListOp=new List<Operation>();
            Operation op;

            
            Personne pers = db.Personnes.Find(codeProp);
            ViewBag.pers = pers;
            double? montant = 0;            
            foreach (int i in Tab)
            {
                op = db.Operations.Find(i);
                
                
                if (op.TypeOp.codeType == 1)
                {
                    op.PaiementLoyer.DateFact = DateTime.Now;
                    //montant = (montant *
                    //(100 - (op.PaiementLoyer.ContratLocation.TaxeDedilite +
                    //op.PaiementLoyer.ContratLocation.Charge +
                    //op.PaiementLoyer.ContratLocation.Local.ServiceLocal))/100);
                    //double? mt = (op2.montant *
                    //(100 - (op2.PaiementLoyer.ContratLocation.TaxeDedilite +
                    //op2.PaiementLoyer.ContratLocation.Charge +
                    //op2.PaiementLoyer.ContratLocation.Local.ServiceLocal)) / 100);

                    //pc=(100 - (op.PaiementLoyer.ContratLocation.TaxeDedilite +
                    //op.PaiementLoyer.ContratLocation.Charge +
                    //op.PaiementLoyer.ContratLocation.Local.ServiceLocal)) / 100;

                    ListOp.Add(op);
                    montant += op.montant.Value * (100 - (op.PaiementLoyer.ContratLocation.TaxeDedilite +
                    op.PaiementLoyer.ContratLocation.Charge +
                    op.PaiementLoyer.ContratLocation.Local.ServiceLocal)) / 100;
                }
                else if (op.TypeOp.codeType == 3) {
                    op.Depense.DateFact = DateTime.Now;
                   
                    montant-=op.montant.Value;
                    ListOp.Add(op);
                }
                
                db.Entry(op).State = EntityState.Modified;
                db.SaveChanges();
            }
            ViewBag.montant = montant;
            Operation operation = new Operation();
            operation.CodePers = codeProp;
            operation.codeType = 2;
            operation.montant = montant;
            operation.dateOp = DateTime.Now;
            
            db.Operations.Add(operation);
            db.SaveChanges();

            //var operations = db.Operations.Include(o => o.Depense).Include(o => o.Personne).Include(o => o.TypeOp).Include(o => o.PaiementLoyer).
            //       Where(d => d.Depense.Local.CodePers == CodePers || d.Depense.Immeuble.CodePers == CodePers ||
            //           d.PaiementLoyer.ContratLocation.Local.CodePers == CodePers &&
         
            //List<Operation> lOP = operations.ToList();
            //ViewBag.listProp = lOP;
            //int codep = CodePers.Value;
            return View(ListOp);
        }
        public ActionResult Acceuil()
        {
            return View();
        }
        public ActionResult CreatePP()
        {
            return View();
        }
        public ActionResult SelectProp(int? id)
        {

            codeProp = id.Value;
            //            ViewBag.codePersLocataire = codePersLocataire;
            return RedirectToAction("Index", new { idp = id });
        }
        // GET: PaiementProp/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Operation operation = db.Operations.Find(id);
            if (operation == null)
            {
                return HttpNotFound();
            }
            return View(operation);
        }
        
        public ActionResult Situation()
        {
           
            return RedirectToAction("Index", new { idp = codeProp});
        }
        // GET: PaiementProp/Create
        public ActionResult Create()
            //public ActionResult Create(int? idp, List<Operation> listOp)
        {
            //if (listOp != null)
            //{
            //    ViewBag.listProp = listOp;
            //}
            //ViewBag.CodePersProp = idp;
            
            ViewBag.codeOp = new SelectList(db.Depenses, "codeOp", "Description");
            ViewBag.CodePers = new SelectList(db.Personnes, "CodePers", "nom");
            ViewBag.codeType = new SelectList(db.TypeOps, "codeType", "libelle");
            ViewBag.codeOp = new SelectList(db.PaiementLoyers, "codeOp", "MoyenPaiement");
            return View();
        }

        // POST: PaiementProp/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codeOp,CodePers,codeType,montant,dateOp,Commentaire")] Operation operation)
        {
            if (ModelState.IsValid)
            {
                db.Operations.Add(operation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.codeOp = new SelectList(db.Depenses, "codeOp", "Description", operation.codeOp);
            ViewBag.CodePers = new SelectList(db.Personnes, "CodePers", "nom", operation.CodePers);
            ViewBag.codeType = new SelectList(db.TypeOps, "codeType", "libelle", operation.codeType);
            ViewBag.codeOp = new SelectList(db.PaiementLoyers, "codeOp", "MoyenPaiement", operation.codeOp);
            return View(operation);
        }

        // GET: PaiementProp/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Operation operation = db.Operations.Find(id);
            if (operation == null)
            {
                return HttpNotFound();
            }
            ViewBag.codeOp = new SelectList(db.Depenses, "codeOp", "Description", operation.codeOp);
            ViewBag.CodePers = new SelectList(db.Personnes, "CodePers", "nom", operation.CodePers);
            ViewBag.codeType = new SelectList(db.TypeOps, "codeType", "libelle", operation.codeType);
            ViewBag.codeOp = new SelectList(db.PaiementLoyers, "codeOp", "MoyenPaiement", operation.codeOp);
            return View(operation);
        }

        // POST: PaiementProp/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codeOp,CodePers,codeType,montant,dateOp,Commentaire")] Operation operation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(operation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.codeOp = new SelectList(db.Depenses, "codeOp", "Description", operation.codeOp);
            ViewBag.CodePers = new SelectList(db.Personnes, "CodePers", "nom", operation.CodePers);
            ViewBag.codeType = new SelectList(db.TypeOps, "codeType", "libelle", operation.codeType);
            ViewBag.codeOp = new SelectList(db.PaiementLoyers, "codeOp", "MoyenPaiement", operation.codeOp);
            return View(operation);
        }

        // GET: PaiementProp/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Operation operation = db.Operations.Find(id);
            if (operation == null)
            {
                return HttpNotFound();
            }
            return View(operation);
        }

        // POST: PaiementProp/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Operation operation = db.Operations.Find(id);
            db.Operations.Remove(operation);
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
