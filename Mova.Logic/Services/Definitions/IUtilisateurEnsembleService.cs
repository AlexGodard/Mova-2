using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mova.Logic.Models.Args;
using Mova.Logic.Models;

namespace Mova.Logic.Services.Definitions
{
    public interface IUtilisateurEnsembleService
    {
        IList<UtilisateurEnsemble> RetrieveEnsembleUtilisateur(RetrieveUtilisateurEnsembleArgs args);   //Classe qui englobe tous les paramêtres nécessaire à un méthode
    }
}
