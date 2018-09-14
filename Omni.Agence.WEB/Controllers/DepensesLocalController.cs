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
    public class DepensesLocalController : ControllerAuth
    {
        private AgenceEntities db = new AgenceEntities();
        //private static int codeLocal = 0;
        // GET: DepensesLocal
        public ActionResult Index(int? idl)
        {
            if (idl != null)
            {
                ViewBag.CodeLocal = idl.Value;
            }
            else
            {
                ViewBag.CodeLocal =0;
            }
            
            var locals = db.Locals.Include(l => l.Adresse).Include(l => l.Immeuble).Include(l => l.Personne).Include(l => l.Syndic).Include(l => l.TypeLocal);
            ViewBag.ListLoc = locals.ToList();
            
            return View();
        }
        [HttpPost]
        public ActionResult Index(int? nbrchambre, string lieuxlocal, int? minprix, int? maxprix, int? minsurface, int? maxsurface, string typelocal)
        {
            //var locals = db.Locals.Include(l => l.Adresse).Include(l => l.Immeuble).Include(l => l.Personne).Include(l => l.Syndic).Include(l => l.TypeLocal);
            ViewBag.pieces = nbrchambre;
            ViewBag.lieux = lieuxlocal;
            ViewBag.minprix = minprix;
            ViewBag.maxprix = maxprix;
            ViewBag.minsurface = minsurface;
            ViewBag.maxsurface = maxsurface;

            var query = from local in db.Locals
                        where ((local.PrixRefLocationLocal >= minprix) || !minprix.HasValue)
                        && ((local.PrixRefLocationLocal <= maxprix) || !maxprix.HasValue)
                            //&& (local.Adresse.descAdresse.Contains(lieuxlocal) || string.IsNullOrEmpty(lieuxlocal))
                            //&& (local.Adresse.quartier.Contains(lieuxlocal) || string.IsNullOrEmpty(lieuxlocal))
                        && (local.Adresse.Ville.Contains(lieuxlocal) || local.Adresse.quartier.Contains(lieuxlocal) || local.Adresse.descAdresse.Contains(lieuxlocal) || string.IsNullOrEmpty(lieuxlocal))
                        && (local.NbrPieceLocal == nbrchambre || !nbrchambre.HasValue)
                        && ((local.SuperficieLocal >= minsurface) || !minsurface.HasValue)
                        && ((local.SuperficieLocal <= maxsurface) || !maxsurface.HasValue)
                        select local;
            ViewBag.ListLoc = query.ToList();
            
            return View();
            
        }
        //public ActionResult SelectLocal(int? id)
        //{

        //    codeLocal = id.Value;
        //    //            ViewBag.codePersLocataire = codePersLocataire;
        //    return RedirectToAction("Index", new { idl = id });
        //}
        [HttpPost, ActionName("CreateDepense")]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDepense([Bind(Include = "codeOp,CodePers,codeType,montant,dateOp")] Operation operation,
            [Bind(Include = "codeOp,CodeImmeuble,CodeLocal,NumTypeDesignationDespense,Description,DateDepense")] Depense depense)
        
        {
            
            //if (ModelState.IsValid)
            //{
                Local local = db.Locals.Where(l => l.CodeLocal == depense.CodeLocal).First();

                operation.dateOp = DateTime.Now;
                operation.codeType = 3;
                operation.CodePers = local.CodePers;
                //operation.montant = montant;
                db.Operations.Add(operation);

                depense.codeOp = operation.codeOp;
               
                db.Depenses.Add(depense);
                db.SaveChanges();
                //return RedirectToAction("Index");
            //}

            ViewBag.CodeImmeuble = new SelectList(db.Immeubles, "CodeImmeuble", "NomImmeuble", depense.CodeImmeuble);
            ViewBag.CodeLocal = new SelectList(db.Locals, "CodeLocal", "TitreLocal", depense.CodeLocal);
            ViewBag.NumTypeDesignationDespense = new SelectList(db.TypeDesignationDepenses, "NumTypeDesignationDespense", "Description", depense.NumTypeDesignationDespense);
            ViewBag.codeOp = new SelectList(db.Operations, "codeOp", "codeOp", depense.codeOp);
            //return View(depense);
            return RedirectToAction("Index");
        }
        // GET: DepensesLocal/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Depense depense = db.Depenses.Find(id);
            if (depense == null)
            {
                return HttpNotFound();
            }
            return View(depense);
        }

        // GET: DepensesLocal/Create
        public ActionResult Create()
        {
            ViewBag.CodeImmeuble = new SelectList(db.Immeubles, "CodeImmeuble", "NomImmeuble");
            ViewBag.CodeLocal = new SelectList(db.Locals, "CodeLocal", "TitreLocal");
            ViewBag.NumTypeDesignationDespense = new SelectList(db.TypeDesignationDepenses, "NumTypeDesignationDespense", "Description");
            ViewBag.codeOp = new SelectList(db.Operations, "codeOp", "Commentaire");
            return View();
        }

        // POST: DepensesLocal/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codeOp,CodeImmeuble,CodeLocal,NumTypeDesignationDespense,Description,DateDepense")] Depense depense)
        {
            if (ModelState.IsValid)
            {
                db.Depenses.Add(depense);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CodeImmeuble = new SelectList(db.Immeubles, "CodeImmeuble", "NomImmeuble", depense.CodeImmeuble);
            ViewBag.CodeLocal = new SelectList(db.Locals, "CodeLocal", "TitreLocal", depense.CodeLocal);
            ViewBag.NumTypeDesignationDespense = new SelectList(db.TypeDesignationDepenses, "NumTypeDesignationDespense", "Description", depense.NumTypeDesignationDespense);
            ViewBag.codeOp = new SelectList(db.Operations, "codeOp", "Commentaire", depense.codeOp);
            return View(depense);
        }

        // GET: DepensesLocal/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Depense depense = db.Depenses.Find(id);
            if (depense == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodeImmeuble = new SelectList(db.Immeubles, "CodeImmeuble", "NomImmeuble", depense.CodeImmeuble);
            ViewBag.CodeLocal = new SelectList(db.Locals, "CodeLocal", "TitreLocal", depense.CodeLocal);
            ViewBag.NumTypeDesignationDespense = new SelectList(db.TypeDesignationDepenses, "NumTypeDesignationDespense", "Description", depense.NumTypeDesignationDespense);
            ViewBag.codeOp = new SelectList(db.Operations, "codeOp", "Commentaire", depense.codeOp);
            return View(depense);
        }

        // POST: DepensesLocal/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codeOp,CodeImmeuble,CodeLocal,NumTypeDesignationDespense,Description,DateDepense")] Depense depense)
        {
            if (ModelState.IsValid)
            {
                db.Entry(depense).State = EntityState.Modified;
                db.SaveChanges();
                //return RedirectToAction("Index");
            }
            ViewBag.CodeImmeuble = new SelectList(db.Immeubles, "CodeImmeuble", "NomImmeuble", depense.CodeImmeuble);
            ViewBag.CodeLocal = new SelectList(db.Locals, "CodeLocal", "TitreLocal", depense.CodeLocal);
            ViewBag.NumTypeDesignationDespense = new SelectList(db.TypeDesignationDepenses, "NumTypeDesignationDespense", "Description", depense.NumTypeDesignationDespense);
            ViewBag.codeOp = new SelectList(db.Operations, "codeOp", "Commentaire", depense.codeOp);
            //return View(depense);
            return RedirectToAction("Index");
        }

        // GET: DepensesLocal/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Depense depense = db.Depenses.Find(id);
            if (depense == null)
            {
                return HttpNotFound();
            }
            return View(depense);
        }

        // POST: DepensesLocal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Depense depense = db.Depenses.Find(id);
            db.Depenses.Remove(depense);
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
