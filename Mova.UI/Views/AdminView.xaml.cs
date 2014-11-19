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
using Microsoft.Win32;

namespace Mova.UI.Views
{
    /// <summary>
    /// Interaction logic for AdminView.xaml
    /// </summary>
    public partial class AdminView : UserControl
    {
        private AdminViewModel ViewModel { get { return (AdminViewModel)DataContext; } }
        private History<UserControl> _historique = new History<UserControl>();

        public AdminView()
        {
            InitializeComponent();
            DataContext = new AdminViewModel();
            _historique.Ajouter(this); 

            

            // Construction des listbox (activités, styles, températures)
            construireListe("Couleur");
            construireListe("Temperature");
            construireListe("Activite");
            construireListe("Style");

            


            // Construction du comboBox Types et Couleurs

            cboTypes.Items.Add("Haut");
            cboTypes.Items.Add("Bas");
            cboTypes.Items.Add("Chaussures");
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

            if(nomListe == "Style")
            {
                List<StyleVetement> listeStyles = Listes.ListeStyles.ToList<StyleVetement>();

                for (int i = 0; i < listeStyles.Count; i++)
                {
                    lstStyles.Items.Add(listeStyles[i].NomStyle);
                }
            }

            if(nomListe == "Couleur")
            {
                List<Couleur> listeCouleurs = Listes.ListeCouleurs.ToList<Couleur>();

                for (int i = 0; i < listeCouleurs.Count; i++)
                {
                    cboCouleurs.Items.Add(listeCouleurs[i].NomCouleur);
                }
            }
        }

        private void btnSoumettre_Click(object sender, RoutedEventArgs e)
        {
            // On doît faire la validation
            // On début, on reset tout
            txbErreurNom.Visibility = Visibility.Hidden;
            lblNomVetement.Foreground = System.Windows.Media.Brushes.Black;
            txbErreurPrix.Visibility = Visibility.Hidden;
            lblPrix.Foreground = System.Windows.Media.Brushes.Black;
            txbErreurCouleur.Visibility = Visibility.Hidden;
            lblCouleur.Foreground = System.Windows.Media.Brushes.Black;
            txbErreurActivites.Visibility = Visibility.Hidden;
            lblActivite.Foreground = System.Windows.Media.Brushes.Black;
            txbErreurStyles.Visibility = Visibility.Hidden;
            lblStyles.Foreground = System.Windows.Media.Brushes.Black;
            txbErreurTemperatures.Visibility = Visibility.Hidden;
            lblTemperature.Foreground = System.Windows.Media.Brushes.Black;
            txbErreurType.Visibility = Visibility.Hidden;
            lblType.Foreground = System.Windows.Media.Brushes.Black;
            txbErreurSexe.Visibility = Visibility.Hidden;
            lblSexe.Foreground = System.Windows.Media.Brushes.Black;
            txbErreurImageURL.Visibility = Visibility.Hidden;
            lblImageURL.Foreground = System.Windows.Media.Brushes.Black;

            bool bContinuer = true;
            // On vérifie si le nom du vêtement est correcte.
            if(txtNomVetement.Text == "")
            {
                txbErreurNom.Visibility = Visibility.Visible;
                lblNomVetement.Foreground = System.Windows.Media.Brushes.Red;
                bContinuer = false;
            }

            // On vérifie si le prix est correcte.
            if (!Regex.IsMatch(txtPrix.Text, @"^\d+$"))
            {
                txbErreurPrix.Visibility = Visibility.Visible;
                lblPrix.Foreground = System.Windows.Media.Brushes.Red;
                bContinuer = false;
            }

            // On vérifie si la couleur est correcte.
            if (cboCouleurs.SelectedIndex == -1)
            {
                txbErreurCouleur.Visibility = Visibility.Visible;
                lblCouleur.Foreground = System.Windows.Media.Brushes.Red;
                bContinuer = false;
            }

            // On vérifie si l'utilisateur a choisi au moins une activité, un style et une température, un type, un sexe
            if(lstActivites.SelectedIndex == -1)
            {
                txbErreurActivites.Visibility = Visibility.Visible;
                lblActivite.Foreground = System.Windows.Media.Brushes.Red;
                bContinuer = false;
            }
            if (lstStyles.SelectedIndex == -1)
            {
                txbErreurStyles.Visibility = Visibility.Visible;
                lblStyles.Foreground = System.Windows.Media.Brushes.Red;
                bContinuer = false;
            }
            if (lstTemperatures.SelectedIndex == -1)
            {
                txbErreurTemperatures.Visibility = Visibility.Visible;
                lblTemperature.Foreground = System.Windows.Media.Brushes.Red;
                bContinuer = false;
            }
            if (cboTypes.SelectedIndex == -1)
            {
                txbErreurType.Visibility = Visibility.Visible;
                lblType.Foreground = System.Windows.Media.Brushes.Red;
                bContinuer = false;
            }
            if (chbFemme.IsChecked == false && chbHomme.IsChecked == false)
            {
                txbErreurSexe.Visibility = Visibility.Visible;
                lblSexe.Foreground = System.Windows.Media.Brushes.Red;
                bContinuer = false;
            }

            // On regarde si c'est un URI
            if (!IsLocalPath(txtImageURL.Text))
            {
                if (!RemoteFileExists(txtImageURL.Text))
                {
                    txbErreurImageURL.Visibility = Visibility.Visible;
                    lblImageURL.Foreground = System.Windows.Media.Brushes.Red;
                    bContinuer = false;
                }
            }   // On regarde si c'est un lien local
            else if (IsLocalPath(txtImageURL.Text))
            {
                using (WebClient client = new WebClient())
                {
                    client.Credentials = new NetworkCredential("1237596", "31amQmXKQ2");
                    // On regarde quel type de vêtement c'est
                    if (cboTypes.SelectedItem.ToString() == "Bas")
                        client.UploadFile("ftp://420.cstj.qc.ca/images_mova/bas/", "STOR", txtImageURL.Text);
                    if (cboTypes.SelectedItem.ToString() == "Haut")
                        client.UploadFile("ftp://420.cstj.qc.ca/images_mova/hauts/", "STOR", txtImageURL.Text);
                    if (cboTypes.SelectedItem.ToString() == "Chaussures")
                        client.UploadFile("ftp://420.cstj.qc.ca/images_mova/chaussures/", "STOR", txtImageURL.Text);
                }
            }


            if (bContinuer)
            {

                bool estHomme = false;
                bool estFemme = false;
                TypeVetement typeVetement = new TypeVetement(cboTypes.SelectedItem.ToString());
                Couleur couleur = new Couleur(cboCouleurs.SelectedItem.ToString());
                // On doit construire la liste des activités, des styles et des températures
                List<Activite> listeActivites = new List<Activite>();
                List<StyleVetement> listeStyles = new List<StyleVetement>();
                List<Temperature> listeTemperatures = new List<Temperature>();
                // Insertion des items de la listbox dans la liste.
                foreach (string nomActivite in lstActivites.SelectedItems)
                {
                    Activite activite = new Activite(nomActivite);
                    listeActivites.Add(activite);
                }
                foreach (string nomStyle in lstStyles.SelectedItems)
                {
                    StyleVetement styleVetement = new StyleVetement(nomStyle);
                    listeStyles.Add(styleVetement);
                }
                foreach (string nomTemperature in lstTemperatures.SelectedItems)
                {
                    Temperature temperature = new Temperature(nomTemperature);
                    listeTemperatures.Add(temperature);
                }

                // On doit aller chercher les informations qui ne sont pas bindé.

                if (chbHomme.IsChecked == true)
                    estHomme = true;
                if (chbFemme.IsChecked == true)
                    estFemme = true;

                // On ajoute le vêtement à la BD. 
                ViewModel.ajouterVetement(typeVetement, estHomme, estFemme, couleur, listeActivites, listeStyles, listeTemperatures);


                // On affiche "Inscription réussie" et on connecte l'utilisateur
                MessageBox.Show("Vêtement ajouté! Il est maintenant disponible pour être selectionné dans un ensemble", "Opération réussie avec succès");
                
                // Ensuite, on se rend à l'écran styliste
                IApplicationService mainVM = ServiceFactory.Instance.GetService<IApplicationService>();

                mainVM.ChangeView<StylisteActiviteView>(new StylisteActiviteView());
            }


        }

        private static bool IsLocalPath(string p)
        {
            if (p.StartsWith("http:\\"))
            {
                return false;
            }

            p = p.Replace("\\", "/");
            if (Uri.IsWellFormedUriString(p, UriKind.Absolute))
                return new Uri(p).IsFile;
            else 
                return false;
        }

        private bool RemoteFileExists(string url)
        {
            try
            {
                //Creating the HttpWebRequest
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                //Setting the Request method HEAD, you can also use GET too.
                request.Method = "HEAD";
                //Getting the Web Response.
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //Returns TRUE if the Status code == 200
                return (response.StatusCode == HttpStatusCode.OK);

            }
            catch
            {
                //Any exception will returns false.
                return false;
            }
        }

        private void btnSelectionnerToutTemperatures_Click(object sender, RoutedEventArgs e)
        {
            lstTemperatures.SelectAll();
        }

        private void btnSelectionnerToutStyles_Click(object sender, RoutedEventArgs e)
        {
            lstStyles.SelectAll();
        }

        private void btnSelectionnerToutActivites_Click(object sender, RoutedEventArgs e)
        {
            lstActivites.SelectAll();
        }

        private void btnParcourir_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";

            open.ShowDialog();
            //if (open.ShowDialog() == DialogResult.OK)
            //{
                txtImageURL.Text = open.FileName;
            //} 
        }
    }
}
