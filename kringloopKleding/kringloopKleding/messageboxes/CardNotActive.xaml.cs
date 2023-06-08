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
using System.Windows.Shapes;

namespace kringloopKleding.messageboxes
{
    /// <summary>
    /// Interaction logic for CardAlreadyExist.xaml
    /// </summary>
    public partial class CardNotActive : Window
    {

        private MainWindow main;
        public bool newCard;
        public CardNotActive(MainWindow main)
        {
            InitializeComponent();
            this.main = main;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnKaartAdd_Click(object sender, RoutedEventArgs e)
        {
            main.NewCard();
            Close();
        }


    }
}
