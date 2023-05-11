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
        private gezin ChangeFamily;
        private gezinslid ChangeFamilyMember;

        private string CardNumberResult;
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
                
                ChangeFamily = (gezin)dgGezin.SelectedItem;
                txtCard.Text = ChangeFamily.kringloopKaartnummer;
                txtLastname.Text = ChangeFamily.achternaam;
                cbResidence.Text = ChangeFamily.woonplaats;
                cbActiveCard.IsChecked = Convert.ToBoolean(ChangeFamily.actief);

                int gezinid = ChangeFamily.id;

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
                ChangeFamilyMember = (gezinslid)dgGezinslid.SelectedItem;
                txtFirstName.Text = ChangeFamilyMember.voornaam;
                txtbirthDate.Text = ChangeFamilyMember.geboortejaar;
                cbActiveFamilyMember.IsChecked = Convert.ToBoolean(ChangeFamilyMember.actief);

                var dgGezinQuery = from gl in db.gezinslids
                                       join g in db.gezins on gl.gezin_id equals g.id
                                       where gl.voornaam == txtFirstName.Text
                                       select g;

                foreach (var kaart in dgGezinQuery)
                {

                    txtCard.Text = kaart.kringloopKaartnummer;
                    txtLastname.Text = kaart.achternaam;
                    cbResidence.Text = kaart.woonplaats;
                    cbActiveCard.IsChecked = Convert.ToBoolean(kaart.actief);
                }
            }
            catch (InvalidCastException b)
            {

            }
        }

        //Seacrh for card Number that is equal to txtCard
        private void btnKaartnummerSearch_Click(object sender, RoutedEventArgs e)
        {
            var GezinQuery = from g in db.gezins
                             where g.kringloopKaartnummer == txtCard.Text
                             select g;

            foreach (var kaartEqual in GezinQuery)
            {
                if (txtCard.Text == kaartEqual.kringloopKaartnummer)
                {
                    CardNumberResult = kaartEqual.kringloopKaartnummer;
                    txtLastname.Text = kaartEqual.achternaam;
                    cbResidence.Text = kaartEqual.woonplaats;
                    cbActiveCard.IsChecked = Convert.ToBoolean(kaartEqual.actief);
                    GezinId = kaartEqual.id;

                    dgGezin.ItemsSource = GezinQuery;
                    var gezinIdQuery = (from gl in db.gezinslids
                                        where kaartEqual.id == gl.gezin_id
                                        select gl);

                    foreach (var gezinslid in gezinIdQuery)
                    {
                        txtFirstName.Text = gezinslid.voornaam;
                        txtbirthDate.Text = gezinslid.geboortejaar;
                        cbActiveFamilyMember.IsChecked = Convert.ToBoolean(gezinslid.actief);
                    }
                }
            }

            if (txtCard.Text != CardNumberResult)
            {
                txtFirstName.Text = null;
                txtLastname.Text = null;
                cbResidence.Text = null;
                txtbirthDate.Text = null;
                cbActiveCard.IsChecked = false;
                cbActiveFamilyMember.IsChecked = false;
            }
        }

        //Add a new family and Member
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
           
            if (txtCard.Text == "" || txtLastname.Text == "" || cbResidence.Text == "")
            {
                messageboxes.legenVakjes windowMessage = new messageboxes.legenVakjes();
                windowMessage.Show();
            }
            //checks if nothing is left empty what needs to be required
            if (txtCard.Text != "" && txtLastname.Text != "" && cbResidence.Text != "")
            {
                var kaartExist = from g in db.gezins
                                 where g.kringloopKaartnummer == txtCard.Text
                                 select new
                                 {
                                     kaartnummer = g.kringloopKaartnummer,
                                 };

                foreach (var kaart in kaartExist)
                {
                    CardNumberResult = kaart.kaartnummer;
                }

                if (CardNumberResult != txtCard.Text)
                {   
                    gezin gezinAdd = new gezin()
                    {
                        kringloopKaartnummer = txtCard.Text,
                        achternaam = txtLastname.Text,
                        woonplaats = cbResidence.Text,
                        actief = Convert.ToInt32(cbActiveCard.IsChecked),
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
                        voornaam = txtFirstName.Text,
                        geboortejaar = txtbirthDate.Text,
                        actief = Convert.ToInt32(cbActiveFamilyMember.IsChecked),
                        gezin_id = Convert.ToInt32(lastId),
                    };

                    db.gezinslids.InsertOnSubmit(gezinslid);
                    db.SubmitChanges();

                    txtCard.Text = null;
                    txtFirstName.Text = null;
                    txtLastname.Text = null;
                    cbResidence.Text = null;
                    txtbirthDate.Text = null;
                    cbActiveCard.IsChecked = false;
                    cbActiveFamilyMember.IsChecked = false;

                    var kaartIdQuery = from g in db.gezins
                                       where g.kringloopKaartnummer == txtCard.Text
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
                    messageboxes.MessageBoxOkAdd wMsgOkAdd = new messageboxes.MessageBoxOkAdd();
                    wMsgOkAdd.Show();
                }
            }


            if(txtCard.Text == CardNumberResult)
            {
                messageboxes.MessageBoxExist messageBoxExist = new messageboxes.MessageBoxExist();
                messageBoxExist.Show();
            }
        }

        //voegt een niewe gezinslid toe aan bijbehorende gezin
        private void btnGezinslid_Click(object sender, RoutedEventArgs e)
        {
            var gezinIdQuery = from gl in db.gezinslids
                               join g in db.gezins on gl.gezin_id equals g.id
                               where g.kringloopKaartnummer == txtCard.Text
                               select new
                               {
                                   kaarQueryResult = g.kringloopKaartnummer,
                                   gezinId = gl.gezin_id,

                               };

            foreach (var item in gezinIdQuery)
            {
                CardNumberResult = item.kaarQueryResult;
                GezinId = Convert.ToInt32(item.gezinId);

            }

            //checkt als geen vakjes leeg gelaten zijn en als ze niet leeg zijn voegt gezinslid toe aan bijbehorende gezin
            if (CardNumberResult != null && txtFirstName.Text != null && txtbirthDate.Text != null)
            {
                gezinslid gezinslidAdd = new gezinslid();
                gezinslidAdd.voornaam = txtFirstName.Text;
                gezinslidAdd.geboortejaar = txtbirthDate.Text;
                gezinslidAdd.actief = Convert.ToInt32(cbActiveFamilyMember.IsChecked);
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
                                   where g.kringloopKaartnummer == txtCard.Text
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

                txtCard.Text = "";
                txtFirstName.Text = "";
                txtLastname.Text = "";
                cbResidence.Text = "";
                txtbirthDate.Text = "";
                cbActiveCard.IsChecked = false;
                cbActiveFamilyMember.IsChecked = false;

                messageboxes.MessageBoxOkAdd messageBoxAdd = new messageboxes.MessageBoxOkAdd();
                messageBoxAdd.Show();
            }
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            //checkt als je een van de vakjes leeggelaten zijn
            if (txtCard.Text == "" || txtLastname.Text == "" || cbResidence.Text == "")
            {
                messageboxes.legenVakjes windowMessage = new messageboxes.legenVakjes();
                windowMessage.Show();
            }

            //checkt als heeft gelaten en als dat niet is dan voegt aanpassingen toe aan database
            if (ChangeFamily != null)
            {
                if (txtCard.Text != "" && txtLastname.Text != "" && cbResidence.Text != "")
                {
                    ChangeFamily.kringloopKaartnummer = txtCard.Text;
                    ChangeFamily.achternaam = txtLastname.Text;
                    ChangeFamily.woonplaats = cbResidence.Text.ToLower();
                    ChangeFamily.actief = Convert.ToInt32(cbActiveCard.IsChecked);

                    if (ChangeFamilyMember != null && txtFirstName.Text != "" && txtbirthDate.Text != "")
                    {
                        ChangeFamilyMember.voornaam = txtFirstName.Text;
                        ChangeFamilyMember.geboortejaar = txtbirthDate.Text;
                        ChangeFamilyMember.actief = Convert.ToInt32(cbActiveFamilyMember.IsChecked);

                    }

                    var gezinIdQuery = from gl in db.gezinslids
                                       select gl;
                    dgGezinslid.ItemsSource = gezinIdQuery;
                    dgGezin.ItemsSource = db.gezins.ToList();
                    db.SubmitChanges();

                    messageboxes.MessageBoxOk messageBoxOK = new messageboxes.MessageBoxOk();
                    messageBoxOK.Show();

                    txtCard.Text = null;
                    txtFirstName.Text = null;
                    txtLastname.Text = null;
                    cbResidence.Text = null;
                    txtbirthDate.Text = null;
                    cbActiveCard.IsChecked = false;
                    cbActiveFamilyMember.IsChecked = false;
                }
            }
        }
        private void cbWoonplaats_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
           
        }
    }
}
