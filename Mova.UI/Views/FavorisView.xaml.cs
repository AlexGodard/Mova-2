using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Cstj.MvvmToolkit.Services;
using Cstj.MvvmToolkit.Services.Definitions;
using Mova.Logic.Models;
using Mova.Logic.Models.Entities;
using Mova.UI.ViewModel;

namespace Mova.UI.Views
{
    /// <summary>
    /// Logique d'interaction pour FavorisView.xaml
    /// </summary>
    public partial class FavorisView : UserControl
    {

        private FavorisViewModel ViewModel { get { return (FavorisViewModel)DataContext; } }

        private const int nbColumnsMax = 3;
        private const int nbColumnsDepart = 1;
        private const int nbRowsDepart = 1;
        private const int maxEnsembleDesire = 3;

        List<UtilisateurEnsemble> listeUtilisateurEnsembles = new List<UtilisateurEnsemble>();
        List<string> listeNomsEnsemble = new List<string>();
        List<EnsembleVetement> listeEnsembleRecents = new List<EnsembleVetement>();

        public FavorisView()
        {
            InitializeComponent();

            DataContext = new FavorisViewModel();

            listeEnsembleRecents = ViewModel.ObtenirFavoris(maxEnsembleDesire);

            if (listeEnsembleRecents.Count != 0)
            {
                AfficherRecents(); 
            }
            else
            {

                Button button = new Button();

                button.Content = "Aucun ensemble favori, allez à l'écran styliste";

                Grid.SetColumn(button, nbColumnsDepart);
                Grid.SetRow(button, nbRowsDepart);
                // On ajoute l'event qui se passe lorsqu'on clique sur le bouton (choisir le vêtement)
                button.Click += btnStyliste_Click;

                DynamicGrid.Children.Add(button);

            }

            


        }

        /// <summary>
        /// 
        /// </summary>
        //Pour le moment on affichera seulement 3 ensembles récents maximum
        private void AfficherRecents()
        {

            int i = nbColumnsDepart;

            foreach (EnsembleVetement ensemble in listeEnsembleRecents)
            {
                EcrireVetementViaListe(ensemble.ListeVetements, i);

                i++;
            }
        
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="l"></param>
        private void EcrireVetementViaListe(List<Vetement> l, int colonne)
        {

            //Écrire le torso
            Vetement torso = l[0];
            Vetement pants = l[1];
            Vetement shoes = l[2];

            DessinerVetement(torso, colonne, nbRowsDepart);
            DessinerVetement(pants, colonne, nbRowsDepart + 1);
            DessinerVetement(shoes, colonne, nbRowsDepart + 2);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        /// <param name="colonne"></param>
        /// <param name="rangee"></param>
        private void DessinerVetement(Vetement v, int colonne, int rangee)
        {
            Image i = new Image();
            i.Source = new BitmapImage(new Uri("http://" + v.ImageURL.ToString()));
            Grid.SetColumn(i, colonne);
            Grid.SetRow(i, rangee);

            DynamicGrid.Children.Add(i);

        }

        private void btnStyliste_Click(object sender, RoutedEventArgs e)
        {
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<UserControl>(new StylisteActiviteView());
        }

    }
}
