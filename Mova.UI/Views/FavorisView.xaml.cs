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

        public static History<UserControl> _historique = new History<UserControl>();

        private const int nbColumnsMax = 3;
        private const int nbColumnsDepart = 1;
        private const int nbRowsDepart = 1;
        private const int maxEnsembleDesire = 3;

        List<UtilisateurEnsemble> listeUtilisateurEnsembles = new List<UtilisateurEnsemble>();
        List<string> listeNomsEnsemble = new List<string>();
        private static List<EnsembleVetement> listeEnsembleFavoris = new List<EnsembleVetement>();

        public FavorisView()
        {
            InitializeComponent();

            DataContext = new FavorisViewModel();

            _historique.Ajouter(this);

            if (_historique.Compte() <= 1) {
                listeEnsembleFavoris = ViewModel.ObtenirFavoris();
            }

            if (listeEnsembleFavoris.Count != 0)
            {

                List<EnsembleVetement> listeAAfficher = new List<EnsembleVetement>();

                //On va chercher ceux que l'on doit afficher pour cette page
                int noPage = _historique.GetNumberOfPage(this);
                int indexDepart = noPage*3;
                int indexDernierAffiché = 0;
                int compteurBackground = 0;
                //On en affiche un maximum de 3 par page
                for (int i = indexDepart; i < (indexDepart + nbColumnsMax) && i < listeEnsembleFavoris.Count; i++)
                {
                    listeAAfficher.Add(listeEnsembleFavoris[i]);

                    // On doit afficher le bon nombre de background blanc
                    if (compteurBackground == 0)
                        lblBackgroundRow1.Visibility = Visibility.Visible;
                    if (compteurBackground == 1)
                        lblBackgroundRow2.Visibility = Visibility.Visible;
                    if (compteurBackground == 2)
                        lblBackgroundRow3.Visibility = Visibility.Visible;
                    compteurBackground++;
                    indexDernierAffiché = i + 1;
                }

                AfficherEnsembles(listeAAfficher);

                //Maintenant on affiche les boutons nécessaires
                AfficherBoutonsAppropries(indexDernierAffiché, listeEnsembleFavoris);


            }
            else
            {

                Button button = new Button();

                lblMessage.Visibility = Visibility.Visible;
                btnPrecedent.Visibility = Visibility.Hidden;
                btnSuivant.Visibility = Visibility.Hidden;

                lblBackgroundRow1.Visibility = Visibility.Hidden;
                lblBackgroundRow2.Visibility = Visibility.Hidden;
                lblBackgroundRow3.Visibility = Visibility.Hidden;

                button.Content = "Aucun ensemble favori, allez à l'écran styliste";

                /*Grid.SetColumn(button, nbColumnsDepart);
                Grid.SetRow(button, nbRowsDepart);
                // On ajoute l'event qui se passe lorsqu'on clique sur le bouton (choisir le vêtement)
                button.Click += btnStyliste_Click;

                DynamicGrid.Children.Add(button);*/

            }

            


        }

        /// <summary>
        /// 
        /// </summary>
        //Pour le moment on affichera seulement 3 ensembles récents maximum
        private void AfficherEnsembles(List<EnsembleVetement> liste)
        {

            int i = 0;

            foreach (EnsembleVetement ensemble in liste)
            {
				EcrireVetementViaListe(ensemble.ListeVetements, i + (2 + (1 * i)));

                i++;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        private void AfficherBoutonsAppropries(int indexDernierElementAfficher, List<EnsembleVetement> listeARespecter)
        {
            //On commence avec tous les boutons cachés
            btnPrecedent.Visibility = Visibility.Hidden;
            btnSuivant.Visibility = Visibility.Hidden;

            //S'il n'est pas le premier
            if (!_historique.IsFirst(this))
            {
                btnPrecedent.Visibility = Visibility.Visible;
            }

            //Si le dernier ensemble de la liste globale n'est pas affiché
            if (indexDernierElementAfficher < listeARespecter.Count)
            {
                btnSuivant.Visibility = Visibility.Visible;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="l"></param>
        private void EcrireVetementViaListe(List<Vetement> l, int colonne)
        {

            List<Vetement> EnOrdre = l.OrderBy(vetement => vetement.TypeVetement.IdType).ToList<Vetement>();

            //Écrire le torso
            Vetement torso = EnOrdre[0];
            Vetement pants = EnOrdre[1];
            Vetement shoes = EnOrdre[2];

            DessinerVetement(torso, colonne, 2);
            DessinerVetement(pants, colonne, 3);
            DessinerVetement(shoes, colonne, 4);

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
            string uri;
            if (v.ImageURL.ToString().Contains("http://"))
                uri = v.ImageURL.ToString();
            else
                uri = "http://" + v.ImageURL.ToString();
            i.Source = new BitmapImage(new Uri(uri));
            Grid.SetColumn(i, colonne);
            Grid.SetRow(i, rangee);

            DynamicGrid.Children.Add(i);

        }

        private void btnStyliste_Click(object sender, RoutedEventArgs e)
        {
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<UserControl>(new StylisteActiviteView());
        }

        private void btnPrecedent_Click(object sender, RoutedEventArgs e)
        {
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<UserControl>(_historique.Last());
        }

        private void btnSuivant_Click(object sender, RoutedEventArgs e)
        {
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<UserControl>(new FavorisView());
        }

    }
}
