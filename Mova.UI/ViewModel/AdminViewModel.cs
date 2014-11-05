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
using Mova.Logic.Models.Args;
using Mova.Logic.Models.Entities;
using Mova.Logic.Services.Definitions;

namespace Mova.UI.ViewModel
{
    public class AdminViewModel : BaseViewModel
    {
        private IVetementService _vetementService;
        private IActiviteService _activiteService;
        private IStyleService _styleService;
        private ITemperatureService _temperatureService;
        private ICouleurService _couleurService;

        public RetrieveVetementArgs RetrieveArgs { get; set; }

        public AdminViewModel()
        {
            Activites = new ObservableCollection<Activite>(ServiceFactory.Instance.GetService<IActiviteService>().RetrieveAll());
            Styles = new ObservableCollection<StyleVetement>(ServiceFactory.Instance.GetService<IStyleService>().RetrieveAll());
            Temperatures = new ObservableCollection<Temperature>(ServiceFactory.Instance.GetService<ITemperatureService>().RetrieveAll());
            Couleurs = new ObservableCollection<Couleur>(ServiceFactory.Instance.GetService<ICouleurService>().RetrieveAll());

            Listes.ListeActivites = Activites.ToList<Activite>();
            Listes.ListeStyles = Styles.ToList<StyleVetement>();
            Listes.ListeTemperatures = Temperatures.ToList<Temperature>();
            Listes.ListeCouleurs = Couleurs.ToList<Couleur>();

            _activiteService = ServiceFactory.Instance.GetService<IActiviteService>();
            _styleService = ServiceFactory.Instance.GetService<IStyleService>();
            _temperatureService = ServiceFactory.Instance.GetService<ITemperatureService>();
            _couleurService = ServiceFactory.Instance.GetService<ICouleurService>();
            
            _vetementService = ServiceFactory.Instance.GetService<IVetementService>();
            RetrieveArgs = new RetrieveVetementArgs();
        }

        public ObservableCollection<Activite> Activites
        {
            get
            {
                return _activites;
            }

            set
            {
                if (_activites == value)
                {
                    return;
                }

                _activites
                 = value;
            }
        }


        public ObservableCollection<StyleVetement> Styles
        {
            get
            {
                return _styles;
            }

            set
            {
                if (_styles == value)
                {
                    return;
                }

                _styles
                 = value;
            }
        }

        public ObservableCollection<Temperature> Temperatures
        {
            get
            {
                return _temperatures;
            }

            set
            {
                if (_temperatures == value)
                {
                    return;
                }

                _temperatures
                 = value;
            }
        }

        public ObservableCollection<Couleur> Couleurs
        {
            get
            {
                return _couleurs;
            }

            set
            {
                if (_couleurs == value)
                {
                    return;
                }

                _couleurs
                 = value;
            }
        }

        private ObservableCollection<StyleVetement> _styles = new ObservableCollection<StyleVetement>();
        private ObservableCollection<Activite> _activites = new ObservableCollection<Activite>();
        private ObservableCollection<Temperature> _temperatures = new ObservableCollection<Temperature>();
        private ObservableCollection<Couleur> _couleurs = new ObservableCollection<Couleur>();

        public void ajouterActivite(string nomActivite)
        {
            _activiteService.Create(new Activite(nomActivite));
            // On doit reloader la liste un coup que la nouvelle activité est ajoutée.
            Activites = new ObservableCollection<Activite>(ServiceFactory.Instance.GetService<IActiviteService>().RetrieveAll());
            Listes.ListeActivites = Activites.ToList<Activite>();
        }

        public void ajouterStyle(string nomStyle)
        {
            _styleService.Create(new StyleVetement(nomStyle));
            Styles = new ObservableCollection<StyleVetement>(ServiceFactory.Instance.GetService<IStyleService>().RetrieveAll());
            Listes.ListeStyles = Styles.ToList<StyleVetement>();
        }

        public void ajouterTemperature(string nomTemperature)
        {
            _temperatureService.Create(new Temperature(nomTemperature));
            Temperatures = new ObservableCollection<Temperature>(ServiceFactory.Instance.GetService<ITemperatureService>().RetrieveAll());
            Listes.ListeTemperatures = Temperatures.ToList<Temperature>();
        }

        public void ajouterCouleur(string nomCouleur)
        {
            _couleurService.Create(new Couleur(nomCouleur));
            Couleurs = new ObservableCollection<Couleur>(ServiceFactory.Instance.GetService<ICouleurService>().RetrieveAll());
            Listes.ListeCouleurs = Couleurs.ToList<Couleur>();
        }

        public void ajouterVetement(TypeVetement typeVetement, bool estHomme, bool estFemme, Couleur couleur, List<Activite> listeActivites, List<StyleVetement> listeStyles, List<Temperature> listeTemperatures)
        {
            RetrieveArgs.Type = typeVetement;
            RetrieveArgs.CouleurVetement = couleur;
            RetrieveArgs.EstHomme = estHomme;
            RetrieveArgs.EstFemme = estFemme;
            RetrieveArgs.ListeActivites = listeActivites;
            RetrieveArgs.ListeStyles = listeStyles;
            RetrieveArgs.ListeTemperatures = listeTemperatures;

            Vetement vetementAAjouter = new Vetement(RetrieveArgs.Type, RetrieveArgs.CouleurVetement, RetrieveArgs.NomVetement, RetrieveArgs.ImageURL, RetrieveArgs.Prix, RetrieveArgs.EstHomme, RetrieveArgs.EstFemme, RetrieveArgs.ListeActivites, RetrieveArgs.ListeStyles, RetrieveArgs.ListeTemperatures);

            _vetementService.Create(vetementAAjouter);
        }
    }
}
