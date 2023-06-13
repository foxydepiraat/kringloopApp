﻿using System;
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

            dgFamily.ItemsSource = db.gezins;

            dgFamilyMembers.ItemsSource = db.gezinslids;

            dgAfhaling.ItemsSource = db.afhalings;
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
            try
            {

                afhalingen = (afhaling)dgAfhaling.SelectedItem;
                dpRapportDatum.Text = afhalingen.datum.ToString();

            }
            catch (Exception a)
            {

            }
        }

        private void dgFamily_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                Family = (gezin)dgFamily.SelectedItem;
                txtCard.Text = Family.kringloopKaartnummer;

                var dgFamilyQuery = from g in db.gezins
                                    where g.kringloopKaartnummer == txtCard.Text
                                    select g;

                foreach (var g in dgFamilyQuery)
                {
                    var dgFamilyMemberQuery = from gl in db.gezinslids
                                              where gl.gezin_id == g.id
                                              select gl;

                    dgFamilyMembers.ItemsSource = dgFamilyMemberQuery;
                }
                
            }
            catch (Exception b)
            {

            }
        }

        private void dgFamilyMembers_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                FamilyMember = (gezinslid)dgFamilyMembers.SelectedItem;
                txtFirstName.Text = FamilyMember.voornaam;

                var dgFamilyQuery = from g in db.gezins
                                    where g.kringloopKaartnummer == txtCard.Text
                                    select g;

                foreach (var g in dgFamilyQuery)
                {
                    var dgFamilyMemberQuery = from gl in db.gezinslids
                                              where gl.gezin_id == g.id
                                              select gl;

                    foreach (var gl in dgFamilyMemberQuery)
                    {
                        var dgPickUpQuery = from a in db.afhalings
                                            where a.gezinslid_id == gl.id
                                            select a;

                        dgAfhaling.ItemsSource = dgPickUpQuery;
                    }

                }
            }
            catch (Exception c)
            {

            }
        }

        //Search result from entered data 
        private void btnKaartnummerSearch_Click(object sender, RoutedEventArgs e)
        {
            if (dpRapportDatum.Text == "")
            {
                pickedDate = DateTime.Today;
            }
            //search with the entered data from the if + data
            if (txtCard.Text != "" && txtFirstName.Text != "")
            {
                var GezinKaartQuery = from g in db.gezins
                                      where g.kringloopKaartnummer == txtCard.Text
                                      select g;

                foreach (var gid in GezinKaartQuery)
                {
                    Familyid = gid.id;
                }

                var FamilyMemberidQuery = from gl in db.gezinslids
                                          where gl.voornaam == txtFirstName.Text
                                          where gl.gezin_id == Familyid
                                          select gl;

                dgFamilyMembers.ItemsSource = FamilyMemberidQuery;

                foreach (var glid in FamilyMemberidQuery)
                {
                    FamilyMemberid = glid.id;
                }

                var afhalingQuery = from a in db.afhalings
                                    where a.gezinslid_id == FamilyMemberid
                                    select a;

                dgAfhaling.ItemsSource = afhalingQuery;

                foreach (var afhaling in afhalingQuery)
                {
                    DateTime Date = Convert.ToDateTime(afhaling.datum);
                    int dateYear = Date.Year;
                    int dateMonth = Date.Month;
                }

                var monthQuery = from a in db.afhalings
                                 where a.gezinslid_id == FamilyMemberid
                                 where a.datum == pickedDate
                                 select a;

                if (monthQuery.Count() > 0)
                {
                    dgAfhaling.ItemsSource = monthQuery;
                }
                else
                {
                    dgAfhaling.ItemsSource = afhalingQuery;
                }
            }
            //search for data was has been enetered what is in the if
            else if (txtCard.Text != "")
            {
                var gezinQuery = from g in db.gezins
                                 where g.kringloopKaartnummer == txtCard.Text
                                 select g;

                foreach (var gezin in gezinQuery)
                {
                    Familyid = gezin.id;
                }

                var gezinslidQuery = from gl in db.gezinslids
                                     where gl.gezin_id == Familyid
                                     select gl;

                foreach (var gezinslid in gezinslidQuery)
                {
                    FamilyMemberid = gezinslid.id;
                    txtFirstName.Text = gezinslid.voornaam;
                }

                dgFamilyMembers.ItemsSource = gezinslidQuery;

                var afhalingQuery = from a in db.afhalings
                                    where a.gezinslid_id == FamilyMemberid
                                    select a;

                foreach (var afhaling in afhalingQuery)
                {
                    DateTime Date = Convert.ToDateTime(afhaling.datum);
                    int dateYear = Date.Year;
                    int dateMonth = Date.Month;
                }

                var SeacrhMonthYearQuery = from a in db.afhalings
                                           where a.gezinslid_id == FamilyMemberid
                                           where dateYear == pickedDate.Year
                                           where dateMonth == pickedDate.Month
                                           select a;

                if (SeacrhMonthYearQuery.Count() > 0)
                {
                    dgAfhaling.ItemsSource = SeacrhMonthYearQuery;
                }
                else
                {
                    dgAfhaling.ItemsSource = afhalingQuery;
                }
            }
            else
            {
                messageboxes.legenVakjes legenVakjes = new messageboxes.legenVakjes();
                legenVakjes.Show();
                dgFamily.ItemsSource = db.gezins;
                dgFamilyMembers.ItemsSource = null;
                dgAfhaling.ItemsSource = null;
            }
        }

        private void dpRapportDatum_CalendarClosed(object sender, RoutedEventArgs e)
        {
            if (dpRapportDatum.SelectedDate != null)
            {
                pickedDate = Convert.ToDateTime(dpRapportDatum.SelectedDate);
            }
        }

        //when pressed it will search data on year
        private void btnYear_Click(object sender, RoutedEventArgs e)
        {
            if (pickedDate == null)
            {
                pickedDate = DateTime.Today;
            }
            //checks if data  has been entered then seacrh for this + the year that has been entered
            if (txtCard.Text != "" && txtFirstName.Text != "")
            {
                var QueryFamilyid = from g in db.gezins
                                    where g.kringloopKaartnummer == txtCard.Text
                                    select g;

                foreach (var g in QueryFamilyid)
                {
                    Familyid = g.id;
                }

                var QueryGezinslid = from gl in db.gezinslids
                                     where gl.gezin_id == Familyid
                                     where gl.voornaam == txtFirstName.Text
                                     select gl;

                foreach (var gl in QueryGezinslid)
                {
                    FamilyMemberid = gl.id;
                }

                var QueryAfhalingYear = from a in db.afhalings
                                        where a.gezinslid_id == FamilyMemberid
                                        where a.datum.Value.Year == pickedDate.Year
                                        select a;

                dgAfhaling.ItemsSource = QueryAfhalingYear.ToList();
            }
            // if txtFirstname has not been enttered  then  search for all data that are equal to the enetered data
            else if (txtCard.Text != "")
            {
                var QueryGezin = from g in db.gezins
                                 where g.kringloopKaartnummer == txtCard.Text
                                 select g;

                foreach (var g in QueryGezin)
                {
                    Familyid = g.id;
                }

                var QueryGezinslid = from gl in db.gezinslids
                                     where gl.gezin_id == Familyid
                                     select gl;

                foreach (var gl in QueryGezinslid)
                {
                    FamilyMemberid = gl.id;
                }

                var QueryAfhalingYear = from a in db.afhalings
                                        join gl in db.gezinslids on a.gezinslid_id equals gl.id
                                        where gl.gezin_id == Familyid
                                        where a.datum.Value.Year == pickedDate.Year
                                        select a;

                dgAfhaling.ItemsSource = QueryAfhalingYear.ToList();
            }
        }

        //when pressed it will search data on month
        private void btnMonth_Click(object sender, RoutedEventArgs e)
        {
            if (pickedDate == null)
            {
                pickedDate = DateTime.Today;
            }
            //checks if data  has been entered then seacrh for this + the year that has been entered
            if (txtCard.Text != "" && txtFirstName.Text != "")
            {
                var QueryFamilyid = from g in db.gezins
                                    where g.kringloopKaartnummer == txtCard.Text
                                    select g;

                foreach (var g in QueryFamilyid)
                {
                    Familyid = g.id;
                }

                var QueryFamilyMemberid = from gl in db.gezinslids
                                          where gl.gezin_id == Familyid
                                          where gl.voornaam == txtFirstName.Text
                                          select gl;

                foreach (var gl in QueryFamilyMemberid)
                {
                    FamilyMemberid = gl.id;
                }

                var QueryAfhalingMonth = from a in db.afhalings
                                         where a.gezinslid_id == FamilyMemberid
                                         where a.datum.Value.Year == pickedDate.Year
                                         where a.datum.Value.Month == pickedDate.Month
                                         select a;

                dgAfhaling.ItemsSource = QueryAfhalingMonth.ToList();

            }
            // if txtFirstname has not been enttered then  search for all data that are equal to the entered data
            else if (txtCard.Text != "")
            {
                var QueryFamilyid = from g in db.gezins
                                    where g.kringloopKaartnummer == txtCard.Text
                                    select g;

                foreach (var g in QueryFamilyid)
                {
                    Familyid = g.id;
                }

                var QueryFamilyMemberid = from gl in db.gezinslids
                                          where gl.gezin_id == Familyid
                                          select gl;

                foreach (var gl in QueryFamilyMemberid)
                {
                    FamilyMemberid = gl.id;
                }

                var QueryAfhalingMonth = from a in db.afhalings
                                         join gl in db.gezinslids on a.gezinslid_id equals gl.id
                                         where gl.gezin_id == Familyid
                                         where a.datum.Value.Year == pickedDate.Year
                                         where a.datum.Value.Month == pickedDate.Month
                                         select a;

                dgAfhaling.ItemsSource = QueryAfhalingMonth.ToList();

            }
            else
            {
                var queryGezin = from g in db.gezins
                                 select g;

                dgFamily.ItemsSource = queryGezin;

                var queryAfhaling = from a in db.afhalings
                                    select a;

                dgAfhaling.ItemsSource = queryAfhaling;
            }
        }
        public void QuerySorten()
        {
            



            
        }
    }
}
