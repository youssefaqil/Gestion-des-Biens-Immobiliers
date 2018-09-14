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
    public class Locals1Controller : ControllerAuth
    {
        private AgenceEntities db = new AgenceEntities();

        // GET: Locals1
        public ActionResult Index()
        {
            var locals = db.Locals.Include(l => l.Adresse).Include(l => l.Immeuble).Include(l => l.Personne).Include(l => l.Syndic).Include(l => l.TypeLocal);
            return View(locals.ToList());
        }

        // GET: Locals1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Local local = db.Locals.Find(id);
            if (local == null)
            {
                return HttpNotFound();
            }
            return View(local);
        }

        // GET: Locals1/Create
        public ActionResult Create()
        {
            ViewBag.NumAdresse = new SelectList(db.Adresses, "NumAdresse", "Pays");
            ViewBag.CodeImmeuble = new SelectList(db.Immeubles, "CodeImmeuble", "NomImmeuble");
            ViewBag.CodePers = new SelectList(db.Personnes, "CodePers", "nom");
            ViewBag.CodeSynd = new SelectList(db.Syndics, "CodeSynd", "NomSynd");
            ViewBag.NumTypeLocal = new SelectList(db.TypeLocals, "NumTypeLocal", "Description");
            return View();
        }

        // POST: Locals1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CodeLocal,NumTypeLocal,NumAdresse,CodeImmeuble,CodePers,CodeSynd,SuperficieLocal,NbrPieceLocal,NbrChambreLocal,TitreLocal,DescrLocal,MeubleLocal,PrixRefLocationLocal,ServiceLocal,Staut,rezCh,Interphone,Parking,Garage,Jardin,DateDDispo,DateFDispo,NumLocal")] Local local)
        {
            if (ModelState.IsValid)
            {
                db.Locals.Add(local);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.NumAdresse = new SelectList(db.Adresses, "NumAdresse", "Pays", local.NumAdresse);
            ViewBag.CodeImmeuble = new SelectList(db.Immeubles, "CodeImmeuble", "NomImmeuble", local.CodeImmeuble);
            ViewBag.CodePers = new SelectList(db.Personnes, "CodePers", "nom", local.CodePers);
            ViewBag.CodeSynd = new SelectList(db.Syndics, "CodeSynd", "NomSynd", local.CodeSynd);
            ViewBag.NumTypeLocal = new SelectList(db.TypeLocals, "NumTypeLocal", "Description", local.NumTypeLocal);
            return View(local);
        }

        // GET: Locals1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Local local = db.Locals.Find(id);
            if (local == null)
            {
                return HttpNotFound();
            }
            ViewBag.NumAdresse = new SelectList(db.Adresses, "NumAdresse", "Pays", local.NumAdresse);
            ViewBag.CodeImmeuble = new SelectList(db.Immeubles, "CodeImmeuble", "NomImmeuble", local.CodeImmeuble);
            ViewBag.CodePers = new SelectList(db.Personnes, "CodePers", "nom", local.CodePers);
            ViewBag.CodeSynd = new SelectList(db.Syndics, "CodeSynd", "NomSynd", local.CodeSynd);
            ViewBag.NumTypeLocal = new SelectList(db.TypeLocals, "NumTypeLocal", "Description", local.NumTypeLocal);
            return View(local);
        }

        // POST: Locals1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CodeLocal,NumTypeLocal,NumAdresse,CodeImmeuble,CodePers,CodeSynd,SuperficieLocal,NbrPieceLocal,NbrChambreLocal,TitreLocal,DescrLocal,MeubleLocal,PrixRefLocationLocal,ServiceLocal,Staut,rezCh,Interphone,Parking,Garage,Jardin,DateDDispo,DateFDispo,NumLocal")] Local local)
        {
            if (ModelState.IsValid)
            {
                db.Entry(local).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NumAdresse = new SelectList(db.Adresses, "NumAdresse", "Pays", local.NumAdresse);
            ViewBag.CodeImmeuble = new SelectList(db.Immeubles, "CodeImmeuble", "NomImmeuble", local.CodeImmeuble);
            ViewBag.CodePers = new SelectList(db.Personnes, "CodePers", "nom", local.CodePers);
            ViewBag.CodeSynd = new SelectList(db.Syndics, "CodeSynd", "NomSynd", local.CodeSynd);
            ViewBag.NumTypeLocal = new SelectList(db.TypeLocals, "NumTypeLocal", "Description", local.NumTypeLocal);
            return View(local);
        }

        // GET: Locals1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Local local = db.Locals.Find(id);
            if (local == null)
            {
                return HttpNotFound();
            }
            return View(local);
        }

        // POST: Locals1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Local local = db.Locals.Find(id);
            db.Locals.Remove(local);
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
