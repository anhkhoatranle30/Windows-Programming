﻿using Cake_Shop.DAO;
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
using Cake_Shop.Business;
using System.ComponentModel;

namespace Cake_Shop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BindingList<CartItem> _cartList;
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

            _cartList = CartItemDAO.CreateList();

            cakeListView.ItemsSource = CakeDAOSQLServer.GetAll();
            RadioButtonGroupChoiceChip.ItemsSource = CategoryDAOSQLServer.GetAll();

            var value = ConfigurationManager.AppSettings["showSplashScreen"];
            var showSplash = bool.Parse(value);

            showSplashScreenCheckBox.IsChecked = showSplash;

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

            var dataContext = ((Button)sender).DataContext;
            var cakeIDSelected = ((CAKE)dataContext).CakeID;
            var cakeSelected = CakeDAOSQLServer.GetByID(cakeIDSelected);

            detailCakeID.Text = cakeIDSelected.ToString();
            detailCakeImage.DataContext = cakeSelected.CakeID;
            detailCakeName.Text = cakeSelected.CakeName;
            detailCakePrice.DataContext = cakeSelected.Price.ToString();
            detailCakeDescription.Text = cakeSelected.Description;
        }

        private void addToCartButton_Click(object sender, RoutedEventArgs e)
        {
            //retrieve the selected cake
            int cakeIDSelected;
            int quantity = 1;
            if(sender.GetType() == typeof(string))
            {
                cakeIDSelected = int.Parse(sender as string);
            }
            else if(sender.GetType() == typeof(CartItem))
            {
                cakeIDSelected = ((CartItem)sender).CakeItem.CakeID;
                quantity = ((CartItem)sender).Quantity;
            }
            else
            {
                var dataContext = ((Button)sender).DataContext;
                cakeIDSelected = ((CAKE)dataContext).CakeID;
            }
            var cakeSelected = CakeDAOSQLServer.GetByID(cakeIDSelected);

            //Add cake to cart
            CartItemDAO.AddCakeToCart(ref _cartList, cakeSelected, quantity);
            cartListView.ItemsSource = _cartList;
            //Money part
            CalcMoneyCart();
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

        private void minimizeWindowButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void cakeCartItemButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void detailAddToCart_Click(object sender, RoutedEventArgs e)
        {
            int selectedCakeID = int.Parse(detailCakeID.Text);
            var selectedCake = CakeDAOSQLServer.GetByID(selectedCakeID);
            int quantity = int.Parse(quantityTextBlock.Text);
            var sentEvent = new CartItem()
            {
                CakeItem = selectedCake,
                Quantity = quantity
            };
            addToCartButton_Click(sentEvent, e);
        }

        private void payButton_Click(object sender, RoutedEventArgs e)
        {
            var newOrder = new ORDER()
            {
                CustomerName = CustomerNameTextBox.Text,
                PhoneNumber = PhoneNumberTextBox.Text,
                HomeAddress = AddressTextBox.Text,
                CreatedAt = DateTime.Now,
                PaymentType = 2
            };

            int addedOrderID = OrderDAOSQLServer.Add(newOrder);
            OrderDetailSQLServer.Add(_cartList, addedOrderID);

            MessageBox.Show("Them thanh cong");
        }

        private void canclePayButton_Click(object sender, RoutedEventArgs e)
        {
        }
        public void CalcMoneyCart()
        {
            //Money part
            int cakePay = CartItemDAO.CalcCakePay(_cartList);
            cakePayTextBlock.Text = cakePay.ToString();
            totalPayTextBlock.Text = (cakePay + 50000).ToString();
        }
        private void deleteItemCartButton_Click(object sender, RoutedEventArgs e)
        {
            var dataContext = ((Button)sender).DataContext;
            var selectedCartItem = (CartItem)dataContext;
            _cartList.Remove(selectedCartItem);
            cartListView.ItemsSource = _cartList;
            CalcMoneyCart();
        }
    }
}