using Mova.Logic.Models;
using Mova.Logic.Models.Args;
using Mova.Logic.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mova.Logic.Services.Definitions
{
    public interface IEnsembleVetementService
    {

        IList<EnsembleVetement> RetrieveSelection(InfoStylisteArgs args);
        IList<Vetement> RetrieveVetementsEnsembles(IList<UtilisateurEnsemble> listeEnsembles);

        void Create(EnsembleVetement ensembleVetement);
        int InsererEnsemble(EnsembleVetement ev);
        void CreateEnsemble(IList<Vetement> v,int idEnsemble);
    }
}
