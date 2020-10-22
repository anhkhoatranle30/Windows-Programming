using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Media.TextFormatting;
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

        private bool isHomeBtnClicked = false;
        private bool isAddBtnClicked = false;
        private bool isFavoriteBtnClicked = false;
        private bool isSettingBtnClicked = false;

        private void Clear(Button btn, string name)
        {
            Image _img = btn.Template.FindName("img", btn) as Image;
            _img.Source = new BitmapImage(new Uri($"Images/img_{name}_default.png", UriKind.Relative));

            Border _border = btn.Template.FindName("border", btn) as Border;
            _border.ClearValue(BackgroundProperty);

            TextBlock _text = btn.Template.FindName("text", btn) as TextBlock;
            _text.ClearValue(ForegroundProperty);
        }

        private void homeBtn_Click(object sender, RoutedEventArgs e)
        {
            Clear(addBtn, "add");
            isAddBtnClicked = false;
            Clear(favoriteBtn, "favorite");
            isFavoriteBtnClicked = false;
            Clear(settingBtn, "setting");
            isSettingBtnClicked = false;

            Image _img = homeBtn.Template.FindName("img", homeBtn) as Image;
            _img.Source = new BitmapImage(new Uri("Images/img_home_checked.png", UriKind.Relative));

            Border _border = homeBtn.Template.FindName("border", homeBtn) as Border;
            _border.Background = Brushes.White;

            TextBlock _text = homeBtn.Template.FindName("text", homeBtn) as TextBlock;
            _text.Foreground = new SolidColorBrush(Color.FromRgb(52, 152, 219));

            isHomeBtnClicked = true;
        }

        private void favoriteBtn_Click(object sender, RoutedEventArgs e)
        {
            Clear(addBtn, "add");
            isAddBtnClicked = false;
            Clear(homeBtn, "home");
            isHomeBtnClicked = false;
            Clear(settingBtn, "setting");
            isSettingBtnClicked = false;

            Image _img = favoriteBtn.Template.FindName("img", favoriteBtn) as Image;
            _img.Source = new BitmapImage(new Uri("Images/img_favorite_checked.png", UriKind.Relative));

            Border _border = favoriteBtn.Template.FindName("border", favoriteBtn) as Border;
            _border.Background = Brushes.White;

            TextBlock _text = favoriteBtn.Template.FindName("text", favoriteBtn) as TextBlock;
            _text.Foreground = new SolidColorBrush(Color.FromRgb(52, 152, 219));

            isFavoriteBtnClicked = true;
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            Clear(homeBtn, "home");
            isHomeBtnClicked = false;
            Clear(favoriteBtn, "favorite");
            isFavoriteBtnClicked = false;
            Clear(settingBtn, "setting");
            isSettingBtnClicked = false;

            Image _img = addBtn.Template.FindName("img", addBtn) as Image;
            _img.Source = new BitmapImage(new Uri("Images/img_add_checked.png", UriKind.Relative));

            Border _border = addBtn.Template.FindName("border", addBtn) as Border;
            _border.Background = Brushes.White;

            TextBlock _text = addBtn.Template.FindName("text", addBtn) as TextBlock;
            _text.Foreground = new SolidColorBrush(Color.FromRgb(52, 152, 219));

            isAddBtnClicked = true;
        }

        private void settingBtn_Click(object sender, RoutedEventArgs e)
        {
            Clear(addBtn, "add");
            isAddBtnClicked = false;
            Clear(favoriteBtn, "favorite");
            isFavoriteBtnClicked = false;
            Clear(homeBtn, "home");
            isHomeBtnClicked = false;

            Image _img = settingBtn.Template.FindName("img", settingBtn) as Image;
            _img.Source = new BitmapImage(new Uri("Images/img_setting_checked.png", UriKind.Relative));

            Border _border = settingBtn.Template.FindName("border", settingBtn) as Border;
            _border.Background = Brushes.White;

            TextBlock _text = settingBtn.Template.FindName("text", settingBtn) as TextBlock;
            _text.Foreground = new SolidColorBrush(Color.FromRgb(52, 152, 219));

            isSettingBtnClicked = true;
        }

        private void homeBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            Image _img = homeBtn.Template.FindName("img", homeBtn) as Image;
            _img.Source = new BitmapImage(new Uri("Images/img_home_checked.png", UriKind.Relative));
        }

        private void favoriteBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            Image _img = favoriteBtn.Template.FindName("img", favoriteBtn) as Image;
            _img.Source = new BitmapImage(new Uri("Images/img_favorite_checked.png", UriKind.Relative));
        }

        private void addBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            Image _img = addBtn.Template.FindName("img", addBtn) as Image;
            _img.Source = new BitmapImage(new Uri("Images/img_add_checked.png", UriKind.Relative));
        }

        private void settingBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            Image _img = settingBtn.Template.FindName("img", settingBtn) as Image;
            _img.Source = new BitmapImage(new Uri("Images/img_setting_checked.png", UriKind.Relative));
        }

        private void homeBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isHomeBtnClicked == false)
            {
                Image _img = homeBtn.Template.FindName("img", homeBtn) as Image;
                _img.Source = new BitmapImage(new Uri("Images/img_home_default.png", UriKind.Relative));
            }
        }

        private void favoriteBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isFavoriteBtnClicked == false)
            {
                Debug.WriteLine(1);
                Image _img = favoriteBtn.Template.FindName("img", favoriteBtn) as Image;
                _img.Source = new BitmapImage(new Uri("Images/img_favorite_default.png", UriKind.Relative));
            }
        }

        private void addBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isAddBtnClicked == false)
            {
                Image _img = addBtn.Template.FindName("img", addBtn) as Image;
                _img.Source = new BitmapImage(new Uri("Images/img_add_default.png", UriKind.Relative));
            }
        }

        private void settingBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isSettingBtnClicked == false)
            {
                Image _img = settingBtn.Template.FindName("img", settingBtn) as Image;
                _img.Source = new BitmapImage(new Uri("Images/img_setting_default.png", UriKind.Relative));
            }
        }
    }
}