using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mova.Logic.Models;
using Mova.Logic.Services.Definitions;
using Mova.Logic.Services.Helpers;
using MySql.Data.MySqlClient;

namespace Mova.Logic.Services.MySql
{
    public class MySqlCouleurService : ICouleurService
    {
        private MySqlConnexion connexion;

        public IList<Couleur> RetrieveAll()
        {
            IList<Couleur> result = new List<Couleur>();
            try
            {
                connexion = new MySqlConnexion();

                string requete = "SELECT * FROM Couleurs";

                DataSet dataset = connexion.Query(requete);
                DataTable table = dataset.Tables[0];

                foreach (DataRow couleur in table.Rows)
                {
                    result.Add(ConstructCouleur(couleur));
                }

            }
            catch (MySqlException)
            {
                throw;
            }

            return result;
        }

        private Couleur ConstructCouleur(DataRow row)
        {
            return new Couleur()
            {
                IdCouleur = (int)row["idCouleur"],
                NomCouleur = (string)row["nomCouleur"]
            };

        }

        public void Create(Couleur couleur)
        {
            try
            {
                connexion = new MySqlConnexion();

                string debutRequete = "INSERT IGNORE INTO Couleurs (nomCouleur) VALUES ";


                string valeurs = "('" + couleur.NomCouleur.Replace("'", "''") + "')";
                string requete = debutRequete + valeurs;

                DataSet dataset = connexion.Query(requete);
            }
            catch (MySqlException)
            {
                throw;
            }
        }
    }
}
