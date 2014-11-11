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

        bool InsererEnsemble(EnsembleVetement ev);

    }
}
