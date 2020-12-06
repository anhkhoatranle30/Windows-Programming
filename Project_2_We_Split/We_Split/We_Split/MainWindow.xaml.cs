using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.Win32;

namespace We_Split
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //var db = new WP_Project2_WeSplitEntities();

            //int memberid = 1;
            //int tripid = 1;
            //var query = from membercost in db.MEMBERCOSTs
            //            where membercost.MemberID == memberid && membercost.TripID == tripid
            //            select membercost;
            //var result = new BindingList<MEMBERCOST>(query.ToList());
            //Debug.WriteLine(query.GetType());
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
            ClearBtnBg(doneBtn);
            ClearBtnBg(onGoingBtn);
            SetNoCorner(doneBtn);
            Border _border = allBtn.Template.FindName("border", allBtn) as Border;

            if (_border != null)
            {
                _border.CornerRadius = new CornerRadius(18, 18, 0, 0);
                _border.Background = Brushes.Black;
            }

            _border = onGoingBtn.Template.FindName("border", onGoingBtn) as Border;

            if (_border != null)
            {
                _border.CornerRadius = new CornerRadius(0, 0, 0, 18);
            }

            borderLeft.CornerRadius = new CornerRadius(0, 0, 16, 0);
        }

        private void onGoingBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearBtnBg(doneBtn);
            ClearBtnBg(allBtn);
            borderRight.CornerRadius = new CornerRadius(0);
            borderLeft.CornerRadius = new CornerRadius(0);

            Border _border = onGoingBtn.Template.FindName("border", onGoingBtn) as Border;

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

            _border = doneBtn.Template.FindName("border", doneBtn) as Border;

            if (_border != null)
            {
                _border.CornerRadius = new CornerRadius(0, 0, 0, 18);
            }
        }

        private void doneBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearBtnBg(allBtn);
            ClearBtnBg(onGoingBtn);
            SetNoCorner(allBtn);
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
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            allBtn_Click(sender, e);

            PointLabel = chartPoint =>
              string.Format("{0}đ", chartPoint.Y);

            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "kem đánh răng",
                    Values = new ChartValues<double> {100,2000}
                },
                new ColumnSeries
                {
                    Title = "đá phò",
                    Values = new ChartValues<double> { 9000 }
                },
                new ColumnSeries
                {
                    Title = "vé máy bay",
                    Values = new ChartValues<double> { 2000 }
                },
                new ColumnSeries
                {
                    Title = "BCS",
                    Values = new ChartValues<double> { 600 }
                },
                new ColumnSeries
                {
                    Title = "Ăn uống",
                    Values = new ChartValues<double> { 5000 }
                },
                new ColumnSeries
                {
                    Title = "tiền nhà",
                    Values = new ChartValues<double> { 3000 }
                }
            };

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

            //var test = new TextBox();
            //test.Style = (Style)this.TryFindResource("textboxAddStyle");
            //var test = new StackPanel();
            //addListView.Items.Add(test);

            //var test = addListView.FindName("costAddListView");
            //if (test == null) Console.WriteLine("null");
            //else Console.WriteLine("ko null");

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
        }

        //private void addCostBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    //costAddListView

        //    var newCostNameTextBox = new TextBox();
        //    var newCostValueTextBox = new TextBox();

        //    ListViewItem item = (ListViewItem)costAddListView.Items[costAddListView.Items.Count - 1];
        //    newCostNameTextBox = (TextBox)item.FindName("costNameTextBox");
        //    newCostValueTextBox = (TextBox)item.FindName("costValueTextBox");

        //    if (newCostNameTextBox == null || newCostValueTextBox == null)
        //    {
        //        Console.WriteLine("co loi");
        //    }
        //    else
        //    {
        //        bool isEmpty = false;

        //        if (newCostNameTextBox.Text.Length == 0)
        //        {
        //            isEmpty = true;
        //            newCostNameTextBox.BorderBrush = Brushes.Red;
        //        }
        //        else
        //        {
        //            newCostNameTextBox.BorderBrush = Brushes.White;
        //        }

        //        if (newCostValueTextBox.Text.Length == 0)
        //        {
        //            isEmpty = true;
        //            newCostValueTextBox.BorderBrush = Brushes.Red;
        //        }
        //        else
        //        {
        //            newCostValueTextBox.BorderBrush = Brushes.White;
        //        }

        //        if (!isEmpty)
        //        {
        //            AddNewItemCostListView();
        //        }
        //        else
        //        {
        //        }
        //    }
        //}

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
                    button = (Button)item.FindName("addCostBtn");
                    button.Click += addCostBtnAll_Click;
                }
            }
        }

        private bool IsNumber(string value)
        {
            bool result = false;

            return result;
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
    }
}