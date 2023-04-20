using System;
using System.Linq;
using System.Windows;

namespace kringloopKleding
{
    /// <summary>
    /// Interaction logic for wRapportage.xaml
    /// </summary>
    public partial class wRapportage : Window
    {
        kringloopAfhalingDataContext db = new kringloopAfhalingDataContext();
        private gezin gezinnen;
        private gezinslid gezinsleden;

        private int gezinid;
        private int gezinslidid;

        private DateTime pickedDate;
        public wRapportage()
        {
            InitializeComponent();

            dgGezin.ItemsSource = db.gezins;

            var afhalingQuery = from a in db.afhalings select a;

            dgrapport.ItemsSource = afhalingQuery;
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

        private void dgGezin_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            gezinnen = (gezin)dgGezin.SelectedItem;
            txtKaart.Text = gezinnen.kringloopKaartnummer;
        }

        private void dgGezinslid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            gezinsleden = (gezinslid)dgGezinslid.SelectedItem;
            txtVoornaam.Text = gezinsleden.voornaam;            
        }

        private void btnKaartnummerSearch_Click(object sender, RoutedEventArgs e)
        {
            if (txtKaart.Text != "" && txtVoornaam.Text != "") 
            {
                var GezinKaartQuery = from g in db.gezins
                                 where g.kringloopKaartnummer == txtKaart.Text
                                 select g;

                foreach(var gid in GezinKaartQuery)
                {
                    gezinid = gid.id;
                }

                var gezinslidIdQuery = from gl in db.gezinslids
                                       where gl.voornaam == txtVoornaam.Text
                                       where gl.gezin_id == gezinid
                                       select gl;

                foreach(var glid in gezinslidIdQuery)
                {
                    gezinslidid = glid.id;
                   
                }

                var afhalingQuery = from a in db.afhalings
                                    where a.gezinslid_id == gezinslidid
                                    select a;

                dgrapport.ItemsSource = afhalingQuery;
                
            }
            else if(txtKaart.Text != "")
            {
                var GezinKaartQuery = from g in db.gezins
                                      where g.kringloopKaartnummer == txtKaart.Text
                                      select g;

                foreach (var gid in GezinKaartQuery)
                {
                    gezinid = gid.id;
                }

                var gezinslidIdQuery = from gl in db.gezinslids
                                       where gl.gezin_id == gezinid
                                       select gl;

                dgGezinslid.ItemsSource = gezinslidIdQuery;
            }
            else
            {
                legenVakjes legenVakjes = new legenVakjes();
                legenVakjes.Show();
            }
        }

        private void dpRapportDatum_CalendarClosed(object sender, RoutedEventArgs e)
        {
            if (dpRapportDatum.SelectedDate != null)
            {
                pickedDate = Convert.ToDateTime(dpRapportDatum.SelectedDate);
                MessageBox.Show(pickedDate.ToString());
            }
        }
    }
}
