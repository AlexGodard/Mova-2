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
using Mova.Logic.Models.Entities;
using Mova.Logic.Services.Definitions;

namespace Mova.UI.ViewModel
{
    public class MaGardeRobeViewModel : BaseViewModel
    {           
        private IUtilisateurEnsembleService _utilisateurEnsembleService;
        private IUtilisateurVetementService _utilisateurVetementservice;
        private IVetementService _vetementService;

        private ObservableCollection<UtilisateurEnsemble> _utilisateurEnsembles = new ObservableCollection<UtilisateurEnsemble>();
        private UtilisateurEnsemble _utilisateurEnsemble = new UtilisateurEnsemble();

        public MaGardeRobeViewModel()
        {
            _utilisateurEnsembleService = ServiceFactory.Instance.GetService<IUtilisateurEnsembleService>();
            _utilisateurVetementservice = ServiceFactory.Instance.GetService<IUtilisateurVetementService>();
            countTypeVetement();           
        }

        public UtilisateurEnsemble UtilisateurEnsembleConnecte
        {
            get
            {
                return _utilisateurEnsemble;
            }

            set
            {
                if (_utilisateurEnsemble == value)
                {
                    return;
                }

                RaisePropertyChanging();
                _utilisateurEnsemble = value;
                RaisePropertyChanged();
            }
        }

        public void countTypeVetement()
        {

             _vetementService = ServiceFactory.Instance.GetService<IVetementService>();
             IList<int> listeType = _vetementService.RetrieveIdTypeAll();
             
             foreach(int i in listeType)
             { 
               switch(i)
               {
                 case 1:
                     Listes.NbHauts++;
                  break;

                 case 2:
                     Listes.NbBas++;
                  break;

                 case 3:
                     Listes.NbSouliers++;
                  break;
               }
             }
        }
    }
}
