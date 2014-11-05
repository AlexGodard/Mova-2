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
using Mova.UI.ViewModel;
using Mova.Logic.Models;
using Cstj.MvvmToolkit.Services.Definitions;
using Cstj.MvvmToolkit.Services;

namespace Mova.UI.Views
{
    /// <summary>
    /// Logique d'interaction pour InscriptionView.xaml
    /// </summary>
    public partial class InscriptionView : UserControl
    {
        private InscriptionViewModel ViewModel { get { return (InscriptionViewModel)DataContext; } }

        public InscriptionView()
        {
            InitializeComponent();

            DataContext = new InscriptionViewModel();
        }

        private void btnConfirmer_Click(object sender, RoutedEventArgs e)
        {
             // Au début, on doit remettre tous les label noir et cacher les messages d'erreur.
             lblPrenom.Foreground = System.Windows.Media.Brushes.Black;
             lblNom.Foreground = System.Windows.Media.Brushes.Black;
             lblIdentifiant.Foreground = System.Windows.Media.Brushes.Black;
             lblMotDePasse.Foreground = System.Windows.Media.Brushes.Black;
             lblSexe.Foreground = System.Windows.Media.Brushes.Black;
             lblCourriel.Foreground = System.Windows.Media.Brushes.Black;
             lblVerifMotDePasse.Foreground = System.Windows.Media.Brushes.Black;
             lblSexe.Foreground = System.Windows.Media.Brushes.Black;
             txbErreurNom.Visibility = Visibility.Hidden;
             txbErreurIdentifiant.Visibility = Visibility.Hidden;
             txbErreurMotDePasse.Visibility = Visibility.Hidden;
             txbErreurCorrespondanceMotDePasse.Visibility = Visibility.Hidden;
             txbErreurCourriel.Visibility = Visibility.Hidden;
             txbErreurCorrespondanceCourriel.Visibility = Visibility.Hidden;
             txbErreurCorrespondanceIdentifiant.Visibility = Visibility.Hidden;

             // Validation du nom/prénom

             RegleNom regleNom = new RegleNom();
             RegleNom reglePrenom = new RegleNom();

             if (!regleNom.valider(txtNom.Text))  // Si le nom n'est pas valide
             {
                 txbErreurNom.Visibility = Visibility.Visible;
                 lblNom.Foreground = System.Windows.Media.Brushes.Red;
             }
             if (!reglePrenom.valider(txtPrenom.Text))    // Si le prénom n'est pas valide
             {
                 txbErreurNom.Visibility = Visibility.Visible;
                 lblPrenom.Foreground = System.Windows.Media.Brushes.Red;
             }
            

             // Validation de l'identifiant

             RegleIdentifiant regleIdentifiant = new RegleIdentifiant();

             if(!regleIdentifiant.valider(txtIdentifiant.Text.ToLower()))
             {
                 txbErreurIdentifiant.Visibility = Visibility.Visible;
                 lblIdentifiant.Foreground = System.Windows.Media.Brushes.Red;
             }
             else
                 txbErreurIdentifiant.Visibility = Visibility.Hidden;

             // Validation du mot de passe

             RegleMotDePasse regleMotDePasse = new RegleMotDePasse();

             if (!regleMotDePasse.valider(txtMotDePasse.Text))
             {
                 txbErreurMotDePasse.Visibility = Visibility.Visible;
                 lblMotDePasse.Foreground = System.Windows.Media.Brushes.Red;
             }

             if (txtMotDePasse.Text != txtVerifMotDePasse.Text)
             {
                 txbErreurCorrespondanceMotDePasse.Visibility = Visibility.Visible;
                 lblVerifMotDePasse.Foreground = System.Windows.Media.Brushes.Red;
             }

             // Validation du sexe

             if (rdoFemme.IsChecked == false && rdoHomme.IsChecked == false)
             {
                 lblSexe.Foreground = System.Windows.Media.Brushes.Red;
             }
            

             // Validation du courriel

             RegleEmail regleEmail = new RegleEmail();

             if (!regleEmail.valider(txtCourriel.Text))
             {
                 txbErreurCourriel.Visibility = Visibility.Visible;
                 lblCourriel.Foreground = System.Windows.Media.Brushes.Red;
             }

             // On regarde s'il est déjà utilisé par un autre utilisateur
             if (ViewModel.verifierCourriel() == false)       // S'il est existant
             {
                 txbErreurCorrespondanceCourriel.Visibility = Visibility.Visible;
                 lblCourriel.Foreground = System.Windows.Media.Brushes.Red;
             }

             // On regarde s'il est déjà utilisé par un autre utilisateur
             if (ViewModel.verifierIdentifiant() == false)       // S'il est existant
             {
                 txbErreurCorrespondanceIdentifiant.Visibility = Visibility.Visible;
                 lblIdentifiant.Foreground = System.Windows.Media.Brushes.Red;
             }

             // Si tout est valide
             if (regleEmail.valider(txtCourriel.Text) &&
                 txtMotDePasse.Text == txtVerifMotDePasse.Text &&
                 regleMotDePasse.valider(txtMotDePasse.Text) &&
                 regleIdentifiant.valider(txtIdentifiant.Text.ToLower()) &&
                 reglePrenom.valider(txtPrenom.Text) &&
                 regleNom.valider(txtPrenom.Text) &&
                 ViewModel.verifierIdentifiant() &&
                 ViewModel.verifierCourriel() &&
                 (rdoFemme.IsChecked == true || rdoHomme.IsChecked == true))
             {
                 

                 string sexe;
                 // On doit aller chercher le sexe pour ensuite pour le passer à RetrieveArgs
                 // car il est impossible de binder un RadioButton.
                 if (rdoFemme.IsChecked == true)
                    sexe = "F";
                 else
                    sexe = "H";

                 // On ajoute l'utilisateur à la BD. 
                 ViewModel.ajouterUtilisateur(sexe);

                   // On affiche "Inscription réussie" et on connecte l'utilisateur
                 MessageBox.Show("Inscription réussie! Vous êtes maintenant connecté(e).");

                 // Ensuite, on se rend à l'écran styliste
                 IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();

                 StylisteView._historique = new History<UserControl>();

                 mainVM.ChangeView<StylisteView>(new StylisteView());
             }
         }

         private void rdoHomme_Checked(object sender, RoutedEventArgs e)
         {
             rdoFemme.IsChecked = false;
         }

         private void rdoFemme_Checked(object sender, RoutedEventArgs e)
         {
             rdoHomme.IsChecked = false;
         }

         private void btnAnnuler_Click(object sender, RoutedEventArgs e)
         {
             //Modification pour réinitialiser l'utilisateur global - Gabriel Piché Cloutier - 2014-10-28
             IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
             mainVM.ChangeView<ConnexionView>(new ConnexionView());
         }
    }
}
