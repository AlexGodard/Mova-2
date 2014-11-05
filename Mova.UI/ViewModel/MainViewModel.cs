using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Cstj.MvvmToolkit;
using Cstj.MvvmToolkit.Services.Definitions;

namespace Mova.UI.ViewModel
{
    public class MainViewModel : BaseViewModel, IApplicationService
    {
        private UserControl _currentView;

        private Visibility _menuVisibility;

        public Visibility MenuVisibility
        {
            get { return _menuVisibility;}
            set
            {
                RaisePropertyChanging();
                _menuVisibility = value;
                RaisePropertyChanged();
            }

        }


        public MainViewModel()
        {
            MenuVisibility = Visibility.Hidden;
        }

        public void AfficheMenu()
        {
            MenuVisibility = Visibility.Visible;
        }


        public UserControl CurrentView
        {
            get
            {
                return _currentView;
            }

            set
            {
                if (_currentView == value)
                {
                    return;
                }

                RaisePropertyChanging();
                _currentView = value;
                RaisePropertyChanged();
            }
        }

        
        public void ChangeView<T>(T view)
        {
            CurrentView = view as UserControl;
        }


    }
}