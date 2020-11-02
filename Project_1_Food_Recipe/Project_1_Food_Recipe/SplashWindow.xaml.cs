using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Project_1_Food_Recipe
{
    /// <summary>
    /// Interaction logic for SplashWindow.xaml
    /// </summary>
    public partial class SplashWindow : Window
    {
        #region functional

        //functional

        #region global variables

        //Lists
        private BindingList<Recipe> _recipeList;

        private BindingList<Recipe> _favoriteRecipeList;
        private BindingList<Recipe> _searchRecipeList;
        private BindingList<Recipe> _splashScreenRecipe;

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

            public abstract BindingList<Recipe> GetAll(int productsPerPage, ref int pageNumber, ref int noPages);

            public abstract Recipe CreateRecipe(String title, String desPicture, String description,
                String videoLink, BindingList<Step> stepsList /*, bool isFavorite*/);

            public abstract void Add(Recipe recipe);

            public abstract void Delete(Recipe recipe);

            public abstract BindingList<Recipe> Search(String searchString);
        }

        #endregion abstract DAO classes

        #region definition classes

        public class PathString
        {
            /// <summary>
            /// Hàm tách từ chuỗi địa chỉ file của hệ thống thành Đường dẫn tuyệt đối
            /// </summary>
            /// <param name="toParseString">đường dẫn hệ thống</param>
            /// <returns>Trả về đường dẫn tuyệt đối</returns>
            public static string ParseSystemPath(string toParseString)
            {
                // path : file: + /// + {absolutePath}
                var tokens = toParseString.Split(new String[] { "file:///" }, StringSplitOptions.RemoveEmptyEntries);
                var result = new StringBuilder();
                result.Append(tokens[0]);
                return result.ToString();
            }
        }

        public class SearchString
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
                foreach (var token in tokens)
                {
                    if (toSearchString.Contains(token))
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

        public class UncompressedStep : Step
        {
            public string StepNumber { get; set; }
            //Methods

            public static BindingList<UncompressedStep> ToUncrompressStepList(BindingList<Step> stepsList)
            {
                var result = new BindingList<UncompressedStep>();
                for (int i = 0; i < stepsList.Count; i++)
                {
                    var stepnumber = new StringBuilder();
                    stepnumber.Append("Bước " + (i + 1).ToString() + ": ");
                    result.Add(new UncompressedStep() { Content = stepsList[i].Content, ImgSource = stepsList[i].ImgSource, StepNumber = stepnumber.ToString() });
                }
                return result;
            }
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
            public override BindingList<Recipe> GetAll(int productsPerPage, ref int pageNumber, ref int noPages)
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
                pageNumber = pageNumber % noPages + ((pageNumber % noPages == 0) ? noPages : 0);
                //end page
                for (int i = productsPerPage * (pageNumber - 1); i < productsPerPage * pageNumber && i < total; i++)
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
                foreach (var recipe in fullList)
                {
                    if (recipe.IsFavorite == true)
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
            public override BindingList<Recipe> GetAll(int productsPerPage, ref int pageNumber, ref int noPages)
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
                pageNumber = pageNumber % noPages + ((pageNumber % noPages == 0) ? noPages : 0);
                //end page
                for (int i = productsPerPage * (pageNumber - 1); i < productsPerPage * pageNumber && i < total; i++)
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

        public Random _rng = new Random();

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
                //splash screen random dish
                var recipeDAOTextFile = new RecipeDAOTextFile();
                _recipeList = recipeDAOTextFile.GetAll();
                int index = _rng.Next(_recipeList.Count);
                _splashScreenRecipe = new BindingList<Recipe>();
                _splashScreenRecipe.Add(_recipeList[index]);
                splashListVIew.ItemsSource = _splashScreenRecipe;
                //end splash screen
                _dispatcherTimer = new DispatcherTimer();
                _dispatcherTimer.Tick += _dispatcherTimer_Tick;
                _dispatcherTimer.Interval = TimeSpan.FromMilliseconds(1);
                _dispatcherTimer.Start();
            }
        }

        private DispatcherTimer _dispatcherTimer;
        private int _counterTime = 0;
        private int _target = 320;

        private void _dispatcherTimer_Tick(object sender, EventArgs e)
        {
            _counterTime++;

            if (_counterTime == _target + 2)
            {
                _dispatcherTimer.Stop();

                var screen = new MainWindow();
                screen.Show();

                this.Close();
            }

            progressbar.Value = _counterTime * 0.3125;
        }
    }
}