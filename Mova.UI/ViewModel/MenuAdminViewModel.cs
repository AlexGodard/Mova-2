﻿using System;
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
    public class MenuAdminViewModel : BaseViewModel
    {
        private IVetementService _vetementService;
        private IActiviteService _activiteService;
        private IStyleService _styleService;
        private ITemperatureService _temperatureService;
        private ICouleurService _couleurService;
        private IMomentService _momentService;

        public RetrieveVetementArgs RetrieveArgs { get; set; }

        public MenuAdminViewModel()
        {
            Activites = new ObservableCollection<Activite>(ServiceFactory.Instance.GetService<IActiviteService>().RetrieveAll());
            Styles = new ObservableCollection<StyleVetement>(ServiceFactory.Instance.GetService<IStyleService>().RetrieveAll());
            Temperatures = new ObservableCollection<Temperature>(ServiceFactory.Instance.GetService<ITemperatureService>().RetrieveAll());
            Couleurs = new ObservableCollection<Couleur>(ServiceFactory.Instance.GetService<ICouleurService>().RetrieveAll());
            Moments = new ObservableCollection<Moment>(ServiceFactory.Instance.GetService<IMomentService>().RetrieveAll());

            Listes.ListeActivites = Activites.ToList<Activite>();
            Listes.ListeStyles = Styles.ToList<StyleVetement>();
            Listes.ListeTemperatures = Temperatures.ToList<Temperature>();
            Listes.ListeCouleurs = Couleurs.ToList<Couleur>();
            Listes.ListeMoments = Moments.ToList<Moment>();

            _activiteService = ServiceFactory.Instance.GetService<IActiviteService>();
            _styleService = ServiceFactory.Instance.GetService<IStyleService>();
            _temperatureService = ServiceFactory.Instance.GetService<ITemperatureService>();
            _couleurService = ServiceFactory.Instance.GetService<ICouleurService>();
            _momentService = ServiceFactory.Instance.GetService<IMomentService>();
            
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

        public ObservableCollection<Moment> Moments
        {
            get
            {
                return _moments;
            }

            set
            {
                if (_moments == value)
                {
                    return;
                }

                _moments
                 = value;
            }
        }

        private ObservableCollection<StyleVetement> _styles = new ObservableCollection<StyleVetement>();
        private ObservableCollection<Activite> _activites = new ObservableCollection<Activite>();
        private ObservableCollection<Temperature> _temperatures = new ObservableCollection<Temperature>();
        private ObservableCollection<Couleur> _couleurs = new ObservableCollection<Couleur>();
        private ObservableCollection<Moment> _moments = new ObservableCollection<Moment>();

        public void ajouterActivite(string nomActivite, bool estOuvrable, bool estConge, List<Moment> listeMoments)
        {
            _activiteService.Create(new Activite(nomActivite), estOuvrable, estConge, listeMoments);
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

        public void modifierActivite(string nomActivite, string newActivite, bool estOuvrable, bool estConge, List<Moment> listeMoments)
        {
            _activiteService.Update(new Activite(nomActivite), newActivite, estOuvrable, estConge, listeMoments);
            // On doit reloader la liste un coup que la nouvelle activité est ajoutée.
            Activites = new ObservableCollection<Activite>(ServiceFactory.Instance.GetService<IActiviteService>().RetrieveAll());
            Listes.ListeActivites = Activites.ToList<Activite>();
        }

        public void modifierStyle(string nomStyle, string newStyle)
        {
            if (nomStyle != newStyle && newStyle != "")
            {
                _styleService.Update(new StyleVetement(nomStyle), newStyle);
                Styles = new ObservableCollection<StyleVetement>(ServiceFactory.Instance.GetService<IStyleService>().RetrieveAll());
                Listes.ListeStyles = Styles.ToList<StyleVetement>();
            }
        }

        public void modifierTemperature(string nomTemperature, string newTemperature)
        {
            if (nomTemperature != newTemperature && newTemperature != "")
            {
                _temperatureService.Update(new Temperature(nomTemperature), newTemperature);
                Temperatures = new ObservableCollection<Temperature>(ServiceFactory.Instance.GetService<ITemperatureService>().RetrieveAll());
                Listes.ListeTemperatures = Temperatures.ToList<Temperature>();
            }
        }

        public void modifierCouleur(string nomCouleur, string newCouleur)
        {
            if (nomCouleur != newCouleur && newCouleur != "")
            {
                _couleurService.Update(new Couleur(nomCouleur), newCouleur);
                Couleurs = new ObservableCollection<Couleur>(ServiceFactory.Instance.GetService<ICouleurService>().RetrieveAll());
                Listes.ListeCouleurs = Couleurs.ToList<Couleur>();
            }
        }

        public void supprimerActivite(string nomActivite)
        {
            _activiteService.Delete(new Activite(nomActivite));
            // On doit reloader la liste un coup que la nouvelle activité est ajoutée.
            Activites = new ObservableCollection<Activite>(ServiceFactory.Instance.GetService<IActiviteService>().RetrieveAll());
            Listes.ListeActivites = Activites.ToList<Activite>();
        }

        public void supprimerStyle(string nomStyle)
        {
            _styleService.Delete(new StyleVetement(nomStyle));
            Styles = new ObservableCollection<StyleVetement>(ServiceFactory.Instance.GetService<IStyleService>().RetrieveAll());
            Listes.ListeStyles = Styles.ToList<StyleVetement>();
        }

        public void supprimerTemperature(string nomTemperature)
        {
            _temperatureService.Delete(new Temperature(nomTemperature));
            Temperatures = new ObservableCollection<Temperature>(ServiceFactory.Instance.GetService<ITemperatureService>().RetrieveAll());
            Listes.ListeTemperatures = Temperatures.ToList<Temperature>();
        }

        public void supprimerCouleur(string nomCouleur)
        {
            _couleurService.Delete(new Couleur(nomCouleur));
            Couleurs = new ObservableCollection<Couleur>(ServiceFactory.Instance.GetService<ICouleurService>().RetrieveAll());
            Listes.ListeCouleurs = Couleurs.ToList<Couleur>();
        }

        public IList<Moment> chargerMomentsPourActivite(string nomActivite)
        {
            return _momentService.RetrieveSpecified(nomActivite);
        }

        public Activite chargerDetailsActivite(string nomActivite)
        {
            return _activiteService.RetrieveDetailsActivite(nomActivite);
        }
    }
}
