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
        private IUtilisateurEnsembleService _utilisateurEnsembleService;
        private IEnsembleService _ensembleService;
        private IEnsembleVetementService _ensembleVetementService;
        private IVetementService _vetementService;

        private ObservableCollection<UtilisateurEnsemble> _vetements = new ObservableCollection<UtilisateurEnsemble>();
        private ObservableCollection<Ensemble> _ensemble = new ObservableCollection<Ensemble>();
        private ObservableCollection<EnsembleVetement> _ensembleVetement = new ObservableCollection<EnsembleVetement>();
        private ObservableCollection<Vetement> _vetement = new ObservableCollection<Vetement>();

        public AjouterEnsembleViewModel()
        {
            _utilisateurEnsembleService = ServiceFactory.Instance.GetService<IUtilisateurEnsembleService>();
            _ensembleService = ServiceFactory.Instance.GetService<IEnsembleService>();
            _ensembleVetementService = ServiceFactory.Instance.GetService<IEnsembleVetementService>();
            _vetementService = ServiceFactory.Instance.GetService<IVetementService>();
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

        public ObservableCollection<Vetement> Vetements
        {
            get
            {
                return _vetement;
            }

            set
            {
                if (_vetement == value)
                {
                    return;
                }

                _vetement = value;
            }
        }
        public void ajouterEnsemble(string nomEnsemble)
        {
            IList<Vetement> ListeVetementAjouter = new List<Vetement>();
            Ensemble NouvelEnsemble = new Ensemble(nomEnsemble);
            NouvelEnsemble.NomEnsemble = nomEnsemble;
            int idNouvelEnsemble = _ensembleService.Create(NouvelEnsemble);
            _utilisateurEnsembleService.InsertAvecId(idNouvelEnsemble);
            ListeVetementAjouter = _vetementService.RetrieveVetementAvecURl(Listes.ListeEnsembleAjouter);  
            _ensembleVetementService.CreateEnsemble(ListeVetementAjouter,idNouvelEnsemble);
            Listes.ListeEnsembleAjouter.Clear();
            Listes.AjouterEnsemble = false;
        } 

    }
}
