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

        public void Create(Ensemble ensemble)
        {
            try
            {
                connexion = new MySqlConnexion();

                string debutRequete = "INSERT IGNORE INTO Ensembles (nomEnsemble) VALUES ";

                string valeurs = "('" + ensemble.NomEnsemble.Replace("'", "''") + "')";
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
