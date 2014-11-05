using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cstj.MvvmToolkit.Services;
using Mova.Logic.Services.Definitions;

namespace Mova.Logic.Models.Entities
{
    /// <summary>
    /// Contient un type de vêtement.
    /// </summary>
    public class TypeVetement
    {
        #region Propriétés

        public int? IdType { get; set; }

        /// <summary>
        /// Nom du type.
        /// </summary>
        public string NomType { get; set; }

        #endregion

        public TypeVetement()
        {
            IdType = null;
            NomType = "Inconnu";
        }
        public TypeVetement(string nomType)
        {
            IdType = null;
            NomType = nomType;
        }
    }
}
