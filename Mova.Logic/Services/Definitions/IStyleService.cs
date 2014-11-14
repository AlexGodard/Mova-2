using Mova.Logic.Models;
using Mova.Logic.Models.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mova.Logic.Services.Definitions
{
    public interface IStyleService
    {
        IList<StyleVetement> RetrieveAll();

        IList<StyleVetement> RetrieveSpecific(InfoStylisteArgs args);

        void Create(StyleVetement styleVetement);
        void Update(StyleVetement styleVetement, string newCouleur);
        void Delete(StyleVetement styleVetement);
    }
}
