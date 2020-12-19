using System;
using System.ComponentModel;
using System.IO;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Xml;
using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.Win32;
using ViewModel.Pagination;
using System.Windows.Controls.Primitives;
using System.Configuration;
using System.Windows.Media.Imaging;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace We_Split
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly PagingCollectionView _tripsView;

        public MainWindow()
        {
            InitializeComponent();
        }

        public Func<ChartPoint, string> PointLabel { set; get; }

        private void miniWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void maxWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Maximized;
            maxWindow.Visibility = Visibility.Collapsed;
            restoreWindow.Visibility = Visibility.Visible;
        }

        private void closeWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void restoreWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Normal;
            maxWindow.Visibility = Visibility.Visible;
            restoreWindow.Visibility = Visibility.Collapsed;
        }

        private void ClearBtnBg(Button btn)
        {
            Border _border = btn.Template.FindName("border", btn) as Border;

            if (_border != null)
            {
                _border.ClearValue(BackgroundProperty);
            }
        }

        private void SetNoCorner(Button btn)
        {
            Border _border = btn.Template.FindName("border", btn) as Border;

            if (_border != null)
            {
                _border.CornerRadius = new CornerRadius(0);
            }
        }

        private void allBtn_Click(object sender, RoutedEventArgs e)
        {
            HideSearchCondition();
            ClearBtnBg(doneBtn);
            ClearBtnBg(planningBtn);
            ClearBtnBg(onGoingBtn);
            SetNoCorner(doneBtn);
            SetNoCorner(onGoingBtn);
            Border _border = allBtn.Template.FindName("border", allBtn) as Border;

            if (_border != null)
            {
                _border.CornerRadius = new CornerRadius(18, 18, 0, 0);
                _border.Background = Brushes.Black;
            }

            _border = onGoingBtn.Template.FindName("border", planningBtn) as Border;

            if (_border != null)
            {
                _border.CornerRadius = new CornerRadius(0, 0, 0, 18);
            }

            borderLeft.CornerRadius = new CornerRadius(0, 0, 16, 0);

            var allTripListViewSource = new TripsDAOsqlserver().GetAll();
            allTripListView.ItemsSource = allTripListViewSource;
        }

        private void planningBtn_Click(object sender, RoutedEventArgs e)
        {
            HideSearchCondition();
            ClearBtnBg(doneBtn);
            ClearBtnBg(allBtn);
            ClearBtnBg(onGoingBtn);
            SetNoCorner(doneBtn);
            borderRight.CornerRadius = new CornerRadius(0);
            borderLeft.CornerRadius = new CornerRadius(0);

            Border _border = planningBtn.Template.FindName("border", planningBtn) as Border;

            if (_border != null)
            {
                _border.CornerRadius = new CornerRadius(18, 18, 0, 0);
                _border.Background = Brushes.Black;
            }

            _border = allBtn.Template.FindName("border", allBtn) as Border;

            if (_border != null)
            {
                _border.CornerRadius = new CornerRadius(0, 0, 18, 0);
            }

            _border = doneBtn.Template.FindName("border", onGoingBtn) as Border;

            if (_border != null)
            {
                _border.CornerRadius = new CornerRadius(0, 0, 0, 18);
            }

            const string sttStr = "Lên kế hoạch";
            var allTripListViewSource = new TripsDAOsqlserver().GetAllByStatusDisplayText(sttStr);
            allTripListView.ItemsSource = allTripListViewSource;
        }

        private void onGoingBtn_Click(object sender, RoutedEventArgs e)
        {
            HideSearchCondition();
            ClearBtnBg(doneBtn);
            ClearBtnBg(allBtn);
            ClearBtnBg(planningBtn);
            SetNoCorner(allBtn);
            borderRight.CornerRadius = new CornerRadius(0);
            borderLeft.CornerRadius = new CornerRadius(0);

            Border _border = onGoingBtn.Template.FindName("border", onGoingBtn) as Border;

            if (_border != null)
            {
                _border.CornerRadius = new CornerRadius(18, 18, 0, 0);
                _border.Background = Brushes.Black;
            }

            _border = allBtn.Template.FindName("border", planningBtn) as Border;

            if (_border != null)
            {
                _border.CornerRadius = new CornerRadius(0, 0, 18, 0);
            }

            _border = doneBtn.Template.FindName("border", doneBtn) as Border;

            if (_border != null)
            {
                _border.CornerRadius = new CornerRadius(0, 0, 0, 18);
            }

            const string sttStr = "Đang đi";
            var allTripListViewSource = new TripsDAOsqlserver().GetAllByStatusDisplayText(sttStr);
            allTripListView.ItemsSource = allTripListViewSource;
        }

        private void doneBtn_Click(object sender, RoutedEventArgs e)
        {
            HideSearchCondition();
            ClearBtnBg(allBtn);
            ClearBtnBg(onGoingBtn);
            ClearBtnBg(planningBtn);
            SetNoCorner(allBtn);
            SetNoCorner(planningBtn);
            borderLeft.CornerRadius = new CornerRadius(0);

            Border _border = doneBtn.Template.FindName("border", doneBtn) as Border;

            if (_border != null)
            {
                _border.CornerRadius = new CornerRadius(18, 18, 0, 0);
                _border.Background = Brushes.Black;
            }

            _border = onGoingBtn.Template.FindName("border", onGoingBtn) as Border;

            if (_border != null)
            {
                _border.CornerRadius = new CornerRadius(0, 0, 18, 0);
            }

            borderRight.CornerRadius = new CornerRadius(0, 0, 0, 18);

            const string sttStr = "Đã đi xong";
            var allTripListViewSource = new TripsDAOsqlserver().GetAllByStatusDisplayText(sttStr);
            allTripListView.ItemsSource = allTripListViewSource;
        }

        private ListViewItem listViewItemTemp = null;
        private ListViewItem addFullTemp = null;

        private void AddNewItemAddtListView()
        {
            var addFullXaml = XamlWriter.Save(addFullTemp);
            StringReader stringReader = new StringReader(addFullXaml);
            XmlReader xmlReader = XmlReader.Create(stringReader);
            ListViewItem newItem = (ListViewItem)XamlReader.Load(xmlReader);

            addListView.Items.Add(newItem);
            var item = (ListViewItem)addListView.Items[addListView.Items.Count - 1];

            Button button = (Button)item.FindName("addCostBtn");
            button.Click += addCostBtnAll_Click;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine(statusComboBox.SelectedItem);

            PointLabel = chartPoint =>
              string.Format("{0}đ", chartPoint.Y);

            var listviewitemXaml = XamlWriter.Save(costAddListViewItem);
            StringReader stringReader = new StringReader(listviewitemXaml);
            XmlReader xmlReader = XmlReader.Create(stringReader);
            listViewItemTemp = (ListViewItem)XamlReader.Load(xmlReader);

            var addFullXaml = XamlWriter.Save(addFullListViewItem);
            stringReader = new StringReader(addFullXaml);
            xmlReader = XmlReader.Create(stringReader);
            addFullTemp = (ListViewItem)XamlReader.Load(xmlReader);

            addCostBtn.Click += addCostBtnAll_Click;
            costNameTextBox.TextChanged += AllTextBox_TextChanged;
            costValueTextBox.TextChanged += AllTextBox_TextChanged;
            memberNameTextBox.TextChanged += AllTextBox_TextChanged;

            homeBtn_Click(sender, e);
            allBtn_Click(sender, e);

            var appTripListViewSource = new TripsDAOsqlserver().GetAll();
            allTripListView.ItemsSource = appTripListViewSource;

            var value = ConfigurationManager.AppSettings["showSplashScreen"];
            var showSplash = bool.Parse(value);

            splashScreen.IsChecked = showSplash;

            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }

        private void searchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (searchCondition.Width == 0)
            {
                var animation = new DoubleAnimation();
                animation.From = 0;
                animation.To = 190;
                animation.Duration = new Duration(TimeSpan.FromSeconds(0.5));

                var myStoryboard = new Storyboard();
                myStoryboard.Children.Add(animation);
                Storyboard.SetTargetName(animation, searchCondition.Name);
                Storyboard.SetTargetProperty(animation, new PropertyPath(StackPanel.WidthProperty));
                myStoryboard.Begin(this);
            }
        }

        private void HideSearchCondition()
        {
            if (searchTextBox.IsFocused || searchCondition.Width != 0)
            {
                searchTextBox.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));

                if (searchCondition.Width != 0)
                {
                    var animation = new DoubleAnimation();
                    animation.From = 190;
                    animation.To = 0;
                    animation.Duration = new Duration(TimeSpan.FromSeconds(0.5));

                    var myStoryboard = new Storyboard();
                    myStoryboard.Children.Add(animation);
                    Storyboard.SetTargetName(animation, searchCondition.Name);
                    Storyboard.SetTargetProperty(animation, new PropertyPath(StackPanel.WidthProperty));
                    myStoryboard.Begin(this);
                }
            }
        }

        private void superGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HideSearchCondition();
        }

        private BindingList<string> tripImgSource = new BindingList<string>();

        private void addImgBtn_Click(object sender, RoutedEventArgs e)
        {
            tripImgSource = new BindingList<string>();

            OpenFileDialog fd = new OpenFileDialog();
            fd.Multiselect = true;

            if (fd.ShowDialog() == true)
            {
                var files = fd.FileNames;

                foreach (var file in files)
                {
                    var info = new FileInfo(file);

                    tripImgSource.Add(file);
                }

                imgAddListView.ItemsSource = tripImgSource;
            }
        }

        private void cancelAddImgBtn_Click(object sender, RoutedEventArgs e)
        {
            imgAddListView.ItemsSource = null;
        }

        private void addCostBtnAll_Click(object sender, RoutedEventArgs e)
        {
            //costAddListView
            StackPanel parent = (StackPanel)(VisualTreeHelper.GetParent((DependencyObject)sender));
            ListView child = (ListView)parent.FindName("costAddListView");
            ListViewItem item = (ListViewItem)child.Items[child.Items.Count - 1];
            Console.WriteLine(child.Items.Count - 1);
            var newCostNameTextBox = (TextBox)item.FindName("costNameTextBox");
            var newCostValueTextBox = (TextBox)item.FindName("costValueTextBox");

            if (newCostNameTextBox == null || newCostValueTextBox == null)
            {
                Console.WriteLine("co loi");
            }
            else
            {
                if (!(string.IsNullOrEmpty(newCostNameTextBox.Text)) && !(string.IsNullOrEmpty(newCostValueTextBox.Text)))
                {
                    var listviewitemXaml = XamlWriter.Save(listViewItemTemp);
                    StringReader stringReader = new StringReader(listviewitemXaml);
                    XmlReader xmlReader = XmlReader.Create(stringReader);
                    ListViewItem newItem = (ListViewItem)XamlReader.Load(xmlReader);

                    child.Items.Add(newItem);
                    var itemTemp = (ListViewItem)child.Items[child.Items.Count - 1];
                    var newCostNameTextBoxTemp = (TextBox)itemTemp.FindName("costNameTextBox");
                    var newCostValueTextBoxTemp = (TextBox)itemTemp.FindName("costValueTextBox");

                    newCostNameTextBoxTemp.TextChanged += AllTextBox_TextChanged;
                    newCostValueTextBoxTemp.TextChanged += AllTextBox_TextChanged;
                }
                else
                {
                    var toolTip = new ToolTip();
                    toolTip.Content = "Vui lòng nhập thông tin trước khi thêm mới";

                    if (newCostNameTextBox.Text.Length == 0)
                    {
                        newCostNameTextBox.BorderBrush = Brushes.Red;
                        newCostNameTextBox.ToolTip = toolTip;
                    }
                    else
                    {
                        //do nothing
                    }

                    if (newCostValueTextBox.Text.Length == 0)
                    {
                        newCostValueTextBox.BorderBrush = Brushes.Red;
                        newCostValueTextBox.ToolTip = toolTip;
                    }
                    else
                    {
                        //do nothing
                    }
                }
            }
        }

        private void addMemberBtn_Click(object sender, RoutedEventArgs e)
        {
            var newMemberNameTextBox = new TextBox();
            var button = new Button();

            ListViewItem item = (ListViewItem)addListView.Items[addListView.Items.Count - 1];
            newMemberNameTextBox = (TextBox)item.FindName("memberNameTextBox");
            button = (Button)item.FindName("addCostBtn");

            if (newMemberNameTextBox == null)
            {
                Console.WriteLine("co loi");
            }
            else
            {
                var toolTip = new ToolTip();
                toolTip.Content = "Vui lòng nhập thông tin trước khi thêm mới";

                if (newMemberNameTextBox.Text.Length == 0)
                {
                    newMemberNameTextBox.BorderBrush = Brushes.Red;
                    newMemberNameTextBox.ToolTip = toolTip;
                }
                else
                {
                    newMemberNameTextBox.BorderBrush = Brushes.White;
                    newMemberNameTextBox.ToolTip = null;
                    AddNewItemAddtListView();
                    item = (ListViewItem)addListView.Items[addListView.Items.Count - 1];
                    var tempMemberName = (TextBox)item.FindName("memberNameTextBox");
                    tempMemberName.TextChanged += AllTextBox_TextChanged;
                    button = (Button)item.FindName("addCostBtn");
                    button.Click += addCostBtnAll_Click;
                }
            }
        }

        private void AllTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            var toolTip = new ToolTip();
            textBox.ToolTip = toolTip;

            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.BorderBrush = Brushes.Red;
                toolTip.Content = "Vui lòng nhập thông tin trước khi thêm mới";
            }
            else
            {
                textBox.BorderBrush = Brushes.White;
                textBox.ToolTip = null;
            }
        }

        private bool isViewing = false;

        private void TripBtn_Click(object sender, RoutedEventArgs e)
        {
            isViewing = true;
            HideSearchCondition();
            tripDetailGrid.Visibility = Visibility.Visible;

            var button = sender as Button;
            var data = button.DataContext as TRIP;
            int tripIDSelected = data.TripID;
            var tripSelected = new TripsDAOsqlserver().GetTripByTripID(tripIDSelected);
            var tripImages = new TripImagesDAOsqlserver().GetTripImagesByTripID(tripIDSelected);
            var memberList = new MembersDAOsqlserver().GetAllByTripID(tripIDSelected);
            var avgPay = MyUtils.calcAverageCostTrip((int)tripIDSelected);
            var locationList = new LocationDAOsqlserver().GetLocationNameByTripID(tripIDSelected);
            //bind data
            dTripNameTxtBlock.Text = tripSelected.TripName;
            dTripImageImgBrush.ImageSource = new BitmapImage(
                                                        new Uri(tripImages[0].Path,
                                                              UriKind.Absolute));
            //keep tracking tripID
            tripIDTextBlock.Text = tripIDSelected.ToString();
            //pay list view
            var payList = new List<MEMBER>(memberList)
                                .Select(m => new
                                {
                                    MemberName = m.MemberName,
                                    Pay = MyUtils.calcTotalCostMember(tripIDSelected, m.MemberID) - avgPay
                                });
            payListView.ItemsSource = payList;
            sumCost.DataContext = MyUtils.calcTotalCostTrip(tripIDSelected).ToString();
            avgCost.DataContext = MyUtils.calcAverageCostTrip(tripIDSelected).ToString();
            //location
            var locationStringB = new StringBuilder();
            foreach (var location in locationList)
            {
                locationStringB.Append(location.LocationName + ", ");
            }
            locationStringB.Remove(locationStringB.Length - 2, 2);
            locationTextBlock.Text = locationStringB.ToString();
            //tripimage list
            tripImgListView.ItemsSource = tripImages;
            //member
            MemberCountTextBlock.Text = memberList.Count.ToString();
            var memberStringB = new StringBuilder();
            foreach (var member in memberList)
            {
                memberStringB.Append(member.MemberName + ", ");
            }
            memberStringB.Remove(memberStringB.Length - 2, 2);
            memberTextBlock.Text = memberStringB.ToString();
            //pieChart
            pieChart.Series.Clear();
            foreach (var member in memberList)
            {
                pieChart.Series.Add(new PieSeries()
                {
                    Title = member.MemberName,
                    Values = new ChartValues<double> { MyUtils.calcTotalCostMember(tripIDSelected, member.MemberID) }
                });
            }

            //cartesianChart
            SeriesCollection = new SeriesCollection();
            var costList = MyUtils.findCostNameAndCostByTripID(tripIDSelected);
            foreach (var cost in costList)
            {
                SeriesCollection.Add(new ColumnSeries()
                {
                    Title = cost.CostName,
                    Values = new ChartValues<double>() { (int)cost.Cost }
                });
            }
            //SeriesCollection = new SeriesCollection
            //{
            //    //new ColumnSeries
            //    //{
            //    //    Title = "tiền nhà",
            //    //    Values = new ChartValues<double> { 3000 }
            //    //}
            //};

            var bc = new BrushConverter();

            planningBorder.Background = (Brush)bc.ConvertFrom("#202020");
            onGoingBorder.Background = (Brush)bc.ConvertFrom("#202020");
            doneBorder.Background = (Brush)bc.ConvertFrom("#202020");

            if (tripSelected.Status == 0)
            {
                planningBorder.Background = new SolidColorBrush(Colors.Orange);
            }
            else if (tripSelected.Status == 1)
            {
                onGoingBorder.Background = new SolidColorBrush(Colors.Orange);
            }
            else if (tripSelected.Status == 2)
            {
                doneBorder.Background = new SolidColorBrush(Colors.Orange);
            }

            cartesianChart.Series = SeriesCollection;

            if (isViewing)
            {
                backBtn.Visibility = Visibility.Visible;
            }
        }

        private void ClearBg(Button btn)
        {
            Border _border = btn.Template.FindName("border", btn) as Border;

            if (_border != null)
            {
                _border.ClearValue(BackgroundProperty);
            }
        }

        private void ChangeColorBg(Button btn)
        {
            Border _border = btn.Template.FindName("border", btn) as Border;

            if (_border != null)
            {
                _border.Background = Brushes.Black;
            }
        }

        private void ClearAllBg()
        {
            ClearBg(homeBtn);
            ClearBg(addBtn);
            ClearBg(settingBtn);
            ClearBg(aboutBtn);
        }

        private void HideAllGrid()
        {
            allTripsGrid.Visibility = Visibility.Collapsed;
            tripDetailGrid.Visibility = Visibility.Collapsed;
            addTripGrid.Visibility = Visibility.Collapsed;
            settingGrid.Visibility = Visibility.Collapsed;
            aboutGrid.Visibility = Visibility.Collapsed;
            backBtn.Visibility = Visibility.Hidden;
        }

        private void homeBtn_Click(object sender, RoutedEventArgs e)
        {
            HideSearchCondition();
            ClearAllBg();
            ChangeColorBg(homeBtn);
            HideAllGrid();
            allTripsGrid.Visibility = Visibility.Visible;
            allTripListView.ItemsSource = new TripsDAOsqlserver().GetAll();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            HideSearchCondition();
            ClearAllBg();
            ChangeColorBg(addBtn);
            HideAllGrid();
            addTripGrid.Visibility = Visibility.Visible;
        }

        private void settingBtn_Click(object sender, RoutedEventArgs e)
        {
            HideSearchCondition();
            ClearAllBg();
            ChangeColorBg(settingBtn);
            HideAllGrid();
            settingGrid.Visibility = Visibility.Visible;
        }

        private void aboutBtn_Click(object sender, RoutedEventArgs e)
        {
            HideSearchCondition();
            ClearAllBg();
            ChangeColorBg(aboutBtn);
            HideAllGrid();
            aboutGrid.Visibility = Visibility.Visible;
        }

        private void cancelAddBtn_Click(object sender, RoutedEventArgs e)
        {
            var choice = MessageBox.Show("Bạn chắc muốn hủy bỏ?",
                                "Thông báo",
                                MessageBoxButton.YesNo,
                                MessageBoxImage.Question);

            if (choice == MessageBoxResult.Yes)
            {
                tripNameTextBox.Text = null;
                locationsTextBox.Text = null;
                statusComboBox.SelectedItem = null;
                imgAddListView.ItemsSource = null;

                while (addListView.Items.Count != 0)
                {
                    addListView.Items.RemoveAt(0);
                }
                AddNewItemAddtListView();
            }
        }

        private struct memberCostSingle
        {
            public string costNameMemberCost;
            public string costValueMemberCost;
        };

        private struct memberCost
        {
            public string memberName;
            public List<memberCostSingle> cost;
        }

        private void accepAddBtn_Click(object sender, RoutedEventArgs e)
        {
            var isValid = true;

            if (string.IsNullOrEmpty(tripNameTextBox.Text))
            {
                tripNameTextBox.BorderBrush = Brushes.Red;
                isValid = false;
            }

            if (string.IsNullOrEmpty(locationsTextBox.Text))
            {
                locationsTextBox.BorderBrush = Brushes.Red;
                isValid = false;
            }

            if (statusComboBox.SelectedItem == null)
            {
                ToggleButton toggleButton = statusComboBox.Template.FindName("toggleButton", statusComboBox) as ToggleButton;
                Border _border = toggleButton.Template.FindName("templateRoot", toggleButton) as Border;

                _border.BorderBrush = Brushes.Red;
                isValid = false;
            }

            List<memberCost> myList = new List<memberCost>();

            if (isValid)
            {
                foreach (ListViewItem item in addListView.Items)
                {
                    TextBox textBox = (TextBox)item.FindName("memberNameTextBox");
                    ListView listView = (ListView)item.FindName("costAddListView");

                    List<memberCostSingle> memCost = new List<memberCostSingle>();

                    foreach (ListViewItem subItem in listView.Items)
                    {
                        TextBox costName = (TextBox)subItem.FindName("costNameTextBox");
                        TextBox costValue = (TextBox)subItem.FindName("costValueTextBox");
                        memCost.Add(new memberCostSingle() { costNameMemberCost = costName.Text, costValueMemberCost = costValue.Text });
                    }

                    myList.Add(new memberCost { memberName = textBox.Text, cost = memCost });

                    Console.WriteLine(myList);
                }

                //add functional
                var sttComboBox = statusComboBox.SelectedItem as ComboBoxItem;
                var sttString = sttComboBox.Content.ToString();
                //trip
                int addedTripID = new TripsDAOsqlserver().AddTripToDB(tripNameTextBox.Text, sttString);
                //add location
                var locationToAdd = new LOCATION()
                {
                    TripID = addedTripID,
                    LocationName = locationsTextBox.Text
                };
                new LocationDAOsqlserver().Add(locationToAdd);
                //add member + memberspertrip + membercost
                foreach (var mc in myList)
                {
                    int addedMemberID = new MembersDAOsqlserver().AddMemberToDB(mc.memberName);
                    //add memberpertrip
                    new MembersPerTripDAOsqlserver().AddMembersPerTripToDB(addedTripID, addedMemberID);
                    foreach (var cost in mc.cost)
                    {
                        //add membercost
                        new MemberCostsDAOsqlserver().AddMemberCostToDB(addedTripID, addedMemberID, cost.costNameMemberCost, int.Parse(cost.costValueMemberCost));
                    }
                }
                //trip image
                var tripImageList = imgAddListView.ItemsSource as BindingList<string>;
                foreach (var tripImagePath in tripImageList)
                {
                    new TripImagesDAOsqlserver().AddTripImgToDBAndLocalStorage(addedTripID, tripImagePath);
                }

                var choice = MessageBox.Show("Đã thêm chuyến đi",
                                "Thông báo",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
            }

            homeBtn_Click(sender, e);
            addTripGrid.Visibility = Visibility.Collapsed;
            allTripsGrid.Visibility = Visibility.Visible;
        }

        private void tripNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(tripNameTextBox.Text))
            {
                tripNameTextBox.BorderBrush = Brushes.Red;
            }
            else
            {
                tripNameTextBox.BorderBrush = Brushes.White;
            }
        }

        private void locationsTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(locationsTextBox.Text))
            {
                locationsTextBox.BorderBrush = Brushes.Red;
            }
            else
            {
                locationsTextBox.BorderBrush = Brushes.White;
            }
        }

        private void statusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ToggleButton toggleButton = statusComboBox.Template.FindName("toggleButton", statusComboBox) as ToggleButton;
            Border _border = toggleButton.Template.FindName("templateRoot", toggleButton) as Border;

            _border.BorderBrush = Brushes.Red;

            if (statusComboBox.SelectedItem == null)
            {
                _border.BorderBrush = Brushes.Red;
            }
            else
            {
                _border.BorderBrush = Brushes.White;
            }
        }

        private void splashScreen_Checked(object sender, RoutedEventArgs e)
        {
            var config = ConfigurationManager.OpenExeConfiguration(
                ConfigurationUserLevel.None);
            config.AppSettings.Settings["ShowSplashScreen"].Value = "true";
            config.Save(ConfigurationSaveMode.Minimal);
        }

        private void splashScreen_Unchecked(object sender, RoutedEventArgs e)
        {
            var config = ConfigurationManager.OpenExeConfiguration(
                ConfigurationUserLevel.None);
            config.AppSettings.Settings["ShowSplashScreen"].Value = "false";
            config.Save(ConfigurationSaveMode.Minimal);
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            backBtn.Visibility = Visibility.Hidden;
            tripDetailGrid.Visibility = Visibility.Collapsed;
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var tripIDSelected = int.Parse(tripIDTextBlock.Text);

            new TripsDAOsqlserver().DeleteWholeTripByTripID(tripIDSelected);

            MessageBox.Show("Đã xóa chuyến đi!");

            homeBtn_Click(sender, e);
            tripDetailGrid.Visibility = Visibility.Collapsed;
            allTripsGrid.Visibility = Visibility.Visible;
        }

        private void searchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                if (searchTextBox.Text == "")
                {
                    nameSearchGrid.Visibility = Visibility.Collapsed;
                    searchResultGrid.Visibility = Visibility.Collapsed;
                }
                else
                {
                    nameSearchTextBlock.Text = "Kết quả tìm kiếm cho " + "\"" + "searchTextBox.Text" + "\":";

                    if ((bool)searchByTrip.IsChecked)//Tìm theo chuyến đi
                    {
                        nameSearchGrid.Visibility = Visibility.Visible;
                        searchResultGrid.Visibility = Visibility.Visible;
                    }
                    else // tìm theo tên thành viên
                    {
                        nameSearchGrid.Visibility = Visibility.Visible;
                        searchResultGrid.Visibility = Visibility.Visible;
                    }

                    backToHomeBtn.Visibility = Visibility.Visible;
                }
            }
        }

        private void TripSearchBtn_Click(object sender, RoutedEventArgs e)//Nhấn vào chuyến đi trong search result grid
        {
        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)//Nhấn vào nút update
        {
            addTripGrid.Visibility = Visibility.Visible;
            backToDetailBtn.Visibility = Visibility.Visible;
            backBtn.Visibility = Visibility.Collapsed;
        }

        private void backToDetailBtn_Click(object sender, RoutedEventArgs e)
        {
            backToDetailBtn.Visibility = Visibility.Hidden;
            addTripGrid.Visibility = Visibility.Collapsed;
            backBtn.Visibility = Visibility.Visible;
        }

        private void backToHomeBtn_Click(object sender, RoutedEventArgs e)
        {
            nameSearchGrid.Visibility = Visibility.Collapsed;
            searchResultGrid.Visibility = Visibility.Collapsed;
            backToHomeBtn.Visibility = Visibility.Hidden;
        }
    }
}