﻿using Mova.UI.ViewModel;
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
using Mova.Logic.Services.NHibernate;
using Mova.Logic.Services.Definitions;
using Mova.Logic.Models.Entities;
using Mova.Logic;

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
        }

		private void btnConnexion_Click(object sender, RoutedEventArgs e)
		{

			if (txtNomUtilisateur.Text == string.Empty || txtMotDePasse.Password == string.Empty)
			{
				lblErreur.Content = "Vous devez remplir tous les champs.";
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
				return;
			}

			//Maxime Laramee -2014-010-14
			//On passe le nouvel utilisateur à celui du viewModel (on s'en reparle en classe pour les propertyChanging etc)
			ViewModel.UtilisateurConnecte = Listes.UtilisateurConnecte;
               

			if (ViewModel.UtilisateurConnecte.IdUtilisateur == null)
			{
				lblErreur.Content = "Nom d'utilisateur ou mot de passe invalide.";
				return;
			}

            // On affiche l'option admin dans le menu s'il est un admin
            if (ViewModel.UtilisateurConnecte.EstAdmin)
            {
                MenuItem btnAdmin = new MenuItem();
                btnAdmin.Name = "btnAdmin";
                btnAdmin.Header = "Administrateur";
                btnAdmin.Click += btnAdmin_Click;
                btnAdmin.Style = (Style)this.FindResource("petitItem");
                ((MainWindow)System.Windows.Application.Current.MainWindow).menuMova.Items.Add(btnAdmin);
            }

			IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();

			mainVM.ChangeView<StylisteView>(new StylisteView());
		}

        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<AdminView>(new AdminView());
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

			//Gabriel Piché Cloutier - 2014/10/25
			//Avant de passer à l'écran Styliste, on vérifie si la BD est disponiblie
			try
			{
                StylisteView._historique = new History<UserControl>();

				mainVM.ChangeView<StylisteView>(new StylisteView());
			}
			catch(Exception)
			{
				lblErreur.Content = "Connexion impossible. Réessayez plus tard";
			}
        }

		



    }
}
