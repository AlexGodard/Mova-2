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
                        sb.Append(args.IdMoment.ToString());
                    }
                    else if (args.IdMoment > 0)
                    {
                        sb.Append(" AND m.idMoment=");
                        sb.Append(args.IdMoment.ToString());
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
                result = CreerEnsembles(lstVetements);


                //return result;
                return result;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ensemble"></param>
        /// <returns></returns>
        private EnsembleVetement ConstructEnsemble(DataRow ensemble)
        {
            return new EnsembleVetement();
        
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
        private List<EnsembleVetement> CreerEnsembles(List<Vetement> l)
        {
        
            //Les listes suivantes sont utiles
            List<Vetement> lstChest = new List<Vetement>();
            List<Vetement> lstLeg = new List<Vetement>();
            List<Vetement> lstFeet = new List<Vetement>();
            List<EnsembleVetement> lstEnsemble = new List<EnsembleVetement>();

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
                    lstEnsemble.Add(new EnsembleVetement(){
                    
                        ListeVetements = lstTemp

                    });
                 
                 }             
              }
           }

           List<EnsembleVetement> source = lstEnsemble;
           var rnd = new Random();
           var result = source.OrderBy(item => rnd.Next());
           lstEnsemble = result.ToList<EnsembleVetement>();

           return lstEnsemble;
        
        }
    }
}
