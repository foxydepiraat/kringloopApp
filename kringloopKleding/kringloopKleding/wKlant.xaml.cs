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
        private int Familyid;
        private int lastId;
        private MessageBoxes messageboxes;
        public wKlant()
        {
            InitializeComponent();

            dgGezin.ItemsSource = db.gezins.ToList();
            dgGezinslid.ItemsSource = db.gezinslids.ToList();

            MessageBoxes messageBoxes = new MessageBoxes();

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
                txtResidence.Text = ChangeFamily.Woonplaats;
                txtReason.Text = ChangeFamily.reden;
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
                    txtResidence.Text = kaart.Woonplaats;
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
                    txtResidence.Text = kaartEqual.Woonplaats;
                    cbActiveCard.IsChecked = Convert.ToBoolean(kaartEqual.actief);
                    Familyid = kaartEqual.id;

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

                if (txtLastname.Text != "")
                {
                    var gezinslidQuery = from gl in db.gezinslids
                                         where gl.voornaam == txtFirstName.Text
                                         select gl;

                    dgGezinslid.ItemsSource = gezinslidQuery;
                }
                else
                {
                    var gezinslidQuery = from gl in db.gezinslids
                                         where gl.gezin_id == Familyid
                                         select gl;
                    dgGezinslid.ItemsSource = gezinslidQuery;
                }


            }

            if (txtCard.Text != CardNumberResult)
            {
                TextBoxReset();
            }
        }

        //Add a new family and Member
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
           
            if (txtCard.Text == "" || txtLastname.Text == "" || txtResidence.Text == "")
            {
                messageboxes.EmptyTextBoxes();
            }
            //checks if nothing is left empty what needs to be required
            if (txtCard.Text != "" && txtLastname.Text != "" && txtResidence.Text != "")
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

                    GezinsAddPlusGezinslid();
                    TextBoxReset();

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
                    messageboxes.MessageBoxOkAdd();
                }
            }


            if(txtCard.Text == CardNumberResult)
            {
                messageboxes.MessageBoxExist();
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
                Familyid = Convert.ToInt32(item.gezinId);

            }

            //checkt als geen vakjes leeg gelaten zijn en als ze niet leeg zijn voegt gezinslid toe aan bijbehorende gezin
            if (CardNumberResult != null && txtFirstName.Text != null && txtbirthDate.Text != null)
            {
                GezinslidAdd();

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

                TextBoxReset();
                messageboxes.MessageBoxOkAdd();
            }
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            //checkt als je een van de vakjes leeggelaten zijn
            if (txtCard.Text == "" || txtLastname.Text == "" || txtResidence.Text == "")
            {
                
                messageboxes.MessageboxOk();
            }

            //checkt als heeft gelaten en als dat niet is dan voegt aanpassingen toe aan database
            if (ChangeFamily != null)
            {
                if (txtCard.Text != "" && txtLastname.Text != "" && txtResidence.Text != "")
                {
                    ChangeFamily.kringloopKaartnummer = txtCard.Text;
                    ChangeFamily.achternaam = txtLastname.Text;
                    ChangeFamily.Woonplaats = txtResidence.Text.ToLower();
                    ChangeFamily.actief = Convert.ToInt32(cbActiveCard.IsChecked);
                    ChangeFamily.reden = txtReason.Text;

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

                    messageboxes.MessageboxOk();
                    TextBoxReset();
                }
            }
        }
        //reset all the textboxes as NULL
        public void TextBoxReset()
        {
            txtCard.Text = null;
            txtFirstName.Text = null;
            txtLastname.Text = null;
            txtResidence.Text = null;
            txtbirthDate.Text = null;
            txtReason.Text = null;
            cbActiveCard.IsChecked = false;
            cbActiveFamilyMember.IsChecked = false;
        }
       
       
        //funtion jsut to add familymember
       public void GezinslidAdd()
        {
            gezinslid gezinslidAdd = new gezinslid()
            {
                voornaam = txtFirstName.Text,
                geboortejaar = txtbirthDate.Text,
                actief = Convert.ToInt32(cbActiveFamilyMember.IsChecked),
                gezin_id = Convert.ToInt32(lastId),
            };

            db.gezinslids.InsertOnSubmit(gezinslidAdd);
            db.SubmitChanges();
        }

        //function to add family and also familymember
        public void GezinsAddPlusGezinslid()
        {
            gezin gezinAdd = new gezin()
            {
                kringloopKaartnummer = txtCard.Text,
                achternaam = txtLastname.Text,
                Woonplaats = txtResidence.Text,
                actief = Convert.ToInt32(cbActiveCard.IsChecked),
                reden = txtReason.Text,
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
        }
    }
}
