using MyProjectClassData;
using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MyProjectPanel
{
    /// <summary>
    /// MySolutionPanel.xaml 的交互逻辑
    /// </summary>
    public partial class MySolutionPanel : MyPanel
    {
        #region 构造函数
        public MySolutionPanel()
        {
            InitializeComponent();
            SolutionIcon.Source = new BitmapImage(new Uri(System.Windows.Forms.Application.StartupPath + "//Icon//SolutionIcon.jpg"));
        }
        #endregion

        #region 属性
        /// <summary>
        /// 项目类
        /// </summary>
        private ProjectClass myProject;
        #endregion

        #region 自定义方法
        /// <summary>
        /// 设置数据源
        /// </summary>
        public void DirSolutionPanel(ProjectClass myProject)
        {
            this.myProject = myProject;
            SolutionTree.ItemsSource = this.myProject.MyListProjectFiles;
        }
        #endregion
    }
}
