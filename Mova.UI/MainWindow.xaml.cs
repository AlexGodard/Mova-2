
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
using Mova.Logic.Services.Definitions;
using Mova.Logic.Services;
using Cstj.MvvmToolkit.Services;
using Mova.UI.ViewModel;
using Mova.UI.Views;
using Cstj.MvvmToolkit.Services.Definitions;
using Mova.Logic.Services.MySql;
using Mova.Logic;
using Mova.Logic.Models.Entities;

namespace Mova.UI
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainViewModel ViewModel { get { return (MainViewModel)DataContext; } }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            Configure();
            ViewModel.MenuVisibility = Visibility.Hidden;
            //Mets à jour le databinding de la vue présenté
            ViewModel.CurrentView = new ConnexionView();
        }


        private void Configure()
        {
            ServiceFactory.Instance.Register<IUtilisateurVetementService, MySqlUtilisateursVetements>(new MySqlUtilisateursVetements());
            ServiceFactory.Instance.Register<IUtilisateurEnsembleService, MySqlUtilisateurEnsemble>(new MySqlUtilisateurEnsemble());
            ServiceFactory.Instance.Register<IMomentService, MySqlMomentService>(new MySqlMomentService());
            ServiceFactory.Instance.Register<IEnsembleVetementService, MySqlEnsembleVetementService>(new MySqlEnsembleVetementService());
            ServiceFactory.Instance.Register<IEnsembleService, MySqlEnsembleService>(new MySqlEnsembleService());
            ServiceFactory.Instance.Register<IUtilisateurService, MySqlUtilisateurService>(new MySqlUtilisateurService());
            ServiceFactory.Instance.Register<IActiviteService, MySqlActiviteService>(new MySqlActiviteService());
            ServiceFactory.Instance.Register<IStyleService, MySqlStyleService>(new MySqlStyleService());
            ServiceFactory.Instance.Register<ITemperatureService, MySqlTemperatureService>(new MySqlTemperatureService());
            ServiceFactory.Instance.Register<IVetementService, MySqlVetementService>(new MySqlVetementService());
            ServiceFactory.Instance.Register<ICouleurService, MySqlCouleurService>(new MySqlCouleurService());
            ServiceFactory.Instance.Register<IApplicationService, MainViewModel>(ViewModel);

        }

        /// <summary>
        /// Méthode qui shutdown l'application
        /// </summary>
        private void Shutdown()
        {
            Application.Current.Shutdown();
        }

        private void MenuStyliste_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.AfficheMenu();
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();

            mainVM.ChangeView<StylisteActiviteView>(new StylisteActiviteView());
        }

        private void MenuRecents_Click(object sender, RoutedEventArgs e)
        {
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<RecentsView>(new RecentsView());
        }

        private void MenuFavoris_Click(object sender, RoutedEventArgs e)
        {
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<UserControl>(new FavorisView());
        }

        private void MenuMaGardeRobe_Click(object sender, RoutedEventArgs e)
        {
           if(Listes.UtilisateurConnecte.IdUtilisateur != null)
           { 
             IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
             mainVM.ChangeView<MaGardeRobeView>(new MaGardeRobeView());
           }
        }
        
        private void MenuActivites_Click(object sender, RoutedEventArgs e)
        {
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<ActiviteView>(new ActiviteView());
        }
        
        private void MenuStyles_Click(object sender, RoutedEventArgs e)
        {
            
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<StyleView>(new StyleView());
        }

        private void MenuConnDeconn_Click(object sender, RoutedEventArgs e)
        {
            
            //On cache le menu
            ViewModel.MenuVisibility = Visibility.Hidden;

            //Gabriel Piché Cloutier - 2014-11-05
            //On cache le bouton administrateur à la déconnexion
            ((MainWindow)System.Windows.Application.Current.MainWindow).btnAdmin.Header = "";
            ((MainWindow)System.Windows.Application.Current.MainWindow).btnAdmin.IsEnabled = false;

            //Modification pour réinitialiser l'utilisateur global - Gabriel Piché Cloutier - 2014-10-28
            Listes.UtilisateurConnecte = new Utilisateur();
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<ConnexionView>(new ConnexionView());
        }

        /// <summary>
        /// Méthode qui répond à l'événement click sur l'option quitter du menu
        /// </summary>
        /// <param name="sender">MenuItem : Quitter</param>
        /// <param name="e">RoutedEventArgs : Evenement de click</param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Shutdown();
        }

        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<MenuAdminView>(new MenuAdminView());
        }
    }
}
