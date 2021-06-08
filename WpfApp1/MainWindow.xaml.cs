using Panuon.UI.Silver;
//using Panuon.UI.Silver.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;



namespace WpfApp1
{
    public partial class MainWindow : WindowX
    {
        // ============================================================MYSQL 
        string app_main_user_name = "Fanstech_DJ";
        string app_main_user_pwd  = "   ";
        string mysql_tables_name1 = "class";
        string mysql_tables_name2 = "资产负债";

        string sqlstr = "";
        string cur_used_table_name = "";
        MySql.Data.MySqlClient.MySqlConnection con;
        MySql.Data.MySqlClient.MySqlDataAdapter adapter;
        System.Data.DataSet ds;
        System.Data.DataTable dt;
        int mysql_param_enter_successed;

        public MainWindow()
        {
            InitializeComponent();
            GuiControlVisibilitySetting();
            MessageBoxSetting();
            mysql_param_enter_successed = 0;
        }

        private void WindowX_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        public void GuiControlVisibilitySetting()
        {
            LoginTabUserAccountText.Text             = "";
            LoginTabUserPwdText.Password             = "";
            LoginTabUserNameDispTextBox.Text         = "";
            MysqlConnectIpText.Password              = "";
            MysqlConnectPortText.Password            = "";
            MysqlConnectDataBasesNameText.Text       = "";
            MysqlConnectUserNameTextBox.Text         = "";
            MysqlConnectUserPwdText.Password         = "";
            MysqlUserNameDispTextBox.Text            = "";

            LoginTabUserAccountText.Visibility       = Visibility.Visible;
            LoginTabUserPwdText.Visibility           = Visibility.Visible;
            LoginTabCheckPwdClickButton.Visibility   = Visibility.Visible;

            LoginTabLogoutClickButton.Visibility     = Visibility.Collapsed;
            LoginTabUserNameDispTextBox.Visibility   = Visibility.Collapsed;

            MysqlConnectIpText.Visibility            = Visibility.Collapsed;
            MysqlConnectPortText.Visibility          = Visibility.Collapsed;
            MysqlConnectDataBasesNameText.Visibility = Visibility.Collapsed;
            MysqlConnectUserNameTextBox.Visibility   = Visibility.Collapsed;
            MysqlConnectUserPwdText.Visibility       = Visibility.Collapsed;
            MysqlConnectLoginClickButton.Visibility  = Visibility.Collapsed;

            MysqlUserNameDispTextBox.Visibility      = Visibility.Collapsed;
            MysqlLogoutClickButton.Visibility        = Visibility.Collapsed;
            MysqlDataGrid1DispLoadingGui.Visibility  = Visibility.Visible;
            MysqlDataGrid2DispLoadingGui.Visibility  = Visibility.Visible;
        }

        public void MysqlLogOutGuiControlVisibilitySetting()
        {
            MysqlConnectIpText.Password              = "";
            MysqlConnectPortText.Password            = "";
            MysqlConnectDataBasesNameText.Text       = "";
            MysqlConnectUserNameTextBox.Text         = "";
            MysqlConnectUserPwdText.Password         = "";
            sqlstr                                   = "";
            mysql_param_enter_successed              = 0;

            LoginTabUserAccountText.Visibility       = Visibility.Collapsed;
            LoginTabUserPwdText.Visibility           = Visibility.Collapsed;
            LoginTabCheckPwdClickButton.Visibility   = Visibility.Collapsed;

            LoginTabLogoutClickButton.Visibility     = Visibility.Visible;
            LoginTabUserNameDispTextBox.Visibility   = Visibility.Visible;

            MysqlConnectIpText.Visibility            = Visibility.Visible;
            MysqlConnectPortText.Visibility          = Visibility.Visible;
            MysqlConnectDataBasesNameText.Visibility = Visibility.Visible;
            MysqlConnectUserNameTextBox.Visibility   = Visibility.Visible;
            MysqlConnectUserPwdText.Visibility       = Visibility.Visible;
            MysqlConnectLoginClickButton.Visibility  = Visibility.Visible;

            MysqlUserNameDispTextBox.Visibility      = Visibility.Collapsed;
            MysqlLogoutClickButton.Visibility        = Visibility.Collapsed;
            MysqlDataGrid1DispLoadingGui.Visibility  = Visibility.Visible;
            MysqlDataGrid2DispLoadingGui.Visibility  = Visibility.Visible;
        }



        public void MessageBoxSetting()
        {
            MessageBoxX.MessageBoxXConfigurations.Add(
                "WarningTheme", new Panuon.UI.Silver.Core.MessageBoxXConfigurations()
                {
                    MessageBoxStyle = MessageBoxStyle.Modern,
                    MessageBoxIcon = MessageBoxIcon.Warning,
                    MinHeight = 100,
                });
            MessageBoxX.MessageBoxXConfigurations.Add(
                "ErrorTheme", new Panuon.UI.Silver.Core.MessageBoxXConfigurations()
                {
                    MessageBoxStyle = MessageBoxStyle.Modern,
                    MessageBoxIcon = MessageBoxIcon.Error,
                    MinHeight = 100,
                });
        }


        private void LoginTabCheckUserPwdClickButton_Click(object sender, RoutedEventArgs e)
        {
            var username_char = LoginTabUserAccountText.Text;
            var userpwd_char = LoginTabUserPwdText.Password;

            int username_check_val = username_char.CompareTo(app_main_user_name);
            int userpwd_check_val = userpwd_char.CompareTo(app_main_user_pwd);

            if (username_check_val == 0 && userpwd_check_val == 0)
            {
                LoginTabUserNameDispTextBox.Text         = username_char;
                LoginTabUserAccountText.Visibility       = Visibility.Collapsed;
                LoginTabUserPwdText.Visibility           = Visibility.Collapsed;
                LoginTabCheckPwdClickButton.Visibility   = Visibility.Collapsed;

                LoginTabLogoutClickButton.Visibility     = Visibility.Visible;
                LoginTabUserNameDispTextBox.Visibility   = Visibility.Visible;

                MysqlConnectIpText.Visibility            = Visibility.Visible;
                MysqlConnectPortText.Visibility          = Visibility.Visible;
                MysqlConnectDataBasesNameText.Visibility = Visibility.Visible;
                MysqlConnectUserNameTextBox.Visibility   = Visibility.Visible;
                MysqlConnectUserPwdText.Visibility       = Visibility.Visible;
                MysqlConnectLoginClickButton.Visibility  = Visibility.Visible;

                MysqlUserNameDispTextBox.Visibility      = Visibility.Collapsed;
                MysqlLogoutClickButton.Visibility        = Visibility.Collapsed;
            }
            else
            {
                MessageBoxX.Show("UserName or Pwd Error.", "Tips", Application.Current.MainWindow, configKey: "ErrorTheme");
            }

        }

        private void LoginTabLogOutClickButton_Click(object sender, RoutedEventArgs e)
        {
            GuiControlVisibilitySetting();
        }

        private void MysqlConnectLoginClickButton_Click(object sender, RoutedEventArgs e)
        {
            if (MysqlConnectIpText.Password.Length == 0 ||
                MysqlConnectPortText.Password.Length == 0 ||
                MysqlConnectDataBasesNameText.Text.Length == 0 ||
                MysqlConnectUserNameTextBox.Text.Length == 0 ||
                MysqlConnectUserPwdText.Password.Length == 0)
            {
                MessageBoxX.Show("Enter Error.", "Tips", Application.Current.MainWindow, configKey: "ErrorTheme");
            }
            else
            {
                //MessageBoxX.Show("Try Connecting ...", "Tips", Application.Current.MainWindow, configKey: "WarningTheme");
                MysqlUserNameDispTextBox.Text            = MysqlConnectIpText.Password + ":" + MysqlConnectPortText.Password + ":" + MysqlConnectUserNameTextBox.Text;
                LoginTabUserAccountText.Visibility       = Visibility.Collapsed;
                LoginTabUserPwdText.Visibility           = Visibility.Collapsed;
                LoginTabCheckPwdClickButton.Visibility   = Visibility.Collapsed;

                LoginTabLogoutClickButton.Visibility     = Visibility.Visible;
                LoginTabUserNameDispTextBox.Visibility   = Visibility.Visible;

                MysqlConnectIpText.Visibility            = Visibility.Collapsed;
                MysqlConnectPortText.Visibility          = Visibility.Collapsed;
                MysqlConnectDataBasesNameText.Visibility = Visibility.Collapsed;
                MysqlConnectUserNameTextBox.Visibility   = Visibility.Collapsed;
                MysqlConnectUserPwdText.Visibility       = Visibility.Collapsed;
                MysqlConnectLoginClickButton.Visibility  = Visibility.Collapsed;

                MysqlUserNameDispTextBox.Visibility      = Visibility.Visible;
                MysqlLogoutClickButton.Visibility        = Visibility.Visible;

                sqlstr = "Data Source=" + MysqlConnectIpText.Password           +
                         ";User ID="    + MysqlConnectUserNameTextBox.Text      +
                         ";Password="   + MysqlConnectUserPwdText.Password      +
                         ";DataBase="   + MysqlConnectDataBasesNameText.Text    +
                         ";Charset=utf8;";

                mysql_param_enter_successed = 1;
            }
        }

        private void MysqlLogOutClickButton_Click(object sender, RoutedEventArgs e)
        {
            MysqlLogOutGuiControlVisibilitySetting();
        }

        private void DataGrid_LoadingRow(object sender, System.Windows.Controls.DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        private void UpdateMySQLData()
        {
            TabItem ti = TabControlTabHandle.SelectedItem as TabItem;

            string table_item_name = ti.Header.ToString();

            if (table_item_name.CompareTo("Login") == 0)
            {
                Notice.Show("LoginTab", "Warning", 5, MessageBoxIcon.Warning);
                cur_used_table_name = "";
                return;
            }
            else if (table_item_name.CompareTo("DataGrid1") == 0)
            {
                Notice.Show("DataGrid1_Tab", "Successed", 5, MessageBoxIcon.Success);
                cur_used_table_name = mysql_tables_name1;
            }
            else if (table_item_name.CompareTo("DataGrid2") == 0)
            {
                Notice.Show("DataGrid2_Tab", "Successed", 5, MessageBoxIcon.Success);
                cur_used_table_name = mysql_tables_name2;
            }

            LoginTabLogoutClickButton.IsEnabled =false;
            MysqlLogoutClickButton.IsEnabled    =false;
            if (con != null)
            {
                con.Close();
            }

            //if (con == null)
            {
                con = new MySql.Data.MySqlClient.MySqlConnection(sqlstr);
                try
                {
                    con.Open();
                }
                catch
                {
                    LoginTabLogoutClickButton.IsEnabled =true;
                    MysqlLogoutClickButton.IsEnabled    =true;
                    MessageBoxX.Show("Connected Failed, Plz retry.", "Tips", Application.Current.MainWindow, configKey: "ErrorTheme");
                    MysqlLogOutGuiControlVisibilitySetting();
                    Console.WriteLine("Conneted Failed");
                    return;
                }

                if (con.State == ConnectionState.Open)
                {

                }
                else
                {
                    LoginTabLogoutClickButton.IsEnabled =true;
                    MysqlLogoutClickButton.IsEnabled    =true;
                    MessageBoxX.Show("Connected Failed, Plz retry.", "Tips", Application.Current.MainWindow, configKey: "ErrorTheme");
                    MysqlLogOutGuiControlVisibilitySetting();
                    Console.WriteLine("Conneted Failed");
                    return;
                }
            }

            if (adapter == null)
            {
                adapter = new MySql.Data.MySqlClient.MySqlDataAdapter("select * from " + cur_used_table_name, con);
            }
            if (ds == null)
            {
                ds = new System.Data.DataSet();
            }
            ds.Clear();
            try
            {
                adapter.Fill(ds, cur_used_table_name);
                if (dt == null)
                {
                    dt = ds.Tables[cur_used_table_name];
                }

                switch (table_item_name)
                {
                    case "DataGrid1":
                        Tab2FirstDataGrid.DataContext = dt.DefaultView;
                        MysqlDataGrid1DispLoadingGui.Visibility = Visibility.Collapsed;
                        break;
                    case "DataGrid2":
                        Tab2SecDataGrid.DataContext = dt.DefaultView;
                        MysqlDataGrid2DispLoadingGui.Visibility = Visibility.Collapsed;
                        break;
                }

                Notice.Show("Update Successed", "Successed", 5, MessageBoxIcon.Success);
            }
            catch
            {
                Notice.Show("Update Failed", "Error", 5, MessageBoxIcon.Error);
            }
            con.Close();

            LoginTabLogoutClickButton.IsEnabled =true;
            MysqlLogoutClickButton.IsEnabled    =true;
        }



        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabItem ti = TabControlTabHandle.SelectedItem as TabItem;

            string table_item_name = ti.Header.ToString();

            if (e.Source is TabControl)
            {
                if (mysql_param_enter_successed == 1)
                {
                    if (table_item_name.CompareTo("DataGrid1") == 0)
                    {
                        MysqlDataGrid2DispLoadingGui.Visibility = Visibility.Visible;
                    }
                    else if (table_item_name.CompareTo("DataGrid2") == 0)
                    {
                        MysqlDataGrid2DispLoadingGui.Visibility = Visibility.Visible;
                    }
                   
                    UpdateMySQLData();
                }
                else
                {
                    if (table_item_name.CompareTo("Login") != 0)
                    {
                        Notice.Show("UnLogin", "Error", 5, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void DataGrid_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ContextMenu context = new ContextMenu();
            MenuItem item1 = new MenuItem();
            MenuItem item2 = new MenuItem();
            MenuItem item3 = new MenuItem();
            MenuItem item4 = new MenuItem();
            item1.Header = "删除该列";
            item1.Click += new RoutedEventHandler(DataGrid_DeleteColItemClick);
            context.Items.Add(item1);
            context.IsOpen = true;

            item2.Header = "删除该行";
            item2.Click += new RoutedEventHandler(DataGrid_DeleteRowItemClick);
            context.Items.Add(item2);
            context.IsOpen = true;

            item3.Header = "插入列";
            item3.Click += new RoutedEventHandler(DataGrid_AddColItemClick);
            context.Items.Add(item3);
            context.IsOpen = true;

            item4.Header = "插入行";
            item4.Click += new RoutedEventHandler(DataGrid_AddRowItemClick);
            context.Items.Add(item4);
            context.IsOpen = true;
        }
        void DataGrid_DeleteColItemClick(object sender, RoutedEventArgs e)
        {
            // do something
            Notice.Show("Button Delete Col Click", "Successed", 5, MessageBoxIcon.Success);
        }

        void DataGrid_DeleteRowItemClick(object sender, RoutedEventArgs e)
        {
            // do something
            Notice.Show("Button Delete Row Click", "Successed", 5, MessageBoxIcon.Success);
        }

        void DataGrid_AddColItemClick(object sender, RoutedEventArgs e)
        {
            // do something
            Notice.Show("Button Add Col Click", "Successed", 5, MessageBoxIcon.Success);
        }

        void DataGrid_AddRowItemClick(object sender, RoutedEventArgs e)
        {
            // do something
            Notice.Show("Button Add Row Click", "Successed", 5, MessageBoxIcon.Success);
        }

        #region 引用 EVERYTHING DLL
        const int EVERYTHING_OK = 0;
        const int EVERYTHING_ERROR_MEMORY = 1;
        const int EVERYTHING_ERROR_IPC = 2;
        const int EVERYTHING_ERROR_REGISTERCLASSEX = 3;
        const int EVERYTHING_ERROR_CREATEWINDOW = 4;
        const int EVERYTHING_ERROR_CREATETHREAD = 5;
        const int EVERYTHING_ERROR_INVALIDINDEX = 6;
        const int EVERYTHING_ERROR_INVALIDCALL = 7;

        const int EVERYTHING_REQUEST_FILE_NAME = 0x00000001;
        const int EVERYTHING_REQUEST_PATH = 0x00000002;
        const int EVERYTHING_REQUEST_FULL_PATH_AND_FILE_NAME = 0x00000004;
        const int EVERYTHING_REQUEST_EXTENSION = 0x00000008;
        const int EVERYTHING_REQUEST_SIZE = 0x00000010;
        const int EVERYTHING_REQUEST_DATE_CREATED = 0x00000020;
        const int EVERYTHING_REQUEST_DATE_MODIFIED = 0x00000040;
        const int EVERYTHING_REQUEST_DATE_ACCESSED = 0x00000080;
        const int EVERYTHING_REQUEST_ATTRIBUTES = 0x00000100;
        const int EVERYTHING_REQUEST_FILE_LIST_FILE_NAME = 0x00000200;
        const int EVERYTHING_REQUEST_RUN_COUNT = 0x00000400;
        const int EVERYTHING_REQUEST_DATE_RUN = 0x00000800;
        const int EVERYTHING_REQUEST_DATE_RECENTLY_CHANGED = 0x00001000;
        const int EVERYTHING_REQUEST_HIGHLIGHTED_FILE_NAME = 0x00002000;
        const int EVERYTHING_REQUEST_HIGHLIGHTED_PATH = 0x00004000;
        const int EVERYTHING_REQUEST_HIGHLIGHTED_FULL_PATH_AND_FILE_NAME = 0x00008000;

        const int EVERYTHING_SORT_NAME_ASCENDING = 1;
        const int EVERYTHING_SORT_NAME_DESCENDING = 2;
        const int EVERYTHING_SORT_PATH_ASCENDING = 3;
        const int EVERYTHING_SORT_PATH_DESCENDING = 4;
        const int EVERYTHING_SORT_SIZE_ASCENDING = 5;
        const int EVERYTHING_SORT_SIZE_DESCENDING = 6;
        const int EVERYTHING_SORT_EXTENSION_ASCENDING = 7;
        const int EVERYTHING_SORT_EXTENSION_DESCENDING = 8;
        const int EVERYTHING_SORT_TYPE_NAME_ASCENDING = 9;
        const int EVERYTHING_SORT_TYPE_NAME_DESCENDING = 10;
        const int EVERYTHING_SORT_DATE_CREATED_ASCENDING = 11;
        const int EVERYTHING_SORT_DATE_CREATED_DESCENDING = 12;
        const int EVERYTHING_SORT_DATE_MODIFIED_ASCENDING = 13;
        const int EVERYTHING_SORT_DATE_MODIFIED_DESCENDING = 14;
        const int EVERYTHING_SORT_ATTRIBUTES_ASCENDING = 15;
        const int EVERYTHING_SORT_ATTRIBUTES_DESCENDING = 16;
        const int EVERYTHING_SORT_FILE_LIST_FILENAME_ASCENDING = 17;
        const int EVERYTHING_SORT_FILE_LIST_FILENAME_DESCENDING = 18;
        const int EVERYTHING_SORT_RUN_COUNT_ASCENDING = 19;
        const int EVERYTHING_SORT_RUN_COUNT_DESCENDING = 20;
        const int EVERYTHING_SORT_DATE_RECENTLY_CHANGED_ASCENDING = 21;
        const int EVERYTHING_SORT_DATE_RECENTLY_CHANGED_DESCENDING = 22;
        const int EVERYTHING_SORT_DATE_ACCESSED_ASCENDING = 23;
        const int EVERYTHING_SORT_DATE_ACCESSED_DESCENDING = 24;
        const int EVERYTHING_SORT_DATE_RUN_ASCENDING = 25;
        const int EVERYTHING_SORT_DATE_RUN_DESCENDING = 26;

        const int EVERYTHING_TARGET_MACHINE_X86 = 1;
        const int EVERYTHING_TARGET_MACHINE_X64 = 2;
        const int EVERYTHING_TARGET_MACHINE_ARM = 3;

        [DllImport("Everything64.dll", CharSet = CharSet.Unicode)]
        public static extern UInt32 Everything_SetSearchW(string lpSearchString);
        [DllImport("Everything64.dll")]
        public static extern void Everything_SetMatchPath(bool bEnable);
        [DllImport("Everything64.dll")]
        public static extern void Everything_SetMatchCase(bool bEnable);
        [DllImport("Everything64.dll")]
        public static extern void Everything_SetMatchWholeWord(bool bEnable);
        [DllImport("Everything64.dll")]
        public static extern void Everything_SetRegex(bool bEnable);
        [DllImport("Everything64.dll")]
        public static extern void Everything_SetMax(UInt32 dwMax);
        [DllImport("Everything64.dll")]
        public static extern void Everything_SetOffset(UInt32 dwOffset);

        [DllImport("Everything64.dll")]
        public static extern bool Everything_GetMatchPath();
        [DllImport("Everything64.dll")]
        public static extern bool Everything_GetMatchCase();
        [DllImport("Everything64.dll")]
        public static extern bool Everything_GetMatchWholeWord();
        [DllImport("Everything64.dll")]
        public static extern bool Everything_GetRegex();
        [DllImport("Everything64.dll")]
        public static extern UInt32 Everything_GetMax();
        [DllImport("Everything64.dll")]
        public static extern UInt32 Everything_GetOffset();
        [DllImport("Everything64.dll")]
        public static extern IntPtr Everything_GetSearchW();
        [DllImport("Everything64.dll")]
        public static extern UInt32 Everything_GetLastError();

        [DllImport("Everything64.dll")]
        public static extern bool Everything_QueryW(bool bWait);

        [DllImport("Everything64.dll")]
        public static extern void Everything_SortResultsByPath();

        [DllImport("Everything64.dll")]
        public static extern UInt32 Everything_GetNumFileResults();
        [DllImport("Everything64.dll")]
        public static extern UInt32 Everything_GetNumFolderResults();
        [DllImport("Everything64.dll")]
        public static extern UInt32 Everything_GetNumResults();
        [DllImport("Everything64.dll")]
        public static extern UInt32 Everything_GetTotFileResults();
        [DllImport("Everything64.dll")]
        public static extern UInt32 Everything_GetTotFolderResults();
        [DllImport("Everything64.dll")]
        public static extern UInt32 Everything_GetTotResults();
        [DllImport("Everything64.dll")]
        public static extern bool Everything_IsVolumeResult(UInt32 nIndex);
        [DllImport("Everything64.dll")]
        public static extern bool Everything_IsFolderResult(UInt32 nIndex);
        [DllImport("Everything64.dll")]
        public static extern bool Everything_IsFileResult(UInt32 nIndex);
        [DllImport("Everything64.dll", CharSet = CharSet.Unicode)]
        public static extern void Everything_GetResultFullPathName(UInt32 nIndex, StringBuilder lpString, UInt32 nMaxCount);
        [DllImport("Everything64.dll")]
        public static extern void Everything_Reset();

        [DllImport("Everything64.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr Everything_GetResultFileName(UInt32 nIndex);

        // Everything 1.4
        [DllImport("Everything64.dll")]
        public static extern void Everything_SetSort(UInt32 dwSortType);
        [DllImport("Everything64.dll")]
        public static extern UInt32 Everything_GetSort();
        [DllImport("Everything64.dll")]
        public static extern UInt32 Everything_GetResultListSort();
        [DllImport("Everything64.dll")]
        public static extern void Everything_SetRequestFlags(UInt32 dwRequestFlags);
        [DllImport("Everything64.dll")]
        public static extern UInt32 Everything_GetRequestFlags();
        [DllImport("Everything64.dll")]
        public static extern UInt32 Everything_GetResultListRequestFlags();
        [DllImport("Everything64.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr Everything_GetResultExtension(UInt32 nIndex);
        [DllImport("Everything64.dll")]
        public static extern bool Everything_GetResultSize(UInt32 nIndex, out long lpFileSize);
        [DllImport("Everything64.dll")]
        public static extern bool Everything_GetResultDateCreated(UInt32 nIndex, out long lpFileTime);
        [DllImport("Everything64.dll")]
        public static extern bool Everything_GetResultDateModified(UInt32 nIndex, out long lpFileTime);
        [DllImport("Everything64.dll")]
        public static extern bool Everything_GetResultDateAccessed(UInt32 nIndex, out long lpFileTime);
        [DllImport("Everything64.dll")]
        public static extern UInt32 Everything_GetResultAttributes(UInt32 nIndex);
        [DllImport("Everything64.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr Everything_GetResultFileListFileName(UInt32 nIndex);
        [DllImport("Everything64.dll")]
        public static extern UInt32 Everything_GetResultRunCount(UInt32 nIndex);
        [DllImport("Everything64.dll")]
        public static extern bool Everything_GetResultDateRun(UInt32 nIndex, out long lpFileTime);
        [DllImport("Everything64.dll")]
        public static extern bool Everything_GetResultDateRecentlyChanged(UInt32 nIndex, out long lpFileTime);
        [DllImport("Everything64.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr Everything_GetResultHighlightedFileName(UInt32 nIndex);
        [DllImport("Everything64.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr Everything_GetResultHighlightedPath(UInt32 nIndex);
        [DllImport("Everything64.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr Everything_GetResultHighlightedFullPathAndFileName(UInt32 nIndex);
        [DllImport("Everything64.dll")]
        public static extern UInt32 Everything_GetRunCountFromFileName(string lpFileName);
        [DllImport("Everything64.dll")]
        public static extern bool Everything_SetRunCountFromFileName(string lpFileName, UInt32 dwRunCount);
        [DllImport("Everything64.dll")]
        public static extern UInt32 Everything_IncRunCountFromFileName(string lpFileName);
        #endregion



        private void UsedEverythingApisButtonSearch_Click(object sender, EventArgs e)
        {
            string str = Tab4SearchFilesText.Text.ToString();
            if (string.IsNullOrWhiteSpace(str))
            {
                Notice.Show("Noting To Find", "Warning", 5, MessageBoxIcon.Warning);
                return;
            }
            
            UInt32 i;
            // set the search
            Everything_SetSearchW(Tab4SearchFilesText.Text);

            // use our own custom scrollbar... 			
            // Everything_SetMax(listBox1.ClientRectangle.Height / listBox1.ItemHeight);
            // Everything_SetOffset(VerticalScrollBarPosition...);

            // request name and size
            Everything_SetRequestFlags(EVERYTHING_REQUEST_FILE_NAME | EVERYTHING_REQUEST_PATH | EVERYTHING_REQUEST_DATE_MODIFIED | EVERYTHING_REQUEST_SIZE);

            Everything_SetSort(13);

            // execute the query
            Everything_QueryW(true);

            // sort by path
            // Everything_SortResultsByPath();

            // clear the old list of results			
            Tab4EverythingApisSearchingResultListView.Items.Clear();

            // set the window title
            //Text = Tab4SearchFilesText.Text + " - " + Everything_GetNumResults() + " Results";

            // loop through the results, adding each result to the listbox.
            for (i = 0; i < Everything_GetNumResults(); i++)
            {
                long date_modified;
                long size;
                var  fullpath = new StringBuilder(1024); ;

                Everything_GetResultDateModified(i, out date_modified);
                Everything_GetResultSize(i, out size);
                Everything_GetResultFullPathName(i, fullpath, 1024);
                // add it to the list box				
                //Tab4EverythingApisSearchingResultListView.Items.Insert((int)i,
                //    //"Size: " + size.ToString()
                //    //+ " Date Modified: " + DateTime.FromFileTime(date_modified).Year
                //    //+ "/" + DateTime.FromFileTime(date_modified).Month
                //    //+ "/" + DateTime.FromFileTime(date_modified).Day + " "
                //    //+ DateTime.FromFileTime(date_modified).Hour + ":"
                //    //+ DateTime.FromFileTime(date_modified).Minute.ToString("D2")
                //    //+ 
                //    " Filename: " + Marshal.PtrToStringUni(Everything_GetResultFileName(i))
                //    + " FullPath: " + fullpath.ToString()
                //    );
                Tab4EverythingApisSearchingResultListView.Items.Add(
                    new { FileName = Marshal.PtrToStringUni(Everything_GetResultFileName(i)), FullPath = fullpath.ToString() });
            }
        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                Notice.Show("   You selected " + item.ToString() + ".", "Warning", 5, MessageBoxIcon.Warning);
            }
        }

        private void ListViewItem_PreviewMouseDoubleClick(object sender, RoutedEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                var nameOfProperty = "FullPath";
                var propertyInfo = item.GetType().GetProperty(nameOfProperty);
                var FullPath = propertyInfo.GetValue(item, null).ToString();
                System.Diagnostics.Process.Start("explorer.exe", FullPath);
            }
        }
    }
}


















