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

        private void Clear(Button btn)
        {
            Grid _img = btn.Template.FindName("img", btn) as Grid;
            _img.ClearValue(BackgroundProperty);

            Border _border = btn.Template.FindName("border", btn) as Border;
            _border.ClearValue(BackgroundProperty);

            TextBlock _text = btn.Template.FindName("text", btn) as TextBlock;
            _text.ClearValue(ForegroundProperty);
        }

        private void ClearAll()
        {
            Clear(homeBtn);
            Clear(addBtn);
            Clear(favoriteBtn);
            Clear(settingBtn);
        }

        private void homeBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();
            Grid _img = homeBtn.Template.FindName("img", homeBtn) as Grid;
            _img.Background = new SolidColorBrush(Color.FromRgb(52, 152, 219));

            Border _border = homeBtn.Template.FindName("border", homeBtn) as Border;
            _border.Background = Brushes.White;

            TextBlock _text = homeBtn.Template.FindName("text", homeBtn) as TextBlock;
            _text.Foreground = new SolidColorBrush(Color.FromRgb(52, 152, 219));
        }

        private void favoriteBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();

            Grid _img = favoriteBtn.Template.FindName("img", favoriteBtn) as Grid;
            _img.Background = new SolidColorBrush(Color.FromRgb(52, 152, 219));

            Border _border = favoriteBtn.Template.FindName("border", favoriteBtn) as Border;
            _border.Background = Brushes.White;

            TextBlock _text = favoriteBtn.Template.FindName("text", favoriteBtn) as TextBlock;
            _text.Foreground = new SolidColorBrush(Color.FromRgb(52, 152, 219));
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();

            Grid _img = addBtn.Template.FindName("img", addBtn) as Grid;
            _img.Background = new SolidColorBrush(Color.FromRgb(52, 152, 219));

            Border _border = addBtn.Template.FindName("border", addBtn) as Border;
            _border.Background = Brushes.White;

            TextBlock _text = addBtn.Template.FindName("text", addBtn) as TextBlock;
            _text.Foreground = new SolidColorBrush(Color.FromRgb(52, 152, 219));
        }

        private void settingBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();

            Grid _img = settingBtn.Template.FindName("img", settingBtn) as Grid;
            _img.Background = new SolidColorBrush(Color.FromRgb(52, 152, 219));

            Border _border = settingBtn.Template.FindName("border", settingBtn) as Border;
            _border.Background = Brushes.White;

            TextBlock _text = settingBtn.Template.FindName("text", settingBtn) as TextBlock;
            _text.Foreground = new SolidColorBrush(Color.FromRgb(52, 152, 219));
        }

        private void shuwdownBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}