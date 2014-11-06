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
using Mova.Logic.Models.Entities;
using Mova.Logic.Services.Definitions;

namespace Mova.UI.ViewModel
{
    public class MaGardeRobeViewModel : BaseViewModel
    {           
        private IUtilisateurEnsembleService _utilisateurEnsembleService;
        private IUtilisateurVetementService _utilisateurVetementservice;
        private IVetementService _vetementService;

        private UtilisateurEnsemble _utilisateurEnsemble = new UtilisateurEnsemble();

        private ObservableCollection<UtilisateurEnsemble> _utilisateurEnsembles = new ObservableCollection<UtilisateurEnsemble>();
        private ObservableCollection<UtilisateurVetements> _utilisateurVetement = new ObservableCollection<UtilisateurVetements>();

        public MaGardeRobeViewModel()
        {
            UtilisateursEnsembles = new ObservableCollection<UtilisateurEnsemble>(ServiceFactory.Instance.GetService<IUtilisateurEnsembleService>().RetrieveEnsembleUtilisateurPrecis());
            UtilisateursVetements = new ObservableCollection<UtilisateurVetements>(ServiceFactory.Instance.GetService<IUtilisateurVetementService>().RetrieveAll());

            Listes.ListeEnsemblesUtilisateur = UtilisateursEnsembles.ToList<UtilisateurEnsemble>();

            Listes.NbEnsembleUtilisateur = UtilisateursEnsembles.Count();

            Listes.ListeVetementsUtilisateur = UtilisateursVetements.ToList<UtilisateurVetements>();

            countTypeVetement();    
                   
            _utilisateurEnsembleService = ServiceFactory.Instance.GetService<IUtilisateurEnsembleService>();
            _utilisateurVetementservice = ServiceFactory.Instance.GetService<IUtilisateurVetementService>();        
        }

        public UtilisateurEnsemble UtilisateurEnsembleConnecte
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

                RaisePropertyChanging();
                _utilisateurEnsemble = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<UtilisateurEnsemble> UtilisateursEnsembles
        {
            get
            {
                return _utilisateurEnsembles;
            }

            set
            {
                if (_utilisateurEnsembles == value)
                {
                    return;
                }

                _utilisateurEnsembles = value;
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



        public void countTypeVetement()
        {

             _vetementService = ServiceFactory.Instance.GetService<IVetementService>();
             IList<int> listeType = _vetementService.RetrieveIdTypeAll();
             
             foreach(int i in listeType)
             { 
               switch(i)
               {
                 case 1:
                     Listes.NbHauts++;
                  break;

                 case 2:
                     Listes.NbBas++;
                  break;

                 case 3:
                     Listes.NbSouliers++;
                  break;
               }
             }
        }
    }
}
