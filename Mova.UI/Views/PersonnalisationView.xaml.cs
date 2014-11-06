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
using Mova.Logic.Models;
using Mova.Logic;
using Mova.Logic.Models.Entities;
using System.Threading;
using Cstj.MvvmToolkit.Services.Definitions;
using Cstj.MvvmToolkit.Services;

namespace Mova.UI.Views
{
    /// <summary>
    /// Logique d'interaction pour PersonnalisationView.xaml
    /// </summary>
    public partial class PersonnalisationView : UserControl
    {
        private PersonnalisationViewModel ViewModel { get { return (PersonnalisationViewModel)DataContext; } }
        private List<Image> listeImages;

        public PersonnalisationView()
        {
            InitializeComponent();
            DataContext = new PersonnalisationViewModel();

            // On retourne la liste d'images à afficher
            listeImages = ViewModel.afficherEnsemble();

            DynamicGrid.Children.Add(listeImages[0]);
            DynamicGrid.Children.Add(listeImages[1]);
            DynamicGrid.Children.Add(listeImages[2]);

        }

        private void btnPrecedent_Click(object sender, RoutedEventArgs e)
        {
            //On commence par obtenir l'objet
            Button bTemp = (Button)sender;

            // On veut obtenir la rangée dans laquelle le bouton se trouve
            int rangee = (int)bTemp.GetValue(Grid.RowProperty);

            Image i = new Image();
            i = ViewModel.changerVetement(rangee, "Precedent");

            // LE PIRE CODE EVER BY GODARD

            if (i.Visibility != Visibility.Collapsed)   // Si on affiche une nouvelle image
            {
                // On efface l'image
                foreach (UIElement control in DynamicGrid.Children)
                {
                    if (Grid.GetRow(control) == rangee && Grid.GetColumn(control) == 2)
                    {
                        DynamicGrid.Children.Remove(control);
                        break;
                    }
                }
                DynamicGrid.Children.Add(i);
            }
        }

        private void btnSuivant_Click(object sender, RoutedEventArgs e)
        {
            //On commence par obtenir l'objet
            Button bTemp = (Button)sender;

            // On veut obtenir la rangée dans laquelle le bouton se trouve
            int rangee = (int)bTemp.GetValue(Grid.RowProperty);

            // On lui passe la rangée pour savoir quel type de vêtement il faut changer (torso, pantalons ou chaussures)
            
            

            Image i = new Image();
            i = ViewModel.changerVetement(rangee, "Suivant");

            // LE PIRE CODE EVER BY GODARD

            if (i.Visibility != Visibility.Collapsed)   // Si on affiche une nouvelle image
            {   
                // On efface l'image
                foreach (UIElement control in DynamicGrid.Children)
                {
                    if (Grid.GetRow(control) == rangee && Grid.GetColumn(control) == 2)
                    {
                        DynamicGrid.Children.Remove(control);
                        break;
                    }
                }

                DynamicGrid.Children.Add(i);
            }
        }

        private void btnChoisir_Click(object sender, RoutedEventArgs e)
        {
            // On vérifie si l'utilisateur a entrer un nom à son ensemble

            if (txtNomEnsemble.Text != "")
            {
                ViewModel.ajouterEnsemble(txtNomEnsemble.Text);

            }
        }


        private void btnFavori_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
