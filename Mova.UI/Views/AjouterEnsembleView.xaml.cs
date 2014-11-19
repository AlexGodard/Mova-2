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
using Cstj.MvvmToolkit;
using Cstj.MvvmToolkit.Services;
using Cstj.MvvmToolkit.Services.Definitions;
using Mova.Logic;
using Mova.UI.ViewModel;

namespace Mova.UI.Views
{
    /// <summary>
    /// Logique d'interaction pour AjouterEnsemble.xaml
    /// </summary>
    public partial class AjouterEnsembleView : UserControl
    {
        private AjouterEnsembleViewModel ViewModel { get { return (AjouterEnsembleViewModel)DataContext; } }

        public AjouterEnsembleView()
        {
            InitializeComponent();

            try
            {
                DataContext = new AjouterEnsembleViewModel();
            }
            catch (Exception)
            {
                throw;
            }

            BitmapImage img1 = new BitmapImage();
            img1.BeginInit();
            img1.UriSource = new Uri(Listes.ListeEnsembleAjouter.ElementAt(0).ToString());
            img1.EndInit();
            BitmapImage img2 = new BitmapImage();
            img2.BeginInit();
            img2.UriSource = new Uri(Listes.ListeEnsembleAjouter.ElementAt(2).ToString());
            img2.EndInit();
            BitmapImage img3 = new BitmapImage();
            img3.BeginInit();
            img3.UriSource = new Uri(Listes.ListeEnsembleAjouter.ElementAt(4).ToString());
            img3.EndInit();

            imgHautsEnseble.Source = img1;
            imgBasEnsebles.Source = img2;
            imgSouliersEnsemble.Source = img3;
        }

        private void txtNomEnsemble_Change(object sender, TextChangedEventArgs e)
        {
           if(txtNomEsemble.Text != "")
           { 
             btnAjouterEnsemble.IsEnabled = true;
           }
           else
           {
             btnAjouterEnsemble.IsEnabled = false;
           }
        }

        private void btnAjouterEnsemble_Click(object sender, RoutedEventArgs e)
        {
            string nomEnsemble = txtNomEsemble.Text;
            ViewModel.ajouterEnsemble(nomEnsemble);
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<MaGardeRobeView>(new MaGardeRobeView());   
        }
    }
}
