using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
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
    /// UI/UX : Hoang
    /// Functional : Khoa
    /// </summary>
    public partial class MainWindow : Window
    {
        //functional
        private BindingList<Recipe> _recipeList;

        BindingList<Recipe> _favoriteRecipeList;

        public static String toAbsolutePath(String relative)
        {
            String result;

            var path = new StringBuilder();
            path.Append(AppDomain.CurrentDomain.BaseDirectory);

            result = $"{path}{relative}";
            return result;
        }

        public abstract class FactoryDAO<T>
        {
            public abstract BindingList<T> GetAll();

            //public abstract void Update();
            //public abstract void Delete();
        }

        public class Step
        {
            //properties
            public string ImgSource { get; set; }

            public string Content { get; set; }
        }

        public class Recipe
        {
            //Properties
            public int RecipeID { get; set; }
            public string Title { get; set; }
            public string DesPicture { get; set; } //absolute path
            public string Description { get; set; }
            public string VideoLink { get; set; }
            public BindingList<Step> StepsList { set; get; }
            public bool IsFavorite { get; set; }
        }

        public class FavoriteRecipeDAOTextFile : FactoryDAO<Recipe>
        {
            public override BindingList<Recipe> GetAll()
            {
                //
                //Initialize list
                //
                var result = new BindingList<Recipe>()
                {
                };

                //
                //Read file txt
                //
                var path = new StringBuilder();

                path.Append(AppDomain.CurrentDomain.BaseDirectory);
                path.Append("Database.txt");

                var lines = File.ReadAllLines(path.ToString());
                //recipes
                foreach (var line in lines)
                {
                    var tokens = line.Split(
                        new String[] { "*" },
                        StringSplitOptions.None);
                    //StepsList
                    var steplist = new BindingList<Step>();
                    for (int i = 5; i < tokens.Length - 1; i += 2)
                    {
                        var step = new Step() { ImgSource = toAbsolutePath(tokens[i]), Content = tokens[i + 1] };

                        steplist.Add(step);
                    }
                    //Recipe
                    var recipe = new Recipe() { RecipeID = int.Parse(tokens[0]), Title = tokens[1], DesPicture = toAbsolutePath(tokens[2]), Description = tokens[2], VideoLink = tokens[4], StepsList = steplist, IsFavorite = bool.Parse(tokens[tokens.Length - 1]) };
                    //Add to list
                    if(recipe.IsFavorite == true)
                    {
                        result.Add(recipe);
                    }
                }
                //return
                return result;
            }
        }
        public class RecipeDAOTextFile : FactoryDAO<Recipe>
        {
            public override BindingList<Recipe> GetAll()
            {
                //
                //Initialize list
                //
                var result = new BindingList<Recipe>()
                {
                };

                //
                //Read file txt
                //
                var path = new StringBuilder();

                path.Append(AppDomain.CurrentDomain.BaseDirectory);
                path.Append("Database.txt");

                var lines = File.ReadAllLines(path.ToString());
                    //recipes
                foreach (var line in lines)
                {
                    var tokens = line.Split(
                        new String[] { "*" },
                        StringSplitOptions.None);
                    //StepsList
                    var steplist = new BindingList<Step>();
                    for (int i = 5; i < tokens.Length - 1; i += 2)
                    {
                        var step = new Step() { ImgSource = toAbsolutePath(tokens[i]), Content = tokens[i + 1] };

                        steplist.Add(step);
                    }
                    //Recipe
                    var recipe = new Recipe() { RecipeID = int.Parse(tokens[0]), Title = tokens[1], DesPicture = toAbsolutePath(tokens[2]), Description = tokens[2], VideoLink = tokens[4], StepsList = steplist, IsFavorite = bool.Parse(tokens[tokens.Length - 1]) };
                    //Add to list
                    result.Add(recipe);
                }
                //return
                return result;
            }
        }

        //end functional

        public MainWindow()
        {
            InitializeComponent();
            var recipeDAOTextFile = new RecipeDAOTextFile();
            _recipeList = recipeDAOTextFile.GetAll();
            var favoriteRecipeDAOTextFile = new FavoriteRecipeDAOTextFile();
            _favoriteRecipeList = favoriteRecipeDAOTextFile.GetAll();

            dataListView.ItemsSource = _recipeList;
            favoriteListView.ItemsSource = _favoriteRecipeList;
        }

        private void Clear(Button btn)
        {
            Grid _img = btn.Template.FindName("img", btn) as Grid;

            if (_img != null)
            {
                _img.ClearValue(BackgroundProperty);
            }

            Border _border = btn.Template.FindName("border", btn) as Border;

            if (_border != null)
            {
                _border.ClearValue(BackgroundProperty);
            }

            TextBlock _text = btn.Template.FindName("text", btn) as TextBlock;

            if (_text != null)
            {
                _text.ClearValue(ForegroundProperty);
            }
        }

        private void ClearAll()
        {
            Clear(homeBtn);
            Clear(addBtn);
            Clear(favoriteBtn);
            Clear(settingBtn);
        }

        private void HideScreen()
        {
            homeScreen.Visibility = Visibility.Hidden;
            addScreen.Visibility = Visibility.Hidden;
            favoriteScreen.Visibility = Visibility.Hidden;
            settingScreen.Visibility = Visibility.Hidden;
        }

        private void homeBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();
            HideScreen();
            homeScreen.Visibility = Visibility.Visible;

            Grid _img = homeBtn.Template.FindName("img", homeBtn) as Grid;
            if (_img != null)
                _img.Background = _backgroundColor.SolidColor;

            Border _border = homeBtn.Template.FindName("border", homeBtn) as Border;
            if (_border != null)
                _border.Background = Brushes.White;

            TextBlock _text = homeBtn.Template.FindName("text", homeBtn) as TextBlock;
            if (_text != null)
                _text.Foreground = _backgroundColor.SolidColor;
        }

        private void favoriteBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();
            HideScreen();
            favoriteScreen.Visibility = Visibility.Visible;

            Grid _img = favoriteBtn.Template.FindName("img", favoriteBtn) as Grid;
            if (_img != null)
                _img.Background = _backgroundColor.SolidColor;

            Border _border = favoriteBtn.Template.FindName("border", favoriteBtn) as Border;
            if (_border != null)
                _border.Background = Brushes.White;

            TextBlock _text = favoriteBtn.Template.FindName("text", favoriteBtn) as TextBlock;
            if (_text != null)
                _text.Foreground = _backgroundColor.SolidColor;

            //functional
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();
            HideScreen();
            addScreen.Visibility = Visibility.Visible;
            Grid _img = addBtn.Template.FindName("img", addBtn) as Grid;

            if (_img != null)
                _img.Background = _backgroundColor.SolidColor;

            Border _border = addBtn.Template.FindName("border", addBtn) as Border;

            if (_border != null)
                _border.Background = Brushes.White;

            TextBlock _text = addBtn.Template.FindName("text", addBtn) as TextBlock;

            if (_text != null)
                _text.Foreground = _backgroundColor.SolidColor;

            //functional
        }

        private void settingBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();
            HideScreen();
            settingScreen.Visibility = Visibility.Visible;
            Grid _img = settingBtn.Template.FindName("img", settingBtn) as Grid;

            if (_img != null)
                _img.Background = _backgroundColor.SolidColor;

            Border _border = settingBtn.Template.FindName("border", settingBtn) as Border;

            if (_border != null)
                _border.Background = Brushes.White;

            TextBlock _text = settingBtn.Template.FindName("text", settingBtn) as TextBlock;

            if (_text != null)
                _text.Foreground = _backgroundColor.SolidColor;

            //functional
        }

        private void shuwdownBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private class BackgroundColor : INotifyPropertyChanged
        {
            public string Color { set; get; }
            public SolidColorBrush SolidColor { set; get; }

            public event PropertyChangedEventHandler PropertyChanged;
        }

        private BackgroundColor _backgroundColor;

        private void setDefaultColor(string buttonName)
        {
            var config = ConfigurationManager.OpenExeConfiguration(
                ConfigurationUserLevel.None);
            config.AppSettings.Settings["colorDefault"].Value = buttonName;
            config.Save(ConfigurationSaveMode.Minimal);
        }

        private void redColor_Checked(object sender, RoutedEventArgs e)
        {
            _backgroundColor.Color = "Red";
            _backgroundColor.SolidColor = new SolidColorBrush(Colors.Red);

            settingBtn_Click(sender, e);
            setDefaultColor("redColor");
        }

        private void blueColor_Checked(object sender, RoutedEventArgs e)
        {
            _backgroundColor.Color = "Blue";
            _backgroundColor.SolidColor = new SolidColorBrush(Colors.Blue);

            settingBtn_Click(sender, e);
            setDefaultColor("blueColor");
        }

        private void yellowColor_Checked(object sender, RoutedEventArgs e)
        {
            _backgroundColor.Color = "Yellow";
            _backgroundColor.SolidColor = new SolidColorBrush(Colors.Yellow);

            settingBtn_Click(sender, e);
            setDefaultColor("yellowColor");
        }

        private void greenColor_Checked(object sender, RoutedEventArgs e)
        {
            _backgroundColor.Color = "Green";
            _backgroundColor.SolidColor = new SolidColorBrush(Colors.Green);

            settingBtn_Click(sender, e);
            setDefaultColor("greenColor");
        }

        private void defaultColor_Checked(object sender, RoutedEventArgs e)
        {
            _backgroundColor.Color = "#3498DB";
            _backgroundColor.SolidColor = new SolidColorBrush(Color.FromRgb(52, 152, 219));

            settingBtn_Click(sender, e);
            setDefaultColor("defaultColor");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_backgroundColor == null)
            {
                _backgroundColor = new BackgroundColor
                {
                    Color = "#3498DB",
                    SolidColor = new SolidColorBrush(Color.FromRgb(52, 152, 219))
                };
            }

            this.DataContext = _backgroundColor;
            var value = ConfigurationManager.AppSettings["colorDefault"];
            var myRadioButton = (RadioButton)this.FindName(value);

            myRadioButton.IsChecked = true;
            homeBtn_Click(sender, e);
        }
    }
}