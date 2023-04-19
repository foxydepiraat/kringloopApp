using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace kringloopKleding
{
    /// <summary>
    /// Interaction logic for wKlant.xaml
    /// </summary>
    public partial class wKlant : Window
    {
        kringloopAfhalingDataContext db = new kringloopAfhalingDataContext();
        private gezin gezinAanpassen;
        private gezinslid gezinslidAanpassen;

        private string kaartnummerResult;
        private int GezinId;
        private int lastId;

        public wKlant()
        {
            InitializeComponent();

            dgGezin.ItemsSource = db.gezins.ToList();
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

            private void dgGezin_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //dgGezin vullen
                gezinAanpassen = (gezin)dgGezin.SelectedItem;
                txtkaart.Text = gezinAanpassen.kringloopKaartnummer;
                txtAchternaam.Text = gezinAanpassen.achternaam;
                txtWoonplaats.Text = gezinAanpassen.woonplaats;
                cbActiefkaart.IsChecked = Convert.ToBoolean(gezinAanpassen.actief);

                int gezinid = gezinAanpassen.id;

                //datagrid gezinslid data die geiznslid.gezin_id == gezin.id 
                var gezinId = (gezin)dgGezin.SelectedItem;
                var gezinIdQuery = (from gl in db.gezinslids
                                    where gezinId.id == gl.gezin_id
                                    select gl);
                dgGezinslid.ItemsSource = gezinIdQuery;

                
            }
            catch (InvalidCastException a)
            {

            }
        }
        //vult textbox die bij gezinslid horen 
        private void dgGezinslid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                gezinslidAanpassen = (gezinslid)dgGezinslid.SelectedItem;
                txtVoornaam.Text = gezinslidAanpassen.voornaam;
                txtGeboortejaar.Text = gezinslidAanpassen.geboortejaar;
                cbActiefGezinsLid.IsChecked = Convert.ToBoolean(gezinslidAanpassen.actief);

                var dgGezinQuery = from gl in db.gezinslids
                                       join g in db.gezins on gl.gezin_id equals g.id
                                       where gl.voornaam == txtVoornaam.Text
                                       select g;

                foreach (var kaart in dgGezinQuery)
                {
                    
                    txtkaart.Text = kaart.kringloopKaartnummer;
                    txtAchternaam.Text = kaart.achternaam;
                    txtWoonplaats.Text = kaart.woonplaats;
                    cbActiefkaart.IsChecked = Convert.ToBoolean(kaart.actief);
                }
            }
            catch (InvalidCastException b)
            {

            }
        }

        //zoekt naar kaartnummer 
        private void btnKaartnummerSearch_Click(object sender, RoutedEventArgs e)
        {
            var GezinQuery = from g in db.gezins
                             where g.kringloopKaartnummer == txtkaart.Text
                             select g;

            foreach (var gelijkKaart in GezinQuery)
            {
                if (txtkaart.Text == gelijkKaart.kringloopKaartnummer)
                {
                    kaartnummerResult = gelijkKaart.kringloopKaartnummer;
                    txtAchternaam.Text = gelijkKaart.achternaam;
                    txtWoonplaats.Text = gelijkKaart.woonplaats;
                    cbActiefkaart.IsChecked = Convert.ToBoolean(gelijkKaart.actief);
                    GezinId = gelijkKaart.id;

                    dgGezin.ItemsSource = GezinQuery;
                    var gezinIdQuery = (from gl in db.gezinslids
                                        where gelijkKaart.id == gl.gezin_id
                                        select gl);

                    foreach (var gezinslid in gezinIdQuery)
                    {
                        txtVoornaam.Text = gezinslid.voornaam;
                        txtGeboortejaar.Text = gezinslid.geboortejaar;
                        cbActiefGezinsLid.IsChecked = Convert.ToBoolean(gezinslid.actief);
                    }
                }
            }

            if (txtkaart.Text != kaartnummerResult)
            {
                txtVoornaam.Text = null;
                txtAchternaam.Text = null;
                txtWoonplaats.Text = null;
                txtGeboortejaar.Text = null;
                cbActiefkaart.IsChecked = false;
                cbActiefGezinsLid.IsChecked = false;
            }
        }

        //voegt nieuwe gezin en gezinslid 
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (txtkaart.Text == "" || txtAchternaam.Text == "" || txtWoonplaats.Text == "")
            {
                legenVakjes windowMessage = new legenVakjes();
                windowMessage.Show();
            }

            if (txtkaart.Text != "" && txtAchternaam.Text != "" && txtWoonplaats.Text != "")
            {
                var kaartExist = from g in db.gezins
                                 where g.kringloopKaartnummer == txtkaart.Text
                                 select new
                                 {
                                     kaartnummer = g.kringloopKaartnummer,
                                 };

                foreach (var kaart in kaartExist)
                {
                    kaartnummerResult = kaart.kaartnummer;
                }

                if (kaartnummerResult != txtkaart.Text)
                {   
                    gezin gezinAdd = new gezin()
                    {
                        kringloopKaartnummer = txtkaart.Text,
                        achternaam = txtAchternaam.Text,
                        woonplaats = txtWoonplaats.Text,
                        actief = Convert.ToInt32(cbActiefkaart.IsChecked),
                    };

                    db.gezins.InsertOnSubmit(gezinAdd);
                    db.SubmitChanges();

                    var addQuery = from g in db.gezins
                                   orderby g.id ascending
                                   select new
                                   {
                                       lastId = g.id
                                   };

                    foreach (var item in addQuery)
                    {
                        lastId = item.lastId;
                    }

                    gezinslid gezinslid = new gezinslid()
                    {
                        voornaam = txtVoornaam.Text,
                        geboortejaar = txtGeboortejaar.Text,
                        actief = Convert.ToInt32(cbActiefGezinsLid.IsChecked),
                        gezin_id = Convert.ToInt32(lastId),
                    };

                    db.gezinslids.InsertOnSubmit(gezinslid);
                    db.SubmitChanges();

                    txtkaart.Text = null;
                    txtVoornaam.Text = null;
                    txtAchternaam.Text = null;
                    txtWoonplaats.Text = null;
                    txtGeboortejaar.Text = null;
                    cbActiefkaart.IsChecked = false;
                    cbActiefGezinsLid.IsChecked = false;

                    var kaartIdQuery = from g in db.gezins
                                       where g.kringloopKaartnummer == txtkaart.Text
                                       select new
                                       {
                                           gezinId = g.id
                                       };

                    foreach (var kaart in kaartIdQuery)
                    {
                        var gezinIdQuery = (from gl in db.gezinslids
                                            where kaart.gezinId == gl.gezin_id
                                            select gl);

                        dgGezinslid.ItemsSource = gezinIdQuery;
                    }

                    dgGezin.ItemsSource = db.gezins.ToList();
                    MessageBoxOkAdd wMsgOkAdd = new MessageBoxOkAdd();
                    wMsgOkAdd.Show();
                }
            }

            else 
            {
                MessageBoxExist messageBoxExist = new MessageBoxExist();
                messageBoxExist.Show();
            }
        }

        //voegt een niewe gezinslid toe aan bijbehorende gezin
        private void btnGezinslid_Click(object sender, RoutedEventArgs e)
        {
            var gezinIdQuery = from gl in db.gezinslids
                               join g in db.gezins on gl.gezin_id equals g.id
                               where g.kringloopKaartnummer == txtkaart.Text
                               select new
                               {
                                   kaarQueryResult = g.kringloopKaartnummer,
                                   gezinId = gl.gezin_id,

                               };

            foreach (var item in gezinIdQuery)
            {
                kaartnummerResult = item.kaarQueryResult;
                GezinId = Convert.ToInt32(item.gezinId);

            }

            //checkt als geen vakjes leeg gelaten zijn en als ze niet leeg zijn voegt gezinslid toe aan bijbehorende gezin
            if (kaartnummerResult != null && txtVoornaam.Text != null && txtGeboortejaar.Text != null)
            {
                gezinslid gezinslidAdd = new gezinslid();
                gezinslidAdd.voornaam = txtVoornaam.Text;
                gezinslidAdd.geboortejaar = txtGeboortejaar.Text;
                gezinslidAdd.actief = Convert.ToInt32(cbActiefGezinsLid.IsChecked);
                gezinslidAdd.gezin_id = GezinId;

                db.gezinslids.InsertOnSubmit(gezinslidAdd);
                db.SubmitChanges();

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

                var kaartIdQuery = from g in db.gezins
                                   where g.kringloopKaartnummer == txtkaart.Text
                                   select new
                                   {
                                       gezinId = g.id
                                   };



                foreach (var kaart in kaartIdQuery)
                {
                    var gezinsIdQuery = (from gl in db.gezinslids
                                         where kaart.gezinId == gl.gezin_id
                                         select gl);
                    dgGezinslid.ItemsSource = gezinsIdQuery;
                }

                txtkaart.Text = "";
                txtVoornaam.Text = "";
                txtAchternaam.Text = "";
                txtWoonplaats.Text = "";
                txtGeboortejaar.Text = "";
                cbActiefkaart.IsChecked = false;
                cbActiefGezinsLid.IsChecked = false;

                MessageBoxOkAdd messageBoxAdd = new MessageBoxOkAdd();
                messageBoxAdd.Show();
            }
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            //checkt als je een van de vakjes leeggelaten zijn
            if (txtkaart.Text == "" || txtAchternaam.Text == "" || txtWoonplaats.Text == "")
            {
                legenVakjes windowMessage = new legenVakjes();
                windowMessage.Show();
            }

            //checkt als heeft gelaten en als dat niet is dan voegt aanpassingen toe aan database
            if (gezinAanpassen != null)
            {
                if (txtkaart.Text != "" && txtAchternaam.Text != "" && txtWoonplaats.Text != "")
                {
                    gezinAanpassen.kringloopKaartnummer = txtkaart.Text;
                    gezinAanpassen.achternaam = txtAchternaam.Text;
                    gezinAanpassen.woonplaats = txtWoonplaats.Text;
                    gezinAanpassen.actief = Convert.ToInt32(cbActiefkaart.IsChecked);

                    if (gezinslidAanpassen != null && txtVoornaam.Text != "" && txtGeboortejaar.Text != "")
                    {
                        gezinslidAanpassen.voornaam = txtVoornaam.Text;
                        gezinslidAanpassen.geboortejaar = txtGeboortejaar.Text;
                        gezinslidAanpassen.actief = Convert.ToInt32(cbActiefGezinsLid.IsChecked);

                    }

                    var gezinIdQuery = from gl in db.gezinslids
                                       select gl;
                    dgGezinslid.ItemsSource = gezinIdQuery;
                    dgGezin.ItemsSource = db.gezins.ToList();
                    db.SubmitChanges();

                    MessageBoxOk messageBoxOK = new MessageBoxOk();
                    messageBoxOK.Show();

                    txtkaart.Text = null;
                    txtVoornaam.Text = null;
                    txtAchternaam.Text = null;
                    txtWoonplaats.Text = null;
                    txtGeboortejaar.Text = null;
                    cbActiefkaart.IsChecked = false;
                    cbActiefGezinsLid.IsChecked = false;
                }
            }
        }


    }
}
