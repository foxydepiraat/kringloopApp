using System;
using System.Collections.Generic;
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
        private gezin Family;
        private gezinslid FamilyMember;
        private afhaling afhalingen;

        private int Familyid;
        private int FamilyMemberid;

        private DateTime pickedDate;
        private int dateYear;
        private int dateMonth;
        public wRapportage()
        {
            InitializeComponent();

            initializeComboboxes();

            txtBoxReset();
        }

        private void txtBoxReset()
        {
            txtResidence.Text = "";
            txtReason.Text = "";
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

        private void dgAfhaling_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            afhalingen = (afhaling)dgAfhaling.SelectedItem;
            dpRapportDatum.Text = afhalingen.datum.ToString();
        }

        private void dgFamily_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Family = (gezin)dgFamily.SelectedItem;
            txtResidence.Text = Family.Woonplaats;
            txtReason.Text = Family.reden;
            cbActiveCard.IsChecked = Convert.ToBoolean(Family.actief);

        }

        private void dgFamilyMembers_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            FamilyMember = (gezinslid)dgFamilyMembers.SelectedItem;
            cbActiveFamilyMember.IsChecked = Convert.ToBoolean(FamilyMember.actief);

        }



        private void dpRapportDatum_CalendarClosed(object sender, RoutedEventArgs e)
        {
            if (dpRapportDatum.SelectedDate != null)
            {
                pickedDate = Convert.ToDateTime(dpRapportDatum.SelectedDate);
            }
        }


        private void btnYear_Click(object sender, RoutedEventArgs e)
        {
            if (dpRapportDatum.Text == "")
            {
                pickedDate = DateTime.Today;
            }

            List<gezinslid> familyMembers = new List<gezinslid>();
            List<afhaling> PickUpList = new List<afhaling>();
            if (txtResidence.Text != "" && txtReason.Text != "")
            {
                var QueryFamilyid = from g in db.gezins
                                    where g.Woonplaats == txtResidence.Text
                                    where g.reden == txtReason.Text
                                    where g.actief == Convert.ToInt16(cbActiveCard.IsChecked)
                                    select g;

                dgFamily.ItemsSource = QueryFamilyid.ToList();

                foreach (var g in QueryFamilyid)
                {

                    var QueryGezinslid = from gl in db.gezinslids
                                         where gl.gezin_id == g.id
                                         where gl.actief == Convert.ToInt16(cbActiveFamilyMember.IsChecked)
                                         select gl;


                    foreach (var glid in QueryGezinslid)
                    {
                        if (glid is gezinslid familyMember)
                        {
                            familyMembers.Add(familyMember);
                        }

                    }
                    foreach (var gl in QueryGezinslid)
                    {
                        var QueryAfhalingYear = from a in db.afhalings
                                                where a.gezinslid_id == gl.id
                                                where a.datum.Value.Year == pickedDate.Year
                                                select a;

                        foreach (var a in QueryAfhalingYear)
                        {
                            if (a is afhaling PickUpLists)
                            {
                                PickUpList.Add(PickUpLists);
                            }
                        }

                    }

                }

                if (QueryFamilyid.Count() <= 0)
                {
                    txtBoxReset();
                }
            }
            else if( txtResidence.Text != "" && txtReason.Text == "")
            {
                var QueryFamilyid = from g in db.gezins
                                    where g.Woonplaats == txtResidence.Text
                                    where g.actief == Convert.ToInt16(cbActiveCard.IsChecked)
                                    select g;

                dgFamily.ItemsSource = QueryFamilyid.ToList();

                foreach (var g in QueryFamilyid)
                {

                    var QueryGezinslid = from gl in db.gezinslids
                                         where gl.gezin_id == g.id
                                         where gl.actief == Convert.ToInt16(cbActiveFamilyMember.IsChecked)
                                         select gl;


                    foreach (var glid in QueryGezinslid)
                    {
                        if (glid is gezinslid familyMember)
                        {
                            familyMembers.Add(familyMember);
                        }

                    }
                    foreach (var gl in QueryGezinslid)
                    {
                        var QueryAfhalingYear = from a in db.afhalings
                                                where a.gezinslid_id == gl.id
                                                where a.datum.Value.Year == pickedDate.Year
                                                select a;

                        foreach (var a in QueryAfhalingYear)
                        {
                            if (a is afhaling PickUpLists)
                            {
                                PickUpList.Add(PickUpLists);
                            }
                        }

                    }

                }

                if (QueryFamilyid.Count() <= 0)
                {
                    txtBoxReset();
                }
            }
            else if( txtResidence.Text == "" && txtReason.Text != "")
            {
                var QueryFamilyid = from g in db.gezins
                                    where g.reden == txtReason.Text
                                    where g.actief == Convert.ToInt16(cbActiveCard.IsChecked)
                                    select g;

                dgFamily.ItemsSource = QueryFamilyid.ToList();

                foreach (var g in QueryFamilyid)
                {

                    var QueryGezinslid = from gl in db.gezinslids
                                         where gl.gezin_id == g.id
                                         where gl.actief == Convert.ToInt16(cbActiveFamilyMember.IsChecked)
                                         select gl;


                    foreach (var glid in QueryGezinslid)
                    {
                        if (glid is gezinslid familyMember)
                        {
                            familyMembers.Add(familyMember);
                        }

                    }
                    foreach (var gl in QueryGezinslid)
                    {
                        var QueryAfhalingYear = from a in db.afhalings
                                                where a.gezinslid_id == gl.id
                                                where a.datum.Value.Year == pickedDate.Year
                                                select a;

                        foreach (var a in QueryAfhalingYear)
                        {
                            if (a is afhaling PickUpLists)
                            {
                                PickUpList.Add(PickUpLists);
                            }
                        }

                    }

                }

                if (QueryFamilyid.Count() <= 0 )
                {
                    txtBoxReset();
                }
            }
            else
            {
                txtBoxReset();
            }

            dgFamilyMembers.ItemsSource = familyMembers.ToList();
            dgAfhaling.ItemsSource = PickUpList.ToList();
        }

        private void btnMonth_Click(object sender, RoutedEventArgs e)
        {
            if(dpRapportDatum.Text == "")
            {
                pickedDate = DateTime.Today;
            }

            List<gezinslid> familyMembers = new List<gezinslid>();
            List<afhaling> PickUpList = new List<afhaling>();
            if (txtResidence.Text != "" && txtReason.Text != "")
            {
                var QueryFamilyid = from g in db.gezins
                                    where g.Woonplaats == txtResidence.Text
                                    where g.reden == txtReason.Text
                                    where g.actief == Convert.ToInt16(cbActiveCard.IsChecked)
                                    select g;

                dgFamily.ItemsSource = QueryFamilyid.ToList();

                foreach (var g in QueryFamilyid)
                {

                    var QueryGezinslid = from gl in db.gezinslids
                                         where gl.gezin_id == g.id
                                         where gl.actief == Convert.ToInt16(cbActiveFamilyMember.IsChecked)
                                         select gl;


                    foreach (var glid in QueryGezinslid)
                    {
                        if (glid is gezinslid familyMember)
                        {
                            familyMembers.Add(familyMember);
                        }

                    }
                    foreach (var gl in QueryGezinslid)
                    {
                        var QueryAfhalingYear = from a in db.afhalings
                                                where a.gezinslid_id == gl.id
                                                where a.datum.Value.Year == pickedDate.Year
                                                where a.datum.Value.Month == pickedDate.Month
                                                select a;

                        foreach (var a in QueryAfhalingYear)
                        {
                            if (a is afhaling PickUpLists)
                            {
                                PickUpList.Add(PickUpLists);
                            }
                        }

                    }

                }

                if (QueryFamilyid.Count() <= 0)
                {
                    txtBoxReset();
                }
            }
            else if (txtResidence.Text != "" && txtReason.Text == "")
            {
                var QueryFamilyid = from g in db.gezins
                                    where g.Woonplaats == txtResidence.Text
                                    where g.actief == Convert.ToInt16(cbActiveCard.IsChecked)
                                    select g;

                foreach (var g in QueryFamilyid)
                {

                    var QueryGezinslid = from gl in db.gezinslids
                                         where gl.gezin_id == g.id
                                         where gl.actief == Convert.ToInt16(cbActiveFamilyMember.IsChecked)
                                         select gl;

                    dgFamily.ItemsSource = QueryFamilyid.ToList();

                    foreach (var glid in QueryGezinslid)
                    {
                        if (glid is gezinslid familyMember)
                        {
                            familyMembers.Add(familyMember);
                        }

                    }
                    foreach (var gl in QueryGezinslid)
                    {
                        var QueryAfhalingYear = from a in db.afhalings
                                                where a.gezinslid_id == gl.id
                                                where a.datum.Value.Year == pickedDate.Year
                                                where a.datum.Value.Month == pickedDate.Month
                                                select a;

                        foreach (var a in QueryAfhalingYear)
                        {
                            if (a is afhaling PickUpLists)
                            {
                                PickUpList.Add(PickUpLists);
                            }
                        }

                    }

                }

                if (QueryFamilyid.Count() <= 0)
                {
                    txtBoxReset();
                }
            }
            else if (txtResidence.Text == "" && txtReason.Text != "")
            {
                var QueryFamilyid = from g in db.gezins
                                    where g.reden == txtReason.Text
                                    where g.actief == Convert.ToInt16(cbActiveCard.IsChecked)
                                    select g;

                dgFamily.ItemsSource = QueryFamilyid.ToList();

                foreach (var g in QueryFamilyid)
                {

                    var QueryGezinslid = from gl in db.gezinslids
                                         where gl.gezin_id == g.id
                                         where gl.actief == Convert.ToInt16(cbActiveFamilyMember.IsChecked)
                                         select gl;


                    foreach (var glid in QueryGezinslid)
                    {
                        if (glid is gezinslid familyMember)
                        {
                            familyMembers.Add(familyMember);
                        }

                    }
                    foreach (var gl in QueryGezinslid)
                    {
                        var QueryAfhalingYear = from a in db.afhalings
                                                where a.gezinslid_id == gl.id
                                                where a.datum.Value.Year == pickedDate.Year
                                                where a.datum.Value.Month == pickedDate.Month
                                                select a;

                        foreach (var a in QueryAfhalingYear)
                        {
                            if (a is afhaling PickUpLists)
                            {
                                PickUpList.Add(PickUpLists);
                            }
                        }

                    }

                }

                if (QueryFamilyid.Count() <= 0)
                {
                    txtBoxReset();
                }
            }
            else
            {
                txtBoxReset();
            }
            dgFamilyMembers.ItemsSource = familyMembers.ToList();
            dgAfhaling.ItemsSource = PickUpList.ToList();
        }
    }
}
