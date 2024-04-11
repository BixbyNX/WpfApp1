using System;
using System.Net.Mail;
using System.Net;
using System.Windows;
using System.Xml.Linq;
using MySqlX.XDevAPI;
namespace WpfApp1
{
    public partial class Window1 : Window
    {

        private string username;
        private string email;
        private string branchService;
        private string personnelType;
        private string contactNo;
        private string serialNumber;
        private string password;

        public Window1()
        {
            InitializeComponent();
            verificationTAB.Visibility = Visibility.Hidden;
            notMatch.Visibility = Visibility.Hidden;
            inputconfirm.Visibility = Visibility.Hidden;
            inputpass.Visibility = Visibility.Hidden;
            incomplete.Visibility = Visibility.Hidden;
        }

        Random rand = new Random();
        public int otp;
        private async void btnRegister_Click(object sender, RoutedEventArgs e)
        {

            string username = fname.Text;
            string email = emailadd.Text;
            string branchService = branch.Text;
            string personnelType = personneltype.Text;
            string contactNo = contact.Text;
            string serialNumber = serialIDNo.Text;
            string password = passwordb.Password;

            try
            {
              

                if (passwordb.Password == "" && confirmpass.Password == "")
                {
                    inputconfirm.Visibility = Visibility.Visible;
                    inputpass.Visibility = Visibility.Visible;
                    notMatch.Visibility = Visibility.Hidden;
                    incomplete.Visibility = Visibility.Hidden;
                }
                else if (fname.Text == "" || emailadd.Text == "" || branch.Text == "" ||
                          personneltype.Text == "" || contact.Text == "" || serialIDNo.Text == "")
                {
                    incomplete.Visibility = Visibility.Visible;
                    inputconfirm.Visibility = Visibility.Hidden;
                    notMatch.Visibility = Visibility.Hidden;
                    inputpass.Visibility = Visibility.Hidden;
                }
                else if (confirmpass.Password == "")
                {
                    inputconfirm.Visibility = Visibility.Visible;
                    notMatch.Visibility = Visibility.Hidden;
                    inputpass.Visibility = Visibility.Hidden;
                    incomplete.Visibility = Visibility.Hidden;
                }
                else if (passwordb.Password == "")
                {
                    inputpass.Visibility = Visibility.Visible;
                    inputconfirm.Visibility = Visibility.Hidden;
                    notMatch.Visibility = Visibility.Hidden;
                    incomplete.Visibility = Visibility.Hidden;
                }
                else if (passwordb.Password != confirmpass.Password)
                {
                    notMatch.Visibility = Visibility.Visible;
                    inputpass.Visibility = Visibility.Hidden;
                    inputconfirm.Visibility = Visibility.Hidden;
                    incomplete.Visibility = Visibility.Hidden;
                }
                else
                {
                    string client = emailadd.Text;
                    verificationTAB.Visibility = Visibility.Visible;
                    notMatch.Visibility = Visibility.Hidden;
                    inputpass.Visibility = Visibility.Hidden;
                    inputconfirm.Visibility = Visibility.Hidden;
                    incomplete.Visibility = Visibility.Hidden;

                    Console.WriteLine(client);

                    try
                    {
                        otp = rand.Next(100000, 999999);

                        MailMessage msg = new MailMessage();
                        msg.From = new MailAddress("encryptalk@outlook.com");
                        msg.To.Add(emailadd.Text);
                        msg.Subject = ("Activate your Encryptalk account.");
                        msg.Body = ("Hi, " + fname.Text + "!" + "\n" + "\nHere's your code to activate your account: " + otp.ToString() + ". Thank you for using Encryptalk, Happy chatting!");

                        SmtpClient smt = new SmtpClient();
                        smt.Host = "smtp-mail.outlook.com";
                        NetworkCredential ntcd = new NetworkCredential();
                        ntcd.UserName = "encryptalk@outlook.com";
                        ntcd.Password = "pritongman0k!";
                        smt.Credentials = ntcd;
                        smt.EnableSsl = true;
                        smt.Port = 587;
                        smt.Send(msg);

                        emailShow.Text = (client);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred during registration: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
} catch (Exception ex)
            {
                MessageBox.Show("An error occurred during registration: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btnPrivacyPolicy_Click(object sender, RoutedEventArgs e)
        {
            PrivacyPolicyWindow window = new PrivacyPolicyWindow();
            this.Visibility = Visibility.Hidden;
            window.Show();
        }

        private async void btnSubmitOTP_Click(object sender, object e)
        {
            if (otp.ToString().Equals(otpverify.Text))
            {
                try
                {
                    AuthServer server = new AuthServer();
                    string response = await server.Register(username, email, branchService, personnelType, contactNo, serialNumber, password);

                    Console.WriteLine(response);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Incorrect OTP");
            }

            MainWindow mw = new MainWindow();
            this.Visibility= Visibility.Hidden;
            mw.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            this.Visibility = Visibility.Hidden;
            mw.Show();
        }
    }
}