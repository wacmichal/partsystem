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
using MySql.Data.MySqlClient;
namespace partsystem
{
    /// <summary>  
   
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        //variables
      MySqlConnection connection;
        public LoginWindow()
        {
            InitializeComponent();
            ConnectDB();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TryLogin(LoginBox.Text, PassBox.Password))
                {
                    ChooseWindow choosewindow = new ChooseWindow();
                    choosewindow.Show();
                    this.Close();
                }
                else
                    MessageBox.Show("Błędne dane");
            }
            catch(Exception ex)
            {

            }
        }
        private void ConnectDB()
        {
            try
            {
                connection = new MySqlConnection("SERVER = localhost; " + "DATABASE = partsystem; " + "UID = root; " + "PASSWORD=1; " + "sslmode = none; ");
                connection.Open();
             
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                this.Close();
            }
        }

        private bool TryLogin(string user, string password)
        {
            
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * from users where login=@user and password=@pass";
            cmd.Parameters.AddWithValue("@user", user);
            cmd.Parameters.AddWithValue("@pass", password);
            cmd.Connection = connection;
            MySqlDataReader login = cmd.ExecuteReader();
            if (login.Read())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

      
    }
}
