﻿using Cstj.MvvmToolkit;
using Cstj.MvvmToolkit.Services;
using Mova.Logic;
using Mova.Logic.Models;
using Mova.Logic.Models.Entities;
using Mova.Logic.Services.Definitions;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mova.UI.ViewModel
{
    class RecentsViewModel : BaseViewModel
    {

        private IUtilisateurEnsembleService _utilisateurEnsembleService;
        private ObservableCollection<UtilisateurEnsemble> _utilisateursEnsembles = new ObservableCollection<UtilisateurEnsemble>();

        public int i = 0, j = 0, k = 0;

        // On ajoute tout les vêtements dans des listes de vêtements temporaires
        List<Vetement> listeHauts = new List<Vetement>();
        List<Vetement> listeBas = new List<Vetement>();
        List<Vetement> listeChaussures = new List<Vetement>();

        /// <summary>
        /// 
        /// </summary>
        public RecentsViewModel()
        {
            _utilisateurEnsembleService = ServiceFactory.Instance.GetService<IUtilisateurEnsembleService>();

            UtilisateursEnsembles = new ObservableCollection<UtilisateurEnsemble>(ServiceFactory.Instance.GetService<IUtilisateurEnsembleService>().RetrieveEnsembleUtilisateurPrecis());

            // On place dans la liste globale, la liste d'ensembles reçue
            Listes.ListeEnsemblesUtilisateur = UtilisateursEnsembles.ToList<UtilisateurEnsemble>();
        }

        public List<EnsembleVetement> ObtenirRecents(int nbMax)
        {
            
            List<EnsembleVetement> listeTemp = _utilisateurEnsembleService.RetrieveRecents().ToList<EnsembleVetement>();
            List<EnsembleVetement> listeRetour = new List<EnsembleVetement>();

            for (int i = 0; i < listeTemp.Count && i < nbMax; i++)
			{
			    listeRetour.Add(listeTemp[i]);
			}

            return listeRetour;

        }

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<UtilisateurEnsemble> UtilisateursEnsembles
        {
            get
            {
                return _utilisateursEnsembles;
            }

            set
            {
                if (_utilisateursEnsembles == value)
                {
                    return;
                }

                _utilisateursEnsembles = value;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        /// <param name="rangee"></param>
        internal Image DessinerVetement(Vetement v, int rangee)
        {
            Image i = new Image();
            i.Width = 200;
            i.Height = 200;
            string uri;
            if (v.ImageURL.ToString().Contains("http://"))
                uri = v.ImageURL.ToString();
            else
                uri = "http://" + v.ImageURL.ToString();
            i.Source = new BitmapImage(new Uri(uri));
            Grid.SetColumn(i, 2);
            Grid.SetRow(i, rangee);

            return i;
        }
    }
}
