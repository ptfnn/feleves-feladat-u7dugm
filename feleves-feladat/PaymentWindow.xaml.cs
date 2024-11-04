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

namespace feleves_feladat
{
    public partial class PaymentWindow : Window
    {
        public PaymentWindow()
        {
            InitializeComponent();
        }
        private void PayButton_Click(object sender, RoutedEventArgs e)
        {
            string name = Name.Text;
            string cardNumber = CardNumber.Text;
            string pin = PINCode.Password;

            SuccesWindow successWindow = new SuccesWindow();
        }
        private void OpenSuccesWindow(object sender, RoutedEventArgs e)
        {
            SuccesWindow succesWindow = new SuccesWindow();
            succesWindow.ShowDialog();
        }
    }
}
