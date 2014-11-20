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

namespace Mova.UI.ViewModel
{
    class VoirEnsembleViewModel : BasViewModel
    {
        private IVetementService _vetementService;
        private IUtilisateurEnsembleService _utilisateurEnsembleService;
        private IEnsembleVetementService _ensembleVetementService;

        private ObservableCollection<Vetement> _vetements = new ObservableCollection<Vetement>();       
        private ObservableCollection<UtilisateurEnsemble> _utilisateurEnsemble = new ObservableCollection<UtilisateurEnsemble>();
        private ObservableCollection<EnsembleVetement> _ensembleVetement = new ObservableCollection<EnsembleVetement>();

        public VoirEnsembleViewModel()
        {
            _vetementService = ServiceFactory.Instance.GetService<IVetementService>();
            _utilisateurEnsembleService = ServiceFactory.Instance.GetService<IUtilisateurEnsembleService>();
            _ensembleVetementService = ServiceFactory.Instance.GetService<IEnsembleVetementService>();

            EnsemblesVetements = new ObservableCollection<EnsembleVetement>(ServiceFactory.Instance.GetService<IUtilisateurEnsembleService>().RetrieveEnsembleVetement());
            Listes.ListesEnsembleUtilisateur = EnsemblesVetements.ToList<EnsembleVetement>();
        }

        public ObservableCollection<Vetement> Vetements
        {
            get
            {
                return _vetements;
            }

            set
            {
                if (_vetements == value)
                {
                    return;
                }

                _vetements = value;
            }
        }

        public ObservableCollection<UtilisateurEnsemble> UtilisateursEnsembles
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

                _utilisateurEnsemble = value;
            }
        }

        public ObservableCollection<EnsembleVetement> EnsemblesVetements
        {
            get
            {
                return _ensembleVetement;
            }

            set
            {
                if (_ensembleVetement == value)
                {
                    return;
                }

                _ensembleVetement = value;
            }
        }

    }
}
