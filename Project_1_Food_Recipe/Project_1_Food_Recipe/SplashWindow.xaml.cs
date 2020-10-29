using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Shapes;

namespace Project_1_Food_Recipe
{
    /// <summary>
    /// Interaction logic for SplashWindow.xaml
    /// </summary>
    public partial class SplashWindow : Window
    {
        public SplashWindow()
        {
            InitializeComponent();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var config = ConfigurationManager.OpenExeConfiguration(
                ConfigurationUserLevel.None);
            config.AppSettings.Settings["ShowSplashScreen"].Value = "false";
            config.Save(ConfigurationSaveMode.Minimal);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var value = ConfigurationManager.AppSettings["ShowSplashScreen"];
            var showSplash = bool.Parse(value);
            Debug.WriteLine(value);

            if (showSplash == false)
            {
                var screen = new MainWindow();
                screen.Show();

                this.Close();
            }
            else
            {
                //
            }
        }
    }
}