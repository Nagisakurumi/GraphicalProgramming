using MyCodeBox;
using MyXAribute;
using MyXAributeDataItem;
using MyXCodeDataOption;
using MyXObject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace MyEditor
{
    /// <summary>
    /// AddCodeBoxXml.xaml 的交互逻辑
    /// </summary>
    public partial class AddCodeBoxXml : Window
    {

        #region 属性
        /// <summary>
        /// 数据ID
        /// </summary>
        private int num = 0;
        /// <summary>
        /// 属性的数据源
        /// </summary>
        private ObservableCollection<XAributeDataItem> _xAributeData = new ObservableCollection<XAributeDataItem>();
        /// <summary>
        /// 当前操作项
        /// </summary>
        private XAributeDataItem _currentItem;
        #endregion
        #region 读取器
        /// <summary>
        /// 属性的数据源
        /// </summary>
        public ObservableCollection<XAributeDataItem> XAributeData
        {
            get
            {
                return _xAributeData;
            }

            set
            {
                _xAributeData = value;
            }
        }
        /// <summary>
        /// 当前选中项
        /// </summary>
        public XAributeDataItem CurrentItem
        {
            get
            {
                return _currentItem;
            }

            set
            {
                _currentItem = value;
            }
        }
        #endregion
        #region 初始化函数
        public AddCodeBoxXml()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 窗体加载函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ///初始化为不可用
            //LastExText.IsEnabled = false;
            //LinkType.IsEnabled = false;

            ///初始化数据
            InitPointTypeData();
            InitListTypeData();
            InitPositionTypeData();
            InitLinkTypeData();
            InitXAributeData();
            InitFileTypeData();
            InitCodeBoxType();
        }
        #endregion
        #region 数据初始化函数
        /// <summary>
        /// 初始化PointType数据
        /// </summary>
        public void InitPointTypeData()
        {
            Dictionary<XAribute.XAttributeType, string> PointTypeData = new Dictionary<XAribute.XAttributeType, string>();
            ///添加数据源
            //PointTypeData.Add(XAribute.XAttributeType.XInt, "Int");
            //PointTypeData.Add(XAribute.XAttributeType.XFloat, "Float");
            //PointTypeData.Add(XAribute.XAttributeType.XDouble, "Double");
            //PointTypeData.Add(XAribute.XAttributeType.XBool, "Bool");
            //PointTypeData.Add(XAribute.XAttributeType.XChar, "Char");
            //PointTypeData.Add(XAribute.XAttributeType.XString, "String");
            //PointTypeData.Add(XAribute.XAttributeType.XByte, "Byte");
            //PointTypeData.Add(XAribute.XAttributeType.XVector2, "Vector2");
            //PointTypeData.Add(XAribute.XAttributeType.XVector3, "Vector3");
            //PointTypeData.Add(XAribute.XAttributeType.XVector4, "Vector4");
            //PointTypeData.Add(XAribute.XAttributeType.XQuaternion, "Quaternion");
            //PointTypeData.Add(XAribute.XAttributeType.XTransform, "Transform");
            //PointTypeData.Add(XAribute.XAttributeType.XDateTime, "DateTime");
            //PointTypeData.Add(XAribute.XAttributeType.XClass, "Class");
            //PointTypeData.Add(XAribute.XAttributeType.XEnter, "Enter");
            //PointTypeData.Add(XAribute.XAttributeType.XExc, "Exc");
            //PointTypeData.Add(XAribute.XAttributeType.XEnum, "Enum");
            foreach(XAribute.XAttributeType type in Enum.GetValues(typeof(XAribute.XAttributeType)))
            {
                PointTypeData.Add(type, type.ToString());
            }

            ///绑定数据
            PointType.ItemsSource = PointTypeData;
            PointType.SelectedValuePath = "Key";
            PointType.DisplayMemberPath = "Value";
        }
        /// <summary>
        /// 初始化ListType数据
        /// </summary>
        public void InitListTypeData()
        {
            Dictionary<XAribute.XAttributeSpec, string> ListTypeData = new Dictionary<XAribute.XAttributeSpec, string>();
            ///添加数据
            ListTypeData.Add(XAribute.XAttributeSpec.XNone, "一般变量");
            ListTypeData.Add(XAribute.XAttributeSpec.XArray, "Array数组");
            ListTypeData.Add(XAribute.XAttributeSpec.XList, "List集合");

            ListType.ItemsSource = ListTypeData;
            ListType.SelectedValuePath = "Key";
            ListType.DisplayMemberPath = "Value";
        }
        /// <summary>
        /// 初始化PositionType数据
        /// </summary>
        public void InitPositionTypeData()
        {
            Dictionary<XAribute.XPositonStyle, string> PositionTypeData = new Dictionary<XAribute.XPositonStyle, string>();
            ///添加数据
            PositionTypeData.Add(XAribute.XPositonStyle.Left, "输入数据");
            PositionTypeData.Add(XAribute.XPositonStyle.right, "输出数据");

            PositionType.ItemsSource = PositionTypeData;
            PositionType.SelectedValuePath = "Key";
            PositionType.DisplayMemberPath = "Value";
        }
        /// <summary>
        /// 初始化LinkType数据
        /// </summary>
        public void InitLinkTypeData()
        {
            Dictionary<XAribute.CanLinkType, string> LinkTypeData = new Dictionary<XAribute.CanLinkType, string>();
            ///添加数据
            LinkTypeData.Add(XAribute.CanLinkType.One, "单链接");
            LinkTypeData.Add(XAribute.CanLinkType.More, "多连接");

            ///数据绑定
            LinkType.ItemsSource = LinkTypeData;
            LinkType.SelectedValuePath = "Key";
            LinkType.DisplayMemberPath = "Value";
        }
        /// <summary>
        /// 初始化属性
        /// </summary>
        public void InitXAributeData()
        {
            Xaribute.ItemsSource = XAributeData;
            //Xaribute.SelectedValue = XAributeDataItem;
        }
        /// <summary>
        /// 初始化FileType数据
        /// </summary>
        public void InitFileTypeData()
        {
            Dictionary<int, string> FileTypeData = new Dictionary<int, string>();
            FileTypeData.Add(0, "XSystemFile");
            FileTypeData.Add(1, "XUserFile");
            FileTypeData.Add(2, "CLanguageFile");

            FileType.ItemsSource = FileTypeData;
            FileType.SelectedValuePath = "Key";
            FileType.DisplayMemberPath = "Value";

            FileType.SelectedIndex = 0;
        }
        /// <summary>
        /// 初始化代码块类型的数据
        /// </summary>
        public void InitCodeBoxType()
        {
            Dictionary<CodeBox.XAType, string> codeBoxTypeData = new Dictionary<CodeBox.XAType, string>();
            ///添加数据
            foreach(CodeBox.XAType item in Enum.GetValues(typeof(CodeBox.XAType)))
            {
                codeBoxTypeData.Add(item, item.ToString());
            }
            ///绑定数据
            CodeBoxType.ItemsSource = codeBoxTypeData;
            CodeBoxType.SelectedValuePath = "Key";
            CodeBoxType.DisplayMemberPath = "Value";
        }
        #endregion     
        #region 界面控件事件
        /// <summary>
        /// 添加属性的按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            XAributeData.Add(new XAributeDataItem(System.Windows.Forms.Application.StartupPath + "//Icon//int.jpg", "newXaribute" + GetNumID()));

        }
        /// <summary>
        ///XaributeListbox 选项改变事件 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Xaribute_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            XAributeDataItem p = (XAributeDataItem)Xaribute.SelectedItem;
            ///将选中的值给CurrentItem
            CurrentItem = p;
            BindingDetail();
        }
        /// <summary>
        /// 选择项被选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Button item = (Button)sender;
            //string name = item.Name;
            //for(int i = 0;i< Xaribute.;i++)
            //{
            //    if(name == Xaribute.Items[i])
            //}
        }
        /// <summary>
        /// 文本改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParamterType_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if(CurrentItem != null)
            {
                CurrentItem.Name = tb.Text;
            }
        }
        /// <summary>
        /// 选项改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PointType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if(CurrentItem != null)
            {
                //object o = cb.SelectedItem.GetType();             
                CurrentItem.PointTypeitem = (KeyValuePair< XAribute.XAttributeType,string>)cb.SelectedItem;
                if(CurrentItem.PointTypeitem.Key == XAribute.XAttributeType.XClass)
                {
                    LastExText.IsEnabled = true;
                }
            }
        }
        /// <summary>
        /// 选项改变时候
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if (CurrentItem != null)
            {
                CurrentItem.ListTypeitem = (KeyValuePair<XAribute.XAttributeSpec,string>)cb.SelectedItem;
            }
        }
        /// <summary>
        /// 选项改变时候
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PositionType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if (CurrentItem != null)
            {
                CurrentItem.PositionTypeitem = (KeyValuePair<XAribute.XPositonStyle,string>)cb.SelectedItem;
            }
        }
        /// <summary>
        /// 选项改变时候
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LinkType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if (CurrentItem != null)
            {
                CurrentItem.LinkTypeitem = (KeyValuePair<XAribute.CanLinkType,string>)cb.SelectedItem;
            }
        }
        /// <summary>
        /// 文本信息改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TipText_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (CurrentItem != null)
            {
                CurrentItem.TipTypeitem = tb.Text;
            }
        }
        /// <summary>
        /// 文本信息改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LastExText_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (CurrentItem != null)
            {
                CurrentItem.LastExTexteitem = tb.Text;
            }
        }
        /// <summary>
        /// 删除按钮按下的时候
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            XAributeData.Remove((XAributeDataItem)Xaribute.SelectedItem);
        }
        /// <summary>
        /// 退出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 确认按钮按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            XCodeDataOption xdo = new XCodeDataOption();
            List<XAributeDataItem> writeData = new List<XAributeDataItem>();
            ///数据转换
            for (int i = 0; i < XAributeData.Count; i++)
            {
                writeData.Add(XAributeData[i]);
            }
            bool issure = true;
            ///写入数据
            if (FileType.SelectedIndex == 0 || FileType.SelectedIndex == 2)
            {
                try
                {
                    XCodeDataOptionDataStructClass dataclass = new XCodeDataOptionDataStructClass();
                    dataclass.nodeList = writeData;
                    dataclass.CodeboxName = CodeBoxName.Text;
                    dataclass.CodeBoxHitText = HitText.Text;
                    dataclass.CodeBoxType = (((KeyValuePair<CodeBox.XAType, string>)CodeBoxType.SelectedItem).Key).ToString();
                    ///获取SystemCodeString的内容
                    TextRange textRange = new TextRange(SystemCodeString.Document.ContentStart, SystemCodeString.Document.ContentEnd);
                    dataclass.CodeBoxSystemCodeString = DelLinebreakString(textRange.Text);
                    ///获取ReturnValue的内容
                    TextRange ReturnValuetextRange = new TextRange(ReturnValue.Document.ContentStart, ReturnValue.Document.ContentEnd);
                    dataclass.ReturnValue = DelLinebreakString(ReturnValuetextRange.Text);
                    ///将代码块信息写入文件
                    if (FileType.SelectedIndex == 0)
                        issure = xdo.WriteXSystemFile(dataclass);
                    else
                        issure = xdo.WriteXCLanguageSystemFile(dataclass);
                }catch(Exception ex)
                {
                    LoggerHelp.WriteLogger(ex.ToString());
                    MessageBox.Show("数据错误请检查填写错误!");
                }
            }
            else if (FileType.SelectedIndex == 1)
            {
                issure = xdo.WriteUserFile(writeData, CodeBoxName.Text, HitText.Text);
            }
            else if(FileType.SelectedIndex == 2)
            {

            }
            if(!issure)
            {
                MessageBox.Show("该代码块和已存在的代码块重名请修改名称或者先删除原先的代码块");
            }
            else
            {
                MessageBox.Show("添加成功");
            }
            ///关闭当前窗口
            //this.Close();
        }
        #endregion
        #region 自定义函数
        /// <summary>
        /// 获取ID
        /// </summary>
        /// <returns></returns>
        public string GetNumID()
        {
            return (num++).ToString();
        }
        /// <summary>
        /// 绑定细节
        /// </summary>
        public void BindingDetail()
        {
            #region 数据绑定版本一
            /////绑定数据源
            //BindingOperations.SetBinding(ParamterType, TextBox.TextProperty, new Binding
            //{
            //    Source = CurrentItem,
            //    Path = new PropertyPath("Name"),
            //    Mode = BindingMode.TwoWay// 绑定模式  
            //    ,
            //    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            //});
            //BindingOperations.SetBinding(PointType, ComboBox.SelectedItemProperty, new Binding
            //{
            //    Source = CurrentItem,
            //    Path = new PropertyPath("PointTypeitem"),
            //    Mode = BindingMode.TwoWay// 绑定模式  
            //    ,
            //    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            //});
            //BindingOperations.SetBinding(ListType, ComboBox.SelectedItemProperty, new Binding
            //{
            //    Source = CurrentItem,
            //    Path = new PropertyPath("PointType"),
            //    Mode = BindingMode.TwoWay// 绑定模式  
            //    ,
            //    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            //});
            //BindingOperations.SetBinding(PositionType, ComboBox.SelectedItemProperty, new Binding
            //{
            //    Source = CurrentItem,
            //    Path = new PropertyPath("PositionType"),
            //    Mode = BindingMode.TwoWay// 绑定模式  
            //    ,
            //    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            //});
            //BindingOperations.SetBinding(LinkType, ComboBox.SelectedItemProperty, new Binding
            //{
            //    Source = CurrentItem,
            //    Path = new PropertyPath("LinkType"),
            //    Mode = BindingMode.TwoWay// 绑定模式  
            //    ,
            //    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            //});

            //BindingOperations.SetBinding(TipText, TextBox.TextProperty, new Binding
            //{
            //    Source = CurrentItem,
            //    Path = new PropertyPath("TipText"),
            //    Mode = BindingMode.TwoWay// 绑定模式  
            //    ,
            //    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            //});

            //BindingOperations.SetBinding(LastExText, TextBox.TextProperty, new Binding
            //{
            //    Source = CurrentItem,
            //    Path = new PropertyPath("LastExText"),
            //    Mode = BindingMode.TwoWay// 绑定模式  
            //    ,
            //    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            //});
            #endregion
            #region 数据绑定版本二
            if (CurrentItem == null)
                return;
            ParamterType.Text = CurrentItem.Name;
            PointType.SelectedItem = CurrentItem.PointTypeitem;
            ListType.SelectedItem = CurrentItem.ListTypeitem;
            //CodeBoxType.SelectedItem = CurrentItem.CodeBoxType;
            PositionType.SelectedItem = CurrentItem.PositionTypeitem;
            LinkType.SelectedItem = CurrentItem.LinkTypeitem;
            TipText.Text = CurrentItem.TipTypeitem;
            LastExText.Text = CurrentItem.LastExTexteitem;
            #endregion
            if (CurrentItem.PointTypeitem.Key == XAribute.XAttributeType.XClass)
            {
                LastExText.IsEnabled = true;
            }
            else
            {
                LastExText.IsEnabled = false;
            }
        }
        /// <summary>
        /// 去掉换行符
        /// </summary>
        /// <param name="oldstring">要去的字符串</param>
        /// <returns></returns>
        protected string DelLinebreakString(string oldstring)
        {
            //if (oldstring.Length > 4)
            //{
            //    string restring = oldstring.Substring(0, oldstring.Length - 4);
            //    return restring;
            //}
            //else
            //{
            //    return "";
            //}
            oldstring = oldstring.Replace("\r\n", "");
            return oldstring;
        }
        #endregion

        
    }
}
