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
using Mova.Logic;
using Mova.UI.ViewModel;

namespace Mova.UI.Views
{
    /// <summary>
    /// Logique d'interaction pour MaGardeRobeView.xaml
    /// </summary>
    public partial class MaGardeRobeView : UserControl
    {
        public MaGardeRobeView()
        {
            InitializeComponent();
            DataContext = new MaGardeRobeViewModel();
            lblNbEnsemble.Content = "Vous avez " + Listes.NbEnsembleUtilisateur + " ensembles";
            
        }

        
    }
}
