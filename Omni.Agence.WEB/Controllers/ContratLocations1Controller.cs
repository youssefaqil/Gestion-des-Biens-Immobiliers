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
    public class ContratLocations1Controller : ControllerAuth
    {
        private AgenceEntities db = new AgenceEntities();

        // GET: ContratLocations1
        public ActionResult Index()
        {
            var contratLocations = db.ContratLocations.Include(c => c.Local).Include(c => c.Personne);
            return View(contratLocations.ToList());
        }

        // GET: ContratLocations1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ContratLocation contratLocation = db.ContratLocations.Find(id);




            if (contratLocation == null)
            {
                return HttpNotFound();
            }
            return View(contratLocation);
        }

        // GET: ContratLocations1/Create
        public ActionResult Create()
        {
            ViewBag.CodeLocal = new SelectList(db.Locals, "CodeLocal", "TitreLocal");
            ViewBag.CodePers = new SelectList(db.Personnes, "CodePers", "nom");
            return View();
        }

        // POST: ContratLocations1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CodePers,CodeLocal,CodeContrat,DateJuissance,Usage,LoyerDebase,Augementation,dateAugmentation,Charge,TaxeDedilite,Garage,LoyerNet,Caution,DateFinContrat,FrequencePaiement")] ContratLocation contratLocation)
        {
            if (ModelState.IsValid)
            {
                db.ContratLocations.Add(contratLocation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CodeLocal = new SelectList(db.Locals, "CodeLocal", "TitreLocal", contratLocation.CodeLocal);
            ViewBag.CodePers = new SelectList(db.Personnes, "CodePers", "nom", contratLocation.CodePers);
            return View(contratLocation);
        }

        // GET: ContratLocations1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContratLocation contratLocation = db.ContratLocations.Find(id);
            if (contratLocation == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodeLocal = new SelectList(db.Locals, "CodeLocal", "TitreLocal", contratLocation.CodeLocal);
            ViewBag.CodePers = new SelectList(db.Personnes, "CodePers", "nom", contratLocation.CodePers);
            return View(contratLocation);
        }

        // POST: ContratLocations1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CodePers,CodeLocal,CodeContrat,DateJuissance,Usage,LoyerDebase,Augementation,dateAugmentation,Charge,TaxeDedilite,Garage,LoyerNet,Caution,DateFinContrat,FrequencePaiement")] ContratLocation contratLocation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contratLocation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CodeLocal = new SelectList(db.Locals, "CodeLocal", "TitreLocal", contratLocation.CodeLocal);
            ViewBag.CodePers = new SelectList(db.Personnes, "CodePers", "nom", contratLocation.CodePers);
            return View(contratLocation);
        }

        // GET: ContratLocations1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContratLocation contratLocation = db.ContratLocations.Find(id);
            if (contratLocation == null)
            {
                return HttpNotFound();
            }
            return View(contratLocation);
        }

        // POST: ContratLocations1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ContratLocation contratLocation = db.ContratLocations.Find(id);
            db.ContratLocations.Remove(contratLocation);
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
