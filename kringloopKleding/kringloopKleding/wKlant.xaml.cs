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
using System.Text.RegularExpressions;

namespace kringloopKleding
{

    /// <summary>
    /// Interaction logic for wKlant.xaml
    /// </summary>
    public partial class wKlant : Window
    {
        kringloopAfhalingDataContext db = new kringloopAfhalingDataContext();
        private gezin changeFamily;
        private gezinslid changeFamilyMember;
        private string CardNumberResult;
        private int Familyid;
        private int lastId;
        private string sameCard;
        MessageBoxes messageboxes = new MessageBoxes();

        public wKlant()
        {
            InitializeComponent();
            initializeComboboxes();
           
            dgGezin.ItemsSource = null;
            dgGezinslid.ItemsSource = null;
            cbActiveCard.IsChecked = true;
            cbActiveFamilyMember.IsChecked = true;
        }

        private void initializeComboboxes()
        {
            List<string> residences = new List<string>();
            foreach (var res in db.woonplaatsens)
            {
                residences.Add(res.woonplaats);
            }
            residences = residences.OrderBy(res => res).ToList();
            txtResidence.SelectedIndex = 0;
            txtResidence.ItemsSource = residences;

            List<string> reasons = new List<string>();
            foreach (var rea in db.redenens)
            {
                reasons.Add(rea.reden);
            }
            reasons = reasons.OrderBy(rea => rea).ToList();
            txtReason.SelectedIndex = 0;
            txtReason.ItemsSource = reasons;
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

        //Fills in the txtboxes with the data that was clicked on.
        private void dgGezin_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {                
                changeFamily = (gezin)dgGezin.SelectedItem;
                txtCard.Text = changeFamily.kringloopKaartnummer;
                txtLastname.Text = changeFamily.achternaam;
                txtResidence.Text = changeFamily.Woonplaats;
                txtReason.Text = changeFamily.reden;
                cbActiveCard.IsChecked = Convert.ToBoolean(changeFamily.actief);
                sameCard = changeFamily.kringloopKaartnummer;

                int familyId = changeFamily.id;

                var familyID = (gezin)dgGezin.SelectedItem;

                var familyIdQuery = (from gl in db.gezinslids
                                    where familyID.id == gl.gezin_id
                                    select gl);

                dgGezinslid.ItemsSource = familyIdQuery;
                              
            }
            catch (InvalidCastException a)
            {

            }
        }
        //Fills in the txtboxes with the data that was clicked on.
        private void dgGezinslid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                changeFamilyMember = (gezinslid)dgGezinslid.SelectedItem;
                txtFirstName.Text = changeFamilyMember.voornaam;
                txtbirthYear.Text = changeFamilyMember.geboortejaar;
                cbActiveFamilyMember.IsChecked = Convert.ToBoolean(changeFamilyMember.actief);
                var familyid = changeFamilyMember.gezin.id;

                var dgFamilyMemeberQuery = from gl in db.gezinslids
                                       where gl.voornaam == txtFirstName.Text
                                       select gl;

                var familyQuery = from g in db.gezins
                                  where g.id == familyid
                                  select g;

                foreach (var card in familyQuery)
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

        //Seacrh for card number that is equal to txtCard.
        private void btnKaartnummerSearch_Click(object sender, RoutedEventArgs e)
        {
            var familyQuery = from g in db.gezins
                              where g.kringloopKaartnummer == txtCard.Text
                              select g;

            var result = familyQuery.Count();
            if (result <= 0)
            {
                TextBoxReset();
                dgGezinslid.ItemsSource = null;
                dgGezin.ItemsSource = null;
            }
            else
            {
                changeFamily = familyQuery.First(); // Set changeFamily to the first result (if any)
                if (txtCard.Text == changeFamily.kringloopKaartnummer)
                {
                    CardNumberResult = changeFamily.kringloopKaartnummer;
                    txtLastname.Text = changeFamily.achternaam;
                    txtResidence.Text = changeFamily.Woonplaats;
                    txtReason.Text = changeFamily.reden;
                    cbActiveCard.IsChecked = Convert.ToBoolean(changeFamily.actief);
                    Familyid = changeFamily.id;
                    sameCard = changeFamily.kringloopKaartnummer;
                    dgGezin.ItemsSource = familyQuery;

                    var familyIdQuery = from gl in db.gezinslids
                                        where changeFamily.id == gl.gezin_id
                                        select gl;

                    foreach (var familyMember in familyIdQuery)
                    {
                        txtFirstName.Text = familyMember.voornaam;
                        txtbirthYear.Text = familyMember.geboortejaar;
                        cbActiveFamilyMember.IsChecked = Convert.ToBoolean(familyMember.actief);
                    }

                    dgGezinslid.ItemsSource = familyIdQuery;
                }

                if (txtLastname.Text != "" )
                {
                    var familyMemberQuery = from gl in db.gezinslids
                                            where gl.gezin_id == changeFamily.id
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

                if (txtCard.Text != CardNumberResult)
                {
                    TextBoxReset();
                }
            }
        }


        //Add a new family and familyMember.
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            makeNewReason();
            //Checks if nothing is left empty what needs to be required.
            if (txtCard.Text == CardNumberResult)
            {
                messageboxes.MessageBoxExist();
            }
            else if (txtCard.Text != "" && txtLastname.Text != "" && txtResidence.Text != "" && txtReason.Text != "")
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

                    dgGezin.ItemsSource = cardIdQuery;

                    TextBoxReset();
                }
            }
            else
            {
                messageboxes.EmptyTextBoxes();
            }

            
        }

        //Add new familyMember on family that is on txtCard.
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

            //Check if the textbox are not empty.
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
            else
            {
                messageboxes.EmptyTextBoxes();
            }
        }
        //Change Family and familymember.
        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            makeNewReason();
            //Check if one of the textbox are empty
            if (txtCard.Text == "" || txtLastname.Text == "" || txtResidence.Text == "")
            {
                messageboxes.EmptyTextBoxes();
            }
            else
            {
               
                //Checkt for changefamily(db.gezins) is not null and second check is if the textboxes are not empty.
                //If that is true then they add the data to the database.
                
                if (changeFamily != null)
                {
                   
                    if (txtCard.Text == sameCard && txtLastname.Text != "" && txtResidence.Text != "")
                    {

                        ChangeFamily();
                    }
                    else
                    {

                        var existinCardQuery = from g in db.gezins
                                               where g.kringloopKaartnummer == txtCard.Text
                                               select g;

                        if (existinCardQuery.Count()  == 0)
                        {
                            
                            changeFamily.kringloopKaartnummer = txtCard.Text;
                            changeFamily.achternaam = txtLastname.Text;
                            changeFamily.Woonplaats = txtResidence.Text;
                            changeFamily.actief = Convert.ToInt32(cbActiveCard.IsChecked);
                            changeFamily.reden = txtReason.Text;

                            if (changeFamilyMember != null && txtFirstName.Text != "" && txtbirthYear.Text != "")
                            {
                                changeFamilyMember.voornaam = txtFirstName.Text;
                                changeFamilyMember.geboortejaar = txtbirthYear.Text;
                                changeFamilyMember.actief = Convert.ToInt32(cbActiveFamilyMember.IsChecked);
                            }

                            dgGezinslid.ItemsSource = db.gezinslids;
                            dgGezin.ItemsSource = db.gezins.ToList();
                            db.SubmitChanges();
                            TextBoxReset();
                        }
                        else
                        {
                            messageboxes.MessageBoxExist();
                        }
                    }
                }
            }
        }

        private void ChangeFamily()
        {
            changeFamily.achternaam = txtLastname.Text;
            changeFamily.Woonplaats = txtResidence.Text;
            changeFamily.actief = Convert.ToInt32(cbActiveCard.IsChecked);
            changeFamily.reden = txtReason.Text.ToLower();

            if (changeFamilyMember != null && txtFirstName.Text != "" && txtbirthYear.Text != "")
            {
                changeFamilyMember.voornaam = txtFirstName.Text;
                changeFamilyMember.geboortejaar = txtbirthYear.Text;
                changeFamilyMember.actief = Convert.ToInt32(cbActiveFamilyMember.IsChecked);
            }

            var familyQuery = from g in db.gezins
                             where g.kringloopKaartnummer == txtCard.Text
                             select g;

            dgGezin.ItemsSource = familyQuery;

            foreach( var g in familyQuery)
            {
                Familyid = g.id;
            }
            var FamilymembrQuery = from gl in db.gezinslids
                                    where gl.gezin_id == Familyid
                                    select gl;
            
            dgGezinslid.ItemsSource = FamilymembrQuery;

            db.SubmitChanges();
            TextBoxReset();
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
                gezin_id = Convert.ToInt32(Familyid),
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
                           where g.kringloopKaartnummer == txtCard.Text
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

            dgGezinslid.ItemsSource = db.gezinslids;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void makeNewReason()
        {
            var ExitingReasonQuery = from rea in db.redenens
                                     where rea.reden == txtReason.Text.ToLower()
                                     select rea;

            if(ExitingReasonQuery.Count() == 0)
            {
                redenen reason = new redenen();
                reason.reden = txtReason.Text.ToLower();

                db.redenens.InsertOnSubmit(reason);
                db.SubmitChanges();

                initializeComboboxes();
            }
        }
    }
}
