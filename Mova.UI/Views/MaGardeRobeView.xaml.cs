﻿using System;
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
using Mova.Logic;
using Mova.UI.ViewModel;

namespace Mova.UI.Views
{
    /// <summary>
    /// Logique d'interaction pour MaGardeRobeView.xaml
    /// </summary>
    public partial class MaGardeRobeView : UserControl
    {
        private MaGardeRobeView ViewModel { get { return (MaGardeRobeView)DataContext; } }

        public MaGardeRobeView()
        {        
            InitializeComponent();
            DataContext = new MaGardeRobeViewModel();
            lblNbEnsemble.Content = "Vous avez " + Listes.NbEnsembleUtilisateur + " ensembles";
            lblNbHauts.Content = "Vous avez " + Listes.NbHauts + " hauts";
            lblNbBas.Content = "Vous avez " + Listes.NbBas + " bas";
            lblNbSouliers.Content = "Vous avez " + Listes.NbSouliers + " souliers";

        }

        private void btnvoirHauts_Click(object sender, RoutedEventArgs e)
        {
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<HautsView>(new HautsView());
        }

        private void btnvoirBas_Click(object sender, RoutedEventArgs e)
        {
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<BasView>(new BasView());
        }

        private void btnvoirSouliers_Click(object sender, RoutedEventArgs e)
        {
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<SouliersView>(new SouliersView());
        }

        private void ajouterHaut_Click(object sender, RoutedEventArgs e)
        {
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<AjouterHautView>(new AjouterHautView());
        }

        private void ajouterBas_Click(object sender, RoutedEventArgs e)
        {
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<AjouterBasView>(new AjouterBasView());
        }

        private void ajouterSouliers_Click(object sender, RoutedEventArgs e)
        {
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<AjouterSoulierView>(new AjouterSoulierView());
        }

        private void ajouterEnsemble_Click(object sender, RoutedEventArgs e)
        {
            Listes.AjouterEnsemble = true;
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<AjouterHautView>(new AjouterHautView());
        }
    }
}
