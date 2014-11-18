using Mova.Logic.Models;
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

namespace Mova.Logic.Services.MySql
{

    /// <summary>
    /// Classe MySql pour les ensembles
    /// </summary>
    public class MySqlEnsembleService : IEnsembleService
    {
        private MySqlConnexion connexion;

        public int Create(Ensemble ensemble)
        {
            try
            {
                connexion = new MySqlConnexion();

                string debutRequete = "INSERT IGNORE INTO Ensembles (nomEnsemble) VALUES ";


                string valeurs = "('" + ensemble.NomEnsemble.Replace("'", "''") + "');";
                string requete = debutRequete + valeurs;
                
                connexion.Query(requete);
                DataSet dataset = connexion.Query("SELECT MAX(idEnsemble) FROM Ensembles");

                return (int)dataset.Tables[0].Rows[0].ItemArray[0];
            }
            catch (MySqlException e)
            {
                throw e;
            }
        }

        public int Create()
        {
            try
            {
                connexion = new MySqlConnexion();

                string debutRequete = "INSERT IGNORE INTO Ensembles (nomEnsemble) VALUES ";


                /*string valeurs = "('" + ensemble.NomEnsemble.Replace("'", "''") + "');";*/
                string requete = debutRequete; /*+ valeurs;*/

                connexion.Query(requete);
                DataSet dataset = connexion.Query("SELECT MAX(idEnsemble) FROM Ensembles");

                return (int)dataset.Tables[0].Rows[0].ItemArray[0];
            }
            catch (MySqlException e)
            {
                throw e;
            }
        }

        public IList<Ensemble> RetrieveAll()
        {
            IList<Ensemble> result = new List<Ensemble>();
            try
            {
                connexion = new MySqlConnexion();

                string requete = "SELECT * FROM Ensembles";

                DataSet dataset = connexion.Query(requete);
                DataTable table = dataset.Tables[0];

                foreach (DataRow ensemble in table.Rows)
                {
                    result.Add(ConstructEnsemble(ensemble));
                }

            }
            catch (MySqlException)
            {
                throw;
            }

            return result;
        }

        private Ensemble ConstructEnsemble(DataRow row)
        {
            return new Ensemble()
            {
                IdEnsemble = (int)row["idEnsemble"],
                NomEnsemble = (string)row["nomEnsemble"]
            };

        }
    }
}
