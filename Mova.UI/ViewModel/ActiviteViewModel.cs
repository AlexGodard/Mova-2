using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cstj.MvvmToolkit;
using Cstj.MvvmToolkit.Services;
using Mova.Logic;
using Mova.Logic.Models;
using Mova.Logic.Services.Definitions;

namespace Mova.UI.ViewModel
{
    public class ActiviteViewModel : BaseViewModel
    {
        private IActiviteService _activiteService;

        public ActiviteViewModel()
        {
            Activites = new ObservableCollection<Activite>(ServiceFactory.Instance.GetService<IActiviteService>().RetrieveAll());
            Listes.ListeActivites = Activites.ToList<Activite>();
           _activiteService = ServiceFactory.Instance.GetService<IActiviteService>();
        }

        public ObservableCollection<Activite> Activites
        {
            get
            {
                return _activites;
            }

            set
            {
                if (_activites == value)
                {
                    return;
                }

                _activites
                 = value;
            }
        }

        private ObservableCollection<Activite> _activites = new ObservableCollection<Activite>();


    }
}
