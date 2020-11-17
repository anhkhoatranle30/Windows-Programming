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
using Test_project1;

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
        //private BindingList<Recipe> _splashScreenRecipe;

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

        //end functional

        #endregion functional

        public Random _rng = new Random();

        public MainWindow()
        {
            InitializeComponent();

            //var value = ConfigurationManager.AppSettings["productsPerPage"];

            //var recipeDAOTextFile = new RecipeDAOTextFile();
            //_recipeList = recipeDAOTextFile.GetAll();
            //var favoriteRecipeDAOTextFile = new FavoriteRecipeDAOTextFile();
            //_favoriteRecipeList = favoriteRecipeDAOTextFile.GetAll();

            //dataListView.ItemsSource = _recipeList;
            //favoriteListView.ItemsSource = _favoriteRecipeList;

            //#region test paging

            //pageNumber = 1;

            //int index = 2;
            //var successParse = int.TryParse(value, out index);

            //if (successParse == false)
            //{
            //    productsPerPage = 8;
            //}
            //else
            //{
            //}

            //_recipeList = recipeDAOTextFile.GetAll(productsPerPage, ref pageNumber, ref noPages);
            //dataListView.ItemsSource = _recipeList;

            //UpdatePageNumber();

            //#endregion test paging
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
            detailFavListView.ItemsSource = null;
            detailListView.ItemsSource = null;
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
            backToHomeBtn_Click(sender, e);
            searchResultEnterKeydown.Visibility = Visibility.Hidden;

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
            backToFavBtn_Click(sender, e);
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

        private void orangeColor_Checked(object sender, RoutedEventArgs e)
        {
            _backgroundColor.Color = "Orange";
            _backgroundColor.SolidColor = new SolidColorBrush(Colors.Orange);

            settingBtn_Click(sender, e);
            setDefaultColor("orangeColor");
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
            //

            var recipeDAOTextFile = new RecipeDAOTextFile();
            _recipeList = recipeDAOTextFile.GetAll();
            var favoriteRecipeDAOTextFile = new FavoriteRecipeDAOTextFile();
            _favoriteRecipeList = favoriteRecipeDAOTextFile.GetAll();

            dataListView.ItemsSource = _recipeList;
            favoriteListView.ItemsSource = _favoriteRecipeList;

            #region test paging

            pageNumber = 1;

            var valuePpP = ConfigurationManager.AppSettings["indexComboBox"];
            int index = int.Parse(valuePpP);
            cboPpP.SelectedIndex = index;
            ComboBoxItem typeItem = (ComboBoxItem)cboPpP.SelectedItem;
            int numPpP = int.Parse(typeItem.Content.ToString());

            productsPerPage = numPpP;

            _recipeList = recipeDAOTextFile.GetAll(productsPerPage, ref pageNumber, ref noPages);
            dataListView.ItemsSource = _recipeList;

            UpdatePageNumber();

            #endregion test paging

            //
            var appName = System.Diagnostics.Process.GetCurrentProcess().ProcessName + ".exe";

            using (var Key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION", true))
                Key.SetValue(appName, 99999, RegistryValueKind.DWord);

            if (_backgroundColor == null)
            {
                _backgroundColor = new BackgroundColor
                {
                    Color = "#3498DB",
                    SolidColor = new SolidColorBrush(Color.FromRgb(52, 152, 219))
                };
            }

            this.DataContext = _backgroundColor;
            var valueColor = ConfigurationManager.AppSettings["colorDefault"];
            var myRadioButton = (RadioButton)this.FindName(valueColor);

            myRadioButton.IsChecked = true;
            homeBtn_Click(sender, e);

            var splash = ConfigurationManager.AppSettings["ShowSplashScreen"];
            var showSplash = bool.Parse(splash);

            splashScreen.IsChecked = !showSplash;
        }

        private bool isChooseImage = false;

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

                isChooseImage = true;
            }
        }

        private void addStepImgBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Multiselect = true;

            if (fd.ShowDialog() == true)
            {
                var files = fd.FileNames;
                BindingList<string> source = new BindingList<string>();

                foreach (var file in files)
                {
                    var info = new FileInfo(file);

                    source.Add(file);
                }

                imageItems.ItemsSource = source;
            }
        }

        private int stepCount = 0;
        private BindingList<AllSteps> allSteps = null;

        private class AllSteps
        {
            public string NumberOfStep { get; set; }
            public string StepDesc { set; get; }
            public BindingList<string> StepImgs { set; get; }
        }

        private void addStepBtn_Click(object sender, RoutedEventArgs e)
        {
            if (stepDescription.Text == "")
            {
                MessageBox.Show("Vui lòng điền mô tả bước làm món ăn!",
                                "Thông báo",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
            }
            else if (imageItems.Items == null)
            {
                MessageBox.Show("Vui lòng chọn ảnh trong bước làm món ăn!",
                                "Thông báo",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
            }
            else
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
                    StepImgs = (BindingList<string>)imageItems.ItemsSource
                });

                allStepListView.ItemsSource = allSteps;

                stepDescription.Clear();
                imageItems.ItemsSource = null;
            }
        }

        private void saveAddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (title.Text == "")
            {
                MessageBox.Show("Vui lòng điền tên món ăn!",
                                "Thông báo",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
            }
            else if (description.Text == "")
            {
                MessageBox.Show("Vui lòng điền mô tả món ăn!",
                                "Thông báo",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
            }
            else if (yt.Text == "")
            {
                MessageBox.Show("Vui lòng điền link youtube hướng dẫn món ăn!",
                                "Thông báo",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
            }
            else if (isChooseImage == false)
            {
                MessageBox.Show("Vui lòng chọn hình ảnh món ăn!",
                                "Thông báo",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
            }
            else
            {
                stepCount = 0;

                #region Add Functional

                //
                var recipeDAOTextFile = new RecipeDAOTextFile();

                #region recipe builder

                var recipeBuilder = new ConcreteRecipeBuilder();
                recipeBuilder.setRecipeID(0);
                recipeBuilder.setTitle(title.Text);
                recipeBuilder.setDesPicture(PathString.ParseSystemPath(((ImageBrush)addImgBtn.Background).ImageSource.ToString()));
                recipeBuilder.setDescription(description.Text);
                recipeBuilder.setVideoLink(yt.Text);
                recipeBuilder.setIsFavorite(false);

                // StepsList
                var stepsList = new BindingList<Step>();
                for (int i = 0; i < allSteps.Count; i++)
                {
                    var stepBuilder = new ConcreteStepBuilder();
                    stepBuilder.setRecipeID(0);
                    stepBuilder.setStepID(i + 1);
                    stepBuilder.setContent(allSteps[i].StepDesc);
                    stepBuilder.setImgSourceList(allSteps[i].StepImgs);
                    stepsList.Add(stepBuilder.Build());
                }
                recipeBuilder.setStepsList(stepsList);
                var recipe = recipeBuilder.Build();

                #endregion recipe builder

                //

                #endregion Add Functional

                MessageBoxResult choice = MessageBox.Show("Bạn có chắc muốn lưu?",
                                                            "Thông báo",
                                                            MessageBoxButton.YesNo,
                                                            MessageBoxImage.Question);

                if (choice == MessageBoxResult.Yes)
                {
                    //các bước lưu

                    //Add to database
                    recipeDAOTextFile.Add(recipe);
                    //end adding to db
                    //Bind list to Home screen
                    _recipeList = recipeDAOTextFile.GetAll(productsPerPage, ref pageNumber, ref noPages);
                    dataListView.ItemsSource = _recipeList;
                    //end binding list

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

        //private void pageTextBox_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (pageTextBlock == null)
        //    {
        //        Debug.WriteLine("pageTextBlock null");
        //        return;
        //    }

        //    if (pageTextBox.Text == "")
        //    {
        //        Debug.WriteLine("text box is null");
        //    }
        //    else
        //    {
        //        int pageNumber = 0;
        //        int totalPage = 0;

        //        bool successParsePageNumber = int.TryParse(pageTextBox.Text, out pageNumber);
        //        bool successParseTotalPage = int.TryParse(pageTextBlock.Text, out totalPage);

        //        if (!successParseTotalPage)
        //        {
        //            Debug.WriteLine("cant parse total page");
        //            return;
        //        }

        //        if (!successParsePageNumber)
        //        {
        //            pageTextBox.Text = pageTextBox.Text.Remove(pageTextBox.Text.Length - 1);
        //            pageTextBox.CaretIndex = pageTextBox.Text.Length;
        //        }
        //        else
        //        {
        //            if (pageNumber > totalPage)
        //            {
        //                pageTextBox.Text = pageTextBox.Text.Remove(pageTextBox.Text.Length - 1);
        //                pageTextBox.CaretIndex = pageTextBox.Text.Length;
        //            }
        //        }
        //    }
        //}

        //private int curentPage = 1;

        //private void pageTextBox_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Key != System.Windows.Input.Key.Enter)
        //    {
        //        Debug.WriteLine("chua nhan enter");
        //        return;
        //    }

        //    Debug.WriteLine("da nhan enter");

        //    if (pageTextBox.Text == "")
        //    {
        //        pageTextBox.Text = curentPage.ToString();
        //    }
        //    else
        //    {
        //        curentPage = int.Parse(pageTextBox.Text);
        //        var recipeDAOTextFile = new RecipeDAOTextFile();
        //        pageNumber = curentPage;
        //        _recipeList = recipeDAOTextFile.GetAll(productsPerPage, ref pageNumber, ref noPages);
        //        dataListView.ItemsSource = _recipeList;
        //    }

        //    pageTextBox.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        //}

        //private void pageTextBox_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    if (pageTextBox.Text == "")
        //    {
        //        pageTextBox.Text = curentPage.ToString();
        //    }
        //}

        //private void nextPageBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    var recipeDAOTextFile = new RecipeDAOTextFile();
        //    pageNumber++;
        //    curentPage = pageNumber;
        //    _recipeList = recipeDAOTextFile.GetAll(productsPerPage, ref pageNumber, ref noPages);
        //    pageTextBox.Text = pageNumber.ToString();
        //    dataListView.ItemsSource = _recipeList;
        //}

        //private void backPageBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    var recipeDAOTextFile = new RecipeDAOTextFile();
        //    pageNumber--;
        //    curentPage = pageNumber;
        //    _recipeList = recipeDAOTextFile.GetAll(productsPerPage, ref pageNumber, ref noPages);
        //    pageTextBox.Text = pageNumber.ToString();
        //    dataListView.ItemsSource = _recipeList;
        //}

        private void search_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = search.Text;
            var recipeDAOTextFile = new RecipeDAOTextFile();
            _searchRecipeList = recipeDAOTextFile.Search(text);
            searchListView.ItemsSource = _searchRecipeList;

            if (_searchRecipeList.Count == 0 && search.Text != "")
            {
                noResult.Visibility = Visibility.Visible;
            }
            else
            {
                noResult.Visibility = Visibility.Collapsed;
            }
        }

        private void search_GotFocus(object sender, RoutedEventArgs e)
        {
            searchListView.Visibility = Visibility.Visible;
        }

        private void search_LostFocus(object sender, RoutedEventArgs e)
        {
            searchListView.Visibility = Visibility.Collapsed;
            noResult.Visibility = Visibility.Collapsed;
        }

        private void recipeBtn_Click(object sender, RoutedEventArgs e)
        {
            var buttonItem = sender as Button;
            var stringToCompare = buttonItem.DataContext.ToString();
            var recipe = new ConcreteRecipeBuilder().BuildFromString(stringToCompare);
            var recipeIDToCompare = recipe.RecipeID;
            var resultList = new BindingList<Recipe>();

            var fullList = new RecipeDAOTextFile().GetAll();
            foreach (var i in fullList)
            {
                if (i.RecipeID == recipeIDToCompare)
                {
                    resultList.Add(i);
                }
            }

            detailListView.ItemsSource = resultList;

            var detailStepsList = UncompressedStep.ToUncrompressStepList(resultList[0].StepsList);
            detailStepsListView.ItemsSource = detailStepsList;

            foreach (var i in detailStepsList[0].ImgSource)
            {
                Debug.WriteLine("debug cai anh: " + i);
            }

            foodDetail.Visibility = Visibility.Visible;
            home.Visibility = Visibility.Hidden;
            searchResultEnterKeydown.Visibility = Visibility.Hidden;
        }

        private void recipeSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            var buttonItem = sender as Button;
            var stringToCompare = buttonItem.DataContext.ToString();
            var recipe = new Recipe();
            var recipeIDToCompare = recipe.RecipeID;
            var resultList = new BindingList<Recipe>();

            var dao = new RecipeDAOTextFile();
            var fullList = dao.GetAll();
            foreach (var i in fullList)
            {
                if (i.RecipeID == recipeIDToCompare)
                {
                    resultList.Add(i);
                }
            }

            detailListView.ItemsSource = resultList;
            foodDetail.Visibility = Visibility.Visible;
        }

        private void recipeSearchBtn_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var buttonItem = sender as Button;
            var stringToCompare = buttonItem.DataContext.ToString();
            var recipe = new Recipe();
            var recipeIDToCompare = recipe.RecipeID;
            var resultList = new BindingList<Recipe>();

            var dao = new RecipeDAOTextFile();
            var fullList = dao.GetAll();
            foreach (var i in fullList)
            {
                if (i.RecipeID == recipeIDToCompare)
                {
                    resultList.Add(i);
                }
            }

            detailListView.ItemsSource = resultList;

            var detailStepsList = UncompressedStep.ToUncrompressStepList(resultList[0].StepsList);
            detailStepsListView.ItemsSource = detailStepsList;

            foodDetail.Visibility = Visibility.Visible;
            home.Visibility = Visibility.Hidden;
        }

        private void checkFavoriteTBtn_Checked(object sender, RoutedEventArgs e)
        {
            var buttonItem = sender as ToggleButton;

            if (buttonItem.Name == "checkFavoriteTBtn")
            {
                var stringToAdd = buttonItem.DataContext.ToString();

                var recipeToAdd = new ConcreteRecipeBuilder().BuildFromString(stringToAdd);

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
                var recipeBuilder = new ConcreteRecipeBuilder();
                var recipe = recipeBuilder.BuildFromString(stringToCompare);
                var recipeIDToCompare = recipe.RecipeID;

                var favDAO = new FavoriteRecipeDAOTextFile();
                var fullList = favDAO.GetAll();
                foreach (var i in fullList)
                {
                    if (i.RecipeID == recipeIDToCompare)
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
                var recipeIDToCompare = recipe.RecipeID;

                var favDAO = new FavoriteRecipeDAOTextFile();
                var fullList = favDAO.GetAll();
                foreach (var i in fullList)
                {
                    if (i.RecipeID == recipeIDToCompare)
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

        private void backToHomeBtn_Click(object sender, RoutedEventArgs e)
        {
            foodDetail.Visibility = Visibility.Collapsed;
            home.Visibility = Visibility.Visible;
        }

        private void recipeFavBtn_Click(object sender, RoutedEventArgs e)
        {
            var buttonItem = sender as Button;
            var stringToCompare = buttonItem.DataContext.ToString();
            var recipe = new ConcreteRecipeBuilder().BuildFromString(stringToCompare);
            var recipeIDToCompare = recipe.RecipeID;
            var resultList = new BindingList<Recipe>();

            var dao = new RecipeDAOTextFile();
            var fullList = dao.GetAll();
            foreach (var i in fullList)
            {
                if (i.RecipeID == recipeIDToCompare)
                {
                    resultList.Add(i);
                }
            }

            detailFavListView.ItemsSource = resultList;

            var detailStepsList = UncompressedStep.ToUncrompressStepList(resultList[0].StepsList);
            detailStepsFavListView.ItemsSource = detailStepsList;

            foodFavDetail.Visibility = Visibility.Visible;
            favoriteListView.Visibility = Visibility.Hidden;
        }

        private void backToFavBtn_Click(object sender, RoutedEventArgs e)
        {
            detailFavListView.ItemsSource = null;
            foodFavDetail.Visibility = Visibility.Hidden;
            favoriteListView.Visibility = Visibility.Visible;
        }

        private void cboPpP_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem typeItem = (ComboBoxItem)cboPpP.SelectedItem;

            if (typeItem.Content != null)
            {
                string value = typeItem.Content.ToString();
                int choose = int.Parse(value);
                SetPpP(cboPpP.SelectedIndex);
                productsPerPage = choose;

                var recipeDAO = new RecipeDAOTextFile();
                _recipeList = recipeDAO.GetAll(productsPerPage, ref pageNumber, ref noPages);
                dataListView.ItemsSource = _recipeList;

                Debug.WriteLine(choose);

                UpdatePageNumber();
            }
        }

        private void SetPpP(int value)
        {
            var config = ConfigurationManager.OpenExeConfiguration(
                ConfigurationUserLevel.None);
            config.AppSettings.Settings["indexComboBox"].Value = value.ToString();
            config.Save(ConfigurationSaveMode.Minimal);
        }

        private void splashScreen_Checked(object sender, RoutedEventArgs e)
        {
            var config = ConfigurationManager.OpenExeConfiguration(
                ConfigurationUserLevel.None);
            config.AppSettings.Settings["ShowSplashScreen"].Value = "false";
            config.Save(ConfigurationSaveMode.Minimal);
            Debug.WriteLine(config.AppSettings.Settings["ShowSplashScreen"].Value);
        }

        private void splashScreen_Unchecked(object sender, RoutedEventArgs e)
        {
            var config = ConfigurationManager.OpenExeConfiguration(
                ConfigurationUserLevel.None);
            config.AppSettings.Settings["ShowSplashScreen"].Value = "true";
            config.Save(ConfigurationSaveMode.Minimal);
            Debug.WriteLine(config.AppSettings.Settings["ShowSplashScreen"].Value);
        }

        private void search_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Enter)
            {
                Debug.WriteLine("chua nhan enter");
                return;
            }

            Debug.WriteLine("da nhan enter");

            searchResultListView.ItemsSource = searchListView.ItemsSource;
            searchResultEnterKeydown.Visibility = Visibility.Visible;
            home.Visibility = Visibility.Hidden;
            searchResultEnterKeydown.Focus();
            searchEnterKeyDown.Text = search.Text;
            resultOf.Text = "Kết quả hiện thị cho \"" + search.Text + "\":";
        }

        private void UpdatePageNumber()
        {
            if (noPages <= 2)
            {
                if (noPages == 1)
                {
                    curPage.Content = "1";
                    prePage.Visibility = Visibility.Hidden;
                    nextPage.Visibility = Visibility.Hidden;
                }
                else if (noPages == 2)
                {
                    prePage.Content = "1";
                    curPage.Content = "2";
                    nextPage.Visibility = Visibility.Hidden;
                }

                firstPage.Visibility = Visibility.Hidden;
                lastPage.Visibility = Visibility.Hidden;
            }
            else
            {
                if (pageNumber == 1)
                {
                    prePage.Content = pageNumber.ToString();
                    prePage.Visibility = Visibility.Visible;
                    //curPage.Content = (pageNumber + 1).ToString();
                    //nextPage.Content = (pageNumber + 2).ToString();

                    if (pageNumber + 1 > noPages)
                    {
                        curPage.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        curPage.Content = (pageNumber + 1).ToString();
                        curPage.Visibility = Visibility.Visible;
                    }

                    if (pageNumber + 2 > noPages)
                    {
                        nextPage.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        nextPage.Content = (pageNumber + 2).ToString();
                        nextPage.Visibility = Visibility.Visible;
                    }

                    if (pageNumber >= 3)
                    {
                        firstPage.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        firstPage.Visibility = Visibility.Hidden;
                    }

                    if (pageNumber <= noPages - 2 && pageNumber + 2 != noPages)
                    {
                        lastPage.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        lastPage.Visibility = Visibility.Hidden;
                    }
                }
                else if (pageNumber == noPages)
                {
                    nextPage.Content = pageNumber.ToString();
                    nextPage.Visibility = Visibility.Visible;

                    if (pageNumber - 1 <= 0)
                    {
                        curPage.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        curPage.Content = (pageNumber - 1).ToString();
                        curPage.Visibility = Visibility.Visible;
                    }

                    if (pageNumber - 2 <= 0)
                    {
                        prePage.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        prePage.Content = (pageNumber - 2).ToString();
                        prePage.Visibility = Visibility.Visible;
                    }

                    //prePage.Content = (pageNumber - 2).ToString();
                    //curPage.Content = (pageNumber - 1).ToString();
                    if (pageNumber >= 3 && pageNumber - 2 > 1)
                    {
                        firstPage.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        firstPage.Visibility = Visibility.Hidden;
                    }

                    if (pageNumber <= noPages - 2)
                    {
                        lastPage.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        lastPage.Visibility = Visibility.Hidden;
                    }
                }
                else
                {
                    curPage.Content = pageNumber.ToString();
                    curPage.Visibility = Visibility.Visible;
                    //prePage.Content = (pageNumber - 1).ToString();
                    //nextPage.Content = (pageNumber + 1).ToString();

                    if (pageNumber - 1 <= 0)
                    {
                        prePage.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        prePage.Content = (pageNumber - 1).ToString();
                        prePage.Visibility = Visibility.Visible;
                    }

                    if (pageNumber + 1 > noPages)
                    {
                        nextPage.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        nextPage.Content = (pageNumber + 1).ToString();
                        nextPage.Visibility = Visibility.Visible;
                    }

                    if (pageNumber >= 3)
                    {
                        firstPage.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        firstPage.Visibility = Visibility.Hidden;
                    }

                    if (pageNumber <= noPages - 2)
                    {
                        lastPage.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        lastPage.Visibility = Visibility.Hidden;
                    }

                    curPage.IsChecked = true;
                }
                totalPage.Content = noPages.ToString();
            }
        }

        private bool isChoosePpP = false;

        private void prePage_Click(object sender, RoutedEventArgs e)
        {
            if (pageNumber == 1)
            {
                //do nothing
            }
            else if (pageNumber == noPages)
            {
                var recipeDAOTextFile = new RecipeDAOTextFile();

                if (noPages == 2)
                {
                    pageNumber -= 1;
                }
                else
                {
                    pageNumber -= 2;
                }

                _recipeList = recipeDAOTextFile.GetAll(productsPerPage, ref pageNumber, ref noPages);
                dataListView.ItemsSource = _recipeList;
                UpdatePageNumber();
            }
            else
            {
                var recipeDAOTextFile = new RecipeDAOTextFile();
                pageNumber--;
                _recipeList = recipeDAOTextFile.GetAll(productsPerPage, ref pageNumber, ref noPages);
                dataListView.ItemsSource = _recipeList;
                UpdatePageNumber();
            }
        }

        private void curPage_Click(object sender, RoutedEventArgs e)
        {
            if (pageNumber == 1)
            {
                var recipeDAOTextFile = new RecipeDAOTextFile();
                pageNumber++;
                _recipeList = recipeDAOTextFile.GetAll(productsPerPage, ref pageNumber, ref noPages);
                dataListView.ItemsSource = _recipeList;
                UpdatePageNumber();
            }
            else if (pageNumber == noPages)
            {
                var recipeDAOTextFile = new RecipeDAOTextFile();
                pageNumber -= 1;
                _recipeList = recipeDAOTextFile.GetAll(productsPerPage, ref pageNumber, ref noPages);
                dataListView.ItemsSource = _recipeList;
                UpdatePageNumber();
            }
            else
            {
                //do nothing
            }
        }

        private void nextPage_Click(object sender, RoutedEventArgs e)
        {
            if (pageNumber == 1)
            {
                var recipeDAOTextFile = new RecipeDAOTextFile();
                pageNumber += 2;
                _recipeList = recipeDAOTextFile.GetAll(productsPerPage, ref pageNumber, ref noPages);
                dataListView.ItemsSource = _recipeList;
                UpdatePageNumber();
            }
            else if (pageNumber == noPages)
            {
                //do nothing
            }
            else
            {
                var recipeDAOTextFile = new RecipeDAOTextFile();
                pageNumber += 1;
                _recipeList = recipeDAOTextFile.GetAll(productsPerPage, ref pageNumber, ref noPages);
                dataListView.ItemsSource = _recipeList;
                UpdatePageNumber();
            }
        }

        private void totalPage_Click(object sender, RoutedEventArgs e)
        {
            var recipeDAOTextFile = new RecipeDAOTextFile();
            pageNumber = noPages;
            _recipeList = recipeDAOTextFile.GetAll(productsPerPage, ref pageNumber, ref noPages);
            dataListView.ItemsSource = _recipeList;
            nextPage.IsChecked = true;
            UpdatePageNumber();
        }

        private void page1_Click(object sender, RoutedEventArgs e)
        {
            var recipeDAOTextFile = new RecipeDAOTextFile();
            pageNumber = 1;
            _recipeList = recipeDAOTextFile.GetAll(productsPerPage, ref pageNumber, ref noPages);
            dataListView.ItemsSource = _recipeList;
            prePage.IsChecked = true;
            UpdatePageNumber();
        }

        private void homeScreen_MouseDown(object sender, MouseButtonEventArgs e)
        {
            homeScreen.Focus();
        }

        private void searchEnterKeyDown_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Enter)
            {
                Debug.WriteLine("chua nhan enter");
                return;
            }

            string text = searchEnterKeyDown.Text;
            var recipeDAOTextFile = new RecipeDAOTextFile();
            _searchRecipeList = recipeDAOTextFile.Search(text);
            searchResultListView.ItemsSource = _searchRecipeList;

            if (_searchRecipeList.Count == 0)
            {
                resultOf.Text = "Không có kết quả hiện thị phù hợp cho \"" + searchEnterKeyDown.Text + "\"";
            }
            else
            {
                resultOf.Text = "Kết quả hiện thị cho \"" + searchEnterKeyDown.Text + "\":";
            }
        }

        private void backToHomeBtnWhenClickEnterKeyDown_Click(object sender, RoutedEventArgs e)
        {
            searchResultEnterKeydown.Visibility = Visibility.Hidden;
            home.Visibility = Visibility.Visible;
        }
    }
}