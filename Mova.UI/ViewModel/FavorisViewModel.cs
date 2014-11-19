using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Cstj.MvvmToolkit.Services;
using Mova.Logic;
using Mova.Logic.Models;
using Mova.Logic.Models.Entities;
using Mova.Logic.Services.Definitions;

namespace Mova.UI.ViewModel
{
    class FavorisViewModel
    {

        private IUtilisateurEnsembleService _utilisateurEnsembleService;
        private ObservableCollection<UtilisateurEnsemble> _utilisateursEnsembles = new ObservableCollection<UtilisateurEnsemble>();

        public int i = 0, j = 0, k = 0, nbMaxFavorisAAfficher = 100;

        // On ajoute tout les vêtements dans des listes de vêtements temporaires
        List<Vetement> listeHauts = new List<Vetement>();
        List<Vetement> listeBas = new List<Vetement>();
        List<Vetement> listeChaussures = new List<Vetement>();

        /// <summary>
        /// 
        /// </summary>
        public FavorisViewModel()
        {
            _utilisateurEnsembleService = ServiceFactory.Instance.GetService<IUtilisateurEnsembleService>();

            UtilisateursEnsembles = new ObservableCollection<UtilisateurEnsemble>(ServiceFactory.Instance.GetService<IUtilisateurEnsembleService>().RetrieveEnsembleUtilisateurPrecis());

            // On place dans la liste globale, la liste d'ensembles reçue
            Listes.ListeEnsemblesUtilisateur = UtilisateursEnsembles.ToList<UtilisateurEnsemble>();
        }

        public List<EnsembleVetement> ObtenirFavoris()
        {

            List<EnsembleVetement> listeTemp = _utilisateurEnsembleService.RetrieveFavoris().ToList<EnsembleVetement>();
            List<EnsembleVetement> listeRetour = new List<EnsembleVetement>();

            for (int i = 0; i < listeTemp.Count && i < nbMaxFavorisAAfficher; i++)
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
            i.Source = new BitmapImage(new Uri("http://" + v.ImageURL.ToString()));
            Grid.SetColumn(i, 2);
            Grid.SetRow(i, rangee);

            return i;
        }
    }
}