using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
using MySql.Data.MySqlClient;
using System.Windows.Threading;
using System.Threading;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private DispatcherTimer timer;
        private TimeSpan remainingTime = TimeSpan.FromMinutes(1);
        private int attempt = 0;
        private int countAttmpt = 0;
        private int duration = 10;

        public MainWindow()
        {
            InitializeComponent();
            
            btnForgot.Visibility = Visibility.Hidden;
            forgotTAB.Visibility = Visibility.Hidden;
            txtinvalid.Visibility = Visibility.Hidden;
            txtTimerpass.Visibility = Visibility.Hidden;
            txtMsgErr.Visibility = Visibility.Hidden;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
        }
        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            string connstring = "server=localhost; uid=root; pwd=; database=chatifyz";
            MySqlConnection con = new MySqlConnection(connstring);

            try
            {
                con.Open();

                string serialNumber = txtboxusername.Text;
                string inputPassword = passbox.Password;

                string query = "SELECT COUNT(*) FROM users WHERE serial_ID_No = @serialNumber AND Password = @password";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@serialNumber", serialNumber);
                cmd.Parameters.AddWithValue("@password", inputPassword);

                int count = Convert.ToInt32(cmd.ExecuteScalar());

                if (count > 0)
                {
                    MessagingWindow window = new MessagingWindow();
                    this.Visibility = Visibility.Hidden;
                    window.Show();
                }
                else if (txtboxusername.Text == " " || passbox.Password == "")
                {
                    txtMsgErr.Visibility = Visibility.Visible;
                    txtinvalid.Visibility = Visibility.Hidden;
                    txtboxusername.Clear();
                    passbox.Clear(); 
                    ErrAttmpt();
                }
                else
                {
                    passbox.Clear();
                    btnForgot.Visibility = Visibility.Visible;
                    txtinvalid.Visibility = Visibility.Visible;
                    txtMsgErr.Visibility = Visibility.Hidden;
                    attempt++;
                    ErrAttmpt();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void StartCountdown()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            remainingTime = remainingTime.Subtract(TimeSpan.FromSeconds(1));

            if (remainingTime <= TimeSpan.Zero)
            {
                timer.Stop();
                txtboxusername.IsEnabled = true;
                passbox.IsEnabled = true;
                btnLogIn.Visibility = Visibility.Visible;
                txtTimerpass.Visibility = Visibility.Hidden;
            }
            else
            {
                txtTimerpass.Content = ("Login blocked, time remaining: " + remainingTime.ToString(@"ss") + ".");
            }
        }

        private void ErrAttmpt()    
        {
            if (attempt >= 3) // Check if attempts exceed or equal to 3
            {
                countAttmpt++; // Increment the count of attempts

                if (countAttmpt <= 1) // Allow only one count of attempts 
                {
                    txtboxusername.IsEnabled = false;
                    passbox.IsEnabled = false;
                    btnLogIn.Visibility = Visibility.Hidden;

                    StartCountdown();

                    txtTimerpass.Visibility = Visibility.Visible;
                    txtinvalid.Visibility = Visibility.Hidden;
                    txtMsgErr.Visibility = Visibility.Hidden;
                }
                else if (countAttmpt == 2) // After the first count of attempts
                {
                    MessageBoxResult drs = MessageBox.Show("Login attempts exceeded, please contact administrator", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Stop);
                    if (drs == MessageBoxResult.OK)
                    {
                        txtboxusername.IsEnabled = false;
                        passbox.IsEnabled = false;
                        btnLogIn.Visibility = Visibility.Hidden;
                        txtTimerpass.Visibility = Visibility.Hidden;
                        txtinvalid.Visibility = Visibility.Hidden;
                        txtMsgErr.Visibility = Visibility.Hidden;
                    }
                }
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            Window1 window = new Window1();
            this.Visibility = Visibility.Hidden;
            window.Show();
        }

        private void btnForgot_Click(object sender, RoutedEventArgs e)
        {
            forgotTAB.Visibility = Visibility.Visible;
        }

        private void btnForgotYes_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSubmitOTP_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnForgotNo_Click(object sender, RoutedEventArgs e)
        {
            forgotTAB.Visibility = Visibility.Hidden;
        }
    }
}
