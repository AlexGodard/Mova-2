using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cstj.MvvmToolkit;
using Cstj.MvvmToolkit.Services;
using Mova.Logic;
using Mova.Logic.Models.Entities;
using Mova.Logic.Services.Definitions;

namespace Mova.UI.ViewModel
{
    class AjouterSoulierViewModel : BaseViewModel
    {
        private IVetementService _vetementService;
        private IUtilisateurVetementService _utilisateurVetementService;
        private ObservableCollection<Vetement> _vetements = new ObservableCollection<Vetement>();
        private ObservableCollection<UtilisateurVetements> _utilisateurVetement = new ObservableCollection<UtilisateurVetements>();

        /// <summary>
        /// 
        /// </summary>
        public AjouterSoulierViewModel()
        {
            _vetementService = ServiceFactory.Instance.GetService<IVetementService>();
            _utilisateurVetementService = ServiceFactory.Instance.GetService<IUtilisateurVetementService>();
            Vetements = new ObservableCollection<Vetement>(ServiceFactory.Instance.GetService<IVetementService>().RetrieveVetementTypeSpecific(3));
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


        public void ajouteVetement(string href)
        {
           _utilisateurVetementService.InsertVetementUtilisateur(href);
        }
    }
}
