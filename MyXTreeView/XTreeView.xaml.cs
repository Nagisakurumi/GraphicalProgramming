using System.Windows;
using System.Windows.Controls;
using MyXObject;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using System.Collections.Generic;

namespace MyXTreeView
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class XMTreeView : UserControl
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="MCallFc">回调函数</param>
        public XMTreeView(MouseCallFunction MyMCallFc)
        {
            InitializeComponent();
            this.MyMCallFc = MyMCallFc;
            this.XTreeView.ItemsSource = this._myData;
        }
        #endregion
        #region 内部属性
        /// <summary>
        /// 点击时间
        /// </summary>
        private DateTime _clickTime = new DateTime();
        /// <summary>
        /// 真实与树绑定的数据源
        /// </summary>
        private ObservableCollection<MyXTreeItem> _myData = new ObservableCollection<MyXTreeItem>();
        /// <summary>
        /// 存放所有数据用于查询的优化
        /// </summary>
        private List<MyXTreeItem> _myDataList = new List<MyXTreeItem>();
        #endregion
        #region 属性读取器
        /// <summary>
        /// 树状数据
        /// </summary>
        public ObservableCollection<MyXTreeItem> MyData
        {
            get { return _myData; }
            set { 
                _myData = value;
                this.XTreeView.ItemsSource = this._myData;
                UpdateCheckData();
            }
        }
        /// <summary>
        /// 回调函数
        /// </summary>
        public MouseCallFunction MyMCallFc { get; set; }
        /// <summary>
        /// 树形
        /// </summary>
        public TreeView MyTreeView { get { return XTreeView; } }
        #endregion
        #region 自定义函数
        /// <summary>
        /// 创建代码块
        /// </summary>
        protected void CreateCodeBox()
        {
            ///如果不是最终节点则退出
            if (((MyXTreeItem)XTreeView.SelectedItem).MyXaributeChildren.Count == 0)
            {
                return;
            }
            XObjectData XData = new XObjectData((MyXTreeItem)XTreeView.SelectedItem);
            this.MyMCallFc(this, MouseState.XCreateCodeBox, XData);
        }
        #region 新方法
        /// <summary>
        /// 更新用于查询的数据
        /// </summary>
        protected void UpdateCheckData()
        {
            _myDataList.Clear();
            foreach(MyXTreeItem item in MyData)
            {
                ToAddListData(item);
            }
        }
        /// <summary>
        /// 递归添加项
        /// </summary>
        /// <param name="item">检查的项</param>
        private void ToAddListData(MyXTreeItem item)
        {
            _myDataList.Add(item);
            ///递归退出条件
            if (item.ChildrenItem.Count == 0)
            {                
                return;
            }
            foreach (MyXTreeItem child in item.ChildrenItem)
            {
                ToAddListData(child);
            }
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="key">要查询的关键词</param>
        protected void SearchDataNode(string key)
        {
            foreach (MyXTreeItem item in _myDataList)
            {
                item.IsExpanded = true;
                if (item.IsDataNode && item.XName.IndexOf(key, StringComparison.CurrentCultureIgnoreCase) == -1)
                {
                    item.IsVisiblity = Visibility.Collapsed;
                }
                else
                {
                    item.IsVisiblity = Visibility.Visible;
                }           
            }
        }
        /// <summary>
        /// 当没有查询的时候变回所有的数据
        /// </summary>
        protected void ReturnAllData()
        {
            foreach (MyXTreeItem item in _myDataList)
            {
                item.IsExpanded = false;
                item.IsVisiblity = Visibility.Visible;
            }
        }
        #endregion
        #endregion
        #region 事件绑定
        /// <summary>
        /// 当搜索框的内容更新的时候修改树的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if(tb.Text == null || tb.Text == "")
            {
                ReturnAllData();
            }
            else
            {
                SearchDataNode(tb.Text);
            }
        }
        /// <summary>
        /// 选项改变的时候
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

        }
        /// <summary>
        /// 键盘抬起的时候
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XTreeView_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                CreateCodeBox();
            }
        }
        /// <summary>
        /// 左击按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XTreeView_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ///获取当前时间
            DateTime nowTime = DateTime.Now;
            TimeSpan times = nowTime - _clickTime;
            if(times.TotalMilliseconds <= 200)
            {
                CreateCodeBox();
            }
            ///更新点击时间
            _clickTime = DateTime.Now;
        }
        #endregion
    }
}
