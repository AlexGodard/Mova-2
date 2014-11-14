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
using Mova.Logic.Models;
using Mova.Logic.Services.Definitions;
using Mova.Logic.Models.Entities;
using Mova.UI.ViewModel;
using Mova.Logic;
using System.Text.RegularExpressions;
using System.Net;
using Mova.Logic.Models.Args;

namespace Mova.UI.Views
{
    /// <summary>
    /// Logique d'interaction pour MenuAdminView.xaml
    /// </summary>
    public partial class MenuAdminView : UserControl
    {
        private MenuAdminViewModel ViewModel { get { return (MenuAdminViewModel)DataContext; } }
        private History<UserControl> _historique = new History<UserControl>();

        public MenuAdminView()
        {
            InitializeComponent();
            DataContext = new MenuAdminViewModel();
            _historique.Ajouter(this); 

            

            // Construction des listbox (activités, styles, températures)
            construireListe("Couleur");
            construireListe("Temperature");
            construireListe("Activite");
            construireListe("Style");
        }

        public void construireListe(string nomListe)
        {
            if (nomListe == "Temperature")
            {
                List<Temperature> listeTemperatures = Listes.ListeTemperatures.ToList<Temperature>();

                for (int i = 0; i < listeTemperatures.Count; i++)
                {
                    lstTemperatures.Items.Add(listeTemperatures[i].NomClimat);
                }
            }

            if (nomListe == "Activite")
            {
                List<Activite> listeActivites = Listes.ListeActivites.ToList<Activite>();

                for (int i = 0; i < listeActivites.Count; i++)
                {
                    lstActivites.Items.Add(listeActivites[i].NomActivite);
                }
            }

            if (nomListe == "Style")
            {
                List<StyleVetement> listeStyles = Listes.ListeStyles.ToList<StyleVetement>();

                for (int i = 0; i < listeStyles.Count; i++)
                {
                    lstStyles.Items.Add(listeStyles[i].NomStyle);
                }
            }

            if (nomListe == "Couleur")
            {
                List<Couleur> listeCouleurs = Listes.ListeCouleurs.ToList<Couleur>();

                for (int i = 0; i < listeCouleurs.Count; i++)
                {
                    lstCouleurs.Items.Add(listeCouleurs[i].NomCouleur);
                }
            }
        }
    

        private void btnAjouterStyle_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnModifierStyle_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSupprimerStyle_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAjouterCouleur_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnModifierCouleur_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSupprimerCouleur_Click(object sender, RoutedEventArgs e)
        {

        }
    
        private void btnAjouterTemperature_Click(object sender, RoutedEventArgs e)
        {

        }
    
        private void btnModifierTemperature_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSupprimerTemperature_Click(object sender, RoutedEventArgs e)
        {

        }
    
        private void btnAjouterActivite_Click(object sender, RoutedEventArgs e)
        {
            // Si le champ est déjà visible, on vérifie s'il a entré quelque chose
            // On débloque le champ
            if (txtAjouterActivite.Visibility == Visibility.Visible)
            {
                if (txtAjouterActivite.Text != "")
                {
                    ViewModel.ajouterActivite(txtAjouterActivite.Text);
                    lstActivites.Items.Clear();
                    construireListe("Activite");
                    txtAjouterActivite.Text = "";
                    txtAjouterActivite.Visibility = Visibility.Hidden;
                    btnAjouterActivite.Content = "Ajouter";
                }
            }
            else
            {
                 // On débloque le champ
                 txtAjouterActivite.Visibility = Visibility.Visible;
                 btnAjouterActivite.Content = "Ajouter la nouvelle activité";
            }
        }

        private void btnModifierActivite_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSupprimerActivite_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
