using Microsoft.Win32;
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
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace Project_1_Food_Recipe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// UI/UX : Hoang
    /// Functional : Khoa
    /// </summary>
    public partial class MainWindow : Window
    {
        #region functional

        //functional

        #region global variables

        //Lists
        private BindingList<Recipe> _recipeList;

        private BindingList<Recipe> _favoriteRecipeList;
        private BindingList<Recipe> _searchRecipeList;

        public static String toAbsolutePath(String relative)
        {
            String result;

            var path = new StringBuilder();
            path.Append(AppDomain.CurrentDomain.BaseDirectory);

            result = $"{path}{relative}";
            return result;
        }

        //paging
        public int noPages;

        public int pageNumber;
        public int productsPerPage;

        #endregion global variables

        #region abstract DAO classes

        public abstract class RecipesQuantityDAO
        {
            public abstract RecipesQuantity GetAll();
        }

        public abstract class RecipeDAO
        {
            public abstract BindingList<Recipe> GetAll();

            public abstract BindingList<Recipe> GetAll(int productsPerPage, ref int page, ref int noPages);

            public abstract Recipe CreateRecipe(String title, String desPicture, String description,
                String videoLink, BindingList<Step> stepsList /*, bool isFavorite*/);

            public abstract void Add(Recipe recipe);

            public abstract void Delete(Recipe recipe);

            public abstract BindingList<Recipe> Search(String searchString);
        }

        #endregion abstract DAO classes

        #region definition classes

        class SearchString
        {
            private static readonly string[] VietnameseSigns = new string[] {

                "aAeEoOuUiIdDyY",

                "áàạảãâấầậẩẫăắằặẳẵ",

                "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",

                "éèẹẻẽêếềệểễ",

                "ÉÈẸẺẼÊẾỀỆỂỄ",

                "óòọỏõôốồộổỗơớờợởỡ",

                "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",

                "úùụủũưứừựửữ",

                "ÚÙỤỦŨƯỨỪỰỬỮ",

                "íìịỉĩ",

                "ÍÌỊỈĨ",

                "đ",

                "Đ",

                "ýỳỵỷỹ",

                "ÝỲỴỶỸ"

            };
            /// <summary>
            /// Hàm để xóa dấu tiếng việt đi thành không dấu
            /// </summary>
            /// <param name="str">Chuỗi có dấu</param>
            /// <returns>Trả về chuỗi không dấu</returns>
            public static string RemoveSign4VietnameseString(string str)

            {

                //Tiến hành thay thế , lọc bỏ dấu cho chuỗi

                for (int i = 1; i < VietnameseSigns.Length; i++)

                {

                    for (int j = 0; j < VietnameseSigns[i].Length; j++)

                        str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);

                }

                return str;

            }
            /// <summary>
            /// Hàm parse từ chuỗi bình thường ra chuỗi để search
            /// </summary>
            /// <param name="str">Hàm muốn search</param>
            /// <returns>Trả về hàm không có dấu và viết thường hết</returns>
            public static string Parse(string str)
            {
                str = RemoveSign4VietnameseString(str);
                str = str.ToLower();
                return str;
            }
            public static bool CheckSearch(string searchedString, string toSearchString)
            {
                searchedString = Parse(searchedString);
                toSearchString = Parse(toSearchString);
                var tokens = searchedString.Split(new String[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                foreach(var token in tokens)
                {
                    if(toSearchString.Contains(token))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public class RecipesQuantity
        {
            public int Total { get; set; }
            public int Default { get; set; }
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

            public Recipe Parse(string line)
            {
                var result = new Recipe();
                var tokens = line.Split(
                        new String[] { "*" },
                        StringSplitOptions.None);
                if (tokens.Length > 0)
                {
                    //StepsList
                    var steplist = new BindingList<Step>();
                    for (int i = 5; i < tokens.Length - 1; i += 2)
                    {
                        var step = new Step() { ImgSource = toAbsolutePath(tokens[i]), Content = tokens[i + 1] };

                        steplist.Add(step);
                    }
                    //Recipe
                    var recipe = new Recipe() { RecipeID = int.Parse(tokens[0]), Title = tokens[1], DesPicture = toAbsolutePath(tokens[2]), Description = tokens[3], VideoLink = tokens[4], StepsList = steplist, IsFavorite = bool.Parse(tokens[tokens.Length - 1]) };
                    result = recipe;
                }
                return result;
            }
        }

        #endregion definition classes

        #region DAOTextFile

        public class FavoriteRecipeDAOTextFile : RecipeDAO
        {
            public override BindingList<Recipe> GetAll()
            {
                //
                //Get the quantity of default
                //
                var recipesQuantityDAOTextFile = new RecipesQuantityDAOTextFile();
                var quantity = recipesQuantityDAOTextFile.GetAll();
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
                    if (tokens.Length > 0)
                    {
                        //StepsList
                        var steplist = new BindingList<Step>();
                        for (int i = 5; i < tokens.Length - 1; i += 2)
                        {
                            var step = new Step() { /*ImgSource = toAbsolutePath(tokens[i]),*/ Content = tokens[i + 1] };
                            if (int.Parse(tokens[0]) < quantity.Default)
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
                        var recipe = new Recipe() { RecipeID = int.Parse(tokens[0]), Title = tokens[1], /*DesPicture = toAbsolutePath(tokens[2]),*/ Description = tokens[3], VideoLink = tokens[4], StepsList = steplist, IsFavorite = bool.Parse(tokens[tokens.Length - 1]) };
                        //if user adds new dishes
                        if (recipe.RecipeID < quantity.Default)
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
                }
                //return
                return result;
            }

            /// <summary>
            /// Hàm hiển thị trang (favorite)
            /// </summary>
            /// <param name="productsPerPage"></param>
            /// <param name="page"></param>
            /// <param name="noPages"></param>
            /// <returns></returns>
            public override BindingList<Recipe> GetAll(int productsPerPage, ref int page, ref int noPages)
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
                //page
                page = page % noPages + ((page % noPages == 0) ? noPages : 0);
                //end page
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
            public override Recipe CreateRecipe(string title, string desPicture,
                string description, string videoLink, BindingList<Step> stepsList)
            {
                //throw new NotImplementedException();
                var result = new Recipe()
                {
                    RecipeID = -1,
                    Title = title,
                    DesPicture = desPicture,
                    Description = description,
                    VideoLink = videoLink,
                    StepsList = stepsList,
                    IsFavorite = true
                };

                return result;
            }

            /// <summary>
            /// Hàm thêm công thức nấu ăn vào mục favorite
            /// </summary>
            /// <param name="recipe"></param>
            public override void Add(Recipe recipe)
            {
                var lineToCompare = recipe.RecipeID.ToString();
                lineToCompare += "*";
                lineToCompare += recipe.Title;
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
                //int linesRemoved = 0;

                //
                // Read file
                //
                using (var sr = new StreamReader(filename))
                {
                    //
                    // Write new file
                    //
                    using (var sw = new StreamWriter(tempFilename))
                    {
                        //
                        // Read lines
                        //
                        String line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            lineNumber++;
                            // if its not the line we re looking for
                            if (!line.Contains(lineToCompare))
                            {
                                sw.WriteLine(line);
                            }
                            else
                            {
                                // Meet the line that needs to be editted
                                //recipe.IsFavorite = true;
                                //var tempLine = recipe.ToString();
                                //sw.WriteLine(tempLine);
                                var tokens = line.Split(new String[] { "*" }, StringSplitOptions.None);
                                var toAddLine = new StringBuilder();
                                for (int i = 0; i < tokens.Length - 1; i++)
                                {
                                    toAddLine.Append(tokens[i]);
                                    toAddLine.Append("*");
                                }
                                toAddLine.Append("True");
                                sw.WriteLine(toAddLine);
                            }
                        }
                    }
                }
                // Delete original file
                File.Delete(filename);

                // ... and put the temp file in its place.
                File.Move(tempFilename, filename);
            }

            /// <summary>
            /// Xóa món ăn ưa thích ra khỏi danh sách ưa thích, món ăn vẫn còn trên db
            /// </summary>
            /// <param name="recipe"></param>
            public override void Delete(Recipe recipe)
            {
                var lineToCompare = recipe.RecipeID.ToString();
                lineToCompare += "*";
                lineToCompare += recipe.Title;
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
                //int linesRemoved = 0;

                //
                // Read file
                //
                using (var sr = new StreamReader(filename))
                {
                    //
                    // Write new file
                    //
                    using (var sw = new StreamWriter(tempFilename))
                    {
                        //
                        // Read lines
                        //
                        String line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            lineNumber++;
                            // if its not the line we re looking for
                            if (!line.Contains(lineToCompare))
                            {
                                sw.WriteLine(line);
                            }
                            else
                            {
                                // Meet the line that needs to be editted
                                //recipe.IsFavorite = false;
                                //var tempLine = recipe.ToString();
                                //sw.WriteLine(tempLine);
                                var tokens = line.Split(new String[] { "*" }, StringSplitOptions.None);
                                var toAddLine = new StringBuilder();
                                for (int i = 0; i < tokens.Length - 1; i++)
                                {
                                    toAddLine.Append(tokens[i]);
                                    toAddLine.Append("*");
                                }
                                toAddLine.Append("False");
                                sw.WriteLine(toAddLine);
                            }
                        }
                    }
                }
                // Delete original file
                File.Delete(filename);

                // ... and put the temp file in its place.
                File.Move(tempFilename, filename);
            }

            /// <summary>
            /// Hàm tìm kiếm món ăn ƯA THÍCH
            /// </summary>
            /// <param name="searchString">Chuỗi cần tìm</param>
            /// <returns></returns>
            public override BindingList<Recipe> Search(string searchString)
            {
                var result = new BindingList<Recipe>();

                var recipes = GetAll();
                foreach (var recipe in recipes)
                {
                    if (SearchString.CheckSearch(searchString, recipe.Title))
                    {
                        result.Add(recipe);
                    }
                }
                return result;
            }
        }

        public class RecipeDAOTextFile : RecipeDAO
        {
            /// <summary>
            /// Hàm lấy tất cả dữ liệu
            /// </summary>
            /// <returns>Trả về BindingList các món ăn</returns>
            public override BindingList<Recipe> GetAll()
            {
                //
                //Get the quantity of default
                //
                var recipesQuantityDAOTextFile = new RecipesQuantityDAOTextFile();
                var quantity = recipesQuantityDAOTextFile.GetAll();
                //
                //Initialize list
                //
                var result = new BindingList<Recipe>();
                var fullList = new BindingList<Recipe>();

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
                    if (tokens.Length > 0)
                    {
                        //StepsList
                        var steplist = new BindingList<Step>();
                        for (int i = 5; i < tokens.Length - 1; i += 2)
                        {
                            var step = new Step() { /*ImgSource = toAbsolutePath(tokens[i]),*/ Content = tokens[i + 1] };
                            if (int.Parse(tokens[0]) < quantity.Default)
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
                        var recipe = new Recipe() { RecipeID = int.Parse(tokens[0]), Title = tokens[1], /*DesPicture = toAbsolutePath(tokens[2]),*/ Description = tokens[3], VideoLink = tokens[4], StepsList = steplist, IsFavorite = bool.Parse(tokens[tokens.Length - 1]) };
                        //if user adds new dishes
                        if (recipe.RecipeID < quantity.Default)
                        {
                            recipe.DesPicture = toAbsolutePath(tokens[2]);
                        }
                        else
                        {
                            recipe.DesPicture = tokens[2];
                        }
                        //Add to list
                        fullList.Add(recipe);
                    }
                }
                //add favorite to head of the list
                foreach(var recipe in fullList)
                {
                    if(recipe.IsFavorite == true)
                    {
                        result.Add(recipe);
                    }
                }
                //add unfavorite to tail of the list
                foreach (var recipe in fullList)
                {
                    if (recipe.IsFavorite == false)
                    {
                        result.Add(recipe);
                    }
                }

                //return
                return result;
            }

            /// <summary>
            /// Hàm hiển thị số trang (normal type)
            /// </summary>
            /// <param name="productsPerPage"></param>
            /// <param name="page"></param>
            /// <param name="noPages"></param>
            /// <returns></returns>
            public override BindingList<Recipe> GetAll(int productsPerPage, ref int page, ref int noPages)
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
                //page
                page = page % noPages + ((page % noPages == 0) ? noPages : 0);
                //end page
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
            public override Recipe CreateRecipe(string title, string desPicture, string description, string videoLink, BindingList<Step> stepsList)
            {
                //throw new NotImplementedException();
                var result = new Recipe()
                {
                    RecipeID = -1,
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
                //db file path
                var path = new StringBuilder();
                path.Append(AppDomain.CurrentDomain.BaseDirectory);
                path.Append("Database.txt");
                //quantity file path
                var quantityFilePath = new StringBuilder();
                quantityFilePath.Append(AppDomain.CurrentDomain.BaseDirectory);
                quantityFilePath.Append("Database_quantity.txt");
                //end file path

                //
                //Get quantity
                //
                var recipesQuantityDAOTextFile = new RecipesQuantityDAOTextFile();
                var quantity = recipesQuantityDAOTextFile.GetAll();

                //Check if new dish has ID
                if (recipe.RecipeID != quantity.Total)
                {
                    recipe.RecipeID = quantity.Total++;
                }
                //Write to Database.txt
                var newRecipeEncoded = recipe.ToString();
                using (StreamWriter sw = File.AppendText(path.ToString()))
                {
                    sw.WriteLine(newRecipeEncoded);
                }
                //Write to Database_quantity.txt
                using (StreamWriter sw = File.CreateText(quantityFilePath.ToString()))
                {
                    sw.WriteLine(quantity.Default);
                    sw.Write(quantity.Total);
                }
            }

            /// <summary>
            /// Hàm xóa một món ăn ra khỏi db
            /// </summary>
            /// <param name="recipe"></param>
            public override void Delete(Recipe recipe)
            {
                //throw new NotImplementedException();
                var lineToCompare = recipe.RecipeID.ToString();
                lineToCompare += "*";
                lineToCompare += recipe.Title;
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
                //int linesRemoved = 0;

                //
                // Read file
                //
                using (var sr = new StreamReader(filename))
                {
                    //
                    // Write new file
                    //
                    using (var sw = new StreamWriter(tempFilename))
                    {
                        //
                        // Read lines
                        //
                        String line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            lineNumber++;
                            // if its not the line we re looking for
                            if (!line.Contains(lineToCompare))
                            {
                                sw.WriteLine(line);
                            }
                            else
                            {
                                // Meet the line that needs to be deleted
                                //Just ignore it and do nothing
                            }
                        }
                    }
                }
                // Delete original file
                File.Delete(filename);

                // ... and put the temp file in its place.
                File.Move(tempFilename, filename);
            }

            /// <summary>
            /// Hàm tìm kiếm món ăn
            /// </summary>
            /// <param name="searchString">chuỗi cần tìm</param>
            /// <returns></returns>
            public override BindingList<Recipe> Search(string searchString)
            {
                var result = new BindingList<Recipe>();

                var recipes = GetAll();
                foreach (var recipe in recipes)
                {
                    if (SearchString.CheckSearch(searchString, recipe.Title))
                    {
                        result.Add(recipe);
                    }
                }
                return result;
            }
        }

        public class RecipesQuantityDAOTextFile : RecipesQuantityDAO
        {
            public override RecipesQuantity GetAll()
            {
                var result = new RecipesQuantity();
                //
                //Read file txt
                //
                var path = new StringBuilder();
                path.Append(AppDomain.CurrentDomain.BaseDirectory);
                path.Append("Database_quantity.txt");

                var lines = File.ReadAllLines(path.ToString());
                //quantities
                result.Default = int.Parse(lines[0]);
                result.Total = int.Parse(lines[1]);
                return result;
            }
        }

        #endregion DAOTextFile

        //end functional

        #endregion functional

        public MainWindow()
        {
            InitializeComponent();
            var recipeDAOTextFile = new RecipeDAOTextFile();
            _recipeList = recipeDAOTextFile.GetAll();
            var favoriteRecipeDAOTextFile = new FavoriteRecipeDAOTextFile();
            _favoriteRecipeList = favoriteRecipeDAOTextFile.GetAll();

            dataListView.ItemsSource = _recipeList;
            favoriteListView.ItemsSource = _favoriteRecipeList;

            #region test paging

            pageNumber = 1;
            productsPerPage = 4;
            _recipeList = recipeDAOTextFile.GetAll(productsPerPage, ref pageNumber, ref noPages);
            dataListView.ItemsSource = _recipeList;
            pageTextBox.Text = pageNumber.ToString();
            pageTextBlock.Text = noPages.ToString();

            #endregion test paging
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
            homeScreen.Visibility = Visibility.Collapsed;
            addScreen.Visibility = Visibility.Collapsed;
            favoriteScreen.Visibility = Visibility.Collapsed;
            settingScreen.Visibility = Visibility.Collapsed;
            aboutScreen.Visibility = Visibility.Collapsed;
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

        private void addStepImgBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();

            if (fd.ShowDialog() == true)
            {
                Image image = new Image();
                image.Source = new BitmapImage(
                    new Uri(fd.FileName));
                stepImage.Source = image.Source;
            }
        }

        private int stepCount = 0;
        private BindingList<AllSteps> allSteps = null;

        private class AllSteps
        {
            public string NumberOfStep { get; set; }
            public string StepDesc { set; get; }
            public BitmapImage StepPathImage { set; get; }
        }

        private void addStepBtn_Click(object sender, RoutedEventArgs e)
        {
            stepCount++;
            Image clone = new Image();
            clone.Source = stepImage.Source;

            if (allSteps == null)
            {
                allSteps = new BindingList<AllSteps>();
            }

            allSteps.Add(new AllSteps
            {
                NumberOfStep = $"Bước {stepCount}",
                StepDesc = stepDescription.Text,
                StepPathImage = (BitmapImage)clone.Source
            });

            Debug.WriteLine(allSteps[stepCount - 1].StepPathImage);
            Debug.WriteLine(((ImageBrush)addImgBtn.Background).ImageSource);
            allStepListView.ItemsSource = allSteps;

            stepDescription.Clear();
            stepImage.Source = null;
        }

        private void saveAddBtn_Click(object sender, RoutedEventArgs e)
        {
            stepCount = 0;

            MessageBoxResult choice = MessageBox.Show("Bạn có chắc muốn lưu?",
                                                        "Thông báo",
                                                        MessageBoxButton.YesNo,
                                                        MessageBoxImage.Question);

            if (choice == MessageBoxResult.Yes)
            {
                //các bước lưu

                Debug.WriteLine(title.Text);

                title.Clear();
                description.Clear();
                yt.Clear();
                addImgBtn.Background = null;
                stepDescription.Clear();
                addImgBtn.Background = Brushes.White;
                stepImage.Source = null;
                allSteps = null;
                allStepListView.ItemsSource = null;
                allStepListView.Items.Clear();

                MessageBox.Show("Đã lưu thành công",
                                "Thông báo",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
            }
            else if (choice == MessageBoxResult.No)
            {
                //do nothing
            }
        }

        private void cancelAddBtn_Click(object sender, RoutedEventArgs e)
        {
            stepCount = 0;

            MessageBoxResult choice = MessageBox.Show("Bạn có chắc muốn hủy?",
                                                       "Thông báo",
                                                       MessageBoxButton.YesNo,
                                                       MessageBoxImage.Question);

            if (choice == MessageBoxResult.Yes)
            {
                title.Clear();
                description.Clear();
                yt.Clear();
                addImgBtn.Background = null;
                stepDescription.Clear();
                //addStepImgBtn.Background = null;
                addImgBtn.Background = Brushes.White;
                stepImage.Source = null;
                allSteps = null;
                allStepListView.ItemsSource = null;
                allStepListView.Items.Clear();
            }
            else if (choice == MessageBoxResult.No)
            {
                //do nothing
            }

            //
        }

        private void pageTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (pageTextBlock == null)
            {
                Debug.WriteLine("pageTextBlock null");
                return;
            }

            if (pageTextBox.Text == "")
            {
                Debug.WriteLine("text box is null");
            }
            else
            {
                int pageNumber = 0;
                int totalPage = 0;

                bool successParsePageNumber = int.TryParse(pageTextBox.Text, out pageNumber);
                bool successParseTotalPage = int.TryParse(pageTextBlock.Text, out totalPage);

                if (!successParseTotalPage)
                {
                    Debug.WriteLine("cant parse total page");
                    return;
                }

                if (!successParsePageNumber)
                {
                    pageTextBox.Text = pageTextBox.Text.Remove(pageTextBox.Text.Length - 1);
                    pageTextBox.CaretIndex = pageTextBox.Text.Length;
                }
                else
                {
                    if (pageNumber > totalPage)
                    {
                        pageTextBox.Text = pageTextBox.Text.Remove(pageTextBox.Text.Length - 1);
                        pageTextBox.CaretIndex = pageTextBox.Text.Length;
                    }
                }
            }
        }

        private int curentPage = 1;

        private void pageTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Enter)
            {
                Debug.WriteLine("chua nhan enter");
                return;
            }

            Debug.WriteLine("da nhan enter");

            if (pageTextBox.Text == "")
            {
                pageTextBox.Text = curentPage.ToString();
            }
            else
            {
                curentPage = int.Parse(pageTextBox.Text);
                var recipeDAOTextFile = new RecipeDAOTextFile();
                pageNumber = curentPage;
                _recipeList = recipeDAOTextFile.GetAll(productsPerPage, ref pageNumber, ref noPages);
                dataListView.ItemsSource = _recipeList;
            }

            pageTextBox.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }

        private void pageTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (pageTextBox.Text == "")
            {
                pageTextBox.Text = curentPage.ToString();
            }
        }

        private void nextPageBtn_Click(object sender, RoutedEventArgs e)
        {
            var recipeDAOTextFile = new RecipeDAOTextFile();
            pageNumber++;
            curentPage = pageNumber;
            _recipeList = recipeDAOTextFile.GetAll(productsPerPage, ref pageNumber, ref noPages);
            pageTextBox.Text = pageNumber.ToString();
            dataListView.ItemsSource = _recipeList;
        }

        private void backPageBtn_Click(object sender, RoutedEventArgs e)
        {
            var recipeDAOTextFile = new RecipeDAOTextFile();
            pageNumber--;
            curentPage = pageNumber;
            _recipeList = recipeDAOTextFile.GetAll(productsPerPage, ref pageNumber, ref noPages);
            pageTextBox.Text = pageNumber.ToString();
            dataListView.ItemsSource = _recipeList;
        }

        private void search_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = search.Text;
            var recipeDAOTextFile = new RecipeDAOTextFile();
            _searchRecipeList = recipeDAOTextFile.Search(text);
            searchListView.ItemsSource = _searchRecipeList;
            Debug.WriteLine("da doi");
        }

        private void search_GotFocus(object sender, RoutedEventArgs e)
        {
            searchListView.Visibility = Visibility.Visible;
        }

        private void search_LostFocus(object sender, RoutedEventArgs e)
        {
            searchListView.Visibility = Visibility.Hidden;
        }

        private void recipeBtn_Click(object sender, RoutedEventArgs e)
        {
            var buttonItem = sender as Button;
            var stringToCompare = buttonItem.DataContext.ToString();
            var recipe = new Recipe();
            var recipeToCompare = recipe.Parse(stringToCompare);
            var resultList = new BindingList<Recipe>();

            var dao = new RecipeDAOTextFile();
            var fullList = dao.GetAll();
            foreach (var i in fullList)
            {
                if (i.RecipeID == recipeToCompare.RecipeID)
                {
                    resultList.Add(i);
                }
            }

            detailListView.ItemsSource = resultList;
            foodDetail.Visibility = Visibility.Visible;
        }

        private void recipeSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            var buttonItem = sender as Button;
            var stringToCompare = buttonItem.DataContext.ToString();
            var recipe = new Recipe();
            var recipeToCompare = recipe.Parse(stringToCompare);
            var resultList = new BindingList<Recipe>();

            var dao = new RecipeDAOTextFile();
            var fullList = dao.GetAll();
            foreach (var i in fullList)
            {
                if (i.RecipeID == recipeToCompare.RecipeID)
                {
                    resultList.Add(i);
                }
            }

            detailListView.ItemsSource = resultList;
            foodDetail.Visibility = Visibility.Visible;
        }

        private void homeScreen_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine("hoho");
            homeScreen.Focus();
        }

        private void homeScreen_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine("hoho1");
            homeScreen.Focus();
        }

        private void recipeSearchBtn_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var buttonItem = sender as Button;
            var stringToCompare = buttonItem.DataContext.ToString();
            var recipe = new Recipe();
            var recipeToCompare = recipe.Parse(stringToCompare);
            var resultList = new BindingList<Recipe>();

            var dao = new RecipeDAOTextFile();
            var fullList = dao.GetAll();
            foreach (var i in fullList)
            {
                if (i.RecipeID == recipeToCompare.RecipeID)
                {
                    resultList.Add(i);
                }
            }

            detailListView.ItemsSource = resultList;
            foodDetail.Visibility = Visibility.Visible;
        }

        private void checkFavoriteTBtn_Checked(object sender, RoutedEventArgs e)
        {
            var buttonItem = sender as ToggleButton;

            if (buttonItem.Name == "checkFavoriteTBtn")
            {
                var stringToAdd = buttonItem.DataContext.ToString();
                var recipe = new Recipe();
                var recipeToAdd = recipe.Parse(stringToAdd);

                var favDAO = new FavoriteRecipeDAOTextFile();
                favDAO.Add(recipeToAdd);

                _favoriteRecipeList = favDAO.GetAll();
                favoriteListView.ItemsSource = _favoriteRecipeList;

                var recipeDAO = new RecipeDAOTextFile();
                _recipeList = recipeDAO.GetAll(productsPerPage, ref pageNumber, ref noPages);
                dataListView.ItemsSource = _recipeList;
            }
        }

        private void checkFavoriteTBtn_Unchecked(object sender, RoutedEventArgs e)
        {
            var buttonItem = sender as ToggleButton;

            if (buttonItem.DataContext != null)
            {
                var stringToCompare = buttonItem.DataContext.ToString();
                var recipe = new Recipe();
                var recipeToCompare = recipe.Parse(stringToCompare);

                var favDAO = new FavoriteRecipeDAOTextFile();
                var fullList = favDAO.GetAll();
                foreach (var i in fullList)
                {
                    if (i.RecipeID == recipeToCompare.RecipeID)
                    {
                        favDAO.Delete(i);
                    }
                }

                _favoriteRecipeList = favDAO.GetAll();
                favoriteListView.ItemsSource = _favoriteRecipeList;

                var recipeDAO = new RecipeDAOTextFile();
                _recipeList = recipeDAO.GetAll(productsPerPage, ref pageNumber, ref noPages);
                dataListView.ItemsSource = _recipeList;
            }
        }

        private void checkFavoriteFavScreenTBtn_Unchecked(object sender, RoutedEventArgs e)
        {
            var buttonItem = sender as ToggleButton;

            if (buttonItem.DataContext != null)
            {
                var stringToCompare = buttonItem.DataContext.ToString();
                var recipe = new Recipe();
                var recipeToCompare = recipe.Parse(stringToCompare);

                var favDAO = new FavoriteRecipeDAOTextFile();
                var fullList = favDAO.GetAll();
                foreach (var i in fullList)
                {
                    if (i.RecipeID == recipeToCompare.RecipeID)
                    {
                        favDAO.Delete(i);
                    }
                }

                _favoriteRecipeList = favDAO.GetAll();
                favoriteListView.ItemsSource = _favoriteRecipeList;

                var recipeDAO = new RecipeDAOTextFile();
                _recipeList = recipeDAO.GetAll(productsPerPage, ref pageNumber, ref noPages);
                dataListView.ItemsSource = _recipeList;
            }
        }
    }
}