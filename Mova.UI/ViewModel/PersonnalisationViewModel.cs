using Cstj.MvvmToolkit;
using Cstj.MvvmToolkit.Services;
using Mova.Logic;
using Mova.Logic.Models;
using Mova.Logic.Models.Entities;
using Mova.Logic.Services.Definitions;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mova.UI.ViewModel
{
    class PersonnalisationViewModel : BaseViewModel
    {

        private IEnsembleService _ensembleService;
        private IUtilisateurEnsembleService _utilisateurEnsembleService;
        private IEnsembleVetementService _ensembleVetementService;
        private ObservableCollection<Ensemble> _ensembles = new ObservableCollection<Ensemble>();
        private ObservableCollection<EnsembleVetement> _ensemblesVetements = new ObservableCollection<EnsembleVetement>();
        private ObservableCollection<UtilisateurEnsemble> _utilisateurEnsembles = new ObservableCollection<UtilisateurEnsemble>();

        public int i = 0, j = 0, k = 0;

        // On ajoute tout les vêtements dans des listes de vêtements temporaires
        List<Vetement> listeHauts = new List<Vetement>();
        List<Vetement> listeBas = new List<Vetement>();
        List<Vetement> listeChaussures = new List<Vetement>();

        /// <summary>
        /// 
        /// </summary>
        public PersonnalisationViewModel()
        {
            _ensembleService = ServiceFactory.Instance.GetService<IEnsembleService>();
            Ensembles = new ObservableCollection<Ensemble>(ServiceFactory.Instance.GetService<IEnsembleService>().RetrieveAll());
            _utilisateurEnsembleService = ServiceFactory.Instance.GetService<IUtilisateurEnsembleService>();

            

            //EnsemblesVetements = new ObservableCollection<EnsembleVetement>(ServiceFactory.Instance.GetService<IEnsembleVetementService>().RetrieveAll());
            _ensembleVetementService = ServiceFactory.Instance.GetService<IEnsembleVetementService>();

            // On place dans la liste globale, la liste d'ensembles reçue
            Listes.ListeEnsembles = Ensembles.ToList<Ensemble>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ev"></param>
        public void AjouterEnsemble(EnsembleVetement ev)
        {
            //_utilisateurEnsembleService.AjouterFavori(ev);
        }

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<Ensemble> Ensembles
        {
            get
            {
                return _ensembles;
            }

            set
            {
                if (_ensembles == value)
                {
                    return;
                }

                _ensembles = value;
            }
        }

        internal List<Image> afficherEnsemble()
        {
            // Pour chaque EnsembleVetement, on extract la liste de Vetements

            //Écrire le torso
            Vetement torso = Listes.ensembleChoisi.ListeVetements[0];
            Vetement pants = Listes.ensembleChoisi.ListeVetements[1];
            Vetement shoes = Listes.ensembleChoisi.ListeVetements[2];
            //On dessine le vetement
            List<Image> listeImages = new List<Image>();
            listeImages.Add(DessinerVetement(torso, 0));
            listeImages.Add(DessinerVetement(pants, 1));
            listeImages.Add(DessinerVetement(shoes, 2));

            // Avant de commencer, on ajoute les vêtements qu'on a choisi pour qu'ils soient en premier de liste.

            listeHauts.Add(Listes.ensembleChoisi.ListeVetements[0]);
            listeBas.Add(Listes.ensembleChoisi.ListeVetements[1]);
            listeChaussures.Add(Listes.ensembleChoisi.ListeVetements[2]);
            // On prépare les listes pour les changements de vêtement (suivant, précédent)

            foreach (EnsembleVetement ensembleVetement in Listes.ListeEnsemblesVetements)
            {
                // On doit trouver un moyen de vérifier si le haut est déjà dedans la liste, il ne faut pas l'ajouter.
                bool hautExiste = false, basExiste = false, chaussureExiste = false;

                foreach (Vetement haut in listeHauts)
                    if (haut.IdVetement == ensembleVetement.ListeVetements[0].IdVetement)
                        hautExiste = true;
                if (!hautExiste)
                    listeHauts.Add(ensembleVetement.ListeVetements[0]);

                foreach (Vetement bas in listeBas)
                    if (bas.IdVetement == ensembleVetement.ListeVetements[1].IdVetement)
                        basExiste = true;
                if (!basExiste)
                    listeBas.Add(ensembleVetement.ListeVetements[1]);

                foreach (Vetement chaussure in listeChaussures)
                    if (chaussure.IdVetement == ensembleVetement.ListeVetements[2].IdVetement)
                        chaussureExiste = true;
                if (!chaussureExiste)
                    listeChaussures.Add(ensembleVetement.ListeVetements[2]);
            }
            

            return listeImages;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        /// <param name="rangee"></param>
        internal Image DessinerVetement(Vetement v, int rangee)
        {
            Image i = new Image();
            i.Width = 200;
            i.Height = 200;
            string uri;
            if (v.ImageURL.ToString().Contains("http://"))
                uri = v.ImageURL.ToString();
            else
                uri = "http://" + v.ImageURL.ToString();
            i.Source = new BitmapImage(new Uri(uri));
            Grid.SetColumn(i, 2);
            Grid.SetRow(i, rangee);

            return i;
        }

        internal Image changerVetement(int rangee, string direction)
        {
            if (direction == "Suivant")
            {
                switch (rangee)
                {
                    case 2: i++;        // On est dans la rangée des hauts
                        if (i < listeHauts.Count())
                        {
                            Listes.ensembleChoisi.ListeVetements[0] = listeHauts[i];
                            return DessinerVetement(listeHauts[i], 2);
                        }
                        else
                        {
                            i--;
                            // On disable le bouton suivant des hauts
                           break; 
                        }
                    case 3: j++;        // On est dans la rangée des bas
                        if (j < listeBas.Count())
                        {
                            Listes.ensembleChoisi.ListeVetements[1] = listeBas[j];
                            return DessinerVetement(listeBas[j], 3);
                        }
                        else
                        {
                            j--;
                            break;
                        }
                    case 4: k++;        // On est dans la rangée des chaussurse
                        if (k < listeChaussures.Count())
                        {
                            Listes.ensembleChoisi.ListeVetements[2] = listeChaussures[k];
                            return DessinerVetement(listeChaussures[k], 4);
                        }
                        else
                        {
                            k--;
                            break;
                        }
                }
            }
            else
            {
                switch (rangee)
                {
                    case 2: i--;
                        if (i != -1)
                        {
                             // On décrémente le compteur des hauts pour la liste
                            Listes.ensembleChoisi.ListeVetements[0] = listeHauts[i];
                            return DessinerVetement(listeHauts[i], 2);
                        }
                        else
                        {
                            i++;
                            break;
                        }        // On disable le bouton précédent des hauts
                    case 3: j--;
                        if (j != -1)
                        {
                            Listes.ensembleChoisi.ListeVetements[1] = listeBas[j];
                            return DessinerVetement(listeBas[j], 3);
                        }
                        else
                        {
                            j++;
                            break;
                        }
                    case 4: k--;
                        if (k != -1)
                        {
                            Listes.ensembleChoisi.ListeVetements[2] = listeChaussures[k];
                            return DessinerVetement(listeChaussures[k], 4);
                        }
                        else
                        {
                            k++;
                            break;
                        }
                }
            }

            // On retourne un image vide (null)
            Image image = new Image();
            image.Visibility = Visibility.Collapsed;
            return image;
        }

        internal int ajouterEnsemble(string nomEnsemble)
        {
            Ensemble ensembleAAjouter = new Ensemble(nomEnsemble);
            return _ensembleService.Create(ensembleAAjouter);
        }

        internal void ajouterEnsembleVetement(EnsembleVetement ensembleVetement)
        {
            _ensembleVetementService.Create(ensembleVetement);
        }

        internal int ajouterUtilisateurEnsemble(int idEnsemble, bool estFavori)
        {
            //throw new NotImplementedException();
            UtilisateurEnsemble utilisateurEnsembleAAjouter = new UtilisateurEnsemble();
            utilisateurEnsembleAAjouter.Ensemble = new Ensemble();
            utilisateurEnsembleAAjouter.Ensemble.IdEnsemble = idEnsemble;
            utilisateurEnsembleAAjouter.Utilisateur = Listes.UtilisateurConnecte;
            utilisateurEnsembleAAjouter.DateCreation = DateTime.Now;
            utilisateurEnsembleAAjouter.EstDansGardeRobe = false;
            utilisateurEnsembleAAjouter.EstFavori = estFavori;
            return _utilisateurEnsembleService.Create(utilisateurEnsembleAAjouter);
        }
    }
}
