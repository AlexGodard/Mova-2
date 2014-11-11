using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mova.Logic.Models.Entities;

namespace Mova.Logic.Models
{
    /// <summary>
    /// Fait le lien entre un utilisateur et un ensemble. Cette table permet donc de garder en mémoire les ensembles détenus par l’utilisateur (garde-robe, récent et/ou favoris).
    /// </summary>
    public class UtilisateurEnsemble
    {
        #region Propriétés

        public virtual int? IdUtilisateurEnsemble { get; set; }

        /// <summary>
        /// La date de création d'un ensemble par un utilisateur ou la date où l'utilisateur a choisi l'ensemble pour l'ajouter aux récents.
        /// </summary>
        public virtual DateTime DateCreation { get; set; }

        /// <summary>
        /// Si l'ensemble est parmi les favoris de l'utilisateur.
        /// </summary>
        public virtual bool EstFavori { get; set; }

        /// <summary>
        /// Permet de savoir à quel utilisateur l'ensemble appartient.
        /// </summary>
        public virtual Utilisateur Utilisateur { get; set; }

        /// <summary>
        /// Permet de savoir l'ensemble détenu par l'utilisateur.
        /// </summary>
        public virtual Ensemble ensemble { get; set; }

        #endregion
    }
}
