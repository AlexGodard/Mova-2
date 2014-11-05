using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mova.Logic.Models
{
    /// <summary>
    /// Contient une condition météorologique pour laquelle on peut porter un vêtement.
    /// </summary>
    public class Temperature
    {
        
        #region Propriétés

        public virtual int? IdTemperature { get; set; }

        /// <summary>
        /// Nom de la condition météorologique.
        /// </summary>
        public virtual string NomClimat { get; set; }

        #endregion

        public Temperature()
        {
            this.IdTemperature = null;
            NomClimat = "Inconnu";
        }

        public Temperature(string nomTemperature)
        {
            NomClimat = nomTemperature;
        }
    }
}
