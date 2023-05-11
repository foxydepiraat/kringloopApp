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
        private gezinslid gezinslidsAfhaling;

        private string kaartnummerResult;

        private int gezinid;
        private int gezinslidid;

        private int DateYear;
        private int DateMonth;

        private DateTime coolDown;
        public MainWindow()
        {
            InitializeComponent();

            dgGezinslid.ItemsSource = db.gezinslids.ToList();
            
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

        private void dgGezinslid_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // vult textbox voornaam dat geselecteerd en daarna 
            try
            {
                gezinslidsAfhaling = (gezinslid)dgGezinslid.SelectedItem;
                txtFirstName.Text = gezinslidsAfhaling.voornaam;

                var kaartOphaalQuery = from gl in db.gezinslids
                                       join g in db.gezins on gl.gezin_id equals g.id
                                       where gl.voornaam == txtFirstName.Text
                                       select g;

                foreach (var kaart in kaartOphaalQuery)
                {
                    txtkaart.Text = kaart.kringloopKaartnummer;
                }

                var afhalingQuery = from a in db.afhalings
                                    join gl in db.gezinslids on a.gezinslid_id equals gl.id
                                    join g in db.gezins on gl.gezin_id equals g.id
                                    where g.kringloopKaartnummer == txtkaart.Text
                                    select new
                                    {
                                        datum = a.datum,
                                        Voornaam = gl.voornaam,
                                        geboortejaar = gl.geboortejaar,
                                        achternaam = g.achternaam,
                                        woonplaats = g.woonplaats,
                                    };

                dgAfhaling.ItemsSource = afhalingQuery;

            }
            catch (InvalidCastException c)
            {

            }
        }

        private void btnKaartnummerSearch_Click(object sender, RoutedEventArgs e)
        {
            // datagrid gezinslid
            string kaartnummer = txtkaart.Text;

            var gezinQuery = from g in db.gezins
                             where g.kringloopKaartnummer == kaartnummer
                             where g.actief == 1
                             select g;

            if (gezinQuery.Count() > 0)
            {
                foreach (var gezins in gezinQuery)
                {
                    var gezinslidIdQuery = from gl in db.gezinslids
                                           where gl.gezin_id == gezins.id
                                           where gl.actief == 1
                                           select gl;

                    if (gezins.kringloopKaartnummer == kaartnummer)
                    {
                        kaartnummerResult = gezins.kringloopKaartnummer;
                        dgGezinslid.ItemsSource = gezinslidIdQuery;
                    }
                }
            }

            else if (gezinQuery.Count() == 0)
            {
                dgAfhaling.ItemsSource = null;
                messageboxes.kaartOfgezinslidNietActief kaartOfgezinslid = new messageboxes.kaartOfgezinslidNietActief();
                kaartOfgezinslid.Show();
            }

            //datagrid afhaling
            var afhalingQuery = from a in db.afhalings
                                join gl in db.gezinslids on a.gezinslid_id equals gl.id
                                join g in db.gezins on gl.gezin_id equals g.id
                                where g.kringloopKaartnummer == txtkaart.Text
                                select new
                                {
                                    datum = a.datum,
                                    Voornaam = gl.voornaam,
                                    geboortejaar = gl.geboortejaar,
                                    achternaam = g.achternaam,
                                    woonplaats = g.woonplaats,
                                };

            dgAfhaling.ItemsSource = afhalingQuery;
        }

        private void btnAfhaling_Click(object sender, RoutedEventArgs e)
        {
            // als de vakjes niet leeg zijn gaat het proberen een afhaling te maken
            if (txtkaart.Text != "" && txtFirstName.Text != "")
            {                
                coolDown = DateTime.Today;
                
                var GezinIdQuery = from g in db.gezins
                                   where g.kringloopKaartnummer == txtkaart.Text
                                   select g;
                foreach (var gid in GezinIdQuery)
                {
                    gezinid = gid.id;
                }


                var gezinslidQuery = from gl in db.gezinslids
                                     where gl.voornaam == txtFirstName.Text
                                     where gl.gezin_id == gezinid
                                     select gl;

                foreach (var glid in gezinslidQuery)
                {
                    gezinslidid = glid.id;
                }

                var MonthsQuery = from a in db.afhalings
                                  where a.gezinslid_id == gezinslidid
                                  select a;

                foreach (var a in MonthsQuery)
                {
                    DateTime Date = Convert.ToDateTime(a.datum);
                    DateYear = Date.Year;
                    DateMonth = Date.Month;
                }

                var onceMonthQuery = from a in db.afhalings
                                     where DateYear == coolDown.Year
                                     where DateMonth == coolDown.Month
                                     where a.gezinslid_id == gezinslidid
                                     select a;


                // checkt als het eerder deze maand gedaan is
                if (onceMonthQuery.Count() == 0)
                {
                    foreach (var gezinId in GezinIdQuery)
                    {
                        var gezinslidIdQuery = from gl in db.gezinslids
                                               where gl.gezin_id == gezinId.id
                                               where gl.voornaam == txtFirstName.Text
                                               select gl;

                        afhaling afhalings = new afhaling();
                        afhalings.datum = DateTime.Today;
                        

                        foreach (var gezinslidId in gezinslidIdQuery)
                        {
                            afhalings.gezinslid_id = gezinslidId.id;
                        }
                        db.afhalings.InsertOnSubmit(afhalings);
                    }
                    db.SubmitChanges();

                    var afhalingQuery = from a in db.afhalings
                                        join gl in db.gezinslids on a.gezinslid_id equals gl.id
                                        join g in db.gezins on gl.gezin_id equals g.id
                                        where g.kringloopKaartnummer == txtkaart.Text
                                        select new
                                        {
                                            datum = a.datum,
                                            Voornaam = gl.voornaam,
                                            geboortejaar = gl.geboortejaar,
                                            achternaam = g.achternaam,
                                            woonplaats = g.woonplaats,
                                        };

                    dgAfhaling.ItemsSource = afhalingQuery;

                    messageboxes.wMessageAfhaling wMessageAfhaling = new messageboxes.wMessageAfhaling();
                    wMessageAfhaling.Show();

                    txtkaart.Text = null;
                    txtFirstName.Text = null;
                }
                else
                {
                    messageboxes.MessageBoxWait messageBoxWait = new messageboxes.MessageBoxWait();
                    messageBoxWait.Show();
                }
            }
            else
            {
                //open een window messagebox dat het vakjes leeg zijn
                messageboxes.legenVakjes legenVakjes = new messageboxes.legenVakjes();
                legenVakjes.Show();
            }
        }
    }
}
