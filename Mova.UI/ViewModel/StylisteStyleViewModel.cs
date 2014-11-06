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

namespace Mova.UI.ViewModel
{
    class StylisteStyleViewModel : BaseViewModel
    {
        private IStyleService _styleService;

        private ObservableCollection<StyleVetement> _styles = new ObservableCollection<StyleVetement>();

        /// <summary>
        /// 
        /// </summary>
        public StylisteStyleViewModel()
        {
            try
            {
                Styles = new ObservableCollection<StyleVetement>(ServiceFactory.Instance.GetService<IStyleService>().RetrieveAll());
                //InitListeMoments();
            }
            catch (Exception)
            {
                throw;
            }

            MainViewModel app = (MainViewModel)ServiceFactory.Instance.GetService<IApplicationService>();

            app.MenuVisibility = System.Windows.Visibility.Visible;

            Listes.ListeStyles = Styles.ToList<StyleVetement>();

            _styleService = ServiceFactory.Instance.GetService<IStyleService>();

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
        public static void SetChoix(string contenu)
        {

            //On fait une requête à notre liste globale
            var query = from style in Listes.ListeStyles
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


        }


    }
}
