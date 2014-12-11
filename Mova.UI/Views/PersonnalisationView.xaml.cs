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
using Mova.Logic.Services.Definitions;

namespace Mova.UI.Views
{
    /// <summary>
    /// Logique d'interaction pour PersonnalisationView.xaml
    /// </summary>
    public partial class PersonnalisationView : UserControl
    {
        private PersonnalisationViewModel ViewModel { get { return (PersonnalisationViewModel)DataContext; } }
        private List<Image> listeImages;
        private bool estFavori = false;
        public PersonnalisationView()
        {
            InitializeComponent();
            DataContext = new PersonnalisationViewModel();

            // On retourne la liste d'images à afficher
            listeImages = ViewModel.afficherEnsemble();

            //Si l'utilisateur n'est pas connecté, on désactive le champ texte du nom de l'ensemble.
            if (Listes.UtilisateurConnecte.IdUtilisateur == null)
            {
                txtNomEnsemble.IsEnabled = false;
            }
            else
            {
                txtNomEnsemble.IsEnabled = true;
            }
            //Gabriel Piché Cloutier - 2014-11-11
            //On affiche les vêtements dans les bonnes images déjà dans la grid.
            Torse.Source = listeImages[0].Source;
            Bas.Source = listeImages[1].Source;
            Chaussures.Source = listeImages[2].Source;

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
                    if (Grid.GetRow(control) == rangee && Grid.GetColumn(control) == 2 && control is Image)
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

            
            // LE PIRE CODE EVER BY GODARD, mais c'est le seul moyen que j'ai trouvé et ça marche
            if (i.Visibility != Visibility.Collapsed)   // Si on affiche une nouvelle image
            {   
                // On efface l'image
                foreach (UIElement control in DynamicGrid.Children)
                {
                    if (Grid.GetRow(control) == rangee && Grid.GetColumn(control) == 2 && control is Image)
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
            //Si l'utilisateur est connecté, on procède, sinon, on lui dit qu'il faut se connecter.
            if (Listes.UtilisateurConnecte.IdUtilisateur != null)
            {
                // On vérifie si l'utilisateur a entrer un nom à son ensemble, sinon, on en met un par défaut.
                if (txtNomEnsemble.Text != "")
                {
                    int idEnsemble = ViewModel.ajouterEnsemble(txtNomEnsemble.Text);
                    int idUtilisateur = ViewModel.ajouterUtilisateurEnsemble(idEnsemble, estFavori);
                    Listes.ensembleChoisi.IdEnsemble = idEnsemble;
                    ViewModel.ajouterEnsembleVetement(Listes.ensembleChoisi);
                }

                IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
                RecentsView._historique = new History<UserControl>();
                mainVM.ChangeView<UserControl>(new RecentsView());

            }
            else
            {
                txbErreurNom.Visibility = Visibility.Visible;
            }
        }

        //Maxime Laramee - 11/11/14
        private void btnFavori_Click(object sender, RoutedEventArgs e)
        {
            //On change la couleur du bouton
            btnFavori.Background = Brushes.Orange;

            imgChecked.Visibility = Visibility.Visible;

            
            //Si l'utilisateur est connecté, on procède, sinon, on lui dit qu'il faut se connecter.
            if (Listes.UtilisateurConnecte.IdUtilisateur != null)
            {
                estFavori = true;
            }
            else
            {
                txbErreurNom.Visibility = Visibility.Visible;
            }
        }

        private EnsembleVetement GetEnsemble()
        {

            EnsembleVetement v;

            try
            {
                 v = Listes.ensembleChoisi;
            }
            catch (Exception e)
            {
                return new EnsembleVetement();
            }

            return v;
        }

        private void btnEcranPrecedent_Click(object sender, RoutedEventArgs e)
        {
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<UserControl>(EnsembleView._historique.ReturnLast());
        }

        private void txtNomEnsemble_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= txtNomEnsemble_GotFocus;
            tb.BorderBrush = Brushes.Transparent;
        }
    }
}
