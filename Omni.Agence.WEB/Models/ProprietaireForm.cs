using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Omni.Agence.WEB.Models
{
    public class ProprietaireForm
    {
        public ProprietaireForm() { }
        
        //public virtual PropPhysiqueForm propPForm { get; set; }
        //public virtual PropMoraleForm propMForm { get; set; }
        public int CodePersP { get; set; }
        public int CodeTypePersonneP { get; set; }
        public int NumAdresseP { get; set; }
        [Required(ErrorMessage = "Ce Champ est Obligatoire")]
        public string nomP { get; set; }
        [Required(ErrorMessage = "Ce Champ est Obligatoire")]
        public string prenomP { get; set; }
        public string nationaliteP { get; set; }
        public Nullable<int> sexeP { get; set; }
        public string situationMatrimonialP { get; set; }
        public string numPassportP { get; set; }
        public string cinP { get; set; }
        public string raisonSocialP { get; set; }

        public string employeurP { get; set; }
        public string identifiantFiscaleP { get; set; }
        public string nPatenteP { get; set; }

        public string rcP { get; set; }

        public string activiteP { get; set; }
        public string fonctionP { get; set; }
        [Required(ErrorMessage = "Ce Champ est Obligatoire")]
        public string carteSejourP { get; set; }

        [RegularExpression(@"^((\+\d{1,3}(-| )?\(?\d\)?(-| )?\d{1,5})|(\(?\d{2,6}\)?))(-| )?(\d{3,4})(-| )?(\d{4})(( x| ext)\d{1,5}){0,1}$", ErrorMessage = "Format Invalide")]
        public string TelFixPersP { get; set; }
        [Required(ErrorMessage = "Ce Champ est Obligatoire")]
        [RegularExpression(@"^((\+\d{1,3}(-| )?\(?\d\)?(-| )?\d{1,5})|(\(?\d{2,6}\)?))(-| )?(\d{3,4})(-| )?(\d{4})(( x| ext)\d{1,5}){0,1}$", ErrorMessage = "Format Invalide")]
        public string TelMobilPersP { get; set; }
        [RegularExpression(@"^((\+\d{1,3}(-| )?\(?\d\)?(-| )?\d{1,5})|(\(?\d{2,6}\)?))(-| )?(\d{3,4})(-| )?(\d{4})(( x| ext)\d{1,5}){0,1}$", ErrorMessage = "Format Invalide")]
        public string TelPers3P { get; set; }
        [EmailAddress(ErrorMessage = "Format Invalide")]
        public string EmailPersP { get; set; }

        [RegularExpression(@"^((\+\d{1,3}(-| )?\(?\d\)?(-| )?\d{1,5})|(\(?\d{2,6}\)?))(-| )?(\d{3,4})(-| )?(\d{4})(( x| ext)\d{1,5}){0,1}$", ErrorMessage = "Format Invalide")]
        public string FaxPersP { get; set; }
        public Nullable<double> soldeP { get; set; }



        public int CodePersM { get; set; }
        public int CodeTypePersonneM { get; set; }
        public int NumAdresseM { get; set; }
        [Required(ErrorMessage = "Ce Champ est Obligatoire")]
        public string nomM { get; set; }
        [Required(ErrorMessage = "Ce Champ est Obligatoire")]
        public string prenomM { get; set; }
        public string nationaliteM { get; set; }
        public Nullable<int> sexeM { get; set; }
        public string situationMatrimonialM { get; set; }
        public string numPassportM { get; set; }
        public string cinM { get; set; }
        public string raisonSocialM { get; set; }
        [Required(ErrorMessage = "Ce Champ est Obligatoire")]
        public string employeurM { get; set; }
        public string identifiantFiscaleM { get; set; }
        public string nPatenteM { get; set; }
        [Required(ErrorMessage = "Ce Champ est Obligatoire")]
        public string rcM { get; set; }
        [Required(ErrorMessage = "Ce Champ est Obligatoire")]
        public string activiteM { get; set; }
        public string fonctionM { get; set; }
        [Required(ErrorMessage = "Ce Champ est Obligatoire")]
        public string carteSejourM { get; set; }
        [Required(ErrorMessage = "Ce Champ est Obligatoire")]

        [RegularExpression(@"^((\+\d{1,3}(-| )?\(?\d\)?(-| )?\d{1,5})|(\(?\d{2,6}\)?))(-| )?(\d{3,4})(-| )?(\d{4})(( x| ext)\d{1,5}){0,1}$", ErrorMessage = "Format Invalide")]
        public string TelFixPersM { get; set; }
        [RegularExpression(@"^((\+\d{1,3}(-| )?\(?\d\)?(-| )?\d{1,5})|(\(?\d{2,6}\)?))(-| )?(\d{3,4})(-| )?(\d{4})(( x| ext)\d{1,5}){0,1}$", ErrorMessage = "Format Invalide")]
        [Required(ErrorMessage = "Ce Champ est Obligatoire")]
        public string TelMobilPersM { get; set; }
        [RegularExpression(@"^((\+\d{1,3}(-| )?\(?\d\)?(-| )?\d{1,5})|(\(?\d{2,6}\)?))(-| )?(\d{3,4})(-| )?(\d{4})(( x| ext)\d{1,5}){0,1}$", ErrorMessage = "Format Invalide")]
        public string TelPers3M { get; set; }
        [EmailAddress(ErrorMessage = "Adresse mail Invalide")]

        public string EmailPersM { get; set; }

        [RegularExpression(@"^((\+\d{1,3}(-| )?\(?\d\)?(-| )?\d{1,5})|(\(?\d{2,6}\)?))(-| )?(\d{3,4})(-| )?(\d{4})(( x| ext)\d{1,5}){0,1}$", ErrorMessage = "Format Invalide")]
        public string FaxPersM { get; set; }
        public Nullable<double> soldeM { get; set; }

        public virtual AdresseForm AdresseFormM { get; set; }

        public virtual AdresseForm AdresseFormP { get; set; }
        
    }
}