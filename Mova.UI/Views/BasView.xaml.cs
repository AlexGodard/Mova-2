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
using Mova.Logic.Models.Entities;
using Mova.UI.ViewModel;

namespace Mova.UI.Views
{
    /// <summary>
    /// Logique d'interaction pour BasView.xaml
    /// </summary>
    public partial class BasView : UserControl
    {

        private BasViewModel ViewModel { get { return (BasViewModel)DataContext; } }
        int iNbVetementCourant = 0;              //Nombre d'activite ayant été afficher au total
        int iVetementDepart = 0;                 //On affiche des activités à partir de cette valeur
        int iVetementTotal;       //Le nombre total d'activités
        int iNombreDeBoutonsDesires = 12;       //Combien d'activité on désire afficher à l'écran
        int iNbVetementPrecedent = 0;           //Le nombre d'activité affiché sur seulement le dernier écran
        int iColonne = 1;
        int iRow = 0;

        public BasView()
        {
            InitializeComponent();

            DataContext = new BasViewModel();

            iVetementTotal = Listes.ListeBasUtilisateur.Count();

            //On crée des boutons pour les premiers 12 activités
            foreach (Vetement v in Listes.ListeBasUtilisateur)
            {
                Label l = new Label();
                l.Background = Brushes.White;
                l.Padding = new Thickness(10, 10, 10, 10);
                l.Margin = new Thickness(10, 10, 10, 10);
                l.Width = 152;

                Grid.SetColumn(l, iColonne);
                Grid.SetRow(l, iRow);

                gridbas.Children.Add(l);

                Image i = new Image();
                string uri;
                if (v.ImageURL.ToString().Contains("http://"))
                    uri = v.ImageURL.ToString();
                else
                    uri = "http://" + v.ImageURL.ToString();
                i.Source = new BitmapImage(new Uri(uri));
                i.Width = 148;
                i.Height = 178;
                Grid.SetColumn(i, iColonne);
                Grid.SetRow(i, iRow);

                gridbas.Children.Add(i);

                iNbVetementCourant++;     //Nombre de activités affichées au total
                iNbVetementPrecedent++;   //Enregistre le nombre d'activités sur l'écran precedant

                if (iColonne == 4)
                {
                    iColonne = 0;
                    iRow++;
                }

                iColonne++;   

                if (iNbVetementCourant == iNombreDeBoutonsDesires)   //Lorsque nous avons 12 boutons on arrête   
                {
                    break;
                }
            }

            /*Si tous les activités n'ont pas été affichées, on offre un bouton suivant à l'utilisateur*/
            if (iVetementTotal - iNbVetementCourant > 0)
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
            iColonne = 1;
            iRow = 0;

            var imageasupprimer = gridbas.Children.OfType<Image>();     //On efface le contenu de l'écran
            var labelasupprimer = gridbas.Children.OfType<Label>();     //On efface le contenu de l'écran

            foreach (var image in imageasupprimer.ToList())
            {
                gridbas.Children.Remove(image);
            }
            foreach (var label in labelasupprimer.ToList())
            {
                gridbas.Children.Remove(label);
            }


            /*S'il avait des activités sur l'écran précedent, on n'offre la possibilité à l'utilisateur d'y revenir*/
            if (iNbVetementPrecedent != 0)
            {
                btnPrecedent.Visibility = Visibility.Visible;
            }
            else
            {
                btnPrecedent.Visibility = Visibility.Hidden;
            }


            iVetementDepart = iNbVetementCourant;     //On enregistre le debut de l'affichage des activités
            iNbVetementPrecedent = 0;   //On efface le nombre d'activités passées pour garder trace des nouveaux

            /*Affiche les activités à partir du point de départ donnée*/
            foreach (Vetement v in Listes.ListeBasUtilisateur.Skip(iVetementDepart))
            {
                Label l = new Label();
                l.Background = Brushes.White;
                l.Padding = new Thickness(10, 10, 10, 10);
                l.Margin = new Thickness(10, 10, 10, 10);
                l.Width = 152;

                Grid.SetColumn(l, iColonne);
                Grid.SetRow(l, iRow);

                gridbas.Children.Add(l);

                Image i = new Image();
                string uri;
                if (v.ImageURL.ToString().Contains("http://"))
                    uri = v.ImageURL.ToString();
                else
                    uri = "http://" + v.ImageURL.ToString();
                i.Source = new BitmapImage(new Uri(uri));
                i.Width = 148;
                i.Height = 178;
                Grid.SetColumn(i, iColonne);
                Grid.SetRow(i, iRow);

                gridbas.Children.Add(i);

                iNbVetementCourant++;
                iNbVetementPrecedent++;
                iNombreDeBoutonAfficher++;

                if (iColonne == 4)
                {
                    iColonne = 0;
                    iRow++;
                }

                iColonne++;   

                if (iNombreDeBoutonAfficher == iNombreDeBoutonsDesires)
                {
                    break;
                }
            }

            if (iVetementTotal - iNbVetementCourant > 0)   //Si le nombre d'activité total moins le nombre d'activité courant est plus grand que 0, il reste des activités à affichées
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
            iColonne = 1;
            iRow = 0;

            var imageasupprimer = gridbas.Children.OfType<Image>();     //On efface le contenu de l'écran
            var labelasupprimer = gridbas.Children.OfType<Label>();     //On efface le contenu de l'écran

            foreach (var image in imageasupprimer.ToList())
            {
                gridbas.Children.Remove(image);
            }
            foreach (var label in labelasupprimer.ToList())
            {
                gridbas.Children.Remove(label);
            }


            if (iNbVetementCourant - iNbVetementPrecedent <= iNombreDeBoutonsDesires)    //Nous offre la possibilité de revenir voir les activités précedent si nous sommes à la fin de la liste
            {
                btnPrecedent.Visibility = Visibility.Hidden;
            }
            if (iNbVetementCourant == iVetementTotal)
            {
                iNbVetementCourant = iNbVetementCourant - iNbVetementPrecedent - iNombreDeBoutonsDesires;
            }
            else
            {
                iNbVetementCourant = iNbVetementCourant - iNbVetementPrecedent - iNbVetementPrecedent;
            }

            iNbVetementPrecedent = 0;

            iVetementDepart = iNbVetementCourant;

            //Affiche le nombre les activités à partir du début proposé
            foreach (Vetement v in Listes.ListeBasUtilisateur.Skip(iVetementDepart))
            {
                Label l = new Label();
                l.Background = Brushes.White;
                l.Padding = new Thickness(10, 10, 10, 10);
                l.Margin = new Thickness(10, 10, 10, 10);
                l.Width = 152;

                Grid.SetColumn(l, iColonne);
                Grid.SetRow(l, iRow);

                gridbas.Children.Add(l);

                Image i = new Image();
                string uri;
                if (v.ImageURL.ToString().Contains("http://"))
                    uri = v.ImageURL.ToString();
                else
                    uri = "http://" + v.ImageURL.ToString();
                i.Source = new BitmapImage(new Uri(uri));
                i.Width = 148;
                i.Height = 178;
                Grid.SetColumn(i, iColonne);
                Grid.SetRow(i, iRow);

                gridbas.Children.Add(i);

                iNbVetementCourant++;
                iNbVetementPrecedent++;
                iNombreDeBoutonAfficher++;

                if (iColonne == 4)
                {
                    iColonne = 0;
                    iRow++;
                }

                iColonne++;   

                if (iNombreDeBoutonAfficher == iNombreDeBoutonsDesires)
                {
                    break;
                }
            }

            if (iVetementTotal - iNbVetementCourant > 0)       //S'il reste des activités à afficher
            {
                btnSuivant.Visibility = Visibility.Visible;
            }

            else
            {
                btnSuivant.Visibility = Visibility.Hidden;
            }

        }

        private void btnBack_Click_1(object sender, RoutedEventArgs e)
        {
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<MaGardeRobeView>(new MaGardeRobeView());
        }
    }
}
