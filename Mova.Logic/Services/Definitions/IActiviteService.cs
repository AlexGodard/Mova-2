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
    public interface IActiviteService
    {
       IList<Activite> RetrieveAll();

       IList<Activite> RetrievePourMoment(int idMoment, bool estOuvrable);

       void Create(Activite activite);
       void Update(Activite activite, string newActivite);
       void Delete(Activite activite);
    }
}
