using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Omni.Agence.WEB.Models
{
    public class AdresseForm
    {

        public int NumAdresse { get; set; }
        public string Pays { get; set; }
        public string Ville { get; set; }
        [Required(ErrorMessage = "Ce Champ est Obligatoire")]
        public string quartier { get; set; }
        [Required(ErrorMessage = "Ce Champ est Obligatoire")]
        public string descAdresse { get; set; }

        public Nullable<int> CPadress { get; set; }


    }
}