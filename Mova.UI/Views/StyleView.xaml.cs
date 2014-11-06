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
        bool bPremiereVueStyles = true;       //Si l'utilisateur ouvre ActiviteView pour la premiere fois

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


            bPremiereVueStyles = false;
            iNbStylesTotal = Listes.ListeStyles.Count();    
            //On crée des boutons pour les premiers 12 activités
            foreach (StyleVetement a in Listes.ListeStyles)
            {
                Button btn = new Button();
                btn.Content = a.NomStyle.ToString();
                btn.Width = 100;
                btn.Height = 100;
                btn.Margin = new Thickness(10, 0, 0, 0);
                btn.Padding = new Thickness(10, 0, 0, 0);
                btn.HorizontalAlignment = HorizontalAlignment.Left;
                WrapPanelStyles.Children.Add(btn);

                iNbStylesCourant++;     //Nombre de activités affichées au total
                iNbStylesPrecedent++;   //Enregistre le nombre d'activités sur l'écran precedant

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

            int iNombreDeBoutonAfficher = 0;   // Garde une trace sur le nombre de bouton courant sur l'écran
            WrapPanelStyles.Children.Clear();

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
                Button btn = new Button();
                btn.Content = s.NomStyle.ToString();
                btn.Width = 100;
                btn.Height = 100;
                btn.HorizontalAlignment = HorizontalAlignment.Left;
                WrapPanelStyles.Children.Add(btn);

                iNbStylesCourant++;
                iNbStylesPrecedent++;
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
            int iNombreDeBoutonAfficher = 0;

            WrapPanelStyles.Children.Clear();     //On efface le contenu de l'écran

            if (iNbStylesCourant - iNbStylesPrecedent < iNbStylesTotal)    //Nous offre la possibilité de revenir voir les activités précedent si nous sommes à la fin de la liste
            {
                btnPrecedent.Visibility = Visibility.Hidden;
            }
            else
            {
                btnPrecedent.Visibility = Visibility.Visible;
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
                Button btn = new Button();
                btn.Content = s.NomStyle.ToString();
                btn.Width = 100;
                btn.Height = 100;
                btn.HorizontalAlignment = HorizontalAlignment.Left;
                WrapPanelStyles.Children.Add(btn);

                iNbStylesCourant++;
                iNbStylesPrecedent++;
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
    }
}
