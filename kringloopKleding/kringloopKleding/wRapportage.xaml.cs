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
        private afhaling afhalingen;

        private int gezinid;
        private int gezinslidid;

        private DateTime pickedDate;
        private int dateYear;
        private int dateMonth;
        public wRapportage()
        {
            InitializeComponent();

            dgGezin.ItemsSource = db.gezins;

            var afhalingQuery = from a in db.afhalings select a;

            dgAfhaling.ItemsSource = afhalingQuery;
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

        private void dgAfhaling_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            afhalingen = (afhaling)dgAfhaling.SelectedItem;
            dpRapportDatum.Text = afhalingen.datum.ToString();
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
            if (dpRapportDatum.Text == "")
            {
                pickedDate = DateTime.Today;
            }
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

                foreach (var afhaling in afhalingQuery)
                {
                    DateTime Date = Convert.ToDateTime(afhaling.datum);
                    int dateYear = Date.Year;
                    int dateMonth = Date.Month;
                }

                var monthQuery = from a in db.afhalings
                                 where a.gezinslid_id == gezinslidid
                                 where a.datum == pickedDate
                                 select a;
                
                if(monthQuery.Count() > 0 )
                {
                    dgAfhaling.ItemsSource = monthQuery;
                }
                else
                {
                    dgAfhaling.ItemsSource = afhalingQuery;
                }               
            }
            else if(txtKaart.Text != "")
            {
                var gezinQuery = from g in db.gezins
                                  where g.kringloopKaartnummer == txtKaart.Text
                                  select g;

                foreach(var gezin in gezinQuery)
                {
                    gezinid = gezin.id;
                }

                var gezinslidQuery = from gl in db.gezinslids
                                     where gl.gezin_id == gezinid
                                     select gl;

                foreach(var gezinslid  in gezinslidQuery)
                {
                    gezinslidid = gezinslid.id;
                    txtVoornaam.Text = gezinslid.voornaam;
                }

                dgGezinslid.ItemsSource = gezinslidQuery;

                var afhalingQuery = from a in db.afhalings
                                    where a.gezinslid_id == gezinslidid
                                    select a;

                foreach (var afhaling in afhalingQuery)
                {
                    DateTime Date = Convert.ToDateTime(afhaling.datum);
                    int dateYear = Date.Year;
                    int dateMonth = Date.Month;
                }

                var SeacrhMonthYearQuery = from a in db.afhalings
                                           where a.gezinslid_id == gezinslidid
                                           where dateYear == pickedDate.Year
                                           where dateMonth == pickedDate.Month
                                           select a;
                if (SeacrhMonthYearQuery.Count() > 0)
                {
                    dgAfhaling.ItemsSource = SeacrhMonthYearQuery;
                }
                else
                {
                    dgAfhaling.ItemsSource = afhalingQuery;
                }
            }
            else
            {
                messageboxes.legenVakjes legenVakjes = new messageboxes.legenVakjes();
                legenVakjes.Show();
                dgGezin.ItemsSource = db.gezins;
                dgGezinslid.ItemsSource = null;
                dgAfhaling.ItemsSource = null;
            }
        }

        private void dpRapportDatum_CalendarClosed(object sender, RoutedEventArgs e)
        {
            if (dpRapportDatum.SelectedDate != null)
            {
                pickedDate = Convert.ToDateTime(dpRapportDatum.SelectedDate);
            }
        }
    }
}
