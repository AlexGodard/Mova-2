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
            if (EnsembleView._historique.IsEmpty())
            {

                _ensembleVetementService = ServiceFactory.Instance.GetService<IEnsembleVetementService>();

                EnsemblesVetements = new ObservableCollection<EnsembleVetement>(_ensembleVetementService.RetrieveSelection(Listes.InfoStyliste));

                List<EnsembleVetement> listeEnsembles = EnsemblesVetements.ToList<EnsembleVetement>();

                Listes.UtilisateurConnecte.ListeEnsembles =  new List<EnsembleVetement>(_ensembleVetementService.RetrieveEnsemblesUtilisateur(Listes.InfoStyliste));

                Listes.ListeEnsemblesVetements= (Listes.UtilisateurConnecte.ListeEnsembles);

                Listes.ListeEnsemblesVetements.AddRange(FiltrerEnsembles(EnsemblesVetements.ToList<EnsembleVetement>()));
            }
        }

        private List<EnsembleVetement> FiltrerEnsembles(List<EnsembleVetement> liste)
        {

            List<EnsembleVetement> listeTemp = new List<EnsembleVetement>();

            //On va tester les matchs
            foreach (EnsembleVetement ev in liste)
            {
                //Si les vêtements ne sont pas considérés comme étant matchable
                if (ValiderMatchListeVetement(ev.ListeVetements))
                {
                    listeTemp.Add(ev);
                    
                    //liste.Remove(ev);

                }

            }

            return listeTemp;

        }

        private bool ValiderMatchListeVetement(List<Vetement> listeVetements)
        {

            //Sachant qu'on a toujours 3 vêtements

            return (listeVetements[0].CouleurVetement.IdCouleur == listeVetements[1].CouleurVetement.IdCouleur && listeVetements[1].CouleurVetement.IdCouleur == listeVetements[2].CouleurVetement.IdCouleur) ? true : false;

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
