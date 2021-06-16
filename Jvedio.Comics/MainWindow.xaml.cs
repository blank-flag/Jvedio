using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using static Jvedio.GlobalVariable;
using Jvedio.Utils;
using Jvedio.Utils.Net;
using System.IO;
using System.Diagnostics;
using Jvedio.Utils.Encrypt;

namespace Jvedio.Comics
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private HwndSource _hwndSource;
        public Point WindowPoint = new Point(100, 100);
        public Size WindowSize = new Size(1000, 600);
        public JvedioWindowState WinState = JvedioWindowState.Normal;
        public bool Resizing = false;
        public VieModel_Main vieModel;
        public DispatcherTimer ResizingTimer = new DispatcherTimer();
        public Settings WindowSet = null;

        public MainWindow()
        {
            InitializeComponent();

            vieModel = new VieModel_Main();
            this.DataContext = vieModel;

            ResizingTimer.Interval = TimeSpan.FromSeconds(0.5);
            ResizingTimer.Tick += new EventHandler(ResizingTimer_Tick);
            Properties.Settings.Default.FirstRun = false;
            #region "改变窗体大小"
            //https://www.cnblogs.com/yang-fei/p/4737308.html

            if (resizeGrid != null)
            {
                foreach (UIElement element in resizeGrid.Children)
                {
                    if (element is Rectangle resizeRectangle)
                    {
                        resizeRectangle.PreviewMouseDown += ResizeRectangle_PreviewMouseDown;
                        resizeRectangle.MouseMove += ResizeRectangle_MouseMove;
                    }
                }
            }
            PreviewMouseMove += OnPreviewMouseMove;
            #endregion
        }

        #region "改变窗体大小"
        private void ResizeRectangle_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized) return;
            if (this.Width == SystemParameters.WorkArea.Width || this.Height == SystemParameters.WorkArea.Height) return;

            if (sender is Rectangle rectangle)
            {
                switch (rectangle.Name)
                {
                    case "TopRectangle":
                        Cursor = Cursors.SizeNS;
                        ResizeWindow(ResizeDirection.Top);
                        break;
                    case "Bottom":
                        Cursor = Cursors.SizeNS;
                        ResizeWindow(ResizeDirection.Bottom);
                        break;
                    case "LeftRectangle":
                        Cursor = Cursors.SizeWE;
                        ResizeWindow(ResizeDirection.Left);
                        break;
                    case "Right":
                        Cursor = Cursors.SizeWE;
                        ResizeWindow(ResizeDirection.Right);
                        break;
                    case "TopLeft":
                        Cursor = Cursors.SizeNWSE;
                        ResizeWindow(ResizeDirection.TopLeft);
                        break;
                    case "TopRight":
                        Cursor = Cursors.SizeNESW;
                        ResizeWindow(ResizeDirection.TopRight);
                        break;
                    case "BottomLeft":
                        Cursor = Cursors.SizeNESW;
                        ResizeWindow(ResizeDirection.BottomLeft);
                        break;
                    case "BottomRight":
                        Cursor = Cursors.SizeNWSE;
                        ResizeWindow(ResizeDirection.BottomRight);
                        break;
                    default:
                        break;
                }
            }
        }


        protected void OnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton != MouseButtonState.Pressed)
                Cursor = Cursors.Arrow;
        }

        private void ResizeRectangle_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized) return;
            if (this.Width == SystemParameters.WorkArea.Width || this.Height == SystemParameters.WorkArea.Height) return;

            if (sender is Rectangle rectangle)
            {
                switch (rectangle.Name)
                {
                    case "TopRectangle":
                        Cursor = Cursors.SizeNS;
                        break;
                    case "Bottom":
                        Cursor = Cursors.SizeNS;
                        break;
                    case "LeftRectangle":
                        Cursor = Cursors.SizeWE;
                        break;
                    case "Right":
                        Cursor = Cursors.SizeWE;
                        break;
                    case "TopLeft":
                        Cursor = Cursors.SizeNWSE;
                        break;
                    case "TopRight":
                        Cursor = Cursors.SizeNESW;
                        break;
                    case "BottomLeft":
                        Cursor = Cursors.SizeNESW;
                        break;
                    case "BottomRight":
                        Cursor = Cursors.SizeNWSE;
                        break;
                    default:
                        break;
                }
            }
        }

        public enum ResizeDirection
        {
            Left = 1,
            Right = 2,
            Top = 3,
            TopLeft = 4,
            TopRight = 5,
            Bottom = 6,
            BottomLeft = 7,
            BottomRight = 8,
        }

        protected override void OnInitialized(EventArgs e)
        {
            SourceInitialized += MainWindow_SourceInitialized;
            base.OnInitialized(e);
        }

        private void MainWindow_SourceInitialized(object sender, EventArgs e)
        {
            _hwndSource = (HwndSource)PresentationSource.FromVisual(this);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, UInt32 msg, IntPtr wParam, IntPtr lParam);

        private void ResizeWindow(ResizeDirection direction)
        {
            SendMessage(_hwndSource.Handle, 0x112, (IntPtr)(61440 + direction), IntPtr.Zero);
        }

        #endregion

        private void TopBorder_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount > 1)
            {
                MaxWindow(sender, new RoutedEventArgs());
            }
        }

        public void CloseWindow(object sender, RoutedEventArgs e)
        {
            FadeOut();
        }

        public void FadeOut()
        {
            if (Properties.Settings.Default.EnableWindowFade)
            {
                var anim = new DoubleAnimation(0, (Duration)FadeInterval);
                anim.Completed += (s, _) => this.Close();
                this.BeginAnimation(UIElement.OpacityProperty, anim);
            }
            else
            {
                this.Close();
            }
        }

        public void MinWindow(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.EnableWindowFade)
            {
                var anim = new DoubleAnimation(0, (Duration)FadeInterval, FillBehavior.Stop);
                anim.Completed += (s, _) => this.WindowState = WindowState.Minimized;
                this.BeginAnimation(UIElement.OpacityProperty, anim);
            }
            else
            {
                this.WindowState = WindowState.Minimized;
            }
        }


        public async void MaxWindow(object sender, RoutedEventArgs e)
        {
            Resizing = true;
            if (WinState == 0)
            {
                var anim = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(0.25)), FillBehavior.Stop);
                this.BeginAnimation(UIElement.OpacityProperty, anim);
                //最大化
                WinState = JvedioWindowState.Maximized;
                WindowPoint = new Point(this.Left, this.Top);
                WindowSize = new Size(this.Width, this.Height);
                this.Width = SystemParameters.WorkArea.Width;
                this.Height = SystemParameters.WorkArea.Height;
                this.Top = SystemParameters.WorkArea.Top;
                this.Left = SystemParameters.WorkArea.Left;
            }
            else
            {
                var anim = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(0.5)), FillBehavior.Stop);
                this.BeginAnimation(UIElement.OpacityProperty, anim);
                WinState = JvedioWindowState.Normal;
                this.Left = WindowPoint.X;
                this.Width = WindowSize.Width;
                this.Top = WindowPoint.Y;
                this.Height = WindowSize.Height;
            }
            this.WindowState = WindowState.Normal;
            this.OnLocationChanged(EventArgs.Empty);
            HideMargin();
        }

        private void HideMargin()
        {
            if (WinState == JvedioWindowState.Normal)
            {
                vieModel.MainGridThickness = new Thickness(10);
                this.ResizeMode = ResizeMode.CanResize;
            }
            else if (WinState == JvedioWindowState.Maximized || this.WindowState == WindowState.Maximized)
            {
                vieModel.MainGridThickness = new Thickness(0);
                this.ResizeMode = ResizeMode.NoResize;
            }
            ResizingTimer.Start();
        }

        private void ResizingTimer_Tick(object sender, EventArgs e)
        {
            Resizing = false;
            ResizingTimer.Stop();
        }



        private void MoveWindow(object sender, MouseEventArgs e)
        {
            vieModel.ShowSearchPopup = false;
            Border border = sender as Border;

            //移动窗口
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (this.WindowState == WindowState.Maximized || (this.Width == SystemParameters.WorkArea.Width && this.Height == SystemParameters.WorkArea.Height))
                {
                    WinState = 0;
                    double fracWidth = e.GetPosition(border).X / border.ActualWidth;
                    this.Width = WindowSize.Width;
                    this.Height = WindowSize.Height;
                    this.WindowState = WindowState.Normal;
                    this.Left = e.GetPosition(border).X - border.ActualWidth * fracWidth;
                    this.Top = e.GetPosition(border).Y - border.ActualHeight / 2;
                    this.OnLocationChanged(EventArgs.Empty);
                    HideMargin();
                }
                this.DragMove();
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.Width == SystemParameters.WorkArea.Width || this.Height == SystemParameters.WorkArea.Height)
            {
                vieModel.MainGridThickness = new Thickness(0);
                this.ResizeMode = ResizeMode.NoResize;
            }
            else if (this.WindowState == WindowState.Maximized)
            {
                vieModel.MainGridThickness = new Thickness(5);
                this.ResizeMode = ResizeMode.NoResize;
            }
            else
            {
                vieModel.MainGridThickness = new Thickness(10);
                this.ResizeMode = ResizeMode.CanResize;
            }
        }

        private bool IsDragingSideGrid = false;

        private void DragRectangle_MouseMove(object sender, MouseEventArgs e)
        {
            if (sender.GetType().Name == "Border") vieModel.ShowSearchPopup = false;
            if (vieModel.SideBorderWidth >= 200) { if (sender is Rectangle rectangle) rectangle.Cursor = Cursors.SizeWE; }
            if (IsDragingSideGrid)
            {
                this.Cursor = Cursors.SizeWE;
                double width = e.GetPosition(this).X;
                if (width > 500 || width < 200)
                    return;
                else
                {
                    vieModel.SideBorderWidth = width;
                }

            }
        }

        private void DragRectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && vieModel.SideBorderWidth >= 200)
            {
                IsDragingSideGrid = true;
            }
        }

        private void DragRectangle_MouseUp(object sender, MouseButtonEventArgs e)
        {
            IsDragingSideGrid = false;
            Properties.Settings.Default.SideGridWidth = vieModel.SideBorderWidth;
            Properties.Settings.Default.Save();
        }

        private void ShowAbout(object sender, RoutedEventArgs e)
        {
            new Dialog_About(this, false).ShowDialog();
        }

        private void OpenFeedBack(object sender, RoutedEventArgs e)
        {
            FileHelper.TryOpenUrl(FeedBackUrl);
        }

        private void OpenHelp(object sender, RoutedEventArgs e)
        {
            FileHelper.TryOpenUrl(WikiUrl);
        }


        private void OpenJvedioWebPage(object sender, RoutedEventArgs e)
        {
            FileHelper.TryOpenUrl(WebPageUrl);
        }

        private void ShowThanks(object sender, RoutedEventArgs e)
        {
            new Dialog_Thanks(this, false).ShowDialog();
        }

        private async void CheckUpgrade(object sender, RoutedEventArgs e)
        {
            if (sender != null)
            {
                new Dialog_Upgrade(this, false, "", "").ShowDialog();
            }
            else
            {
                (bool success, string remote, string updateContent) = await new MyNet().CheckUpdate(UpdateUrl);
                string local = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
                if (success && local.CompareTo(remote) < 0)
                {
                    new Dialog_Upgrade(this, false, remote, updateContent).ShowDialog();
                }
            }
        }

        public void ShowSettingsPopup(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                Border border = sender as Border;
                ContextMenu contextMenu = border.ContextMenu;
                contextMenu.PlacementTarget = border;
                contextMenu.Placement = PlacementMode.Bottom;
                contextMenu.IsOpen = true;
            }
            e.Handled = true;
        }

        private void OpenSettings(object sender, RoutedEventArgs e)
        {
            if (WindowSet != null) WindowSet.Close();
            WindowSet = new Settings();
            WindowSet.Show();
        }

        public void SetSkin()
        {
            FileProcess.SetSkin(Jvedio.Comics.Properties.Settings.Default.Themes);
            if (GlobalFont != null) this.FontFamily = GlobalFont;
            switch (Properties.Settings.Default.Themes)
            {
                case "蓝色":
                    //设置渐变
                    LinearGradientBrush myLinearGradientBrush = new LinearGradientBrush();
                    myLinearGradientBrush.StartPoint = new Point(0.5, 0);
                    myLinearGradientBrush.EndPoint = new Point(0.5, 1);
                    myLinearGradientBrush.GradientStops.Add(new GradientStop(Color.FromRgb(62, 191, 223), 1));
                    myLinearGradientBrush.GradientStops.Add(new GradientStop(Color.FromRgb(11, 114, 189), 0));
                    SideBorder.Background = myLinearGradientBrush;
                    break;

                default:
                    SideBorder.Background = (SolidColorBrush)Application.Current.Resources["BackgroundSide"];
                    break;
            }

            if (BackgroundImage != null)
            {
                SideBorder.Background = Brushes.Transparent;
                TitleBorder.Background = Brushes.Transparent;
                BgImage.Source = BackgroundImage;
            }
            else
            {
                TitleBorder.Background = (SolidColorBrush)Application.Current.Resources["BackgroundTitle"];
            }
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            SetSkin();
            vieModel.LoadDataBaseList();
            //设置当前数据库
            for (int i = 0; i < vieModel.DataBases.Count; i++)
            {
                if (vieModel.DataBases[i].ToLower() == System.IO.Path.GetFileNameWithoutExtension(Properties.Settings.Default.DataBasePath).ToLower())
                {
                    vieModel.DatabaseSelectedIndex = i;
                    break;
                }
            }


        }

        private void Test(object sender, RoutedEventArgs e)
        {
            var sw = new Stopwatch();
            sw.Start();
            string path = @"F:\Jvedio\Pic\BigPic";
            //var files = Directory.GetFiles(path, "*.*", SearchOption.TopDirectoryOnly);
            Console.WriteLine(Encrypt.GetDirectoryMD5(path));
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds + "ms");
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0) return;
            string name = e.AddedItems[0].ToString().ToLower();
            if (name != System.IO.Path.GetFileNameWithoutExtension(Properties.Settings.Default.DataBasePath).ToLower())
            {
                Properties.Settings.Default.DataBasePath = AppDomain.CurrentDomain.BaseDirectory + $"DataBase\\Comics\\{name}.sqlite";
                //切换数据库
                //vieModel.IsRefresh = true;
                //vieModel.Reset();
                //vieModel.InitLettersNavigation();
                //vieModel.GetFilterInfo();
            }
        }

        private void ClearSearchHistory(object sender, MouseButtonEventArgs e)
        {
            vieModel.SearchHistory.Clear();
            vieModel.SaveSearchHistory();
            SearchHistoryStackPanel.Visibility = Visibility.Collapsed;
        }

        private void RandomDisplay(object sender, MouseButtonEventArgs e)
        {

        }

        private void SearchBar_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void SearchBar_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void SearchBar_PreviewKeyUp(object sender, KeyEventArgs e)
        {

        }

        private void RefreshCandiadte(object sender, TextChangedEventArgs e)
        {

        }

        private void ShowSearchPopup(object sender, MouseButtonEventArgs e)
        {

        }

        private void SaveAllSearchType(object sender, RoutedEventArgs e)
        {

        }

        private void SetSearchValue(object sender, MouseButtonEventArgs e)
        {

        }

        private void SetSelectMode(object sender, RoutedEventArgs e)
        {

        }

        private void ShowFilterGrid(object sender, MouseButtonEventArgs e)
        {

        }

        private void ShowSearchMenu(object sender, MouseButtonEventArgs e)
        {

        }

        private void ShowSortPopup(object sender, MouseButtonEventArgs e)
        {

        }

        private void ToolsGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
