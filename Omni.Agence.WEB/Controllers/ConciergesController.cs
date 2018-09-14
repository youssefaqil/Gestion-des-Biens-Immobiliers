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
    public class ConciergesController : ControllerAuth
    {
        private AgenceEntities db = new AgenceEntities();

        // GET: Concierges
        public ActionResult Index()
        {
            var concierges = db.Concierges;
            return View(concierges.ToList());
        }

        // GET: Concierges/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Concierge concierge = db.Concierges.Find(id);
            if (concierge == null)
            {
                return HttpNotFound();
            }
            return View(concierge);
        }

        // GET: Concierges/Create
        public ActionResult Create()
        {
            ViewBag.NumAdresse = new SelectList(db.Adresses, "NumAdresse", "Pays");
            return View();
        }

        // POST: Concierges/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NumConcierge,NumAdresse,NomConci,PrenomConci,CinConci,SalaireConci,TelConci")] Concierge concierge,
            [Bind(Include = "NumAdresse,Pays,Ville,quartier,descAdresse")] Adresse adresse)
        {
            if (ModelState.IsValid)
            {
                db.Adresses.Add(adresse);
                db.Concierges.Add(concierge);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.NumAdresse = new SelectList(db.Adresses, "NumAdresse", "Pays", concierge.NumAdresse);
            return View(concierge);
        }

        // GET: Concierges/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Concierge concierge = db.Concierges.Find(id);
            if (concierge == null)
            {
                return HttpNotFound();
            }
            //ViewBag.NumAdresse = new SelectList(db.Adresses, "NumAdresse", "Pays", concierge.NumAdresse);
            return View(concierge);
        }

        // POST: Concierges/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NumConcierge,NomConci,PrenomConci,CinConci,SalaireConci,TelConci")] Concierge concierge
           )
        {
            if (ModelState.IsValid)
            {

                db.Entry(concierge).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.NumAdresse = new SelectList(db.Adresses, "NumAdresse", "Pays", concierge.NumAdresse);
            return View(concierge);
        }

        // GET: Concierges/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Concierge concierge = db.Concierges.Find(id);
            if (concierge == null)
            {
                return HttpNotFound();
            }
            return View(concierge);
        }

        // POST: Concierges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Concierge concierge = db.Concierges.Find(id);
            db.Concierges.Remove(concierge);
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
