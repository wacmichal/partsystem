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
  
    public partial class AddPart : Window
    {

        List<ExisitingPart> PartListGlobal;
        private ExisitingPart part;
        bool PartAddedNotSaved;
        public AddPart()
        {
            InitializeComponent();
            Refresh();
        }
    
   



        private void ExistingPartsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
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
              //  searchTypeCB.SelectedIndex = part.type; DO ZROBIENIA!
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
            
        PartSystem.AddUpdate(PartIDTB.Text, NameTB.Text, DescriptionTB.Text, (ChangeTypeCB.SelectedItem as ComboBoxItem).Content.ToString(), ExtNameTB.Text, part.id);
        Refresh();


        }
   
        private void Refresh()
        {

            PartListGlobal = PartSystem.PartList();
            this.ExistingPartsDataGrid.ItemsSource = PartListGlobal;
            ExistingPartsDataGrid.Items.Refresh();
            this.Width = 800;
            ItemDetailsGrid.Visibility = Visibility.Hidden;
            PartAddedNotSaved = false;
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ExistingPartsDataGrid.ItemsSource = PartSystem.SearchInList(PartListGlobal, SearchBox.Text, (searchTypeCB.SelectedItem as ComboBoxItem).Content.ToString());

            ExistingPartsDataGrid.Items.Refresh();
           
        }

        private void searchTypeCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          
           }
    }
}