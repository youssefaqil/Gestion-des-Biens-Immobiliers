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
    public class ContratLocationsController : ControllerAuth
    {
        private AgenceEntities db = new AgenceEntities();        
        private static int codeLocal;
        Personne personne=new Personne();
        // GET: ContratLocations
        public ActionResult Index(int? id)
        {
            if(id!=null){
                //personne = db.Personnes.Find(id);
                //codeLocal = personne.CodePers;
                codeLocal = int.Parse(id.ToString());
                ViewBag.CodeLocal = codeLocal;
            }
            return View();
        }

        [HttpPost]
        
        public ActionResult IndexLocataire(int? id)
        {
           
                    //Personne personne = db.Personnes.Find(id);
                    
           
            return RedirectToAction("Index");
        }
        //[HttpPost, ActionName("Index")]
        //[ValidateAntiForgeryToken]
        //public ActionResult IndexCL(int? id)
        //{

            
        //    return RedirectToAction("Index");
        //}


        // GET: ContratLocations/Details/5
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

        //[HttpPost, ActionName("ListLocataire")]
        //[ValidateAntiForgeryToken]
        //public ActionResult ListLocataire(int id)
        //{
        //    ViewBag.CodeLocataire = id;
        //    return View();
        //}
      








        // GET: ContratLocations/Create
        public ActionResult Create()
        {
            ViewBag.CodeLocal = new SelectList(db.Locals, "CodeLocal", "TitreLocal");
            ViewBag.CodePers = new SelectList(db.Personnes, "CodePers", "nom");
            return View();
        }

        // POST: ContratLocations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "CodePers,CodeLocal,CodeContrat,DateJuissance,Usage,LoyerDebase,Augementation,dateAugmentation,Charge,TaxeDedilite,Garage,LoyerNet,Caution,DateFinContrat,FrequencePaiement")] ContratLocation contratLocation)
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

        // GET: ContratLocations/Edit/5
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

        // POST: ContratLocations/Edit/5
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

        // GET: ContratLocations/Delete/5
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

        // POST: ContratLocations/Delete/5
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
