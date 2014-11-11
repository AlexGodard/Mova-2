using Cstj.MvvmToolkit;
using Cstj.MvvmToolkit.Services;
using Mova.Logic;
using Mova.Logic.Models;
using Mova.Logic.Models.Entities;
using Mova.Logic.Services.Definitions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mova.UI.Views;

namespace Mova.UI.ViewModel
{

    /// <summary>
    /// 
    /// </summary>
    class EnsembleVetementViewModel : BaseViewModel
    {
        private IEnsembleVetementService _ensembleVetementService;
        private ObservableCollection<EnsembleVetement> _ensemblesVetements = new ObservableCollection<EnsembleVetement>();

        /// <summary>
        /// 
        /// </summary>
        public EnsembleVetementViewModel()
        {
            if (EnsembleView._historique.IsEmpty()){

                _ensembleVetementService = ServiceFactory.Instance.GetService<IEnsembleVetementService>();

                EnsemblesVetements = new ObservableCollection<EnsembleVetement>(ServiceFactory.Instance.GetService<IEnsembleVetementService>().RetrieveSelection(Listes.InfoStyliste));

                // On place dans la liste globale, la liste d'ensembles reçue
                Listes.ListeEnsemblesVetements = EnsemblesVetements.ToList<EnsembleVetement>();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<EnsembleVetement> EnsemblesVetements
        {
            get
            {
                return _ensemblesVetements;
            }

            set
            {
                if (_ensemblesVetements == value)
                {
                    return;
                }

                _ensemblesVetements = value;
            }
        }

    }
}
