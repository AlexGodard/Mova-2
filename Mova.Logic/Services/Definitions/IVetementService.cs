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
    public interface IVetementService
    {
        //IList<Vetement> RetrieveAll();

        void Create(Vetement vetement);   
        int RetrieveIdType(string nomType);
        int RetrieveIdCouleur(string nomCouleur);
        IList<int> RetrieveIdTypeAll();
    }
}
