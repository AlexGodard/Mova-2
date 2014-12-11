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
    public class MySqlStyleService : IStyleService
    {
        private MySqlConnexion connexion;
        public IList<StyleVetement> RetrieveAll()
        {
            IList<StyleVetement> result = new List<StyleVetement>();
            try
            {
                connexion = new MySqlConnexion();

                string requete = "SELECT * FROM Styles";
                

                DataSet dataset = connexion.Query(requete);
                DataTable table = dataset.Tables[0];

                foreach (DataRow styles in table.Rows)
                {
                    result.Add(ConstructStyles(styles));
                }

            }
            catch (MySqlException)
            {
                throw;
            }

            return result;
        }

       /* public StyleVetement Retrieve(RetrieveStyleArgs args)
        {
            try
            {
                connexion = new MySqlConnexion();

                string requete = String.Format("SELECT * FROM Vetements");
                DataSet dataset = connexion.Query(requete);

                return ConstructStyles(dataset.Tables[0].Rows[0]);
            }
            catch (MySqlException)
            {
                throw;
            }
        }*/



        private StyleVetement ConstructStyles(DataRow row)
        {
            return new StyleVetement()
            {
                IdStyle = (int)row["idStyle"],
                NomStyle = (string)row["nomStyle"]
            };

        }

        public void Create(StyleVetement styleVetement)
        {
            try
            {
                connexion = new MySqlConnexion();

                string debutRequete = "INSERT IGNORE INTO Styles (nomStyle) VALUES ";


                string valeurs = "('" + styleVetement.NomStyle.Replace("'", "''") + "')";
                string requete = debutRequete + valeurs;

                DataSet dataset = connexion.Query(requete);
            }
            catch (MySqlException)
            {
                throw;
            }
        }

        public void Update(StyleVetement styleVetement, string newStyleVetement)
        {
            try
            {
                connexion = new MySqlConnexion();

                string requete = "UPDATE Styles SET nomStyle = '" + newStyleVetement.Replace("'", "''") + "' WHERE nomStyle = '" + styleVetement.NomStyle.Replace("'", "''") + "'";

                DataSet dataset = connexion.Query(requete);
            }
            catch (MySqlException)
            {
                throw;
            }
        }

        public void Delete(StyleVetement styleVetement)
        {
            try
            {
                connexion = new MySqlConnexion();

                string requete = "DELETE FROM Styles WHERE nomStyle = '" + styleVetement.NomStyle.Replace("'", "''") + "'";

                DataSet dataset = connexion.Query(requete);
            }
            catch (MySqlException)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public IList<StyleVetement> RetrieveSpecific(InfoStylisteArgs args)
        {
            IList<StyleVetement> result = new List<StyleVetement>();
            try
            {
                connexion = new MySqlConnexion();

                string requete = "SELECT DISTINCT(s.idStyle), s.nomStyle  FROM Styles s INNER JOIN StylesVetements sv ON sv.idStyle = s.idStyle INNER JOIN Vetements v ON v.idVetement = sv.idVetement INNER JOIN ActivitesVetements av ON av.idVetement = v.idVetement INNER JOIN Activites a ON a.idActivite = av.idActivite INNER JOIN ActivitesMoments am ON am.idActivite = a.idActivite WHERE av.idActivite = " + args.IdActivite.ToString() + " AND am.idMoment = " + args.IdMoment;
                
                DataSet dataset = connexion.Query(requete);
                DataTable table = dataset.Tables[0];

                foreach (DataRow styles in table.Rows)
                {
                    result.Add(ConstructStyles(styles));
                }

            }
            catch (MySqlException)
            {
                throw;
            }

            return result;
        }
    }
}
