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

        private Activite ConstructActivite(DataRow row)
        {
            return new Activite()
            {
                IdActivite = (int)row["idActivite"],
                NomActivite = (string)row["nomActivite"]
            };

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private Activite ConstructActiviteNew(DataRow row)
        {
            return new Activite()
            {
                IdActivite = (int)row["idActivite"],
                NomActivite = (string)row["nomActivite"],
                EstOuvrable = (bool)row["estOuvrable"],
                EstConge = (bool)row["estConge"]
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

                //Lors de la semaine, il est possible de vouloir tout faire ce qui n'est pas considéré comme étant congé mais lorsque ce n'est pas un jour ouvrable on ne veut pas avoir les activités habituelles de 9 à 5
                if(estOuvrable)
                    requete = "SELECT DISTINCT(a.idActivite), a.nomActivite FROM Activites a INNER JOIN ActivitesMoments am ON am.idActivite = a.idActivite INNER JOIN Moments m ON m.idMoment = am.idMoment INNER JOIN ActivitesVetements av ON av.idActivite = a.idActivite INNER JOIN Vetements v ON v.idVetement = av.idVetement WHERE m.idMoment = " + idMoment;
                else
                    requete = "SELECT DISTINCT(a.idActivite), a.nomActivite FROM Activites a INNER JOIN ActivitesMoments am ON am.idActivite = a.idActivite INNER JOIN Moments m ON m.idMoment = am.idMoment INNER JOIN ActivitesVetements av ON av.idActivite = a.idActivite INNER JOIN Vetements v ON v.idVetement = av.idVetement WHERE m.idMoment = " + idMoment;
                

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
                                  ", estConge = " + estConge + " WHERE nomActivite = '" + activite.NomActivite.Replace("'", "''") + "'";

                DataSet dataset = connexion.Query(requete);

                // On doit aller chercher l'id de l'activité qu'on vient de modifier

                requete = "SELECT MAX(idActivite) FROM Activites WHERE nomActivite = '" + activite.NomActivite.Replace("'", "''") + "'";
                dataset = connexion.Query(requete);

                int idActivite = (int)dataset.Tables[0].Rows[0].ItemArray[0];

                // On update maintenant les moments

                // ON DELETE LES MOMENTS LIÉS À L'ACTIVITÉ POUR ENSUITE LES RÉAJOUTER, CAR IL EST IMPOSSIBLE DE FAIRE UN UPDATE
                connexion.Query("DELETE FROM ActivitesMoments WHERE idActivite = " + idActivite);

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

        public Activite RetrieveDetailsActivite(string nomActivite)
        {
            try
            {
                connexion = new MySqlConnexion();

                string requete = "SELECT * FROM Activites WHERE nomActivite = '" + nomActivite.Replace("'", "''") + "'";

                DataSet dataset = connexion.Query(requete);
                DataTable table = dataset.Tables[0];

                return ConstructActiviteNew(table.Rows[0]);
                
            }
            catch (MySqlException)
            {
                throw;
            }
        }

        #endregion
    }
}
