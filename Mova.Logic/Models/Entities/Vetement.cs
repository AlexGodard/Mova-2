using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mova.Logic.Models.Entities
{
    /// <summary>
    /// Un vêtement du logiciel.
    /// </summary>
    public class Vetement
    {
        #region Propriétés

        public int? IdVetement { get; set; }

        /// <summary>
        /// Le nom du vêtement.
        /// </summary>
        public string NomVetement { get; set; }

        /// <summary>
        /// Le lien de l'image du vêtement.
        /// </summary>
        public string ImageURL { get; set; }

        /// <summary>
        /// Le prix du vêtement.
        /// </summary>
        public int Prix { get; set; }

        /// <summary>
        /// True si le vêtement est fair pour un homme.
        /// </summary>
        public bool EstHomme { get; set; }

        /// <summary>
        /// True sir le vêtement est fait pour une femme.
        /// </summary>
        public bool EstFemme { get; set; }

        /// <summary>
        /// Type du vêtement.
        /// </summary>
        public TypeVetement TypeVetement { get; set; }

        /// <summary>
        /// Couleur principale du vêtement.
        /// </summary>
        public Couleur CouleurVetement { get; set; }

        /// <summary>
        /// Température idéale pour porter le vêtement.
        /// </summary>
        public List<Temperature> ListeTemperatures { get; set; }

        /// <summary>
        /// Liste de tous les styles qui correspondent au vêtement.
        /// </summary>
        public List<StyleVetement> ListeStyles { get; set; }

        /// <summary>
        /// Liste de toutes les activités que l'on pratique en portant ce vêtement.
        /// </summary>
        public List<Activite> ListeActivites { get; set; }

        #endregion

        public Vetement()
        {
            IdVetement = 0;
            NomVetement = "Inconnu";
            ImageURL = "default.jpg";
            Prix = 10;
            EstHomme = true;
            EstFemme = false;
            TypeVetement = null;
            CouleurVetement = null;
            ListeTemperatures = null;
            ListeStyles = null;
            ListeActivites = null;
        }

        /// <summary>
        /// Constructeur paramétré d'un vêtement.
        /// </summary>
        /// <param name="idVetement">Id du vêtement</param>
        /// <param name="typeVetement">Type du vêtement</param>
        /// <param name="couleur">Couleur du vêtement</param>
        /// <param name="nomVetement">Nom du vêtement</param>
        /// <param name="imageURL">URL de l'image du vêtement</param>
        /// <param name="prix">Prix du vêtement</param>
        /// <param name="estHomme">Si le vêtement peut être porté par un homme</param>
        /// <param name="estFemme">Si le vêtement peut être poerté par une femme</param>
        /// <param name="listeActivites">Liste des activités que l'on peut faire en portant le vêtement</param>
        /// <param name="listeStyles">Liste des styles auxquels le vêtement appartient</param>
        /// <param name="listeTemperature">Liste des températures dans lesquelles on peut porter le vêtement</param>
        public Vetement(TypeVetement typeVetement, Couleur couleur, string nomVetement, string imageURL, int prix, bool estHomme, bool estFemme, List<Activite> listeActivites, List<StyleVetement> listeStyles, List<Temperature> listeTemperatures)
        {
            //IdVetement = idVetement;
            TypeVetement = typeVetement;
            CouleurVetement = couleur;
            NomVetement = nomVetement;
            ImageURL = imageURL;
            Prix = prix;
            EstHomme = estHomme;
            EstFemme = estFemme;
            ListeActivites = listeActivites;
            ListeStyles = listeStyles;
            ListeTemperatures = listeTemperatures;
        }
    }
}
