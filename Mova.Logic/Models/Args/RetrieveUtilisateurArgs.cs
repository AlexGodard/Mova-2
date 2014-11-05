using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mova.Logic.Models.Args
{
    public class RetrieveUtilisateurArgs
    {
         public int IdUtilisateur { get; set; }
         public string NomUtilisateur { get; set; }    /*Enregistre le nom d'utilisateur qu'il écrit dans le champ txtUtilisateur (BINDING)*/
         public string MotPasse { get; set; }   /*Enregistre le mot de passe qu'il écrit dans le champ txtMotPasse (BINDING)*/
         public string Nom { get; set; }
         public string Prenom { get; set; }
         public string Courriel { get; set; }
         public string Sexe { get; set; }
    }
}
