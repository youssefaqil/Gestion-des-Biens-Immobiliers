using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Omni.Agence.DAL;
using Omni.Agence.WEB.Models;
using Omni.Agence.WEB.Controllers;

namespace Omni.Agence.Web.Controllers
{  
    public class LocatairesController : ControllerAuth
    {
        private AgenceEntities db = new AgenceEntities();
        private static int CodeLocataire = 0;
        private static int CodeLocal = 0;

        // GET: Locataires
        public ActionResult Index()
        {
            var personnes = db.Personnes.Include(p => p.Adresse).Include(p => p.TypePersonne).Where(p => p.CodeTypePersonne.Equals(2));
            return View(personnes.ToList());
        }


        //id = 
        public ActionResult PaiementLoc(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var cll = db.ContratLocations;
            for (int i = 0; i < cll.ToList().Count; i++)
            {
                if (cll.ToList()[i].CodeContrat == id)
                {
                    CodeLocal = cll.ToList()[i].CodeLocal;
                }
            }

            //ContratLocation contratLocation = db.ContratLocations.Find(id);
            var paiementLoyers = db.PaiementLoyers.Include(p => p.ContratLocation).Include(p => p.Operation).Where(c => c.CodeContrat == id);
            ViewBag.NomPre = paiementLoyers.First().ContratLocation.Personne.nom + " " + paiementLoyers.First().ContratLocation.Personne.prenom;
            ViewBag.CodeContrat = id;

            //PaiementLoyer paiementLoyer=db.PaiementLoyers.Find(id);
            //List<PaiementLoyer> plList = new List<PaiementLoyer>();
            //CodeLocal = contratLocation.CodeLocal;
            //String clString = plList[plList.Count-1].ContratLocation.CodeLocal.ToString();
            //CodeLocal = Int32.Parse(clString);
            return View(paiementLoyers.ToList());

        }
        public ActionResult EditContrat(int? id)
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditContrat([Bind(Include = "CodePers,CodeLocal,CodeContrat,DateJuissance,Usage,LoyerDebase,Augementation,dateAugmentation,Charge,TaxeDedilite,Garage,LoyerNet,Caution,DateFinContrat,FrequencePaiement")] ContratLocation contratLocation)
        {
            if (ModelState.IsValid)
            {

                double ln = ((100 + contratLocation.TaxeDedilite.Value + contratLocation.Charge.Value) / 100) * contratLocation.LoyerDebase.Value;
                double test = (100 + contratLocation.TaxeDedilite.Value + contratLocation.Charge.Value);
                contratLocation.LoyerNet = (test / 100) * contratLocation.LoyerDebase.Value;

                db.Entry(contratLocation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DetailsContrat", new { id = contratLocation.CodeContrat });
            }

            ViewBag.CodeLocal = new SelectList(db.Locals, "CodeLocal", "TitreLocal", contratLocation.CodeLocal);
            ViewBag.CodePers = new SelectList(db.Personnes, "CodePers", "nom", contratLocation.CodePers);
            return View(contratLocation);
        }

        // GET: Locataires/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personne personne = db.Personnes.Find(id);
            CodeLocataire = personne.CodePers;
            //CodeLocal=personne.
            if (personne.sexe == 0)
            {
                ViewBag.sexe = "Femme";
            }
            else if (personne.sexe == 1)
            {
                ViewBag.sexe = "Homme";
            }
            if (personne == null)
            {
                return HttpNotFound();
            }
            return View(personne);
        }


        // GET: Locataires/Create
        public ActionResult Create()
        {
            ViewBag.NumAdresse = new SelectList(db.Adresses, "NumAdresse", "Pays");
            ViewBag.CodeTypePersonne = new SelectList(db.TypePersonnes, "CodeTypePersonne", "Libelle");
            return View();
        }
        public ActionResult CreatePL(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContratLocation contrat = db.ContratLocations.Where(x => x.CodeContrat == id.Value).First();
            ViewBag.Statut = contrat.isClosed;
            ViewBag.CodeC = id;
            double loyerNet = 0;
            ViewBag.NomPre = contrat.Personne.nom + " " + contrat.Personne.prenom;
            ViewBag.CodePre = contrat.Personne.CodePers;
            int aug;

            var cll = db.ContratLocations;
            CodeLocal = cll.Where(l => l.CodeContrat == id).First().CodeLocal;
            if (contrat.Augementations.Count() > 0)
            {
                if (contrat.Augementations.Where(d => d.dateAug.Value < DateTime.Now).Count() > 0)
                {
                    aug = contrat.Augementations.Where(d => d.dateAug.Value < DateTime.Now).OrderBy(d => d.dateAug).Last().pcAug.Value;
                }
                else
                {
                    aug = 0;
                }
            }
            else
            {
                aug = 0;
            }
            int taxe = contrat.TaxeDedilite.Value;
            //DateTime dateDP;
            if (contrat.PaiementLoyers.Count() > 0)
            {
                ViewBag.dateDP = contrat.PaiementLoyers.OrderBy(d => d.DteFin).Last().DteFin.Value.AddDays(1);
                if (contrat.FrequencePaiement == 0)
                {
                    ViewBag.dateFP = contrat.PaiementLoyers.OrderBy(d => d.DteFin).Last().DteFin.Value.AddMonths(1);
                }
                else if (contrat.FrequencePaiement == 1)
                {
                    ViewBag.dateFP = contrat.PaiementLoyers.OrderBy(d => d.DteFin).Last().DteFin.Value.AddMonths(3);
                }
            }
            else
            {
                ViewBag.dateDP = contrat.DateJuissance.Value;

                if (contrat.FrequencePaiement == 0)
                {
                    ViewBag.dateFP = contrat.DateJuissance.Value.AddMonths(1).AddDays(-1);
                }
                else if (contrat.FrequencePaiement == 1)
                {
                    ViewBag.dateFP = contrat.DateJuissance.Value.AddMonths(3).AddDays(-1);
                }
            }

            var paiementLoyers = db.PaiementLoyers.Include(p => p.ContratLocation).Include(p => p.Operation).Where(c => c.CodeContrat == id);


            //ViewBag.NomPre = paiementLoyers.First().ContratLocation.Personne.nom + " " + paiementLoyers.First().ContratLocation.Personne.prenom;
            //ViewBag.CodeContrat = id;
            ViewBag.ListP = paiementLoyers.ToList();


            int charge = contrat.Charge.Value;
            int garage = contrat.Garage.Value;
            double loyerBase = contrat.LoyerDebase.Value;
            loyerNet = garage + ((100 + taxe + charge + aug) * loyerBase) / 100;
            if (contrat.FrequencePaiement == 0)
            {
                ViewBag.loyerNet = loyerNet;
            }
            else if (contrat.FrequencePaiement == 1)
            {
                ViewBag.loyerNet = loyerNet * 3;
            }
            ViewBag.contrat = contrat;
            ViewBag.CodePers = new SelectList(db.ContratLocations, "CodePers", "CodePers");
            ViewBag.codeOp = new SelectList(db.Operations, "codeOp", "codeOp");
            return View();
        }
        [HttpPost]
        [ActionName(name: "DetailsContrat")]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAug([Bind(Include = "idAug,CodePers,CodeLocal,CodeContrat,dateAug,pcAug")] Augementation augementation)
        {
            if (ModelState.IsValid)
            {
                ContratLocation contrat = db.ContratLocations.Where(c => c.CodeContrat == augementation.CodeContrat).First();
                if (augementation.dateAug > contrat.DateJuissance && augementation.dateAug < contrat.DateFinContrat)
                {
                    db.Augementations.Add(augementation);

                    db.SaveChanges();

                    return RedirectToAction("DetailsContrat", new { id = augementation.CodeContrat });
                }
                else
                {
                    ModelState.AddModelError("", "La date est invalide");

                    return RedirectToAction("DetailsContrat", new { id = augementation.CodeContrat });
                }
            }

            ViewBag.CodePers = new SelectList(db.ContratLocations, "CodePers", "commentaire", augementation.CodePers);
            return View(augementation);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePL([Bind(Include = "codeOp,CodePers,codeType,montant,dateOp,Commentaire")] Operation operation,
            [Bind(Include = "codeOp,CodePers,CodeLocal,CodeContrat,MoyenPaiement,Reference,DteDebut,DteFin,NbrQuittanceImprime")] PaiementLoyer paiementLoyer, int? id)
        {
            if (ModelState.IsValid)
            {

                operation.dateOp = DateTime.Now;
                operation.codeType = 1;
                operation.CodePers = CodeLocataire;
                db.Operations.Add(operation);
                paiementLoyer.CodePers = CodeLocataire;
                paiementLoyer.CodeLocal = CodeLocal;

                paiementLoyer.codeOp = operation.codeOp;
                paiementLoyer.CodeContrat = id;
                ContratLocation contrat = db.ContratLocations.Where(c => c.CodeContrat == id).First();


                if (contrat.DateFinContrat <= paiementLoyer.DteFin)
                {

                    return RedirectToAction("CreatePL", new { id = contrat.CodeContrat });

                }
                else
                {

                    contrat.LoyerNet = operation.montant;
                    db.Entry(contrat).State = EntityState.Modified;

                    db.PaiementLoyers.Add(paiementLoyer);
                    db.SaveChanges();

                    return RedirectToAction("print", new { id = operation.codeOp });
                }

            }

            ViewBag.codeOp = new SelectList(db.Depenses, "codeOp", "Description", operation.codeOp);
            ViewBag.CodePers = new SelectList(db.Personnes, "CodePers", "nom", operation.CodePers);
            ViewBag.codeType = new SelectList(db.TypeOps, "codeType", "libelle", operation.codeType);
            ViewBag.codeOp = new SelectList(db.PaiementLoyers, "codeOp", "MoyenPaiement", operation.codeOp);
            return View(operation);
        }
        public ActionResult DetailsContrat(int? id)
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
            return View(contratLocation);
        }



        public ActionResult print(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Operation op = db.Operations.Find(id);

            ViewBag.NomPre = op.Personne.nom + " " + op.Personne.prenom;
            ViewBag.CodePre = op.Personne.CodePers;

            return View(op);
        }

        // POST: Locataires/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CodePers,NumAdresse,nom,prenom,nationalite,sexe,situationMatrimonial,numPassport,cin,raisonSocial,employeur,identifiantFiscale,nPatente,rc,activite,fonction,carteSejour,TelFixPers,TelMobilPers,TelPers3,EmailPers,FaxPers,Solde,RefCodePers")] Personne personne,
              [Bind(Include = "NumAdresse,Pays,Ville,quartier,descAdresse,CPadress")] Adresse adresse)
        {
            if (ModelState.IsValid)
            {

                List<Personne> listPersonne = new List<Personne>();
                listPersonne = db.Personnes.Where(p => p.CodeTypePersonne == 2).ToList();

                bool testi = false;
                bool testid = false;
                bool testrc = false;
                bool testrs = false;
                for (int i = 0; i < listPersonne.Count(); i++)
                {
                    //cin = listPersonne[i].cin;
                    //cs = listPersonne[i].carteSejour;
                    if (listPersonne[i].cin == personne.cin && listPersonne[i].carteSejour == personne.carteSejour)
                    {
                        testi = true;

                        break;
                    }
                    else if (listPersonne[i].identifiantFiscale == personne.identifiantFiscale && !string.IsNullOrEmpty(listPersonne[i].identifiantFiscale))
                    {

                        testid = true;
                        break;

                    }
                    else if (listPersonne[i].raisonSocial == personne.raisonSocial && !string.IsNullOrEmpty(listPersonne[i].raisonSocial))
                    {
                        testrs = true;
                        break;
                    }
                    else if (listPersonne[i].rc == personne.rc && !string.IsNullOrEmpty(listPersonne[i].rc))
                    {
                        testrc = true;
                        break;
                    }

                }
                if (testi == true)
                {

                    ModelState.AddModelError("carteSejour", "Cet identifiant existe déja");
                }
                else if (testid == true)
                {
                    ModelState.AddModelError("identifiantFiscale", "Cet identifiant fiscale existe déja");
                }
                else if (testrs == true)
                {
                    ModelState.AddModelError("raisonSocial", "Cette raison sociale existe déja");
                }
                else if (testrc == true)
                {
                    ModelState.AddModelError("rc", "Ce regitre de commerce existe déja");
                }

                else if (testi == false)
                {
                    db.Adresses.Add(adresse);
                    personne.CodeTypePersonne = 2;
                    personne.Solde = 0;
                    db.Personnes.Add(personne);
                    db.SaveChanges();
                    return RedirectToAction("Details", new { id = personne.CodePers });
                }
            }

            ViewBag.NumAdresse = new SelectList(db.Adresses, "NumAdresse", "Pays", personne.NumAdresse);
            ViewBag.CodeTypePersonne = new SelectList(db.TypePersonnes, "CodeTypePersonne", "Libelle", personne.CodeTypePersonne);
            return View(personne);
        }
        public ActionResult CreatePersonne()
        {
            if (ModelState.IsValid)
            {
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePersonne([Bind(Include = "CodePers,NumAdresse,nom,prenom,nationalite,sexe,situationMatrimonial,numPassport,cin,raisonSocial,employeur,identifiantFiscale,nPatente,rc,activite,fonction,carteSejour,TelFixPers,TelMobilPers,TelPers3,EmailPers,FaxPers,Solde,RefCodePers")] PropMoraleForm personneForm,
              [Bind(Include = "NumAdresse,Pays,Ville,quartier,descAdresse,CPadress")] AdresseForm adresseForm)
        {
            List<Personne> listPersonne = new List<Personne>();
            listPersonne = db.Personnes.Where(p => p.CodeTypePersonne == 2).ToList();
            if (!string.IsNullOrEmpty(personneForm.carteSejour) && !string.IsNullOrEmpty(personneForm.nom)
                && !string.IsNullOrEmpty(personneForm.prenom) && !string.IsNullOrEmpty(personneForm.TelMobilPers)
                && !string.IsNullOrEmpty(adresseForm.descAdresse) && !string.IsNullOrEmpty(adresseForm.quartier))
            {
                bool testi = false;

                for (int i = 0; i < listPersonne.Count(); i++)
                {
                    if (listPersonne[i].cin == personneForm.cin && listPersonne[i].carteSejour == personneForm.carteSejour)
                    {
                        testi = true;
                    }
                }
                if (testi == true)
                {
                    ModelState.AddModelError("carteSejour", "Cet identifiant existe déja");
                }
                else
                {
                    Adresse adresse = new Adresse();
                    adresse.CPadress = adresseForm.CPadress;
                    adresse.descAdresse = adresseForm.descAdresse;
                    adresse.quartier = adresseForm.quartier;
                    adresse.Ville = adresseForm.Ville;
                    adresse.Pays = adresseForm.Pays;
                    db.Adresses.Add(adresse);
                    Personne personne = new Personne();
                    personne.RefCodePers = personneForm.RefCodePers;
                    personne.nom = personneForm.nom;
                    personne.prenom = personneForm.prenom;
                    personne.cin = personneForm.cin;
                    personne.carteSejour = personneForm.carteSejour;
                    personne.sexe = personneForm.sexe;
                    personne.situationMatrimonial = personneForm.situationMatrimonial;
                    personne.nationalite = personneForm.nationalite;
                    personne.TelMobilPers = personneForm.TelMobilPers;
                    personne.EmailPers = personneForm.EmailPers;
                    personne.TelFixPers = personneForm.TelFixPers;
                    personne.activite = personneForm.activite;
                    personne.fonction = personneForm.fonction;
                    personne.employeur = personneForm.employeur;

                    personne.CodeTypePersonne = 2;
                    db.Personnes.Add(personne);
                    db.SaveChanges();
                    return RedirectToAction("Details", new { id = personne.CodePers });
                }
            }

            return View(personneForm);
        }
        public ActionResult CreateEntreprise()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEntreprise([Bind(Include = "CodePers,NumAdresse,nom,prenom,nationalite,sexe,situationMatrimonial,numPassport,cin,raisonSocial,employeur,identifiantFiscale,nPatente,rc,activite,fonction,carteSejour,TelFixPers,TelMobilPers,TelPers3,EmailPers,FaxPers,Solde,RefCodePers")] PropMoraleForm personneForm,
              [Bind(Include = "NumAdresse,Pays,Ville,quartier,descAdresse,CPadress")] AdresseForm adresseForm)
        {
            List<Personne> listPersonne = new List<Personne>();
            listPersonne = db.Personnes.Where(p => p.CodeTypePersonne == 1).ToList();
            if (!string.IsNullOrEmpty(personneForm.employeur) && !string.IsNullOrEmpty(personneForm.activite)
                && !string.IsNullOrEmpty(personneForm.rc) && !string.IsNullOrEmpty(personneForm.TelFixPers)
                && !string.IsNullOrEmpty(personneForm.nom) && !string.IsNullOrEmpty(personneForm.prenom)
                && !string.IsNullOrEmpty(personneForm.TelMobilPers)
                && !string.IsNullOrEmpty(adresseForm.descAdresse) && !string.IsNullOrEmpty(adresseForm.quartier))
            {
                bool testid = false;
                bool testrc = false;
                bool testrs = false;
                bool testnp = false;
                for (int i = 0; i < listPersonne.Count(); i++)
                {
                    if (listPersonne[i].identifiantFiscale == personneForm.identifiantFiscale && !string.IsNullOrEmpty(personneForm.identifiantFiscale))
                    {
                        testid = true;
                    }
                }
                for (int i = 0; i < listPersonne.Count(); i++)
                {
                    if (listPersonne[i].raisonSocial == personneForm.raisonSocial && !string.IsNullOrEmpty(personneForm.raisonSocial))
                    {
                        testrs = true;
                    }
                }
                for (int i = 0; i < listPersonne.Count(); i++)
                {
                    if (listPersonne[i].rc == personneForm.rc && !string.IsNullOrEmpty(personneForm.rc))
                    {
                        testrc = true;
                    }
                }
                for (int i = 0; i < listPersonne.Count(); i++)
                {
                    if (listPersonne[i].nPatente == personneForm.nPatente && !string.IsNullOrEmpty(personneForm.nPatente))
                    {
                        testnp = true;
                    }
                }
                if (testid == true)
                {
                    ModelState.AddModelError("identifiantFiscale", "Cet identifiant fiscale existe déja");
                }
                if (testrs == true)
                {
                    ModelState.AddModelError("raisonSocial", "Cette raison sociale existe déja");
                }
                if (testrc == true)
                {
                    ModelState.AddModelError("rc", "Ce regitre de commerce existe déja");
                }
                if (testnp == true)
                {
                    ModelState.AddModelError("nPatente", "Ce numero de patente existe déja");
                }
                if (testid == false && testrs == false && testrc == false && testnp == false)
                {
                    Adresse adresse = new Adresse();
                    adresse.CPadress = adresseForm.CPadress;
                    adresse.descAdresse = adresseForm.descAdresse;
                    adresse.quartier = adresseForm.quartier;
                    adresse.Ville = adresseForm.Ville;
                    adresse.Pays = adresseForm.Pays;
                    db.Adresses.Add(adresse);
                    Personne personne = new Personne();
                    personne.RefCodePers = personneForm.RefCodePers;
                    personne.employeur = personneForm.employeur;
                    personne.activite = personneForm.activite;
                    personne.rc = personneForm.rc;
                    personne.identifiantFiscale = personneForm.identifiantFiscale;
                    personne.raisonSocial = personneForm.raisonSocial;
                    personne.nPatente = personneForm.nPatente;
                    personne.TelFixPers = personneForm.TelFixPers;
                    personne.FaxPers = personneForm.FaxPers;
                    personne.nom = personneForm.nom;
                    personne.prenom = personneForm.prenom;
                    personne.fonction = personneForm.fonction;
                    personne.TelMobilPers = personneForm.TelMobilPers;
                    personne.EmailPers = personneForm.EmailPers;
                    personne.CodeTypePersonne = 2;
                    personne.Solde = 0;
                    db.Personnes.Add(personne);
                    db.SaveChanges();
                    return RedirectToAction("Details", new { id = personne.CodePers });
                }
            }

            return View(personneForm);
        }
        // GET: Locataires/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Personne personne = db.Personnes.Find(id);

            AdresseForm adr = new AdresseForm();
            adr.NumAdresse = personne.Adresse.NumAdresse;
            adr.CPadress = personne.Adresse.CPadress;
            adr.descAdresse = personne.Adresse.descAdresse;
            adr.quartier = personne.Adresse.quartier;
            adr.Ville = personne.Adresse.Ville;
            adr.Pays = personne.Adresse.Pays;

            PropPhysiqueForm pers = new PropPhysiqueForm();
            pers.RefCodePers = personne.RefCodePers;
            pers.CodePers = personne.CodePers;
            pers.nom = personne.nom;
            pers.prenom = personne.prenom;
            pers.cin = personne.cin;
            pers.carteSejour = personne.carteSejour;
            pers.Sexe = personne.sexe;
            pers.SituationMatrimonial = personne.situationMatrimonial;
            pers.nationalite = personne.nationalite;
            pers.TelMobilPers = personne.TelMobilPers;
            pers.EmailPers = personne.EmailPers;
            pers.TelFixPers = personne.TelFixPers;
            pers.activite = personne.activite;
            pers.fonction = personne.fonction;
            pers.employeur = personne.employeur;
            pers.CodeTypePersonne = personne.CodeTypePersonne;
            pers.Solde = personne.Solde;
            pers.AdresseForm = adr;
            if (personne == null)
            {
                return HttpNotFound();
            }
            ViewBag.NumAdresse = new SelectList(db.Adresses, "NumAdresse", "Pays", personne.NumAdresse);
            ViewBag.CodeTypePersonne = new SelectList(db.TypePersonnes, "CodeTypePersonne", "Libelle", personne.CodeTypePersonne);
            return View(pers);
        }

        // POST: Proprietaires/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CodePers,CodeTypePersonne,NumAdresse,nom,prenom,nationalite,sexe,situationMatrimonial,cin,raisonSocial,employeur,identifiantFiscale,nPatente,rc,activite,fonction,carteSejour,TelFixPers,TelMobilPers,TelPers3,EmailPers,FaxPers,Solde,RefCodePers")] PropPhysiqueForm personneForm,
            [Bind(Include = "NumAdresse,Pays,Ville,quartier,descAdresse,CPadress")] AdresseForm adresseForm)
        {
            if (ModelState.IsValid)
            {
                Personne personne = db.Personnes.Where(c => c.CodePers == personneForm.CodePers).First();
                Adresse adresse = db.Adresses.Where(c => c.NumAdresse == adresseForm.NumAdresse).First();
                //personne.CodeTypePersonne = 1;


                //Adresse adresse = new Adresse();
                adresse.CPadress = adresseForm.CPadress;
                adresse.descAdresse = adresseForm.descAdresse;
                adresse.quartier = adresseForm.quartier;
                adresse.Ville = adresseForm.Ville;
                adresse.Pays = adresseForm.Pays;
                //Personne personne = new Personne();
                personne.RefCodePers = personneForm.RefCodePers;
                personne.nom = personneForm.nom;
                personne.prenom = personneForm.prenom;
                personne.cin = personneForm.cin;
                personne.carteSejour = personneForm.carteSejour;
                personne.sexe = personneForm.Sexe;
                personne.situationMatrimonial = personneForm.SituationMatrimonial;
                personne.nationalite = personneForm.nationalite;
                personne.TelMobilPers = personneForm.TelMobilPers;
                personne.EmailPers = personneForm.EmailPers;
                personne.TelFixPers = personneForm.TelFixPers;
                personne.activite = personneForm.activite;
                personne.fonction = personneForm.fonction;
                personne.employeur = personneForm.employeur;

                personne.CodeTypePersonne = 2;
                personne.Solde = personneForm.Solde;


                db.Entry(adresse).State = EntityState.Modified;
                db.Entry(personne).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = personne.CodePers });

            }
            ViewBag.NumAdresse = new SelectList(db.Adresses, "NumAdresse", "Pays", personneForm.NumAdresse);
            ViewBag.CodeTypePersonne = new SelectList(db.TypePersonnes, "CodeTypePersonne", "Libelle", personneForm.CodeTypePersonne);
            return View(personneForm);
        }
        public ActionResult EditEntreprise(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Personne personne = db.Personnes.Find(id);


            AdresseForm adr = new AdresseForm();
            adr.NumAdresse = personne.Adresse.NumAdresse;
            adr.CPadress = personne.Adresse.CPadress;
            adr.descAdresse = personne.Adresse.descAdresse;
            adr.quartier = personne.Adresse.quartier;
            adr.Ville = personne.Adresse.Ville;
            adr.Pays = personne.Adresse.Pays;

            PropMoraleForm pers = new PropMoraleForm();
            pers.RefCodePers = personne.RefCodePers;
            pers.CodePers = personne.CodePers;
            pers.employeur = personne.employeur;
            pers.activite = personne.activite;
            pers.rc = personne.rc;
            pers.identifiantFiscale = personne.identifiantFiscale;
            pers.raisonSocial = personne.raisonSocial;
            pers.nPatente = personne.nPatente;
            pers.TelFixPers = personne.TelFixPers;
            pers.FaxPers = personne.FaxPers;
            pers.nom = personne.nom;
            pers.prenom = personne.prenom;
            pers.fonction = personne.fonction;
            pers.TelMobilPers = personne.TelMobilPers;
            pers.EmailPers = personne.EmailPers;
            pers.CodeTypePersonne = 2;

            pers.AdresseForm = adr;
            if (personne == null)
            {
                return HttpNotFound();
            }
            ViewBag.NumAdresse = new SelectList(db.Adresses, "NumAdresse", "Pays", personne.NumAdresse);
            ViewBag.CodeTypePersonne = new SelectList(db.TypePersonnes, "CodeTypePersonne", "Libelle", personne.CodeTypePersonne);
            return View(pers);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEntreprise([Bind(Include = "CodePers,CodeTypePersonne,NumAdresse,nom,prenom,nationalite,sexe,situationMatrimonial,cin,raisonSocial,employeur,identifiantFiscale,nPatente,rc,activite,fonction,carteSejour,TelFixPers,TelMobilPers,TelPers3,EmailPers,FaxPers,Solde,RefCodePers")] PropMoraleForm personneForm,
            [Bind(Include = "NumAdresse,Pays,Ville,quartier,descAdresse,CPadress")] AdresseForm adresseForm)
        {
            //if (ModelState.IsValid)
            //{

            Personne personne = db.Personnes.Where(c => c.CodePers == personneForm.CodePers).First();
            Adresse adresse = db.Adresses.Where(c => c.NumAdresse == adresseForm.NumAdresse).First();
            //personne.CodeTypePersonne = 1;


            //Adresse adresse = new Adresse();
            adresse.CPadress = adresseForm.CPadress;
            adresse.descAdresse = adresseForm.descAdresse;
            adresse.quartier = adresseForm.quartier;
            adresse.Ville = adresseForm.Ville;
            adresse.Pays = adresseForm.Pays;
            //Personne personne = new Personne();

            personne.RefCodePers = personneForm.RefCodePers;
            personne.employeur = personneForm.employeur;
            personne.activite = personneForm.activite;
            personne.rc = personneForm.rc;
            personne.identifiantFiscale = personneForm.identifiantFiscale;
            personne.raisonSocial = personneForm.raisonSocial;
            personne.nPatente = personneForm.nPatente;
            personne.TelFixPers = personneForm.TelFixPers;
            personne.FaxPers = personneForm.FaxPers;
            personne.nom = personneForm.nom;
            personne.prenom = personneForm.prenom;
            personne.fonction = personneForm.fonction;
            personne.TelMobilPers = personneForm.TelMobilPers;
            personne.EmailPers = personneForm.EmailPers;
            personne.CodeTypePersonne = 2;



            db.Entry(adresse).State = EntityState.Modified;
            db.Entry(personne).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Details", new { id = personne.CodePers });

            //}
            //ViewBag.NumAdresse = new SelectList(db.Adresses, "NumAdresse", "Pays", personneForm.NumAdresse);
            //ViewBag.CodeTypePersonne = new SelectList(db.TypePersonnes, "CodeTypePersonne", "Libelle", personneForm.CodeTypePersonne);
            //return View(personneForm);
        }
        public ActionResult DeleteAug(int id)
        {
            Augementation augementation = db.Augementations.Find(id);
            int codec = augementation.CodeContrat.Value;
            db.Augementations.Remove(augementation);
            db.SaveChanges();
            return RedirectToAction("DetailsContrat", new { id = codec });
        }
        public ActionResult DeletePLoc(int? id)
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
            return View(operation);
        }

        [HttpPost, ActionName("DeletePLoc")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedPLoc(int id)
        {
            Operation operation = db.Operations.Find(id);
            PaiementLoyer paiementLoyer = db.PaiementLoyers.Find(id);
            db.Operations.Remove(operation);
            db.PaiementLoyers.Remove(paiementLoyer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }









        // GET: Locataires/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personne personne = db.Personnes.Find(id);
            if (personne == null)
            {
                return HttpNotFound();
            }
            return View(personne);
        }

        // POST: Locataires/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Personne personne = db.Personnes.Find(id);
        //    db.Personnes.Remove(personne);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        public ActionResult DeleteConfirmed(int id)
        {
            Personne personne = db.Personnes.Find(id);
            Adresse adresse = db.Adresses.Find(personne.NumAdresse);
            db.Personnes.Remove(personne);
            db.Adresses.Remove(adresse);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteContrat(int? id)
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
        [HttpPost, ActionName("DeleteContrat")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteContratConfirmed(int id)
        {
            ContratLocation contratLocation = db.ContratLocations.Where(c => c.CodeContrat == id).First();
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
