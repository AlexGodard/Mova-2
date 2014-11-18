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
    class AjouterEnsembleViewModel : BaseViewModel
    {
        private IUtilisateurEnsembleService _utilisateurService;
        private IEnsembleService _ensembleService;

        private IUtilisateurVetementService _utilisateurVetementService;

        private ObservableCollection<UtilisateurEnsemble> _vetements = new ObservableCollection<UtilisateurEnsemble>();
        private ObservableCollection<UtilisateurVetements> _utilisateurVetement = new ObservableCollection<UtilisateurVetements>();
        private ObservableCollection<Ensemble> _ensemble = new ObservableCollection<Ensemble>();

        public AjouterEnsembleViewModel()
        {
            _utilisateurService = ServiceFactory.Instance.GetService<IUtilisateurEnsembleService>();
            _utilisateurVetementService = ServiceFactory.Instance.GetService<IUtilisateurVetementService>();
            _ensembleService = ServiceFactory.Instance.GetService<IEnsembleService>();  
            // On place dans la liste globale, la liste d'ensembles reçue
        }

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<UtilisateurEnsemble> UtilisateursEnsembles
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

        public ObservableCollection<UtilisateurVetements> UtilisateursVetements
        {
            get
            {
                return _utilisateurVetement;
            }

            set
            {
                if (_utilisateurVetement == value)
                {
                    return;
                }

                _utilisateurVetement = value;
            }
        }

        public ObservableCollection<Ensemble> Ensembles
        {
            get
            {
                return _ensemble;
            }

            set
            {
                if (_ensemble == value)
                {
                    return;
                }

                _ensemble = value;
            }
        }

        public void ajouterEnsemble(string nomEnsemble)
        {
           Ensemble NouvelleEnsemble = new Ensemble(nomEnsemble);
           int idNouvelEnsemble = _ensembleService.Create(NouvelleEnsemble); 
        }
    }
}
