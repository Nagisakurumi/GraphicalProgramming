using MyProjectClassData;
using MyProjectData;
using MyXCodeDataOption;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Forms;
using MyXToolsPanel;

namespace MyEditor
{
    /// <summary>
    /// NewProject.xaml 的交互逻辑
    /// </summary>
    public partial class NewProject : Window
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public NewProject()
        {
            InitializeComponent();
            ///初始化数据
            InitProjectTypeData();
            ///初始化路径
            InitProjectPath();
            ///设置默认值
            DefaultSelected();
        }
        #endregion
        #region 属性
        /// <summary>
        /// 项目类型数据源
        /// </summary>
        private ObservableCollection<XProjectDataItem> _projectTypeName = new ObservableCollection<XProjectDataItem>();
        /// <summary>
        /// 项目类
        /// </summary>
        private ProjectClass _myProject;
        /// <summary>
        /// 解决方案
        /// </summary>
        private SolutionClass _mySolution;
        /// <summary>
        /// 图标路径
        /// </summary>
        private static string _iconPath = System.Windows.Forms.Application.StartupPath;
        #endregion
        #region 读取器
        /// <summary>
        /// 项目类型数据源
        /// </summary>
        public ObservableCollection<XProjectDataItem> ProjectTypeName
        {
            get
            {
                return _projectTypeName;
            }

            set
            {
                _projectTypeName = value;
            }
        }
        #endregion
        #region 初始化数据
        /// <summary>
        /// 初始化可以创建的项目类型数据
        /// </summary>
        private void InitProjectTypeData()
        {
            ///添加C#工程
            XProjectDataItem dataCsharp = new XProjectDataItem(_iconPath + "//Icon//kong.png", "控制台程序");
            dataCsharp.ProjectLanguages = "C#";
            ProjectTypeName.Add(dataCsharp);
            ///添加C语言工程
            XProjectDataItem dataC = new XProjectDataItem(_iconPath + "//Icon//ClanguageIcon.jpg", "C语言程序");
            dataC.ProjectLanguages = "C";
            ProjectTypeName.Add(dataC) ;
            ProjectType.ItemsSource = this._projectTypeName;
        }
        /// <summary>
        /// 设置默认选中项
        /// </summary>
        private void DefaultSelected()
        {
            if(ProjectTypeName.Count > 0)
            {
                ProjectTypeName[0].IsSelected = true;
                ProjectType.SelectedIndex = 0;
            }
        }
        /// <summary>
        /// 初始化项目路径
        /// </summary>
        private void InitProjectPath()
        {
            projectPath.Text = System.Windows.Forms.Application.StartupPath;
        }
        #endregion    
        #region 界面控件事件
        /// <summary>
        /// 选择项目创建路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog m_Dialog = new FolderBrowserDialog();
            DialogResult result = m_Dialog.ShowDialog();
            ///如果用户选择取消的话
            if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            string m_Dir = m_Dialog.SelectedPath.Trim();
            this.projectPath.Text = m_Dir;
        }
        /// <summary>
        /// 取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 确认创建项目按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Click(object sender, RoutedEventArgs e)
        {
            if(projectName.Text == "")
            {
                System.Windows.MessageBox.Show("项目名称不能为空");
                return;
            }
            else if(projectPath.Text == "")
            {
                System.Windows.MessageBox.Show("项目存放路径不能为空");
                return;
            }
            XProjectDataItem data = ProjectType.SelectedItem as XProjectDataItem;
            if(data == null)
            {
                return;
            }
            XCreateConfigurationInformation xConfig = new XCreateConfigurationInformation();
            bool isSuccessed = xConfig.CreateOpenFile(projectPath.Text,
                data.OpenFileExtension, projectName.Text,
                data.ProjectFileExtension,data.ProjectLanguages);
            if (isSuccessed)
            {
                _myProject.OutPath = xConfig.OutPath;
                _myProject.RootPath = xConfig.RootPath;
                _myProject.FilesPath = xConfig.FilesPath;
                _myProject.ProjectName = projectName.Text + "." + data.ProjectFileExtension;
                _myProject.ConfigFilePath = xConfig.ConfigFilePath;
                ///设置项目语言
                _myProject.Language = data.ProjectLanguages;
                ///设置项目语言
                _mySolution.ProjectLanguage = data.ProjectLanguages;
                ProjectClass.Version = XCreateConfigurationInformation.Version;
                ProjectClass.Editorname = XCreateConfigurationInformation.Editorname;
                this.Close();
            }
        
        }
        #endregion
        #region 自定义函数
        /// <summary>
        /// 设置项目类
        /// </summary>
        /// <param name="proClass">项目类的实例</param>
        /// <param name="solution">解决方案的实例</param>
        public void SetProjectClass(ProjectClass proClass, SolutionClass solution)
        {
            _myProject = proClass;
            _mySolution = solution;
        }
        #endregion
    }
}
