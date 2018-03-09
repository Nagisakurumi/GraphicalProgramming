using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Wpf.AvalonDock.Layout;

namespace MyXMainLayout
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class XMainLayout : UserControl
    {
        public XMainLayout()
        {
            InitializeComponent();

            AddLayoutAnchorablePane(LeftAnchorableGroup, "using指令");
            AddLayoutAnchorablePane(LeftAnchorableGroup, "方法");
            AddLayoutAnchorablePane(LeftAnchorableGroup, "字段");

            AddLayoutAnchorablePane(RightAnchorableGroup, "解决方案");
            AddLayoutAnchorablePane(RightAnchorableGroup, "属性");
        }

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
        public void AddLayoutAnchorablePane(LayoutAnchorablePaneGroup FatherLayout, string Title = "属性")
        {
            try
            {
                LayoutAnchorablePane pane = new LayoutAnchorablePane();
                LayoutAnchorable anchorable = new LayoutAnchorable();
                anchorable.Title = Title;
                pane.Children.Add(anchorable);
                FatherLayout.Children.Add(pane);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "[MainWindow][miAnchorVerticalPane_Click_1]");
            }
        }
        #endregion
    }
}
