using Mova.Logic.Models.Args;
using Mova.Logic.Models.Entities;
using Mova.Logic.Services.Definitions;
using Mova.Logic.Services.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mova.Logic.Models;
using Cstj.MvvmToolkit.Services;

namespace Mova.Logic.Services.MySql
{
    public class MySqlUtilisateurService : IUtilisateurService
    {

        private MySqlConnexion connexion;

        public IList<Utilisateur> RetrieveAll()
        {
            IList<Utilisateur> result = new List<Utilisateur>();

            try
            {
                connexion = new MySqlConnexion();

                string requete = "SELECT * FROM Utilisateurs";

                DataSet dataset = connexion.Query(requete);
                DataTable table = dataset.Tables[0];

                foreach (DataRow utilisateur in table.Rows)
                {
                    result.Add(ConstructUtilisateur(utilisateur));
                }

            }
            catch (MySqlException)
            {
                throw;
            }

            return result;


        }

        public Utilisateur Retrieve(RetrieveUtilisateurArgs args)
        {
            try
            {
                connexion = new MySqlConnexion();

                string requete = "SELECT * FROM Utilisateurs WHERE nomUtilisateur = '" + args.NomUtilisateur + "' AND BINARY motPasse = '"
                 + Listes.pwd + "'";

                DataSet dataset = connexion.Query(requete);
                DataTable table = dataset.Tables[0];

                //Ici nous devrions avoir un seul utilisateur et si on va dans le foreach alors on retourne le premier (et le seul) de la réponse
                foreach (DataRow utilisateur in table.Rows)
                {
                    return ConstructUtilisateur(utilisateur);
                }

            }
            catch (MySqlException)
            {
                throw;
            }

            //Si on se rend ici, on retourne un utilisateur vide
            return new Utilisateur();
        }

        public Utilisateur RetrieveIdentifiant(RetrieveUtilisateurArgs args)
        {
            try
            {
                connexion = new MySqlConnexion();

                string requete = "SELECT * FROM Utilisateurs WHERE nomUtilisateur = '" + args.NomUtilisateur + "'";

                DataSet dataset = connexion.Query(requete);
                DataTable table = dataset.Tables[0];

                //Ici nous devrions avoir un seul utilisateur et si on va dans le foreach alors on retourne le premier (et le seul) de la réponse
                foreach (DataRow utilisateur in table.Rows)
                {
                    return ConstructUtilisateur(utilisateur);
                }

            }
            catch (MySqlException)
            {
                throw;
            }

            //Si on se rend ici, on retourne un utilisateur vide
            return null;
        }

        public Utilisateur RetrieveCourriel(RetrieveUtilisateurArgs args)
        {
            try
            {
                connexion = new MySqlConnexion();

                string requete = "SELECT * FROM Utilisateurs WHERE courriel = '" + args.Courriel + "'";

                DataSet dataset = connexion.Query(requete);
                DataTable table = dataset.Tables[0];

                //Ici nous devrions avoir un seul utilisateur et si on va dans le foreach alors on retourne le premier (et le seul) de la réponse
                foreach (DataRow utilisateur in table.Rows)
                {
                    return ConstructUtilisateur(utilisateur);
                }

            }
            catch (MySqlException)
            {
                throw;
            }

            //Si on se rend ici, on retourne un utilisateur vide
            return null;
        }


        public void Create(Utilisateur utilisateur)
        {
            try
            {
                connexion = new MySqlConnexion();

                string debutRequete = "INSERT INTO Utilisateurs (nom,prenom,nomUtilisateur,courriel,motPasse,sexe,estAdmin) VALUES ";
                string valeurs = "('" + utilisateur.Nom.Replace("'", "''") + "','" + utilisateur.Prenom.Replace("'", "''") + "','" + utilisateur.NomUtilisateur.Replace("'", "''") + "','" + utilisateur.Courriel + "','" + utilisateur.MotDePasse.Replace("'", "''") + "','" + utilisateur.Sexe + "', FALSE )"; 

                string requete = debutRequete + valeurs;

                DataSet dataset = connexion.Query(requete);


            }
            catch (MySqlException)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ev"></param>
        /// <returns></returns>
        public bool AjouterFavori(EnsembleVetement ev)
        {
            try
            {
                connexion = new MySqlConnexion();

                IEnsembleVetementService _ensembleVetementService = ServiceFactory.Instance.GetService<IEnsembleVetementService>();

                try {
                    _ensembleVetementService.InsererEnsemble(ev);
                }
                catch (MySqlException)
                {
                    throw;
                }

                


               // string debutRequete = "INSERT INTO UtilisateursEnsembles (idUtilisateur,idEnsemble,dateCreation,estFavori,estDansGardeRobe) VALUES ";
                //string valeurs = "('" + utilisateur.Nom.Replace("'", "''") + "','" + utilisateur.Prenom.Replace("'", "''") + "','" + utilisateur.NomUtilisateur.Replace("'", "''") + "','" + utilisateur.Courriel + "','" + utilisateur.MotDePasse.Replace("'", "''") + "','" + utilisateur.Sexe + "', FALSE )";

                //string requete = debutRequete + valeurs;

                //DataSet dataset = connexion.Query(requete);


            }
            catch (MySqlException)
            {
                throw;
            }

            return true;
        }

        private Utilisateur ConstructUtilisateur(DataRow row)
        {
            return new Utilisateur()
            {
                IdUtilisateur = (int)row["idUtilisateur"],
                Nom = (string)row["nom"],
                Prenom = (string)row["prenom"],
                NomUtilisateur = (string)row["nomUtilisateur"],
                Courriel = (string)row["courriel"],
                MotDePasse = (string)row["motPasse"],
                Sexe = (string)row["sexe"],
                EstAdmin = (bool)row["estAdmin"]
            };

        }

        private string validateSQL(string s)
        {
        
            StringBuilder retour = new StringBuilder();

            for (int i = 0; i < s.Length; i++)
            {

                if (s[i].ToString() == "'")
                {
                    retour.Append("'");
                }

                retour.Append(s[i].ToString());

            }

            return retour.ToString();
        }
    }
}
