using Cake_Shop.DAO;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Configuration;
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
using LiveCharts;

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

        public Func<ChartPoint, string> PointLabel { set; get; }

        private bool isBackToDetail = false;

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void homeRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            homeGrid.Visibility = Visibility.Visible;
            cartGrid.Visibility = Visibility.Collapsed;
            detailCakeGrid.Visibility = Visibility.Collapsed;
            gridName.Text = "Trang Chủ";
        }

        private void addCakeRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            addCakeGrid.Visibility = Visibility.Visible;
            cartGrid.Visibility = Visibility.Collapsed;
            detailCakeGrid.Visibility = Visibility.Collapsed;
            gridName.Text = "Thêm sản phẩm";

            AddGridCategoryComboBox.ItemsSource = CategoryDAOSQLServer.GetAll();
        }

        private void chartRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            chartGrid.Visibility = Visibility.Visible;
            cartGrid.Visibility = Visibility.Collapsed;
            detailCakeGrid.Visibility = Visibility.Collapsed;
            gridName.Text = "Thống kê";
        }

        private void settingRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            settingGrid.Visibility = Visibility.Visible;
            cartGrid.Visibility = Visibility.Collapsed;
            detailCakeGrid.Visibility = Visibility.Collapsed;
            gridName.Text = "Cài đặt";
        }

        private void aboutRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            aboutGrid.Visibility = Visibility.Visible;
            cartGrid.Visibility = Visibility.Collapsed;
            detailCakeGrid.Visibility = Visibility.Collapsed;
            gridName.Text = "Chúng tôi";
        }

        private void homeRadioButton_Unchecked(object sender, RoutedEventArgs e)
        {
            homeGrid.Visibility = Visibility.Collapsed;
        }

        private void addCakeRadioButton_Unchecked(object sender, RoutedEventArgs e)
        {
            addCakeGrid.Visibility = Visibility.Collapsed;
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
            backButton.IsEnabled = false;
            cakeListView.ItemsSource = CakeDAOSQLServer.GetAll();
            RadioButtonGroupChoiceChip.ItemsSource = CategoryDAOSQLServer.GetAll();

            var value = ConfigurationManager.AppSettings["showSplashScreen"];
            var showSplash = bool.Parse(value);

            showSplashScreenCheckBox.IsChecked = showSplash;
            var test = RevenueDAOSQLServer.GetAllMonths();

            PointLabel = chartPoint =>
              string.Format("{0}đ", chartPoint.Y);
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
            isBackToDetail = true;
            backButton.IsEnabled = true;
            detailCakeGrid.Visibility = Visibility.Visible;
            homeGrid.Visibility = Visibility.Collapsed;

            if (int.Parse(quantityTextBlock.Text) == 1)
            {
                decButton.IsEnabled = false;
            }
        }

        private void addToCartButton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("nhan vao nut them vao gio");
        }

        private void cartButton_Click(object sender, RoutedEventArgs e)
        {
            gridName.Text = "Giỏ hàng";
            cartGrid.Visibility = Visibility.Visible;
            homeGrid.Visibility = Visibility.Collapsed;
            addCakeGrid.Visibility = Visibility.Collapsed;
            settingGrid.Visibility = Visibility.Collapsed;
            aboutGrid.Visibility = Visibility.Collapsed;
            detailCakeGrid.Visibility = Visibility.Collapsed;

            backButton.IsEnabled = true;
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            if (isBackToDetail && cartGrid.Visibility == Visibility.Visible)
            {
                cartGrid.Visibility = Visibility.Collapsed;
                detailCakeGrid.Visibility = Visibility.Visible;

                isBackToDetail = false;
            }
            else
            {
                cartGrid.Visibility = Visibility.Collapsed;
                detailCakeGrid.Visibility = Visibility.Collapsed;

                if (homeRadioButton.IsChecked == true)
                {
                    homeGrid.Visibility = Visibility.Visible;
                }
                else if (addCakeRadioButton.IsChecked == true)
                {
                    addCakeGrid.Visibility = Visibility.Visible;
                }
                else if (settingRadioButton.IsChecked == true)
                {
                    settingGrid.Visibility = Visibility.Visible;
                }
                else if (aboutRadioButton.IsChecked == true)
                {
                    aboutGrid.Visibility = Visibility.Visible;
                }

                backButton.IsEnabled = false;
            }
        }

        private void addCakeImgButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();

            if (fd.ShowDialog() == true)
            {
                ImageBrush myBrush = new ImageBrush();
                Image image = new Image();
                image.Source = new BitmapImage(
                    new Uri(fd.FileName));
                myBrush.ImageSource = image.Source;
                addCakeImgCard.Background = myBrush;
            }
        }

        private void Grid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            menuToggleButton.IsChecked = false;
        }

        private void RadioButtonGroupChoiceChip_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listbox = sender as ListBox;
            var selectedCategory = listbox.SelectedItem as CATEGORY;
            var catID = selectedCategory.CatID;

            cakeListView.ItemsSource = CakeDAOSQLServer.GetAllByCatID(catID);
        }

        private void calcFeeButton_Click(object sender, RoutedEventArgs e)
        {
            calcFeeTextBlock.Text = "50,000";
            calcFeeButton.Visibility = Visibility.Collapsed;
        }

        private void showSplashScreenCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var config = ConfigurationManager.OpenExeConfiguration(
                ConfigurationUserLevel.None);
            config.AppSettings.Settings["ShowSplashScreen"].Value = "true";
            config.Save(ConfigurationSaveMode.Minimal);
        }

        private void showSplashScreenCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var config = ConfigurationManager.OpenExeConfiguration(
                ConfigurationUserLevel.None);
            config.AppSettings.Settings["ShowSplashScreen"].Value = "false";
            config.Save(ConfigurationSaveMode.Minimal);
        }

        private void decButton_Click(object sender, RoutedEventArgs e)
        {
            int num = int.Parse(quantityTextBlock.Text);
            num--;
            quantityTextBlock.Text = num.ToString();

            if (num == 1)
            {
                decButton.IsEnabled = false;
            }
        }

        private void incButton_Click(object sender, RoutedEventArgs e)
        {
            int num = int.Parse(quantityTextBlock.Text);
            num++;

            quantityTextBlock.Text = num.ToString();
            decButton.IsEnabled = true;
        }

        private void decButtonCart_Click(object sender, RoutedEventArgs e)
        {
            int num = int.Parse(quantityTextBlockCart.Text);
            num--;
            quantityTextBlockCart.Text = num.ToString();

            if (num == 1)
            {
                decButtonCart.IsEnabled = false;
            }
        }

        private void incButtonCart_Click(object sender, RoutedEventArgs e)
        {
            int num = int.Parse(quantityTextBlockCart.Text);
            num++;

            quantityTextBlockCart.Text = num.ToString();
            decButtonCart.IsEnabled = true;
        }

        private void minimizeWindowButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}