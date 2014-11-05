using Mova.Logic.Models;
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
    public class MySqlMomentService : IMomentService
    {

        private MySqlConnexion connexion;

        public IList<Moment> RetrieveAll()
        {
            IList<Moment> result = new List<Moment>();
            try
            {
                connexion = new MySqlConnexion();

                string requete = "SELECT * FROM moments";

                DataSet dataset = connexion.Query(requete);
                DataTable table = dataset.Tables[0];

                foreach (DataRow activite in table.Rows)
                {
                    result.Add(ConstructMoment(activite));
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
        private Moment ConstructMoment(DataRow row)
        {
            return new Moment()
            {
                IdMoment = (int)row["idMoment"],
                NomMoment = (string)row["nomMoment"],
                //heureDebut = (DateTime)row["heureDebut"],
                //heureFin = (DateTime)row["heureFin"]
            };

        }

    }
}
