using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mova.Logic.Models
{
    /// <summary>
    /// Défini à quel style un vêtement appartient. Exemple: vêtement 1 = gothique
    /// </summary>
    public class StyleVetement
    {
        #region Propriétés

        public virtual int? IdStyle { get; set; }

        /// <summary>
        /// Nom du syle.
        /// </summary>
        public virtual string NomStyle { get; set; }

        #endregion
        
        public StyleVetement()
        {
            IdStyle = null;
            NomStyle = "Inconnu";
        }

        public StyleVetement(string nomStyle)
        {
            // TODO: Complete member initialization
            NomStyle = nomStyle;
        }
    }
}
