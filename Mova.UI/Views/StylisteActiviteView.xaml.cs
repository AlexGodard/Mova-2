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
using System.Windows.Shapes;
using Cstj.MvvmToolkit.Services;
using Cstj.MvvmToolkit.Services.Definitions;
using Mova.Logic.Models;
using Mova.UI.ViewModel;
using Mova.Logic;
using Mova.Logic.Models.Args;
using Mova.Logic.Services.Definitions;

namespace Mova.UI.Views
{
    /// <summary>
    /// Logique d'interaction pour StylisteView.xaml
    /// </summary>
    public partial class StylisteActiviteView : UserControl
    {

        /// <summary>
        /// Variables
        /// </summary>
        
        private StylisteActiviteViewModel ViewModel { get { return (StylisteActiviteViewModel)DataContext; } }
        
        private const int _maxStack = 2;
        private const int _maxBoutons = 4;	//Nombre maximum de boutons d'activités

        /// <summary>
        /// Constructeur
        /// </summary>
        public StylisteActiviteView()
        {
            InitializeComponent();
            try
            {
                DataContext = new StylisteActiviteViewModel();
            }
            catch (Exception)
            {
                throw;
            }


            setButtonsContentActivites();


            if (Listes.InfoStyliste == null)
            {
                Listes.InfoStyliste = new InfoStylisteArgs();
            }

        }

        /// <summary>
        /// 
        /// </summary>
        private void setButtonsContentActivites()
        {

            StringBuilder sb = new StringBuilder();
            int maxColonnes = gridPrincipale.ColumnDefinitions.Count - 1;
            int maxLignes = gridPrincipale.RowDefinitions.Count - 1;

            sb.Append("Nous sommes ").Append(Moment.GetStringTime().ToLower().ToString()).Append(",");

            //Maxime Laramee - 2014-10-02
            lblMoment.Content = sb.ToString();
            lblShabiller.Content = "S'habiller pour";

            //4 éléments maximum dans la liste
            List<Activite> listeActivites = Listes.RetourneAleatoire<Activite>(4, Listes.ListeActivites.ToList<Activite>());



            //Gabriel Piché Cloutier - 2014-10-23
            //On affiche seulement 4 boutons dans l'écran styliste.
            int col = 2;
            int rangee = 2;
            for (int i = 1; i <= _maxBoutons && i <= listeActivites.Count; i++)
            {

                //On nettoie les boutons au cas ou
                NettoyerBoutons(rangee, col);

                Button btn = new Button();
                btn.Content = listeActivites[i - 1].NomActivite.ToString();
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

            }

        }

        private void NettoyerBoutons(int row, int col) {

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
        /// Méthode qui change la view pour un écran styliste
        /// </summary>
        private void ChangeView()
        {

            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();

            mainVM.ChangeView<UserControl>(new StylisteStyleView());

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

            StylisteActiviteViewModel.SetChoix(contenu);

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
            setButtonsContentActivites();
        }

        private void btnAide_Click(object sender, RoutedEventArgs e)
        {
            var aide = new AideView();
            aide.Show();
            aide.aideBrowser.Navigate("http://420.cstj.qc.ca/gabrielpichecloutier/Styliste.pdf");
        }


    }
}
