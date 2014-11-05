using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mova.Logic.Models
{
    /// <summary>
    /// Contient une activité  durant laquelle un vêtement peut être porté.
    /// </summary>
    public class Activite
    {
        #region Propriétés
        
        public virtual int? IdActivite { get; set; }

        /// <summary>
        /// Nom de l'activité sous forme d'un verbe à l'infinitif comme "Aller à..." ou "Jouer au...".
        /// </summary>
        public virtual string NomActivite { get; set; }

        /// <summary>
        /// Permet de savoir les moments où l'on peut pratiquer l'activité.
        /// </summary>
        public virtual List<Moment> ListeMoments { get; set; }

        #endregion

        //Commentaire activite

        public Activite()
        {
            this.IdActivite = null;
            this.NomActivite = "Inconnu";
        }
        public Activite(string nomActivite)
        {
            // TODO: Complete member initialization
            this.NomActivite = nomActivite;
        }
    }
}
