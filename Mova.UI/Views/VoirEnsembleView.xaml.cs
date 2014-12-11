using Mova.Logic;
using Mova.Logic.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Cstj.MvvmToolkit.Services;
using Cstj.MvvmToolkit.Services.Definitions;

namespace Mova.UI.Views
{
    /// <summary>
    /// Interaction logic for VoirEnsembleView.xaml
    /// </summary>
    public partial class VoirEnsembleView : UserControl
    {
        private VoirEnsembleViewModel ViewModel { get { return (VoirEnsembleViewModel)DataContext; } }
        int iNbVetementCourant = 0;              //Nombre d'activite ayant été afficher au total
        int iVetementDepart = 0;                 //On affiche des activités à partir de cette valeur
        int iVetementTotal;       //Le nombre total d'activités
        int iNombreDeBoutonsDesires = 3;       //Combien d'activité on désire afficher à l'écran
        int iNbVetementPrecedent = 0;           //Le nombre d'activité affiché sur seulement le dernier écran
        int iColonne = 0;
        int iRow = 0;

        public VoirEnsembleView()
        {
            InitializeComponent();
            try
            {
                DataContext = new VoirEnsembleViewModel();
            }
            catch (Exception)
            {
                throw;
            }
            iVetementTotal = Listes.ListesEnsembleUtilisateur.Count();

            //On crée des boutons pour les premiers 12 activités
            foreach (EnsembleVetement v in Listes.ListesEnsembleUtilisateur)
            {
                iColonne++;

                Image i = new Image();
                string uri;
                if (v.ListeVetements[0].ImageURL.ToString().Contains("http://"))
                    uri = v.ListeVetements[0].ImageURL.ToString();
                else
                    uri = "http://" + v.ListeVetements[0].ImageURL.ToString();
                i.Source = new BitmapImage(new Uri("http://" + v.ListeVetements[0].ImageURL.ToString()));
                Grid.SetColumn(i, iColonne);
                Grid.SetRow(i, iRow);
                GridEnsembles.Children.Add(i);

                iRow++;

                Image i2 = new Image();
                if (v.ListeVetements[1].ImageURL.ToString().Contains("http://"))
                    uri = v.ListeVetements[1].ImageURL.ToString();
                else
                    uri = "http://" + v.ListeVetements[1].ImageURL.ToString();
                i2.Source = new BitmapImage(new Uri("http://" + v.ListeVetements[1].ImageURL.ToString()));
                Grid.SetColumn(i2, iColonne);
                Grid.SetRow(i2, iRow);
                GridEnsembles.Children.Add(i2);

                iRow++;

                Image i3 = new Image();
                if (v.ListeVetements[2].ImageURL.ToString().Contains("http://"))
                    uri = v.ListeVetements[2].ImageURL.ToString();
                else
                    uri = "http://" + v.ListeVetements[2].ImageURL.ToString();
                i3.Source = new BitmapImage(new Uri("http://" + v.ListeVetements[2].ImageURL.ToString()));
                Grid.SetColumn(i3, iColonne);
                Grid.SetRow(i3, iRow);
                GridEnsembles.Children.Add(i3);

                iRow = 0;
                          
                iNbVetementCourant++;     //Nombre de activités affichées au total
                iNbVetementPrecedent++;   //Enregistre le nombre d'activités sur l'écran precedant

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

        private void btnPrecedent_Click(object sender, RoutedEventArgs e)
        {
            iColonne = 0;
            int iCompteur = 0;

            var imageasupprimer = GridEnsembles.Children.OfType<Image>();     //On efface le contenu de l'écran

            foreach (var image in imageasupprimer.ToList())
            {
                GridEnsembles.Children.Remove(image);
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


            //On crée des boutons pour les premiers 12 activités
            foreach (EnsembleVetement v in Listes.ListesEnsembleUtilisateur.Skip(iVetementDepart))
            {
                iColonne++;

                Image i = new Image();
                string uri;
                if (v.ListeVetements[0].ImageURL.ToString().Contains("http://"))
                    uri = v.ListeVetements[0].ImageURL.ToString();
                else
                    uri = "http://" + v.ListeVetements[0].ImageURL.ToString();
                i.Source = new BitmapImage(new Uri("http://" + v.ListeVetements[0].ImageURL.ToString()));
                Grid.SetColumn(i, iColonne);
                Grid.SetRow(i, iRow);
                GridEnsembles.Children.Add(i);
                Image i2 = new Image();

                iRow++;

                if (v.ListeVetements[1].ImageURL.ToString().Contains("http://"))
                    uri = v.ListeVetements[1].ImageURL.ToString();
                else
                    uri = "http://" + v.ListeVetements[1].ImageURL.ToString();
                i2.Source = new BitmapImage(new Uri("http://" + v.ListeVetements[1].ImageURL.ToString()));
                Grid.SetColumn(i2, iColonne);
                Grid.SetRow(i2, iRow);
                GridEnsembles.Children.Add(i2);
                Image i3 = new Image();

                iRow++;


                if (v.ListeVetements[2].ImageURL.ToString().Contains("http://"))
                    uri = v.ListeVetements[2].ImageURL.ToString();
                else
                    uri = "http://" + v.ListeVetements[2].ImageURL.ToString();
                i3.Source = new BitmapImage(new Uri("http://" + v.ListeVetements[2].ImageURL.ToString()));
                Grid.SetColumn(i3, iColonne);
                Grid.SetRow(i3, iRow);
                GridEnsembles.Children.Add(i3);

                iRow = 0;

                iCompteur++;
                iNbVetementCourant++;     //Nombre de activités affichées au total
                iNbVetementPrecedent++;   //Enregistre le nombre d'activités sur l'écran precedant

                if (iCompteur == iNombreDeBoutonsDesires)   //Lorsque nous avons 12 boutons on arrête   
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

        private void btnSuivant_Click(object sender, RoutedEventArgs e)
        {
            iColonne = 0;
            int iCompteur = 0;

            var imageasupprimer = GridEnsembles.Children.OfType<Image>();     //On efface le contenu de l'écran

            foreach (var image in imageasupprimer.ToList())
            {
                GridEnsembles.Children.Remove(image);
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

            //On crée des boutons pour les premiers 12 activités
            foreach (EnsembleVetement v in Listes.ListesEnsembleUtilisateur.Skip(iVetementDepart))
            {
                iColonne++;
             

                Image i = new Image();
                string uri;
                if (v.ListeVetements[0].ImageURL.ToString().Contains("http://"))
                    uri = v.ListeVetements[0].ImageURL.ToString();
                else
                    uri = "http://" + v.ListeVetements[0].ImageURL.ToString();
                i.Source = new BitmapImage(new Uri("http://" + v.ListeVetements[0].ImageURL.ToString()));
                Grid.SetColumn(i, iColonne);
                Grid.SetRow(i, iRow);
                GridEnsembles.Children.Add(i);
                Image i2 = new Image();

                iRow++;

                if (v.ListeVetements[1].ImageURL.ToString().Contains("http://"))
                    uri = v.ListeVetements[1].ImageURL.ToString();
                else
                    uri = "http://" + v.ListeVetements[1].ImageURL.ToString();
                i2.Source = new BitmapImage(new Uri("http://" + v.ListeVetements[1].ImageURL.ToString()));
                Grid.SetColumn(i2, iColonne);
                Grid.SetRow(i2, iRow);
                GridEnsembles.Children.Add(i2);
                Image i3 = new Image();

                iRow++;


                if (v.ListeVetements[2].ImageURL.ToString().Contains("http://"))
                    uri = v.ListeVetements[2].ImageURL.ToString();
                else
                    uri = "http://" + v.ListeVetements[2].ImageURL.ToString();
                i3.Source = new BitmapImage(new Uri("http://" + v.ListeVetements[2].ImageURL.ToString()));
                Grid.SetColumn(i3, iColonne);
                Grid.SetRow(i3, iRow);
                GridEnsembles.Children.Add(i3);

                iRow = 0;

                iCompteur++;
                iNbVetementCourant++;     //Nombre de activités affichées au total
                iNbVetementPrecedent++;   //Enregistre le nombre d'activités sur l'écran precedant

                if (iCompteur == iNombreDeBoutonsDesires)   //Lorsque nous avons 12 boutons on arrête   
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

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<MaGardeRobeView>(new MaGardeRobeView());
        }
    }
}
