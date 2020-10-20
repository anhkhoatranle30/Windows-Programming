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

namespace Project_1_Food_Recipe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public delegate void MenuButton_Click(object sender, RoutedEventArgs e);

        public event MenuButton_Click MenuBtn_Click;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            addButton.Background = new SolidColorBrush(Color.FromArgb(192, 192, 192, 192));
        }

        private void homeButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void favoriteButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void settingButton_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}