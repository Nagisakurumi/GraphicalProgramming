using MyXObject;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MyXAribute
{
    /// <summary>
    /// ExName值改变事件
    /// </summary>
    /// <param name="sender">事件发送者</param>
    /// <param name="ChangeValue">值的改变</param>
    public delegate void ExNameTextChange(object sender, string OldValue, string NewValue);
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:MyXAribute"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:MyXAribute;assembly=MyXAribute"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误: 
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    public class XAribute : XObject
    {
        static XAribute()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(XAribute), new FrameworkPropertyMetadata(typeof(XAribute)));
        }
        #region 自定义属性
        /// <summary>
        /// 是否显示名称
        /// </summary>
        private bool _isDirText = true;
        /// <summary>
        /// 属性的图标
        /// </summary>
        private string _icon = "";
        /// <summary>
        /// 按钮位置样式枚举
        /// </summary>
        public enum XPositonStyle
        {
            /// <summary>
            /// 按钮在左边
            /// </summary>
            Left = 1,
            /// <summary>
            /// 按钮在右边
            /// </summary>
            right = 2,
        }
        /// <summary>
        /// 默认左边
        /// </summary>
        private XPositonStyle _selectPositionStyle = XPositonStyle.Left;
        /// <summary>
        /// 端口所代表的类型
        /// </summary>
        public enum XAttributeType
        {
            /// <summary>
            /// 基础整形
            /// </summary>
            XInt = 0,
            /// <summary>
            /// 基础浮点型
            /// </summary>
            XFloat = 1,
            /// <summary>
            /// 基础双精度浮点类型
            /// </summary>
            XDouble = 2,
            /// <summary>
            /// 基础布尔类型（真假类型）
            /// </summary>
            XBool = 3,
            /// <summary>
            /// 基础字符类型
            /// </summary>
            XChar = 4,
            /// <summary>
            /// 基础字符串类型
            /// </summary>
            XString = 5,
            /// <summary>
            /// 基础字节类型
            /// </summary>
            XByte = 6,
            /// <summary>
            /// 二维向量
            /// </summary>
            XVector2 = 40,
            /// <summary>
            /// 三维向量
            /// </summary>
            XVector3 = 41,
            /// <summary>
            /// 四维向量
            /// </summary>
            XVector4 = 42,
            /// <summary>
            /// 四元数
            /// </summary>
            XQuaternion = 43,
            /// <summary>
            /// 游戏物件位置信息
            /// </summary>
            XTransform = 44,
            /// <summary>
            /// 时间类型
            /// </summary>
            XDateTime = 45,
            /// <summary>
            /// 所有的类 类型
            /// </summary>
            XClass = 7,
            /// <summary>
            /// 入口
            /// </summary>
            XEnter = 8,
            /// <summary>
            /// 出口
            /// </summary>
            XExc = 9,
            /// <summary>
            /// 枚举类型
            /// </summary>
            XEnum = 10,
            /// <summary>
            /// 目标类型
            /// </summary>
            XTarget = 11,
            /// <summary>
            /// 跳出
            /// </summary>
            XBreak = 12
            ///// <summary>
            ///// 基础整形
            ///// </summary>
            //XIntArray = 8,
            ///// <summary>
            ///// 基础浮点型
            ///// </summary>
            //XFloatArray = 9,
            ///// <summary>
            ///// 基础双精度浮点类型
            ///// </summary>
            //XDoubleArray = 10,
            ///// <summary>
            ///// 基础布尔类型（真假类型）
            ///// </summary>
            //XBoolArray = 11,
            ///// <summary>
            ///// 基础字符类型
            ///// </summary>
            //XCharArray = 12,
            ///// <summary>
            ///// 基础字符串类型
            ///// </summary>
            //XStringArray = 13,
            ///// <summary>
            ///// 基础字节类型
            ///// </summary>
            //XByteArray = 14,
            ///// <summary>
            ///// 所有的类的数组
            ///// </summary>
            //XClassArray = 15,

            ///// <summary>
            ///// 基础整形
            ///// </summary>
            //XIntList = 16,
            ///// <summary>
            ///// 基础浮点型
            ///// </summary>
            //XFloatList = 17,
            ///// <summary>
            ///// 基础双精度浮点类型
            ///// </summary>
            //XDoubleList = 18,
            ///// <summary>
            ///// 基础布尔类型（真假类型）
            ///// </summary>
            //XBoolList = 19,
            ///// <summary>
            ///// 基础字符类型
            ///// </summary>
            //XCharList = 20,
            ///// <summary>
            ///// 基础字符串类型
            ///// </summary>
            //XStringList = 21,
            ///// <summary>
            ///// 基础字节类型
            ///// </summary>
            //XByteList = 22,
            ///// <summary>
            ///// 类型集合
            ///// </summary>
            //XClasslist = 23
        }
        /// <summary>
        /// 属性是否为特殊数组或集合
        /// </summary>
        public enum XAttributeSpec
        {
            /// <summary>
            /// 默认只是一般变量
            /// </summary>
            XNone = 0,
            /// <summary>
            /// 数组类型
            /// </summary>
            XArray = 1,
            /// <summary>
            /// 集合类型
            /// </summary>
            XList = 2
        }
        /// <summary>
        /// 改按钮所代表的类型默认为XInt
        /// </summary>
        private XAttributeType _selectType = XAttributeType.XInt;
        /// <summary>
        /// 决定当前变量数组类型
        /// </summary>
        private XAttributeSpec _selectSpc = XAttributeSpec.XNone;
        /// <summary>
        /// 类型扩展名
        /// </summary>
        private string LastExName = "";
        /// <summary>
        /// 上一级父控件
        /// </summary>
        private XObject _vectialParentControl;
        /// <summary>
        /// 框的中心点
        /// </summary>
        private Point centerPosition;
        /// <summary>
        /// 此接口的允许连接方式
        /// </summary>
        public enum CanLinkType
        {
            /// <summary>
            /// 只允许连接一个对象
            /// </summary>
            One = 1,
            /// <summary>
            /// 允许多对象
            /// </summary>
            More = 2
        }
        /// <summary>
        /// 允许的连接方式
        /// </summary>
        private CanLinkType _canLinkNum = CanLinkType.One;
        /// <summary>
        /// 贝塞尔曲线
        /// </summary>
        private List<BezierLine> MyBeziers = new List<BezierLine>();
        /// <summary>
        /// 用于枚举的下拉框
        /// </summary>
        private ComboBox _myEnumBox = new System.Windows.Controls.ComboBox();
        /// <summary>
        /// 枚举下拉框的数据源
        /// </summary>
        private Dictionary<int, string> _myEnumBoxData = new Dictionary<int, string>();
        /// <summary>
        /// 项目中可以调用的类
        /// </summary>
        private Dictionary<string, string> _projectUseClassType = new Dictionary<string, string>();
        /// <summary>
        /// 填装值的内容
        /// </summary>
        private TextBox ValueTextBox = new TextBox();
        /// <summary>
        /// 标题文本
        /// </summary>
        private TextBlock _titleTextBlock = new TextBlock();
        /// <summary>
        /// 矩形外面的框
        /// </summary>
        private Border _checkBorder = new Border();
        /// <summary>
        /// ExName改变事件
        /// </summary>
        private ExNameTextChange _exNameChange;
        /// <summary>
        /// 主内容面板
        /// </summary>
        private StackPanel _myPanel = new StackPanel();
        #endregion
        #region 读取器
        /// <summary>
        /// 按钮位置样式读取器
        /// </summary>
        public XPositonStyle SelectPositionStyle
        {
            get
            {
                return _selectPositionStyle;
            }
            set
            {
                _selectPositionStyle = value;
                OnPropertyChanged("SelectPositionStyle");
                this.InvalidateVisual();
            }
        }
        /// <summary>
        /// 变量数组类型读取器
        /// </summary>
        public XAttributeSpec SelectSpc
        {
            get
            {
                return _selectSpc;
            }
            set
            {
                _selectSpc = value;
                OnPropertyChanged("SelectSpc");
                this.InvalidateVisual();
            }
        }
        /// <summary>
        /// 改按钮所代表的类型默认为XInt
        /// </summary>
        public XAttributeType SelectType
        {
            get
            {
                return _selectType;
            }
            set
            {
                _selectType = value;
                OnPropertyChanged("SelectType");
                Switch_selectType();
                this.InvalidateVisual();
            }
        }
        /// <summary>
        /// 允许的连接数量
        /// </summary>
        public CanLinkType CanLinkNum
        {
            get
            {
                return _canLinkNum;
            }

            set
            {
                _canLinkNum = value;
                OnPropertyChanged("CanLinkNum");
            }
        }
        /// <summary>
        /// 枚举下拉框的数据源
        /// </summary>
        public Dictionary<int, string> MyEnumBoxData
        {
            get
            {
                return _myEnumBoxData;
            }

            set
            {
                _myEnumBoxData = value;
                OnPropertyChanged("MyEnumBoxData");
                AddEnumValue();
            }
        }
        /// <summary>
        /// 类扩展名
        /// </summary>
        public string ExName
        {
            get
            {
                return LastExName;
            }

            set
            {
                /////发送属性值改变事件
                //ToSenderPropertyValueChangeEvent("ExName", Title, value);  
                                            
                ///ExName值改变事件
                if(ExNameChange != null)
                {
                    ///ExName值改变事件
                    ExNameChange(this,LastExName, value);
                }
                ///改变属性的值
                LastExName = value;
                ///调整属性类型的对应颜色
                WhileExNameChange();
                ///调整连接的贝塞尔曲线
                AdjustBezierLine();
                OnPropertyChanged("ExName");
            }
        }
        /// <summary>
        /// 贝塞尔曲线
        /// </summary>
        public List<BezierLine> GetMyBeziers
        {
            get
            {
                return MyBeziers;
            }
        }
        /// <summary>
        /// 上一级父控件
        /// </summary>
        public XObject VectialParentControl
        {
            get
            {
                return _vectialParentControl;
            }

            set
            {
                _vectialParentControl = value;
            }
        }
        /// <summary>
        /// 获取一个值指示是否选用输入框中的值
        /// </summary>
        public bool HasValueTextBox { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon
        {
            get
            {
                return _icon;
            }

            set
            {
                _icon = value;
            }
        }
        /// <summary>
        /// 是否显示名称
        /// </summary>
        public bool IsDirText
        {
            get
            {
                return _isDirText;
            }

            set
            {
                _isDirText = value;
            }
        }
        /// <summary>
        /// 项目中可以调用的类
        /// </summary>
        public Dictionary<string, string> ProjectUseClassType
        {
            get
            {
                return _projectUseClassType;
            }

            set
            {
                _projectUseClassType = value;
            }
        }
        /// <summary>
        /// 表示没有颜色
        /// </summary>
        public Color NanColor
        {
            get
            {
                ///返回一个空颜色
                return Color.FromArgb(0, 0, 0, 0);
            }
        }
        /// <summary>
        /// 标题框
        /// </summary>
        public TextBlock TitleTextBlock
        {
            get
            {
                return _titleTextBlock;
            }
        }
        /// <summary>
        /// 外边框
        /// </summary>
        public Border CheckBorder
        {
            get
            {
                return _checkBorder;
            }
        }
        /// <summary>
        /// ExName改变事件
        /// </summary>
        public ExNameTextChange ExNameChange
        {
            get
            {
                return _exNameChange;
            }

            set
            {
                _exNameChange = value;
            }
        }
        /// <summary>
        /// 主内容面板
        /// </summary>
        public StackPanel MyPanel
        {
            get
            {
                return _myPanel;
            }
        }
        /// <summary>
        /// 重写子元素集合
        /// </summary>
        public new UIElementCollection Children
        {
            get
            {
                return MyPanel.Children;
            }
        }
        #endregion
        #region 构造函数
        /// <summary>
        /// 空构造函数
        /// </summary>
        public XAribute() : base(0,"默认名称")
        {
            InitBaseInfo();
        }
        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param name="id">id必须主动赋予</param>
        /// <param name="bute">拷贝对象</param>
        public XAribute(int id,  XAribute bute) : base(id, bute.Title)
        {
            this.SelectType = bute.SelectType;
            this.SelectSpc = bute.SelectSpc;
            this.SelectPositionStyle = bute.SelectPositionStyle;
            this.CallBackFunction = bute.CallBackFunction;
            this.CanLinkNum = bute.CanLinkNum;
            this.LastExName = bute.ExName;
            InitBaseInfo();
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">控件id</param>
        /// <param name="paramt">参数名字</param>
        /// <param name="xat">属性类型</param>
        /// <param name="xbs">属性是否为集合</param>
        /// <param name="xps">属性名称和按钮的位置</param>
        /// <param name="mcf">子控件事件回调</param>
        public XAribute(int id, string paramt, XAttributeType xat, XAttributeSpec xbs, XPositonStyle xps, MouseCallFunction mcf, CanLinkType clt, string LastExName) : base(id, paramt)
        {
            this.SelectType = xat;
            this.SelectSpc = xbs;
            this.SelectPositionStyle = xps;
            this.CallBackFunction = mcf;
            this.CanLinkNum = clt;
            this.LastExName = LastExName;
            InitBaseInfo();
        }
        /// <summary>
        /// 初始化基本信息
        /// </summary>
        protected override void InitBaseInfo()
        {
            #region 设置主内容面板
            ///绑定高度
            ToolHelp.SetBindingHeight(MyPanel, this);
            ///绑定宽度
            ToolHelp.SetBindingWidth(MyPanel, this);
            ///设置水平布局
            MyPanel.Orientation = Orientation.Horizontal;
            ///设置布局流向
            if(this.SelectPositionStyle == XPositonStyle.Left)
            {
                MyPanel.FlowDirection = FlowDirection.LeftToRight;
            }
            else
            {
                MyPanel.FlowDirection = FlowDirection.RightToLeft;
            }
            this.GetChildren().Add(MyPanel); 
            #endregion          
            base.InitBaseInfo();
            this.Height = 30;
            this.Width = Height * 4;
            #region 基本内容控件初始化
            ///设置默认没有被填充
            CheckBorder.Background = new SolidColorBrush(NanColor);
            ///赋值框的颜色
            CheckBorder.BorderBrush = new SolidColorBrush(BorderColor);
            ///设置边框的粗细
            CheckBorder.BorderThickness = new Thickness(BorderWidth);
            ///设置圆角
            CheckBorder.CornerRadius = new CornerRadius(Radius);
            ///把框的高度属性绑定到本控件的高度
            ToolHelp.SetBindingHeight(CheckBorder, this);
            ///将框的宽度绑定到框的高度上（使其成为宽=高）
            ToolHelp.SetBindingWidthToHeigth(CheckBorder, CheckBorder);
            ///设置标题
            TitleTextBlock.Text = Title;
            ///设置字体大小
            TitleTextBlock.FontSize = FontSize;
            ///设置字体
            TitleTextBlock.FontFamily = new FontFamily(MyFont);
            ///用于计算文本的宽度和高度
            FormattedText fonttext = new FormattedText(Title, CultureInfo.CurrentUICulture, TitleTextBlock.FlowDirection,
                new Typeface(FontFamily, TitleTextBlock.FontStyle, TitleTextBlock.FontWeight, TitleTextBlock.FontStretch), FontSize, TitleTextBlock.Foreground);
            ///设定死文本的宽度
            TitleTextBlock.Width = fonttext.Width + 20;
            ///设置文本溢出方式
            TitleTextBlock.TextTrimming = TextTrimming.CharacterEllipsis;
            ///设置标题改变事件
            TitleChange += (s, o, v) =>
            {
                ///修改文本的值
                TitleTextBlock.Text = v;
                ///用于计算文本的宽度和高度
                FormattedText changetext = new FormattedText(v, CultureInfo.CurrentUICulture, TitleTextBlock.FlowDirection,
                    new Typeface(FontFamily, TitleTextBlock.FontStyle, TitleTextBlock.FontWeight, TitleTextBlock.FontStretch), FontSize, TitleTextBlock.Foreground);
                TitleTextBlock.Width = changetext.Width;
                ///修改本身控件的尺寸
                AutoSize();
                ///通知修改父控件的尺寸
                ToCallBackParent(MouseState.XModifyLayout);
                ///修改贝塞尔曲线
                BezierPositionChange();
                return true;
            };
            ///将高度绑定到本控件的高度
            ToolHelp.SetBindingHeight(TitleTextBlock, this);
            ///绑定文本改变事件
            ValueTextBox.TextChanged += (s, e) => 
            {                
                AutoSize();
            };
            ///设置内容框的固定宽度
            ValueTextBox.Width = 80;
            ///设置高度
            ValueTextBox.Height = this.Height;
            ///设置属性框的背景
            ValueTextBox.Background = new SolidColorBrush(Color.FromArgb(20, BorderColor.R, BorderColor.G, BorderColor.B));
            ///设置当按下回车后取消输入焦点
            ValueTextBox.KeyDown += (s, e) => 
            {
                if (e.Key == Key.Enter)
                {
                    ValueTextBox.TabIndex = -1;
                }
            };
            ///设置下拉框的背景
            _myEnumBox.Background = new SolidColorBrush(Color.FromArgb(20, BorderColor.R, BorderColor.G, BorderColor.B));
            ///将高度绑定到这个对象
            ToolHelp.SetBindingHeight(_myEnumBox, this);
            ///设置字号
            _myEnumBox.FontSize = FontSize;
            ///设置字体
            _myEnumBox.FontFamily = FontFamily;
            ///设置宽度
            _myEnumBox.Width = 100;
            ///设置字体
            _myEnumBox.FontFamily = FontFamily;
            ///设置字号
            _myEnumBox.FontSize = FontSize - 3;
            ///设置背景颜色
            _myEnumBox.Background = new SolidColorBrush(Color.FromArgb(20, BorderColor.R, BorderColor.B, BorderColor.G));
            ///添加主要的框
            this.Children.Add(CheckBorder);
            this.Children.Add(TitleTextBlock);
            #endregion
            BkColor = Color.FromArgb(0, 255, 255, 255);
            ///调整扩展名
            AdjustmentExName();
            ///根据属性调整内容
            SelectXAributeDirBox();
            ///允许显示提示信息
            this.IsDirTip = true;          
            ///调整本身的宽度
            AutoSize();           
        }
        #endregion
        #region 自定义函数
        /// <summary>
        /// 自动修改尺寸
        /// </summary>
        public override void AutoSize()
        {
            double wid = 0;
            ///查询所有子元素
            foreach (FrameworkElement element in Children)
            {
                ///加上子元素的宽度
                wid += element.Width;
            }
            ///设置元素宽度
            this.Width = wid;
            ///设置连线点的位置
            if (SelectPositionStyle == XPositonStyle.Left)
            {
                centerPosition = new Point(CheckBorder.Width / 2, this.Height / 2);
            }
            else
            {
                centerPosition = new Point(wid - CheckBorder.Width / 2, this.Height / 2);
            }
            ///通知修改布局
            ToCallBackParent(MouseState.XModifyLayout);
        }
        /// <summary>
        /// 自动规划控件显示
        /// </summary>
        protected void AutoMyDir()
        {
            Control child = IsNeedBox();
            if (child != null && Children.Contains(child))
            {
                child.Visibility = Visibility.Visible;
            }
            ///如果已经没有曲线之后则改变框
            if (MyBeziers.Count == 0)
            {
                ///设置为没有颜色
                CheckBorder.Background = new SolidColorBrush(NanColor);
                ///重新设置边框的颜色
                CheckBorder.BorderBrush = new SolidColorBrush(BorderColor);
            }
            else
            {
                ///设置为边框颜色
                CheckBorder.Background = new SolidColorBrush(BorderColor);
                ///重新设置边框的颜色
                CheckBorder.BorderBrush = new SolidColorBrush(BorderColor);
            }
        }       
        /// <summary>
        /// (废弃可以删除)调整扩展名
        /// </summary>
        protected void AdjustmentExName()
        {
            ///将类型和扩展名
            switch (_selectType)
            {
                case XAttributeType.XBool:
                    ExName = ExNameEnum.XBool;
                    break;
                case XAttributeType.XByte:
                    ExName = ExNameEnum.XByte;
                    break;
                case XAttributeType.XChar:
                    ExName = ExNameEnum.XChar;
                    break;
                case XAttributeType.XClass:
                    break;
                case XAttributeType.XDateTime:
                    ExName = ExNameEnum.XDateTime;
                    break;
                case XAttributeType.XDouble:
                    ExName = ExNameEnum.XDouble;
                    break;
                case XAttributeType.XFloat:
                    ExName = ExNameEnum.XFloat;
                    break;
                case XAttributeType.XInt:
                    ExName = ExNameEnum.XInt;
                    break;
                case XAttributeType.XQuaternion:

                    break;
                case XAttributeType.XString:
                    ExName = ExNameEnum.XString;
                    break;
                case XAttributeType.XTransform:

                    break;
                case XAttributeType.XVector2:

                    break;
                case XAttributeType.XVector3:

                    break;
                case XAttributeType.XVector4:

                    break;
                case XAttributeType.XEnter:

                    break;
                case XAttributeType.XExc:

                    break;
                case XAttributeType.XEnum:
                    ExName = ExNameEnum.XEnum;
                    break;
                default:

                    break;
            }
        }
        /// <summary>
        /// 当属性的类型被改变的时候需要调整贝塞尔曲线
        /// </summary>
        protected void AdjustBezierLine()
        {
            ///需要删除的曲线
            List<BezierLine> delline = new List<BezierLine>();
            ///查询所有贝塞尔曲线
            foreach (BezierLine line in MyBeziers)
            {
                if (!line.StartPoint.LinkAribute.IsCanLin(line.EndPoint.LinkAribute))
                {
                    delline.Add(line);
                }
            }
            ///开始删除
            foreach (BezierLine line in delline)
            {
                ///删除
                DelBezierLine(line);
            }
            ///清除
            delline.Clear();
        }
        /// <summary>
        /// 给枚举框添加数据
        /// </summary>
        protected void AddEnumValue()
        {
            if (SelectType == XAttributeType.XEnum || SelectType == XAttributeType.XBool)
            {
                _myEnumBox.ItemsSource = MyEnumBoxData;
                _myEnumBox.SelectedValuePath = "Key";
                _myEnumBox.DisplayMemberPath = "Value";
                _myEnumBox.SelectedIndex = 0;
            }
        }
        /// <summary>
        /// 将对应的属性进行对应的修改
        /// </summary>
        protected void Switch_selectType()
        {
            Color nowColor;
            ///将颜色与类型进行匹配
            switch (_selectType)
            {
                case XAttributeType.XBool:
                    nowColor = Color.FromArgb(255, 60, 00, 00);
                    break;
                case XAttributeType.XByte:
                    nowColor = Color.FromArgb(255, 00, 66, 204);
                    break;
                case XAttributeType.XChar:
                    nowColor = Color.FromArgb(255, 255, 00, 255);
                    break;
                case XAttributeType.XClass:
                    nowColor = Color.FromArgb(255, 00, 80, 255);
                    break;
                case XAttributeType.XDateTime:
                    nowColor = Color.FromArgb(255, 33, 66, 66);
                    break;
                case XAttributeType.XDouble:
                    nowColor = Color.FromArgb(255, 61, 61, 30);
                    break;
                case XAttributeType.XFloat:
                    nowColor = Color.FromArgb(255, 214, 214, 173);
                    break;
                case XAttributeType.XInt:
                    nowColor = Color.FromArgb(255, 206, 206, 255);
                    break;
                case XAttributeType.XQuaternion:
                    nowColor = Color.FromArgb(255, 82, 217, 00);
                    break;
                case XAttributeType.XString:
                    nowColor = Color.FromArgb(255, 84, 193, 255);
                    break;
                case XAttributeType.XTransform:
                    nowColor = Color.FromArgb(255, 249, 249, 00);
                    break;
                case XAttributeType.XVector2:
                    nowColor = Color.FromArgb(255, 187, 255, 74);
                    break;
                case XAttributeType.XVector3:
                    nowColor = Color.FromArgb(255, 187, 255, 74);
                    break;
                case XAttributeType.XVector4:
                    nowColor = Color.FromArgb(255, 187, 255, 74);
                    break;
                case XAttributeType.XEnter:
                    nowColor = Color.FromArgb(255, 255, 255, 255);
                    break;
                case XAttributeType.XExc:
                    nowColor = Color.FromArgb(255, 255, 255, 255);
                    break;
                case XAttributeType.XEnum:
                    nowColor = Color.FromArgb(255, 00, 60, 30);
                    break;
                default:
                    nowColor = Color.FromArgb(255, 255, 255, 255);
                    break;
            }
            BorderColor = nowColor;
            SelectColor = nowColor;
            DefultColor = nowColor;
        }
        /// <summary>
        /// 当类型名改变时候
        /// </summary>
        protected void WhileExNameChange()
        {
            Color nowColor;
            switch (ExName)
            {
                case "System.Boolean":
                    nowColor = Color.FromArgb(255, 60, 00, 00);
                    SelectType = XAttributeType.XBool;
                    break;
                case "System.Byte":
                    nowColor = Color.FromArgb(255, 00, 66, 204);
                    SelectType = XAttributeType.XByte;
                    break;
                case "System.Char":
                    nowColor = Color.FromArgb(255, 255, 00, 255);
                    SelectType = XAttributeType.XChar;
                    break;
                case "System.DateTime":
                    nowColor = Color.FromArgb(255, 33, 66, 66);
                    SelectType = XAttributeType.XDateTime;
                    break;
                case "System.Double":
                    nowColor = Color.FromArgb(255, 61, 61, 30);
                    SelectType = XAttributeType.XDouble;
                    break;
                case "System.Single":
                    nowColor = Color.FromArgb(255, 214, 214, 173);
                    SelectType = XAttributeType.XFloat;
                    break;
                case "System.Int32":
                    nowColor = Color.FromArgb(255, 206, 206, 255);
                    SelectType = XAttributeType.XInt;
                    break;
                //case XAttributeType.XQuaternion:
                //    nowColor = Color.FromArgb(255, 82, 217, 00);
                //    //BorderColor = Color.FromArgb(255, 82, 217, 00);
                //    //SelectColor = BorderColor;
                //    break;
                case "System.String":
                    nowColor = Color.FromArgb(255, 84, 193, 255);
                    SelectType = XAttributeType.XString;
                    break;
                default:
                    nowColor = BorderColor;
                    break;
            }
            BorderColor = nowColor;
            SelectColor = nowColor;
            DefultColor = nowColor;
            ///同时修改显示
            AutoMyDir();
        }
        /// <summary>
        /// 获取坐标
        /// </summary>
        /// <returns></returns>
        public Point GetWorldPosition()
        {
            Point RePoint = VectialParentControl.GetPosition();
            Point ParPoint = ParentControl.GetPosition();
            Point thisPoint = VectialParentControl.GetChildWorldPosition(this);
            if (ParentControl != null)
            {
                RePoint = new Point(RePoint.X + ParPoint.X + thisPoint.X + centerPosition.X, RePoint.Y + ParPoint.Y + thisPoint.Y + centerPosition.Y);
            }
            return RePoint;
        }
        /// <summary>
        /// 获取一个与自身方向相反的XPositonStyle
        /// </summary>
        /// <returns></returns>
        public XPositonStyle GetOrXPositonStyle()
        {
            if (SelectPositionStyle == XPositonStyle.Left)
            {
                return XPositonStyle.right;
            }
            else
            {
                return XPositonStyle.Left;
            }
        }
        /// <summary>
        /// 获取和本对象相反的连接方式
        /// </summary>
        /// <returns>返回相反的连接方式</returns>
        public CanLinkType GetOrCanLinkType()
        {
            if(this.CanLinkNum == CanLinkType.More)
            {
                return CanLinkType.One;
            }
            else
            {
                return CanLinkType.More;
            }
        }
        /// <summary>
        /// 另外一个属性接口是否和这个属性接口可以连接 （用于连线的时候）
        /// </summary>
        /// <param name="xps">接口位置属性</param>
        /// <returns></returns>
        public bool IsCanLin(XAribute xa)
        {
            ///不允许自连接
            if (this.ParentControl.Equals(xa.ParentControl))
            {
                return false;
            }
            if (this.SelectPositionStyle != xa.SelectPositionStyle)
            {
                if (this.SelectType == XAttributeType.XEnter && xa.SelectType == XAttributeType.XExc)
                {
                    return true;
                }
                else if (this.SelectType == XAttributeType.XExc && xa.SelectType == XAttributeType.XEnter)
                {
                    return true;
                }
                else
                {
                    if (this.SelectSpc == xa.SelectSpc)
                    {
                        if (xa.ExName == "System.Object" || this.ExName == "System.Object")
                        {
                            return true;
                        }
                        else if (this.ExName == xa.ExName)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 判断此控件是否应该拥有显示框（EnumBox、ValueBox）
        /// </summary>
        /// <returns>返回需要的控件（没有则返回null）</returns>
        protected Control IsNeedBox()
        {
            ///如果还有线连接着或者是不需要复制匡的
            if(MyBeziers.Count != 0 || SelectPositionStyle == XPositonStyle.right)
            {
                ///关闭组件的显示
                _myEnumBox.Visibility = Visibility.Collapsed;
                ValueTextBox.Visibility = Visibility.Collapsed;
                return null;
            }
            ///如果需要添加枚举框
            if (ExName == ExNameEnum.XBool || ExName == ExNameEnum.XEnum)
            {
                ///如果已经改变就移除之前的组件
                if(this.Children.Contains(ValueTextBox))
                {
                    this.Children.Remove(ValueTextBox);
                }
                ///如果必要的不存在则添加
                if(!this.Children.Contains(_myEnumBox))
                {
                    this.Children.Add(_myEnumBox);
                }
                return _myEnumBox;
            }
            ///如果需要绘制输入框
            if(ExName == ExNameEnum.XByte || ExName == ExNameEnum.XChar || ExName == ExNameEnum.XDateTime ||
                ExName == ExNameEnum.XDouble || ExName == ExNameEnum.XFloat || ExName== ExNameEnum.XInt ||
                ExName == ExNameEnum.XString)
            {
                ///如果已经改变就移除之前的组件
                if (this.Children.Contains(_myEnumBox))
                {
                    this.Children.Remove(_myEnumBox);
                }
                ///如果必要的不存在则添加
                if (!this.Children.Contains(ValueTextBox))
                {
                    this.Children.Add(ValueTextBox);
                }
                return ValueTextBox;
            }
            return null;
        }
        /// <summary>
        /// 给此节点放置一个贝塞尔曲线的起点或终点
        /// </summary>
        /// <param name="bl"></param>
        public void AddBezierLine(BezierLine bl)
        {
            if (GetMyBeziers.Contains(bl))
            {
                return;
            }
            if (_canLinkNum == CanLinkType.One)
            {
                for (int i = 0; i < GetMyBeziers.Count; i++)
                {
                    //((IDisposable)MyBeziers[i]).Dispose();
                    BezierLine bz = GetMyBeziers[i];
                    GetMyBeziers[i].DelBezierLine();
                    ToCallBackParent(MouseState.XDelBezier, new XObjectData(bz));
                }
                GetMyBeziers.Clear();
                GetMyBeziers.Add(bl);
            }
            else
            {
                GetMyBeziers.Add(bl);
            }
            ///改变显示
            AutoMyDir();
        }
        /// <summary>
        /// 删除该节点的
        /// </summary>
        /// <param name="bl"></param>
        public void DelBezierLine(BezierLine bl)
        {
            if (bl != null)
            {
                if (GetMyBeziers.Contains(bl))
                {
                    GetMyBeziers.Remove(bl);
                    if (bl != null)
                    {
                        bl.DelBezierLine();
                        ToCallBackParent(MouseState.XDelBezier, new XObjectData(bl));
                    }
                }
            }
            ///重新规划界面
            AutoMyDir();
        }
        /// <summary>
        /// 从本对象中删除一个贝塞尔曲线的引用(仅限于贝塞尔曲线对象调用)
        /// </summary>
        /// <param name="bz"></param>
        public void ChildRemoveBezierLine(BezierLine bz)
        {
            if (bz != null && GetMyBeziers.Contains(bz))
            {
                GetMyBeziers.Remove(bz);
            }
            ///重新规划界面
            AutoMyDir();
        }
        /// <summary>
        /// 清空本节点有关的所有贝塞尔曲线
        /// </summary>
        public void ClearBezierLine()
        {
            DelXAribute();
        }
        /// <summary>
        /// 当父控件移动的时候调整贝塞尔曲线的位置
        /// </summary>
        public void BezierPositionChange()
        {
            for (int i = 0; i < GetMyBeziers.Count; i++)
            {
                if (GetMyBeziers[i] != null)
                {
                    //GetMyBeziers[i].BezierPositionChange(this.GetWorldPosition(), this.SelectPositionStyle);
                    GetMyBeziers[i].AdjustBezierLine();
                }
                else
                {
                    GetMyBeziers.Remove(GetMyBeziers[i]);
                }
            }
        }
        /// <summary>
        /// 删除XAribute的时候 进行必要的操作
        /// </summary>
        public void DelXAribute()
        {
            for (int i = 0; i < GetMyBeziers.Count; i++)
            {
                DelBezierLine(GetMyBeziers[i]);
            }
        }
        /// <summary>
        /// 获取相连的另一个对象没有则返回空
        /// </summary>
        /// <returns></returns>
        public XAribute GetOtherXAribute()
        {
            XAribute bute = null;
            if (this.SelectType != XAttributeType.XExc && this.SelectPositionStyle == XPositonStyle.right)
                return null;
            if (this.MyBeziers.Count > 0)
                bute = this.MyBeziers[0].StartPoint.LinkAribute == this ? this.MyBeziers[0].EndPoint.LinkAribute : this.MyBeziers[0].StartPoint.LinkAribute;
            return bute;
        }
        /// <summary>
        /// 绘制属性值的框
        /// </summary>
        public void RenderValueBox(Size TextSize)
        {
            if (SelectPositionStyle == XPositonStyle.Left)
            {
                this.Width = TextSize.Width + this.Height + 30;
                switch (SelectType)
                {
                    case XAttributeType.XBool:
                        SetLeft(_myEnumBox, this.Width - 20);
                        SetTop(_myEnumBox, BorderWidth * 2);
                        break;
                    case XAttributeType.XEnum:
                        SetLeft(_myEnumBox, this.Width - 20);
                        SetTop(_myEnumBox, BorderWidth * 2);
                        break;
                    case XAttributeType.XInt:
                        //ValueTextBox.Text = "1";
                        SetLeft(ValueTextBox, this.Width - 20);
                        SetTop(ValueTextBox, BorderWidth * 2);
                        break;
                    case XAttributeType.XFloat:
                        //ValueTextBox.Text = "1";
                        SetLeft(ValueTextBox, this.Width - 20);
                        SetTop(ValueTextBox, BorderWidth * 2);
                        break;
                    case XAttributeType.XDouble:
                        //ValueTextBox.Text = "1";
                        SetLeft(ValueTextBox, this.Width - 20);
                        SetTop(ValueTextBox, BorderWidth * 2);
                        break;
                    case XAttributeType.XChar:
                        //ValueTextBox.Text = "";
                        SetLeft(ValueTextBox, this.Width - 20);
                        SetTop(ValueTextBox, BorderWidth * 2);
                        break;
                    case XAttributeType.XByte:
                        //ValueTextBox.Text = "1";
                        SetLeft(ValueTextBox, this.Width - 20);
                        SetTop(ValueTextBox, BorderWidth * 2);
                        break;
                    case XAttributeType.XString:
                        //ValueTextBox.Text = "";
                        SetLeft(ValueTextBox, this.Width - 20);
                        SetTop(ValueTextBox, BorderWidth * 2);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                this.Width = TextSize.Width + this.Height;
            }
        }
        /// <summary>
        /// 根据属性类型选择是否给与初始属性框
        /// </summary>
        public void SelectXAributeDirBox()
        {
            if (this.SelectPositionStyle == XPositonStyle.Left)
            {
                if (this.SelectType == XAttributeType.XBool)
                {
                    MyEnumBoxData.Add(1, "true(真)");
                    MyEnumBoxData.Add(0, "false(假)");
                    AddEnumValue();
                }            
            }
            ///调整显示
            AutoMyDir();
        }
        /// <summary>
        /// 映射枚举值
        /// </summary>
        /// <param name="value">枚举值的字符串形式</param>
        /// <returns>返回具体枚举值</returns>
        public static XAttributeType XAttributeTypeMapping(string value)
        {
            foreach (XAribute.XAttributeType item in Enum.GetValues(typeof(XAribute.XAttributeType)))
            {
                if (value == item.ToString())
                {
                    return item;
                }
            }
            return XAribute.XAttributeType.XExc;
        }
        /// <summary>
        /// 映射枚举值
        /// </summary>
        /// <param name="value">枚举值的字符串形式</param>
        /// <returns>返回具体枚举值</returns>
        public static XAttributeSpec XAttributeSpecMapping(string value)
        {
            foreach (XAribute.XAttributeSpec item in Enum.GetValues(typeof(XAribute.XAttributeSpec)))
            {
                if (value == item.ToString())
                {
                    return item;
                }
            }
            return XAribute.XAttributeSpec.XNone;
        }
        /// <summary>
        /// 映射枚举值
        /// </summary>
        /// <param name="value">枚举值的字符串形式</param>
        /// <returns>返回具体枚举值</returns>
        public static XPositonStyle XPositonStyleMapping(string value)
        {
            foreach (XAribute.XPositonStyle item in Enum.GetValues(typeof(XAribute.XPositonStyle)))
            {
                if (value == item.ToString())
                {
                    return item;
                }
            }
            return XAribute.XPositonStyle.Left;
        }
        /// <summary>
        /// 映射枚举值
        /// </summary>
        /// <param name="value">枚举值的字符串形式</param>
        /// <returns>返回具体枚举值</returns>
        public static CanLinkType CanLinkTypeMapping(string value)
        {
            foreach (CanLinkType item in Enum.GetValues(typeof(XAribute.CanLinkType)))
            {
                if (value == item.ToString())
                {
                    return item;
                }
            }
            return CanLinkType.One;
        }
        /// <summary>
        /// 获取属性框的值
        /// </summary>
        /// <returns></returns>
        public string GetValueTextBox()
        {
            string reValue = "";
            switch (this.SelectType)
            {
                case XAttributeType.XString:
                    reValue = "\"" + ValueTextBox.Text + "\"";
                    break;
                case XAttributeType.XBool:
                    int resselect = ((KeyValuePair<int, string>)_myEnumBox.SelectedItem).Key;
                    reValue = resselect == 1 ? "true" : "false";
                    break;
                default:
                    reValue = ValueTextBox.Text;
                    break;
            }
            return reValue;
        }
        /// <summary>
        /// 设置属性框的值
        /// </summary>
        /// <param name="value"></param>
        public void SetValueTextBox(string value)
        {
            if (this.SelectType == XAribute.XAttributeType.XString || this.SelectType == XAribute.XAttributeType.XInt || this.SelectType == XAribute.XAttributeType.XFloat ||
               this.SelectType == XAribute.XAttributeType.XDouble || this.SelectType == XAribute.XAttributeType.XChar)
            {
                if (this.SelectType == XAttributeType.XString && value.Length >= 1)
                {
                    value = value.Substring(1, value.Length - 2);
                    ValueTextBox.Text = value;
                }
                else
                {
                    ValueTextBox.Text = value;
                }
            }
            else if (this.SelectType == XAttributeType.XBool)
            {
                int key = value == "true" ? 1 : 0;
                _myEnumBox.SelectedItem = new KeyValuePair<int, string>(key, value);
            }
        }
        /// <summary>
        /// 是否启用遮罩
        /// </summary>
        /// <param name="ismask">是否启用遮罩</param>
        protected void SetOrCancelMask(bool ismask)
        {
            if (ismask)
            {
                this.Background = new SolidColorBrush(NanColor);
                return;
            }
            ///绘制遮罩
            if (this.SelectPositionStyle == XPositonStyle.Left)
            {
                this.Background = new LinearGradientBrush(BorderColor, Color.FromArgb(0, BorderColor.R
                    , BorderColor.B, BorderColor.G), new Point(0, 0), new Point(this.Width, this.Height));
            }
            else
            {
                this.Background = new LinearGradientBrush(BorderColor, Color.FromArgb(0, BorderColor.R
                    , BorderColor.B, BorderColor.G), new Point(this.Width, this.Height), new Point(0, 0));
            }
        }
        #region 编译使用的函数
        /// <summary>
        /// 映射类型为C语言的类型
        /// </summary>
        /// <returns>返回映射的代码</returns>
        public string MappingExNameToCLanguages()
        {
            string mappingstring = "";
            switch (ExName)
            {
                case "System.Int32":
                    mappingstring = "int";
                    break;
                case "System.Int":
                    mappingstring = "int";
                    break;
                case "System.Boolean":
                    mappingstring = "bool";
                    break;
                case "System.Single":
                    mappingstring = "float";
                    break;
                case "System.Double":
                    mappingstring = "double";
                    break;
                case "System.Char":
                    mappingstring = "char";
                    break;
            }
            return mappingstring;
        }
        #endregion
        #endregion
        #region 继承函数
        /// <summary>
        /// 界面渲染函数
        /// </summary>
        /// <param name="dc"></param>
        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            ///调整宽度
            AutoSize();
        }
        /// <summary>
        /// 重写被选中的时候
        /// </summary>
        public override void SetSelectState()
        {
            SetState(true);
            if (GetMyBeziers.Count > 0)
            {
                _myEnumBox.Visibility = Visibility.Collapsed;
            }
        }
        /// <summary>
        /// 重写取消选中的时候
        /// </summary>
        public override void CanelSelectState()
        {
            SetState(false);
            if (GetMyBeziers.Count == 0)
            {
                _myEnumBox.Visibility = Visibility.Visible;
            }
        }
        /// <summary>
        /// 当鼠标进入的时候
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseEnter(System.Windows.Input.MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            ///通知父控件鼠标在子控件上
            ToCallBackParent(MouseState.XMouseEnter);
            ///启用遮罩
            SetOrCancelMask(true);
            //InvalidateVisual();
        }
        /// <summary>
        /// 当鼠标离开的时候
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(System.Windows.Input.MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            ///通知父控件鼠标离开子控件
            ToCallBackParent(MouseState.XMouseLeave);
            ///取消遮罩
            SetOrCancelMask(false);
            //InvalidateVisual();
        }
        /// <summary>
        /// 鼠标左键按下
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            //base.OnMouseLeftButtonDown(e);          
            ToCallBackParent(MouseState.XToDrawBezier);
        }
        /// <summary>
        /// 鼠标左键抬起
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
        }
        /// <summary>
        /// 鼠标移动事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
        }   
        #endregion
    }
    /// <summary>
    /// 对ExName类型的字符串属性枚举静态类(基础属性)
    /// </summary>
    public static class ExNameEnum
    {
        /// <summary>
        /// 布尔类型的属性枚举
        /// </summary>
        public static string XBool = "System.Boolean";
        /// <summary>
        /// 字节类型的属性枚举
        /// </summary>
        public static string XByte = "System.Byte";
        /// <summary>
        /// 字符类型的属性枚举
        /// </summary>
        public static string XChar = "System.Char";
        /// <summary>
        /// 类类型的枚举
        /// </summary>
        public static string XClass = "XClass";
        /// <summary>
        /// 时间类型的属性枚举
        /// </summary>
        public static string XDateTime = "System.DateTime";
        /// <summary>
        /// 双精度浮点型的属性类型枚举
        /// </summary>
        public static string XDouble = "System.Double";
        /// <summary>
        /// 单精度浮点型的属性类型枚举
        /// </summary>
        public static string XFloat = "System.Float";
        /// <summary>
        /// 整形类型的枚举
        /// </summary>
        public static string XInt = "System.Int32";
        /// <summary>
        /// 字符串类型属性的枚举
        /// </summary>
        public static string XString = "System.String";
        /// <summary>
        /// 枚举类型的属性的枚举
        /// </summary>
        public static string XEnum = "System.Enum";
    }
}
