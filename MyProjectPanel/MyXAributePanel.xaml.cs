using MyPicTabPage;
using MyProjectData;
using MyXAribute;
using MyXObject;
using System.Windows;
using System.Windows.Controls;

namespace MyProjectPanel
{
    /// <summary>
    /// MyXAributePanel.xaml 的交互逻辑
    /// </summary>
    public partial class MyXAributePanel : MyPanel
    {
        #region 构造函数
        public MyXAributePanel()
        {
            InitializeComponent();
        } 
        #endregion
        #region 属性
        /// <summary>
        /// 代码面板
        /// </summary>
        private PicTabPage picPage;
        #endregion
        #region 自定义方法
        /// <summary>
        /// 添加dll文件和显示
        /// </summary>
        /// <param name="pro">项目参数</param>
        public void DirXAribute(PicTabPage picPage)
        {
            this.picPage = picPage;
            XAributeListBox.ItemsSource = this.picPage.ListXAributes;
        }
        /// <summary>
        /// 根据属性名字获取属性对象
        /// </summary>
        /// <param name="name">属性名称</param>
        /// <returns></returns>
        protected XAribute GetXAribute(string name)
        {
            return picPage.GetClassXAribute(name);
        }
        #endregion
        #region 控件事件
        /// <summary>
        /// 删除按钮的点击事件
        /// </summary>
        /// <param name="sender">信息发送对象</param>
        /// <param name="e">事件信息</param>
        private void DelXAribute_Click(object sender, RoutedEventArgs e)
        {
            if (XAributeListBox.SelectedIndex != -1)
            {
                picPage.DelXAribute((XAribute)XAributeListBox.SelectedItem);
            }
        }
        /// <summary>
        /// 添加dll文件点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddXAribute_Click(object sender, RoutedEventArgs e)
        {
            string newFunctionName = "NewXAributeName";
            while (!picPage.CreatePicXAribute(newFunctionName))
            {
                newFunctionName += "_0";
            }
        }
        /// <summary>
        /// 鼠标左击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StackPanel_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            StackPanel panel = (StackPanel)sender;        
            TextBox tex = (TextBox)panel.Children[1];
            ///检测是否应该发送数据
            if (XAributeListBox.SelectedIndex != -1)
            {
                XObjectData data = new XObjectData(GetXAribute(tex.Text));
                ToCallFunctionParentControl(MouseState.XSelectObjectToDir, data);
            }
        }
        #endregion
        /// <summary>
        /// 事件派送前的鼠标左击事件
        /// </summary>
        /// <param name="sender">信息发送员</param>
        /// <param name="e">消息</param>
        private void XAributeListBox_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if(XAributeListBox.SelectedIndex != -1)
            {
                XAribute item = (sender as ListBox).SelectedItem as XAribute;
                if(item != null)
                {
                    ///打包数据
                    DataObject data = new DataObject(item);
                    DragDrop.DoDragDrop(item, data, DragDropEffects.Copy);
                }
            }
        }
    }
}
