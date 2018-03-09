using MyProjectClassData;
using MyProjectData;
using System.Windows;
using System.Windows.Forms;
using System.IO;

namespace MyProjectPanel
{
    /// <summary>
    /// MyProjectDllFilePanel.xaml 的交互逻辑
    /// </summary>
    public partial class MyProjectDllFilePanel : MyPanel
    {
        #region 属性
        /// <summary>
        /// 存放项目的参数
        /// </summary>
        private ProjectClass projectDll;
        #endregion
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public MyProjectDllFilePanel()
        {
            InitializeComponent();
        }
        #endregion
        #region 自定义方法
        /// <summary>
        /// 添加dll文件和显示
        /// </summary>
        /// <param name="pro">项目参数</param>
        public void DirDllFile(ProjectClass pro)
        {
            projectDll = pro;
            DllListBox.ItemsSource = projectDll.MyDllFiles;        
        }
        #endregion
        #region 控件事件
        /// <summary>
        /// 删除按钮的点击事件
        /// </summary>
        /// <param name="sender">信息发送对象</param>
        /// <param name="e">事件信息</param>
        private void DelDll_Click(object sender, RoutedEventArgs e)
        {
            if(DllListBox.SelectedIndex != -1 && projectDll != null)
            {
                DllFile deldllfile = (DllFile)DllListBox.SelectedItem;
                if (File.Exists(deldllfile.Path))
                {
                    File.Delete(deldllfile.Path);                    
                }
                else
                {
                    System.Windows.MessageBox.Show("文件已经被外部操作删除!");
                }
                ///删除选中项
                projectDll.DeletDllFilePath(deldllfile);
            }
        }
        /// <summary>
        /// 添加dll文件点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddDll_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Dll文件 | *.dll;";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                string goalPath = projectDll.OutPath + openFile.SafeFileName;
                if (!File.Exists(goalPath))
                {
                    File.Copy(openFile.FileName, goalPath);
                    DllFile newDll = new DllFile(openFile.SafeFileName, goalPath);
                    if (projectDll != null)
                    {
                        projectDll.AddDllFilePath(newDll);
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("该Dll文件已经存在请先删除");
                }
            }
        }
        #endregion       
    }
}
