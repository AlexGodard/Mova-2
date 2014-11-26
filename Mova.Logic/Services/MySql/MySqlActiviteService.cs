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
        public IList<Activite> RetrievePourMoment(int idMoment, bool estOuvrable)
        {
            IList<Activite> result = new List<Activite>();
            try
            {
                connexion = new MySqlConnexion();
                string requete;

                if(estOuvrable)
                    requete = "SELECT DISTINCT(a.idActivite), a.nomActivite FROM Activites a INNER JOIN ActivitesMoments am ON am.idActivite = a.idActivite INNER JOIN Moments m ON m.idMoment = am.idMoment INNER JOIN ActivitesVetements av ON av.idActivite = a.idActivite INNER JOIN Vetements v ON v.idVetement = av.idVetement WHERE m.idMoment = " + idMoment + " AND estOuvrable = TRUE";
                else
                    requete = "SELECT DISTINCT(a.idActivite), a.nomActivite FROM Activites a INNER JOIN ActivitesMoments am ON am.idActivite = a.idActivite INNER JOIN Moments m ON m.idMoment = am.idMoment INNER JOIN ActivitesVetements av ON av.idActivite = a.idActivite INNER JOIN Vetements v ON v.idVetement = av.idVetement WHERE m.idMoment = " + idMoment + " AND estConge = TRUE";

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

        public void Create(Activite activite, bool estOuvrable, bool estConge, List<Moment> listeMomentsSelectionnes)
        {
            try
            {
                connexion = new MySqlConnexion();

                string debutRequete = "INSERT IGNORE INTO Activites (nomActivite, estOuvrable, estConge) VALUES ";


                string valeurs = "('" + activite.NomActivite.Replace("'", "''") + "', " + estOuvrable + ", " + estConge + ")";
                string requete = debutRequete + valeurs;

                DataSet dataset = connexion.Query(requete);

                dataset = connexion.Query("SELECT MAX(idActivite) FROM Activites");

                foreach (Moment moment in listeMomentsSelectionnes)
                {
                    string debut = "INSERT IGNORE INTO ActivitesMoments (idActivite, idMoment) VALUES ";
                    string reste = "( " + (int)dataset.Tables[0].Rows[0].ItemArray[0] + ", " +
                                   "(SELECT idMoment FROM Moments WHERE nomMoment = '" + moment.NomMoment + "') );";

                    DataSet datasetQuery = connexion.Query(debut + reste);
                }
                
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

        public void Update(Activite activite, string newActivite, bool estOuvrable, bool estConge, List<Moment> listeMomentsSelectionnes)
        {
            try
            {
                connexion = new MySqlConnexion();

                string requete = "UPDATE Activites SET nomActivite = '" + newActivite.Replace("'", "''") + "', estOuvrable = " + estOuvrable +
                                  ", estOuvrable = " + estOuvrable + " WHERE nomActivite = '" + activite.NomActivite.Replace("'", "''") + "'";

                DataSet dataset = connexion.Query(requete);

                // On doit aller chercher l'id de l'activité qu'on vient de modifier
                dataset = connexion.Query("SELECT MAX(idActivite) FROM Activites WHERE nomActivite = '" + activite.NomActivite.Replace("'", "''") + "')");

                int idActivite = (int)dataset.Tables[0].Rows[0].ItemArray[0];

                // On update maintenant les moments

                // ON DELETE LES MOMENTS LIÉS À L'ACTIVITÉ POUR ENSUITE LES RÉAJOUTER, CAR IL EST IMPOSSIBLE DE FAIRE UN UPDATE
                connexion.Query("DELETE FROM Moments WHERE idActivite = " + idActivite + " )");

                // On insert maintenant les bon moments pour cette activité
                foreach (Moment moment in listeMomentsSelectionnes)
                {
                    string debut = "INSERT IGNORE INTO ActivitesMoments (idActivite, idMoment) VALUES ";
                    string reste = "( " + idActivite + ", " +
                                   "(SELECT idMoment FROM Moments WHERE nomMoment = '" + moment.NomMoment + "') );";

                    DataSet datasetQuery = connexion.Query(debut + reste);
                }
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
