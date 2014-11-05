using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mova.Logic.Models.Entities
{
    /// <summary>
    /// Utilisateur du logiciel.
    /// </summary>
    public class Utilisateur
    {
        #region Propriétés

        public virtual int? IdUtilisateur { get; set; }

        /// <summary>
        /// Prenom de l'utilisateur.
        /// </summary>
        public virtual string Prenom { get; set; }

        /// <summary>
        /// Nom de l'utilisateur.
        /// </summary>
        public virtual string Nom { get; set; }

        /// <summary>
        /// Nom d'utilisateur (identifiant) de l'utilisateur.
        /// </summary>
        public virtual string NomUtilisateur { get; set; }

        /// <summary>
        /// Mot de passe de l'utilisateur.
        /// </summary>
        public virtual string MotDePasse { get; set; }

        /// <summary>
        /// Courriel de l'utilisateur.
        /// </summary>
        public virtual string Courriel { get; set; }

        /// <summary>
        /// Sexe de l'utilisateur.
        /// </summary>
        public virtual string Sexe { get; set; }

        /// <summary>
        /// Si l'utilisateur est un admin.
        /// </summary>
        public virtual bool EstAdmin { get; set; }

        /// <summary>
        /// Liste de vêtements appartenant à l'utilisateur.
        /// </summary>
        public virtual List<Vetement> ListeVetements { get; set; }

        #endregion

        #region Constructeurs

        /// <summary>
        /// Constructeur de base d'un utilisateur.
        /// </summary>
        public Utilisateur()
        {
            IdUtilisateur = null;
            Prenom = string.Empty;
            Nom = string.Empty;
            NomUtilisateur = string.Empty;
            MotDePasse = string.Empty;
            Courriel = string.Empty;
            Sexe = string.Empty;
            EstAdmin = false;
        }

        public Utilisateur(Utilisateur u)
        {
            IdUtilisateur = u.IdUtilisateur;
            Prenom=  u.Prenom;
            Nom = u.Nom;
            NomUtilisateur = u.NomUtilisateur;
            Courriel = u.Courriel;
            Sexe = u.Sexe;
            EstAdmin = u.EstAdmin;
        }

        /// <summary>
        /// Constructeur paramétré d'un utilisateur.
        /// </summary>
        /// <param name="i">IdUtilisateur</param>
        /// <param name="p">Prenom</param>
        /// <param name="n">Nom</param>
        /// <param name="nm">NomUtilisateur</param>
        /// <param name="mp">MotDePasse</param>
        /// <param name="c">Courriel</param>
        /// <param name="s">Sexe</param>
        public Utilisateur(int i, string p, string n, string nm, string mp, string c, string s, bool eA)
        {
            IdUtilisateur = i;
            Prenom = p;
            Nom = n;
            NomUtilisateur = nm;
            MotDePasse = mp;
            Courriel = c;
            Sexe = s;
            EstAdmin = eA;
        }

        #endregion
    }
}
