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
    class AjouterSoulierViewModel : BaseViewModel
    {
        private IVetementService _vetementService;
        private IUtilisateurVetementService _utilisateurVetementService;
        private IEnsembleService _ensembleService;
        private ObservableCollection<Vetement> _vetements = new ObservableCollection<Vetement>();
        private ObservableCollection<UtilisateurVetements> _utilisateurVetement = new ObservableCollection<UtilisateurVetements>();
        private ObservableCollection<Ensemble> _ensemble = new ObservableCollection<Ensemble>();

        /// <summary>
        /// 
        /// </summary>
        public AjouterSoulierViewModel()
        {
            _vetementService = ServiceFactory.Instance.GetService<IVetementService>();
            _utilisateurVetementService = ServiceFactory.Instance.GetService<IUtilisateurVetementService>();
            _ensembleService = ServiceFactory.Instance.GetService<IEnsembleService>();
            if (Listes.AjouterEnsemble == true)
            {
                Vetements = new ObservableCollection<Vetement>(ServiceFactory.Instance.GetService<IVetementService>().RetrieveVetementTypeSpecific(3, true));

            }
            else
            {
                Vetements = new ObservableCollection<Vetement>(ServiceFactory.Instance.GetService<IVetementService>().RetrieveVetementTypeSpecific(3, false));
            }
            // On place dans la liste globale, la liste d'ensembles reçue
            Listes.ListeSouliersComplet = Vetements.ToList<Vetement>();
        }

        /// <summary>
        /// 
        /// </summary>
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


        public void ajouteVetement(string href)
        {
           _utilisateurVetementService.InsertVetementUtilisateur(href);
        }
    }
}
