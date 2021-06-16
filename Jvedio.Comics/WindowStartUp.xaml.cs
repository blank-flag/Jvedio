using Jvedio.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using static Jvedio.FileProcess;
using static Jvedio.GlobalVariable;
using Jvedio.Utils;
using Jvedio.Utils.Net;

namespace Jvedio.Comics
{
    /// <summary>
    /// WindowStartUp.xaml 的交互逻辑
    /// </summary>
    public partial class WindowStartUp : Window
    {

        public CancellationTokenSource cts;
        public CancellationToken ct;
        public List<string> DataBases;
        public WindowStartUp()
        {

            InitializeComponent();


            cts = new CancellationTokenSource();
            cts.Token.Register(() => Console.WriteLine("取消任务"));
            ct = cts.Token;

            if (!Directory.Exists("DataBase\\Comics")) Directory.CreateDirectory("DataBase\\Comics");

            if (File.Exists("upgrade.bat"))
            {
                try { File.Delete("upgrade.bat"); } catch { }
            }

            if (Directory.Exists("Temp"))
            {
                try { Directory.Delete("Temp", true); } catch { }
            }
            ListDatabase();
        }


        public void ListDatabase()
        {
            DataBases = new List<string>();
            try
            {
                var files = Directory.GetFiles("DataBase\\Comics", "*.sqlite", SearchOption.TopDirectoryOnly).ToList();
                foreach (var item in files)
                {
                    string name = Path.GetFileNameWithoutExtension(item);
                    if (!string.IsNullOrEmpty(name))
                        DataBases.Add(name);
                }
            }
            catch { }

            if (!DataBases.Contains(Jvedio.Language.Resources.NewLibrary)) DataBases.Add(Jvedio.Language.Resources.NewLibrary);

            RefreshItemsControl();
        }

        private void RefreshItemsControl()
        {
            ItemsControl.ItemsSource = null;
            ItemsControl.ItemsSource = DataBases;
        }

        public static string TranslateDataBasePath = AppDomain.CurrentDomain.BaseDirectory + "Translate-Comics.sqlite";


        public void LoadDataBase(object sender, MouseButtonEventArgs e)
        {
            //加载数据库
            StackPanel stackPanel = sender as StackPanel;
            TextBox TextBox = stackPanel.Children[1] as TextBox;

            string name = TextBox.Text;
            if (name == Jvedio.Language.Resources.NewLibrary)
            {
                //重命名
                TextBox.IsReadOnly = false;
                TextBox.Text = Jvedio.Language.Resources.MyComics;
                TextBox.Focus();
                TextBox.SelectAll();
                TextBox.Cursor = Cursors.IBeam;
                return;
            }
            else
                Properties.Settings.Default.DataBasePath = AppDomain.CurrentDomain.BaseDirectory + $"DataBase\\Comics\\{name}.sqlite";


            if (!File.Exists(Properties.Settings.Default.DataBasePath)) return;

            SelectDbBorder.Visibility = Visibility.Hidden;


            //启动主窗口
            MainWindow main = new MainWindow();
            statusText.Text = Jvedio.Language.Resources.Loading;
            main.Show();
            this.Close();
        }




        public void ClearLogBefore(DateTime dateTime, string filepath)
        {
            if (!Directory.Exists(filepath)) return;
            try
            {
                string[] files = Directory.GetFiles(filepath, "*.log", SearchOption.TopDirectoryOnly);
                foreach (var file in files)
                {
                    DateTime.TryParse(file.Split('\\').Last().Replace(".log", ""), out DateTime date);
                    if (date < dateTime) File.Delete(file);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


        }
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {

            statusText.Text = Jvedio.Language.Resources.Status_UpdateConfig;
            try
            {
                if (Properties.Settings.Default.UpgradeRequired)
                {
                    Properties.Settings.Default.Upgrade();
                    Properties.Settings.Default.UpgradeRequired = false;
                    Properties.Settings.Default.Save();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.LogE(ex);
            }


            statusText.Text = Jvedio.Language.Resources.Status_RepairConfig;
            try
            {
                CheckFile(); //判断文件是否存在
                CheckSettings();//修复设置错误
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.LogE(ex);
            }

            statusText.Text = Jvedio.Language.Resources.Status_CreateDir;
            try
            {
                if (!Directory.Exists("log")) { Directory.CreateDirectory("log"); }//创建 Log文件夹
                if (!Directory.Exists("log\\scanlog")) { Directory.CreateDirectory("log\\scanlog"); }//创建 ScanLog 文件夹
                if (!Directory.Exists("DataBase\\Comics")) { Directory.CreateDirectory("DataBase\\Comics"); }            //创建 DataBase 文件夹
                if (!Directory.Exists("BackUp")) { Directory.CreateDirectory("BackUp"); }            //创建备份文件夹
                SetSkin(Properties.Settings.Default.Themes);
                if (GlobalFont != null) this.FontFamily = GlobalFont;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.LogE(ex);
            }
            statusText.Text = Jvedio.Language.Resources.Status_InitDatabase;
            try
            {
                InitDataBase();//初始化数据库
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.LogE(ex);
            }

            try
            {
                statusText.Text = Jvedio.Language.Resources.Status_ClearRecentWatch;
                ClearDateBefore(-10);
                statusText.Text = Jvedio.Language.Resources.Status_ClearLog;
                ClearLogBefore(DateTime.Now.AddDays(-10), AppDomain.CurrentDomain.BaseDirectory + "log");
                ClearLogBefore(DateTime.Now.AddDays(-10), AppDomain.CurrentDomain.BaseDirectory + "log\\NetWork");
                ClearLogBefore(DateTime.Now.AddDays(-10), AppDomain.CurrentDomain.BaseDirectory + "log\\scanlog");
                ClearLogBefore(DateTime.Now.AddDays(-10), AppDomain.CurrentDomain.BaseDirectory + "log\\file");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.LogE(ex);
            }

            BackUp("Translate-Comics.sqlite");//备份文件

            //默认打开某个数据库
            if (Properties.Settings.Default.OpenDataBaseDefault && File.Exists(Properties.Settings.Default.DataBasePath))
            {
                //启动主窗口
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
            }
            else
            {
                SelectDbBorder.Visibility = Visibility.Visible;
            }
        }


        public void CheckSettings()
        {

            //TODO 主题增加后产生的问题
            //if (!Enum.IsDefined(typeof(Skin), Properties.Settings.Default.Themes))
            //{
            //    Properties.Settings.Default.Themes = Skin.黑色.ToString();
            //    Properties.Settings.Default.Save();
            //}

            //if (!Enum.IsDefined(typeof(MyLanguage), Properties.Settings.Default.Language))
            //{
            //    Properties.Settings.Default.Language = MyLanguage.中文.ToString();
            //    Properties.Settings.Default.Save();
            //}

        }


        public void CheckFile()
        {
            if (!File.Exists(@"x64\SQLite.Interop.dll") || !File.Exists(@"x86\SQLite.Interop.dll"))
            {
                MessageBox.Show($"{Jvedio.Language.Resources.Missing} SQLite.Interop.dll", "Jvedio");
                this.Close();
            }
        }

        private void BackUp(string filename)
        {
            if (!Directory.Exists("BackUp")) Directory.CreateDirectory("BackUp");
            if (File.Exists(filename))
            {
                string src = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);
                string target = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BackUp", filename);
                if (!File.Exists(target))
                {
                    FileHelper.TryCopyFile(src, target);


                }
                else if (new FileInfo(target).Length < new FileInfo(src).Length)
                {
                    FileHelper.TryCopyFile(src, target, true);
                }
            }
        }

        private void InitDataBase()
        {

            if (!File.Exists(TranslateDataBasePath))
            {
                MySqlite db = new MySqlite("Translate");
                db.CreateTable(DataBase.SQLITETABLE_YOUDAO);
                db.CreateTable(DataBase.SQLITETABLE_BAIDUTRANSLATE);
                db.CloseDB();
            }
            else
            {
                //是否具有表结构
                MySqlite db = new MySqlite("Translate");
                if (!db.IsTableExist("youdao")) db.CreateTable(DataBase.SQLITETABLE_YOUDAO);
                if (!db.IsTableExist("baidu")) db.CreateTable(DataBase.SQLITETABLE_BAIDUTRANSLATE);
                db.CloseDB();
            }
        }





        private void MoveWindow(object sender, MouseEventArgs e)
        {
            //移动窗口
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            OpenFileDialog1.Title = Jvedio.Language.Resources.ChooseDataBase;
            OpenFileDialog1.Filter = $"Sqlite {Jvedio.Language.Resources.File}|*.sqlite";
            OpenFileDialog1.Multiselect = true;
            if (OpenFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string[] names = OpenFileDialog1.FileNames;
                foreach (var item in names)
                {
                    string name = Path.GetFileNameWithoutExtension(item);
                    if (name == Jvedio.Language.Resources.NewLibrary) continue;
                    if (!DataBase.IsProperSqlite(item)) continue;
                    if (File.Exists($"DataBase\\Comics\\{name}.sqlite"))
                    {
                        if (new Msgbox(this, $"{Jvedio.Language.Resources.Message_AlreadyExist} {name} {Jvedio.Language.Resources.IsToOverWrite} ？").ShowDialog() == true)
                        {
                            FileHelper.TryCopyFile(item, $"DataBase\\Comics\\{name}.sqlite", true);
                            if (!DataBases.Contains(name)) DataBases.Add(name);
                        }
                    }
                    else
                    {
                        FileHelper.TryCopyFile(item, $"DataBase\\Comics\\{name}.sqlite", true);
                        if (!DataBases.Contains(name)) DataBases.Add(name);
                    }

                }
                RefreshItemsControl();
            }
        }

        private void Border_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        Image optionImage;

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            optionImage = (sender as Border).Child as Image;
            OptionPopup.IsOpen = true;
        }

        private void DelSqlite(object sender, RoutedEventArgs e)
        {
            Border border = optionImage.Parent as Border;
            Grid grid = border.Parent as Grid;
            StackPanel stackPanel = grid.Children.OfType<StackPanel>().First();
            TextBox TextBox = stackPanel.Children[1] as TextBox;
            string name = TextBox.Text;

            if (name == Jvedio.Language.Resources.NewLibrary) return;


            if (new Msgbox(this, $"{Jvedio.Language.Resources.IsToDelete} {name}?").ShowDialog() == true)
            {
                string dirpath = DateTime.Now.ToString("yyyyMMddHHss");
                Directory.CreateDirectory($"BackUp\\Comics\\{dirpath}");
                if (File.Exists($"DataBase\\Comics\\{name}.sqlite"))
                {
                    //备份
                    FileHelper.TryCopyFile($"DataBase\\Comics\\{name}.sqlite", $"BackUp\\Comics\\{dirpath}\\{name}.sqlite", true);
                    //删除

                    try
                    {
                        File.Delete($"DataBase\\Comics\\{name}.sqlite");
                        DataBases.Remove(name);
                        RefreshItemsControl();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }



            }
        }

        public string beforeRename = "";

        private void RenameSqlite(object sender, RoutedEventArgs e)
        {
            Border border = optionImage.Parent as Border;
            Grid grid = border.Parent as Grid;
            StackPanel stackPanel = grid.Children.OfType<StackPanel>().First();
            TextBox TextBox = stackPanel.Children[1] as TextBox;
            string name = TextBox.Text;

            //重命名
            OptionPopup.IsOpen = false;
            TextBox.IsReadOnly = false;
            TextBox.Focus();
            TextBox.SelectAll();
            TextBox.Cursor = Cursors.IBeam;
            beforeRename = TextBox.Text;
        }


        private void Rename(TextBox textBox)
        {
            string name = textBox.Text;

            //不修改
            if (name == beforeRename)
            {
                textBox.IsReadOnly = true;
                textBox.Cursor = Cursors.Hand;
                beforeRename = "";
                return;
            }


            //新建一个数据库
            if (beforeRename == "")
            {
                if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrEmpty(name) && !IsItemInList(name, DataBases) && name.IndexOfAny(Path.GetInvalidFileNameChars()) == -1)
                {
                    //新建

                    MySqlite db = new MySqlite("DataBase\\Comics\\" + name);
                    db.CreateTable(SqliteHelper.SQLITETABLE_COMICS);
                    db.CloseDB();

                    if (DataBases.Contains(Jvedio.Language.Resources.NewLibrary)) DataBases.Remove(Jvedio.Language.Resources.NewLibrary);
                    textBox.IsReadOnly = true;
                    textBox.Cursor = Cursors.Hand;

                    DataBases.Add(name);
                    DataBases.Add(Jvedio.Language.Resources.NewLibrary);
                    RefreshItemsControl();
                }
                else
                {
                    textBox.Text = Jvedio.Language.Resources.NewLibrary;
                }
            }
            else
            {
                //重命名
                if (IsItemInList(name, DataBases))
                {
                    textBox.Text = beforeRename; //重复的
                }
                else
                {
                    //重命名
                    if (name.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) == -1)
                    {
                        try
                        {
                            File.Move(AppDomain.CurrentDomain.BaseDirectory + $"DataBase\\Comics\\{beforeRename}.sqlite",
                                AppDomain.CurrentDomain.BaseDirectory + $"DataBase\\Comics\\{name}.sqlite");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            Logger.LogE(ex);
                        }
                    }
                    else
                    {
                        textBox.Text = beforeRename;
                    }


                }
                beforeRename = "";
            }
            textBox.IsReadOnly = true;
            textBox.Cursor = Cursors.Hand;
            textBox.TextAlignment = TextAlignment.Left;

        }

        private bool IsItemInList(string str, List<string> list)
        {
            foreach (var item in list)
            {
                if (item?.ToLower() == str.ToLower()) return true;
            }
            return false;
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            Rename(textBox);
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (e.Key == Key.Enter)
            {
                LoadButton.Focus();
            }
            else if (e.Key == Key.Escape)
            {
                textBox.IsReadOnly = true;
                textBox.Cursor = Cursors.Hand;
            }
        }

        private void SetAsDefault(object sender, RoutedEventArgs e)
        {
            Border border = optionImage.Parent as Border;
            Grid grid = border.Parent as Grid;
            StackPanel stackPanel = grid.Children.OfType<StackPanel>().First();
            TextBox TextBox = stackPanel.Children[1] as TextBox;
            string name = TextBox.Text;
            Properties.Settings.Default.OpenDataBaseDefault = true;
            Properties.Settings.Default.DataBasePath = AppDomain.CurrentDomain.BaseDirectory + $"DataBase\\Comics\\{name}.sqlite";
            OptionPopup.IsOpen = false;
            LoadDataBase(stackPanel, new MouseButtonEventArgs(InputManager.Current.PrimaryMouseDevice, 0, MouseButton.Left));
        }

        private void OpenPath(object sender, RoutedEventArgs e)
        {
            Border border = optionImage.Parent as Border;
            Grid grid = border.Parent as Grid;
            StackPanel stackPanel = grid.Children.OfType<StackPanel>().First();
            TextBox TextBox = stackPanel.Children[1] as TextBox;
            string name = TextBox.Text;
            string path = AppDomain.CurrentDomain.BaseDirectory + $"DataBase\\Comics\\{name}.sqlite";
            FileHelper.TryOpenSelectPath(path);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Console.WriteLine(e.AddedItems[0].ToString());
        }
    }
}
