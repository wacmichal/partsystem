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

namespace partsystem
{
    /// <summary>
    /// Logika interakcji dla klasy Window1.xaml
    /// </summary>
    public partial class ChooseWindow : Window
    {
        public ChooseWindow()
        {
            InitializeComponent();
        }
       private void _buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            AddPart AddPart = new AddPart();
            AddPart.Show();
            this.Close();
        }

        private void _buttonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void _buttonManage_Click(object sender, RoutedEventArgs e)
        {
            Status Status = new Status();
            Status.Show();
            this.Close();
        }
    }
    
}
