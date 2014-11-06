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

namespace Mova.UI.ViewModel
{
    class StylisteActiviteViewModel : BaseViewModel
    {
        private IActiviteService _activiteService;

        private ObservableCollection<Activite> _activites = new ObservableCollection<Activite>();

        /// <summary>
        /// 
        /// </summary>
        public StylisteActiviteViewModel()
        {
			try
			{
				Activites = new ObservableCollection<Activite>(ServiceFactory.Instance.GetService<IActiviteService>().RetrievePourMoment(Moment.GetIDMomentNow()));
			}
			catch(Exception)
			{
				throw;
			}
            
            MainViewModel app = (MainViewModel)ServiceFactory.Instance.GetService<IApplicationService>();

            app.MenuVisibility = System.Windows.Visibility.Visible;

            Listes.ListeActivites = Activites.ToList<Activite>();
            

            _activiteService = ServiceFactory.Instance.GetService<IActiviteService>();
           
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
        public void SetChoix(string contenu)
        {

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
          
            
      
        }
    }
}
