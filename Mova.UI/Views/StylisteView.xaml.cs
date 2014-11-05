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
    public partial class StylisteView : UserControl
    {

        /// <summary>
        /// Variables
        /// </summary>
        private StylisteViewModel ViewModel { get { return (StylisteViewModel)DataContext; } }
        public static History<UserControl> _historique = new History<UserControl>();
        private const int _maxStack = 2;
		private const int _maxBoutons = 4;	//Nombre maximum de boutons d'activités

        /// <summary>
        /// Constructeur
        /// </summary>
        public StylisteView()
        {
            InitializeComponent();
			try
			{
				DataContext = new StylisteViewModel();
			}
			catch (Exception)
			{
				throw;
			}


            // Historique
            _historique.Ajouter(this);

            // On crée un nouvel InfoStylisteArgs dans la liste globale
            if (_historique.Compte() <= 1)
            {
                Listes.InfoStyliste = new InfoStylisteArgs();
            }


			//Gabriel Piché Cloutier - 2014/1-24
			//Mais qu'est-ce???
            //Maxime Laramee - 2014-10-26
            //Ca change l'information des boutons dépendement de la UserControl dans laquelle on est
            //Dépendement du contexte on change le contenu
			switch (_historique.Compte())
			{
				case 1:
					setButtonsContentActivites();
					break;
				default:
					setButtonsContentStyles();
					break;            
			}
			//setButtonsContentActivites();

            //Si on a besoin d'ajouter un bouton Back
            if (_historique.Compte() > 1)
            {
                btnBack.Visibility = Visibility.Visible;
                //createBackButton();
            }

            //gridPrincipale.ShowGridLines = true; //Pour test seulement
        }

        /// <summary>
        /// 
        /// </summary>
        private void setButtonsContentActivites()
        {

            StringBuilder sb = new StringBuilder();
            int maxColonnes = gridPrincipale.ColumnDefinitions.Count - 1;
            int maxLignes = gridPrincipale.RowDefinitions.Count - 1 ;

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
			for (int i = 1; i <= _maxBoutons; i++)
            {
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

                //listeActivites.RemoveAt(i-1);
            }
            
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

                if(iCompteur == _maxBoutons)
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

            //Si il y a des éléments dans la liste
            if (!_historique.IsEmpty())
            {
                UserControl uc = _historique.Last(); //On va chercher le dernier UserControl

                if (uc != null)
                {
                    IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();
                    mainVM.ChangeView<UserControl>(uc);
                }
            }
        }

        /// <summary>
        /// Méthode qui change la view pour un écran styliste
        /// </summary>
        private void ChangeView()
        {

            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();

            //On permet un nombre maximum d'instance de cette classe (fenêtre)
            if (_historique.Compte() >= _maxStack)
            {

                //Avant de changer de fenêtre on place un ID utile dans les args InfoStyliste
                Listes.InfoStyliste.IdMoment = Moment.GetIDMomentNow();

                //_historique.DeleteLast();
                mainVM.ChangeView<UserControl>(new EnsembleView());

                return;
            }
                
            mainVM.ChangeView<UserControl>(new StylisteView());

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        private void registerButtonInput(object sender, int indexPage) //Maxime Laramee - 2014-10-14
        {
            //On commence par obtenir l'objet
            Button bTemp = (Button)sender;

            //On obtient son contenu
            string contenu = bTemp.Content.ToString();

            ViewModel.SetChoix(indexPage, contenu);

        }

        /// <summary>
        /// Méthode appelée après l'événement click sur un bouton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Click(object sender, RoutedEventArgs e)
        {
            registerButtonInput(sender, _historique.Compte());

            ChangeView();
        }


    }
}
