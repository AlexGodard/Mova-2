using Mova.Logic.Models;
using Mova.Logic.Models.Args;
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
    public class MySqlTemperatureService : ITemperatureService
    {
        private MySqlConnexion connexion;
        public IList<Temperature> RetrieveAll()
        {
            IList<Temperature> result = new List<Temperature>();
            try
            {
                connexion = new MySqlConnexion();

                string requete = "SELECT * FROM Temperatures";

                DataSet dataset = connexion.Query(requete);
                DataTable table = dataset.Tables[0];

                foreach (DataRow temperatures in table.Rows)
                {
                    result.Add(ConstructTemperatures(temperatures));
                }

            }
            catch (MySqlException)
            {
                throw;
            }

            return result;
        }

        private Temperature ConstructTemperatures(DataRow row)
        {
            return new Temperature()
            {
                IdTemperature = (int)row["idTemperature"],
                NomClimat = (string)row["nomClimat"]
            };

        }

        public void Create(Temperature temperature)
        {
            try
            {
                connexion = new MySqlConnexion();

                string debutRequete = "INSERT IGNORE INTO Temperatures (nomClimat) VALUES ";

                string valeurs = "('" + temperature.NomClimat.Replace("'", "''") + "')";
                string requete = debutRequete + valeurs;

                DataSet dataset = connexion.Query(requete);
            }
            catch (MySqlException)
            {
                throw;
            }
        }

        public void Update(Temperature temperature, string newTemperature)
        {
            try
            {
                connexion = new MySqlConnexion();

                string requete = "UPDATE Temperatures SET nomClimat = '" + newTemperature.Replace("'", "''") + "' WHERE nomClimat = '" + temperature.NomClimat.Replace("'", "''") + "'";

                DataSet dataset = connexion.Query(requete);
            }
            catch (MySqlException)
            {
                throw;
            }
        }

        public void Delete(Temperature temperature)
        {
            try
            {
                connexion = new MySqlConnexion();

                string requete = "DELETE FROM Temperatures WHERE nomClimat = '" + temperature.NomClimat.Replace("'", "''") + "'";

                DataSet dataset = connexion.Query(requete);
            }
            catch (MySqlException)
            {
                throw;
            }
        }
    }
}
