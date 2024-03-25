using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net;
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
using MySql.Data.MySqlClient;

namespace WpfApp1
{
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            verificationTAB.Visibility = Visibility.Hidden;
        }

        Random rand = new Random();
        public int otp;
        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            verificationTAB.Visibility = Visibility.Visible;
            string client = emailadd.Text;

            try
            {
                string connstring = "server=localhost; uid=root; pwd=; database=chatifyz";
                MySqlConnection con = new MySqlConnection(connstring);

                otp = rand.Next(100000, 999999);

                MailMessage msg = new MailMessage();
                msg.From = new MailAddress("raengbiag@outlook.com");
                msg.To.Add(emailadd.Text);
                msg.Subject = ("Verify your Encryptalk account.");
                msg.Body = ("Hi, " + fname.Text + "!" + "\n" + "\nHere's your verification code: " + otp.ToString() + ". Thank you for using Encryptalk, Happy chatting!");

                SmtpClient smt = new SmtpClient();
                smt.Host = "smtp-mail.outlook.com";
                System.Net.NetworkCredential ntcd = new NetworkCredential();
                ntcd.UserName = "raengbiag@outlook.com";
                ntcd.Password = "saitama122620";
                smt.Credentials = ntcd;
                smt.EnableSsl = true;
                smt.Port = 587;
                smt.Send(msg);

                con.Close();

                emailShow.Text = (client);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void btnPrivacyPolicy_Click(object sender, RoutedEventArgs e)
        {
            PrivacyPolicyWindow window = new PrivacyPolicyWindow();
            this.Visibility = Visibility.Hidden;
            window.Show();
        }

        private void btnSubmitOTP_Click(object sender, object e)
        {
            if (otp.ToString().Equals(otpverify.Text))
            {
                try
                {
                    string connstring = "server=localhost; uid=root; pwd=; database=chatifyz";
                    MySqlConnection con = new MySqlConnection(connstring);

                    con.Open();

                    string username = fname.Text;
                    string email = emailadd.Text;
                    string branchService = branch.Text;
                    string personnelType = personneltype.Text;
                    string contactNo = contact.Text;
                    string serialNumber = serialIDNo.Text;
                    string password = passwordb.Password;

                    string query = "INSERT INTO users (Name, Email, Branch_of_Service, Personnel_Type, Contact_No, Serial_ID_No, Password)"
                        + "VALUES (@username, @email, @branchService, @personnelType, @contactNo, @serialNumber, @password)";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@branchService", branchService);
                    cmd.Parameters.AddWithValue("@personnelType", personnelType);
                    cmd.Parameters.AddWithValue("@contactNo", contactNo);
                    cmd.Parameters.AddWithValue("@serialNumber", serialNumber);
                    cmd.Parameters.AddWithValue("@password", password);

                    int rowsAffected = cmd.ExecuteNonQuery();
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