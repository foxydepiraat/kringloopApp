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
        MessageBoxes messageboxes = new MessageBoxes();

        public wKlant()
        {
            InitializeComponent();
            dgGezin.ItemsSource = db.gezins.ToList();
            dgGezinslid.ItemsSource = db.gezinslids.ToList();

            cbActiveCard.IsChecked = true;
            cbActiveFamilyMember.IsChecked = true;

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

                int familyId = ChangeFamily.id;

                var familyID = (gezin)dgGezin.SelectedItem;
                var familyIdQuery = (from gl in db.gezinslids
                                    where familyID.id == gl.gezin_id
                                    select gl);
                dgGezinslid.ItemsSource = familyIdQuery;
                foreach(var item in familyIdQuery)
                {
                    txtFirstName.Text = item.voornaam;
                    txtbirthYear.Text = item.geboortejaar;
                    cbActiveFamilyMember.IsChecked = Convert.ToBoolean(item.actief);
                }

            }
            catch (InvalidCastException a)
            {

            }
        }
        //
        private void dgGezinslid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ChangeFamilyMember = (gezinslid)dgGezinslid.SelectedItem;
                txtFirstName.Text = ChangeFamilyMember.voornaam;
                txtbirthYear.Text = ChangeFamilyMember.geboortejaar;
                cbActiveFamilyMember.IsChecked = Convert.ToBoolean(ChangeFamilyMember.actief);

                var dgFamilyQuery = from gl in db.gezinslids
                                       join g in db.gezins on gl.gezin_id equals g.id
                                       where gl.voornaam == txtFirstName.Text
                                       select g;

                foreach (var card in dgFamilyQuery)
                {
                    txtCard.Text = card.kringloopKaartnummer;
                    txtLastname.Text = card.achternaam;
                    txtResidence.Text = card.Woonplaats;
                    cbActiveCard.IsChecked = Convert.ToBoolean(card.actief);
                }
            }
            catch (InvalidCastException b)
            {

            }
        }

        //Seacrh for card number that is equal to txtCard
        private void btnKaartnummerSearch_Click(object sender, RoutedEventArgs e)
        {            
            var familyQuery = from g in db.gezins
                             where g.kringloopKaartnummer == txtCard.Text
                             select g;
             var result = familyQuery.Count();
            if (result <= 0) 
            {
                TextBoxReset();
                dgGezin.ItemsPanel = null;
                dgGezinslid.ItemsPanel = null;
            }
            else
            {
                foreach (var cardEqual in familyQuery)
                {
                    if (txtCard.Text == cardEqual.kringloopKaartnummer)
                    {
                        CardNumberResult = cardEqual.kringloopKaartnummer;
                        txtLastname.Text = cardEqual.achternaam;
                        txtResidence.Text = cardEqual.Woonplaats;
                        cbActiveCard.IsChecked = Convert.ToBoolean(cardEqual.actief);
                        Familyid = cardEqual.id;

                        dgGezin.ItemsSource = familyQuery;
                        var familyIdQuery = (from gl in db.gezinslids
                                            where cardEqual.id == gl.gezin_id
                                            select gl);

                        foreach (var familyMember in familyIdQuery)
                        {
                            txtFirstName.Text = familyMember.voornaam;
                            txtbirthYear.Text = familyMember.geboortejaar;
                            cbActiveFamilyMember.IsChecked = Convert.ToBoolean(familyMember.actief);
                        }
                    }

                    if (txtLastname.Text != "")
                    {
                        var familyMemberQuery = from gl in db.gezinslids
                                             where gl.voornaam == txtFirstName.Text
                                             select gl;

                        dgGezinslid.ItemsSource = familyMemberQuery;
                    }
                    else
                    {
                        var familyMemberQuery = from gl in db.gezinslids
                                             where gl.gezin_id == Familyid
                                             select gl;
                        dgGezinslid.ItemsSource = familyMemberQuery;

                    }
                }

                if (txtCard.Text != CardNumberResult)
                {
                    TextBoxReset();
                }
            }            
        }

        //Add a new family and familyMember
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {            
            //checks if nothing is left empty what needs to be required
            if (txtCard.Text != "" && txtLastname.Text == "" && txtResidence.Text != "" && txtReason.Text != "")
            {               

                var cardExist = from g in db.gezins
                                 where g.kringloopKaartnummer == txtCard.Text
                                 select new
                                 { 
                                     kaartnummer = g.kringloopKaartnummer,
                                 };

                foreach (var card in cardExist)
                {
                    CardNumberResult = card.kaartnummer;
                }

                if (CardNumberResult != txtCard.Text)
                {
                    GezinsAddPlusGezinslid();
                    TextBoxReset();

                    var cardIdQuery = from g in db.gezins
                                       where g.kringloopKaartnummer == txtCard.Text
                                       select new
                                       {
                                           gezinId = g.id
                                       };

                    foreach (var card in cardIdQuery)
                    {
                        var familyIdQuery = (from gl in db.gezinslids
                                            where card.gezinId == gl.gezin_id
                                            select gl);

                        dgGezinslid.ItemsSource = familyIdQuery;
                    }

                    dgGezin.ItemsSource = db.gezins.ToList();
                }
            }
            else
            {
                messageboxes.EmptyTextBoxes();
            }

            if (txtCard.Text == CardNumberResult)
            {
                messageboxes.MessageBoxExist();
            }
        }

        //add new familyMember on family that is on txtCard
        private void btnGezinslid_Click(object sender, RoutedEventArgs e)
        {
            var familyIdQuery = from gl in db.gezinslids
                               join g in db.gezins on gl.gezin_id equals g.id
                               where g.kringloopKaartnummer == txtCard.Text
                               select new
                               {
                                   kaarQueryResult = g.kringloopKaartnummer,
                                   gezinId = gl.gezin_id,
                               };

            foreach (var item in familyIdQuery)
            {
                CardNumberResult = item.kaarQueryResult;
                Familyid = Convert.ToInt32(item.gezinId);
            }

            //check if the textbox are not empty
            if (CardNumberResult != "" && txtFirstName.Text != "" && txtbirthYear.Text != "")
            {
                GezinslidAdd();

                var cardIdQuery = from g in db.gezins
                                   where g.kringloopKaartnummer == txtCard.Text
                                   select new
                                   {
                                       gezinId = g.id
                                   };

                foreach (var card in cardIdQuery)
                {
                    var familyMemberIdQuery = (from gl in db.gezinslids
                                         where card.gezinId == gl.gezin_id
                                         select gl);
                    dgGezinslid.ItemsSource = familyMemberIdQuery;
                }

                TextBoxReset();
            }
        }
        //change Family and familymember
        private void btnChange_Click(object sender, RoutedEventArgs e)
        {

            //check if one of the textox are empty
            if (txtCard.Text == "" || txtLastname.Text == "" || txtResidence.Text == "" || txtFirstName.Text == "" || txtbirthYear.Text == "")
            {
                messageboxes.EmptyTextBoxes();
            }
            else
            {
                //checkt en als dat is dan voegt aanpassingen toe aan database
                
                if (ChangeFamily != null)
                {
                    if (txtCard.Text != "" && txtLastname.Text != "" && txtResidence.Text != "")
                    {
                        ChangeFamily.achternaam = txtLastname.Text;
                        ChangeFamily.Woonplaats = txtResidence.Text.ToLower();
                        ChangeFamily.actief = Convert.ToInt32(cbActiveCard.IsChecked);
                        ChangeFamily.reden = txtReason.Text;

                        if (ChangeFamilyMember != null && txtFirstName.Text != "" && txtbirthYear.Text != "")
                        {
                            ChangeFamilyMember.voornaam = txtFirstName.Text;
                            ChangeFamilyMember.geboortejaar = txtbirthYear.Text;
                            ChangeFamilyMember.actief = Convert.ToInt32(cbActiveFamilyMember.IsChecked);
                        }

                        var familyIdQuery = from gl in db.gezinslids
                                            select gl;
                        dgGezinslid.ItemsSource = familyIdQuery;
                        dgGezin.ItemsSource = db.gezins.ToList();
                        db.SubmitChanges();

                        TextBoxReset();

                    }
                }
            else
            {
                messageboxes.MessageBoxExist();
            }
        }
            
        }
        //reset all the textboxes as Default
        public void TextBoxReset()
        {
            txtCard.Text = "";
            txtFirstName.Text = "";
            txtLastname.Text = "";
            txtResidence.Text = "";
            txtbirthYear.Text = "";
            txtReason.Text = "";
            cbActiveCard.IsChecked = true;
            cbActiveFamilyMember.IsChecked = true;
        }
               
       //funtion just to add familymember
       public void GezinslidAdd()
        {
            gezinslid familyMemberAdd = new gezinslid()
            {
                voornaam = txtFirstName.Text,
                geboortejaar = txtbirthYear.Text,
                actief = Convert.ToInt32(cbActiveFamilyMember.IsChecked),
                gezin_id = Convert.ToInt32(lastId),
            };

            db.gezinslids.InsertOnSubmit(familyMemberAdd);
            db.SubmitChanges();
        }

        //function to add family and also familymember
        public void GezinsAddPlusGezinslid()
        {
            gezin familyAdd = new gezin()
            {
                kringloopKaartnummer = txtCard.Text,
                achternaam = txtLastname.Text,
                Woonplaats = txtResidence.Text,
                actief = Convert.ToInt32(cbActiveCard.IsChecked),
                reden = txtReason.Text,
            };

            db.gezins.InsertOnSubmit(familyAdd);
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

            gezinslid familyMember = new gezinslid()
            {
                voornaam = txtFirstName.Text,
                geboortejaar = txtbirthYear.Text,
                actief = Convert.ToInt32(cbActiveFamilyMember.IsChecked),
                gezin_id = Convert.ToInt32(lastId),
            };

            db.gezinslids.InsertOnSubmit(familyMember);
            db.SubmitChanges();
        }
    }
}
