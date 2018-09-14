using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
//using Omni.Agence.DAL;
namespace Omni.Agence.WEB.Models
{
    public class PropMoraleForm
    {
        public PropMoraleForm()
        {
            //this.ContratLocations = new HashSet<ContratLocation>();
            //this.Immeubles = new HashSet<Immeuble>();
            //this.Locals = new HashSet<Local>();
            //this.Operations = new HashSet<Operation>();
        }
    
        public int CodePers { get; set; }
        public Nullable<int> RefCodePers { get; set; }
        public int CodeTypePersonne { get; set; }
        public int NumAdresse { get; set; }
        [Required(ErrorMessage = "Ce Champ est Obligatoire")]
        public string nom { get; set; }
        [Required(ErrorMessage = "Ce Champ est Obligatoire")]
        public string prenom { get; set; }
        public string nationalite { get; set; }
        public Nullable<int> sexe { get; set; }
        public string situationMatrimonial { get; set; }
        public string numPassport { get; set; }
        public string cin { get; set; }
        public string raisonSocial { get; set; }
        [Required(ErrorMessage = "Ce Champ est Obligatoire")]
        public string employeur { get; set; }
        public string identifiantFiscale { get; set; }
        public string nPatente { get; set; }
        [Required(ErrorMessage = "Ce Champ est Obligatoire")]
        public string rc { get; set; }
        [Required(ErrorMessage = "Ce Champ est Obligatoire")]
        public string activite { get; set; }
        public string fonction { get; set; }
        [Required(ErrorMessage="Ce Champ est Obligatoire")]
        public string carteSejour { get; set; }
        [Required(ErrorMessage="Ce Champ est Obligatoire")]

        [RegularExpression(@"^((\+\d{1,3}(-| )?\(?\d\)?(-| )?\d{1,5})|(\(?\d{2,6}\)?))(-| )?(\d{3,4})(-| )?(\d{4})(( x| ext)\d{1,5}){0,1}$", ErrorMessage = "Format Invalide")]
        public string TelFixPers { get; set; }
        [RegularExpression(@"^((\+\d{1,3}(-| )?\(?\d\)?(-| )?\d{1,5})|(\(?\d{2,6}\)?))(-| )?(\d{3,4})(-| )?(\d{4})(( x| ext)\d{1,5}){0,1}$", ErrorMessage = "Format Invalide")]
        [Required(ErrorMessage = "Ce Champ est Obligatoire")]
        public string TelMobilPers { get; set; }
        [RegularExpression(@"^((\+\d{1,3}(-| )?\(?\d\)?(-| )?\d{1,5})|(\(?\d{2,6}\)?))(-| )?(\d{3,4})(-| )?(\d{4})(( x| ext)\d{1,5}){0,1}$", ErrorMessage = "Format Invalide")]
        public string TelPers3 { get; set; }
        [EmailAddress(ErrorMessage="Adresse mail Invalide")]

        public string EmailPers { get; set; }

        [RegularExpression(@"^((\+\d{1,3}(-| )?\(?\d\)?(-| )?\d{1,5})|(\(?\d{2,6}\)?))(-| )?(\d{3,4})(-| )?(\d{4})(( x| ext)\d{1,5}){0,1}$", ErrorMessage = "Format Invalide")]
        public string FaxPers { get; set; }
        public Nullable<double> solde { get; set; }
    
        public virtual AdresseForm AdresseForm { get; set; }
        //public virtual ICollection<ContratLocation> ContratLocations { get; set; }
        //public virtual ICollection<Immeuble> Immeubles { get; set; }
        //public virtual ICollection<Local> Locals { get; set; }
        //public virtual ICollection<Operation> Operations { get; set; }
        //public virtual TypePersonne TypePersonne { get; set; }
    }
}