using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mova.Logic.Models;
using Mova.Logic.Models.Args;
using Mova.Logic.Services.Definitions;
using Mova.Logic.Services.Helpers;
using MySql.Data.MySqlClient;

namespace Mova.Logic.Services.MySql
{
    public class MySqlActiviteService : IActiviteService
    {
        private MySqlConnexion connexion;

        public IList<Activite> RetrieveAll()
        {
            IList<Activite> result = new List<Activite>();
            try
            {
                connexion = new MySqlConnexion();

                string requete = "SELECT * FROM activites";

                DataSet dataset = connexion.Query(requete);
                DataTable table = dataset.Tables[0];

                foreach (DataRow activite in table.Rows)
                {
                    result.Add(ConstructActivite(activite));
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
        private Activite ConstructActivite(DataRow row)
        {
            return new Activite()
            {
                IdActivite = (int)row["idActivite"],
                NomActivite = (string)row["nomActivite"]
            };

        }

        #region IActiviteService Membres

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<Activite> RetrievePourMoment(int idMoment)
        {
            IList<Activite> result = new List<Activite>();
            try
            {
                connexion = new MySqlConnexion();

                string requete = "SELECT DISTINCT(a.idActivite), a.nomActivite FROM Activites a INNER JOIN ActivitesMoments am ON am.idActivite = a.idActivite INNER JOIN Moments m ON m.idMoment = am.idMoment INNER JOIN ActivitesVetements av ON av.idActivite = a.idActivite INNER JOIN Vetements v ON v.idVetement = av.idVetement WHERE m.idMoment = " + idMoment;

                DataSet dataset = connexion.Query(requete);
                DataTable table = dataset.Tables[0];

                foreach (DataRow activite in table.Rows)
                {
                    result.Add(ConstructActivite(activite));
                }

            }
            catch (MySqlException)
            {
                throw;
            }

            return result;
        }

        public void Create(Activite activite)
        {
            try
            {
                connexion = new MySqlConnexion();

                string debutRequete = "INSERT IGNORE INTO Activites (nomActivite) VALUES ";


                string valeurs = "('" + activite.NomActivite.Replace("'", "''") + "')";
                string requete = debutRequete + valeurs;

                DataSet dataset = connexion.Query(requete);
            }
            catch (MySqlException)
            {
                throw;
            }
        }

        public void Update(Activite activite, string newActivite)
        {
            try
            {
                connexion = new MySqlConnexion();

                string requete = "UPDATE Activites SET nomActivite = '" + newActivite.Replace("'", "''") + "' WHERE nomActivite = '" + activite.NomActivite.Replace("'", "''") + "'";

                DataSet dataset = connexion.Query(requete);
            }
            catch (MySqlException)
            {
                throw;
            }
        }

        public void Delete(Activite activite)
        {
            try
            {
                connexion = new MySqlConnexion();

                string requete = "DELETE FROM Activites WHERE nomActivite = '" + activite.NomActivite.Replace("'", "''") + "'";

                DataSet dataset = connexion.Query(requete);
            }
            catch (MySqlException)
            {
                throw;
            }
        }

        #endregion
    }
}
