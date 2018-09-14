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
    public class DepensesController : ControllerAuth
    {
        private AgenceEntities db = new AgenceEntities();

        // GET: Depenses
        public ActionResult Index()
        {
            var depenses = db.Depenses.Include(d => d.Immeuble).Include(d => d.Local).Include(d => d.TypeDesignationDepense).Include(d => d.Operation);
            return View(depenses.ToList());
        }

        // GET: Depenses/Details/5
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

        // GET: Depenses/Create
        public ActionResult Create()
        {
            ViewBag.CodeImmeuble = new SelectList(db.Immeubles, "CodeImmeuble", "NomImmeuble");
            ViewBag.CodeLocal = new SelectList(db.Locals, "CodeLocal", "TitreLocal");
            ViewBag.NumTypeDesignationDespense = new SelectList(db.TypeDesignationDepenses, "NumTypeDesignationDespense", "Description");
            ViewBag.codeOp = new SelectList(db.Operations, "codeOp", "codeOp");
            return View();
        }

        // POST: Depenses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codeOp,CodeImmeuble,CodeLocal,NumTypeDesignationDespense,Description")] Depense depense)
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
            ViewBag.codeOp = new SelectList(db.Operations, "codeOp", "codeOp", depense.codeOp);
            return View(depense);
        }

        // GET: Depenses/Edit/5
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
            ViewBag.codeOp = new SelectList(db.Operations, "codeOp", "codeOp", depense.codeOp);
            return View(depense);
        }

        // POST: Depenses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codeOp,CodeImmeuble,CodeLocal,NumTypeDesignationDespense,Description")] Depense depense)
        {
            if (ModelState.IsValid)
            {
                db.Entry(depense).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CodeImmeuble = new SelectList(db.Immeubles, "CodeImmeuble", "NomImmeuble", depense.CodeImmeuble);
            ViewBag.CodeLocal = new SelectList(db.Locals, "CodeLocal", "TitreLocal", depense.CodeLocal);
            ViewBag.NumTypeDesignationDespense = new SelectList(db.TypeDesignationDepenses, "NumTypeDesignationDespense", "Description", depense.NumTypeDesignationDespense);
            ViewBag.codeOp = new SelectList(db.Operations, "codeOp", "codeOp", depense.codeOp);
            return View(depense);
        }

        // GET: Depenses/Delete/5
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

        // POST: Depenses/Delete/5
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
