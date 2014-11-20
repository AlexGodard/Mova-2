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
using Cstj.MvvmToolkit.Services.Definitions;
using Cstj.MvvmToolkit.Services;
using Mova.Logic.Models;
using Mova.Logic.Services.Definitions;
using Mova.Logic.Models.Entities;
using Mova.Logic;
using System.Timers;

namespace Mova.UI.Views
{
    /// <summary>
    /// Logique d'interaction pour UserControl.xaml
    /// </summary>
    public partial class ConnexionView : UserControl
    {
        private ConnexionViewModel ViewModel {get{return (ConnexionViewModel)DataContext;}}
        
        public ConnexionView()
        {
            InitializeComponent();

            DataContext = new ConnexionViewModel();

            ((MainWindow)System.Windows.Application.Current.MainWindow).btnRecent.Focusable = true;
            ((MainWindow)System.Windows.Application.Current.MainWindow).btnRecent.IsEnabled = true;
            ((MainWindow)System.Windows.Application.Current.MainWindow).btnRecent.Foreground = Brushes.White;

            ((MainWindow)System.Windows.Application.Current.MainWindow).btnFavoris.Focusable = true;
            ((MainWindow)System.Windows.Application.Current.MainWindow).btnFavoris.IsEnabled = true;
            ((MainWindow)System.Windows.Application.Current.MainWindow).btnFavoris.Foreground = Brushes.White;

            ((MainWindow)System.Windows.Application.Current.MainWindow).btnGardeRobe.Focusable = true;
            ((MainWindow)System.Windows.Application.Current.MainWindow).btnGardeRobe.IsEnabled = true;
            ((MainWindow)System.Windows.Application.Current.MainWindow).btnGardeRobe.Foreground = Brushes.White;
        }

		private void btnConnexion_Click(object sender, RoutedEventArgs e)
		{

			if (txtNomUtilisateur.Text == string.Empty || txtMotDePasse.Password == string.Empty)
			{
				lblErreur.Content = "Vous devez remplir tous les champs.";
                lblErreur.Padding = new Thickness(40,2,0,0);
                lblErreur.Visibility = Visibility.Visible;
				return;
			}

			Listes.pwd = txtMotDePasse.Password;

			try
			{
				ViewModel.RechercheUtilisateur();
			}
			catch (Exception)
			{
				//Envoyer un message d'erreur au View
                lblErreur.Content = "Connexion impossible. Réessayez plus tard";
                lblErreur.Padding = new Thickness(15, 2, 0, 0);
                lblErreur.Visibility = Visibility.Visible;
				return;
			}

			//Maxime Laramee -2014-010-14
			//On passe le nouvel utilisateur à celui du viewModel (on s'en reparle en classe pour les propertyChanging etc)
			ViewModel.UtilisateurConnecte = Listes.UtilisateurConnecte;
               

			if (ViewModel.UtilisateurConnecte.IdUtilisateur == null)
			{
                lblErreur.Content = "Nom d'utilisateur ou mot de passe invalide.";
                lblErreur.Padding = new Thickness(10, 2, 0, 0);
                lblErreur.Visibility = Visibility.Visible;
				return;
			}

            // On affiche l'option admin dans le menu s'il est un admin
            if (ViewModel.UtilisateurConnecte.EstAdmin)
            {
                ((MainWindow)System.Windows.Application.Current.MainWindow).btnAdmin.Header = "Administrateur";
                ((MainWindow)System.Windows.Application.Current.MainWindow).btnAdmin.IsEnabled = true;
            }

			IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();

            mainVM.ChangeView<StylisteActiviteView>(new StylisteActiviteView());
		}

        private void Quitter(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnCreationCompte_Click(object sender, RoutedEventArgs e)
        {
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<InscriptionView>(new InscriptionView());
        }

        private void btnInvite_Click(object sender, RoutedEventArgs e)
        {
            //Gabriel Piché Cloutier - 2014/10/09
            //Instancie un utilisateur vide en tant qu'invité.
            //ViewModel.UtilisateurConnecte = new Utilisateur();

            //Modification pour initialiser l'utilisateur global -Maxime Laramee - 2014-10-14
            Listes.UtilisateurConnecte = new Utilisateur();

            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();

            ((MainWindow)System.Windows.Application.Current.MainWindow).btnRecent.Focusable = false;
            ((MainWindow)System.Windows.Application.Current.MainWindow).btnRecent.IsEnabled = false;
            ((MainWindow)System.Windows.Application.Current.MainWindow).btnRecent.Foreground = Brushes.Gray;

            ((MainWindow)System.Windows.Application.Current.MainWindow).btnFavoris.Focusable = false;
            ((MainWindow)System.Windows.Application.Current.MainWindow).btnFavoris.IsEnabled = false;
            ((MainWindow)System.Windows.Application.Current.MainWindow).btnFavoris.Foreground = Brushes.Gray;

            ((MainWindow)System.Windows.Application.Current.MainWindow).btnGardeRobe.Focusable = false;
            ((MainWindow)System.Windows.Application.Current.MainWindow).btnGardeRobe.IsEnabled = false;
            ((MainWindow)System.Windows.Application.Current.MainWindow).btnGardeRobe.Foreground = Brushes.Gray;

			//Gabriel Piché Cloutier - 2014/10/25
			//Avant de passer à l'écran Styliste, on vérifie si la BD est disponiblie
			try
			{
                mainVM.ChangeView<StylisteActiviteView>(new StylisteActiviteView());
			}
			catch(Exception)
			{
                lblErreur.Content = "Connexion impossible. Réessayez plus tard";
                lblErreur.Padding = new Thickness(10, 2, 0, 0);
                lblErreur.Visibility = Visibility.Visible;
			}
        }

		



    }
}
