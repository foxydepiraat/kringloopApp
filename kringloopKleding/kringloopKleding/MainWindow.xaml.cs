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

        messageboxes.kaartOfgezinslidNietActief kaartOfgezinslid = new messageboxes.kaartOfgezinslidNietActief();
        private gezinslid gezinslidsAfhaling;
        private MessageBoxes MessageBoxes;

        private string CardNumberResult;

        private int Familyid;
        private int FamilyMemberid;

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
                    txtCard.Text = kaart.kringloopKaartnummer;
                    Familyid = kaart.id;
                }

                var glidQuery = from gl in db.gezinslids
                                where gl.gezin_id == Familyid
                                select gl;                                

                foreach(var glid in glidQuery)
                {
                    FamilyMemberid = glid.id;
                }

                var afhalingQuery = from a in db.afhalings
                                    where a.gezinslid_id == FamilyMemberid
                                    select a;

                dgAfhaling.ItemsSource = afhalingQuery;

            }
            catch (InvalidCastException c)
            {

            }
        }

        private void btnKaartnummerSearch_Click(object sender, RoutedEventArgs e)
        {
            // datagrid gezinslid
            string kaartnummer = txtCard.Text;

            var gezinQuery = from g in db.gezins
                             where g.kringloopKaartnummer == kaartnummer
                             where g.actief == 1
                             select g;

            if (gezinQuery.Count() > 0)
            {
                foreach (var gezins in gezinQuery)
                {
                    var FamilyMemberIdQuery = from gl in db.gezinslids
                                           where gl.gezin_id == gezins.id
                                           where gl.actief == 1
                                           select gl;

                    if (gezins.kringloopKaartnummer == kaartnummer)
                    {
                        CardNumberResult = gezins.kringloopKaartnummer;
                        dgGezinslid.ItemsSource = FamilyMemberIdQuery;
                    }
                }
            }

            else if (gezinQuery.Count() == 0)
            {

                messageboxes.kaartOfgezinslidNietActief kaartOfgezinslid = new messageboxes.kaartOfgezinslidNietActief();
                dgAfhaling.ItemsSource = null;
                kaartOfgezinslid.Show();                            
                            
            }

            //datagrid afhaling

            var kaartOphaalQuery = from gl in db.gezinslids
                                   join g in db.gezins on gl.gezin_id equals g.id
                                   where gl.voornaam == txtFirstName.Text
                                   select g;

            foreach (var kaart in kaartOphaalQuery)
            {
                txtCard.Text = kaart.kringloopKaartnummer;
                Familyid = kaart.id;
            }

            var glidQuery = from gl in db.gezinslids
                            where gl.gezin_id == Familyid
                            select gl;

            foreach (var glid in glidQuery)
            {
                FamilyMemberid = glid.id;
            }

            var afhalingQuery = from a in db.afhalings
                                where a.gezinslid_id == FamilyMemberid
                                select a;

            dgAfhaling.ItemsSource = afhalingQuery;
        }

        private void btnAfhaling_Click(object sender, RoutedEventArgs e)
        {
            // als de vakjes niet leeg zijn gaat het proberen een afhaling te maken
            if (txtCard.Text != "" && txtFirstName.Text != "")
            {                
                coolDown = DateTime.Today;
                
                var FamilyidQuery = from g in db.gezins
                                   where g.kringloopKaartnummer == txtCard.Text
                                   select g;
                foreach (var gid in FamilyidQuery)
                {
                    Familyid = gid.id;
                }

                var gezinslidQuery = from gl in db.gezinslids
                                     where gl.voornaam == txtFirstName.Text
                                     where gl.gezin_id == Familyid
                                     select gl;

                foreach (var glid in gezinslidQuery)
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

                // checkt als het eerder deze maand gedaan is
                if (onceMonthQuery.Count() == 0)
                {
                    foreach (var Familyid in FamilyidQuery)
                    {
                        var FamilyMemberIdQuery = from gl in db.gezinslids
                                               where gl.gezin_id == Familyid.id
                                               where gl.voornaam == txtFirstName.Text
                                               select gl;

                        afhaling afhalings = new afhaling();
                        afhalings.datum = DateTime.Today;
                        

                        foreach (var FamilyMemberId in FamilyMemberIdQuery)
                        {
                            afhalings.gezinslid_id = FamilyMemberId.id;
                        }
                        db.afhalings.InsertOnSubmit(afhalings);
                    }
                    db.SubmitChanges();


                    var kaartOphaalQuery = from gl in db.gezinslids
                                           join g in db.gezins on gl.gezin_id equals g.id
                                           where gl.voornaam == txtFirstName.Text
                                           select g;

                    foreach (var kaart in kaartOphaalQuery)
                    {
                        txtCard.Text = kaart.kringloopKaartnummer;
                        Familyid = kaart.id;
                    }

                    var glidQuery = from gl in db.gezinslids
                                    where gl.gezin_id == Familyid
                                    select gl;

                    foreach (var glid in glidQuery)
                    {
                        FamilyMemberid = glid.id;
                    }

                    var afhalingQuery = from a in db.afhalings
                                        where a.gezinslid_id == FamilyMemberid
                                        select a;

                    dgAfhaling.ItemsSource = afhalingQuery;

                    messageboxes.wMessageAfhaling wMessageAfhaling = new messageboxes.wMessageAfhaling();
                    wMessageAfhaling.Show();
                    
                    TextBoxReset();
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

        public void TextBoxReset()
        {
            txtCard.Text = null;
            txtFirstName.Text = null;
        }
        public void NewCard()
        {            
            wKlant klant = new wKlant();
            klant.Show();
            this.Close();
        }
    }
}
