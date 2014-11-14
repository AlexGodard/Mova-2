using Mova.Logic;
using Mova.Logic.Models;
using Mova.Logic.Models.Entities;
using Mova.UI.ViewModel;
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
using System.Windows.Shapes;
using Cstj.MvvmToolkit.Services.Definitions;
using Cstj.MvvmToolkit.Services;

namespace Mova.UI.Views
{
    /// <summary>
    /// Interaction logic for StylesView.xaml
    /// </summary>
    public partial class StyleView : UserControl
    {
        int iNbStylesCourant = 0;              //Nombre d'activite ayant été afficher au total
        int iStylesDepart = 0;                 //On affiche des activités à partir de cette valeur
        int iNbStylesTotal;                     //Le nombre total d'activités
        int iNombreDeBoutonsDesires = 12;       //Combien d'activité on désire afficher à l'écran
        int iNbStylesPrecedent = 0;           //Le nombre d'activité affiché sur seulement le dernier écran
        int iColonne = 0;
        int iLigne = 0;

        private StyleViewModel ViewModelStyle { get { return (StyleViewModel)DataContext; } }

        public StyleView()
        {
            InitializeComponent();

            try
            {
                DataContext = new StyleViewModel();
            }
            catch (Exception)
            {
                throw;
            }

            iNbStylesTotal = Listes.ListeStyles.Count();    
            //On crée des boutons pour les premiers 12 activités
            foreach (StyleVetement a in Listes.ListeStyles)
            {
                afficherBtnStyle(a);

                if (iNbStylesCourant == iNombreDeBoutonsDesires)   //Lorsque nous avons 12 boutons on arrête   
                {
                    break;
                }
            }

            /*Si tous les activités n'ont pas été affichées, on offre un bouton suivant à l'utilisateur*/
            if (iNbStylesTotal - iNbStylesCourant > 0)
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
            gridStyle.Children.Clear();     //On efface le contenu de l'écran

            /*S'il avait des activités sur l'écran précedent, on n'offre la possibilité à l'utilisateur d'y revenir*/
            if (iNbStylesPrecedent != 0)
            {
                btnPrecedent.Visibility = Visibility.Visible;
            }
            else
            {
                btnPrecedent.Visibility = Visibility.Hidden;
            }


            iStylesDepart = iNbStylesCourant;     //On enregistre le debut de l'affichage des activités
            iNbStylesPrecedent = 0;   //On efface le nombre d'activités passées pour garder trace des nouveaux

            /*Affiche les activités à partir du point de départ donnée*/
            foreach (StyleVetement s in Listes.ListeStyles.Skip(iStylesDepart))
            {
                afficherBtnStyle(s);
                iNombreDeBoutonAfficher++;

                if (iNombreDeBoutonAfficher == iNombreDeBoutonsDesires)
                {
                    break;
                }
            }

            if (iNbStylesTotal - iNbStylesCourant > 0)   //Si le nombre d'activité total moins le nombre d'activité courant est plus grand que 0, il reste des activités à affichées
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
            iColonne = 0;
            iLigne = 0;
            int iNombreDeBoutonAfficher = 0;
            gridStyle.Children.Clear();     //On efface le contenu de l'écran

            if (iNbStylesCourant - iNbStylesPrecedent <= iNombreDeBoutonsDesires)    //Nous offre la possibilité de revenir voir les activités précedent si nous sommes à la fin de la liste
            {
                btnPrecedent.Visibility = Visibility.Hidden;
            }

            if (iNbStylesCourant == iNbStylesTotal)  //Nous sommes à la fin de notre liste
            {
                iNbStylesCourant = iNbStylesCourant - iNbStylesPrecedent - iNombreDeBoutonsDesires;
            }
            else
            {
                iNbStylesCourant = iNbStylesCourant - iNbStylesPrecedent - iNbStylesPrecedent;
            }

            iNbStylesPrecedent = 0;

            iStylesDepart = iNbStylesCourant;

            //Affiche le nombre les activités à partir du début proposé
            foreach (StyleVetement s in Listes.ListeStyles.Skip(iStylesDepart))
            {
                afficherBtnStyle(s);
                iNombreDeBoutonAfficher++;

                if (iNombreDeBoutonAfficher == iNombreDeBoutonsDesires)
                {
                    break;
                }
            }

            if (iNbStylesTotal - iNbStylesCourant > 0)       //S'il reste des activités à afficher
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

            StylisteStyleViewModel.SetChoix(contenu);

            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<UserControl>(new EnsembleView());
        
        }

        /// <summary>
        /// Gabriel Piché Cloutier - 2014-11-13
        /// Permet d'afficher un bouton au bon endroit dans l'écran.
        /// </summary>
        /// <param name="a">Activité à afficher dans le bouton.</param>
        private void afficherBtnStyle(StyleVetement a)
        {
            Button btn = new Button();
            btn.Content = a.NomStyle.ToString();
            btn.Style = (Style)FindResource("btnActivite");
            btn.Click += AllerAEnsembles;

            Grid.SetColumn(btn, iColonne);
            Grid.SetRow(btn, iLigne);
            gridStyle.Children.Add(btn);
            //GridPrincipale.Children.Add(btn);

            iNbStylesCourant++;     //Nombre de activités affichées au total
            iNbStylesPrecedent++;   //Enregistre le nombre d'activités sur l'écran precedant

            //Gabriel Piché Cloutier - 2014-11-12
            //On change les coordonées de la position du bouton pour le porchain.
            if (iColonne >= iNombreDeBoutonsDesires / 4)
            {
                iColonne = 0;
                iLigne++;
            }
            else
                iColonne++;


            if (iNbStylesCourant == iNombreDeBoutonsDesires)   //Lorsque nous avons 12 boutons on arrête   
            {
                return;
            }
        }
    }
}
