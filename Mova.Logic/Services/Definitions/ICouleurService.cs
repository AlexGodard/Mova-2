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
    public interface ICouleurService
    {
        IList<Couleur> RetrieveAll();
        void Create(Couleur couleur);
        void Update(Couleur couleur, string newCouleur);
        void Delete(Couleur couleur);
    }
}
