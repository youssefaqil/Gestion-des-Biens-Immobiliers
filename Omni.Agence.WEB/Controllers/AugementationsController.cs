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
    public class AugementationsController : ControllerAuth
    {
        private AgenceEntities db = new AgenceEntities();

        // GET: Augementations
        public ActionResult Index()
        {
            var augementation = db.Augementations.Include(a => a.ContratLocation);
            return View(augementation.ToList());
        }

        // GET: Augementations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Augementation augementation = db.Augementations.Find(id);
            if (augementation == null)
            {
                return HttpNotFound();
            }
            return View(augementation);
        }

        // GET: Augementations/Create
        public ActionResult Create()
        {
            ViewBag.CodePers = new SelectList(db.ContratLocations, "CodePers", "commentaire");
            return View();
        }

        // POST: Augementations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idAug,dateAug,pcAug,CodeContrat,CodePers,CodeLocal")] Augementation augementation)
        {
            if (ModelState.IsValid)
            {
                db.Augementations.Add(augementation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CodePers = new SelectList(db.ContratLocations, "CodePers", "commentaire", augementation.CodePers);
            return View(augementation);
        }

        // GET: Augementations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Augementation augementation = db.Augementations.Find(id);
            if (augementation == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodePers = new SelectList(db.ContratLocations, "CodePers", "commentaire", augementation.CodePers);
            return View(augementation);
        }

        // POST: Augementations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idAug,dateAug,pcAug,CodeContrat,CodePers,CodeLocal")] Augementation augementation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(augementation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CodePers = new SelectList(db.ContratLocations, "CodePers", "commentaire", augementation.CodePers);
            return View(augementation);
        }

        // GET: Augementations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Augementation augementation = db.Augementations.Find(id);
            if (augementation == null)
            {
                return HttpNotFound();
            }
            return View(augementation);
        }

        // POST: Augementations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Augementation augementation = db.Augementations.Find(id);
            db.Augementations.Remove(augementation);
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
