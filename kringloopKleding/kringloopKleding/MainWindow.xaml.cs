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
            try
            {
                gezinslidsAfhaling = (gezinslid)dgGezinslid.SelectedItem;
                txtVoornaam.Text = gezinslidsAfhaling.voornaam;

                var kaartOphaalQuery = from gl in db.gezinslids
                                       join g in db.gezins on gl.gezin_id equals g.id
                                       where gl.voornaam == txtVoornaam.Text
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
                             where  g.kringloopKaartnummer == kaartnummer
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
                kaartOfgezinslidNietActief kaartOfgezinslid = new kaartOfgezinslidNietActief();
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
            var GezinsliIddQuery = from gl in db.gezinslids
                                   join g in db.gezins on gl.gezin_id equals g.id
                                   where g.kringloopKaartnummer == txtkaart.Text
                                   where gl.voornaam == txtVoornaam.Text
                                   select gl;

            if(GezinsliIddQuery != null) {
                foreach (var GlGJoin in GezinsliIddQuery)
                {
                    var oldDateQuery = from a in db.afhalings
                                       where a.gezinslid_id == GlGJoin.id
                                       select a;
                    foreach (var Date in oldDateQuery)
                    {
                        DateTime oldDate = (DateTime)Date.datum;
                        coolDown = oldDate;
                    }
                }
            }
            
            if (Convert.ToDateTime(datePicker.Text) != coolDown)
            {
                if (txtkaart.Text != "" && txtVoornaam.Text != "")
                {
                    var GezinIdQuery = from g in db.gezins
                                       where g.kringloopKaartnummer == txtkaart.Text
                                       select g;
                    foreach (var gezinId in GezinIdQuery)
                    {
                        var gezinslidIdQuery = from gl in db.gezinslids
                                               where gl.gezin_id == gezinId.id
                                               where gl.voornaam == txtVoornaam.Text
                                               select gl;

                        afhaling afhalings = new afhaling();
                        afhalings.datum = datePicker.SelectedDate;
                        if (datePicker.Text == "")
                        {
                            afhalings.datum = DateTime.Now;
                        }

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

                    wMessageAfhaling wMessageAfhaling = new wMessageAfhaling();
                    wMessageAfhaling.Show();

                    txtkaart.Text = null;
                    txtVoornaam.Text = null;
                    datePicker.SelectedDate = null;
                }
                else
                {
                    legenVakjes legenVakjes = new legenVakjes();
                    legenVakjes.Show();
                }
            }
        }
    }
}

