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
    class EnsembleViewModel : BaseViewModel
    {
        private IEnsembleService _ensembleService;
        private ObservableCollection<EnsembleVetement> _ensembles = new ObservableCollection<EnsembleVetement>();

        /// <summary>
        /// 
        /// </summary>
        public EnsembleViewModel()
        {

            if (EnsembleView._historique.IsEmpty()){

                _ensembleService = ServiceFactory.Instance.GetService<IEnsembleService>();

                Ensembles = new ObservableCollection<EnsembleVetement>(ServiceFactory.Instance.GetService<IEnsembleService>().RetrieveSelection(Listes.InfoStyliste));

                // On place dans la liste globale, la liste d'ensembles reçue
                Listes.ListeEnsembles = Ensembles.ToList<EnsembleVetement>();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<EnsembleVetement> Ensembles
        {
            get
            {
                return _ensembles;
            }

            set
            {
                if (_ensembles == value)
                {
                    return;
                }

                _ensembles = value;
            }
        }

    }
}
