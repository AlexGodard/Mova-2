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
using Cstj.MvvmToolkit.Services;
using Cstj.MvvmToolkit.Services.Definitions;
using Mova.Logic;
using Mova.Logic.Models;
using Mova.Logic.Models.Args;
using Mova.Logic.Services.Definitions;
using Mova.UI.ViewModel;

namespace Mova.UI.Views
{
    /// <summary>
    /// Logique d'interaction pour StylisteViewStyle.xaml
    /// </summary>
    public partial class StylisteStyleView : UserControl
    {
        /// <summary>
        /// Variables
        /// </summary>
        private StylisteStyleViewModel ViewModel { get { return (StylisteStyleViewModel)DataContext; } }
        private const int _maxBoutons = 4;	//Nombre maximum de boutons d'activités

        /// <summary>
        /// Constructeur
        /// </summary>
        public StylisteStyleView()
        {
            InitializeComponent();
            try
            {
                DataContext = new StylisteStyleViewModel();
            }
            catch (Exception)
            {
                throw;
            }

            setButtonsContentStyles();


        }

        /// <summary>
        /// 
        /// </summary>
        private void setButtonsContentStyles()
        {

            int maxColonnes = gridPrincipale.ColumnDefinitions.Count - 1;
            int maxLignes = gridPrincipale.RowDefinitions.Count - 1;

            //Maxime Laramee - 2014-10-02
            lblMoment.Content = "Choisi un style";

            //On place de l'informations supplémentaires dans le "bundle" InfoStylise (moment et jour)
            Listes.InfoStyliste.IdMoment = Moment.GetIDMomentNow();
            

            //Éléments de la liste
            List<StyleVetement> listeStyles = Listes.RetourneAleatoire<StyleVetement>(4, ServiceFactory.Instance.GetService<IStyleService>().RetrieveSpecific(Listes.InfoStyliste).ToList<StyleVetement>());

            //Gabriel Piché Cloutier - 2014-10-23
            //On affiche seulement 4 boutons dans l'écran styliste.
            int col = 2;
            int rangee = 2;
            int iCompteur = 0;
            var rnd = new Random();
            var result = listeStyles.OrderBy(item => rnd.Next());
            foreach (StyleVetement s in result) //(int i = 1; i <= _maxBoutons && listeStyles.Count > i; i++)
            {

                //On nettoie les emplacements qui contiennent les boutons au cas où il y en aurait déjà
                NettoyerBoutons(rangee, col);

                Button btn = new Button();
                iCompteur++;
                btn.Content = s.NomStyle.ToString();
                btn.Click += btn_Click;
                btn.Style = (Style)FindResource("btnActivite");
                gridPrincipale.Children.Add(btn);
                Grid.SetColumn(btn, col);
                Grid.SetRow(btn, rangee);

                if (col < (_maxBoutons + 2) / 2)
                    col++;
                else
                {
                    rangee++;
                    col = 2;
                }

                if (iCompteur == _maxBoutons)
                {
                    return;
                }

            }


        }

        private void NettoyerBoutons(int row, int col)
        {

            foreach (UIElement control in gridPrincipale.Children)
            {
                if (Grid.GetRow(control) == row && Grid.GetColumn(control) == col)
                {

                    gridPrincipale.Children.Remove(control);
                    break;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {

            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<UserControl>(new StylisteActiviteView());

        }

        /// <summary>
        /// Méthode qui change la view pour un écran styliste
        /// </summary>
        private void ChangeView()
        {

            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();

            //Avant de changer de fenêtre on place un ID utile dans les args InfoStyliste
            Listes.InfoStyliste.IdMoment = Moment.GetIDMomentNow();

            //On reset la liste
            EnsembleView._historique.Reset();

            //On signale qu'on est le delegate du EnsembleView
            EnsembleView.derniereFenetre = this;

            //Si on trouve des ensembles, on peut passer à la fenêtre suivante
            if (ViewModel.ensemblesFound())
            {
                mainVM.ChangeView<UserControl>(new EnsembleView());
            
            }
            //Sinon on revient à la page précédente pour offrir d'autres choix à l'utilisateur
            else {
                MessageBox.Show("Aucun ensemble n'a été trouvé");
                mainVM.ChangeView<UserControl>(new StylisteActiviteView());
            }
            

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        private void registerButtonInput(object sender) //Maxime Laramee - 2014-10-14
        {
            //On commence par obtenir l'objet
            Button bTemp = (Button)sender;

            //On obtient son contenu
            string contenu = bTemp.Content.ToString();

            StylisteStyleViewModel.SetChoix(contenu);

        }

        /// <summary>
        /// Méthode appelée après l'événement click sur un bouton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Click(object sender, RoutedEventArgs e)
        {
            registerButtonInput(sender);

            ChangeView();
        }

        private void btnRandom_Click(object sender, RoutedEventArgs e)
        {
            setButtonsContentStyles();
        }

        private void btnAide_Click(object sender, RoutedEventArgs e)
        {
            var aide = new AideView();
            aide.Show();
            aide.aideBrowser.Navigate("http://420.cstj.qc.ca/gabrielpichecloutier/Styliste.pdf");
        }
    }
}
