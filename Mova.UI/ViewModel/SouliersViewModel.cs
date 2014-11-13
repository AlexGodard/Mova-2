using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cstj.MvvmToolkit.Services;
using Mova.Logic;
using Mova.Logic.Models.Entities;
using Mova.Logic.Services.Definitions;

namespace Mova.UI.ViewModel
{
    class SouliersViewModel : BasViewModel
    {
        private IVetementService _vetementService;
        private ObservableCollection<Vetement> _vetements = new ObservableCollection<Vetement>();

        public SouliersViewModel()
        {
            _vetementService = ServiceFactory.Instance.GetService<IVetementService>();

            Vetements = new ObservableCollection<Vetement>(ServiceFactory.Instance.GetService<IVetementService>().RetrieveVetementTypeSpecificUtilisateur(3));

            // On place dans la liste globale, la liste d'ensembles reçue
            Listes.ListeSouliersUtilisateur = Vetements.ToList<Vetement>();
        }

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
    }
}
