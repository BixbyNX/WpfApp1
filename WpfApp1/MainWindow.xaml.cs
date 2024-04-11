using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using WebSocketSharp;
using System.Net;
using static WpfApp1.AuthServer;


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
        private WebSocket ws;
        public MainWindow()
        {
            InitializeComponent();
            
            btnForgot.Visibility = Visibility.Hidden;
            forgotTAB.Visibility = Visibility.Hidden;
            txtinvalid.Visibility = Visibility.Hidden;
            txtTimerpass.Visibility = Visibility.Hidden;
            txtMsgErr.Visibility = Visibility.Hidden;
            warning.Visibility = Visibility.Hidden;
            warningMessage.Visibility = Visibility.Hidden;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
        }
        private async void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string serialNumber = txtboxusername.Text;
                string inputPassword = passbox.Password;

                AuthServer server = new AuthServer();
                LoginResponse response = await server.Login(serialNumber, inputPassword);

                Console.WriteLine($"Response Status Code: {response.StatusCode}");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    MessageDashboard window = new MessageDashboard();
                    this.Visibility = Visibility.Hidden;
                    window.Show();
                    //tama credentials
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    passbox.Clear();
                    warning.Visibility = Visibility.Visible;
                    btnForgot.Visibility = Visibility.Visible;
                    txtinvalid.Visibility = Visibility.Visible;
                    txtMsgErr.Visibility = Visibility.Hidden;
                    attempt++;
                    ErrAttmpt();
                }
                else if (txtboxusername.Text == "" || passbox.Password == "")
                {
                    txtMsgErr.Visibility = Visibility.Visible;
                    txtinvalid.Visibility = Visibility.Hidden;
                    txtboxusername.Clear();
                    passbox.Clear();
                    ErrAttmpt();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void warning_MouseEnter(object sender, MouseEventArgs e)
        {
            warningMessage.Visibility = Visibility.Visible; 
        }

        private void warning_MouseLeave(object sender, MouseEventArgs e)
        {
            warningMessage.Visibility = Visibility.Hidden;
        }

        private void toggleButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
