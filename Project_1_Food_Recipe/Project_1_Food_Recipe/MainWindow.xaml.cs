using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        BindingList<Recipe> _recipeList;
        //BindingList<Recipe> _favoriteList;

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
            //Attributes
            public List<Step> stepsList;
            //Properties
            public string Title { get; set; }
            public string DesPicture { get; set; } //absolute path
            public string Description { get; set; }
            public string VideoLink { get; set; }
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
                foreach (var line in lines)
                {
                    var tokens = line.Split(
                        new String[] { "*" },
                        StringSplitOptions.None);
                    //StepsList
                    var steplist = new List<Step>();
                    for(int i = 4; i < tokens.Length; i += 2)
                    {
                        var step = new Step() { ImgSource = toAbsolutePath(tokens[i]), Content = tokens[i + 1] };
                        
                        steplist.Add(step);
                    }
                    //Recipe
                    var recipe = new Recipe() { Title = tokens[0], DesPicture = toAbsolutePath(tokens[1]), Description = tokens[2], VideoLink = tokens[3], stepsList = steplist};
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
            var temp = new RecipeDAOTextFile();
            _recipeList = temp.GetAll();
            Debug.WriteLine(_recipeList[0].DesPicture);
            dataListView.ItemsSource = _recipeList;
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

            //functional

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

            //functional
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

            //functional
        }

        private void shuwdownBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}