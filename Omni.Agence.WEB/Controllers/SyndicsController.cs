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
    public class SyndicsController : ControllerAuth
    {
        private AgenceEntities db = new AgenceEntities();

        // GET: Syndics
        public ActionResult Index()
        {
            var syndics = db.Syndics.Include(s => s.Adresse);
            return View(syndics.ToList());
        }

        // GET: Syndics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Syndic syndic = db.Syndics.Find(id);
            if (syndic == null)
            {
                return HttpNotFound();
            }
            return View(syndic);
        }

        // GET: Syndics/Create
        public ActionResult Create()
        {
            ViewBag.NumAdresse = new SelectList(db.Adresses, "NumAdresse", "Pays");
            return View();
        }

        // POST: Syndics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CodeSynd,NumAdresse,NomSynd,PrenomSynd,TelSynd,CinSynd,Salaire")] Syndic syndic,
             [Bind(Include = "NumAdresse,Pays,Ville,quartier,descAdresse")] Adresse adresse)
        {
            if (ModelState.IsValid)
            {
                db.Adresses.Add(adresse);
                db.Syndics.Add(syndic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.NumAdresse = new SelectList(db.Adresses, "NumAdresse", "Pays", syndic.NumAdresse);
            return View(syndic);
        }

        // GET: Syndics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Syndic syndic = db.Syndics.Find(id);
            if (syndic == null)
            {
                return HttpNotFound();
            }
            ViewBag.NumAdresse = new SelectList(db.Adresses, "NumAdresse", "Pays", syndic.NumAdresse);
            return View(syndic);
        }

        // POST: Syndics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CodeSynd,NumAdresse,NomSynd,PrenomSynd,TelSynd,CinSynd,Salaire")] Syndic syndic,
       [Bind(Include = "NumAdresse,Pays,Ville,quartier,descAdresse")] Adresse adresse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adresse).State = EntityState.Modified;
                db.Entry(syndic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NumAdresse = new SelectList(db.Adresses, "NumAdresse", "Pays", syndic.NumAdresse);
            return View(syndic);
        }

        // GET: Syndics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Syndic syndic = db.Syndics.Find(id);
            if (syndic == null)
            {
                return HttpNotFound();
            }
            return View(syndic);
        }

        // POST: Syndics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Syndic syndic = db.Syndics.Find(id);
            db.Syndics.Remove(syndic);
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
