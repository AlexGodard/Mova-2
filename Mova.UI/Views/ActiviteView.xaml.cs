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
using Mova.Logic;
using Mova.Logic.Models;
using Mova.UI.ViewModel;

namespace Mova.UI.Views
{
    /// <summary>
    /// Logique d'interaction pour ActiviteView.xaml
    /// </summary>
    public partial class ActiviteView : UserControl
    {
        private ActiviteViewModel ViewModel { get { return (ActiviteViewModel)DataContext; } }
        int iNbActiviteCourant = 0;              //Nombre d'activite ayant été afficher au total
        int iActiviteDepart = 0;                 //On affiche des activités à partir de cette valeur
        int iNbActiviteTotal;       //Le nombre total d'activités
        int iNombreDeBoutonsDesires = 12;       //Combien d'activité on désire afficher à l'écran
        int iNbActivitePrecedent = 0;           //Le nombre d'activité affiché sur seulement le dernier écran        //Gabriel Piché CLoutier - 2014-11-12
        //Sert à déterminer la position du bouton dans la grid. Celle-ci commence à la colonne 2 et la ligne 2.
        int iColonne = 0;
        int iLigne = 0;

        public ActiviteView()
        {
            InitializeComponent();

            try
            {
                DataContext = new ActiviteViewModel();
            }
            catch (Exception)
            {
                throw;
            }

            iNbActiviteTotal = Listes.ListeActivites.Count(); 

            //On crée des boutons pour les premiers 12 activités
            foreach (Activite a in Listes.ListeActivites)
            {
                afficherBtnActivite(a);

                if (iNbActiviteCourant == iNombreDeBoutonsDesires)   //Lorsque nous avons 12 boutons on arrête   
                {
                    break;
                }
            }

            /*Si toutes les activités n'ont pas été affichées, on offre un bouton suivant à l'utilisateur*/
            //Gabriel Piché Cloutier - 2014-11-13
            //Correction des boucle

            if (iNbActiviteTotal - iNbActiviteCourant > 0)
            {
                btnSuivant.Visibility = Visibility.Visible;
            }
            else
            {
                btnSuivant.Visibility = Visibility.Hidden;
            }
        }

        private void btnSuivant_Click(object sender, RoutedEventArgs e)
        {
            iColonne = 0;
            iLigne = 0;
            int iNombreDeBoutonAfficher = 0;   // Garde une trace sur le nombre de bouton courant sur l'écran
            gridActivite.Children.Clear();     //On efface le contenu de l'écran

            /*S'il avait des activités sur l'écran précedent, on n'offre la possibilité à l'utilisateur d'y revenir*/
            if (iNbActivitePrecedent != 0)
            {
                btnPrecedent.Visibility = Visibility.Visible;
            }
            else
            {
               btnPrecedent.Visibility = Visibility.Hidden;
            }


            iActiviteDepart = iNbActiviteCourant;     //On enregistre le debut de l'affichage des activités
            iNbActivitePrecedent = 0;   //On efface le nombre d'activités passées pour garder trace des nouveaux

            /*Affiche les activités à partir du point de départ donnée*/
            foreach (Activite a in Listes.ListeActivites.Skip(iActiviteDepart))
            {
                afficherBtnActivite(a);
                iNombreDeBoutonAfficher++;

                if (iNombreDeBoutonAfficher == iNombreDeBoutonsDesires)
                {
                    break;
                }
            }

            if (iNbActiviteTotal - iNbActiviteCourant > 0)   //Si le nombre d'activité total moins le nombre d'activité courant est plus grand que 0, il reste des activités à affichées
            {                                                //On affiche donc le bouton suivant
                btnSuivant.Visibility = Visibility.Visible;
            }
            else
            {
                btnSuivant.Visibility = Visibility.Hidden;
            }
        }

        private void btnPrecedent_Click(object sender, RoutedEventArgs e)
        {
            int iNombreDeBoutonAfficher = 0;
            iColonne = 0;
            iLigne = 0;
            gridActivite.Children.Clear();     //On efface le contenu de l'écran

            if (iNbActiviteCourant - iNbActivitePrecedent <= iNombreDeBoutonsDesires)    //Nous offre la possibilité de revenir voir les activités précedent si nous sommes à la fin de la liste
            {
                btnPrecedent.Visibility = Visibility.Hidden;
            }

            if(iNbActiviteCourant == iNbActiviteTotal)  //Nous sommes à la fin de notre liste
            {
                iNbActiviteCourant = iNbActiviteCourant - iNbActivitePrecedent - iNombreDeBoutonsDesires;
            }
            else
            {
                iNbActiviteCourant = iNbActiviteCourant - iNbActivitePrecedent - iNbActivitePrecedent;
            }

            iNbActivitePrecedent = 0;

            iActiviteDepart = iNbActiviteCourant;

            //Affiche le nombre les activités à partir du début proposé
            foreach (Activite a in Listes.ListeActivites.Skip(iActiviteDepart))
            {
                afficherBtnActivite(a);
                iNombreDeBoutonAfficher++;

                if (iNombreDeBoutonAfficher == iNombreDeBoutonsDesires)
                {
                    break;
                }
            }

            //Est-ce que on est au début de la liste?
            if (iNbActiviteTotal - iNbActiviteCourant > 0)       //S'il reste des activités à afficher
            {
                btnSuivant.Visibility = Visibility.Visible;
            }
            else
            {
                btnSuivant.Visibility = Visibility.Hidden;
            }

        }

        private void AllerAEnsembles(object sender, RoutedEventArgs e)
        {

            //On reset la variable des Args
            Listes.InfoStyliste.Reset();

            //On reset la liste des ensembles
            EnsembleView._historique.Reset();

            //On signale qu'on est le delegate du EnsembleView
            EnsembleView.derniereFenetre = this;


            //On commence par obtenir l'objet
            Button bTemp = (Button)sender;

            //On obtient son contenu
            string contenu = bTemp.Content.ToString();

            StylisteActiviteViewModel.SetChoix(contenu);

            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<UserControl>(new EnsembleView());

        }

        /// <summary>
        /// Gabriel Piché Cloutier - 2014-11-12
        /// Permet d'afficher un bouton au bon endroit dans l'écran.
        /// </summary>
        /// <param name="a">Activité à afficher dans le bouton.</param>
        private void afficherBtnActivite(Activite a)
        {
            Button btn = new Button();
            btn.Content = a.NomActivite.ToString();
            btn.Style = (Style)FindResource("btnActivite");
            btn.Click += AllerAEnsembles;

            Grid.SetColumn(btn, iColonne);
            Grid.SetRow(btn, iLigne);
            gridActivite.Children.Add(btn);
            //GridPrincipale.Children.Add(btn);

            iNbActiviteCourant++;     //Nombre de activités affichées au total
            iNbActivitePrecedent++;   //Enregistre le nombre d'activités sur l'écran precedant

            //Gabriel Piché Cloutier - 2014-11-12
            //On change les coordonées de la position du bouton pour le porchain.
            if (iColonne >= iNombreDeBoutonsDesires / 4)
            {
                iColonne = 0;
                iLigne++;
            }
            else
                iColonne++;


            if (iNbActiviteCourant == iNombreDeBoutonsDesires)   //Lorsque nous avons 12 boutons on arrête   
            {
                return;
            }
        }

        public UIElement btnActivite { get; set; }

        private void btnAide_Click(object sender, RoutedEventArgs e)
        {
            var aide = new AideView();
            aide.Show();
            aide.aideBrowser.Navigate("http://420.cstj.qc.ca/gabrielpichecloutier/ParcourirActivites.pdf");
        }
    }
}
