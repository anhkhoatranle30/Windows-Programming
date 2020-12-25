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

namespace Cake_Shop
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

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void homeRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            homeGrid.Visibility = Visibility.Visible;
        }

        private void addRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            addGrid.Visibility = Visibility.Visible;
        }

        private void chartRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            chartGrid.Visibility = Visibility.Visible;
        }

        private void settingRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            settingGrid.Visibility = Visibility.Visible;
        }

        private void aboutRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            aboutGrid.Visibility = Visibility.Visible;
        }

        private void homeRadioButton_Unchecked(object sender, RoutedEventArgs e)
        {
            homeGrid.Visibility = Visibility.Collapsed;
        }

        private void addRadioButton_Unchecked(object sender, RoutedEventArgs e)
        {
            addGrid.Visibility = Visibility.Collapsed;
        }

        private void chartRadioButton_Unchecked(object sender, RoutedEventArgs e)
        {
            chartGrid.Visibility = Visibility.Collapsed;
        }

        private void settingRadioButton_Unchecked(object sender, RoutedEventArgs e)
        {
            settingGrid.Visibility = Visibility.Collapsed;
        }

        private void aboutRadioButton_Unchecked(object sender, RoutedEventArgs e)
        {
            aboutGrid.Visibility = Visibility.Collapsed;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            homeRadioButton.IsChecked = true;
            badgedCart.Badge = null;
            moneyTextBlock1.DataContext = "6270000";
        }

        private void menuToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            Grid.SetZIndex(menuGrid, 99);
        }

        private void menuToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            Grid.SetZIndex(menuGrid, 1);
        }

        private void mainGrid_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            menuToggleButton.IsChecked = false;
        }

        private void cakeButton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("nhan vao nut");
        }

        private void addToCartButton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("nhan vao nut them vao gio");
        }
    }
}