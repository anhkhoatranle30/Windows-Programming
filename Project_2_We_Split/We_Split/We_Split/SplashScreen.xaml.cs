using System.Windows;

namespace We_Split
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        public SplashScreen()
        {
            InitializeComponent();
            _screen = new MainWindow();
            _screen.Show();
            _screen.Visibility = Visibility.Hidden;
        }

        private void shutdownScreen_Click(object sender, RoutedEventArgs e)
        {
            _screen.Visibility = Visibility.Visible;
            this.Close();
        }

        private void showSplashScreen_Checked(object sender, RoutedEventArgs e)
        {
            var config = ConfigurationManager.OpenExeConfiguration(
                ConfigurationUserLevel.None);
            config.AppSettings.Settings["ShowSplashScreen"].Value = "false";
            config.Save(ConfigurationSaveMode.Minimal);
        }

        private MainWindow _screen;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var value = ConfigurationManager.AppSettings["showSplashScreen"];
            var showSplash = bool.Parse(value);

            if (showSplash == false)
            {
                _screen.Visibility = Visibility.Visible;

                this.Close();
            }
        }
    }
}