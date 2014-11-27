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
using Cstj.MvvmToolkit.Services.Definitions;
using Cstj.MvvmToolkit.Services;
using Mova.Logic.Models;
using Mova.Logic.Services.Definitions;
using Mova.Logic.Models.Entities;
using Mova.UI.ViewModel;
using Mova.Logic;
using System.Text.RegularExpressions;
using System.Net;
using Mova.Logic.Models.Args;

namespace Mova.UI.Views
{
    /// <summary>
    /// Logique d'interaction pour MenuAdminView.xaml
    /// </summary>
    public partial class MenuAdminView : UserControl
    {
        private MenuAdminViewModel ViewModel { get { return (MenuAdminViewModel)DataContext; } }
        private History<UserControl> _historique = new History<UserControl>();
        private bool estModifie = false;

        public MenuAdminView()
        {
            InitializeComponent();
            DataContext = new MenuAdminViewModel();
            _historique.Ajouter(this); 

            

            // Construction des listbox (activités, styles, températures)
            construireListe("Couleur");
            construireListe("Temperature");
            construireListe("Activite");
            construireListe("Style");
        }

        public void construireListe(string nomListe)
        {
            if (nomListe == "Temperature")
            {
                List<Temperature> listeTemperatures = Listes.ListeTemperatures.ToList<Temperature>();

                for (int i = 0; i < listeTemperatures.Count; i++)
                {
                    lstTemperatures.Items.Add(listeTemperatures[i].NomClimat);
                }
            }

            if (nomListe == "Activite")
            {
                List<Activite> listeActivites = Listes.ListeActivites.ToList<Activite>();

                for (int i = 0; i < listeActivites.Count; i++)
                {
                    lstActivites.Items.Add(listeActivites[i].NomActivite);
                }
            }

            if (nomListe == "Style")
            {
                List<StyleVetement> listeStyles = Listes.ListeStyles.ToList<StyleVetement>();

                for (int i = 0; i < listeStyles.Count; i++)
                {
                    lstStyles.Items.Add(listeStyles[i].NomStyle);
                }
            }

            if (nomListe == "Couleur")
            {
                List<Couleur> listeCouleurs = Listes.ListeCouleurs.ToList<Couleur>();

                for (int i = 0; i < listeCouleurs.Count; i++)
                {
                    lstCouleurs.Items.Add(listeCouleurs[i].NomCouleur);
                }
            }

            if (nomListe == "Moment")
            {
                List<Moment> listeMoments = Listes.ListeMoments.ToList<Moment>();

                for (int i = 0; i < listeMoments.Count; i++)
                {
                    lstMoments.Items.Add(listeMoments[i].NomMoment);
                }
            }
        }
    

        #region Styles

        private void btnAjouterStyle_Click(object sender, RoutedEventArgs e)
        {
            // Si le champ est déjà visible, on vérifie s'il a entré quelque chose
            // On débloque le champ
            if (txtAjouterStyle.Visibility == Visibility.Visible)
            {
                if (txtAjouterStyle.Text != "")
                {
                    ViewModel.ajouterStyle(txtAjouterStyle.Text);
                    lstStyles.Items.Clear();
                    construireListe("Style");
                    txtAjouterStyle.Text = "";
                    txtAjouterStyle.Visibility = Visibility.Hidden;
                    btnAjouterStyle.Content = "Ajouter";
                    btnModifierStyle.IsEnabled = true;
                    btnSupprimerStyle.IsEnabled = true;
                    lstStyles.IsEnabled = true;
                    btnAnnulerAjoutStyle.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                // On débloque le champ
                txtAjouterStyle.Visibility = Visibility.Visible;
                btnAjouterStyle.Content = "Ajouter le style";
                // On bloque les autres boutons
                btnModifierStyle.IsEnabled = false;
                btnSupprimerStyle.IsEnabled = false;
                lstStyles.IsEnabled = false;
                btnAnnulerAjoutStyle.Visibility = Visibility.Visible;
            }
        }

        private void btnModifierStyle_Click(object sender, RoutedEventArgs e)
        {
            if (txtModifierStyle.Visibility == Visibility.Visible)
            {

                ViewModel.modifierStyle(lstStyles.SelectedItem.ToString(), txtModifierStyle.Text);
                lstStyles.Items.Clear();
                construireListe("Style");
                txtModifierStyle.Text = "";
                txtModifierStyle.Visibility = Visibility.Hidden;
                btnModifierStyle.Content = "Modifier";
                btnAjouterStyle.IsEnabled = true;
                btnSupprimerStyle.IsEnabled = true;
                lstStyles.IsEnabled = true;
                btnAnnulerModifStyle.Visibility = Visibility.Hidden;
            }
            else
            {
                // On débloque le champ
                if (lstStyles.SelectedIndex != -1)
                {
                    txtModifierStyle.Visibility = Visibility.Visible;
                    btnModifierStyle.Content = "Enregistrer";
                    txtModifierStyle.Text = lstStyles.SelectedItem.ToString();
                    btnAjouterStyle.IsEnabled = false;
                    btnSupprimerStyle.IsEnabled = false;
                    lstStyles.IsEnabled = false;
                    btnAnnulerModifStyle.Visibility = Visibility.Visible;
                }
                //TODO: Message d'erreur
            }
        }

        private void btnSupprimerStyle_Click(object sender, RoutedEventArgs e)
        {
            if (lstStyles.SelectedIndex != -1)
            {
                ViewModel.supprimerStyle(lstStyles.SelectedItem.ToString());
                lstStyles.Items.Clear();
                construireListe("Style");
            }
            //TODO: Message d'erreur
        }

        private void btnAnnulerAjoutStyle_Click(object sender, RoutedEventArgs e)
        {
            txtAjouterStyle.Text = string.Empty;
            txtAjouterStyle.Visibility = Visibility.Hidden;
            btnAnnulerAjoutStyle.Visibility = Visibility.Hidden;
            btnModifierStyle.IsEnabled = true;
            btnSupprimerStyle.IsEnabled = true;
            lstStyles.IsEnabled = true;
            btnAjouterStyle.Content = "Ajouter";
        }

        private void btnAnnulerModifStyle_Click(object sender, RoutedEventArgs e)
        {
            txtModifierStyle.Text = string.Empty;
            txtModifierStyle.Visibility = Visibility.Hidden;
            btnAnnulerModifStyle.Visibility = Visibility.Hidden;
            btnAjouterStyle.IsEnabled = true;
            btnSupprimerStyle.IsEnabled = true;
            lstStyles.IsEnabled = true;
            btnModifierStyle.Content = "Modifier";
        }

        #endregion

        #region Activités

        private void btnAjouterActivite_Click(object sender, RoutedEventArgs e)
        {
            //On désactive les champs.
            btnAjouterActivite.IsEnabled = false;
            btnModifierActivite.IsEnabled = false;
            btnSupprimerActivite.IsEnabled = false;
            chkConge.IsChecked = false;
            chkOuvrable.IsChecked = false;
            lstActivites.IsEnabled = false;
            construireListe("Moment");
            //On affiche les champs pour ajouter une activité.
            afficherChampsActivite();
            btnEnregistrerActivite.Content = "Ajouter";
        }

        private void btnSupprimerActivite_Click(object sender, RoutedEventArgs e)
        {
            if (lstActivites.SelectedIndex != -1)
            {
                ViewModel.supprimerActivite(lstActivites.SelectedItem.ToString());
                lstActivites.Items.Clear();
                construireListe("Activite");
            }
            //TODO: Message d'erreur
        }

        private void btnEnregistrerActivite_Click(object sender, RoutedEventArgs e)
        {
            //Si on a cliqué sur le bouton modifier, on exécute ceci.
            if (estModifie)
            {
                // On doit obtenir la liste des moments sélectionnés
                List<Moment> listeMomentsSelectionnes = new List<Moment>();
                // Insertion des items de la listbox dans la liste.
                foreach (string nomMoment in lstMoments.SelectedItems)
                {
                    Moment moment = new Moment(nomMoment);
                    listeMomentsSelectionnes.Add(moment);
                }
                // On doit aller chercher les moments liés à l'activité
                ViewModel.modifierActivite(lstActivites.SelectedItem.ToString(), txtNomActivite.Text, (bool)chkOuvrable.IsChecked, (bool)chkConge.IsChecked, listeMomentsSelectionnes);
            }
            else
            {
                // On doit obtenir la liste des moments sélectionnés
                List<Moment> listeMomentsSelectionnes = new List<Moment>();
                // Insertion des items de la listbox dans la liste.
                foreach (string nomMoment in lstMoments.SelectedItems)
                {
                    Moment moment = new Moment(nomMoment);
                    listeMomentsSelectionnes.Add(moment);
                }

                ViewModel.ajouterActivite(txtNomActivite.Text, (bool)chkOuvrable.IsChecked, (bool)chkOuvrable.IsChecked, listeMomentsSelectionnes);
            }

            //On réinitialise les champs.
            lstMoments.Items.Clear();
            lstActivites.Items.Clear();
            construireListe("Activite");
            txtNomActivite.Text = string.Empty;
            estModifie = false;

            btnAjouterActivite.IsEnabled = true;
            btnModifierActivite.IsEnabled = true;
            btnSupprimerActivite.IsEnabled = true;
            lstActivites.IsEnabled = true;

            cacherChampsActivite();
        }

        private void btnAnnulerActivite_Click(object sender, RoutedEventArgs e)
        {
            //On réinitialise les champs.
            txtNomActivite.Text = string.Empty;
            btnAjouterActivite.IsEnabled = true;
            btnModifierActivite.IsEnabled = true;
            btnSupprimerActivite.IsEnabled = true;
            lstActivites.IsEnabled = true;
            lstMoments.Items.Clear();
            btnAjouterActivite.Content = "Ajouter";
            //On cache les champs.
            cacherChampsActivite();
        }

        private void afficherChampsActivite()
        {
            lblNomActivite.Visibility = Visibility.Visible;
            txtNomActivite.Visibility = Visibility.Visible;
            chkOuvrable.Visibility = Visibility.Visible;
            chkConge.Visibility = Visibility.Visible;
            lblNomMoment.Visibility = Visibility.Visible;
            lstMoments.Visibility = Visibility.Visible;
            btnEnregistrerActivite.Visibility = Visibility.Visible;
            btnAnnulerActivite.Visibility = Visibility.Visible;
        }

        private void cacherChampsActivite()
        {
            lblNomActivite.Visibility = Visibility.Hidden;
            txtNomActivite.Visibility = Visibility.Hidden;
            chkOuvrable.Visibility = Visibility.Hidden;
            chkConge.Visibility = Visibility.Hidden;
            lblNomMoment.Visibility = Visibility.Hidden;
            lstMoments.Visibility = Visibility.Hidden;
            btnEnregistrerActivite.Visibility = Visibility.Hidden;
            btnAnnulerActivite.Visibility = Visibility.Hidden;
        }


        #endregion

        #region Couleurs

        private void btnAjouterCouleur_Click(object sender, RoutedEventArgs e)
        {
            // Si le champ est déjà visible, on vérifie s'il a entré quelque chose
            // On débloque le champ
            if (txtAjouterCouleur.Visibility == Visibility.Visible)
            {
                if (txtAjouterCouleur.Text != "")
                {
                    ViewModel.ajouterCouleur(txtAjouterCouleur.Text);
                    lstCouleurs.Items.Clear();
                    construireListe("Couleur");
                    txtAjouterCouleur.Text = "";
                    txtAjouterCouleur.Visibility = Visibility.Hidden;
                    btnAjouterCouleur.Content = "Ajouter";
                    btnModifierCouleur.IsEnabled = true;
                    btnSupprimerCouleur.IsEnabled = true;
                    btnAnnulerAjoutCouleur.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                // On débloque le champ
                txtAjouterCouleur.Visibility = Visibility.Visible;
                btnAjouterCouleur.Content = "Ajouter la couleur";
                btnModifierCouleur.IsEnabled = false;
                btnSupprimerCouleur.IsEnabled = false;
                btnAnnulerAjoutCouleur.Visibility = Visibility.Visible;
            }
        }

        private void btnModifierCouleur_Click(object sender, RoutedEventArgs e)
        {
            if (txtModifierCouleur.Visibility == Visibility.Visible)
            {

                ViewModel.modifierCouleur(lstCouleurs.SelectedItem.ToString(), txtModifierCouleur.Text);
                lstCouleurs.Items.Clear();
                construireListe("Couleur");
                txtModifierCouleur.Text = "";
                txtModifierCouleur.Visibility = Visibility.Hidden;
                btnModifierCouleur.Content = "Modifier";
                btnAjouterCouleur.IsEnabled = true;
                btnSupprimerCouleur.IsEnabled = true;
                btnAnnulerModifCouleur.Visibility = Visibility.Hidden;
            }
            else
            {
                // On débloque le champ
                if (lstCouleurs.SelectedIndex != -1)
                {
                    txtModifierCouleur.Visibility = Visibility.Visible;
                    btnModifierCouleur.Content = "Enregistrer";
                    txtModifierCouleur.Text = lstCouleurs.SelectedItem.ToString();
                    btnAjouterCouleur.IsEnabled = false;
                    btnSupprimerCouleur.IsEnabled = false;

                    btnAnnulerModifCouleur.Visibility = Visibility.Visible;
                }
                //TODO: Message d'erreur
            }
        }

        private void btnSupprimerCouleur_Click(object sender, RoutedEventArgs e)
        {
            if (lstCouleurs.SelectedIndex != -1)
            {
                ViewModel.supprimerCouleur(lstCouleurs.SelectedItem.ToString());
                lstCouleurs.Items.Clear();
                construireListe("Couleur");
            }
            //TODO: Message d'erreur
        }

        private void btnAnnulerAjoutCouleur_Click(object sender, RoutedEventArgs e)
        {
            txtAjouterCouleur.Text = string.Empty;
            txtAjouterCouleur.Visibility = Visibility.Hidden;
            btnAnnulerAjoutCouleur.Visibility = Visibility.Hidden;
            btnModifierCouleur.IsEnabled = true;
            btnSupprimerCouleur.IsEnabled = true;
            lstCouleurs.IsEnabled = true;
            btnAjouterCouleur.Content = "Ajouter";
        }

        private void btnAnnulerModifCouleur_Click(object sender, RoutedEventArgs e)
        {
            txtModifierCouleur.Text = string.Empty;
            txtModifierCouleur.Visibility = Visibility.Hidden;
            btnAnnulerModifCouleur.Visibility = Visibility.Hidden;
            btnAjouterCouleur.IsEnabled = true;
            btnSupprimerCouleur.IsEnabled = true;
            lstCouleurs.IsEnabled = true;
            btnModifierCouleur.Content = "Modifier";
        }
    
        #endregion

        #region Températures

        private void btnAjouterTemperature_Click(object sender, RoutedEventArgs e)
        {
            // Si le champ est déjà visible, on vérifie s'il a entré quelque chose
            // On débloque le champ
            if (txtAjouterTemperature.Visibility == Visibility.Visible)
            {
                if (txtAjouterTemperature.Text != "")
                {
                    ViewModel.ajouterTemperature(txtAjouterTemperature.Text);
                    lstTemperatures.Items.Clear();
                    construireListe("Temperature");
                    txtAjouterTemperature.Text = "";
                    txtAjouterTemperature.Visibility = Visibility.Hidden;
                    btnAjouterTemperature.Content = "Ajouter";
                    btnModifierTemperature.IsEnabled = true;
                    btnSupprimerTemperature.IsEnabled = true;
                    btnAnnulerAjoutTemperature.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                // On débloque le champ
                txtAjouterTemperature.Visibility = Visibility.Visible;
                btnAjouterTemperature.Content = "Ajouter la température";
                btnModifierTemperature.IsEnabled = false;
                btnSupprimerTemperature.IsEnabled = false;
                btnAnnulerAjoutTemperature.Visibility = Visibility.Visible;

            }
        }
    
        private void btnModifierTemperature_Click(object sender, RoutedEventArgs e)
        {
            if (txtModifierTemperature.Visibility == Visibility.Visible)
            {

                ViewModel.modifierTemperature(lstTemperatures.SelectedItem.ToString(), txtModifierTemperature.Text);
                lstTemperatures.Items.Clear();
                construireListe("Temperature");
                txtModifierTemperature.Text = "";
                txtModifierTemperature.Visibility = Visibility.Hidden;
                btnModifierTemperature.Content = "Modifier";
                btnAjouterTemperature.IsEnabled = true;
                btnSupprimerTemperature.IsEnabled = true;
                btnAnnulerModifTemperature.Visibility = Visibility.Hidden;
            }
            else
            {
                // On débloque le champ
                if (lstTemperatures.SelectedIndex != -1)
                {
                    txtModifierTemperature.Visibility = Visibility.Visible;
                    btnModifierTemperature.Content = "Enregistrer";
                    txtModifierTemperature.Text = lstTemperatures.SelectedItem.ToString();
                    btnAjouterTemperature.IsEnabled = false;
                    btnSupprimerTemperature.IsEnabled = false;
                    btnAnnulerModifTemperature.Visibility = Visibility.Visible;
                }
                //TODO: Message d'erreur
            }
        }

        private void btnSupprimerTemperature_Click(object sender, RoutedEventArgs e)
        {
            if (lstTemperatures.SelectedIndex != -1)
            {
                ViewModel.supprimerTemperature(lstTemperatures.SelectedItem.ToString());
                lstTemperatures.Items.Clear();
                construireListe("Temperature");
            }
            //TODO: Message d'erreur
        }

        private void btnAnnulerAjoutTemperature_Click(object sender, RoutedEventArgs e)
        {
            txtAjouterTemperature.Text = string.Empty;
            txtAjouterTemperature.Visibility = Visibility.Hidden;
            btnAnnulerAjoutTemperature.Visibility = Visibility.Hidden;
            btnModifierTemperature.IsEnabled = true;
            btnSupprimerTemperature.IsEnabled = true;
            lstTemperatures.IsEnabled = true;
            btnAjouterTemperature.Content = "Ajouter";
        }

        private void btnAnnulerModifTemperature_Click(object sender, RoutedEventArgs e)
        {
            txtModifierTemperature.Text = string.Empty;
            txtModifierTemperature.Visibility = Visibility.Hidden;
            btnAnnulerModifTemperature.Visibility = Visibility.Hidden;
            btnAjouterTemperature.IsEnabled = true;
            btnSupprimerTemperature.IsEnabled = true;
            lstTemperatures.IsEnabled = true;
            btnModifierTemperature.Content = "Modifier";
        }

        #endregion


        private void btnModifierActivite_Click(object sender, RoutedEventArgs e)
        {
            // On débloque le champ
            if (lstActivites.SelectedIndex != -1)
            {
                txtNomActivite.Text = lstActivites.SelectedItem.ToString();
                btnEnregistrerActivite.Content = "Enregistrer";
                // On selectionne les bons moments.


                List<Moment> listeMomentsASelectionner = new List<Moment>();
                listeMomentsASelectionner = (List<Moment>)chargerMomentsPourActivite(txtNomActivite.Text);

                construireListe("Moment");

                // On selectionne les bons moments.
                foreach(Moment moment in listeMomentsASelectionner)
                {
                    foreach(string item in lstMoments.Items)
                    {
                        if (moment.NomMoment == item)
                        {
                            lstMoments.SelectedItems.Add(moment.NomMoment);
                            break;
                        }
                    }
                }
                
                // On check les bonnes checkbox
                
                // On va chercher les informations de l'activité en question
                Activite a = new Activite();
                a = chargerDetailsActivite(txtNomActivite.Text);
                if (a.EstConge)
                    chkConge.IsChecked = true;
                if (a.EstOuvrable)
                    chkOuvrable.IsChecked = true;

                afficherChampsActivite();

                estModifie = true;
                btnModifierActivite.IsEnabled = false;
                btnSupprimerActivite.IsEnabled = false;
                lstActivites.IsEnabled = false;
            }
        }

        private Activite chargerDetailsActivite(string nomActivite)
        {
            return ViewModel.chargerDetailsActivite(nomActivite);
        }

        

        private void btnAjouterVetement_Click(object sender, RoutedEventArgs e)
        {
            // On switch la view vers la page de l'ajout de vêtement
       
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();

            mainVM.ChangeView<UserControl>(new AdminView());
        }

        private void txtNomActivite_GotFocus(object sender, RoutedEventArgs e)
        {
            btnModifierActivite.IsEnabled = false;
            btnSupprimerActivite.IsEnabled = false;
            lstActivites.IsEnabled = false;
            construireListe("Moment");

        }

        private IList<Moment> chargerMomentsPourActivite(string nomActivite)
        {
            // On crée la liste de moments
            return ViewModel.chargerMomentsPourActivite(nomActivite);
        }
    }
}
