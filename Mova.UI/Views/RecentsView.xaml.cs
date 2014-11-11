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
    /// Logique d'interaction pour RecentsView.xaml
    /// </summary>
    public partial class RecentsView : UserControl
    {
        private RecentsViewModel ViewModel { get { return (RecentsViewModel)DataContext; } }
        public static History<UserControl> _historique = new History<UserControl>();
        public static UserControl derniereFenetre = null;

        public RecentsView()
        {
            InitializeComponent();

            ViewModel.afficherEnsemblesRecents();
        }
    }
}
