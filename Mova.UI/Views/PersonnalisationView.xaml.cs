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
        public static History<UserControl> _historique = new History<UserControl>();
        private const int _maxStack = 4;
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

            _historique.Ajouter(this);

        }

        private void btnPrecedent_Click(object sender, RoutedEventArgs e)
        {
            //On commence par obtenir l'objet
            Button bTemp = (Button)sender;

            // On veut obtenir la rangée dans laquelle le bouton se trouve
            int rangee = (int)bTemp.GetValue(Grid.RowProperty);

            // On lui passe la rangée pour savoir quel type de vêtement il faut changer (torso, pantalons ou chaussures)
            DynamicGrid.Children.Add(ViewModel.changerVetement(rangee, "Precedent"));
        }

        private void btnSuivant_Click(object sender, RoutedEventArgs e)
        {
            //On commence par obtenir l'objet
            Button bTemp = (Button)sender;

            // On veut obtenir la rangée dans laquelle le bouton se trouve
            int rangee = (int)bTemp.GetValue(Grid.RowProperty);

            // On lui passe la rangée pour savoir quel type de vêtement il faut changer (torso, pantalons ou chaussures)
            // On efface l'image
            Image i = new Image();

            Grid.SetColumn(i, 2);
            Grid.SetRow(i, rangee);

            DynamicGrid.Children.Remove(i);

            DynamicGrid.Children.Add(ViewModel.changerVetement(rangee, "Suivant"));
        }

        private void btnChoisir_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnFavori_Click(object sender, RoutedEventArgs e)
        {
           
        }

    }
}
