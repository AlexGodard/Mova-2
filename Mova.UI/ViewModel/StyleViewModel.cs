using Cstj.MvvmToolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mova.Logic.Services.Definitions;
using System.Collections.ObjectModel;
using Mova.Logic.Models;
using Cstj.MvvmToolkit.Services;
using Cstj.MvvmToolkit.Services.Definitions;
using Mova.Logic;

namespace Mova.UI.ViewModel
{
    public class StyleViewModel : BaseViewModel
    {

    private IStyleService _styleService;

        private ObservableCollection<StyleVetement> _styles = new ObservableCollection<StyleVetement>();

        /// <summary>
        /// 
        /// </summary>
        public StyleViewModel()
        {
            try
            {
                Styles = new ObservableCollection<StyleVetement>(ServiceFactory.Instance.GetService<IStyleService>().RetrieveAll());
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

    }
}
