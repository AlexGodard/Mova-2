using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cstj.MvvmToolkit;
using Cstj.MvvmToolkit.Services;
using Cstj.MvvmToolkit.Services.Definitions;
using Mova.Logic;
using Mova.Logic.Models;
using Mova.Logic.Services.Definitions;
using Mova.Logic.Models.Args;
using Mova.Logic.Models.Entities;

namespace Mova.UI.ViewModel
{
    public class StylisteViewModel : BaseViewModel
    {
        private IActiviteService _activiteService;
        private IStyleService _styleService;
        private IUtilisateurEnsembleService _utilisateurEnsembleService;
        private IUtilisateurVetementService _utilisateurVetementService;

        private ObservableCollection<StyleVetement> _styles = new ObservableCollection<StyleVetement>();
        private ObservableCollection<Activite> _activites = new ObservableCollection<Activite>();
        private ObservableCollection<UtilisateurEnsemble> _utilisateurEnsemble = new ObservableCollection<UtilisateurEnsemble>();
        private ObservableCollection<UtilisateurVetements>  _utilisateurVetement = new ObservableCollection<UtilisateurVetements>();

        //private static InfoStylisteArgs _infoTemp = new InfoStylisteArgs();

        /// <summary>
        /// 
        /// </summary>
        public StylisteViewModel()
        {
			try
			{
				Activites = new ObservableCollection<Activite>(ServiceFactory.Instance.GetService<IActiviteService>().RetrievePourMoment(Moment.GetIDMomentNow()));
				Styles = new ObservableCollection<StyleVetement>(ServiceFactory.Instance.GetService<IStyleService>().RetrieveAll());
                UtilisateursEnsembles =  new ObservableCollection<UtilisateurEnsemble>(ServiceFactory.Instance.GetService<IUtilisateurEnsembleService>().RetrieveEnsembleUtilisateurPrecis());
                UtilisateursVetements = new ObservableCollection<UtilisateurVetements>(ServiceFactory.Instance.GetService<IUtilisateurVetementService>().RetrieveAll());

			}
			catch(Exception)
			{
				throw;
			}
            
            MainViewModel app = (MainViewModel)ServiceFactory.Instance.GetService<IApplicationService>();

            app.MenuVisibility = System.Windows.Visibility.Visible;

            Listes.ListeActivites = Activites.ToList<Activite>();
            Listes.ListeStyles = Styles.ToList<StyleVetement>();
            Listes.ListeEnsemblesUtilisateur = UtilisateursEnsembles.ToList<UtilisateurEnsemble>();
            Listes.NbEnsembleUtilisateur = UtilisateursEnsembles.Count();

            _activiteService = ServiceFactory.Instance.GetService<IActiviteService>();
            _styleService = ServiceFactory.Instance.GetService<IStyleService>();
            _utilisateurEnsembleService = ServiceFactory.Instance.GetService<IUtilisateurEnsembleService>();
            
        }

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        // ObservableCollection<StyleVetement> ou ObservableCollection<Activite>
        private ObservableCollection<T> returnRandomListe<T>(ObservableCollection<T> oc)
        {   
            //Variables
            int nbARetourner = 4;
            int nbMax = oc.Count;
            ObservableCollection<T> liste = new ObservableCollection<T>();
            Random rdm = new Random();

            for (int i = 0; i < nbARetourner; i++)
			{
			    int randomized = rdm.Next(1, nbMax);

                T sv = oc.ElementAtOrDefault<T>(randomized);

                liste.Add(sv);
                
			}
        
        
        return liste;
            
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="contenu"></param>
        public void SetChoix(int index, string contenu)
        {

            switch (index)
            { 
                case 1:
                    //On fait une requête à notre liste globale
                    var query = from activite in Listes.ListeActivites
                                where activite.NomActivite == contenu
                                select activite.IdActivite;

                    try
                    {
                        Listes.InfoStyliste.IdActivite = Convert.ToInt32(query.FirstOrDefault().ToString());
                    }
                    catch (Exception e)
                    {
                        Listes.InfoStyliste.IdActivite = 1;                    
                    }
                    

                    break;
                case 2:
                    //On fait une requête à notre liste globale
                    query = from style in Listes.ListeStyles
                                where style.NomStyle == contenu
                                select style.IdStyle;

                    try
                    {
                        Listes.InfoStyliste.IdStyle = Convert.ToInt32(query.FirstOrDefault().ToString());
                    }
                    catch (Exception e)
                    {
                        Listes.InfoStyliste.IdStyle = 1;
                    }

                    //Listes.InfoStyliste = _infoTemp;

                    break;
                default:
                    break;
            
            
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /*private void InitListeMoments()
        {

            List<Moment> lstTemp = new List<Moment>(ServiceFactory.Instance.GetService<IMomentService>().RetrieveAll());
        
        }*/

    }
}
