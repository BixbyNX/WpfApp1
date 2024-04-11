using MySqlX.XDevAPI;
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
using static System.Net.WebRequestMethods;

namespace WpfApp1
{
    public partial class Window2 : Window
    {

        
        public Window2()
        {
            InitializeComponent();
        }

        public object otp { get; private set; }

        private void btnSubmitOTP_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Verify_Click(object sender, RoutedEventArgs e)
        {
            if (otp.ToString().Equals(otpverify.Text))
            {
                MessageBox.Show("Email verified");
            }
            else
            {
                MessageBox.Show("Incorrect OTP");
            }
        }
    }
}
