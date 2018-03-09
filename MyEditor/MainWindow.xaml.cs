using MyCodeBox;
using MyPicTabPage;
using MyProjectClassData;
using MyProjectPanel;
using MyXCodeDataOption;
using MyXObject;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Xceed.Wpf.AvalonDock.Layout;
using MyXToolsPanel;
using System.Collections.Generic;

namespace MyEditor
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        #region 构造函数
        public MainWindow()
        {
            InitializeComponent();
            #region 初始化布局
            _dllPanel = AddLayoutAnchorablePane(LeftAnchorableGroup, "Dll文件");
            _functionPanel = AddLayoutAnchorablePane(LeftAnchorableGroup, "方法");
            _xaributePanel = AddLayoutAnchorablePane(LeftAnchorableGroup, "字段");
            _solutionPanel = AddLayoutAnchorablePane(RightAnchorableGroup, "解决方案");
            _valuePanel = AddLayoutAnchorablePane(RightAnchorableGroup, "属性");
            _outWindows = outw;
            _outWindows.Content = new RichTextBox();
            #endregion
            #region 初始化信息
            _myProject = new ProjectClass();
            Solution = new SolutionClass();
            #endregion
        }
        #endregion
        #region 自定义属性
        /// <summary>
        /// 生成ID
        /// </summary>
        private int IDNum = 0;
        /// <summary>
        /// 当前正在操作的项目类
        /// </summary>
        private ProjectClass _myProject;
        /// <summary>
        /// 解决方案
        /// </summary>
        private SolutionClass _solution;
        /// <summary>
        /// 存放dll文件的面板
        /// </summary>
        private LayoutAnchorable _dllPanel;
        /// <summary>
        /// 存放函数的面板
        /// </summary>
        private LayoutAnchorable _functionPanel;
        /// <summary>
        /// 字段的面板
        /// </summary>
        private LayoutAnchorable _xaributePanel;
        /// <summary>
        /// 属性面板
        /// </summary>
        private LayoutAnchorable _valuePanel;
        /// <summary>
        /// 解决方案面板
        /// </summary>
        private LayoutAnchorable _solutionPanel;
        /// <summary>
        /// 输出窗体
        /// </summary>
        private LayoutAnchorable _outWindows;
        /// <summary>
        /// 属性面板窗体
        /// </summary>
        private MyPropertiesToolPanel _propertiesWindow = new MyPropertiesToolPanel();
        #endregion
        #region 读取器
        /// <summary>
        /// 当前正在操作的项目类
        /// </summary>
        public ProjectClass MyProject
        {
            get
            {
                return _myProject;
            }

            set
            {
                _myProject = value;
            }
        }
        /// <summary>
        /// 解决方案
        /// </summary>
        public SolutionClass Solution
        {
            get
            {
                return _solution;
            }

            set
            {
                _solution = value;
            }
        }
        #endregion
        #region 函数
        #region 自定义函数
        /// <summary>
        /// 创建一个空白的Pictabpage
        /// </summary>
        /// <param name="Title">页面的标题</param>
        public void CreatePicTabPage(string Title)
        {
            PicTabPage page = new PicTabPage(CreateID(), this.ChileEventCallBack, Title);
            AddLayoutDocument(ContentPanel, page);
            MyProject.AddPicTabPage(page);
        }
        /// <summary>
        /// 向当前添加一个pictabpage
        /// </summary>
        /// <param name="page">要添加的page</param>
        public void AddPicTabPage(PicTabPage page)
        {
            AddLayoutDocument(ContentPanel, page);
            ///设置回调函数
            page.CallBackFunction = this.ChileEventCallBack;
            MyProject.AddPicTabPage(page);
        }
        #region 各个窗体的操作
        /// <summary>
        /// 绑定dll文件窗口数据
        /// </summary>
        public void BindingDllWindow()
        {
            MyProjectDllFilePanel dllpanel = new MyProjectDllFilePanel();
            dllpanel.DirDllFile(_myProject);
            _dllPanel.Content = dllpanel;
        }
        /// <summary>
        /// 绑定函数面板
        /// </summary>
        /// <param name="pic">要绑定的picTabPage</param>
        public void BindingFunctionWindow(PicTabPage pic)
        {
            MyFunctionPanel funcpanel = new MyFunctionPanel();
            funcpanel.SetCallFunction(this.ChileEventCallBack);
            funcpanel.DirFunction(pic);
            _functionPanel.Content = funcpanel;
        }
        /// <summary>
        /// 绑定属性面板
        /// </summary>
        /// <param name="pic">要绑定的picTabPage</param>
        public void BindingXAributeWindow(PicTabPage pic)
        {
            MyXAributePanel xaributepanel = new MyXAributePanel();
            xaributepanel.SetCallFunction(this.ChileEventCallBack);
            xaributepanel.DirXAribute(pic);
            _xaributePanel.Content = xaributepanel;
        }
        /// <summary>
        /// 绑定文件系统
        /// </summary>
        public void BindingSolutionWindow()
        {
            MySolutionPanel solutionpanel = new MySolutionPanel();
            solutionpanel.DirSolutionPanel(_myProject);
            _solutionPanel.Content = solutionpanel;
        }
        /// <summary>
        /// 绑定内容属性面板
        /// </summary>
        public void BindingPropertiesWindow(object obj)
        {
            if (obj == null)
                return;
            ///初始化面板信息
            _propertiesWindow.SetTargetObj(obj);
            XObject xobj = obj as XObject;
            #region 设置不显示的数据
            List<string> notdirlist = new List<string>();
            if (xobj != null)
            {            
                notdirlist.Add("SelectPositionStyle");
                notdirlist.Add("SelectSpc");
                notdirlist.Add("SelectType");
                notdirlist.Add("FontSize");
                notdirlist.Add("CanLinkNum");
                notdirlist.Add("Icon");
                notdirlist.Add("Id");
                notdirlist.Add("Alpha");
                notdirlist.Add("BorderColor");
                notdirlist.Add("DefultColor");
                notdirlist.Add("Radius");
                notdirlist.Add("SelectBorderWidth");
                notdirlist.Add("SelectColor");
                notdirlist.Add("BorderWidth");
                notdirlist.Add("MyFont");
                notdirlist.Add("FontColor");
                notdirlist.Add("BkColor");
                notdirlist.Add("Name");
                notdirlist.Add("Width");
                notdirlist.Add("MinWidth");
                notdirlist.Add("MaxWidth");
                notdirlist.Add("Height");
                notdirlist.Add("MinHeight");
                notdirlist.Add("MaxHeight");
                notdirlist.Add("FlowDirection");
                notdirlist.Add("HorizontalAlignment");
                notdirlist.Add("VerticalAlignment");
                notdirlist.Add("Opacity");
                notdirlist.Add("Uid");
                notdirlist.Add("Visibility");              
            }
            _propertiesWindow.AddNotDirProperties(notdirlist);
            #endregion
            _propertiesWindow.Play();
            _valuePanel.Content = _propertiesWindow;
        }
        /// <summary>
        /// 绑定所有数据面板
        /// </summary>
        public void BindingPanels(PicTabPage mainpage)
        {
            ///绑定各个面板数据
            BindingDllWindow();
            BindingFunctionWindow(mainpage);
            BindingXAributeWindow(mainpage);
            //BindingPropertiesWindow();
            BindingSolutionWindow();
        }
        #endregion
        /// <summary>
        /// 生成ID
        /// </summary>
        /// <returns></returns>
        protected int CreateID()
        {
            return IDNum++;
        }
        #region 初始化函数模块
        /// <summary>
        /// 初始化Dll面板
        /// </summary>
        protected void InitDllPanel()
        {
            MyProjectDllFilePanel dllp = new MyProjectDllFilePanel();
            dllp.DirDllFile(MyProject);
            _dllPanel.Content = dllp;

        }
        #endregion
        #endregion
        #region 窗体事件
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
        /// <summary>
        /// 键盘按下
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            //cav.OnMyKeyDown(e);
            foreach (LayoutDocument document in ContentPanel.Children)
            {
                if (document.IsActive == true)
                {
                    ((PicTabPage)document.Content).OnMyKeyDown(e);
                }
            }
        }
        /// <summary>
        /// 键盘松开
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            foreach (LayoutDocument document in ContentPanel.Children)
            {
                if (document.IsActive == true)
                {
                    ((PicTabPage)document.Content).OnMyKeyUp(e);
                }
            }
            //cav.OnMyKeyUp(e);
        }
        #endregion
        #region 菜单控件事件
        /// <summary>
        /// 添加代码块事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddCodeBoxXml_Click(object sender, RoutedEventArgs e)
        {
            AddCodeBoxXml ACodeBox = new MyEditor.AddCodeBoxXml();
            ACodeBox.Show();
        }
        /// <summary>
        /// 新建项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewProject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NewProject nProject = new NewProject();
                ///设置项目类
                nProject.SetProjectClass(_myProject,_solution);
                nProject.ShowDialog();
                if (_myProject.RootPath != "")
                {
                    ///设置项目根目录
                    Solution.RootPath = _myProject.RootPath;
                    ///创建主类和主函数信息
                    PicTabPage mainpage = new PicTabPage((new Random()).Next(1, 2000000), this.ChileEventCallBack, "Program.cx");
                    CodeBox mainbox = mainpage.CreateXCodeBox("Main", PicTabPage.CenterPoint, CodeBox.XAType.XMain);
                    mainbox.AddAttribute(MyXAribute.XAribute.XAttributeType.XExc, MyXAribute.XAribute.XAttributeSpec.XNone,
                        MyXAribute.XAribute.XPositonStyle.right, "出口", MyXAribute.XAribute.CanLinkType.One, "主函数出口", "");
                    ///添加主信息
                    _myProject.AddPicTabPage(mainpage);
                    ///将代码图添加到tab页
                    AddLayoutDocument(ContentPanel, mainpage);
                    ///绑定各个面板数据
                    BindingPanels(mainpage);
                }
                ///保存配置文件路径
                Solution.SolutionConfigPath = XCreateConfigurationInformation.SolutionConfigPath;
                ///添加到解决方案
                Solution.AddProjectClass(MyProject);
            }
            catch (Exception ex)
            {
                LoggerHelp.WriteLogger(ex.ToString());
            }
        }
        /// <summary>
        /// 打开项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenProject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Forms.OpenFileDialog openFile = new System.Windows.Forms.OpenFileDialog();
                openFile.Filter = "配置文件 | *.xpl;";
                if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    ///保存解决方案配置文件路径
                    Solution.SolutionConfigPath = openFile.FileName;
                    ///加载解决方案
                    Solution.LoadSolution(openFile.FileName);
                    MyProject = Solution.GetFirstProject();
                    ///获取主类
                    PicTabPage mainpage = _myProject.GetFirstPicTabPage();
                    ///将代码图添加到tab页
                    AddLayoutDocument(ContentPanel, mainpage);
                    BindingPanels(mainpage);
                }
            }
            catch (Exception ex)
            {
                LoggerHelp.WriteLogger(ex.ToString());
            }
        }
        /// <summary>
        /// 运行项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Run_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string ErrorString = Solution.Editor();
                ///显示编译信息
                ((RichTextBox)_outWindows.Content).AppendText(ErrorString);
            }catch(Exception ex)
            {
                LoggerHelp.WriteLogger(ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 生成相应代码
        /// </summary>
        /// <param name="sender">事件发送者</param>
        /// <param name="e">事件信息</param>
        private void Code_Click(object sender, RoutedEventArgs e)
        {
            if(_solution != null)
            {
                try
                {
                    //保存代码
                    _solution.SaveCode();
                }
                catch (Exception ex)
                {
                    LoggerHelp.WriteLogger(ex.ToString());
                    ((RichTextBox)_outWindows.Content).AppendText(ex.Message);
                }
            }
            else
            {
                ((RichTextBox)_outWindows.Content).AppendText("还没有项目，请先创建或者打开项目然后再使用生成代码功能！");
            }
        }
        /// <summary>
        /// 保存项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveProject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Solution.SaveSolution();
            }
            catch (Exception ex)
            {
                LoggerHelp.WriteLogger(ex.ToString());
            }
        }
        /// <summary>
        /// Dll文件窗口的显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DllFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Root.Hidden.Count != 0)
                {
                    for (int i = 0; i < Root.Hidden.Count; i++)
                    {
                        if (Root.Hidden[i].Title == _dllPanel.Title)
                        {
                            Root.Hidden[i].Show();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerHelp.WriteLogger(ex.ToString());
            }
        }
        /// <summary>
        /// 函数窗口的显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FunctionFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Root.Hidden.Count != 0)
                {
                    for (int i = 0; i < Root.Hidden.Count; i++)
                    {
                        if (Root.Hidden[i].Title == _functionPanel.Title)
                        {
                            Root.Hidden[i].Show();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerHelp.WriteLogger(ex.ToString());
            }
        }
        /// <summary>
        /// 字段窗口的显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Root.Hidden.Count != 0)
                {
                    for (int i = 0; i < Root.Hidden.Count; i++)
                    {
                        if (Root.Hidden[i].Title == _xaributePanel.Title)
                        {
                            Root.Hidden[i].Show();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerHelp.WriteLogger(ex.ToString());
            }
        }
        /// <summary>
        /// 解决方案面板显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SolutionFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Root.Hidden.Count != 0)
                {
                    for (int i = 0; i < Root.Hidden.Count; i++)
                    {
                        if (Root.Hidden[i].Title == _solutionPanel.Title)
                        {
                            Root.Hidden[i].Show();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerHelp.WriteLogger(ex.ToString());
            }
        }
        /// <summary>
        /// 属性面板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XAributeFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Root.Hidden.Count != 0)
                {
                    for (int i = 0; i < Root.Hidden.Count; i++)
                    {
                        if (Root.Hidden[i].Title == _valuePanel.Title)
                        {
                            Root.Hidden[i].Show();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerHelp.WriteLogger(ex.ToString());
            }
        }
        /// <summary>
        /// 输出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OUTWindow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Root.Hidden.Count != 0)
                {
                    for (int i = 0; i < Root.Hidden.Count; i++)
                    {
                        if (Root.Hidden[i].Title == _outWindows.Title)
                        {
                            Root.Hidden[i].Show();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerHelp.WriteLogger(ex.ToString());
            }
        }
        #endregion
        #region 主布局函数
        /// <summary>
        /// 添加竖直的窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miAnchorVerticalPane_Click_1(object sender, RoutedEventArgs e)
        {
            AddLayoutAnchorablePane(RightAnchorableGroup, "属性");
        }
        #region 添加布局函数集合
        /// <summary>
        /// 获取一个布局可抛锚窗格
        /// </summary>
        /// <param name="Title">标题</param>
        /// <param name="FatherLayout">父布局</param>
        /// <returns></returns>
        public LayoutAnchorable AddLayoutAnchorablePane(LayoutAnchorablePaneGroup FatherLayout, string Title = "属性")
        {
            try
            {
                LayoutAnchorablePane pane = new LayoutAnchorablePane();
                LayoutAnchorable anchorable = new LayoutAnchorable();
                anchorable.Title = Title;
                pane.Children.Add(anchorable);
                FatherLayout.Children.Add(pane);
                return anchorable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "[MainWindow][miAnchorVerticalPane_Click_1]");
                return null;
            }
        }
        /// <summary>
        /// 添加一个LayoutDocument元素
        /// </summary>
        /// <param name="fatherPane">LayoutDocumentPane类型LayoutDocument的父级元素</param>
        /// <param name="Title">标题</param>
        /// <returns></returns>
        public bool AddLayoutDocument(LayoutDocumentPane fatherPane, PicTabPage page)
        {
            if (!CheckIsOpen(page))
            {
                LayoutDocument lt = new LayoutDocument();
                lt.Title = page.Title;
                lt.Content = page;
                ///添加回调事件
                page.CallBackFunction = this.ChileEventCallBack;
                fatherPane.Children.Add(lt);
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 检测这个是否已经打开
        /// </summary>
        /// <param name="page">要检查的页面</param>
        /// <returns>返回结果</returns>
        public bool CheckIsOpen(PicTabPage page)
        {
            try
            {
                if (ContentPanel.Children.Count == 0)
                    return false;
                foreach (LayoutDocument doc in ContentPanel.Children)
                {
                    if ((doc.Content as PicTabPage).Equals(page))
                        return true;
                }
            }catch(Exception ex)
            {
                LoggerHelp.WriteLogger(ex.ToString());
                return false;
            }
            return false;
        }
        /// <summary>
        /// LayoutDocument控件改变时候
        /// </summary>
        /// <param name="sender">消息发送者</param>
        /// <param name="e">事件</param>
        public void LayoutDocumentChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                if (sender.GetType().Name == "LayoutDocument")
                {
                    LayoutDocument ldt = (LayoutDocument)sender;
                    if (ldt.Content != null)
                    {
                        PicTabPage page = (PicTabPage)ldt.Content;
                        page.Width = ldt.FloatingWidth;
                        page.Height = ldt.FloatingHeight;
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerHelp.WriteLogger(ex.ToString());
            }
        }
        #endregion
        #endregion
        #region 子元素回调函数
        /// <summary>
        /// 资源数回调
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ms"></param>
        /// <param name="data"></param>
        public object ChileEventCallBack(Object sender, MouseState ms, XObjectData data = null)
        {
            switch (ms)
            {
                case MouseState.XSelectObjectToDir:
                    BindingPropertiesWindow(data.data);
                    break;
                case MouseState.XOpenFunction:
                    PicFunctionTabPage fun = data.data as PicFunctionTabPage;
                    if (fun != null)
                    {
                        ///将代码图添加到tab页
                        AddLayoutDocument(ContentPanel, fun);
                    }
                    break;
            }
            return null;
        }
        #endregion
        #endregion
    }
}
