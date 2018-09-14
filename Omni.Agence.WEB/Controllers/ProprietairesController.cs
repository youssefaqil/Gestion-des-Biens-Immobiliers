using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Omni.Agence.DAL;
using Subgurim.Controles;
using Omni.Agence.WEB.Models;
using Omni.Agence.WEB.Controllers;

namespace Omni.Agence.Web.Controllers
{
     public class ProprietairesController : ControllerAuth
    {
        private AgenceEntities db = new AgenceEntities();
        private int variable;
        private static int CodePersonne;
        private static double soldeprop;
        // GET: Proprietaires
        public ActionResult Index()
        {
            var personnes = db.Personnes.Include(p => p.Adresse).Include(p => p.TypePersonne).Where(p => p.CodeTypePersonne.Equals(1));
            return View(personnes.ToList());
        }
        public ActionResult IndexM()
        {
            var personnes = db.Personnes.Include(p => p.Adresse).Include(p => p.TypePersonne).Where(p => p.CodeTypePersonne.Equals(1));
            return View(personnes.ToList());
        }

        // GET: Proprietaires/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.idProp = id;

            Personne personne = db.Personnes.Find(id);
            if (personne.sexe==0){
                ViewBag.sexe = "Femme";
            }
            else if(personne.sexe==1) { 
                ViewBag.sexe = "Homme";
            }
            ViewBag.nbrIm=  db.Immeubles.Where(p => p.CodePers == id).Count();
            ViewBag.nbrLc= db.Locals.Where(p => p.CodePers == id).Count();
            variable = personne.Adresse.NumAdresse;
            if (personne == null)
            {
                return HttpNotFound();
            }
            return View(personne);
        }

        public ActionResult Acceuil()
        {
            return View();
        }


        public ActionResult CreateProprietaire()
        {
            return View();
        }

        //public ActionResult CreatePropForm()
        //{

        //    return View();
        //}
        public ActionResult CreateProp()
        {
            return View();
        }
        //[HttpPost]
        ////public ActionResult CreatePP(String cinP,String carteSejourP,String nomP,String prenomP,String sexeP,String situationMatrimonialP,String nationaliteP,String TelMobilPersP,String EmailPersP ,String TelFixPersP,String activiteP,String fonctionP,String employeurP[Bind(Include = "CodePersP,NumAdresse,nomP,prenomP,nationaliteP,sexeP,situationMatrimonialP,numPassportP,cinP,raisonSocialP,employeurP,identifiantFiscaleP,nPatenteP,rcP,activiteP,fonctionP,carteSejourP,TelFixPersP,TelMobilPersP,TelPers3P,EmailPersP,FaxPersP,soldeP")] ProprietaireForm propForm,
        ////      [Bind(Include = "NumAdresse,Pays,Ville,quartier,descAdresse,CPadress")] AdresseForm adresseForm)
        //public ActionResult CreatePP(
        //    [Bind(Include = "CodePers,NumAdresse,nom,prenom,nationalite,sexe,situationMatrimonial,numPassport,cin,raisonSocial,employeur,identifiantFiscale,nPatente,rc,activite,fonction,carteSejour,TelFixPers,TelMobilPers,TelPers3,EmailPers,FaxPers,solde")] Personne personneF,
        //    String CIN, String carteSejour, String nom, String prenom,
        //    int? sexe, String situationMatrimonial, String nationalite, String TelMobilPers, String EmailPers,
        //    String TelFixPers, String activite, String fonction, String employeur, int? CPadress, String descAdresse, String quartier, String Ville, String Pays)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        ViewBag.carteSejour = personneF.carteSejour;
        //        ViewBag.CIN = CIN;
        //        ViewBag.nom = nom;
        //        ViewBag.prenom = prenom;
        //        Personne personne = new Personne();
        //        Adresse adresse = new Adresse();
        //        List<Personne> listPersonne = new List<Personne>();
        //        listPersonne = db.Personnes.Where(p => p.CodeTypePersonne == 1).ToList();
        //        bool testi = false;
        //        for (int i = 0; i < listPersonne.Count(); i++)
        //        {
        //            if (listPersonne[i].cin == CIN && listPersonne[i].carteSejour == personneF.carteSejour)
        //            {
        //                testi = true;
        //            }
        //        }
        //        if (testi == true)
        //        {
        //            ModelState.AddModelError("carteSejour", "Cet identifiant existe déja");
        //        }
        //        else if (testi == false)
        //        {
        //            adresse.CPadress = CPadress;
        //            adresse.descAdresse = descAdresse;
        //            adresse.quartier = quartier;
        //            adresse.Ville = Ville;
        //            adresse.Pays = Pays;
        //            db.Adresses.Add(adresse);
        //            db.SaveChanges();

        //            personne.NumAdresse = adresse.NumAdresse;
        //            personne.cin = CIN;
        //            personne.carteSejour = carteSejour;
        //            personne.nom = nom;
        //            personne.prenom = prenom;
        //            personne.sexe = sexe;
        //            personne.situationMatrimonial = situationMatrimonial;
        //            personne.nationalite = nationalite;
        //            personne.TelMobilPers = TelMobilPers;
        //            personne.EmailPers = EmailPers;
        //            personne.TelFixPers = TelFixPers;
        //            personne.activite = activite;
        //            personne.fonction = fonction;
        //            personne.employeur = employeur;

        //            personne.CodeTypePersonne = 1;
        //            personne.solde = 0;
        //            db.Personnes.Add(personne);
        //            db.SaveChanges();
        //            return RedirectToAction("Details", new { id = personne.CodePers });
        //        }
        //    }
            
        //    return RedirectToAction("CreateProprietaire");
        //}

        [HttpPost]
        public ActionResult CreateProprietaire(
            [Bind(Include = "CodePers,NumAdresse,nom,prenom,nationalite,sexe,situationMatrimonial,numPassport,cin,raisonSocial,employeur,identifiantFiscale,nPatente,rc,activite,fonction,carteSejour,TelFixPers,TelMobilPers,TelPers3,EmailPers,FaxPers,Solde")] Personne personneF,
            String CIN, String carteSejour, String nom, String prenom, String rcM,
            int? sexe, String situationMatrimonial, String nationalite, String TelMobilPers, String EmailPers,
            String TelFixPers, String activite, String fonction, String employeur, int? CPadress, String descAdresse, String quartier, String Ville, String Pays)
        {
            if (ModelState.IsValid)
            {
                if(rcM==null){
                    ViewBag.carteSejour = personneF.carteSejour;
                    ViewBag.CIN = CIN;
                    ViewBag.nom = nom;
                    ViewBag.prenom = prenom;
                    Personne personne = new Personne();
                    Adresse adresse = new Adresse();
                    List<Personne> listPersonne = new List<Personne>();
                    listPersonne = db.Personnes.Where(p => p.CodeTypePersonne == 1).ToList();
                    bool testi = false;
                    for (int i = 0; i < listPersonne.Count(); i++)
                    {
                        if (listPersonne[i].cin == CIN && listPersonne[i].carteSejour == personneF.carteSejour)
                        {
                            testi = true;
                        }
                    }
                    if (testi == true)
                    {
                        ModelState.AddModelError("carteSejour", "Cet identifiant existe déja");
                        return RedirectToAction("CreateProprietaire");
                    }
                    else if (testi == false)
                    {
                        adresse.CPadress = CPadress;
                        adresse.descAdresse = descAdresse;
                        adresse.quartier = quartier;
                        adresse.Ville = Ville;
                        adresse.Pays = Pays;
                        db.Adresses.Add(adresse);
                        db.SaveChanges();

                        personne.NumAdresse = adresse.NumAdresse;
                        personne.cin = CIN;
                        personne.carteSejour = carteSejour;
                        personne.nom = nom;
                        personne.prenom = prenom;
                        personne.sexe = sexe;
                        personne.situationMatrimonial = situationMatrimonial;
                        personne.nationalite = nationalite;
                        personne.TelMobilPers = TelMobilPers;
                        personne.EmailPers = EmailPers;
                        personne.TelFixPers = TelFixPers;
                        personne.activite = activite;
                        personne.fonction = fonction;
                        personne.employeur = employeur;

                        personne.CodeTypePersonne = 1;
                        personne.Solde = 0;
                        db.Personnes.Add(personne);
                        db.SaveChanges();
                        return RedirectToAction("Details", new { id = personne.CodePers });
                    }
                }
                
            }
            return RedirectToAction("");
        }

        [HttpPost]
        public ActionResult CreatePM(String employeurM, String activiteM, String nomM, String prenomM,
            String rcM, String idFiscaleM, String rsM, String patenteM, String fixM,
            String faxM, String fonctionM, String mobileM, String emailM, int? cpM, String adresseM, String quartierM, String VilleM, String PaysM)
        {
            if (ModelState.IsValid)
            {
                Personne personne = new Personne();
                Adresse adresse = new Adresse();
                List<Personne> listPersonne = new List<Personne>();
                listPersonne = db.Personnes.Where(p => p.CodeTypePersonne == 1).ToList();
                bool testi = false;
                bool testid = false;
                bool testrc = false;
                bool testrs = false;
                for (int i = 0; i < listPersonne.Count(); i++)
                {
                    //cin = listPersonne[i].cin;
                    //cs = listPersonne[i].carteSejour;
                    
                     if (listPersonne[i].identifiantFiscale == idFiscaleM && !string.IsNullOrEmpty(listPersonne[i].identifiantFiscale))
                    {
                        testid = true;
                        break;
                    }
                    else if (listPersonne[i].raisonSocial == rsM && !string.IsNullOrEmpty(listPersonne[i].raisonSocial))
                    {
                        testrs = true;
                        break;
                    }
                    else if (listPersonne[i].rc == rcM && !string.IsNullOrEmpty(listPersonne[i].rc))
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
                    adresse.CPadress = cpM;
                    adresse.descAdresse = adresseM;
                    adresse.quartier = quartierM;
                    adresse.Ville = VilleM;
                    adresse.Pays = PaysM;
                    db.Adresses.Add(adresse);
                    db.SaveChanges();

                    personne.NumAdresse = adresse.NumAdresse;
                    personne.identifiantFiscale = idFiscaleM;
                    personne.raisonSocial = rsM;
                    personne.nom = nomM;
                    personne.prenom = prenomM;
                    personne.nPatente = patenteM;
                    personne.TelFixPers = fixM;
                    personne.FaxPers = faxM;
                    personne.TelMobilPers = mobileM;
                    personne.EmailPers = emailM;
                    
                    personne.activite = activiteM;
                    personne.fonction = fonctionM;
                    personne.employeur = employeurM;

                    personne.CodeTypePersonne = 1;
                    personne.Solde = 0;
                    db.Personnes.Add(personne);
                    db.SaveChanges();
                    return RedirectToAction("Details", new { id = personne.CodePers });
                }
            }
            
            return RedirectToAction("CreateProprietaire");
        }



       
        // GET: Proprietaires/Create
        public ActionResult Create()
        {
            ViewBag.NumAdresse = new SelectList(db.Adresses, "NumAdresse", "Pays");
            ViewBag.CodeTypePersonne = new SelectList(db.TypePersonnes, "CodeTypePersonne", "Libelle");
            return View();
        }

        // POST: Proprietaires/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CodePers,NumAdresse,nom,prenom,nationalite,sexe,situationMatrimonial,numPassport,cin,raisonSocial,employeur,identifiantFiscale,nPatente,rc,activite,fonction,carteSejour,TelFixPers,TelMobilPers,TelPers3,EmailPers,FaxPers,Solde,RefCodePers")] Personne personne,
              [Bind(Include = "NumAdresse,Pays,Ville,quartier,descAdresse,CPadress")] Adresse adresse)
        {
            if (ModelState.IsValid)
            {
                if (personne.rc == null) {
                    if (string.IsNullOrEmpty(personne.carteSejour))
                    {
                        ModelState.AddModelError("carteSejour", "Ce Champ est Obligatoire");
                    }
                    if(string.IsNullOrEmpty(personne.nom)){
                        ModelState.AddModelError("nom", "Ce Champ est Obligatoire");
                    }
                    if (string.IsNullOrEmpty(personne.prenom))
                    {
                        ModelState.AddModelError("prenom", "Ce Champ est Obligatoire");
                    }
                    if (string.IsNullOrEmpty(personne.TelMobilPers))
                    {
                        ModelState.AddModelError("TelMobilPers", "Ce Champ est Obligatoire");
                    }
                    if (string.IsNullOrEmpty(adresse.quartier))
                    {
                        ModelState.AddModelError("quartier", "Cet identifiant existe déja");
                    }
                    if (string.IsNullOrEmpty(adresse.descAdresse))
                    {
                        ModelState.AddModelError("descAdresse", "Cet identifiant existe déja");
                    }

                    List<Personne> listPersonne = new List<Personne>();
                    listPersonne = db.Personnes.Where(p => p.CodeTypePersonne == 1).ToList();
                    
                    bool testi = false;
                    bool testid = false;
                    bool testrc = false;
                    bool testrs = false;
                   
                        for (int i = 0; i < listPersonne.Count(); i++)
                        {                           
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
                        personne.CodeTypePersonne = 1;
                        personne.Solde = 0;
                        db.Personnes.Add(personne);
                        db.SaveChanges();
                        return RedirectToAction("Details", new { id = personne.CodePers });
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(personne.employeur))
                    {
                        ModelState.AddModelError("employeur", "Ce Champ est Obligatoire");
                    }
                    if (string.IsNullOrEmpty(personne.activite))
                    {
                        ModelState.AddModelError("activite", "Ce Champ est Obligatoire");
                    }
                    if (string.IsNullOrEmpty(personne.rc))
                    {
                        ModelState.AddModelError("rc", "Ce Champ est Obligatoire");
                    }
                    if (string.IsNullOrEmpty(personne.TelFixPers))
                    {
                        ModelState.AddModelError("TelFixPers", "Ce Champ est Obligatoire");
                    }
                    if (string.IsNullOrEmpty(personne.nom))
                    {
                        ModelState.AddModelError("nom", "Ce Champ est Obligatoire");
                    }
                    if (string.IsNullOrEmpty(personne.prenom))
                    {
                        ModelState.AddModelError("prenom", "Ce Champ est Obligatoire");
                    }
                    if (string.IsNullOrEmpty(personne.TelMobilPers))
                    {
                        ModelState.AddModelError("TelMobilPers", "Ce Champ est Obligatoire");
                    }
                    if (string.IsNullOrEmpty(adresse.quartier))
                    {
                        ModelState.AddModelError("quartier", "Cet identifiant existe déja");
                    }
                    if (string.IsNullOrEmpty(adresse.descAdresse))
                    {
                        ModelState.AddModelError("descAdresse", "Cet identifiant existe déja");
                    }
                    List<Personne> listPersonne = new List<Personne>();
                    listPersonne = db.Personnes.Where(p => p.CodeTypePersonne == 1).ToList();

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
                        personne.CodeTypePersonne = 1;
                        personne.Solde = 0;
                        db.Personnes.Add(personne);
                        db.SaveChanges();
                        return RedirectToAction("Details", new { id = personne.CodePers });
                    }


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
        public ActionResult CreatePersonne([Bind(Include = "CodePers,NumAdresse,nom,prenom,nationalite,sexe,situationMatrimonial,numPassport,cin,raisonSocial,employeur,identifiantFiscale,nPatente,rc,activite,fonction,carteSejour,TelFixPers,TelMobilPers,TelPers3,EmailPers,FaxPers,Solde,RefCodePers")] PropPhysiqueForm personneForm,
              [Bind(Include = "NumAdresse,Pays,Ville,quartier,descAdresse,CPadress")] AdresseForm adresseForm)
        {
            try
            {

            
            List<Personne> listPersonne = new List<Personne>();
            listPersonne = db.Personnes.Where(p => p.CodeTypePersonne == 1).ToList();
            if (!string.IsNullOrEmpty(personneForm.carteSejour) && !string.IsNullOrEmpty(personneForm.nom)
                && !string.IsNullOrEmpty(personneForm.prenom) && !string.IsNullOrEmpty(personneForm.TelMobilPers)
                && !string.IsNullOrEmpty(adresseForm.descAdresse)&& !string.IsNullOrEmpty(adresseForm.quartier))
            {
                bool testi = false;

                //for (int i = 0; i < listPersonne.Count(); i++)
                //{
                //    if (listPersonne[i].cin == personneForm.cin && listPersonne[i].carteSejour == personneForm.carteSejour)
                //    {
                //        testi = true;
                //    }
                //}
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
                    // ajouté car ne fct pas en local
                    db.SaveChanges();
                    Personne personne = new Personne();
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
                    // ajouté car ne fct pas en local
                    personne.NumAdresse = adresse.NumAdresse;
                    personne.CodeTypePersonne = 1;
                    personne.Solde = 0;
                    db.Personnes.Add(personne);
                    db.SaveChanges();
                    return RedirectToAction("Details", new { id = personne.CodePers });
                }
            }
            }
            catch
            {
                ModelState.AddModelError("carteSejour", "Cet identifiant existe déja");
                return View(personneForm);
            }
            //if (string.IsNullOrEmpty(personne.carteSejour))
            //{
            //    ModelState.AddModelError("carteSejour", "Ce Champ est Obligatoire");
            //}
            //if (string.IsNullOrEmpty(personne.nom)){
            //    ModelState.AddModelError("nom", "Ce Champ est Obligatoire");
            //}
            //if (string.IsNullOrEmpty(personne.prenom))
            //{
            //    ModelState.AddModelError("prenom", "Ce Champ est Obligatoire");
            //}
            //if (string.IsNullOrEmpty(personne.TelMobilPers))
            //{
            //    ModelState.AddModelError("TelMobilPers", "Ce Champ est Obligatoire");
            //}
            // if (string.IsNullOrEmpty(adresse.descAdresse))
            //{
            //    ModelState.AddModelError("Adresse.descAdresse", "Ce Champ est Obligatoire");
            //}
            //if (string.IsNullOrEmpty(adresse.quartier))
            //{
            //    ModelState.AddModelError("Adresse.quartier", "Ce Champ est Obligatoire");
            //}
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
                    }}
                for (int i = 0; i < listPersonne.Count(); i++)
                {
                    if (listPersonne[i].raisonSocial == personneForm.raisonSocial && !string.IsNullOrEmpty(personneForm.raisonSocial))
                    {
                        testrs = true;
                    }}
                for (int i = 0; i < listPersonne.Count(); i++)
                {
                    if (listPersonne[i].rc == personneForm.rc && !string.IsNullOrEmpty(personneForm.rc))
                    {
                        testrc = true;
                    }}
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
                 if (testid == false && testrs==false && testrc == false && testnp == false) 
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
                    personne.CodeTypePersonne = 1;
                    personne.Solde = 0;
                    db.Personnes.Add(personne);
                    db.SaveChanges();
                    return RedirectToAction("Details", new { id = personne.CodePers });
                }
            }
            if (string.IsNullOrEmpty(personneForm.nom))
            {
                ModelState.AddModelError("nom", "Ce Champ est Obligatoire");
            }
            if (string.IsNullOrEmpty(personneForm.prenom))
            {
                ModelState.AddModelError("prenom", "Ce Champ est Obligatoire");
            }
            if (string.IsNullOrEmpty(personneForm.TelMobilPers))
            {
                ModelState.AddModelError("TelMobilPers", "Ce Champ est Obligatoire");
            }
            if (string.IsNullOrEmpty(personneForm.employeur))
            {
                ModelState.AddModelError("employeur", "Ce Champ est Obligatoire");
            }
            if (string.IsNullOrEmpty(personneForm.activite))
            {
                ModelState.AddModelError("activite", "Ce Champ est Obligatoire");
            }
            if (string.IsNullOrEmpty(personneForm.rc))
            {
                ModelState.AddModelError("rc", "Ce Champ est Obligatoire");
            }
            if (string.IsNullOrEmpty(personneForm.TelFixPers))
            {
                ModelState.AddModelError("TelFixPers", "Ce Champ est Obligatoire");
            }
            if (string.IsNullOrEmpty(adresseForm.descAdresse))
            {
                ModelState.AddModelError("Adresse.descAdresse", "Ce Champ est Obligatoire");
            }
            if (string.IsNullOrEmpty(adresseForm.quartier))
            {
                ModelState.AddModelError("Adresse.quartier", "Ce Champ est Obligatoire");
            }
            return View(personneForm);
        }
        public ActionResult DetailsOP(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Operation operation=db.Operations.Find(id);
            
            ViewBag.nomproprietaire = operation.Personne.nom;
            ViewBag.prenomproprietaire = operation.Personne.prenom;
            return View(operation);
        }



        public ActionResult EditImm(int? id)
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
        public ActionResult EditImm([Bind(Include = "CodeImmeuble,NumAdresse,NumConcierge,NomImmeuble,ServiceImmeuble,NbrEtage,CodePers,Parking,Jardin,Interphone,Description,RefCodeImm")] Immeuble immeuble,
            [Bind(Include = "NumAdresse,Pays,Ville,quartier,descAdresse,CPadress")] Adresse adresse,
            [Bind(Include = "NumConcierge,NomConci,PrenomConci,CinConci,SalaireConci,TelConci,CPadress")] Concierge concierge, int? id)
        {
            if (ModelState.IsValid)
            {
                //db.Adresses.Add(adresse);
                db.Entry(adresse).State = EntityState.Modified;
                db.Entry(concierge).State = EntityState.Modified;
                db.Entry(immeuble).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("DetailsImmeuble", new { id = immeuble.CodeImmeuble });
            }
            ViewBag.NumAdresse = new SelectList(db.Adresses, "NumAdresse", "Pays", immeuble.NumAdresse);
            ViewBag.NumConcierge = new SelectList(db.Concierges, "NumConcierge", "NomConci", immeuble.NumConcierge);
            ViewBag.CodePers = new SelectList(db.Personnes, "CodePers", "nom", immeuble.CodePers);
            return View(immeuble);
        }
        public ActionResult EditLoc(int? id)
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditLoc([Bind(Include = "CodeLocal,NumTypeLocal,NumAdresse,CodeImmeuble,CodePers,CodeSynd,SuperficieLocal,NbrPieceLocal,NbrChambreLocal,TitreLocal,DescrLocal,MeubleLocal,PrixRefLocationLocal,ServiceLocal,Staut,rezCh,Interphone,Parking,Garage,Jardin,DateDDispo,DateFDispo,NumLocal,RefCodePers")] Local local,
            [Bind(Include = "NumAdresse,Pays,Ville,quartier,descAdresse,CPadress")] Adresse adresse)
        {
            if (ModelState.IsValid)
            {

                db.Entry(adresse).State = EntityState.Modified;
                db.Entry(local).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DetailsLocal", new { id = local.CodeLocal });
            }
            ViewBag.NumAdresse = new SelectList(db.Adresses, "NumAdresse", "Pays", local.NumAdresse);
            ViewBag.CodeImmeuble = new SelectList(db.Immeubles, "CodeImmeuble", "NomImmeuble", local.CodeImmeuble);
            ViewBag.CodePers = new SelectList(db.Personnes, "CodePers", "nom", local.CodePers);
            ViewBag.CodeSynd = new SelectList(db.Syndics, "CodeSynd", "NomSynd", local.CodeSynd);
            ViewBag.NumTypeLocal = new SelectList(db.TypeLocals, "NumTypeLocal", "Description", local.NumTypeLocal);
            return View(local);
        }

        public ActionResult CreatePaiementProp(int? id)
        {
            ViewBag.codeOp = new SelectList(db.Depenses, "codeOp", "Description");
            ViewBag.CodePers = new SelectList(db.Personnes, "CodePers", "nom");
            ViewBag.codeType = new SelectList(db.TypeOps, "codeType", "libelle");
            ViewBag.codeOp = new SelectList(db.PaiementLoyers, "codeOp", "MoyenPaiement");
            Personne pers = db.Personnes.Find(id);
            ViewBag.CodeProp = id;
            ViewBag.nomproprietaire = pers.nom;
            ViewBag.prenomproprietaire = pers.prenom;

            return View();
        }

        // POST: Operations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ActionName("compteProp")]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePaiementProp([Bind(Include = "codeOp,CodePers,codeType,montant,dateOp,Commentaire")] Operation operation)
        {
            if (ModelState.IsValid)
            {
                operation.CodePers = CodePersonne;
                operation.codeType = 2;
                operation.dateOp = DateTime.Now;
                db.Operations.Add(operation);


                Personne personne = db.Personnes.Find(CodePersonne);
                personne.Solde = soldeprop-operation.montant;
                db.Entry(personne).State = EntityState.Modified;
                
                db.SaveChanges();

                return RedirectToAction("print", new { id = operation.codeOp });
            }

            ViewBag.codeOp = new SelectList(db.Depenses, "codeOp", "Description", operation.codeOp);
            ViewBag.CodePers = new SelectList(db.Personnes, "CodePers", "nom", operation.CodePers);
            ViewBag.codeType = new SelectList(db.TypeOps, "codeType", "libelle", operation.codeType);
            ViewBag.codeOp = new SelectList(db.PaiementLoyers, "codeOp", "MoyenPaiement", operation.codeOp);
            return View(operation);
        }
        public ActionResult print(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Operation op = db.Operations.Find(id);
            ViewBag.CodeProp = op.Personne.CodePers;
            ViewBag.nomproprietaire=op.Personne.nom;
            ViewBag.prenomproprietaire = op.Personne.prenom;

            return View(op);
        }

        public ActionResult compteProp(int? id) {
            //var paiementLoyers = db.Operations.Include(o => o.Depense).Include(o => o.Personne).Include(o => o.TypeOp).Include(o => o.PaiementLoyer).Where(p => p.PaiementLoyer.ContratLocation.Local.CodePers == id);
            //var depenses = db.Operations.Include(o => o.Depense).Include(o => o.Personne).Include(o => o.TypeOp).Include(o => o.PaiementLoyer).Where(p => p.Depense.Local.CodePers == id);
            ////var paiementProp=db.Operations
            var query = from r in db.Operations.Include(o => o.Depense).Include(o => o.Personne).Include(o => o.TypeOp).Include(o => o.PaiementLoyer)
                        where r.PaiementLoyer.ContratLocation.Local.CodePers == id ||
                              r.Depense.Local.CodePers == id ||
                              r.Depense.Immeuble.CodePers == id ||
                              (r.codeType==2 && r.CodePers==id && r.Personne.CodeTypePersonne==1)
                        select r;
            ViewBag.CodeProp = id;

            double SoldePL=0;
            double SoldeD = 0;
            CodePersonne = id.Value;
            List<Operation> Lop = query.Where(p => p.codeType == 1).ToList();
            double serv = 0;
            int servpc = 0;
            for (int i = 0; i < Lop.Count(); i++)
            {
                //Lop[i].montant = Lop[i].montant.Value - (SoldePL + Lop[i].montant.Value * (Lop[i].PaiementLoyer.ContratLocation.Local.ServiceLocal.Value)) / 100;
                servpc = Lop[i].PaiementLoyer.ContratLocation.Local.ServiceLocal.Value;
                serv = Convert.ToDouble(servpc);

                //Lop[i].montant = Lop[i].PaiementLoyer.ContratLocation.LoyerDebase - Lop[i].PaiementLoyer.ContratLocation.LoyerDebase*serv/100;
                Lop[i].montant = Lop[i].montant *
                    (100-(Lop[i].PaiementLoyer.ContratLocation.TaxeDedilite +
                    Lop[i].PaiementLoyer.ContratLocation.Charge +
                    Lop[i].PaiementLoyer.ContratLocation.Local.ServiceLocal)) / 100;
                SoldePL = SoldePL + Lop[i].montant.Value;
            }

            List<Operation> LopD = query.Where(p => p.codeType == 2 || p.codeType == 3).ToList();

            for (int i = 0; i < LopD.Count(); i++)
            {
                SoldeD = SoldeD + LopD[i].montant.Value;
            }
            Personne pers=db.Personnes.Find(id);
            ViewBag.pers = pers;
            //ViewBag.nomproprietaire = pers.nom;
            //ViewBag.prenomproprietaire = pers.prenom;

            //soldeprop = int.Parse(SoldePL - SoldeD);
            soldeprop = SoldePL - SoldeD;
            ViewBag.SoldeProp = soldeprop;


            //db.Entry(pers).State = EntityState.Modified;
            //db.SaveChanges();
            
                //List<Operation> paiementLoyersList = paiementLoyers.ToList();
                //List<Operation> depensesList = depenses.ToList();
                ////List<Operation> paiementPrp=
                return View(query.ToList());
        }


        public ActionResult ImmeublesProp(int? id) {

            var immeubles = db.Immeubles.Include(i => i.Adresse).Include(i => i.Concierge).Include(i => i.Personne).Where(i => i.CodePers == id);
            ViewBag.codeprp = id;
            
            Personne pers = db.Personnes.Find(id);
            
            ViewBag.codeprp = pers.CodePers;
            ViewBag.nomprp = pers.nom;
            ViewBag.prenomprp = pers.prenom;
                
            return View(immeubles.ToList());
        }

        public ActionResult LocalProp(int? id) {
            var locals = db.Locals.Include(l => l.Adresse).Include(l => l.Immeuble).Include(l => l.Personne).Include(l => l.Syndic).Include(l => l.TypeLocal).Where(i => i.CodePers == id);
            Personne pers = db.Personnes.Find(id);
            ViewBag.codeprp = pers.CodePers;
            ViewBag.nomprp = pers.nom;
            ViewBag.prenomprp = pers.prenom;
            return View(locals.ToList());
        }

        public ActionResult CreateLocalProp(int? id) {
            ViewBag.NumAdresse = new SelectList(db.Adresses, "NumAdresse", "Pays");
            ViewBag.CodeImmeuble = new SelectList(db.Immeubles, "CodeImmeuble", "NomImmeuble");
            ViewBag.CodePers = new SelectList(db.Personnes.Where(p => p.CodeTypePersonne == 1), "CodePers", "nom");
            ViewBag.CodeSynd = new SelectList(db.Syndics, "CodeSynd", "NomSynd");
            ViewBag.NumTypeLocal = new SelectList(db.TypeLocals, "NumTypeLocal", "Description");
            var locals = db.Locals.Include(l => l.Adresse).Include(l => l.Immeuble).Include(l => l.Personne).Include(l => l.Syndic).Include(l => l.TypeLocal).Where(i => i.CodePers == id);
            ViewBag.listLocals = locals.ToList();


            Personne pers = db.Personnes.Find(id);

            ViewBag.Pers = pers;
            //ViewBag.nomprp = pers.nom;
            //ViewBag.prenomprp = pers.prenom;
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult CreateLocalProp([Bind(Include = "CodeLocal,NumTypeLocal,NumAdresse,CodeImmeuble,CodePers,CodeSynd,SuperficieLocal,NbrPieceLocal,NbrChambreLocal,TitreLocal,DescrLocal,MeubleLocal,PrixRefLocationLocal,ServiceLocal,Staut,rezCh,Interphone,Parking,Garage,Jardin,DateDDispo,DateFDispo,NumLocal")] Local local,
             [Bind(Include = "NumAdresse,Pays,Ville,quartier,descAdresse,CPadress")] Adresse adresse, int? id)
        {
            // Boucle (if) commentée car l'ajout d'un local sur server ne passe pas !!
            //if (ModelState.IsValid)
            //{
                local.CodePers = id;
                db.Adresses.Add(adresse);
                local.Staut = 0;
                db.Locals.Add(local);
                db.SaveChanges();
                return RedirectToAction("DetailsLocal", new { id = local.CodeLocal });
            //}

            //ViewBag.NumAdresse = new SelectList(db.Adresses, "NumAdresse", "Pays", local.NumAdresse);
            //ViewBag.CodeImmeuble = new SelectList(db.Immeubles, "CodeImmeuble", "NomImmeuble", local.CodeImmeuble);
            //ViewBag.CodePers = new SelectList(db.Personnes, "CodePers", "nom", local.CodePers);
            //ViewBag.CodeSynd = new SelectList(db.Syndics, "CodeSynd", "NomSynd", local.CodeSynd);
            //ViewBag.NumTypeLocal = new SelectList(db.TypeLocals, "NumTypeLocal", "Description", local.NumTypeLocal);
            //return View(local);
        }




        public ActionResult CreateImmeublesProp(int? id)
        {
            ViewBag.NumAdresse = new SelectList(db.Adresses, "NumAdresse", "Pays");
            ViewBag.NumConcierge = new SelectList(db.Concierges, "NumConcierge", "NomConci");

            Personne pers = db.Personnes.Find(id);
            ViewBag.pers = pers;
            List<Immeuble> listImm = db.Immeubles.Where(c => c.CodePers == id).ToList();
            ViewBag.listImm = listImm;
            ViewBag.codeprp = pers.CodePers;
            ViewBag.nomprp = pers.nom;
            ViewBag.prenomprp = pers.prenom;

            //ViewBag.CodePers = new SelectList(db.Personnes, "CodePers", "nom");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateImmeublesProp(
            [Bind(Include = "CodeImmeuble,NumAdresse,NumConcierge,NomImmeuble,ServiceImmeuble,NbrEtage,CodePers,Parking,Jardin,Description,Interphone,RefCodeImm")] Immeuble immeuble,
            [Bind(Include = "NumAdresse,Pays,Ville,quartier,descAdresse,CPadress")] Adresse adresse,
            [Bind(Include = "NumConcierge,NomConci,PrenomConci,CinConci,SalaireConci,TelConci")] Concierge concierge, int? id)
        {
            if (ModelState.IsValid)
            {
                db.Concierges.Add(concierge);
                immeuble.CodePers = id;
                db.Adresses.Add(adresse);
                immeuble.NumConcierge = concierge.NumConcierge;
                immeuble.ServiceImmeuble = 0;
                db.Immeubles.Add(immeuble);

                db.SaveChanges();
                return RedirectToAction("DetailsImmeuble", new {id=immeuble.CodeImmeuble });
            }

            ViewBag.NumAdresse = new SelectList(db.Adresses, "NumAdresse", "Pays", immeuble.NumAdresse);
            ViewBag.NumConcierge = new SelectList(db.Concierges, "NumConcierge", "NomConci", immeuble.NumConcierge);
            ViewBag.CodePers = new SelectList(db.Personnes, "CodePers", "nom", immeuble.CodePers);
            return View(immeuble);
        }

        public ActionResult CreateApprt(int? id) {
          
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Immeuble immeuble = db.Immeubles.Find(id);

            List<Local> listLoc = db.Locals.Where(i => i.CodeImmeuble == immeuble.CodeImmeuble).ToList();
            ViewBag.ListLocalImmeuble = listLoc;
            if (immeuble == null)
            {
                return HttpNotFound();
            }
            ViewBag.immeuble = immeuble;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DetailsImmeuble(
            [Bind(Include =
                "CodeLocal,NumTypeLocal,NumAdresse,CodeImmeuble,CodePers,CodeSynd,SuperficieLocal,NbrPieceLocal,NbrChambreLocal,TitreLocal,DescrLocal,MeubleLocal,PrixRefLocationLocal,ServiceLocal,rezCh,Interphone,Parking,Jardin,Garage,DateDDispo,DateFDispo,NumLocal,RefCodeImm")] Local local, int? id)
        {
            if (ModelState.IsValid)
            {
                Immeuble immeuble= db.Immeubles.Find(id);
                local.NumTypeLocal=1;
                local.NumAdresse = immeuble.NumAdresse;
                local.CodeImmeuble = immeuble.CodeImmeuble;
                local.CodePers = immeuble.CodePers;
                local.Staut = 0;
                local.Jardin = false;
                local.Garage = false;
                db.Locals.Add(local);
                double serv=0;
                List<Local> ListL = new List<Local>();
                ListL=immeuble.Locals.ToList();
                for (int i = 0; i < ListL.Count(); i++) {
                    serv = serv + (ListL[i].PrixRefLocationLocal.Value * ListL[i].ServiceLocal.Value) / 100;
                }
                immeuble.ServiceImmeuble = Convert.ToInt32(serv);
                db.Entry(immeuble).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DetailsImmeuble", new { id = local.CodeImmeuble });
            }

            ViewBag.NumAdresse = new SelectList(db.Adresses, "NumAdresse", "Pays", local.NumAdresse);
            ViewBag.CodeImmeuble = new SelectList(db.Immeubles, "CodeImmeuble", "NomImmeuble", local.CodeImmeuble);
            ViewBag.CodePers = new SelectList(db.Personnes, "CodePers", "nom", local.CodePers);
            ViewBag.CodeSynd = new SelectList(db.Syndics, "CodeSynd", "NomSynd", local.CodeSynd);
            ViewBag.NumTypeLocal = new SelectList(db.TypeLocals, "NumTypeLocal", "Description", local.NumTypeLocal);
            return View(local);
        }


        public ActionResult DetailsLocal(int? id) {
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

        public ActionResult DetailsApp(int? id)
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




        public ActionResult DetailsImmeuble(int? id) {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Immeuble immeuble = db.Immeubles.Find(id);

            List<Local> listLoc = db.Locals.Where(i => i.CodeImmeuble == immeuble.CodeImmeuble).ToList();
            ViewBag.ListLocalImmeuble = listLoc;
            if (immeuble == null)
            {
                return HttpNotFound();
            }
            ViewBag.immeuble = immeuble;
            return View();
        
        
        }

        // GET: Proprietaires/Edit/5
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
            pers.CodeTypePersonne =personne.CodeTypePersonne ;
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
                Personne personne = db.Personnes.Where(c=>c.CodePers==personneForm.CodePers).First();
                Adresse adresse = db.Adresses.Where(c=>c.NumAdresse==adresseForm.NumAdresse).First();
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

                personne.CodeTypePersonne = 1;
                personne.Solde = personneForm.Solde;


                db.Entry(adresse).State = EntityState.Modified;
                db.Entry(personne).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new {id=personne.CodePers });
                
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
            pers.CodeTypePersonne = 1;
            pers.solde = personne.Solde;
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
                personne.CodeTypePersonne = 1;
                personne.Solde = personneForm.solde;

                

                db.Entry(adresse).State = EntityState.Modified;
                db.Entry(personne).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = personne.CodePers });

            //}
            //ViewBag.NumAdresse = new SelectList(db.Adresses, "NumAdresse", "Pays", personneForm.NumAdresse);
            //ViewBag.CodeTypePersonne = new SelectList(db.TypePersonnes, "CodeTypePersonne", "Libelle", personneForm.CodeTypePersonne);
            //return View(personneForm);
        }

        // GET: Proprietaires/Delete/5
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

        // POST: Proprietaires/Delete/5
        //[HttpPost, ActionName("Details")]
        public ActionResult DeleteConfirmed(int id)
        {
            Personne personne = db.Personnes.Find(id);
            Adresse adresse = db.Adresses.Find(personne.NumAdresse);
            db.Personnes.Remove(personne);
            db.Adresses.Remove(adresse);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteImm(int? id)
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


        public ActionResult DeleteLoc(int? id)
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
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedLoc(int id)
        {
            Local local = db.Locals.Find(id);
            Adresse adresse = db.Adresses.Find(local.NumAdresse);
            db.Locals.Remove(local);
            db.Adresses.Remove(adresse);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteConfirmedAppart(int id)
        {
            Local local = db.Locals.Find(id);
            db.Locals.Remove(local);
            db.SaveChanges();
            return RedirectToAction("CreateAppart", new { id = local.CodeLocal });
        }


        // POST: Immeubles/Delete/5
        [HttpPost, ActionName("DeleteImm")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedImm(int id)
        {
            Immeuble immeuble = db.Immeubles.Find(id);
            int codepers = immeuble.CodePers.Value;
            db.Immeubles.Remove(immeuble);
            db.SaveChanges();
            return RedirectToAction("ImmeublesProp", new { id = codepers });
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
