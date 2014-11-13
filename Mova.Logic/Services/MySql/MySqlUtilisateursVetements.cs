using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mova.Logic.Models;
using Mova.Logic.Models.Entities;
using Mova.Logic.Services.Definitions;
using Mova.Logic.Services.Helpers;
using MySql.Data.MySqlClient;

namespace Mova.Logic.Services.MySql
{
    public class MySqlUtilisateursVetements : IUtilisateurVetementService
    {
        private MySqlConnexion connexion;

        public IList<UtilisateurVetements> RetrieveAll()
        {
            IList<UtilisateurVetements> result = new List<UtilisateurVetements>();

            try
            {
                connexion = new MySqlConnexion();

                string requete = "SELECT * FROM UtilisateursVetements WHERE idUtilisateur = " + Listes.UtilisateurConnecte.IdUtilisateur;

                DataSet dataset = connexion.Query(requete);
                DataTable table = dataset.Tables[0];

                foreach (DataRow utilisateur in table.Rows)
                {
                    result.Add(ConstructUtilisateurVetement(utilisateur));
                }

            }
            catch (MySqlException)
            {
                throw;
            }

            return result;
        }


        public void InsertVetementUtilisateur(string imageURL)
        {
            try
            {
                connexion = new MySqlConnexion();

                string requete = "INSERT INTO UtilisateursVetements (idUtilisateur,idVetement) VALUES " +
                "((SELECT idUtilisateur FROM Utilisateurs WHERE idUtilisateur = " + 
                Listes.UtilisateurConnecte.IdUtilisateur + ")," +
                "(SELECT idVetement FROM Vetements WHERE imageURL = '" + imageURL + "'))";

                DataSet dataset = connexion.Query(requete);
            }
            catch (MySqlException)
            {
                throw;
            }

        }


        private UtilisateurVetements ConstructUtilisateurVetement(DataRow row)
        {
            return new UtilisateurVetements()
            {
                IdUtilisateurVetement = (int)row["idUtilisateurVetement"],
                IdUtilisateur = (int)row["idUtilisateur"],
                IdVetement = (int)row["idVetement"]
            };
        }

        private Vetement ConstructVetement(DataRow row)
        {
            return new Vetement()
            {
                IdVetement = (int)row["idUtilisateurVetement"],
                NomVetement = (string)row["idUtilisateur"],
                ImageURL = (string)row["idVetement"],
                Prix = (int)row["idUtilisateurVetement"],
                EstHomme = (bool)row["idUtilisateur"],
                EstFemme = (bool)row["idVetement"],
                TypeVetement = (TypeVetement)row["idTypeVetement"],
                CouleurVetement = (Couleur)row["idCouleur"],
                ListeTemperatures = null,
                ListeStyles = null,
                ListeActivites = null
            };
        }
    }
}
