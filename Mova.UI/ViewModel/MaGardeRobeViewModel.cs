using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cstj.MvvmToolkit;
using Cstj.MvvmToolkit.Services;
using Mova.Logic.Models.Entities;
using Mova.Logic.Services.Definitions;

namespace Mova.UI.ViewModel
{
    public class MaGardeRobeViewModel : BaseViewModel
    {
        private IUtilisateurService _utilisateurService;
        private ObservableCollection<Utilisateur> _utilisateurs = new ObservableCollection<Utilisateur>();
        private Utilisateur _utilisateur = new Utilisateur();

        public MaGardeRobeViewModel()
        {
            _utilisateurService = ServiceFactory.Instance.GetService<IUtilisateurService>();
        }

        public Utilisateur UtilisateurConnecte
        {
            get
            {
                return _utilisateur;
            }

            set
            {
                if (_utilisateur == value)
                {
                    return;
                }

                RaisePropertyChanging();
                _utilisateur = value;
                RaisePropertyChanged();
            }
        }
    }
}
