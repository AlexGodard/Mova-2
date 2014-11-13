using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cstj.MvvmToolkit.Services;
using Mova.Logic.Models;
using Mova.Logic.Models.Args;
using Mova.Logic.Models.Entities;
using Mova.Logic.Services.Definitions;
using Mova.Logic.Services.Helpers;
using MySql.Data.MySqlClient;

namespace Mova.Logic.Services.MySql
{
    public class MySqlUtilisateurEnsemble : IUtilisateurEnsembleService
    {
        private MySqlConnexion connexion;

        public IList<UtilisateurEnsemble> RetrieveEnsembleUtilisateur(RetrieveUtilisateurEnsembleArgs args)
        {
            IList<UtilisateurEnsemble> result = new List<UtilisateurEnsemble>();
            try
            {
                connexion = new MySqlConnexion();

                string requete = "SELECT * FROM UtilisateurEnsemble";

                DataSet dataset = connexion.Query(requete);
                DataTable table = dataset.Tables[0];

                foreach (DataRow utilisateurensemble in table.Rows)
                {
                    result.Add(ConstructUtilisateurEnsemble(utilisateurensemble));
                }

            }
            catch (MySqlException)
            {
                throw;
            }

            return result;
        }


        public IList<UtilisateurEnsemble> RetrieveEnsembleUtilisateurPrecis()
        {
            IList<UtilisateurEnsemble> result = new List<UtilisateurEnsemble>();
            try
            {
                connexion = new MySqlConnexion();

                string requete = "SELECT idUtilisateurEnsemble FROM UtilisateursEnsembles WHERE idUtilisateur = " + Listes.UtilisateurConnecte.IdUtilisateur;

                DataSet dataset = connexion.Query(requete);
                DataTable table = dataset.Tables[0];

                foreach (DataRow utilisateurEnsemble in table.Rows)
                {
                    result.Add(ConstructUtilisateurEnsemble(utilisateurEnsemble));
                }

            }
            catch (MySqlException)
            {
                throw;
            }

            return result;
        }

        public IList<EnsembleVetement> RetrieveRecents()
        {
            IList<EnsembleVetement> result = new List<EnsembleVetement>();
            List<Vetement> listeVetementTemp = new List<Vetement>();
            //int idEnsembleActuel = 0;

            try
            {
                connexion = new MySqlConnexion();


                string requete = "SELECT v.*, ev.idEnsemble FROM UtilisateursEnsembles ue INNER JOIN EnsemblesVetements ev ON ev.idEnsemble=ue.idEnsemble INNER JOIN Vetements v ON v.idVetement=ev.idVetement WHERE ue.idUtilisateur = " + Listes.UtilisateurConnecte.IdUtilisateur + " ORDER BY ue.dateCreation DESC";

                DataSet dataset = connexion.Query(requete);

                //Ici on a les ensemble récents, en ordre décroissant d'un utilisateur
                DataTable table = dataset.Tables[0];

                //On reconstruit les ensembles à partir des vêtements
                //L'algorithme devrait être plus complexe mais pour l'instant cela suffit
                foreach (DataRow enregistrement in table.Rows)
                {

                    //On ajoute toujours un vêtement dans cette liste
                    listeVetementTemp.Add(ConstructVetement(enregistrement));

                    if (listeVetementTemp.Count >= 3)
                    {

                        result.Add(new EnsembleVetement() { IdEnsemble = (int)enregistrement["idEnsemble"], ListeVetements = listeVetementTemp });

                        listeVetementTemp = new List<Vetement>();
                    }
                }

            }
            catch (MySqlException)
            {
                throw;
            }

            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private Vetement ConstructVetement(DataRow row)
        {
            return new Vetement()
            {
                IdVetement = (int)row["idVetement"],
                NomVetement = (string)row["nomVetement"],
                ImageURL = (string)row["imageURL"],
                Prix = (float)row["prix"],
                EstHomme = (bool)row["estHomme"],
                EstFemme = (bool)row["estFemme"],
                TypeVetement = new TypeVetement((int)row["idTypeVetement"])
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vetements"></param>
        /// <returns></returns>
        private List<EnsembleVetement> ConstructEnsembles(List<Vetement> vetements)
        {
            List<EnsembleVetement> listeRetour = new List<EnsembleVetement>();
            List<Vetement> listeVetements = new List<Vetement>();
            int idEnsembleActuel = 0;

            //Étant donné que la liste est triée par la BD, il est possible de faire ceci sans vérification
            foreach (Vetement vetement in vetements)
            {
                //Nouvel ensemble
                if (listeVetements.Count <= 0)
                {
                    //idEnsembleActuel = vet

                }
                //Ensemble complet (3 vetements)
                else if (listeVetements.Count >= 3)
                {
                    listeRetour.Add(new EnsembleVetement() { IdEnsemble = idEnsembleActuel, ListeVetements = listeVetements });
                    listeVetements = new List<Vetement>();
                }
                else
                {

                }



            }


            return listeRetour;
        }


        private UtilisateurEnsemble ConstructUtilisateurEnsemble(DataRow row)
        {
            return new UtilisateurEnsemble()
            {
                /*IdTemperature = (int)row["idTemperature"],
                NomClimat = (string)row["nomClimat"]*/
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ev"></param>
        /// <returns></returns>
        public bool Insert(EnsembleVetement ev)
        {

            int noEnsembleInsere = ServiceFactory.Instance.GetService<IEnsembleVetementService>().InsererEnsemble(ev);

            //Si on obtient un nombre négatif c'est que quelque chose s'est passé
            if (noEnsembleInsere < 0)
            {
                return false;
            }

            try
            {
                connexion = new MySqlConnexion();
                string query = "INSERT INTO UtilisateursEnsembles (idUtilisateur,idEnsemble,dateCreation,estFavori) VALUES(" + Listes.UtilisateurConnecte.IdUtilisateur + "," + noEnsembleInsere + ",NOW(),TRUE)";

                //On ajoute un Ensemble normal sans rien parce qu'on a pas le choix
                DataSet dataset = connexion.Query(query);
            }
            catch (MySqlException)
            {
                throw;
            }


            return true;
        }


        public int Create(UtilisateurEnsemble utilisateurEnsemble)
        {
            try
            {
                connexion = new MySqlConnexion();

                string debutRequete = "INSERT INTO UtilisateursEnsembles (idUtilisateur, idEnsemble, dateCreation, estFavori, estDansGardeRobe) VALUES ";


                string valeurs = "(" + utilisateurEnsemble.Utilisateur.IdUtilisateur + ", " + utilisateurEnsemble.Ensemble.IdEnsemble + ", '" + utilisateurEnsemble.DateCreation + "', " + utilisateurEnsemble.EstFavori + ", " + utilisateurEnsemble.EstDansGardeRobe + ");";
                string requete = debutRequete + valeurs;

                connexion.Query(requete);
                DataSet dataset = connexion.Query("SELECT MAX(idUtilisateurEnsemble) FROM UtilisateursEnsembles");

                return (int)dataset.Tables[0].Rows[0].ItemArray[0];
            }
            catch (MySqlException e)
            {
                throw e;
            }
        }



    }
}
