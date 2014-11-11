using Cstj.MvvmToolkit;
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

        internal List<Image> afficherEnsemblesRecents()
        {
            /*// Pour chaque EnsembleVetement, on extract la liste de Vetements
            foreach (UtilisateurEnsemble utilisateurEnsemble in Listes.ListeEnsemblesUtilisateur)
            {
            //Écrire le torso
                Vetement torso = utilisateurEnsemble.;
                Vetement pants = Listes.ensembleChoisi.ListeVetements[1];
                Vetement shoes = Listes.ensembleChoisi.ListeVetements[2];
                //On dessine le vetement
                List<Image> listeImages = new List<Image>();
                listeImages.Add(DessinerVetement(torso, 0));
                listeImages.Add(DessinerVetement(pants, 1));
                listeImages.Add(DessinerVetement(shoes, 2));

                // Avant de commencer, on ajoute les vêtements qu'on a choisi pour qu'ils soient en premier de liste.

                listeHauts.Add(Listes.ensembleChoisi.ListeVetements[0]);
                listeBas.Add(Listes.ensembleChoisi.ListeVetements[1]);
                listeChaussures.Add(Listes.ensembleChoisi.ListeVetements[2]);
                // On prépare les listes pour les changements de vêtement (suivant, précédent)

            }
            return listeImages;*/
            List<Image> listeImages = new List<Image>();
            return listeImages;
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
