using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Omni.Agence.DAL;
using Omni.Agence.WEB.Controllers;

namespace Omni.Agence.Web.Controllers
{
    public class ImmeublesController : ControllerAuth
    {
        private AgenceEntities db = new AgenceEntities();

        // GET: Immeubles
        public ActionResult Index()
        {
            var immeubles = db.Immeubles.Include(i => i.Adresse).Include(i => i.Concierge).Include(i => i.Personne);
            return View(immeubles.ToList());
        }



        //public ActionResult Index(int? idp)
        //{

        //    //if (id == null)
        //    //{
        //    //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    //}
        //    //Immeuble immeuble = db.Immeubles.Find(id);
        //    //if (immeuble == null)
        //    //{
        //    //    return HttpNotFound();
        //    //}
        //    //ViewBag.NumAdresse = new SelectList(db.Adresses, "NumAdresse", "Pays", immeuble.NumAdresse);
        //    //ViewBag.NumConcierge = new SelectList(db.Concierges, "NumConcierge", "NomConci", immeuble.NumConcierge);
        //    //ViewBag.CodePers = new SelectList(db.Personnes, "CodePers", "nom", immeuble.CodePers);
        //    //return View(immeuble);

        //    var immeubles = db.Immeubles.Include(i => i.Adresse).Include(i => i.Concierge).Include(i => i.Personne).Where(i => i.CodePers == idp);
        //    return View(immeubles.ToList());
        //}

        // GET: Immeubles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Immeuble immeuble = db.Immeubles.Find(id);
            //if (!immeuble.Parking.HasValue)
            //{
            //    immeuble.Parking = false;
            //}
            //if (!immeuble.Jardin.HasValue)
            //{
            //    immeuble.Jardin = false;
            //}
            //if (!immeuble.Interphone.HasValue)
            //{
            //    immeuble.Interphone = false;
            //}

            if (immeuble == null)
            {
                return HttpNotFound();
            }
            ViewBag.immeuble = immeuble;
            return View();
        }

        // GET: Immeubles/Create
        public ActionResult Create()
        {
            ViewBag.NumAdresse = new SelectList(db.Adresses, "NumAdresse", "Pays");
            ViewBag.NumConcierge = new SelectList(db.Concierges, "NumConcierge", "NomConci");
            ViewBag.CodePers = new SelectList(db.Personnes, "CodePers", "nom");
            return View();
        }

        // POST: Immeubles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CodeImmeuble,NumAdresse,NumConcierge,NomImmeuble,ServiceImmeuble,NbrEtage,CodePers,RefCodeImm")] Immeuble immeuble,
            [Bind(Include = "NumAdresse,Pays,Ville,quartier,descAdresse")] Adresse adresse)
        {
            if (ModelState.IsValid)
            {
                
                db.Adresses.Add(adresse);
                db.Immeubles.Add(immeuble);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.NumAdresse = new SelectList(db.Adresses, "NumAdresse", "Pays", immeuble.NumAdresse);
            ViewBag.NumConcierge = new SelectList(db.Concierges, "NumConcierge", "NomConci", immeuble.NumConcierge);
            ViewBag.CodePers = new SelectList(db.Personnes, "CodePers", "nom", immeuble.CodePers);
            return View(immeuble);
        }



        public ActionResult CreateDepense(int? id)
        {
            ViewBag.Codeimm = id;
            ViewBag.CodeImmeuble = new SelectList(db.Immeubles, "CodeImmeuble", "NomImmeuble");
            ViewBag.CodeLocal = new SelectList(db.Locals, "CodeLocal", "TitreLocal");
            //ViewBag.NumTypeDesignationDespense = new SelectList(db.TypeDesignationDepenses, "NumTypeDesignationDespense", "Description");
            ViewBag.codeOp = new SelectList(db.Operations, "codeOp", "codeOp");
            ViewBag.CodePers = new SelectList(db.ContratLocations, "CodePers", "CodePers");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details([Bind(Include = "codeOp,CodePers,codeType,montant,dateOp")] Operation operation,
             [Bind(Include = "codeOp,CodeImmeuble,CodeLocal,NumTypeDesignationDespense,Description,DateDepense")] Depense depense, int? id)
        {
            if (ModelState.IsValid)
            {
                Immeuble immeuble = db.Immeubles.Find(id);


                operation.dateOp = DateTime.Now;
                operation.codeType = 3;
                operation.CodePers = immeuble.CodePers;
                db.Operations.Add(operation);

                depense.codeOp = operation.codeOp;
                depense.CodeImmeuble = id;
                db.Depenses.Add(depense);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = immeuble.CodeImmeuble });
            }

            ViewBag.CodeImmeuble = new SelectList(db.Immeubles, "CodeImmeuble", "NomImmeuble", depense.CodeImmeuble);
            ViewBag.CodeLocal = new SelectList(db.Locals, "CodeLocal", "TitreLocal", depense.CodeLocal);
            ViewBag.NumTypeDesignationDespense = new SelectList(db.TypeDesignationDepenses, "NumTypeDesignationDespense", "Description", depense.NumTypeDesignationDespense);
            ViewBag.codeOp = new SelectList(db.Operations, "codeOp", "codeOp", depense.codeOp);
            return View(depense);
        }



        public ActionResult ModifierDepense(int? id)
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
            ViewBag.NumTypeDesignationDespense = new SelectList(db.TypeDesignationDepenses, "NumTypeDesignationDespense", "Description", operation.Depense.NumTypeDesignationDespense);
            ViewBag.codeOp = new SelectList(db.PaiementLoyers, "codeOp", "MoyenPaiement", operation.codeOp);
            return View(operation);
        }

        // POST: Operations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModifierDepense([Bind(Include = "codeOp,CodePers,codeType,montant,dateOp")] Operation operation,
            [Bind(Include = "codeOp,CodeImmeuble,CodeLocal,NumTypeDesignationDespense,Description")] Depense depense)
        {
            if (ModelState.IsValid)
            {
                db.Entry(depense).State = EntityState.Modified;
                db.Entry(operation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = depense.CodeImmeuble });
            }
            ViewBag.codeOp = new SelectList(db.Depenses, "codeOp", "Description", operation.codeOp);
            ViewBag.CodePers = new SelectList(db.Personnes, "CodePers", "nom", operation.CodePers);
            ViewBag.codeType = new SelectList(db.TypeOps, "codeType", "libelle", operation.codeType);
            ViewBag.codeOp = new SelectList(db.PaiementLoyers, "codeOp", "MoyenPaiement", operation.codeOp);
            return View(operation);
        }

        public ActionResult DeleteDepense(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Operation operation = db.Operations.Find(id);
            //            Depense depense = db.Depenses.Find(id);
            if (operation == null)
            {
                return HttpNotFound();
            }
            return View(operation);
        }

        // POST: Operations/Delete/5
        [HttpPost, ActionName("DeleteDepense")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteDepenseConfirmed(int id)
        {
            Operation operation = db.Operations.Find(id);
            Depense depense = db.Depenses.Find(id);
            db.Depenses.Remove(depense);
            db.Operations.Remove(operation);

            db.SaveChanges();
            return RedirectToAction("Index");
        }




        // GET: Immeubles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Immeuble immeuble = db.Immeubles.Find(id);
            //Pour test si les valeurs ne sont pas null au niveau de la BDD
            //if (!immeuble.Parking.HasValue)
            //{
            //    immeuble.Parking = false;
            //}
            //if (!immeuble.Jardin.HasValue)
            //{
            //    immeuble.Jardin = false;
            //}
            //if (!immeuble.Interphone.HasValue)
            //{
            //    immeuble.Interphone = false;
            //}
            if (immeuble == null)
            {
                return HttpNotFound();
            }

            ViewBag.NumAdresse = new SelectList(db.Adresses, "NumAdresse", "Pays", immeuble.NumAdresse);
            ViewBag.NumConcierge = new SelectList(db.Concierges, "NumConcierge", "NomConci", immeuble.NumConcierge);
            ViewBag.CodePers = new SelectList(db.Personnes, "CodePers", "nom", immeuble.CodePers);
            return View(immeuble);
        }

        // POST: Immeubles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CodeImmeuble,NumAdresse,NumConcierge,NomImmeuble,ServiceImmeuble,NbrEtage,CodePers,Parking,Jardin,Interphone,Description")] Immeuble immeuble,
            [Bind(Include = "NumAdresse,Pays,Ville,quartier,descAdresse,CPadress")] Adresse adresse,
            [Bind(Include = "NumConcierge,NomConci,PrenomConci,CinConci,SalaireConci,TelConci")] Concierge concierge)
        {
            //if (ModelState.IsValid)
            //{
            //db.Adresses.Add(adresse);
            immeuble.Adresse = adresse;


            db.Entry(immeuble.Adresse).State = EntityState.Modified;
            if (concierge.NumConcierge != 0)
            {
                immeuble.Concierge = concierge;
                db.Entry(immeuble.Concierge).State = EntityState.Modified;
            }
            db.Entry(immeuble).State = EntityState.Modified;

            db.SaveChanges();
            return RedirectToAction("Details", new { id = immeuble.CodeImmeuble });
            //}
            ViewBag.NumAdresse = new SelectList(db.Adresses, "NumAdresse", "Pays", immeuble.NumAdresse);
            ViewBag.NumConcierge = new SelectList(db.Concierges, "NumConcierge", "NomConci", immeuble.NumConcierge);
            ViewBag.CodePers = new SelectList(db.Personnes, "CodePers", "nom", immeuble.CodePers);
            return View(immeuble);
        }

        // GET: Immeubles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Immeuble immeuble = db.Immeubles.Find(id);

            if (immeuble == null)
            {
                return HttpNotFound();
            }
            return View(immeuble);
        }

        // POST: Immeubles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Immeuble immeuble = db.Immeubles.Find(id);
            db.Immeubles.Remove(immeuble);
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
