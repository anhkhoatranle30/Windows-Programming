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
            homeButton.ClearValue(BackgroundProperty);
            favoriteButton.ClearValue(BackgroundProperty);
            settingButton.ClearValue(BackgroundProperty);
            homePressed.ClearValue(BackgroundProperty);
            favoritePressed.ClearValue(BackgroundProperty);
            settingPressed.ClearValue(BackgroundProperty);

            //addButton.Background = new SolidColorBrush(Color.FromArgb(192, 192, 192, 192));
            addPressed.Background = Brushes.Orange;
        }

        private void homeButton_Click(object sender, RoutedEventArgs e)
        {
            addButton.ClearValue(BackgroundProperty);
            favoriteButton.ClearValue(BackgroundProperty);
            settingButton.ClearValue(BackgroundProperty);
            addPressed.ClearValue(BackgroundProperty);
            favoritePressed.ClearValue(BackgroundProperty);
            settingPressed.ClearValue(BackgroundProperty);

            //homeButton.Background = new SolidColorBrush(Color.FromArgb(192, 192, 192, 192));
            homePressed.Background = Brushes.Orange;
        }

        private void favoriteButton_Click(object sender, RoutedEventArgs e)
        {
            addButton.ClearValue(BackgroundProperty);
            homeButton.ClearValue(BackgroundProperty);
            settingButton.ClearValue(BackgroundProperty);
            addPressed.ClearValue(BackgroundProperty);
            homePressed.ClearValue(BackgroundProperty);
            settingPressed.ClearValue(BackgroundProperty);

            //favoriteButton.Background = new SolidColorBrush(Color.FromArgb(192, 192, 192, 192));
            favoritePressed.Background = Brushes.Orange;
        }

        private void settingButton_Click(object sender, RoutedEventArgs e)
        {
            addButton.ClearValue(BackgroundProperty);
            homeButton.ClearValue(BackgroundProperty);
            favoriteButton.ClearValue(BackgroundProperty);
            addPressed.ClearValue(BackgroundProperty);
            homePressed.ClearValue(BackgroundProperty);
            favoritePressed.ClearValue(BackgroundProperty);

            //settingButton.Background = new SolidColorBrush(Color.FromArgb(192, 192, 192, 192));
            settingPressed.Background = Brushes.Orange;
        }
    }
}