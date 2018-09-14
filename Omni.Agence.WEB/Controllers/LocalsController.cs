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
    public class LocalsController : ControllerAuth
    {
        private AgenceEntities db = new AgenceEntities();

        // GET: Locals
        public ActionResult Index()
        {
            
            var locals = db.Locals.Include(l => l.Adresse).Include(l => l.Immeuble).Include(l => l.Personne).Include(l => l.Syndic).Include(l => l.TypeLocal);
            return View(locals.ToList());
        }
        [HttpPost]
        public ViewResult Index(int? nbrchambre, string lieuxlocal, int? minprix, int? maxprix, int? minsurface, int? maxsurface, string typelocal)
        {
            var locals = db.Locals.Include(l => l.Adresse).Include(l => l.Immeuble).Include(l => l.Personne).Include(l => l.Syndic).Include(l => l.TypeLocal);
            ViewBag.pieces = nbrchambre;
            ViewBag.lieux = lieuxlocal;
            ViewBag.minprix = minprix;
            ViewBag.maxprix = maxprix;
            ViewBag.minsurface = minsurface;
            ViewBag.maxsurface = maxsurface;
            
            var query = from local in db.Locals
                        where ((local.PrixRefLocationLocal >= minprix) || !minprix.HasValue) 
                        && ((local.PrixRefLocationLocal <= maxprix) || !maxprix.HasValue)
                        //&& (local.Adresse.descAdresse.Contains(lieuxlocal) || string.IsNullOrEmpty(lieuxlocal))
                        //&& (local.Adresse.quartier.Contains(lieuxlocal) || string.IsNullOrEmpty(lieuxlocal))
                        && (local.Adresse.Ville.Contains(lieuxlocal)||local.Adresse.quartier.Contains(lieuxlocal)||local.Adresse.descAdresse.Contains(lieuxlocal) || string.IsNullOrEmpty(lieuxlocal))
                        && (local.NbrPieceLocal==nbrchambre || !nbrchambre.HasValue)
                        && ((local.SuperficieLocal >= minsurface) || !minsurface.HasValue)
                        && ((local.SuperficieLocal <= maxsurface) || !maxsurface.HasValue)
                        select local;

         
            return View(query.ToList());
        }

        // GET: Locals/Details/5
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
            ViewBag.Local = local;
            return View();
        }

        // GET: Locals/Create
        public ActionResult Create(int? id)
        {
            ViewBag.NumAdresse = new SelectList(db.Adresses, "NumAdresse", "Pays");
            ViewBag.CodeImmeuble = new SelectList(db.Immeubles, "CodeImmeuble", "NomImmeuble");
            ViewBag.CodePers = new SelectList(db.Personnes.Where(p => p.CodeTypePersonne == 1), "CodePers", "nom");
            ViewBag.CodeSynd = new SelectList(db.Syndics, "CodeSynd", "NomSynd");
            ViewBag.NumTypeLocal = new SelectList(db.TypeLocals, "NumTypeLocal", "Description");

            //Add 21/10/15
            var locals = db.Locals.Include(l => l.Adresse).Include(l => l.Immeuble).Include(l => l.Personne).Include(l => l.Syndic).Include(l => l.TypeLocal).Where(i => i.CodePers == id);
            ViewBag.listLocals = locals.ToList();

            Personne pers = db.Personnes.Find(id);
            ViewBag.Pers = pers; 
            return View();
        }

        // POST: Locals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

             public ActionResult Create([Bind(Include = "CodeLocal,NumTypeLocal,NumAdresse,CodeImmeuble,CodePers,CodeSynd,SuperficieLocal,NbrPieceLocal,NbrChambreLocal,TitreLocal,DescrLocal,MeubleLocal,PrixRefLocationLocal,ServiceLocal,Staut,rezCh,Interphone,Parking,Garage,Jardin,DateDDispo,DateFDispo,NumLocal")] Local local,
             [Bind(Include = "NumAdresse,Pays,Ville,quartier,descAdresse,CPadress")] Adresse adresse, int? id)
        {
            if (ModelState.IsValid)
            {
                local.CodePers = id;
                db.Adresses.Add(adresse);
                local.Staut = 0;
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




        public ActionResult CreateDepense(int? id)
        {
            ViewBag.CodeImmeuble = new SelectList(db.Immeubles, "CodeImmeuble", "NomImmeuble");
            ViewBag.CodeLocal = new SelectList(db.Locals, "CodeLocal", "TitreLocal");
            //ViewBag.NumTypeDesignationDespense = new SelectList(db.TypeDesignationDepenses, "NumTypeDesignationDespense", "Description");
            ViewBag.codeOp = new SelectList(db.Operations, "codeOp", "codeOp");
            ViewBag.CodePers = new SelectList(db.ContratLocations, "CodePers", "CodePers");
            ViewBag.CodeLoc = id;
            return View();
        }

        // POST: Depenses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details([Bind(Include = "codeOp,CodePers,codeType,montant,dateOp")] Operation operation,
            [Bind(Include = "codeOp,CodeImmeuble,CodeLocal,NumTypeDesignationDespense,Description,DateDepense")] Depense depense, int? id)
        {
            if (ModelState.IsValid)
            {
                Local local = db.Locals.Find(id);

                operation.dateOp = DateTime.Now;
                operation.codeType = 3;
                operation.CodePers = local.CodePers;
                db.Operations.Add(operation);

                depense.codeOp = operation.codeOp;
                depense.CodeLocal = id;
                db.Depenses.Add(depense);
                db.SaveChanges();
                return RedirectToAction("Details", new  {id=depense.CodeLocal });
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
            ViewBag.CodeLoc = operation.Depense.CodeLocal;
            ViewBag.codeOp = new SelectList(db.Depenses, "codeOp", "Description", operation.codeOp);
            ViewBag.CodePers = new SelectList(db.Personnes, "CodePers", "nom", operation.CodePers);
            ViewBag.codeType = new SelectList(db.TypeOps, "codeType", "libelle", operation.codeType);

            //String sx = model => model.Depense.NumTypeDesignationDespense;
            int sx = operation.Depense.NumTypeDesignationDespense;
            //ViewBag.selected = "";
            if (sx == 1)
            {
                ViewBag.selectedr = "selected";
            }
            else if (sx == 2)
            {
                ViewBag.selecteds = "selected";
            }
            else if (sx == 3)
            {
                ViewBag.selectedf = "selected";
            }



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
                return RedirectToAction("Details", new {id=depense.CodeLocal });
            }
            ViewBag.codeOp = new SelectList(db.Depenses, "codeOp", "Description", operation.codeOp);
            ViewBag.CodePers = new SelectList(db.Personnes, "CodePers", "nom", operation.CodePers);
            ViewBag.codeType = new SelectList(db.TypeOps, "codeType", "libelle", operation.codeType);
            ViewBag.codeOp = new SelectList(db.PaiementLoyers, "codeOp", "MoyenPaiement", operation.codeOp);
            return View(operation);
        }



        public ActionResult ModifierDepensetest(int? id)
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
        public ActionResult ModifierDepensetest([Bind(Include = "codeOp,CodePers,codeType,montant,dateOp")] Operation operation,
            [Bind(Include = "codeOp,CodeImmeuble,CodeLocal,NumTypeDesignationDespense,Description")] Depense depense)
        {
            if (ModelState.IsValid)
            {
                db.Entry(depense).State = EntityState.Modified;
                db.Entry(operation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
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



        // GET: Locals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Local local = db.Locals.Find(id);
            //Pour test si les valeurs ne sont pas null au niveau de la BDD
            //if (!local.rezCh.HasValue)
            //{
            //    local.rezCh = false;            
            //}
            //if (!local.Interphone.HasValue)
            //{
            //    local.Interphone = false;
            //}
            //if (!local.Parking.HasValue)
            //{
            //    local.Parking = false;
            //}
            //if (!local.Jardin.HasValue)
            //{
            //    local.Jardin = false;
            //}

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

        // POST: Locals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CodeLocal,NumTypeLocal,NumAdresse,CodeImmeuble,CodePers,CodeSynd,SuperficieLocal,NbrPieceLocal,NbrChambreLocal,TitreLocal,DescrLocal,MeubleLocal,PrixRefLocationLocal,ServiceLocal,Staut,rezCh,Interphone,Parking,Garage,Jardin,DateDDispo,DateFDispo,NumLocal")] Local local,
            [Bind(Include = "NumAdresse,Pays,Ville,quartier,descAdresse,CPadress")] Adresse adresse)
        {
            if (ModelState.IsValid)
            {
                
                db.Entry(adresse).State = EntityState.Modified;
                db.Entry(local).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new {id=local.CodeLocal });
            }
            ViewBag.NumAdresse = new SelectList(db.Adresses, "NumAdresse", "Pays", local.NumAdresse);
            ViewBag.CodeImmeuble = new SelectList(db.Immeubles, "CodeImmeuble", "NomImmeuble", local.CodeImmeuble);
            ViewBag.CodePers = new SelectList(db.Personnes, "CodePers", "nom", local.CodePers);
            ViewBag.CodeSynd = new SelectList(db.Syndics, "CodeSynd", "NomSynd", local.CodeSynd);
            ViewBag.NumTypeLocal = new SelectList(db.TypeLocals, "NumTypeLocal", "Description", local.NumTypeLocal);
            return View(local);
        }

        // GET: Locals/Delete/5
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

        // POST: Locals/Delete/5
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
