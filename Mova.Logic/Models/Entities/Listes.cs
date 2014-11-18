                                          using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mova.Logic.Models;
using Mova.Logic.Models.Args;
using Mova.Logic.Models.Entities;

namespace Mova.Logic
{
    public static class Listes
    {
        public static List<StyleVetement> ListeStyles;
        public static List<Activite> ListeActivites;
        public static List<Couleur> ListeCouleurs;
        public static List<Moment> ListeMoments;
        public static List<Temperature> ListeTemperatures;
        public static List<TypeVetement> ListeTypes;

        public static List<UtilisateurEnsemble> ListeEnsemblesUtilisateur;
        public static List<UtilisateurVetements> ListeVetementsUtilisateur;
        public static List<Ensemble> ListeEnsembles;
        public static List<EnsembleVetement> ListeEnsemblesVetements;

        public static Utilisateur UtilisateurConnecte;

        public static List<Vetement> ListeHautsUtilisateur;
        public static List<Vetement> ListeBasUtilisateur;
        public static List<Vetement> ListeSouliersUtilisateur;

        public static List<Vetement> ListeHautsComplet;
        public static List<Vetement> ListeBasComplet;
        public static List<Vetement> ListeSouliersComplet;

        public static List<string> ListeEnsembleAjouter = new List<string>();


        //Concernant les choix faits dans l'écran styliste
        public static List<int> ChoixStyliste;

        public static InfoStylisteArgs InfoStyliste;

        //Mot de passe utilisateur
        public static string pwd;

        public static EnsembleVetement ensembleChoisi;

        //Le nombre d<ensemble que lutilisateur possede dans son garderobe
        public static int NbEnsembleUtilisateur;

        public static int NbHauts;
        public static int NbBas;
        public static int NbSouliers;

        public static bool AjouterEnsemble = false;

        public static List<T> RetourneAleatoire<T>(int nombreMaxRandom, List<T> listeSource)
        {
            
            List<T> listeTemp = new List<T>();
            List<T> ListeSourceCopie = listeSource;
            Random rand = new Random();
            int max = nombreMaxRandom;
            int nbRandom = 0;

            //On trouve le maximum d'élément à retourner (exemple la liste est plus petite que le nombreMaxRandom)
            if(ListeSourceCopie.Count < max){
                max = ListeSourceCopie.Count;
            }

            //On ajoute des éléments à la liste de retour
            for (int i = 0; i < max; i++)
            {

                nbRandom = rand.Next(0, ListeSourceCopie.Count);

                listeTemp.Add(ListeSourceCopie.ElementAt(nbRandom));

                //En retirant ensuite l'élément de la liste, on s'assure de ne pas avoir de répétition
                ListeSourceCopie.RemoveAt(nbRandom);

            }

            return listeTemp;
        }


    }
}
