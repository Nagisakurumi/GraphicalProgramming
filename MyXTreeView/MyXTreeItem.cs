using MyCodeBox;
using MyXAribute;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace MyXTreeView
{
    public class MyXTreeItem : INotifyPropertyChanged
    {
        #region 内置属性
        /// <summary>
        /// 显示的名字
        /// </summary>
        private string _name;
        /// <summary>
        /// 是否被选中
        /// </summary>
        private bool _isSelected = false;
        /// <summary>
        /// 是否展开
        /// </summary>
        private bool _isExpanded = false;
        /// <summary>
        /// 是否启用
        /// </summary>
        private bool _isEnabled = true;
        /// <summary>
        /// 是否显示
        /// </summary>
        private Visibility _isVisiblity = Visibility.Visible;
        #endregion
        #region 读取器
        /// <summary>
        /// 子项
        /// </summary>
        public List<MyXTreeItem> ChildrenItem { get; set; }
        /// <summary>
        /// 系统编译代码
        /// </summary>
        public string SystemCodeString { get; set; }
        /// <summary>
        /// 代码块的输出值
        /// </summary>
        public string ReturnValue { set; get; }
        /// <summary>
        /// 显示用的名字
        /// </summary>
        public string XName
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("XName"); }
        }
        /// <summary>
        /// 显示类型的图片
        /// </summary>
        public string TypeImagePath { get; set; }
        /// <summary>
        /// 是否展开
        /// </summary>
        public bool IsExpanded
        {
            get
            {
                return _isExpanded;
            }
            set
            {
                _isExpanded = value;
                OnPropertyChanged("IsExpanded");
            }
        }
        /// <summary>
        /// 代码块中所存在的属性
        /// </summary>
        public List<XAributeItem> MyXaributeChildren { get; set; }
        /// <summary>
        /// 提示信息
        /// </summary>
        public string MyHitText { get; set; }
        /// <summary>
        /// 代码块的类型
        /// </summary>
        public CodeBox.XAType MyCodeBoxType { get; set; }
        /// <summary>
        /// 是否被选中
        /// </summary>
        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                _isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }

            set
            {
                _isEnabled = value;
                OnPropertyChanged("IsEnabled");
            }
        }
        /// <summary>
        /// 是否显示
        /// </summary>
        public Visibility IsVisiblity
        {
            get
            {
                return _isVisiblity;
            }

            set
            {
                _isVisiblity = value;
                OnPropertyChanged("IsVisiblity");
            }
        }
        /// <summary>
        /// 是否是数据节点
        /// </summary>
        public bool IsDataNode
        {
            get
            {
                if(ChildrenItem.Count == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        #endregion
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public MyXTreeItem()
        {
            ChildrenItem = new List<MyXTreeItem>();
            MyXaributeChildren = new List<XAributeItem>();
        }
        /// <summary>
        /// 拷贝构造函数
        /// 不拷贝Children
        /// </summary>
        /// <param name="item">要拷贝的对象</param>
        public MyXTreeItem(MyXTreeItem item)
        {
            this.XName = item.XName;
            ///重新申请一个新的空间
            this.ChildrenItem = new List<MyXTreeItem>();
            this.TypeImagePath = item.TypeImagePath;
            this.IsExpanded = item.IsExpanded;
            this.IsSelected = item.IsSelected;
            this.MyCodeBoxType = item.MyCodeBoxType;
            this.MyHitText = item.MyHitText;
            this.MyXaributeChildren = item.MyXaributeChildren;
            this.ReturnValue = item.ReturnValue;
            this.SystemCodeString = item.SystemCodeString;
            this.IsEnabled = item.IsEnabled;
            this.IsVisiblity = item.IsVisiblity;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ChildrenItem">子项集合</param>
        /// <param name="XName">显示用的名字</param>
        /// <param name="TypeImagePath">显示类型的图片</param>
        /// <param name="MyXaributeChildren">代码块中所存在的属性集合</param>
        /// <param name="MyHitText">MyHitText</param>
        public MyXTreeItem(List<MyXTreeItem> ChildrenItem, string XName,
            string TypeImagePath, List<XAributeItem> MyXaributeChildren, string MyHitText, CodeBox.XAType MyCodeBoxType
            , string SystemCodeString, string ReturnValue)
        {
            this.ChildrenItem = ChildrenItem;
            this.XName = XName;
            this.TypeImagePath = TypeImagePath;
            this.MyXaributeChildren = MyXaributeChildren;
            this.IsExpanded = false;
            this.MyHitText = MyHitText;
            this.MyCodeBoxType = MyCodeBoxType;
            this.SystemCodeString = SystemCodeString;
            this.ReturnValue = ReturnValue;
        }
        #endregion
        #region 数据改变通知
        /// <summary>
        /// 具体通知事件的实现
        /// </summary>
        /// <param name="propertyName">数据改变的属性名字</param>
        protected internal virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        /// <summary>
        /// 数据改变通知事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged; 
        #endregion
    }
    /// <summary>
    /// 代码块中的属性
    /// </summary>
    public class XAributeItem
    {
        /// <summary>
        /// 属性名称
        /// </summary>
        public string Parameter_name { get; set; }
        /// <summary>
        /// 允许连接的数量
        /// </summary>
        public XAribute.CanLinkType MyCanLinkType { get; set; }
        /// <summary>
        /// 数据集合类型
        /// </summary>
        public XAribute.XAttributeSpec MyXAttributeSpec { get; set; }
        /// <summary>
        /// 数据类型（节点类型）
        /// </summary>
        public XAribute.XAttributeType MyXAttributeType { get; set; }
        /// <summary>
        /// 数据在左边还是右边位置属性
        /// </summary>
        public XAribute.XPositonStyle MyXPositonStyle { get; set; }
        /// <summary>
        /// 提示信息
        /// </summary>
        public string MyHittext { get; set; }
        /// <summary>
        /// 类名
        /// </summary>
        public string MyLastExText { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        public XAributeItem()
        {

        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Parameter_name"></param>
        /// <param name="MyCanLinkType"></param>
        /// <param name="MyXAttributeSpec"></param>
        /// <param name="MyXAttributeType"></param>
        /// <param name="MyXPositonStyle"></param>
        /// <param name="MyHittext"></param>
        public XAributeItem(string Parameter_name, XAribute.CanLinkType MyCanLinkType, XAribute.XAttributeSpec MyXAttributeSpec,
            XAribute.XAttributeType MyXAttributeType, XAribute.XPositonStyle MyXPositonStyle, string MyHittext,string MyLastExText)
        {
            this.Parameter_name = Parameter_name;
            this.MyCanLinkType = MyCanLinkType;
            this.MyXAttributeSpec = MyXAttributeSpec;
            this.MyXAttributeType = MyXAttributeType;
            this.MyXPositonStyle = MyXPositonStyle;
            this.MyHittext = MyHittext;
            this.MyLastExText = MyLastExText;
        }
        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param name="item">要拷贝的对象</param>
        public XAributeItem(XAributeItem item)
        {
            this.Parameter_name = item.Parameter_name;
            this.MyCanLinkType = item.MyCanLinkType;
            this.MyXAttributeSpec = item.MyXAttributeSpec;
            this.MyXAttributeType = item.MyXAttributeType;
            this.MyXPositonStyle = item.MyXPositonStyle;
            this.MyHittext = item.MyHittext;
            this.MyLastExText = item.MyLastExText;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="bute">项</param>
        public XAributeItem(XAribute bute)
        {
            this.Parameter_name = bute.Title;
            this.MyCanLinkType = bute.CanLinkNum;
            this.MyXAttributeSpec = bute.SelectSpc;
            this.MyXAttributeType = bute.SelectType;
            this.MyXPositonStyle = bute.SelectPositionStyle;
            this.MyHittext = bute.Hint;
            this.MyLastExText = bute.ExName;
        }
    }
}
