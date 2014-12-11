using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cstj.MvvmToolkit;
using Cstj.MvvmToolkit.Services;
using Mova.Logic.Models;
using Mova.Logic.Models.Args;
using Mova.Logic.Models.Entities;
using Mova.Logic.Services.Definitions;
using Mova.Logic;

namespace Mova.UI.ViewModel
{
    public class InscriptionViewModel : BaseViewModel
    {
        private IUtilisateurService _utilisateurService;

        public RetrieveUtilisateurArgs RetrieveArgs { get; set; }
        private ObservableCollection<Utilisateur> _utilisateurs = new ObservableCollection<Utilisateur>();
        private Utilisateur _utilisateur = new Utilisateur();

        public InscriptionViewModel()
        {
            _utilisateurService = ServiceFactory.Instance.GetService<IUtilisateurService>();
            //usagers = new ObservableCollection<Utilisateur>(_utilisateurService.RetrieveAll());
            RetrieveArgs = new RetrieveUtilisateurArgs();
        }

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

        public bool verifierIdentifiant()
        {
            if (_utilisateurService.RetrieveIdentifiant(RetrieveArgs) != null)
            {
                return false;  // On a trouvé un identifiant, donc il n'est pas unique
            }

            return true;   /*Sinon, on retourne false.*/
        }

        public bool verifierCourriel()
        {
            if (_utilisateurService.RetrieveCourriel(RetrieveArgs) != null)
            {
                return false;  // On a trouvé un courriel identique, donc, il est pas unique
            }

            return true;   /*Sinon, on retourne true.*/
        }

        public void ajouterUtilisateur(string sexe)
        {
            RetrieveArgs.Sexe = sexe;

            Utilisateur utilisateurAAjouter = new Utilisateur(RetrieveArgs.IdUtilisateur, RetrieveArgs.Prenom, RetrieveArgs.Nom, RetrieveArgs.NomUtilisateur.ToLower(), RetrieveArgs.MotPasse, RetrieveArgs.Courriel, RetrieveArgs.Sexe, false);

            _utilisateurService.Create(utilisateurAAjouter);

            Listes.UtilisateurConnecte = utilisateurAAjouter;
            
        }
    }
}
