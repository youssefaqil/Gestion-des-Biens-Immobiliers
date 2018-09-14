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

    public class DepensesImmeubleController : ControllerAuth
    {
        private AgenceEntities db = new AgenceEntities();

        // GET: DepensesImmeuble
        public ActionResult Index()
        {
            var Immeubles = db.Immeubles.Include(i => i.Adresse).Include(l => l.Personne);
            ViewBag.ListImm = Immeubles.ToList();

            return View();
        }

        // GET: DepensesImmeuble/Details/5
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

        // GET: DepensesImmeuble/Create
        public ActionResult Create()
        {
            ViewBag.CodeImmeuble = new SelectList(db.Immeubles, "CodeImmeuble", "NomImmeuble");
            ViewBag.CodeLocal = new SelectList(db.Locals, "CodeLocal", "TitreLocal");
            ViewBag.NumTypeDesignationDespense = new SelectList(db.TypeDesignationDepenses, "NumTypeDesignationDespense", "Description");
            ViewBag.codeOp = new SelectList(db.Operations, "codeOp", "Commentaire");
            return View();
        }

        // POST: DepensesImmeuble/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codeOp,CodeImmeuble,CodeLocal,NumTypeDesignationDespense,Description,DateDepense,DateFact")] Depense depense)
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
        [HttpPost, ActionName("CreateDepense")]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDepense([Bind(Include = "codeOp,CodePers,codeType,montant,dateOp")] Operation operation,
            [Bind(Include = "codeOp,CodeImmeuble,CodeLocal,NumTypeDesignationDespense,Description,DateDepense")] Depense depense)
        {

            if (ModelState.IsValid)
            {
                Immeuble immeuble = db.Immeubles.Where(l => l.CodeImmeuble == depense.CodeImmeuble).First();

                operation.dateOp = DateTime.Now;
                operation.codeType = 3;
                operation.CodePers = immeuble.CodePers;
                //operation.montant = montant;
                db.Operations.Add(operation);

                depense.codeOp = operation.codeOp;

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
        // GET: DepensesImmeuble/Edit/5
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

        // POST: DepensesImmeuble/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codeOp,CodeImmeuble,CodeLocal,NumTypeDesignationDespense,Description,DateDepense,DateFact")] Depense depense)
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
            ViewBag.codeOp = new SelectList(db.Operations, "codeOp", "Commentaire", depense.codeOp);
            return View(depense);
        }

        // GET: DepensesImmeuble/Delete/5
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

        // POST: DepensesImmeuble/Delete/5
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
