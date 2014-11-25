﻿using System;
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
        }
    

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
                }
            }
            else
            {
                // On débloque le champ
                txtAjouterStyle.Visibility = Visibility.Visible;
                btnAjouterStyle.Content = "Ajouter le nouveau style";
                // On bloque les autres boutons
                btnModifierStyle.IsEnabled = false;
                btnSupprimerStyle.IsEnabled = false;
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
            }
            else
            {
                // On débloque le champ
                if (lstStyles.SelectedIndex != -1)
                {
                    txtModifierStyle.Visibility = Visibility.Visible;
                    btnModifierStyle.Content = "Enregistrer la modification";
                    txtModifierStyle.Text = lstStyles.SelectedItem.ToString();
                    btnAjouterStyle.IsEnabled = false;
                    btnSupprimerStyle.IsEnabled = false;
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

        private void btnAjouterActivite_Click(object sender, RoutedEventArgs e)
        {
            if (txtNomActivite.Text != "" && !estModifie)
            {
                ViewModel.ajouterActivite(txtNomActivite.Text);
                lstActivites.Items.Clear();
                construireListe("Activite");
                txtNomActivite.Text = string.Empty;
                btnAjouterActivite.Content = "Ajouter";
                estModifie = false;
            }

            //Si on a cliqué sur le bouton modifier, on exécute ceci.
            if (estModifie)
            {
                ViewModel.modifierActivite(lstActivites.SelectedItem.ToString(), txtNomActivite.Text);
                lstActivites.Items.Clear();
                construireListe("Activite");
                txtNomActivite.Text = string.Empty;
                btnAjouterActivite.Content = "Ajouter";
                estModifie = false;
            }

            btnModifierActivite.IsEnabled = true;
            btnSupprimerActivite.IsEnabled = true;
            lstActivites.IsEnabled = true;
            lblOu.Content = "-OU-";

            // Si le champ est déjà visible, on vérifie s'il a entré quelque chose
            // On débloque le champ
            //if (txtAjouterActivite.Visibility == Visibility.Visible)
            //{
            //    if (txtAjouterActivite.Text != "")
            //    {
            //        ViewModel.ajouterActivite(txtAjouterActivite.Text);
            //        lstActivites.Items.Clear();
            //        construireListe("Activite");
            //        txtAjouterActivite.Text = "";
            //        txtAjouterActivite.Visibility = Visibility.Hidden;
            //        btnAjouterActivite.Content = "Ajouter";
            //        btnModifierActivite.IsEnabled = true;
            //        btnSupprimerActivite.IsEnabled = true;
            //    }
            //}
            //else
            //{
            //    // On débloque le champ
            //    txtAjouterActivite.Visibility = Visibility.Visible;
            //    btnAjouterActivite.Content = "Ajouter la nouvelle activité";
            //    btnModifierActivite.IsEnabled = false;
            //    btnSupprimerActivite.IsEnabled = false;
            //}
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
            }
            else
            {
                // On débloque le champ
                if (lstCouleurs.SelectedIndex != -1)
                {
                    txtModifierCouleur.Visibility = Visibility.Visible;
                    btnModifierCouleur.Content = "Enregistrer la modification";
                    txtModifierCouleur.Text = lstCouleurs.SelectedItem.ToString();
                    btnAjouterCouleur.IsEnabled = false;
                    btnSupprimerCouleur.IsEnabled = false;
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
                }
            }
            else
            {
                // On débloque le champ
                txtAjouterTemperature.Visibility = Visibility.Visible;
                btnAjouterTemperature.Content = "Ajouter la nouvelle température";
                btnModifierTemperature.IsEnabled = false;
                btnSupprimerTemperature.IsEnabled = false;
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
            }
            else
            {
                // On débloque le champ
                if (lstTemperatures.SelectedIndex != -1)
                {
                    txtModifierTemperature.Visibility = Visibility.Visible;
                    btnModifierTemperature.Content = "Enregistrer la modification";
                    txtModifierTemperature.Text = lstTemperatures.SelectedItem.ToString();
                    btnAjouterTemperature.IsEnabled = false;
                    btnSupprimerTemperature.IsEnabled = false;
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
                }
            }
            else
            {
                 // On débloque le champ
                 txtAjouterCouleur.Visibility = Visibility.Visible;
                 btnAjouterCouleur.Content = "Ajouter la nouvelle couleur";
                 btnModifierCouleur.IsEnabled = false;
                 btnSupprimerCouleur.IsEnabled = false;
            }
        }

        private void btnModifierActivite_Click(object sender, RoutedEventArgs e)
        {
            // On débloque le champ
            if (lstActivites.SelectedIndex != -1)
            {
                btnAjouterActivite.Content = "Enregistrer";
                txtNomActivite.Text = lstActivites.SelectedItem.ToString();
                estModifie = true;
                btnModifierActivite.IsEnabled = false;
                btnSupprimerActivite.IsEnabled = false;
                lstActivites.IsEnabled = false;
                lblOu.Content = "-ET-";
            }

            //if (txtModifierActivite.Visibility == Visibility.Visible)
            //{

            //    ViewModel.modifierActivite(lstActivites.SelectedItem.ToString(), txtModifierActivite.Text);
            //    lstActivites.Items.Clear();
            //    construireListe("Activite");
            //    txtModifierActivite.Text = "";
            //    txtModifierActivite.Visibility = Visibility.Hidden;
            //    btnModifierActivite.Content = "Modifier";
            //    btnAjouterActivite.IsEnabled = true;
            //    btnSupprimerActivite.IsEnabled = true;
            //}
            //else
            //{
            //    // On débloque le champ
            //    if (lstActivites.SelectedIndex != -1)
            //    {
            //        txtModifierActivite.Visibility = Visibility.Visible;
            //        btnModifierActivite.Content = "Enregistrer la modification";
            //        txtModifierActivite.Text = lstActivites.SelectedItem.ToString();
            //        btnAjouterActivite.IsEnabled = false;
            //        btnSupprimerActivite.IsEnabled = false;
            //    }
            //    //TODO: Message d'erreur
            //}
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

        private void btnAjouterVetement_Click(object sender, RoutedEventArgs e)
        {
            // On switch la view vers la page de l'ajout de vêtement
       
            IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();

            mainVM.ChangeView<UserControl>(new AdminView());
        }

        private void btnAnnulerActivite_Click(object sender, RoutedEventArgs e)
        {
            txtNomActivite.Text = string.Empty;
            btnModifierActivite.IsEnabled = true;
            btnSupprimerActivite.IsEnabled = true;
            lstActivites.IsEnabled = true;
        }

        private void txtNomActivite_GotFocus(object sender, RoutedEventArgs e)
        {
            btnModifierActivite.IsEnabled = false;
            btnSupprimerActivite.IsEnabled = false;
            lstActivites.IsEnabled = false;
        }
    }
}
