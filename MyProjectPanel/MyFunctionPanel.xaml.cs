using MyPicTabPage;
using MyXObject;
using System.Windows;
using System.Windows.Controls;

namespace MyProjectPanel
{
    /// <summary>
    /// MyFunctionPanel.xaml 的交互逻辑
    /// </summary>
    public partial class MyFunctionPanel : MyPanel
    {
        #region 构造函数
        public MyFunctionPanel()
        {
            InitializeComponent();
        }
        #endregion
        #region 属性
        /// <summary>
        /// 代码面板
        /// </summary>
        private PicTabPage picPage;
        /// <summary>
        /// 点击的函数名
        /// </summary>
        private string ClickTitle = "";
        #endregion
        #region 自定义方法
        /// <summary>
        /// 添加函数显示
        /// </summary>
        /// <param name="pro">项目参数</param>
        public void DirFunction(PicTabPage picPage)
        {
            this.picPage = picPage;
            FunctionListBox.ItemsSource = this.picPage.ListFunction;
        }
        #endregion
        #region 控件事件
        /// <summary>
        /// 删除按钮的点击事件
        /// </summary>
        /// <param name="sender">信息发送对象</param>
        /// <param name="e">事件信息</param>
        private void DelFunction_Click(object sender, RoutedEventArgs e)
        {
            if(FunctionListBox.SelectedIndex != -1)
            {
                PicFunctionTabPage fun = FunctionListBox.SelectedItem as PicFunctionTabPage;
                if(fun != null)
                    picPage.DelPicFunctionPage(fun);
            }
        }
        /// <summary>
        /// 添加事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddFunction_Click(object sender, RoutedEventArgs e)
        {
            string newFunctionName = "NewFunctionName";
            while(!picPage.CreatePicFunctionPage(newFunctionName))
            {
                newFunctionName += "_0";
            }
        }
        /// <summary>
        /// 点击选中事件
        /// </summary>
        /// <param name="sender">信息源</param>
        /// <param name="e">信息</param>
        private void StackPanel_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Panel panel = sender as StackPanel;
            if (panel == null)
                return;
            TextBlock box = panel.Children[1] as TextBlock;
            if (box == null)
                return;
            if(FunctionListBox.SelectedIndex != -1)
            {
                PicFunctionTabPage fun = FunctionListBox.SelectedItem as PicFunctionTabPage;
                if (fun != null && box.Text == fun.Title)
                {
                    ///如果算是第二次点击
                    if(fun.Title == ClickTitle)
                    {
                        ///主页面打开函数页面去编辑
                        XObjectData data = new XObjectData(fun);
                        ToCallFunctionParentControl(MouseState.XOpenFunction, data);
                    }
                    else
                    {
                        ///设置第一次点击
                        ClickTitle = fun.Title;
                        XObjectData data = new XObjectData(fun);
                        ToCallFunctionParentControl(MouseState.XSelectObjectToDir, data);
                    }
                }
            }

        }
        #endregion      
    }
}
