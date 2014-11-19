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
        IList<UtilisateurEnsemble> RetrieveEnsembleUtilisateurPrecis();

        IList<EnsembleVetement> RetrieveRecents();
        IList<EnsembleVetement> RetrieveFavoris();

        int Create(UtilisateurEnsemble utilisateurEnsemble);
        bool Insert(EnsembleVetement ev);
        bool InsertAvecId(int id);
    }
}
