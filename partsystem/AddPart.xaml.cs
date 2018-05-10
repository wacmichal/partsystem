using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
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
namespace partsystem
{
    /// <summary>
    /// Logika interakcji dla klasy Window1.xaml
    /// </summary>
    class ExisitingPart
    {
        public int id { get; set; }
        public int part_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public string ext_name { get; set; }
    }
    public partial class AddPart : Window
    {
        MySqlConnection connection;
        List<ExisitingPart> PartListGlobal;
        private ExisitingPart part;
        bool PartAddedNotSaved;
        public AddPart()
        {
            InitializeComponent();
            UpdateParts();



            // MessageBox.Show(prts[1].id.ToString());

        }
        private void UpdateParts()
        {
            PartListGlobal = PartList();
            this.ExistingPartsDataGrid.ItemsSource = PartListGlobal;
        }
        private bool ConnectDB()
        {
            try
            {
                connection = new MySqlConnection("SERVER = localhost; " + "DATABASE = partsystem; " + "UID = root; " + "PASSWORD=1; " + "sslmode = none; ");
                connection.Open();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }


        private List<ExisitingPart> PartList()
        {
            var list = new List<ExisitingPart>();
            if (this.ConnectDB() == true)
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM parts", connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    list.Add(new ExisitingPart
                    {
                        id = int.Parse(dataReader["id"].ToString()),
                        part_id = int.Parse(dataReader["part_id"].ToString()),
                        name = dataReader["name"].ToString(),
                        description = dataReader["description"].ToString(),
                        type = dataReader["type"].ToString(),
                        ext_name = dataReader["ext_name"].ToString()
                    });
                }
                dataReader.Close();
                connection.Close();


                return list;
            }
            else
            {
                return list;
            }
        }


        private void ExistingPartsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //poszerzenie okna , wyswietlenie kolejno parametrow, przycisk zapisz/anuluj
            this.Width = 1200;
            ItemDetailsGrid.Visibility = Visibility.Visible;
            part = ExistingPartsDataGrid.SelectedItem as ExisitingPart;
            if (part != null)
            {
                if (part.part_id.ToString() != "0")
                    PartIDTB.Text = part.part_id.ToString();
                else
                    PartIDTB.Text = "";
                NameTB.Text = part.name;
                DescriptionTB.Text = part.description;
                TypeTB.Text = part.type;
                ExtNameTB.Text = part.ext_name;
            }
        }



        private void _CloseButton_Click(object sender, RoutedEventArgs e)
        {
            ChooseWindow ChooseWindow = new ChooseWindow();
            ChooseWindow.Show();
            this.Close();
        }

        private void AddPartButton_Click(object sender, RoutedEventArgs e)
        {
            if (!PartAddedNotSaved)
                PartListGlobal.Add(new ExisitingPart()
                    );

            ExistingPartsDataGrid.Items.Refresh();
            PartAddedNotSaved = true;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (part.id == 0)
                CreatePart();
            else
                UpdatePart();
        }
        private void UpdatePart() {
            if (this.ConnectDB() == true && part != null)
            {

                string cmdText = String.Format("UPDATE parts SET part_id = '{0}',name = '{1}', description = '{2}',type = '{3}', ext_name = '{4}' WHERE id = '{5}' ", PartIDTB.Text
                , NameTB.Text
                , DescriptionTB.Text
               , TypeTB.Text
               , ExtNameTB.Text, part.id);
                MySqlCommand cmd = new MySqlCommand(cmdText, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
                UpdateParts();
                ExistingPartsDataGrid.Items.Refresh();
                this.Width = 800;
                ItemDetailsGrid.Visibility = Visibility.Hidden;

            }
        }
        private void CreatePart()
        {


            if (this.ConnectDB() == true)
            {

                string cmdText = String.Format("INSERT INTO parts (part_id,name,description,type,ext_name) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}') ", PartIDTB.Text
                , NameTB.Text
                , DescriptionTB.Text
               , TypeTB.Text
               , ExtNameTB.Text);
                MySqlCommand cmd = new MySqlCommand(cmdText, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
                UpdateParts();
                ExistingPartsDataGrid.Items.Refresh();
                this.Width = 800;
                ItemDetailsGrid.Visibility = Visibility.Hidden;
                PartAddedNotSaved = false;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateParts();
            ExistingPartsDataGrid.Items.Refresh();
            this.Width = 800;
            ItemDetailsGrid.Visibility = Visibility.Hidden;
            PartAddedNotSaved = false;
        }
    }
}