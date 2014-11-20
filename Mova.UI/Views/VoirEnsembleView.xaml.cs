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
        int iColonne = 1;
        int iRow = 1;

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
                Image i = new Image();
                i.Source = new BitmapImage(new Uri("http://" + v.ListeVetements[0].ImageURL.ToString()));
                Grid.SetColumn(i, iColonne);
                Grid.SetRow(i, iRow);
                GridEnsembles.Children.Add(i);
                Image i2 = new Image();
                i2.Source = new BitmapImage(new Uri("http://" + v.ListeVetements[1].ImageURL.ToString()));
                Grid.SetColumn(i2, iColonne + 1);
                Grid.SetRow(i2, iRow);
                GridEnsembles.Children.Add(i2);
                Image i3 = new Image();
                i3.Source = new BitmapImage(new Uri("http://" + v.ListeVetements[2].ImageURL.ToString()));
                Grid.SetColumn(i3, iColonne + 2);
                Grid.SetRow(i3, iRow);
                GridEnsembles.Children.Add(i3);



                iColonne++;

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

        }

        private void btnSuivant_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
