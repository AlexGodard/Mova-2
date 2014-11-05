using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mova.Logic.Models.Entities
{
    public class UtilisateurVetements
    {
        public virtual int? IdUtilisateurVetement { get; set; }
        public virtual int IdUtilisateur { get; set; }
        public virtual int IdVetement { get; set; }
    }
}
