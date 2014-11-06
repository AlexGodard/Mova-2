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
    public class MySqlVetementService : IVetementService
    {

        private MySqlConnexion connexion;

        public void Create(Vetement vetement)
        {
            try
            {
                connexion = new MySqlConnexion();

                string debutRequete = "INSERT INTO Vetements (idTypeVetement,idCouleur,nomVetement,imageURL,prix,estHomme,estFemme) VALUES ";
                // IL EST NÉCESSAIRE DE CONVERTIR LES BOOL EN INT, CAR LA BD NE COMPREND PAS TRUE ET FALSE, DÛ AU FAIT QUE LES CHAMP SONT DES TINYINT

                string valeurs = "(" + RetrieveIdType(vetement.TypeVetement.NomType) + "," + RetrieveIdCouleur(vetement.CouleurVetement.NomCouleur) + ",'" + vetement.NomVetement + "','" + vetement.ImageURL + "'," + vetement.Prix + "," + vetement.EstHomme + "," + vetement.EstFemme + ")";
                string requete = debutRequete + valeurs;

                DataSet dataset = connexion.Query(requete);

                // Ici, on va aussi devoir prendre les listes et ajouter leur contenu dans la BD.
                // On commence par la liste des activités;

                for (int i = 0; i < vetement.ListeActivites.Count(); i++)
                {
                    debutRequete = "INSERT INTO ActivitesVetements (idActivite, idVetement) VALUES ";
                    valeurs = "( (SELECT idActivite FROM Activites WHERE nomActivite = '" + vetement.ListeActivites[i].NomActivite.Replace("'", "''") + "'), " +
                    " (SELECT idVetement FROM Vetements WHERE nomVetement = '" + vetement.NomVetement.Replace("'", "''") + "') );";
                    requete = debutRequete + valeurs;

                    dataset = connexion.Query(requete);
                }

                for (int i = 0; i < vetement.ListeStyles.Count(); i++)
                {
                    debutRequete = "INSERT INTO StylesVetements (idStyle, idVetement) VALUES ";
                    valeurs = "( (SELECT idStyle FROM Styles WHERE nomStyle = '" + vetement.ListeStyles[i].NomStyle.Replace("'", "''") + "'), " +
                    " (SELECT idVetement FROM Vetements WHERE nomVetement = '" + vetement.NomVetement.Replace("'", "''") + "') );";
                    requete = debutRequete + valeurs;

                    dataset = connexion.Query(requete);
                }

                for (int i = 0; i < vetement.ListeTemperatures.Count(); i++)
                {
                    debutRequete = "INSERT INTO VetementsTemperatures (idTemperature, idVetement) VALUES ";
                    valeurs = "( (SELECT idTemperature FROM Temperatures WHERE nomClimat = '" + vetement.ListeTemperatures[i].NomClimat.Replace("'", "''") + "'), " +
                    " (SELECT idVetement FROM Vetements WHERE nomVetement = '" + vetement.NomVetement.Replace("'", "''") + "') );";
                    requete = debutRequete + valeurs;

                    dataset = connexion.Query(requete);
                }

                
            }
            catch (MySqlException)
            {
                throw;
            }
        }

        public int RetrieveIdType(string nomType)
        {
            try
            {
                connexion = new MySqlConnexion();

                string requete = "SELECT idTypeVetement FROM TypesVetements WHERE nomType = '" + nomType + "'";
                //string requete = "SELECT * FROM TypesVetements";

                DataSet dataset = connexion.Query(requete);
                DataTable table = dataset.Tables[0];

                //Ici nous devrions avoir un seul type et si on va dans le foreach alors on retourne le premier (et le seul) de la réponse
                foreach (DataRow idTypeVetement in table.Rows)
                {
                    return (int)idTypeVetement["idTypeVetement"];
                }

            }
            catch (MySqlException)
            {
                throw;
            }

            //Si on se rend ici, on retourne un utilisateur vide
            return 0;
        }

        public int RetrieveIdCouleur(string nomCouleur)
        {
            try
            {
                connexion = new MySqlConnexion();

                string requete = "SELECT idCouleur FROM Couleurs WHERE nomCouleur = '" + nomCouleur + "'";
                //string requete = "SELECT * FROM TypesVetements";

                DataSet dataset = connexion.Query(requete);
                DataTable table = dataset.Tables[0];

                //Ici nous devrions avoir un seul type et si on va dans le foreach alors on retourne le premier (et le seul) de la réponse
                foreach (DataRow idCouleur in table.Rows)
                {
                    return (int)idCouleur["idCouleur"];
                }

            }
            catch (MySqlException)
            {
                throw;
            }

            
            return 0;
        }

        public IList<int> RetrieveIdTypeAll()
        {
          IList<UtilisateurVetements> listeTemp = Listes.ListeVetementsUtilisateur;
          IList<int> result = new List<int>();
            try
            {
                connexion = new MySqlConnexion();

                foreach(UtilisateurVetements v in listeTemp)
                {
                   string requete = "SELECT idTypeVetement FROM Vetements WHERE idVetement = '" + v.IdVetement + "'";
                   DataSet dataset = connexion.Query(requete);
                   DataTable table = dataset.Tables[0];
                   foreach (DataRow vetement in table.Rows)
                   {
                       result.Add((int)vetement["idTypeVetement"]);
                   }
                }             

            }
            catch (MySqlException)
            {
                throw;
            }

            //Si on se rend ici, on retourne un utilisateur vide
            return result;
        }


    }
}