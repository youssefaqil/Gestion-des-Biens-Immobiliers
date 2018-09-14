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
    public class OperationsController : ControllerAuth
    {
        private AgenceEntities db = new AgenceEntities();

        // GET: Operations
        public ActionResult Index()
        {
            var operations = db.Operations.Include(o => o.Depense).Include(o => o.Personne).Include(o => o.TypeOp).Include(o => o.PaiementLoyer);
            return View(operations.ToList());
        }

        // GET: Operations/Details/5
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

        // GET: Operations/Create
        public ActionResult Create()
        {
            ViewBag.codeOp = new SelectList(db.Depenses, "codeOp", "Description");
            ViewBag.CodePers = new SelectList(db.Personnes, "CodePers", "nom");
            ViewBag.codeType = new SelectList(db.TypeOps, "codeType", "libelle");
            ViewBag.codeOp = new SelectList(db.PaiementLoyers, "codeOp", "MoyenPaiement");
            return View();
        }

        // POST: Operations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codeOp,CodePers,codeType,montant,dateOp,MoyenPaiement,Reference,DteDebut,DteFin")] Operation operation,
            [Bind(Include = "codeOp,MoyenPaiement,Reference,DteDebut,DteFin")] PaiementLoyer paiementLoyer)
        {
            if (ModelState.IsValid)
            {
                paiementLoyer.codeOp = operation.codeOp;

                db.Operations.Add(operation);
                db.PaiementLoyers.Add(paiementLoyer);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.codeOp = new SelectList(db.Depenses, "codeOp", "Description", operation.codeOp);
            ViewBag.CodePers = new SelectList(db.Personnes, "CodePers", "nom", operation.CodePers);
            ViewBag.codeType = new SelectList(db.TypeOps, "codeType", "libelle", operation.codeType);
            ViewBag.codeOp = new SelectList(db.PaiementLoyers, "codeOp", "MoyenPaiement", operation.codeOp);
            return View(operation);
        }

        // GET: Operations/Edit/5
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

        // POST: Operations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codeOp,CodePers,codeType,montant,dateOp")] Operation operation)
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

        // GET: Operations/Delete/5
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

        // POST: Operations/Delete/5
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
