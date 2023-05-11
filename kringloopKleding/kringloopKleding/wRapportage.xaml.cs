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

            dgFamily.ItemsSource = db.gezins;

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

        private void dgFamily_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            gezinnen = (gezin)dgFamily.SelectedItem;
            txtKaart.Text = gezinnen.kringloopKaartnummer;
        }

        private void dgFamilyMembers_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            gezinsleden = (gezinslid)dgFamilyMembers.SelectedItem;
            txtFirstName.Text = gezinsleden.voornaam;            
        }

        //Search result from entered data
        private void btnKaartnummerSearch_Click(object sender, RoutedEventArgs e)
        {
            if (dpRapportDatum.Text == "")
            {
                pickedDate = DateTime.Today;
            }
            //search with the entered data from the if + data
            if (txtKaart.Text != "" && txtFirstName.Text != "") 
            {                   
                var GezinKaartQuery = from g in db.gezins
                                      where g.kringloopKaartnummer == txtKaart.Text
                                      select g;

                foreach(var gid in GezinKaartQuery)
                {
                    gezinid = gid.id;
                }

                var gezinslidIdQuery = from gl in db.gezinslids
                                       where gl.voornaam == txtFirstName.Text
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
            //search for data was has been enetered what is in the if
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
                    txtFirstName.Text = gezinslid.voornaam;
                }

                dgFamilyMembers.ItemsSource = gezinslidQuery;

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
                dgFamily.ItemsSource = db.gezins;
                dgFamilyMembers.ItemsSource = null;
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

        //when pressed it will search data on year
        private void btnYear_Click(object sender, RoutedEventArgs e)
        {
            if(pickedDate == null)
            {
                pickedDate = DateTime.Today;
            }
            //checks if data  has been entered then seacrh for this + the year that has been entered
            if(txtKaart.Text != "" && txtFirstName.Text != "")
            {
                var Querygezinid = from g in db.gezins
                                   where g.kringloopKaartnummer == txtKaart.Text
                                   select g;

                foreach(var g in Querygezinid)
                {
                    gezinid = g.id;
                }

                var QueryGezinslid = from gl in db.gezinslids
                                     where gl.gezin_id == gezinid
                                     where gl.voornaam == txtFirstName.Text
                                     select gl;

                foreach(var gl in QueryGezinslid)
                {
                    gezinslidid = gl.id;
                }

                var QueryAfhalingYear = from a in db.afhalings
                                        where a.gezinslid_id == gezinslidid
                                        where a.datum.Value.Year == pickedDate.Year
                                        select a;

                dgAfhaling.ItemsSource = QueryAfhalingYear.ToList();
            }
            // if txtFirstname has not been enttered  then  search for all data that are equal to the enetered data (not done)
            else if(txtKaart.Text != "")
            {
                var QueryGezin = from g in db.gezins
                                 where g.kringloopKaartnummer == txtKaart.Text
                                 select g;

                foreach(var g in QueryGezin)
                {
                    gezinid = g.id;
                }

                var QueryGezinslid = from gl in db.gezinslids
                                     where gl.gezin_id == gezinid
                                     select gl;

                foreach(var gl in QueryGezinslid)
                {
                    gezinslidid = gl.id;
                }

                var QueryAfhalingYear = from a in db.afhalings
                                        join gl in db.gezinslids on a.gezinslid_id equals gl.id
                                        where gl.gezin_id == gezinid
                                        where a.datum.Value.Year == pickedDate.Year
                                        select a;
            }
        }

        //when pressed it will search data on month
        private void btnMonth_Click(object sender, RoutedEventArgs e)
        {
            if(pickedDate == null)
            {
                pickedDate = DateTime.Today;
            }
            //checks if data  has been entered then seacrh for this + the year that has been entered
            if (txtKaart.Text != "" && txtFirstName.Text != "")
            {
                var QueryGezinId = from g in db.gezins
                                   where g.kringloopKaartnummer == txtKaart.Text
                                   select g;

                foreach (var g in QueryGezinId)
                {
                    gezinid = g.id;
                }

                var QuerygezinslidId = from gl in db.gezinslids
                                       where gl.gezin_id == gezinid
                                       where gl.voornaam == txtFirstName.Text
                                       select gl;

                foreach (var gl in QuerygezinslidId)
                {
                    gezinslidid = gl.id;
                }

                var QueryAfhalingMonth = from a in db.afhalings
                                         where a.gezinslid_id == gezinslidid
                                         where a.datum.Value.Year == DateTime.Today.Year
                                         where a.datum.Value.Month == pickedDate.Month
                                         select a;

                dgAfhaling.ItemsSource = QueryAfhalingMonth.ToList();

            }
            // if txtFirstname has not been enttered  then  search for all data that are equal to the enetered data
            else if (txtKaart.Text != "" )
            {
                    var QueryGezinId = from g in db.gezins
                                       where g.kringloopKaartnummer == txtKaart.Text
                                       select g;

                    foreach (var g in QueryGezinId)
                    {
                        gezinid = g.id;
                    }

                    var QuerygezinslidId = from gl in db.gezinslids
                                           where gl.gezin_id == gezinid
                                           select gl;

                foreach (var gl in QuerygezinslidId)
                {
                    gezinslidid = gl.id;
                }

                var QueryAfhalingMonth = from a in db.afhalings
                                         join gl in db.gezinslids on a.gezinslid_id equals gl.id
                                         where gl.gezin_id == gezinid
                                         where a.datum.Value.Month == pickedDate.Month
                                         select a;
                                        
                dgAfhaling.ItemsSource = QueryAfhalingMonth.ToList();
                              
            }
            else
            {
                messageboxes.legenVakjes WlegenVakjes = new messageboxes.legenVakjes();
                WlegenVakjes.Show();
            }
        }
    }
}
