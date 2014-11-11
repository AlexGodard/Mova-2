using Mova.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Mova.Logic.Models;
using Mova.Logic;
using Mova.Logic.Models.Entities;
using System.Threading;
using Cstj.MvvmToolkit.Services.Definitions;
using Cstj.MvvmToolkit.Services;

namespace Mova.UI.Views
{
    /// <summary>
    /// Logique d'interaction pour RecentsView.xaml
    /// </summary>
    public partial class RecentsView : UserControl
    {
        private RecentsViewModel ViewModel { get { return (RecentsViewModel)DataContext; } }
        public static History<UserControl> _historique = new History<UserControl>();
        public static UserControl derniereFenetre = null;

        private const int nbColumns = 3;
        private const int nbRows = 3;

        List<UtilisateurEnsemble> listeUtilisateurEnsembles = new List<UtilisateurEnsemble>();
        List<string> listeNomsEnsemble = new List<string>();
        public RecentsView()
        {
            InitializeComponent();

            DataContext = new RecentsViewModel();

            _historique.Ajouter(this);

            
            /*listeEnsemblesUtilisateurTrouves = ViewModel.chargerEnsemblesRecents();

            ViewModel.afficherEnsemblesRecents();*/
        }

        /*internal List<UtilisateurEnsemble> chargerEnsemblesRecents()
        {
            //On va chercher le numéro de fenêtre que l'on est (toujours le dernier car la fenêtre affichée sera toujours la dernière
            int noFenetre = _historique.GetNumberOfPage(this);
            int noPremierEnsembleAPrendre = (noFenetre) * nbColumns;
            List<EnsembleVetement> listeTemp = new List<EnsembleVetement>();

            // On va chercher tout les ensembles récents de l'utilisateur

            foreach (UtilisateurEnsemble utilisateurEnsemble in Listes.ListeEnsemblesUtilisateur)
            {
                // On crée une liste des noms 
                List<String> listeNomEnsembles = 
            }

            return listeTemp;
        }

        internal List<UtilisateurEnsemble> chargerEnsemblesRecents()
        {
            //On va chercher le numéro de fenêtre que l'on est (toujours le dernier car la fenêtre affichée sera toujours la dernière
            List<string> listeTemp = new List<string>();

            // On va chercher tout les ensembles récents de l'utilisateur

            foreach (UtilisateurEnsemble utilisateurEnsemble in Listes.ListeEnsemblesUtilisateur)
            {
                // On crée une liste des noms 
                listeTemp.Add(utilisateurEnsemble)
            }

            return listeTemp;
        }*/
    }
}
