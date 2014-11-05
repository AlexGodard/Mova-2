using Mova.Logic.Models;
using Mova.Logic.Models.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mova.Logic.Models.Entities;

namespace Mova.Logic.Services.Definitions
{
    public interface IUtilisateurService
    {
       IList<Utilisateur> RetrieveAll();

       Utilisateur Retrieve(RetrieveUtilisateurArgs args);
       Utilisateur RetrieveIdentifiant(RetrieveUtilisateurArgs args);
       Utilisateur RetrieveCourriel(RetrieveUtilisateurArgs args);

       void Create(Utilisateur utilisateur);       // Retourne true si ajouté, false si il y a eu un erreur
    }
}
