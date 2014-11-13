using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cstj.MvvmToolkit.Services;
using Mova.Logic.Models;
using Mova.Logic.Models.Args;
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
 

        private UtilisateurEnsemble ConstructUtilisateurEnsemble(DataRow row)
        {
            // TODO: THIS PART
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

                string debutRequete = "INSERT IGNORE INTO UtilisateursEnsembles (idUtilisateur, idEnsemble, dateCreation, estFavori, estDansGardeRobe) VALUES ";


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
