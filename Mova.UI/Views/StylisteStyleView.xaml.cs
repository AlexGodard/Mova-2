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
    public partial class StylisteViewStyle : UserControl
    {
        /// <summary>
        /// Variables
        /// </summary>
        private StylisteStyleViewModel ViewModel { get { return (StylisteStyleViewModel)DataContext; } }
        private const int _maxBoutons = 4;	//Nombre maximum de boutons d'activités

        /// <summary>
        /// Constructeur
        /// </summary>
        public StylisteViewStyle()
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {

            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
            mainVM.ChangeView<UserControl>(new StylisteViewStyle());

        }

        /// <summary>
        /// Méthode qui change la view pour un écran styliste
        /// </summary>
        private void ChangeView()
        {

            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();

            //Avant de changer de fenêtre on place un ID utile dans les args InfoStyliste
            Listes.InfoStyliste.IdMoment = Moment.GetIDMomentNow();

            mainVM.ChangeView<UserControl>(new EnsembleView());



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

            ViewModel.SetChoix(contenu);

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
    }
}
