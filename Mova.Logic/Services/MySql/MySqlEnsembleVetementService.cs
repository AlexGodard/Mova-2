﻿using Mova.Logic.Models;
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

        public IList<EnsembleVetement> RetrieveSelection(InfoStylisteArgs args)
        {

            List<Vetement> lstVetements = new List<Vetement>();
            IList<EnsembleVetement> result = new List<EnsembleVetement>();
            int nbArgumentsValides = 0;

            try
            {
                connexion = new MySqlConnexion();

                StringBuilder sb = new StringBuilder();

                sb.Append("SELECT DISTINCT(v.idVetement), v.idTypeVetement, v.nomVetement, v.imageURL, v.estHomme, v.estFemme, v.prix, v.idCouleur FROM Vetements v INNER JOIN ActivitesVetements av ON av.idVetement = v.idVetement INNER JOIN Activites a ON a.idActivite = av.idActivite INNER JOIN ActivitesMoments am ON am.idActivite = a.idActivite INNER JOIN Moments m ON m.idMoment = am.idMoment INNER JOIN StylesVetements sv ON sv.idVetement = v.idVetement INNER JOIN Styles s ON s.idStyle = sv.idStyle INNER JOIN VetementsTemperatures vt ON vt.idVetement = v.idVetement INNER JOIN Temperatures t ON t.idTemperature ");

                //Pour Activité
                if (args.IdActivite > 0)
                {
                    sb.Append("WHERE a.idActivite=");
                    sb.Append(args.IdActivite.ToString());
                    nbArgumentsValides++;
                }

                //Pour Style
                if (args.IdStyle > 0 && nbArgumentsValides == 0)
                {
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

        public IList<EnsembleVetement> RetrieveEnsemblesUtilisateur(InfoStylisteArgs args)
        {

            List<Vetement> lstVetements = new List<Vetement>();
            IList<EnsembleVetement> result = new List<EnsembleVetement>();
            int nbArgumentsValides = 0;

            try
            {
                connexion = new MySqlConnexion();

                StringBuilder sb = new StringBuilder();

                sb.Append("SELECT DISTINCT(v.idVetement), v.idTypeVetement, v.nomVetement, v.imageURL, v.estHomme, v.estFemme, v.prix, v.idCouleur FROM Vetements v INNER JOIN EnsemblesVetements ev ON ev.idVetement=v.idVetement INNER JOIN UtilisateursEnsembles ue ON ue.idEnsemble=ev.idEnsemble INNER JOIN ActivitesVetements av ON av.idVetement = v.idVetement INNER JOIN Activites a ON a.idActivite = av.idActivite INNER JOIN ActivitesMoments am ON am.idActivite = a.idActivite INNER JOIN Moments m ON m.idMoment = am.idMoment INNER JOIN StylesVetements sv ON sv.idVetement = v.idVetement INNER JOIN Styles s ON s.idStyle = sv.idStyle INNER JOIN VetementsTemperatures vt ON vt.idVetement = v.idVetement INNER JOIN Temperatures t ON t.idTemperature ");

                //Pour Activité
                if (args.IdActivite > 0)
                {
                    sb.Append("WHERE a.idActivite=");
                    sb.Append(args.IdActivite.ToString());
                    nbArgumentsValides++;
                }

                //Pour Style
                if (args.IdStyle > 0 && nbArgumentsValides == 0)
                {
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

                //Pour Moment
                if (Listes.UtilisateurConnecte.IdUtilisateur > 0 && nbArgumentsValides == 0)
                {
                    sb.Append(" WHERE ue.idUtilisateur=");
                    sb.Append(Listes.UtilisateurConnecte.IdUtilisateur.ToString());
                }
                else if (Listes.UtilisateurConnecte.IdUtilisateur > 0)
                {
                    sb.Append(" AND ue.idUtilisateur=");
                    sb.Append(Listes.UtilisateurConnecte.IdUtilisateur.ToString());
                }

                sb.Append(" ORDER BY ev.idEnsemble ASC, v.idTypeVetement ASC");

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
            result = CreerEnsemblesVetementsUtilisateur(lstVetements);


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

        public void Create(EnsembleVetement ensembleVetement)
        {
            connexion = new MySqlConnexion();

            foreach (Vetement vetement in ensembleVetement.ListeVetements)
            {
                //On commence par créer un ensemble vide (pour avoir le ID)
                string debutRequete = "INSERT IGNORE INTO EnsemblesVetements (idEnsemble, idVetement) VALUES ";
                string valeurs = "(" + ensembleVetement.IdEnsemble + ", " + vetement.IdVetement + ")";
                string requete = debutRequete + valeurs;

                DataSet dataset = connexion.Query(requete);
            }
        }

        public void CreateEnsemble(IList<Vetement> lv, int idEnsemble)
        {
            connexion = new MySqlConnexion();

            foreach (Vetement v in lv)
            {
                //On commence par créer un ensemble vide (pour avoir le ID)
                string debutRequete = "INSERT IGNORE INTO EnsemblesVetements (idEnsemble, idVetement) VALUES ";
                string valeurs = "(" + idEnsemble + ", " + v.IdVetement + ")";
                string requete = debutRequete + valeurs;

                DataSet dataset = connexion.Query(requete);
            }
        }

        public IList<Vetement> RetrieveVetementsEnsembles(IList<UtilisateurEnsemble> listeEnsembles)
        {
            IList<Vetement> result = new List<Vetement>();
            try
            {
                connexion = new MySqlConnexion();

                foreach (UtilisateurEnsemble e in listeEnsembles)
                {
                    string requete = "SELECT v.*,ev.idEnsemble FROM Vetements v INNER JOIN EnsemblesVetements ev ON " +
                                   "v.idVetement = ev.idVetement WHERE ev.idEnsemble = " + e.idEnsemble + "ORDER BY idTypeVetement ASC";

                    DataSet dataset = connexion.Query(requete);
                    DataTable table = dataset.Tables[0];

                    foreach (DataRow vetement in table.Rows)
                    {
                        result.Add(ConstructVetement(vetement));
                    }
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

            Vetement v = new Vetement()
            {

                IdVetement = (int)vetement["idVetement"],
                NomVetement = (string)vetement["nomVetement"],
                ImageURL = (string)vetement["imageURL"],
                //Prix = (double)vetement["prix"],
                EstHomme = (bool)vetement["estHomme"],
                EstFemme = (bool)vetement["estFemme"],
                CouleurVetement = new Couleur((int)vetement["idCouleur"]),
                TypeVetement = new TypeVetement()
                {
                    IdType = (int)vetement["idTypeVetement"]
                }
            };

            return v;
        }

        private Vetement ConstructVetementEnsemble(DataRow vetement)
        {

            Vetement v = new Vetement()
            {

                IdVetement = (int)vetement["idVetement"],
                NomVetement = (string)vetement["nomVetement"],
                ImageURL = (string)vetement["imageURL"],
                //Prix = (double)vetement["prix"],
                EstHomme = (bool)vetement["estHomme"],
                EstFemme = (bool)vetement["estFemme"],
                TypeVetement = new TypeVetement()
                {
                    IdType = (int)vetement["idTypeVetement"]
                }
            };

            return v;
        }

        /// <summary>
        /// 
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


            foreach (Vetement vc in lstChest)
            {
                foreach (Vetement vb in lstLeg)
                {
                    foreach (Vetement vs in lstFeet)
                    {

                        List<Vetement> lstTemp = new List<Vetement>();
                        lstTemp.Add(vc);
                        lstTemp.Add(vb);
                        lstTemp.Add(vs);

                        //On ajoute un nouvel EnsembleVetement à la liste
                        lstEnsembleVetement.Add(new EnsembleVetement()
                        {

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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="l"></param>
        /// <returns></returns>
        private List<EnsembleVetement> CreerEnsemblesVetementsUtilisateur(List<Vetement> l)
        {

            List<EnsembleVetement> lstEnsembleVetement = new List<EnsembleVetement>();
            List<Vetement> lstTemp = new List<Vetement>();
            bool match = false;

            //Exploded view of the problem
            foreach (Vetement v in l)
            {

                lstTemp.Add(v);

                switch (lstTemp.Count){
                
                    case 1:
                        match = true;
                        break;

                    case 2:
                        match = ((int)(lstTemp[lstTemp.Count - 1].TypeVetement.IdType)) > ((int)(lstTemp[lstTemp.Count - 2].TypeVetement.IdType));
                        break;
                    case 3:
                        match = ((int)(lstTemp[lstTemp.Count - 1].TypeVetement.IdType)) > ((int)(lstTemp[lstTemp.Count - 2].TypeVetement.IdType));
                        
                        break;
                    default:
                        match = false;
                        lstTemp = new List<Vetement>();
                        break;
                
                }

                if (match && lstTemp.Count >= 3)
                {

                    lstEnsembleVetement.Add(new EnsembleVetement()
                    {

                        ListeVetements = lstTemp

                    });

                    lstTemp = new List<Vetement>();

                }
                else if(!match){
                    lstTemp = new List<Vetement>();
                    lstTemp.Add(v);
                }


            }


            return lstEnsembleVetement;
        }


    }
}
