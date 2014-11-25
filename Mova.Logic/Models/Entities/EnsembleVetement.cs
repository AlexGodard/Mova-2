using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mova.Logic.Models.Entities;

namespace Mova.Logic.Models
{
    /// <summary>
    /// Lien entre un ensemble et un vêtement pour permettre de savoir les trois vêtements d’un ensemble.
    /// </summary>
    public class EnsembleVetement
    {
        #region Propriétés

        public int? IdEnsembleVetement { get; set; }

        public DateTime? DateAjout { get; set; }

        /// <summary>
        /// Permet de savoir à quel ensemble les vêtements appartiennent.
        /// </summary>
        public int IdEnsemble { get; set; }

        /// <summary>
        /// Permet de savoir les trois vêtement (haut, bas et chaussures) composant l'ensemble.
        /// </summary>
        public List<Vetement> ListeVetements { get; set; }

        #endregion

        public EnsembleVetement()
        {
            IdEnsembleVetement = 0;
            IdEnsemble = 0;
            ListeVetements = new List<Vetement>();
        }
    }
}
