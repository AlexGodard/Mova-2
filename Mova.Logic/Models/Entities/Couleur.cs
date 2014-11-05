using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mova.Logic.Models
{
    /// <summary>
    /// Contient une couleur pour les vêtements.
    /// </summary>
    public class Couleur
    {
        #region Propriétés

        public virtual int? IdCouleur { get; set; }

        /// <summary>
        /// Nom de la couleur majeure du vêtement.
        /// </summary>
        public virtual string NomCouleur { get; set; }

        #endregion

        public Couleur()
        {
            IdCouleur = null;
            NomCouleur = "Inconnu";
        }
        public Couleur(string nomCouleur)
        {
            IdCouleur = null;
            NomCouleur = nomCouleur;
        }
    }
}
