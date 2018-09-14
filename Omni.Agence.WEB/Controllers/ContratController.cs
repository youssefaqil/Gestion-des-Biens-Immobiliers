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
    public class ContratController : ControllerAuth
    {
        private AgenceEntities db = new AgenceEntities();
        private static int codeLocal;
        private static int codePersLocataire;
        private static int prixLocal;
        private static int codePersLocal;
        // GET: Contrat
        public ActionResult Index()
        {
            codePersLocataire = 0;
            codePersLocal = 0;
            prixLocal = 0;
            var contratLocations = db.ContratLocations.Include(c => c.Local).Include(c => c.Personne);
            return View(contratLocations.ToList());
        }
        public ActionResult Create1(int? id)
        {
            if (id != null)
            {
                //personne = db.Personnes.Find(id);
                //codeLocal = personne.CodePers;
                codeLocal = int.Parse(id.ToString());
                ViewBag.CodeLocal = codeLocal;
            }
            return RedirectToAction("Create");
        }
        public ActionResult CContrat()
        {
            List<Personne> ListLoc = db.Personnes.Where(c => c.CodeTypePersonne == 2).ToList();
            ViewBag.ListLoc = ListLoc;
            return View();
        }
        [HttpPost]
        public ActionResult CContrat([Bind(Include = "CodePers,CodeLocal,CodeContrat,DateJuissance,Usage,LoyerDebase,Augementation,dateAugmentation,Charge,TaxeDedilite,Garage,LoyerNet,Caution,DateFinContrat,FrequencePaiement,adults,enfants,animaux,commentaire")] ContratLocation contratLocation,
            int? payment)
        {

            contratLocation.CodeLocal = payment.Value;
            Local loc = db.Locals.Find(contratLocation.CodeLocal);
            loc.Staut = 1;
            db.Entry(loc).State = EntityState.Modified;

            double ln = ((100 + contratLocation.TaxeDedilite.Value + contratLocation.Charge.Value) / 100) * contratLocation.LoyerDebase.Value;
            double test = (100 + contratLocation.TaxeDedilite.Value + contratLocation.Charge.Value);
            contratLocation.LoyerNet = (test / 100) * contratLocation.LoyerDebase.Value + contratLocation.Garage;
            contratLocation.isClosed = 1;
            if (contratLocation.Garage == null)
            {
                contratLocation.Garage = 0;
            }
            if(contratLocation.Caution == null)
            {
                contratLocation.Caution = 0;
            }
            db.ContratLocations.Add(contratLocation);
            db.SaveChanges();
            return RedirectToAction("Details", new { id = contratLocation.CodeContrat });
            //}

            //ViewBag.CodeLocal = new SelectList(db.Locals, "CodeLocal", "TitreLocal", contratLocation.CodeLocal);
            //ViewBag.CodePers = new SelectList(db.Personnes, "CodePers", "nom", contratLocation.CodePers);
            //return RedirectToAction("Index");
        }

        public ActionResult DetailsLocataire()
        {
            return View();
        }
        public ActionResult Acceuil()
        {
            return View();
        }
        // GET: Contrat/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Augementation> ListAug = db.Augementations.Where(c => c.CodeContrat == id).ToList();
            ViewBag.ListAug = ListAug;
            if (ListAug.Count() > 0)
            {
                int pcAug = ListAug.Max(c => c.pcAug).Value;
                ViewBag.PcAug = pcAug + 1;
            }
            var contratLocations = db.ContratLocations;
            ContratLocation contratLocation = null;
            List<ContratLocation> ListeC = contratLocations.ToList();
            for (int i = 0; i < db.ContratLocations.Count(); i++)
            {

                if (ListeC[i].CodeContrat == id.Value)
                {
                    contratLocation = ListeC[i];
                    if (contratLocation.Usage == 0)
                    {
                        ViewBag.us = "Habitation";
                    }
                    else if (contratLocation.Usage == 1)
                    {
                        ViewBag.us = "Commercial";
                    }
                    if (contratLocation.Garage == 0)
                    {
                        ViewBag.garage = "Non";
                    }
                    else if (contratLocation.Garage == 1)
                    {
                        ViewBag.garage = "Oui";
                    }
                    if (contratLocation.FrequencePaiement == 0)
                    {
                        ViewBag.fp = "Mensuel";
                    }
                    else if (contratLocation.FrequencePaiement == 1)
                    {
                        ViewBag.fp = "Trimestriel";
                    }



                }

            }

            //ContratLocation contratLocation = db.ContratLocations.Find(id);
            //if (contratLocation == null)
            //{
            //    return HttpNotFound();
            //}
            return View(contratLocation);
        }

        // GET: Contrat/Create
        //public ActionResult Create(int? id)
        //{
        //    if (id != null) { 
        //    Personne pers = db.Personnes.Find(id);
        //    ViewBag.nompers = pers.nom;
        //    ViewBag.prenompers = pers.prenom;
        //    }
        //    ViewBag.CodeLocal = new SelectList(db.Locals, "CodeLocal", "TitreLocal");
        //    ViewBag.CodePers = new SelectList(db.Personnes, "CodePers", "nom");

        //    return View();
        //}

        //// POST: Contrat/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "CodePers,CodeLocal,CodeContrat,DateJuissance,Usage,LoyerDebase,Augementation,dateAugmentation,Charge,TaxeDedilite,Garage,LoyerNet,Caution,DateFinContrat,FrequencePaiement")] ContratLocation contratLocation,

        //    [Bind(Include = "CodeLocal")] Local local)
        //{
        //    //try {
        //        if (ModelState.IsValid)
        //        {
        //            if (contratLocation.DateJuissance < contratLocation.DateFinContrat)
        //            {
        //                int id = local.CodeLocal;
        //                Local loc = db.Locals.Find(id);
        //                loc.Statut = 1;
        //                db.Entry(loc).State = EntityState.Modified;



        //                double ln = ((100 + contratLocation.TaxeDedilite.Value + contratLocation.Charge.Value) / 100) * contratLocation.LoyerDebase.Value;
        //                double test = (100 + contratLocation.TaxeDedilite.Value + contratLocation.Charge.Value);
        //                contratLocation.LoyerNet = (test / 100) * contratLocation.LoyerDebase.Value;      
        //                db.ContratLocations.Add(contratLocation);


        //                db.SaveChanges();
        //            }
        //            return RedirectToAction("Index");
        //        }

        //        ViewBag.CodeLocal = new SelectList(db.Locals, "CodeLocal", "TitreLocal", contratLocation.CodeLocal);
        //        ViewBag.CodePers = new SelectList(db.Personnes, "CodePers", "nom", contratLocation.CodePers);
        //        return View(contratLocation);
        //    //}
        //    //catch
        //    //{
        //    //    return View("Create");
        //    //    }
        //}


        [HttpPost]
        [ActionName(name: "Details")]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAug([Bind(Include = "idAug,CodePers,CodeLocal,CodeContrat,dateAug,pcAug")] Augementation augementation)
        {
            //if (ModelState.IsValid)
            //{
                ContratLocation contrat = db.ContratLocations.Where(c => c.CodeContrat == augementation.CodeContrat).First();
                if (!string.IsNullOrEmpty(contrat.DateFinContrat.ToString()))
                {


                    if (augementation.dateAug > contrat.DateJuissance && augementation.dateAug < contrat.DateFinContrat)
                    {
                        if (augementation.pcAug.Value > db.Augementations.Where(c => c.CodeContrat == augementation.CodeContrat).Max(c => c.pcAug))
                        {
                            db.Augementations.Add(augementation);

                            db.SaveChanges();
                        }
                        return RedirectToAction("Details", new { id = augementation.CodeContrat });
                    }
                    else
                    {
                        //ModelState.AddModelError("dateAugErr", "La date est invalide");

                        return RedirectToAction("Details", new { id = augementation.CodeContrat });
                    }


                }
                else if (augementation.dateAug > contrat.DateJuissance)
                {

                    if (contrat.Augementations.Count() > 0) {

                        if (augementation.pcAug.Value > db.Augementations.Where(c => c.CodeContrat == augementation.CodeContrat).Max(c => c.pcAug) && augementation.dateAug.Value > db.Augementations.Where(c => c.CodeContrat == augementation.CodeContrat).Max(c => c.dateAug))
                    {
                        db.Augementations.Add(augementation);

                        db.SaveChanges();
                    }
                    }
                    else
                    {

                        db.Augementations.Add(augementation);

                        db.SaveChanges();
                    }
                    
                    return RedirectToAction("Details", new { id = augementation.CodeContrat });
                
                }
            //}

            ViewBag.CodePers = new SelectList(db.ContratLocations, "CodePers", "commentaire", augementation.CodePers);
            return View(augementation);
        }





        //[HttpPost, ActionName("DeleteAug")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteAug(int id)
        {
            Augementation augementation = db.Augementations.Find(id);
            int codec = augementation.CodeContrat.Value;
            db.Augementations.Remove(augementation);
            db.SaveChanges();
            return RedirectToAction("Details", new { id = codec });
        }





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
        public ActionResult SelectLocataire(int? id)
        {
            codePersLocataire = id.Value;

            //            ViewBag.codePersLocataire = codePersLocataire;
            return RedirectToAction("CreateContrat", new { idp = id, idl = codePersLocal, idpr = prixLocal });
        }
        public ActionResult SelectLocal(int? id, int? idpr)
        {
            codePersLocal = id.Value;
            prixLocal = idpr.Value;
            //ViewBag.codePersLocal = codePersLocal;
            return RedirectToAction("CreateContrat", new { idp = codePersLocataire, idl = id, idpr = prixLocal });
        }



        public ActionResult CreateContrat(int? idp, int? idl, int? idpr)
        {
            if (idp == 0 && idl == 0)
            {
                ViewBag.codePersLocataire = "";
                ViewBag.codePersLocal = "";
            }
            else
            {

                ViewBag.prixLocal = idpr;
                ViewBag.codePersLocataire = idp;
                ViewBag.codePersLocal = idl;

            }
            ViewBag.CodeLocal = new SelectList(db.Locals, "CodeLocal", "TitreLocal");
            ViewBag.CodePers = new SelectList(db.Personnes, "CodePers", "nom");

            return View();
        }

        // POST: Contrat/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult CreateContrat([Bind(Include = "CodePers,CodeLocal,CodeContrat,DateJuissance,Usage,LoyerDebase,Augementation,dateAugmentation,Charge,TaxeDedilite,Garage,LoyerNet,Caution,DateFinContrat,FrequencePaiement")] ContratLocation contratLocation,
        //    [Bind(Include = "CodeLocal")] Local local)
        //{

        //    if (ModelState.IsValid)
        //    {
        //            int id = local.CodeLocal;
        //            Local loc = db.Locals.Find(id);
        //            loc.Statut = 1;
        //            db.Entry(loc).State = EntityState.Modified;

        //            double ln = ((100 + contratLocation.TaxeDedilite.Value + contratLocation.Charge.Value) / 100) * contratLocation.LoyerDebase.Value;
        //            double test = (100 + contratLocation.TaxeDedilite.Value + contratLocation.Charge.Value);
        //            contratLocation.LoyerNet = (test / 100) * contratLocation.LoyerDebase.Value;
        //            db.ContratLocations.Add(contratLocation);

        //            db.SaveChanges();

        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.CodeLocal = new SelectList(db.Locals, "CodeLocal", "TitreLocal", contratLocation.CodeLocal);
        //    ViewBag.CodePers = new SelectList(db.Personnes, "CodePers", "nom", contratLocation.CodePers);
        //    return View(contratLocation);

        //}
        public ActionResult CreateContratTest()
        {
            ViewBag.CodeLocal = new SelectList(db.Locals, "CodeLocal", "TitreLocal");
            ViewBag.CodePers = new SelectList(db.Personnes, "CodePers", "nom");
            return View();
        }

        // POST: Contrat/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateContrat([Bind(Include = "CodePers,CodeLocal,CodeContrat,DateJuissance,Usage,LoyerDebase,Augementation,dateAugmentation,Charge,TaxeDedilite,Garage,LoyerNet,Caution,DateFinContrat,FrequencePaiement,adults,enfants,animaux,commentaire")] ContratLocation contratLocation)
        {
            if (ModelState.IsValid)
            {
                //int id = local.CodeLocal;
                Local loc = db.Locals.Find(contratLocation.CodeLocal);
                loc.Staut = 1;
                db.Entry(loc).State = EntityState.Modified;

                double ln = ((100 + contratLocation.TaxeDedilite.Value + contratLocation.Charge.Value) / 100) * contratLocation.LoyerDebase.Value;
                double test = (100 + contratLocation.TaxeDedilite.Value + contratLocation.Charge.Value);
                contratLocation.LoyerNet = (test / 100) * contratLocation.LoyerDebase.Value + contratLocation.Garage;
                contratLocation.isClosed = 1;
                if (contratLocation.Garage == null)
                {
                    contratLocation.Garage = 0;
                }
                db.ContratLocations.Add(contratLocation);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = contratLocation.CodeContrat });
            }
            ViewBag.CodeLocal = new SelectList(db.Locals, "CodeLocal", "TitreLocal", contratLocation.CodeLocal);
            ViewBag.CodePers = new SelectList(db.Personnes, "CodePers", "nom", contratLocation.CodePers);
            return View(contratLocation);
        }
        // GET: Contrat/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var contratLocations = db.ContratLocations;
            ContratLocation contratLocation = null;
            List<ContratLocation> ListeC = contratLocations.ToList();
            for (int i = 0; i < db.ContratLocations.Count(); i++)
            {
                if (ListeC[i].CodeContrat == id.Value)
                {
                    contratLocation = ListeC[i];
                }
            }

            //ContratLocation contratLocation = db.ContratLocations.Find(id);
            if (contratLocation == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodeLocal = new SelectList(db.Locals, "CodeLocal", "TitreLocal", contratLocation.CodeLocal);
            ViewBag.CodePers = new SelectList(db.Personnes, "CodePers", "nom", contratLocation.CodePers);
            return View(contratLocation);
        }

        // POST: Contrat/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CodePers,CodeLocal,CodeContrat,DateJuissance,Usage,LoyerDebase,isClosed,dateModification,Charge,TaxeDedilite,Garage,LoyerNet,Caution,DateFinContrat,FrequencePaiement,adults,enfants,animaux,commentaire")] ContratLocation contratLocation)
        {
            //if (ModelState.IsValid)
            //{

                double ln = ((100 + contratLocation.TaxeDedilite.Value + contratLocation.Charge.Value) / 100) * contratLocation.LoyerDebase.Value;
                double test = (100 + contratLocation.TaxeDedilite.Value + contratLocation.Charge.Value);
                contratLocation.LoyerNet = (test / 100) * contratLocation.LoyerDebase.Value + contratLocation.Garage;
                contratLocation.dateModification = DateTime.Now;
                //DateTime Datetest = contratLocation.DateFinContrat.Value;
                db.Entry(contratLocation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = contratLocation.CodeContrat });
            //}

            //ViewBag.CodeLocal = new SelectList(db.Locals, "CodeLocal", "TitreLocal", contratLocation.CodeLocal);
            //ViewBag.CodePers = new SelectList(db.Personnes, "CodePers", "nom", contratLocation.CodePers);
            //return View(contratLocation);
        }

        // GET: Contrat/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContratLocation contratLocation = db.ContratLocations.Where(c => c.CodeContrat == id).First();
            if (contratLocation == null)
            {
                return HttpNotFound();
            }
            return View(contratLocation);
        }

        // POST: Contrat/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var contratLocations = db.ContratLocations;
            ContratLocation contratLocation = null;
            List<ContratLocation> ListeC = contratLocations.ToList();
            for (int i = 0; i < db.ContratLocations.Count(); i++)
            {
                if (ListeC[i].CodeContrat == id)
                {
                    contratLocation = ListeC[i];
                }
            }
            Local local = db.Locals.Find(contratLocation.CodeLocal);
            local.Staut = 0;
            db.Entry(local).State = EntityState.Modified;
            contratLocation.isClosed = 0;
            contratLocation.dateClose = DateTime.Now;
            //ContratLocation contratLocation = db.ContratLocations.Find(id);
            //db.ContratLocations.Remove(contratLocation);

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
