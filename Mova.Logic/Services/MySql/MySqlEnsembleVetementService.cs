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
    public class MySqlEnsembleVetementService : IEnsembleVetementService
    {

        private MySqlConnexion connexion;

        /*public IList<EnsembleVetement> RetrieveAll()
        {
            IList<EnsembleVetement> result = new List<EnsembleVetement>();
            try
            {
                connexion = new MySqlConnexion();

                string requete = "SELECT * FROM EnsemblesVetements";

                DataSet dataset = connexion.Query(requete);
                DataTable table = dataset.Tables[0];

                foreach (DataRow ensembleVetement in table.Rows)
                {
                    result.Add(ConstructEnsembleVetement(ensembleVetement));
                }
            }
            catch (MySqlException)
            {
                throw;
            }

            return result;
        }*/

        public IList<EnsembleVetement> RetrieveSelection(InfoStylisteArgs args)
        {

            List<Vetement> lstVetements = new List<Vetement>();
            IList<EnsembleVetement> result = new List<EnsembleVetement>();
            int nbArgumentsValides = 0;

                try
                {
                    connexion = new MySqlConnexion();

                    StringBuilder sb = new StringBuilder();

                    sb.Append("SELECT DISTINCT(v.idVetement), v.idTypeVetement, v.nomVetement, v.imageURL, v.estHomme, v.estFemme, v.prix FROM Vetements v INNER JOIN ActivitesVetements av ON av.idVetement = v.idVetement INNER JOIN Activites a ON a.idActivite = av.idActivite INNER JOIN ActivitesMoments am ON am.idActivite = a.idActivite INNER JOIN Moments m ON m.idMoment = am.idMoment INNER JOIN StylesVetements sv ON sv.idVetement = v.idVetement INNER JOIN Styles s ON s.idStyle = sv.idStyle INNER JOIN VetementsTemperatures vt ON vt.idVetement = v.idVetement INNER JOIN Temperatures t ON t.idTemperature ");

                    //Pour Activité
                    if (args.IdActivite > 0){
                        sb.Append("WHERE a.idActivite=");
                        sb.Append(args.IdActivite.ToString());
                        nbArgumentsValides++;
                    }

                    //Pour Style
                    if (args.IdStyle > 0 && nbArgumentsValides == 0){
                        sb.Append(" WHERE s.idStyle=");
                        sb.Append(args.IdStyle.ToString());
                        nbArgumentsValides++;
                    }
                    else if (args.IdStyle > 0)
                    {
                        sb.Append(" AND s.idStyle=");
                        sb.Append(args.IdStyle.ToString());
                        nbArgumentsValides++;
                    }
                    
                    //Pour Moment
                    if (args.IdMoment > 0 && nbArgumentsValides == 0)
                    {
                        sb.Append(" WHERE m.idMoment=");
                        sb.Append(args.IdStyle.ToString());
                    }
                    else if (args.IdMoment > 0)
                    {
                        sb.Append(" AND m.idMoment=");
                        sb.Append(args.IdStyle.ToString());
                    }
                    string requete = sb.ToString();
	                                

                    DataSet dataset = connexion.Query(requete);
                    DataTable table = dataset.Tables[0];

                    foreach (DataRow vetement in table.Rows)
                    {
                        lstVetements.Add(ConstructVetement(vetement));
                    }
                }
                catch (MySqlException)
                {
                    throw;
                }


                //On construit des ensembles avec la liste de vêtements
                result = CreerEnsemblesVetements(lstVetements);


                //return result;
                return result;

        }

        //Retourne le id d'un ensemble correctement inséré
        public int InsererEnsemble(EnsembleVetement ev)
        {

            int noEnsemble = 0;

            //On commence par créer un ensemble vide (pour avoir le ID)
            string query = "INSERT INTO Ensembles()VALUES()";
            string query2 = "SELECT idEnsemble FROM Ensembles ORDER BY idEnsemble DESC LIMIT 1";

            try
            {
                connexion = new MySqlConnexion();

                //On ajoute un Ensemble normal sans rien parce qu'on a pas le choix
                DataSet dataset = connexion.Query(query);
                dataset = connexion.Query(query2);
                DataTable table = dataset.Tables[0];
                DataRow dr = table.Rows[0];
                noEnsemble = Convert.ToInt32(dr["idEnsemble"]);

                //Maintenant qu'on a le numéro d'ensemble, on est heureux
                //On insère maintenant les EnsemblesVetements
                
                string query3 = new StringBuilder().Append("INSERT INTO EnsemblesVetements(idEnsemble,idVetement) VALUES (").Append(noEnsemble).Append(",").Append(ev.ListeVetements[0].IdVetement).Append("),(").Append(noEnsemble).Append(",").Append(ev.ListeVetements[1].IdVetement).Append("),(").Append(noEnsemble).Append(",").Append(ev.ListeVetements[2].IdVetement).Append(")").ToString();
                dataset = connexion.Query(query3);
            }
            catch (MySqlException)
            {
                return -1;
            }

            return noEnsemble;
        }
        /*
        private int FindIDEnsemble(EnsembleVetement ev)
        {
        
            string select = "SELECT DISTINCT(idEnsemble) FROM EnsemblesVetements WHERE idVetement IN"

        }
        */
        public void Create(EnsembleVetement ensembleVetement)
        {
            connexion = new MySqlConnexion();

            foreach(Vetement vetement in ensembleVetement.ListeVetements)
            {
                //On commence par créer un ensemble vide (pour avoir le ID)
                string debutRequete = "INSERT IGNORE INTO EnsemblesVetements (idEnsemble, idVetement) VALUES ";
                string valeurs = "(" + ensembleVetement.IdEnsemble + ", " + vetement.IdVetement + ")";
                string requete = debutRequete + valeurs;

                DataSet dataset = connexion.Query(requete);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ensemble"></param>
        /// <returns></returns>
        private EnsembleVetement ConstructEnsembleVetement(DataRow row)
        {
            return new EnsembleVetement()
            {
                IdEnsembleVetement = (int)row["idEnsembleVetement"],
                IdEnsemble = (int)row["idEnsemble"],
                ListeVetements = new List<Vetement>((int)row["idVetement"])
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vetement"></param>
        /// <returns></returns>
        private Vetement ConstructVetement(DataRow vetement)
        {
            
            Vetement v =  new Vetement(){
            
                IdVetement = (int)vetement["idVetement"],
                NomVetement = (string)vetement["nomVetement"],
                ImageURL = (string)vetement["imageURL"],
                //Prix = (double)vetement["prix"],
                EstHomme = (bool)vetement["estHomme"],
                EstFemme = (bool)vetement["estFemme"],
                TypeVetement = new TypeVetement(){
                    IdType = (int)vetement["idTypeVetement"]
                }   
            };


            return v;
        
        }

        /// <summary>
        /// Meilleure façon de faire
        /// </summary>
        /// <param name="l"></param>
        /// <returns></returns>
        private List<EnsembleVetement> CreerEnsemblesVetements(List<Vetement> l)
        {
        
            //Les listes suivantes sont utiles
            List<Vetement> lstChest = new List<Vetement>();
            List<Vetement> lstLeg = new List<Vetement>();
            List<Vetement> lstFeet = new List<Vetement>();
            List<EnsembleVetement> lstEnsembleVetement = new List<EnsembleVetement>();

            //Exploded view of the problem
            foreach (Vetement v in l)
            {
                switch (v.TypeVetement.IdType)
                {
                    case 1:
                        lstChest.Add(v);
                        break;
                    case 2:
                        lstLeg.Add(v);
                        break;
                    case 3:
                        lstFeet.Add(v);
                        break;
                    default:
                        break;
                }
            }

            
           foreach(Vetement vc in lstChest)
           { 
              foreach(Vetement vb in lstLeg)
              { 
                 foreach(Vetement vs in lstFeet)
                 { 
                    
                    List<Vetement> lstTemp = new List<Vetement>();
                    lstTemp.Add(vc);
                    lstTemp.Add(vb);
                    lstTemp.Add(vs);

                    //On ajoute un nouvel EnsembleVetement à la liste
                    lstEnsembleVetement.Add(new EnsembleVetement(){
                    
                        ListeVetements = lstTemp

                    });
                 
                 }             
              }
           }

           List<EnsembleVetement> source = lstEnsembleVetement;
           var rnd = new Random();
           var result = source.OrderBy(item => rnd.Next());
           lstEnsembleVetement = result.ToList<EnsembleVetement>();

           return lstEnsembleVetement;
        }

    }
}
