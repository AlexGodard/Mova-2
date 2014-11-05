using Cstj.MvvmToolkit;
using Mova.Logic.Models;
using Mova.Logic.Models.Args;
using Mova.Logic.Services.Definitions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cstj.MvvmToolkit.Services;
using Mova.Logic.Models.Entities;
using Mova.Logic;

namespace Mova.UI.ViewModel
{
    public class ConnexionViewModel : BaseViewModel
    {
        private IUtilisateurService _utilisateurService;

        public RetrieveUtilisateurArgs RetrieveArgs { get; set; }
        private Utilisateur _utilisateur = new Utilisateur();

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

        #region Constructeur

        public ConnexionViewModel()
        {
            /*Usager = new Utilisateur {        idUtilisateur=0,
                                              prenom = "Unset",
                                              nom = "Unset",
                                              nomUtilisateur = "UnSet",
                                              courriel = "Unset",
                                              motDePasse = "Unset",
                                              sexe = "Unset"
                                     };   */

            _utilisateurService = ServiceFactory.Instance.GetService<IUtilisateurService>();
            //usagers = new ObservableCollection<Utilisateur>(_utilisateurService.RetrieveAll());
            RetrieveArgs = new RetrieveUtilisateurArgs();
        }

        #endregion

        public void RechercheUtilisateur()
        {
            Listes.UtilisateurConnecte = new Utilisateur(_utilisateurService.Retrieve(RetrieveArgs));
        }
    }
}
