using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
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

        private BindingList<Recipe> _favoriteRecipeList;

        public static String toAbsolutePath(String relative)
        {
            String result;

            var path = new StringBuilder();
            path.Append(AppDomain.CurrentDomain.BaseDirectory);

            result = $"{path}{relative}";
            return result;
        }

        public class RecipesQuantity
        {
            public int Total { get; set; }
            public int Default { get; set; }
        }

        public abstract class RecipeDAO
        {
            public abstract BindingList<Recipe> GetAll();
            public abstract BindingList<Recipe> GetAll(int productsPerPage, int page, ref int noPages);
            public abstract Recipe CreateRecipe(ref int quantityExisted, String title, String desPicture, String description,
                String videoLink, BindingList<Step> stepsList /*, bool isFavorite*/);
            public abstract void Add(Recipe recipe);
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
            //Method
            public override string ToString()
            {
                var result = new StringBuilder();
                result.Append(this.RecipeID.ToString());
                result.Append('*');
                result.Append(this.Title);
                result.Append('*');
                result.Append(this.DesPicture);
                result.Append('*');
                result.Append(this.Description);
                result.Append('*');
                result.Append(this.VideoLink);
                result.Append('*');
                foreach (var step in this.StepsList)
                {
                    result.Append(step.ImgSource);
                    result.Append('*');
                    result.Append(step.Content);
                    result.Append('*');
                }
                result.Append(this.IsFavorite.ToString());
                //result.Append('\n');
                return result.ToString();
            }
        }

        public class FavoriteRecipeDAOTextFile : RecipeDAO
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
                        var step = new Step() { /*ImgSource = toAbsolutePath(tokens[i]),*/ Content = tokens[i + 1] };
                        if(int.Parse(tokens[0]) < 10)
                        {
                            step.ImgSource = toAbsolutePath(tokens[i]);
                        }
                        else
                        {
                            step.ImgSource = tokens[i];
                        }

                        steplist.Add(step);
                    }
                    //Recipe
                    var recipe = new Recipe() { RecipeID = int.Parse(tokens[0]), Title = tokens[1], /*DesPicture = toAbsolutePath(tokens[2]),*/ Description = tokens[2], VideoLink = tokens[4], StepsList = steplist, IsFavorite = bool.Parse(tokens[tokens.Length - 1]) };
                    //if user adds new dishes
                    if (recipe.RecipeID < 10)
                    {
                        recipe.DesPicture = toAbsolutePath(tokens[2]);
                    }
                    else
                    {
                        recipe.DesPicture = tokens[2];
                    }
                    //Add to list
                    if (recipe.IsFavorite == true)
                    {
                        result.Add(recipe);
                    }
                }
                //return
                return result;
            }
<<<<<<< Updated upstream
=======

            /// <summary>
            /// Hàm hiển thị trang (favorite)
            /// </summary>
            /// <param name="productsPerPage"></param>
            /// <param name="page"></param>
            /// <param name="noPages"></param>
            /// <returns></returns>
            public override BindingList<Recipe> GetAll(int productsPerPage, int page, ref int noPages)
            {
                //throw new NotImplementedException();
                
                
                var result = new BindingList<Recipe>();
                //total : 30, ppp : 4, nopage : 2
                //for(ppp*(nopage - 1); i < ppp*nopage && i < total; i++)
                //  result.Add(fullList[i]);
                //1. [0 - 3] for(i = 0; i < 4)
                //2. [4 - 7] for(i = 4
                var fullList = new BindingList<Recipe>();
                fullList = GetAll();
                var total = fullList.Count();
                noPages = (total / productsPerPage) + ((total % productsPerPage == 0) ? 0 : 1);
                for (int i = productsPerPage * (page - 1); i < productsPerPage * page && i < total; i++)
                {
                    result.Add(fullList[i]);
                }

                return result;
            }
            
            /// <summary>
            /// Hàm tạo công thức nấu ăn
            /// </summary>
            /// <param name="quantityExisted"></param>
            /// <param name="title"></param>
            /// <param name="desPicture"></param>
            /// <param name="description"></param>
            /// <param name="videoLink"></param>
            /// <param name="stepsList"></param>
            /// <returns>Trả về công thức nấu ăn</returns>
            public override Recipe CreateRecipe(ref int quantityExisted, string title, string desPicture, 
                string description, string videoLink, BindingList<Step> stepsList)
            {
                //throw new NotImplementedException();
                var result = new Recipe() { RecipeID = quantityExisted++, Title = title, DesPicture = desPicture, 
                      Description = description, VideoLink = videoLink, StepsList = stepsList, IsFavorite = true};
                
                return result;
            }

            public override void Add(Recipe recipe)
            {
                var lineToCompare = recipe.ToString();
                //file name
                var path = new StringBuilder();
                path.Append(AppDomain.CurrentDomain.BaseDirectory);
                path.Append("Database.txt");
                var filename = path.ToString();

                //Anonymous path
                var path2 = new StringBuilder();
                path2.Append(AppDomain.CurrentDomain.BaseDirectory);
                path2.Append("Database2.txt");
                var tempFilename = path2.ToString();
                // Initial values

                int lineNumber = 0;
                int linesRemoved = 0;


                // Read file
                using (var sr = new StreamReader(filename))
                {
                    // Write new file
                    using (var sw = new StreamWriter(tempFilename))
                    {
                        // Read lines
                        String line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            lineNumber++;
                            // Look for text to remove
                            if (line.Contains(lineToCompare))
                            {
                                // Meet the line that needs to be editted
                                recipe.IsFavorite = true;
                                var tempLine = recipe.ToString();
                                sw.Write('\n');
                                sw.Write(tempLine);
                            }
                            else
                            {
                                // Ignore lines that DO match
                                linesRemoved++;
                            }
                        }
                    }
                }
            }
>>>>>>> Stashed changes
        }

        public class RecipeDAOTextFile : RecipeDAO
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
                        var step = new Step() { /*ImgSource = toAbsolutePath(tokens[i]),*/ Content = tokens[i + 1] };
                        if (int.Parse(tokens[0]) < 10)
                        {
                            step.ImgSource = toAbsolutePath(tokens[i]);
                        }
                        else
                        {
                            step.ImgSource = tokens[i];
                        }

                        steplist.Add(step);
                    }
                    //Recipe
                    var recipe = new Recipe() { RecipeID = int.Parse(tokens[0]), Title = tokens[1], /*DesPicture = toAbsolutePath(tokens[2]),*/ Description = tokens[2], VideoLink = tokens[4], StepsList = steplist, IsFavorite = bool.Parse(tokens[tokens.Length - 1]) };
                        //if user adds new dishes
                    if(recipe.RecipeID < 10)
                    {
                        recipe.DesPicture = toAbsolutePath(tokens[2]);
                    } 
                    else
                    {
                        recipe.DesPicture = tokens[2];
                    }
                    //Add to list
                    result.Add(recipe);
                }
                //return
                return result;
            }
<<<<<<< Updated upstream
=======

            /// <summary>
            /// Hàm hiển thị số trang (normal type)
            /// </summary>
            /// <param name="productsPerPage"></param>
            /// <param name="page"></param>
            /// <param name="noPages"></param>
            /// <returns></returns>
            public override BindingList<Recipe> GetAll(int productsPerPage, int page, ref int noPages)
            {
                //throw new NotImplementedException();
                ///<summary>
                ///Hàm hiển thị trang (paging)
                ///<para>aaa</para>
                ///</summary>

                var result = new BindingList<Recipe>();
                //total : 30, ppp : 4, nopage : 2
                //for(ppp*(nopage - 1); i < ppp*nopage && i < total; i++)
                //  result.Add(fullList[i]);
                //1. [0 - 3] for(i = 0; i < 4)
                //2. [4 - 7] for(i = 4
                var fullList = new BindingList<Recipe>();
                fullList = GetAll();
                var total = fullList.Count();
                noPages = (total / productsPerPage) + ((total % productsPerPage == 0) ? 0 : 1);
                for (int i = productsPerPage * (page - 1); i < productsPerPage * page && i < total; i++)
                {
                    result.Add(fullList[i]);
                }

                return result;
            }
            
            /// <summary>
            /// Hàm tạo ra công thức nấu ăn bình thường
            /// </summary>
            /// <param name="quantityExisted"></param>
            /// <param name="title"></param>
            /// <param name="desPicture"></param>
            /// <param name="description"></param>
            /// <param name="videoLink"></param>
            /// <param name="stepsList"></param>
            /// <returns>Trả về kiểu Recipe</returns>
            public override Recipe CreateRecipe(ref int quantityExisted, string title, string desPicture,
                string description, string videoLink, BindingList<Step> stepsList)
            {
                //throw new NotImplementedException();
                var result = new Recipe()
                {
                    RecipeID = quantityExisted++,
                    Title = title,
                    DesPicture = desPicture,
                    Description = description,
                    VideoLink = videoLink,
                    StepsList = stepsList,
                    IsFavorite = false
                };

                return result;
            }

            /// <summary>
            /// Hàm thêm công thức nấu ăn vào database
            /// </summary>
            /// <param name="recipe"></param>
            public override void Add(Recipe recipe)
            {
                //throw new NotImplementedException();
                //file path
                var path = new StringBuilder();
                path.Append(AppDomain.CurrentDomain.BaseDirectory);
                path.Append("Database.txt");
                //end file path
                var newRecipeEncoded = recipe.ToString();
                using (StreamWriter sw = File.AppendText(path.ToString()))
                {
                    sw.Write('\n');
                    sw.Write(newRecipeEncoded);
                }
            }
>>>>>>> Stashed changes
        }

        //end functional

        public MainWindow()
        {
            InitializeComponent();
            //DAO and binding
            var recipeDAOTextFile = new RecipeDAOTextFile();
            _recipeList = recipeDAOTextFile.GetAll();
            var favoriteRecipeDAOTextFile = new FavoriteRecipeDAOTextFile();
            _favoriteRecipeList = favoriteRecipeDAOTextFile.GetAll();

            dataListView.ItemsSource = _recipeList;
            favoriteListView.ItemsSource = _favoriteRecipeList;

            Debug.WriteLine(_recipeList[0].ToString());
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
            Clear(aboutBtn);
        }

        private void HideScreen()
        {
            homeScreen.Visibility = Visibility.Hidden;
            addScreen.Visibility = Visibility.Hidden;
            favoriteScreen.Visibility = Visibility.Hidden;
            settingScreen.Visibility = Visibility.Hidden;
            aboutScreen.Visibility = Visibility.Hidden;
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

        private void aboutBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();
            HideScreen();
            aboutScreen.Visibility = Visibility.Visible;
            Grid _img = aboutBtn.Template.FindName("img", aboutBtn) as Grid;

            if (_img != null)
                _img.Background = _backgroundColor.SolidColor;

            Border _border = aboutBtn.Template.FindName("border", aboutBtn) as Border;

            if (_border != null)
                _border.Background = Brushes.White;
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

        private void addImgBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();

            if (fd.ShowDialog() == true)
            {
                ImageBrush myBrush = new ImageBrush();
                Image image = new Image();
                image.Source = new BitmapImage(
                    new Uri(fd.FileName));
                myBrush.ImageSource = image.Source;
                addImgBtn.Background = myBrush;
            }
        }
    }
}