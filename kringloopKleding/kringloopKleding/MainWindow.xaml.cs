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

using System.Text.RegularExpressions;

namespace kringloopKleding
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        kringloopAfhalingDataContext db = new kringloopAfhalingDataContext();

        private gezinslid gezinslidsAfhaling;
        MessageBoxes messageboxes = new MessageBoxes();

        private string CardNumberResult;

        private int Familyid;
        private int FamilyMemberid;

        private int DateYear;
        private int DateMonth;

        private DateTime coolDown;
        public MainWindow()
        {
            InitializeComponent();
            dgFamilymember.ItemsSource = db.gezinslids.ToList();
        }

        private void klantenBeheer_Click(object sender, RoutedEventArgs e)
        {
            wKlant wKlant = new wKlant();
            wKlant.Show();
            Close();
        }

        private void Afhaling_Click(object sender, RoutedEventArgs e)
        {
            MainWindow wAfhaling = new MainWindow();
            wAfhaling.Show();
            Close();
        }

        private void Rapportage_Click(object sender, RoutedEventArgs e)
        {
            wRapportage wRapportage = new wRapportage();
            wRapportage.Show();
            Close();
        }

        private void dgFamilymember_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // 
            try
            {
                gezinslidsAfhaling = (gezinslid)dgFamilymember.SelectedItem;
                txtFirstName.Text = gezinslidsAfhaling.voornaam;
                var familyid = gezinslidsAfhaling.gezin_id;

                

                var cardPickUpQuery = from g in db.gezins
                                       where g.id == familyid
                                       select g;

                foreach (var card in cardPickUpQuery)
                {
                    txtCard.Text = card.kringloopKaartnummer;
                    Familyid = card.id;
                }

                var glidQuery = from gl in db.gezinslids
                                where gl.voornaam == txtFirstName.Text
                                where gl.gezin_id == Familyid
                                select gl;                                

                foreach(var glid in glidQuery)
                {
                    FamilyMemberid = glid.id;
                }

                var pickUpQuery = from a in db.afhalings
                                    where a.gezinslid_id == FamilyMemberid
                                    select a;

                dgPickUp.ItemsSource = pickUpQuery;

            }
            catch (InvalidCastException c)
            {
                
            }
        }

        //search for card that exist and is active and if not ask for redirecting to add a new card or to be active 
        private void btnKaartnummerSearch_Click(object sender, RoutedEventArgs e)
        {
            if (txtCard.Text != "")
            {

                var familyQuery = from g in db.gezins
                                  where g.kringloopKaartnummer == txtCard.Text
                                  select g;

                if (familyQuery.Count() <= 0)
                {
                    messageboxes.CardAlreadyExist cardAlreadyExist = new messageboxes.CardAlreadyExist(this);
                    dgPickUp.ItemsSource = null;
                    dgFamilymember.ItemsSource = null;
                    cardAlreadyExist.Show();
                }
                else
                {
                    // checking if card is active
                    var familyActiveQuery = from g in db.gezins
                                            where g.kringloopKaartnummer == txtCard.Text
                                            where g.actief == 1
                                            select g;

                    if (familyActiveQuery.Count() > 0)
                    {
                        foreach (var family in familyActiveQuery)
                        {
                            var FamilyMemberIdQuery = from gl in db.gezinslids
                                                      where gl.gezin_id == family.id
                                                      where gl.actief == 1
                                                      select gl;

                            if (family.kringloopKaartnummer == txtCard.Text)
                            {
                                CardNumberResult = family.kringloopKaartnummer;
                                dgFamilymember.ItemsSource = FamilyMemberIdQuery;
                            }
                        }
                    }
                    else
                    {
                        messageboxes.CardNotActive cardNotActive = new messageboxes.CardNotActive(this);
                        dgPickUp.ItemsSource = null;
                        dgFamilymember.ItemsSource = null;
                        cardNotActive.Show();
                    }

                    //datagrid afhaling

                    var cardPickUpQueryQuery = from g in db.gezins
                                               where g.kringloopKaartnummer == txtCard.Text
                                               select g;

                    foreach (var kaart in cardPickUpQueryQuery)
                    {
                        Familyid = kaart.id;
                    }

                    var glidQuery = from gl in db.gezinslids
                                    where gl.gezin_id == Familyid
                                    select gl;

                    foreach (var glid in glidQuery)
                    {
                        FamilyMemberid = glid.id;
                    }

                    var pickUpQuery = from a in db.afhalings
                                      where a.gezinslid_id == FamilyMemberid
                                      select a;

                    dgPickUp.ItemsSource = pickUpQuery;

                }
            }
            else
            {
                dgFamilymember.ItemsSource = db.gezinslids;
                dgPickUp.ItemsSource = null;
                TextBoxReset();
            }
        }

        private void btnAfhaling_Click(object sender, RoutedEventArgs e)
        {
            //if textboxes not empty try to make a pick up(afhaling) from time now.
            if (txtCard.Text != "" && txtFirstName.Text != "")
            {                
                coolDown = DateTime.Now;
                
                var FamilyidQuery = from g in db.gezins
                                   where g.kringloopKaartnummer == txtCard.Text
                                   select g;
                foreach (var gid in FamilyidQuery)
                {
                    Familyid = gid.id;
                }

                var familyMemberQuery = from gl in db.gezinslids
                                     where gl.voornaam == txtFirstName.Text
                                     where gl.gezin_id == Familyid
                                     select gl;

                foreach (var glid in familyMemberQuery)
                {
                    FamilyMemberid = glid.id;
                }

                var MonthsQuery = from a in db.afhalings
                                  where a.gezinslid_id == FamilyMemberid
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
                                     where a.gezinslid_id == FamilyMemberid
                                     select a;

                // check if were already earlier
                if (onceMonthQuery.Count() == 0)
                {
                    foreach (var Familyid in FamilyidQuery)
                    {
                        var FamilyMemberIdQuery = from gl in db.gezinslids
                                               where gl.gezin_id == Familyid.id
                                               where gl.voornaam == txtFirstName.Text
                                               select gl;

                        afhaling pickUp = new afhaling();
                        pickUp.datum = DateTime.Now;
                        
                        foreach (var FamilyMemberId in FamilyMemberIdQuery)
                        {
                            pickUp.gezinslid_id = FamilyMemberId.id;
                        }
                        db.afhalings.InsertOnSubmit(pickUp);
                    }
                    db.SubmitChanges();

                    var cardPickUpQuery = from gl in db.gezinslids
                                           join g in db.gezins on gl.gezin_id equals g.id
                                           where gl.voornaam == txtFirstName.Text
                                           select g;

                    foreach (var card in cardPickUpQuery)
                    {
                        txtCard.Text = card.kringloopKaartnummer;
                        Familyid = card.id;
                    }

                    var glidQuery = from gl in db.gezinslids
                                    where gl.gezin_id == Familyid
                                    select gl;

                    foreach (var glid in glidQuery)
                    {
                        FamilyMemberid = glid.id;
                    }

                    var pickUpQuery = from a in db.afhalings
                                      where a.gezinslid_id == FamilyMemberid
                                      select a;

                    dgPickUp.ItemsSource = pickUpQuery;
                                        
                    TextBoxReset();
                }
                else
                {
                    messageboxes.WaitAfhaling();
                }
            }
            else
            {
                messageboxes.EmptyTextBoxes();
            }
        }

        public void TextBoxReset()
        {
            txtCard.Text = "";
            txtFirstName.Text = "";
        }

        //function coems form messagboxes CardNotActice or CardsAlreadyExist
        public void NewCard()
        {
            wKlant wKlant = new wKlant();
            wKlant.Show();
            Close();
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
