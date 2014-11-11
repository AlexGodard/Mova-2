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
    /// Interaction logic for EnsembleView.xaml
    /// </summary>
    public partial class EnsembleView : UserControl
    {

        /// <summary>
        /// Variables
        /// </summary>
        private EnsembleVetementViewModel ViewModel { get { return (EnsembleVetementViewModel)DataContext; } }
        public static History<UserControl> _historique = new History<UserControl>();
        public static UserControl derniereFenetre = null;

        //Variables constantes pour la définition de la Grid
        private const int nbColumns = 3;
        private const int nbRows = 3;

        private int nombreDeVetementAfficheJusquaDate = 0;

        //Variable pour la browser les ensembles
        List<EnsembleVetement> listeEnsemblesTrouves = new List<EnsembleVetement>();

        public EnsembleView()
        {
            InitializeComponent();

            DataContext = new EnsembleVetementViewModel();

            _historique.Ajouter(this);

            listeEnsemblesTrouves = GetEnsemblesPourFenetre();

            AfficherBoutonsAppropries();

            PlacerEnsembles();
        }

        /// <summary>
        /// 
        /// </summary>
        private void AfficherBoutonsAppropries()
        {
            
            //On commence avec tous les boutons cachés
            btnPrecedent.Visibility = Visibility.Hidden;
            btnSuivant.Visibility = Visibility.Hidden;

            //S'il n'est pas le premier
            if (!_historique.IsFirst(this))
            {
                btnPrecedent.Visibility = Visibility.Visible;
            }

            //Si le dernier ensemble de la liste globale n'est pas affiché
            if (nombreDeVetementAfficheJusquaDate < Listes.ListeEnsemblesVetements.Count)
            {
                btnSuivant.Visibility = Visibility.Visible;
            }
        
        }

        /// <summary>
        /// Méthode qui permettra d'aller chercher les ensembles à afficher pour la fenêtre (maximum de 3 en tous les cas)
        /// On va aller les chercher dans la liste globale
        /// </summary>
        /// <returns></returns>
        private List<EnsembleVetement> GetEnsemblesPourFenetre()
        {        
        
            //On va chercher le numéro de fenêtre que l'on est (toujours le dernier car la fenêtre affichée sera toujours la dernière
            int noFenetre = _historique.GetNumberOfPage(this);
            int noPremierEnsembleAPrendre = (noFenetre) * nbColumns;
            List<EnsembleVetement> listeTemp = new List<EnsembleVetement>();


            for (int i = noPremierEnsembleAPrendre; i < Listes.ListeEnsemblesVetements.Count; i++)
            {
        
                listeTemp.Add(Listes.ListeEnsemblesVetements[i]);

                nombreDeVetementAfficheJusquaDate = i + 1;

                if (listeTemp.Count >= nbColumns){

                    nombreDeVetementAfficheJusquaDate = i+1;
                    
                    break;
                }
            }


            return listeTemp;
        
        }

        /// <summary>
        /// 
        /// </summary>
        private void PlacerEnsembles()
        {
        
            List<EnsembleVetement> listesTemp;

            // Try Catch pour vérifier des trucs
            try
            {
                listesTemp = listeEnsemblesTrouves;
            }
            catch (Exception e)
            {
                return;
            }


            try
            {
                //On écrit les ensembles trouvés
                EcrireEnsembleViaListe(listesTemp);


            }
            catch
            { 
                MessageBox.Show("Aucun ensemble n'a été trouvé");
                
                //RetournerEcranStyliste();
                
                Button but = new Button();
                but.Content = "Retourner à l'écran styliste";
                but.Width = 150;
                but.Height = 90;
                but.Click += RetournerEcranStyliste;
                Grid.SetColumn(but,DynamicGrid.ColumnDefinitions.Count -1);
                Grid.SetRow(but, 0);
                DynamicGrid.Children.Add(but);

            }
            
        
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="l"></param>
        private void EcrireEnsembleViaListe(List<EnsembleVetement> l)
        {
            
            //Variable
            int nbEnsemblesMax = nbColumns;
            List<Vetement> listeVetements;

            for (int i = 0; i < nbEnsemblesMax; i++)
            {
                
                // Pour chaque EnsembleVetement, on extract la liste de Vetements
                listeVetements = l[i].ListeVetements;
                
                // Et on ajoute l'ensemble à la liste d'ensemble qu'on a AFFICHÉ
                listeEnsemblesTrouves.Add(l[i]);

                //On dessine le vetement
                EcrireVetementViaListe(listeVetements, i + (2 + (1*i))); //On a un maximum de 3 ensembles par pages et nous avons 5 colonnes, nous voulons écrire les ensembles dans les 3 du milieu

            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="l"></param>
        private void EcrireVetementViaListe(List<Vetement> l, int colonne)
        { 
       
            //Écrire le torso
            Vetement torso = l[0];
            Vetement pants = l[1];
            Vetement shoes = l[2];

            DessinerVetement(torso, colonne, 2);
            DessinerVetement(pants, colonne, 3);
            DessinerVetement(shoes, colonne, 4);

            //On génère le bon numéro d'ensemble -> Maxime Laramee 2014-11-06
            int noEnsemble = ((_historique.GetNumberOfPage(this)*3) + (colonne/2));

            //On ajoute le bouton Choisir en bas de l'ensemble
            Button button = new Button();
            button.Content = "Choisir #" + noEnsemble.ToString();
            button.Margin = new Thickness(20);
            Grid.SetColumn(button,colonne);
            Grid.SetRow(button,5);
            // On ajoute un nom au bouton
            button.Name = "btnChoisir" + noEnsemble.ToString();
            // On ajoute l'event qui se passe lorsqu'on clique sur le bouton (choisir le vêtement)
            button.Click += new RoutedEventHandler(btnChoisir_Click);

            DynamicGrid.Children.Add(button);
        }
        /// <summary>
        /// Méthode qui change la view pour un écran styliste
        /// </summary>
        private void ChangeView()
        {

            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();

            mainVM.ChangeView<UserControl>(new PersonnalisationView());
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        private void RegisterButtonInput(object sender, int indexPage) //Alexandre Godard 2014-10-30
        {
            //On commence par obtenir l'objet
            Button bTemp = (Button)sender;


            //On veut obtenir la colonne dans laquelle le bouton a été choisi
            int numero = GetFirstIntInString(bTemp.Content.ToString());

            //Listes.ensembleChoisi = ListeslisteEnsemblesTrouves[numero-1];

            Listes.ensembleChoisi = Listes.ListeEnsemblesVetements[numero - 1];
        }

        /// <summary>
        /// Méthode appelée après l'événement click sur un bouton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnChoisir_Click(object sender, EventArgs e)
        {
            // On doit changer d'écran et afficher l'ensemble choisi pour ensuite permettre à l'utilisateur de faire des modifications
            RegisterButtonInput(sender, _historique.Compte());

            ChangeView();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        /// <param name="colonne"></param>
        /// <param name="rangee"></param>
        private void DessinerVetement(Vetement v, int colonne, int rangee)
        {
            Image i = new Image();
            i.Source = new BitmapImage(new Uri("http://" + v.ImageURL.ToString()));
            Grid.SetColumn(i, colonne);
            Grid.SetRow(i, rangee);

            DynamicGrid.Children.Add(i);
        
        }

        /// <summary>
        /// 
        /// </summary>
        private void RetournerEcranStyliste(object sender, RoutedEventArgs e)
        {
            RetournerEcranStyliste();
        }

        /// <summary>
        /// 
        /// </summary>
        private void RetournerEcranStyliste()
        {

            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();

            mainVM.ChangeView<UserControl>(new StylisteActiviteView());
        }

        /// <summary>
        /// Méthode qui change la fenêtre pour la fenêtre suivante
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSuivant_Click(object sender, RoutedEventArgs e)
        {
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();

            mainVM.ChangeView<UserControl>(new EnsembleView());
        }

        /// <summary>
        /// Méthode qui change la fenêtre pour la fenêtre précédente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();

            mainVM.ChangeView<UserControl>(_historique.Last());
        }

        private void btnEcranPrecedent_Click(object sender, RoutedEventArgs e)
        {
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();

            if (derniereFenetre != null)
            {

                mainVM.ChangeView<UserControl>(derniereFenetre);
            }
            else
            {
                mainVM.ChangeView<UserControl>(new StylisteActiviteView());
            }
            
        }

        private int GetFirstIntInString(string s)
        {

            int intARetourner = 0;
            bool isNumber = true;

            for (int i = 0; i < s.Length; i++)
            {

                isNumber = true;

                try {

                    string t = s.Substring(i);

                    intARetourner = Convert.ToInt32(t);

                }
                catch (Exception e) {
                
                    isNumber = false;
                }

                if (isNumber) {
                    return intARetourner;
                }

            
            }

            return -1;
        
        }

    }
}
