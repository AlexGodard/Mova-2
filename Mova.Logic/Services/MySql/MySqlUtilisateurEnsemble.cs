﻿using System;
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
    public class MySqlUtilisateurEnsemble : IUtilisateurEnsembleService
    {
        private MySqlConnexion connexion;

        public IList<UtilisateurEnsemble> RetrieveEnsembleUtilisateur(RetrieveUtilisateurEnsembleArgs args)
        {
            IList<UtilisateurEnsemble> result = new List<UtilisateurEnsemble>();
            try
            {
                connexion = new MySqlConnexion();

                string requete = "SELECT * FROM UtilisateurEnsemble";

                DataSet dataset = connexion.Query(requete);
                DataTable table = dataset.Tables[0];

                foreach (DataRow utilisateurensemble in table.Rows)
                {
                    result.Add(ConstructUtilisateurEnsemble(utilisateurensemble));
                }

            }
            catch (MySqlException)
            {
                throw;
            }

            return result;
        }


        public IList<UtilisateurEnsemble> RetrieveEnsembleUtilisateurPrecis()
        {
            IList<UtilisateurEnsemble> result = new List<UtilisateurEnsemble>();
            try
            {
                connexion = new MySqlConnexion();

                string requete = "SELECT idUtilisateurEnsemble FROM UtilisateursEnsembles WHERE idUtilisateur = " + Listes.UtilisateurConnecte.IdUtilisateur;

                DataSet dataset = connexion.Query(requete);
                DataTable table = dataset.Tables[0];

                foreach (DataRow utilisateurensemble in table.Rows)
                {
                    result.Add(ConstructUtilisateurEnsemble(utilisateurensemble));
                }

            }
            catch (MySqlException)
            {
                throw;
            }

            return result;
        }
 

        private UtilisateurEnsemble ConstructUtilisateurEnsemble(DataRow row)
        {
            return new UtilisateurEnsemble()
            {
                /*IdTemperature = (int)row["idTemperature"],
                NomClimat = (string)row["nomClimat"]*/
            };

        }



    }
}
