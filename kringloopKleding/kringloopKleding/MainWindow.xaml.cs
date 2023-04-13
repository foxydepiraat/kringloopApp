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

namespace kringloopKleding
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        kringloopAfhalingDataContext db = new kringloopAfhalingDataContext();
        private string kaartnummerResult;
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void klantenBeheer_Click(object sender, RoutedEventArgs e)
        {
            wKlant wKlant = new wKlant();
            wKlant.Show();
            this.Close();
        }

        private void Afhaling_Click(object sender, RoutedEventArgs e)
        {
            MainWindow wAfhaling = new MainWindow();
            wAfhaling.Show();
            this.Close();
        }

        private void Rapportage_Click(object sender, RoutedEventArgs e)
        {
            wRapportage wRapportage = new wRapportage();
            wRapportage.Show();
            this.Close();

        }

        private void btnKaartnummerSearch_Click(object sender, RoutedEventArgs e)
        {
            string kaartnummer = txtkaart.Text;

            var joinSearch = from gl in db.gezinslids
                             join g in db.gezins on gl.gezin_id equals g.id

                             where g.kringloopKaartnummer == kaartnummer
                             select new 
                             {
                                 Kaartnummer = g.kringloopKaartnummer,
                                 Voornaam = gl.voornaam,
                                 Achternaam = g.achternaam,
                                 Woonplaats = g.woonplaats,
                                 Geboortejaar = gl.geboortejaar,
                                 ActiefKaart = Convert.ToBoolean(g.actief),
                                 ActiefGezinslid = Convert.ToBoolean(gl.actief),
                             };




            foreach (var item in joinSearch)
            {
                if (item.Kaartnummer == kaartnummer)
                {
                    kaartnummerResult = item.Kaartnummer;
                    dgAfhaling.ItemsSource = joinSearch.ToList();

                }
            }

            if (kaartnummerResult != kaartnummer)
            {
                var joinQuery = from gl in db.gezinslids
                                join g in db.gezins on gl.gezin_id equals g.id
                                select new 
                                {
                                    Kaartnummer = g.kringloopKaartnummer,
                                    Voornaam = gl.voornaam,
                                    Achternaam = g.achternaam,
                                    Woonplaats = g.woonplaats,
                                    Geboortejaar = gl.geboortejaar,
                                    ActiefKaart = Convert.ToBoolean(g.actief),
                                    ActiefGezinslid = Convert.ToBoolean(gl.actief),
                                };

                txtkaart.Text = "";

                dgAfhaling.ItemsSource = joinQuery.ToList();


            };
        }

    }
}

