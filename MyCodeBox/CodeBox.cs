using MyVectialPanel;
using MyXAribute;
using MyXObject;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MyCodeBox
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:MyCodeBox"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:MyCodeBox;assembly=MyCodeBox"
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
    public class CodeBox : XObject 
    {
        public delegate void XAributeChange();
        static CodeBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CodeBox), new FrameworkPropertyMetadata(typeof(CodeBox)));
        }  
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public CodeBox()
        {
            InitBaseInfo();
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">唯一标识符</param>
        /// <param name="title">控件标题</param>
        /// <param name="mcf">控件事件回调函数</param>
        public CodeBox(int id,XObject ParentControl, string title, MouseCallFunction mcf, XAType CodeBoxType) : base(id, title)
        {         
            this.CallBackFunction = mcf;
            this.Alpha = 80;
            this.ParentControl = ParentControl;
            this.CodeBoxType = CodeBoxType;
            InitBaseInfo();
        }
        /// <summary>
        /// 初始化基本信息
        /// </summary>
        protected override void InitBaseInfo()
        {
            ///初始化属性信息
            base.InitBaseInfo();
            this.TitleHeight = 30;
            this.Alpha = 60;
            this.Width = 300;
            this.Height = 210;
            this.MoveDistance = 1;

            ///初始化子控件信息
            this.LeftAribute.Height = 0;
            this.LeftAribute.Width = 0;
            this.RightAribute.Height = 0;
            this.RightAribute.Width = 0;

            ///允许显示提示信息
            this.IsDirTip = true;

            ///初始化位置信息
            this.Children.Add(this.LeftAribute);
            this.Children.Add(this.RightAribute);
            this.IsToRenderTitLe = true;
            this.Height = TitleHeight + 2;
            FontSize = 20;
            ///如果标题改变
            this.TitleChange += (s, o, v) =>
            {
                AdjustTitleText();
                return true;
            };
            ///调整标题
            AdjustTitleText();
        }
        #endregion
        #region 定义属性
        /// <summary>
        /// 用于产生随机字符串
        /// </summary>
        private static char[] constant =
        {
        'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
        'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'
        };
        /// <summary>
        /// 要被替换的属性变量
        /// </summary>
        private string myvariables = "/*myvariables*/";
        /// <summary>
        /// 标题框的颜色
        /// </summary>
        private Color _titleBoxColor = Color.FromArgb(150, 150, 150, 150);
        /// <summary>
        /// 最大的子元素个数
        /// </summary>
        private int _maxIdNum = 300000;
        /// <summary>
        /// 用于ID计算
        /// </summary>
        private int _currentId = 0;
        /// <summary>
        /// 对于属性的操作
        /// </summary>
        public enum XAType
        {
            /// <summary>
            /// 对属性的读
            /// </summary>
            get = 1,
            /// <summary>
            /// 对属性的设置s
            /// </summary>
            set = 2,
            /// <summary>
            /// 入口函数
            /// </summary>
            XMain = 3,
            /// <summary>
            /// 通过函数获取所需的值
            /// </summary>
            GetValue = 4,
            /// <summary>
            /// 函数入口事件
            /// </summary>
            OrallyEvent = 5,
            /// <summary>
            /// 事件
            /// </summary>
            XEvent = 6,
            /// <summary>
            /// 函数
            /// </summary>
            XFunction = 7,
            /// <summary>
            /// 函数入口节点
            /// </summary>
            XFunctionEnter = 8,
            /// <summary>
            /// 函数出口节点
            /// </summary>
            XFunctionExc = 9,
            /// <summary>
            /// 系统自带类型
            /// </summary>
            XSystem = 10,
            /// <summary>
            /// 条件语句块
            /// </summary>
            XIf = 11,
            /// <summary>
            /// 基础的for循环
            /// </summary>
            XFor = 12,
            /// <summary>
            /// 对象循环
            /// </summary>
            XForeach = 13
        }     
        /// <summary>
        /// 代码块的类型
        /// </summary>
        private XAType _codeBoxType = XAType.get;
        /// <summary>
        /// 左边的一列
        /// </summary>
        private VectialPanel lefAribute = new VectialPanel( VectialPanel.PositonType.Left);
        /// <summary>
        /// 右边的一列
        /// </summary>
        private VectialPanel rightAribute = new VectialPanel( VectialPanel.PositonType.Right);
        /// <summary>
        /// 标题框高度
        /// </summary>
        private float TitleHeight = 50;
        /// <summary>
        /// 底部预留空间
        /// </summary>
        private double ButtonSpace = 20;
        /// <summary>
        /// 是否绘制标题
        /// </summary>
        private bool _isToRenderTitLe = true;
        /// <summary>
        /// 命名空间
        /// </summary>
        private string _namespace = "";
        /// <summary>
        /// 作为节点返回值的值存储变量
        /// </summary>
        private string _returnValueName = "";
        /// <summary>
        /// 作为系统自带代码块时候编译所用的代码
        /// </summary>
        private string _systemCodeString = "";
        #region 控件
        /// <summary>
        /// 填充标题的文本
        /// </summary>
        private TextBlock _titleText = new TextBlock();
        /// <summary>
        /// 标题框
        /// </summary>
        private Border _titleBorder = new Border();
        /// <summary>
        /// 左边添加的按钮
        /// </summary>
        private Button _leftAddButton;
        /// <summary>
        /// 左边减少按钮
        /// </summary>
        private Button _leftDesButton;
        /// <summary>
        /// 右边添加按钮
        /// </summary>
        private Button _rightAddButton;
        /// <summary>
        /// 右边减少按钮
        /// </summary>
        private Button _rightDesButton;
        /// <summary>
        /// 图片0
        /// </summary>
        private Uri imageadduri0 = new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\Icon\\addbutton0.png");
        /// <summary>
        /// 图片1
        /// </summary>
        private Uri imageadduri1 = new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\Icon\\addbutton1.png");
        /// <summary>
        /// 图片0
        /// </summary>
        private Uri imagedeseuri0 = new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\Icon\\desbutton0.png");
        /// <summary>
        /// 图片1
        /// </summary>
        private Uri imagedeseuri1 = new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\Icon\\desbutton1.png");
        /// <summary>
        /// 当前正在使用的图片资源
        /// </summary>
        private ButtonKey imagenum = new ButtonKey();
        /// <summary>
        /// 启用按钮
        /// </summary>
        private int _openButton = 0;
        #endregion
        #endregion
        #region 读取器
        /// <summary>
        /// 标题框颜色的读取器
        /// </summary>
        public Color TitleBoxColor
        {
            get
            {
                return _titleBoxColor;
            }

            set
            {
                _titleBoxColor = value;
            }
        }
        /// <summary>
        /// 是否绘制标题
        /// </summary>
        public bool IsToRenderTitLe
        {
            get
            {
                return _isToRenderTitLe;
            }

            set
            {
                _isToRenderTitLe = value;
                ///调整尺寸
                AutoSize();
            }
        }
        /// <summary>
        /// 代码块的类型
        /// </summary>
        public XAType CodeBoxType
        {
            get
            {
                return _codeBoxType;
            }

            set
            {
                _codeBoxType = value;
            }
        }
        /// <summary>
        /// 左边属性
        /// </summary>
        public VectialPanel LeftAribute
        {
            get
            {
                return lefAribute;
            }
        }
        /// <summary>
        /// 右边属性
        /// </summary>
        public VectialPanel RightAribute
        {
            get
            {
                return rightAribute;
            }
        }
        /// <summary>
        /// 命名空间
        /// </summary>
        public string Namespace
        {
            get
            {
                return _namespace;
            }

            set
            {
                _namespace = value;
            }
        }
        /// <summary>
        /// 作为节点返回值的值存储变量
        /// </summary>
        public string ReturnValueName
        {
            get
            {
                return _returnValueName;
            }

            set
            {
                _returnValueName = value;
            }
        }
        /// <summary>
        /// 作为系统自带代码块时候编译所用的代码
        /// </summary>
        public string SystemCodeString
        {
            get
            {
                return _systemCodeString;
            }

            set
            {
                _systemCodeString = value;
            }
        }
        /// <summary>
        /// 填充标题的文本
        /// </summary>
        public TextBlock TitleText
        {
            get
            {
                return _titleText;
            }
        }
        /// <summary>
        /// 标题框
        /// </summary>
        public Border TitleBorder
        {
            get
            {
                return _titleBorder;
            }

        }
        /// <summary>
        /// 标题框的位置变换信息
        /// </summary>
        public TranslateTransform TitleTextTransForm
        {
            get
            {
                if(TitleText.RenderTransform as TranslateTransform == null)
                {
                    TitleText.RenderTransform = new TranslateTransform();
                }
                return TitleText.RenderTransform as TranslateTransform;
            }
        }
        /// <summary>
        /// 标题框的位置变换信息
        /// </summary>
        public TranslateTransform BorderTransForm
        {
            get
            {
                if(TitleBorder.RenderTransform as TranslateTransform == null)
                {
                    TitleBorder.RenderTransform = new TranslateTransform();
                }
                return TitleBorder.RenderTransform as TranslateTransform;
            }
        }
        /// <summary>
        /// 左边添加按钮
        /// </summary>
        public Button LeftAddButton
        {
            get
            {
                if(_leftAddButton == null)
                {
                    ///初始化按钮
                    _leftAddButton = new Button();
                    ///设置位置变换旋转
                    _leftAddButton.RenderTransform = new TranslateTransform();
                    ///设置大小
                    _leftAddButton.Width = 50;
                    _leftAddButton.Height = 50;
                    ///设置笔触大小
                    _leftAddButton.BorderThickness = new Thickness(0.1);
                    ///左边的图片刷
                    ImageBrush leftb = new ImageBrush();
                    leftb.Stretch = Stretch.Fill;
                    ///设置图片
                    ImageSource source = new BitmapImage(imageadduri0);
                    leftb.ImageSource = source;
                    ///设置画刷
                    _leftAddButton.Background = leftb;
                    ///设置点击事件
                    _leftAddButton.Click += (s, e) =>
                    {
                        if(imagenum.LeftAdd == 0)
                        {
                            (_leftAddButton.Background as ImageBrush).ImageSource = new BitmapImage(imageadduri1);
                        }
                        else
                        {
                            (_leftAddButton.Background as ImageBrush).ImageSource = new BitmapImage(imageadduri0);
                        }
                        ///取反
                        imagenum.LeftAdd = imagenum.LeftAdd == 0 ? 1 : 0;
                        ///添加一个属性
                        ControlXAributeAddOrDes(1, 2);
                    };                  
                }
                return _leftAddButton;
            }
        }
        /// <summary>
        /// 左边减少按钮
        /// </summary>
        public Button LeftDesButton
        {
            get
            {
                if(_leftDesButton == null)
                {
                    _leftDesButton = new Button();
                    ///设置位置变换旋转
                    _leftDesButton.RenderTransform = new TranslateTransform();
                    ///设置大小
                    _leftDesButton.Width = 50;
                    _leftDesButton.Height = 50;
                    ///左边的图片刷
                    ImageBrush leftb = new ImageBrush();
                    leftb.Stretch = Stretch.Fill;
                    ///设置图片
                    ImageSource source = new BitmapImage(imagedeseuri0);
                    leftb.ImageSource = source;
                    ///设置画刷
                    _leftDesButton.Background = leftb;
                    ///设置点击事件
                    _leftDesButton.Click += (s, e) =>
                    {
                        if (imagenum.LeftAdd == 0)
                        {
                            (_leftDesButton.Background as ImageBrush).ImageSource = new BitmapImage(imagedeseuri1);
                        }
                        else
                        {
                            (_leftDesButton.Background as ImageBrush).ImageSource = new BitmapImage(imagedeseuri0);
                        }
                        ///取反
                        imagenum.LeftDes = imagenum.LeftDes == 0 ? 1 : 0;
                        ///删除一个属性
                        ControlXAributeAddOrDes(1, 1);
                    };
                }
                return _leftDesButton;
            }
        }
        /// <summary>
        /// 右边添加按钮
        /// </summary>
        public Button RightAddButton
        {
            get
            {
                if (_rightAddButton == null)
                {
                    _rightAddButton = new Button();
                    ///设置位置变换旋转
                    _rightAddButton.RenderTransform = new TranslateTransform();
                    ///设置大小
                    _rightAddButton.Width = 50;
                    _rightAddButton.Height = 50;
                    ///设置笔触大小
                    _leftAddButton.BorderThickness = new Thickness(0.1);
                    ///左边的图片刷
                    ImageBrush leftb = new ImageBrush();
                    leftb.Stretch = Stretch.Fill;
                    ///设置图片
                    ImageSource source = new BitmapImage(imageadduri0);
                    leftb.ImageSource = source;
                    ///设置画刷
                    _rightAddButton.Background = leftb;
                    ///设置点击事件
                    _rightAddButton.Click += (s, e) =>
                    {
                        if (imagenum.LeftAdd == 0)
                        {
                            (_rightAddButton.Background as ImageBrush).ImageSource = new BitmapImage(imageadduri1);
                        }
                        else
                        {
                            (_rightAddButton.Background as ImageBrush).ImageSource = new BitmapImage(imageadduri0);
                        }
                        ///取反
                        imagenum.RightAdd = imagenum.RightAdd == 0 ? 1 : 0;
                        ///添加一个属性
                        ControlXAributeAddOrDes(2, 2);
                    };
                }
                return _rightAddButton;
            }
        }
        /// <summary>
        /// 右边减少按钮
        /// </summary>
        public Button RightDesButton
        {
            get
            {
                if(_rightDesButton == null)
                {
                    _rightDesButton = new Button();
                    ///设置位置变换旋转
                    _rightDesButton.RenderTransform = new TranslateTransform();
                    ///设置大小
                    _rightDesButton.Width = 50;
                    _rightDesButton.Height = 50;
                    ///左边的图片刷
                    ImageBrush leftb = new ImageBrush();
                    leftb.Stretch = Stretch.Fill;
                    ///设置图片
                    ImageSource source = new BitmapImage(imagedeseuri0);
                    leftb.ImageSource = source;
                    ///设置画刷
                    _rightDesButton.Background = leftb;
                    ///设置点击事件
                    _rightDesButton.Click += (s, e) =>
                    {
                        if (imagenum.LeftAdd == 0)
                        {
                            (_rightDesButton.Background as ImageBrush).ImageSource = new BitmapImage(imagedeseuri1);
                        }
                        else
                        {
                            (_rightDesButton.Background as ImageBrush).ImageSource = new BitmapImage(imagedeseuri0);
                        }
                        ///取反
                        imagenum.RightDes = imagenum.RightDes == 0 ? 1 : 0;
                        ///删除一个属性
                        ControlXAributeAddOrDes(2,1);
                    };
                }
                return _rightDesButton;
            }
        }
        /// <summary>
        /// 0为2个都不启用，1为启用左边，2位启用右边，3位2边都启用
        /// </summary>
        public int OpenButton
        {
            get
            {
                return _openButton;
            }
            set
            {
                _openButton = value;
                #region 添加按钮
                ///处理按钮的显示
                switch (value)
                {
                    case 0:
                        break;
                    case 1:
                        ///设置空间
                        ButtonSpace = LeftAddButton.Height * 2 + 10;
                        ///添加按钮
                        this.Children.Add(LeftAddButton);
                        this.Children.Add(LeftDesButton);
                        break;
                    case 2:
                        ///设置空间
                        ButtonSpace = RightAddButton.Height * 2 + 10;
                        ///添加按钮
                        this.Children.Add(RightAddButton);
                        this.Children.Add(RightDesButton);
                        break;
                    case 3:
                        ///设置空间
                        ButtonSpace = RightAddButton.Height * 2 + 10;
                        ///添加按钮
                        this.Children.Add(LeftAddButton);
                        this.Children.Add(LeftDesButton);
                        ///添加按钮
                        this.Children.Add(RightAddButton);
                        this.Children.Add(RightDesButton);
                        break;
                }
                #endregion
                ///调整显示
                AutoSize();
            }
        }
        /// <summary>
        ///  属性改变通知
        /// </summary>
        public XAributeChange XAributeChangeMessage { get; set; }
        #endregion
        #region 继承父类的函数
        /// <summary>
        /// 界面渲染函数
        /// </summary>
        /// <param name="dc"></param>
        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            #region 绘制背景
            dc.DrawRoundedRectangle(new SolidColorBrush(Color.FromArgb((byte)Alpha, this.BkColor.R, this.BkColor.G, this.BkColor.B)), new Pen(new SolidColorBrush(this.BorderColor), this.BorderWidth), new Rect(0, 0, RenderSize.Width, RenderSize.Height), this.Radius, this.Radius);
            #endregion
            #region 绘制标题
            //if (IsToRenderTitLe)
            //{
            //    TextPoint.X = this.RenderSize.Width / 12;
            //    TextPoint.Y = 5;
            //    Pen MyTextPen = new Pen(new SolidColorBrush(this.FontColor), 1);
            //    FormattedText MyFontF = new FormattedText(this.Title, CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, new Typeface(this.MyFont), FontSize, new SolidColorBrush(this.FontColor));
            //    dc.DrawText(MyFontF, TextPoint);
            //    dc.DrawRoundedRectangle(new LinearGradientBrush(_titleBoxColor, Color.FromArgb(10, this.BkColor.R, this.BkColor.G, this.BkColor.B), 0), new Pen(), new Rect(new Point(0, 0), new Size(RenderSize.Width, TitleHeight)), this.Radius, this.Radius);
            //}
            #endregion          
        }
        #region 用于控件移动的属性
        /// <summary>
        /// 是否可以准备移动
        /// </summary>
        private bool isReadToMove = false;
        /// <summary>
        /// 开始移动时候的起始位置
        /// </summary>
        Point _mouseStartPoint = new Point();
        /// <summary>
        /// 要移动到的地方的点
        /// </summary>
        Point _mouseEndPoint = new Point();
        #endregion
        /// <summary>
        /// 鼠标按下的时候
        /// </summary>
        /// <param name="e">鼠标信息</param>
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {                                        
            if(ParentControl != null && HasChildControled())
            {
                ///调用父类的发送被点击事件
                base.OnMouseDown(e);
                ///按下的时候捕获鼠标焦点   
                CaptureMouse();
                isReadToMove = true;
                _mouseStartPoint = e.GetPosition(ParentControl);
                _mouseEndPoint = _mouseStartPoint;
            }
        }
        /// <summary>
        /// 鼠标移动函数
        /// </summary>
        /// <param name="e">鼠标的信息</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);          
            if(isReadToMove)
            {
                _mouseEndPoint = e.GetPosition(ParentControl);
                if(ToolHelp.DisPoint(_mouseStartPoint,_mouseEndPoint) > MoveDistance)
                {
                    ///获取当前控件的位置信息
                    Point nowPoint = new Point();
                    nowPoint.X = GetLeft();
                    nowPoint.Y = GetTop();
                    ///计算鼠标移动量
                    Point toPoint = ToolHelp.PointSecPoint(_mouseStartPoint, _mouseEndPoint);
                    ///转换为控件的移动量
                    nowPoint.X += toPoint.X;
                    nowPoint.Y += toPoint.Y;
                    ///将转换后的坐标放入
                    SetPosition(nowPoint);
                    ///重置鼠标初始位置
                    _mouseStartPoint = _mouseEndPoint;
                    BezierLinePositionChange();
                    ToCallBackParent(MouseState.XMoveControl, new XObjectData(toPoint));
                }
            }
        }
        /// <summary>
        /// 当鼠标抬起的时候
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            ///释放捕获的鼠标
            ReleaseMouseCapture();
            if (isReadToMove)
            {
                isReadToMove = false;
            }
        }
        /// <summary>
        /// 键盘按下事件
        /// </summary>
        /// <param name="e"></param>
        public override void OnMyKeyDown(KeyEventArgs e)
        {
            base.OnMyKeyDown(e);
        }
        #endregion
        #region 自定义函数
        /// <summary>
        /// 控制按钮去添加或者函数属性
        /// </summary>
        /// <param name="pos">控制左边还是右边（1是左边，2是右边）</param>
        /// <param name="ctl">控制删除还是添加(1是删除，2是添加)</param>
        protected void ControlXAributeAddOrDes(int pos, int ctl)
        {
            ///要使用到的面板
            VectialPanel vecpanel;
            ///根据要被控制的方向确定具体使用的
            if(pos == 1)
            {
                vecpanel = LeftAribute;
            }
            else if(pos == 2)
            {
                vecpanel = rightAribute;
            }
            else
            {
                return;
            }
            if(ctl == 1)
            {                
                ///被控制的属性
                XAribute ctlbute = vecpanel.Children[vecpanel.Children.Count - 1] as XAribute;
                ///删除一个属性
                if (ctlbute != null && ctlbute.SelectType != XAribute.XAttributeType.XExc && ctlbute.SelectType != XAribute.XAttributeType.XEnter)
                {
                        ///删除一个属性
                        this.DelAttribute(ctlbute);                                        
                }
            }
            else if(ctl == 2)
            {
                if (vecpanel.Children.Count >= 2 && this.CodeBoxType == XAType.XFunctionExc)
                {
                    return;
                }
                ///被控制的属性
                XAribute ctlbute = vecpanel.Children[vecpanel.Children.Count - 1] as XAribute;
                if(ctlbute != null)
                {
                    if(ctlbute.SelectType == XAribute.XAttributeType.XEnter || ctlbute.SelectType == XAribute.XAttributeType.XExc)
                    {
                        AddAttribute(XAribute.XAttributeType.XBool, XAribute.XAttributeSpec.XNone,
                            ctlbute.SelectPositionStyle, "new" + GenerateRandom(5), ctlbute.GetOrCanLinkType(),
                            "新添加的属性", "System.Boolean");
                    }
                    else
                    {
                        ///添加一个属性
                        XAribute newbute = AddAttribute(ctlbute);
                        if(newbute != null)
                        {
                            newbute.Title = "new" + GenerateRandom(5);
                        }
                    }
                }
            }
            else
            {
                return;
            }
            ///调整尺寸
            AutoSize();
            XAributeChangeMessage(); //发送通知
        }
        /// <summary>
        /// 调整按钮的位置
        /// </summary>
        protected void AdjustButtonPositon()
        {
            TranslateTransform trans;
            if(this.Children.Contains(LeftAddButton))
            {
                ///获取位置信息
                trans = LeftAddButton.RenderTransform as TranslateTransform;
                ///设置位置信息
                trans.X = 5;
                trans.Y = this.Height - ButtonSpace;
            }
            if(this.Children.Contains(LeftDesButton))
            {
                trans = LeftDesButton.RenderTransform as TranslateTransform;
                trans.X = 5;
                trans.Y = this.Height - ButtonSpace / 2;
            }
            if(this.Children.Contains(RightAddButton))
            {
                trans = RightAddButton.RenderTransform as TranslateTransform;
                trans.X = this.Width - RightAddButton.Width - 5;
                trans.Y = this.Height - ButtonSpace;
            }
            if(this.Children.Contains(RightDesButton))
            {
                trans = RightDesButton.RenderTransform as TranslateTransform;
                trans.X = this.Width - RightDesButton.Width - 5;
                trans.Y = this.Height - ButtonSpace / 2;
            }
        }
        /// <summary>
        /// 调整标题的显示
        /// </summary>
        protected void AdjustTitleText()
        {
            ///如果不绘制标题
            if (!IsToRenderTitLe)
            {
                ///设置标题高度为0
                TitleHeight = Radius;
                TitleText.Visibility = Visibility.Collapsed;
                TitleBorder.Visibility = Visibility.Collapsed;
                return;
            }
            if (IsToRenderTitLe)
            {
                #region 确认已经添加了绘制标题
                if (!this.Children.Contains(TitleText))
                {
                    ///设置标题框
                    this.Children.Add(TitleText);
                    TitleText.FontFamily = FontFamily;
                    TitleText.FontSize = FontSize;
                    TitleTextTransForm.X = Radius;
                    TitleTextTransForm.Y = Radius;
                    TitleText.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                }
                TitleText.Text = Title;
                FormattedText text = new FormattedText(Title, CultureInfo.CurrentUICulture, TitleText.FlowDirection
                    , new Typeface(MyFont), TitleText.FontSize, TitleText.Foreground);
                TitleText.Width = text.Width + 10;
                TitleText.Height = text.Height;
                TitleHeight = (float)text.Height + 2 * Radius;
                if (!this.Children.Contains(TitleBorder))
                {
                    this.Children.Add(TitleBorder);
                    ///设置边框
                    TitleBorder.BorderThickness = new Thickness(0.3f);
                    TitleBorder.CornerRadius = new CornerRadius(Radius, Radius, Radius, Radius);
                    ///将宽度绑定
                    ToolHelp.SetBindingWidth(TitleBorder, this);
                    _titleBoxColor.A = (byte)Alpha;
                }
                TitleBorder.Height = TitleHeight;               
                #endregion
            }
        }
        /// <summary>
        /// 获取代码块的入口(没有则返回null )
        /// </summary>
        /// <returns>返回入口节点</returns>
        public XAribute GetLeftEnter()
        {
            foreach(XAribute bute in this.LeftAribute.Children)
            {
                if (bute.SelectType == XAribute.XAttributeType.XEnter)
                    return bute;
            }
            return null;
        }
        /// <summary>
        /// 获取该代码块右边一列的出口属性
        /// </summary>
        /// <returns></returns>
        public List<XAribute> GetRightExc()
        {
            List<XAribute> xas = new List<XAribute>();
            foreach(XAribute xa in rightAribute.Children)
            {
                if(xa.SelectType == XAribute.XAttributeType.XExc)
                {
                    xas.Add(xa);
                }
            }
            return xas;
        }      
        /// <summary>
        /// 获取随机长度的字符串
        /// </summary>
        /// <param name="Length"></param>
        /// <returns></returns>
        public static string GenerateRandom(int Length)
        {
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(52);
            Random rd = new Random();
            for (int i = 0; i < Length; i++)
            {
                newRandom.Append(constant[rd.Next(52)]);
            }
            return newRandom.ToString();
        }
        /// <summary>
        /// 按照向量移动
        /// </summary>
        /// <param name="VectorPoint">要移动的向量</param>
        public void SetVectorMove(Point VectorPoint)
        {
            SetMoveOffect(VectorPoint);
            BezierLinePositionChange();
        }
        /// <summary>
        /// 创建一个子元素可用的ID
        /// </summary>
        /// <returns></returns>
        protected int CreatXAributeId()
        {
            return (_currentId++) % _maxIdNum;
        }
        /// <summary>
        /// 添加一个子元素
        /// </summary>
        /// <param name="xt">属性类型</param>
        /// <param name="xs">属性的集合类型</param>
        /// <param name="xp">复制匡的位置是在左边还是右边</param>
        /// <param name="paramterName">属性名称</param>
        public XAribute AddAttribute(XAribute.XAttributeType xt, XAribute.XAttributeSpec xs, XAribute.XPositonStyle xp, string paramterName, XAribute.CanLinkType clt, string tiptext, string LastExName)
        {
            int id = CreatXAributeId();
            XAribute xb = new XAribute(id,paramterName,xt,xs,xp,this.ChileEventCallBack,clt, LastExName);
            xb.ParentControl = this;
            xb.Hint = tiptext;
            if (xp == XAribute.XPositonStyle.Left)
            {
                this.LeftAribute.AddXAbutrite(xb);
                xb.VectialParentControl = LeftAribute;
            }
            else
            {
                this.RightAribute.AddXAbutrite(xb);
                xb.VectialParentControl = RightAribute;
            }
            ///调整尺寸
            AutoSize();
            return xb;
        }
        /// <summary>
        /// 添加一个子元素
        /// </summary>
        /// <param name="bute">以拷贝的形式</param>
        /// <returns></returns>
        public XAribute AddAttribute(XAribute bute)
        {
            int id = CreatXAributeId();
            XAribute xb = new XAribute(id, bute);
            xb.ParentControl = this;
            if (bute.SelectPositionStyle == XAribute.XPositonStyle.Left)
            {
                this.LeftAribute.AddXAbutrite(xb);
                xb.VectialParentControl = LeftAribute;
            }
            else
            {
                this.RightAribute.AddXAbutrite(xb);
                xb.VectialParentControl = RightAribute;
            }
            ///调整尺寸
            AutoSize();
            return xb;
        }
        /// <summary>
        /// 给代码块添加一个入口
        /// </summary>
        public void AddXEnterXAribute()
        {
            AddAttribute(XAribute.XAttributeType.XEnter, XAribute.XAttributeSpec.XNone, XAribute.XPositonStyle.Left
                , "入口", XAribute.CanLinkType.More, "入口", "");
        }
        /// <summary>
        /// 给代码块添加一个出口
        /// </summary>
        public void AddXExcXAribute()
        {
            AddAttribute(XAribute.XAttributeType.XExc, XAribute.XAttributeSpec.XNone, XAribute.XPositonStyle.right
                , "出口", XAribute.CanLinkType.One, "出口", "");
        }
        /// <summary>
        /// 添加一个子元素
        /// </summary>
        /// <param name="xt">属性类型</param>
        /// <param name="xs">属性的集合类型</param>
        /// <param name="xp">复制匡的位置是在左边还是右边</param>
        /// <param name="paramterName">属性名称</param>
        /// <param name="clt">连接方式</param>
        /// <param name="tiptext">提示信息</param>
        /// <param name="LastExName">属性类型</param>
        /// <param name="id">属性框ID</param>
        /// <param name="opentype">公开类型</param>
        public XAribute LoadAttribute(XAribute.XAttributeType xt, XAribute.XAttributeSpec xs, XAribute.XPositonStyle xp, string paramterName, XAribute.CanLinkType clt, string tiptext, string LastExName,int id,OpenType opentype)
        {
            XAribute xb = new XAribute(id, paramterName, xt, xs, xp, this.ChileEventCallBack, clt, LastExName);
            xb.ParentControl = this;
            xb.Hint = tiptext;
            xb.MyOpenType = opentype;
            if (xp == XAribute.XPositonStyle.Left)
            {
                this.LeftAribute.AddXAbutrite(xb);
                xb.VectialParentControl = LeftAribute;
            }
            else
            {
                this.RightAribute.AddXAbutrite(xb);
                xb.VectialParentControl = RightAribute;
            }
            ///调整尺寸
            AutoSize();
            return xb;
        }
        /// <summary>
        /// 删除一个属性
        /// </summary>
        /// <param name="xa">要删除的对象</param>
        public void DelAttribute(XAribute xa)
        {
            if(xa.SelectPositionStyle == XAribute.XPositonStyle.Left)
            {    
                    this.LeftAribute.DelXAbutrite(xa);
            }
            else
            {       
                    this.RightAribute.DelXAbutrite(xa);
            }
            ///调整尺寸
            AutoSize();
        }
        /// <summary>
        /// 修改控件尺寸
        /// </summary>
        public override void AutoSize()
        {
            ///修改子控件的尺寸
            LeftAribute.AutoSize();
            RightAribute.AutoSize();
            double toaddheight = LeftAribute.Height > RightAribute.Height ? LeftAribute.Height : RightAribute.Height;
            this.Height = TitleHeight + toaddheight + ButtonSpace;
            this.Width = LeftAribute.Width + RightAribute.Width + 5 > TitleText.Width + 15 ? LeftAribute.Width + RightAribute.Width + 5 : TitleText.Width + 15;
            ///修改右边位置
            SetLeft(RightAribute, this.Width - RightAribute.Width);
            ///修改左边位置
            SetLeft(LeftAribute, 2);
            ///修改高度
            SetTop(LeftAribute, this.TitleHeight + 2);
            SetTop(RightAribute, this.TitleHeight + 2);
            if (IsToRenderTitLe)
            {
                ///修改标题框的绘制
                TitleBorder.Background = new LinearGradientBrush(_titleBoxColor, Color.FromArgb(0, 0, 0, 0), new Point(0, 0), new Point(this.Width, this.TitleHeight));
            }
            ///最后调整按钮的位置
            AdjustButtonPositon();
        }
        /// <summary>
        /// 移动过程中贝塞尔曲线一起变化
        /// </summary>
        public void BezierLinePositionChange()
        {
            for(int i = 0; i < LeftAribute.Children.Count; i++)
            {
                XAribute xa = (XAribute)LeftAribute.Children[i];
                if(xa != null)
                {
                    xa.BezierPositionChange();
                }
            }
            for (int i = 0; i < RightAribute.Children.Count; i++)
            {
                XAribute xa = (XAribute)RightAribute.Children[i];
                if (xa != null)
                {
                    xa.BezierPositionChange();
                }
            }
        }
        /// <summary>
        /// 获取当前控件的世界坐标
        /// </summary>
        /// <returns></returns>
        public Point GetWorldPosition()
        {
            Point nowPoint = new Point(Canvas.GetLeft(this),Canvas.GetTop(this));
            return nowPoint;
        }
        /// <summary>
        /// 删除代码块的时候 进行必要操作
        /// </summary>
        public void DelCodeBox()
        {
            for (int i = 0;i < LeftAribute.Children.Count; i++)
            {
                XAribute xa = (XAribute)LeftAribute.Children[i];
                if (xa != null)
                {
                    xa.DelXAribute();
                }
            }
            for (int i = 0; i < RightAribute.Children.Count; i++)
            {
                XAribute xa = (XAribute)RightAribute.Children[i];
                if (xa != null)
                {
                    xa.DelXAribute();
                }
            }
        }
        /// <summary>
        /// 映射枚举值
        /// </summary>
        /// <param name="value">枚举值的字符串形式</param>
        /// <returns>返回具体枚举值</returns>
        public static XAType CodeBoxTypeMapping(string value)
        {
            foreach (XAType item in Enum.GetValues(typeof(CodeBox.XAType)))
            {
                if (value == item.ToString())
                {
                    return item;
                }
            }
            return XAType.XFunction;
        }
        #region 供编译C#代码时候使用代码
        /// <summary>
        /// 获得该属性的值（供编译使用）
        /// </summary>
        /// <param name="bute">属性对象</param>
        /// <returns></returns>
        public string GetXAributeValue(XAribute bute)
        {
            string ValueString = "";
            switch (this.CodeBoxType)
            {
                case XAType.XFunction:
                    ValueString += this.ReturnValueName;
                    break;
                case XAType.get:
                    AnGetTypeValueString();
                    ValueString += this.ReturnValueName;                   
                    break;
                case XAType.set:
                    ValueString += this.ReturnValueName;
                    break;
                case XAType.XSystem:
                    ///替换掉未声明的变量
                    string getstringl = GenerateRandom(10);
                    SystemCodeString = SystemCodeString.Replace(myvariables, getstringl);
                    ReturnValueName = ReturnValueName.Replace(myvariables, getstringl);
                    ///获取值
                    ValueString += GetSytemTypeValueString(this.ReturnValueName);
                    break;
                case XAType.XFor:
                    ValueString += this.ReturnValueName;
                    break;
                default:
                    break;
            }
            return ValueString;
        }
        /// <summary>
        /// 获取属性框的值（供编译使用）
        /// </summary>
        /// <returns></returns>
        public string GetCodeBoxValue()
        {
            string ValueString = "";
            string LineBack = "\r\n";
            XAribute target;
            ///数字
            int num = 0;
            switch (this.CodeBoxType)
            {
                #region 函数
                case XAType.XFunction:
                    ///输出数据的保存
                    foreach (XAribute bute in this.rightAribute.Children)
                    {
                        if (bute.SelectType != XAribute.XAttributeType.XExc)
                        {
                            if (bute.GetOtherXAribute() == null)
                            {
                                ValueString += bute.GetValueTextBox();
                            }
                            else
                            {
                                ValueString += bute.ExName + " " + bute.Title + " = ";
                            }
                            ReturnValueName = bute.Title;
                        }
                    }
                    ValueString += this.Title + "(";
                    ///输入数据的赋值                   
                    foreach (XAribute bute in this.lefAribute.Children)
                    {
                        if (bute.SelectType != XAribute.XAttributeType.XEnter)
                        {

                            if (num == 0)
                                ValueString += ((CodeBox)bute.GetOtherXAribute().ParentControl).GetXAributeValue(bute.GetOtherXAribute());
                            else
                                ValueString += "," + ((CodeBox)bute.GetOtherXAribute().ParentControl).GetXAributeValue(bute.GetOtherXAribute());
                            num++;
                        }
                    }
                    ValueString += ");" + LineBack;
                    break;
                #endregion
                #region get/set方法
                case XAType.get:
                    //XAribute rebute = (XAribute)(this.rightAribute.Children[0]);
                    //if (this.LeftAribute.Children.Count > 0)
                    //{
                    //    target = ((XAribute)(this.LeftAribute.Children[0])).GetOtherXAribute();                        
                    //    ValueString += ((CodeBox)(target.ParentControl)).GetXAributeValue(target) + "." + rebute.Title;                       
                    //}
                    //else
                    //{
                    //    ValueString += rebute.Title;
                    //}
                    //ReturnValueName = ValueString;
                    AnGetTypeValueString();
                    break;
                case XAType.set:
                    foreach (XAribute bute in this.lefAribute.Children)
                    {
                        if (bute.SelectType == XAribute.XAttributeType.XTarget)
                        {
                            ///获取该属性的对象
                            string st = ((CodeBox)(bute.GetOtherXAribute().ParentControl)).GetXAributeValue(bute.GetOtherXAribute()) + "." + ((XAribute)this.rightAribute.Children[1]).Title;
                            ValueString += st + " = ";
                            //复制返回值
                            ReturnValueName = st;
                        }
                        else if(bute.SelectType != XAribute.XAttributeType.XEnter)
                        {
                            if (ValueString == "")
                            {
                                ///如果是本对象的自己的属性 则加上this
                                ValueString = ((XAribute)this.rightAribute.Children[1]).Title + " = ";
                                ///赋值返回值
                                ReturnValueName = ((XAribute)this.rightAribute.Children[1]).Title;
                            }
                            string targetna;
                            if (bute.GetOtherXAribute() == null)
                            {                               
                                targetna = bute.GetValueTextBox();
                                ValueString += targetna + ";";
                            }
                            else
                            {
                                target = bute.GetOtherXAribute();
                                targetna = ((CodeBox)bute.GetOtherXAribute().ParentControl).GetXAributeValue(target);
                                ValueString += targetna + ";";
                            }                           
                        }
                    }
                    break;
                #endregion
                #region System
                case XAType.XSystem:
                    ///替换掉未声明的变量
                    string getstringl = GenerateRandom(10) + this.Id.ToString();
                    SystemCodeString = SystemCodeString.Replace(myvariables, getstringl);
                    ReturnValueName = ReturnValueName.Replace(myvariables, getstringl);
                    ValueString = GetSytemTypeValueString(SystemCodeString);
                    break;
                #endregion
                #region MyRegion
                case XAType.XFor:
                    ///替换掉未声明的变量(用于for循环的计数)
                    string forstring = GenerateRandom(10) + this.Id.ToString();
                    SystemCodeString = SystemCodeString.Replace(myvariables, forstring);
                    ReturnValueName = ReturnValueName.Replace(myvariables, forstring);
                    ValueString = GetSytemTypeValueString(SystemCodeString);
                    break;
                #endregion
                #region 获取判断的代码解析
                case XAType.XIf:
                    ValueString = "(" + GetIFTypeValueString() + ")";
                    break; 
                #endregion
            }
            return ValueString;
        }
        #region 代码解析
        /// <summary>
        /// 获取是判断类型的代码块的解析
        /// </summary>
        /// <returns>返回结果</returns>
        protected string GetIFTypeValueString()
        {
            string ValueString = "";
            XAribute target;
            foreach(XAribute bute in this.lefAribute.Children)
            {
                if(bute.SelectType == XAribute.XAttributeType.XBool)
                {
                    target = bute.GetOtherXAribute();
                    if(target == null)
                    {
                        ValueString = bute.GetValueTextBox();
                    }
                    else
                    {
                        ValueString = ((CodeBox)target.ParentControl).GetXAributeValue(target);
                    }
                    break;
                }
            }
            return ValueString;
        }
        /// <summary>
        /// 获取当为Sytem类型时候的代码解析
        /// </summary>
        /// <param name="StartSystemCodeString">要进行解析的初始代码</param>
        /// <returns>解析好的代码</returns>
        public string GetSytemTypeValueString(string StartSystemCodeString)
        {           
            string ValueString = StartSystemCodeString;
            XAribute target;
            foreach (XAribute bute in this.LeftAribute.Children)
            {
                if (bute.SelectType != XAribute.XAttributeType.XEnter && bute.SelectType != XAribute.XAttributeType.XExc)
                {
                    string butestring = "";
                    target = bute.GetOtherXAribute();
                    if (bute.GetOtherXAribute() == null)
                    {
                        butestring = bute.GetValueTextBox();
                    }
                    else
                    {
                        butestring = ((CodeBox)target.ParentControl).GetXAributeValue(target);
                    }
                    ValueString = ValueString.Replace("<" + bute.Title + ">", butestring);
                }
            }
            return ValueString;
        }
        /// <summary>
        /// 解析get方法的代码解析
        /// </summary>
        /// <returns></returns>
        public void AnGetTypeValueString()
        {
            string ValueString = "";
            XAribute target;
            XAribute rebute = (XAribute)(this.rightAribute.Children[0]);
            if (this.LeftAribute.Children.Count > 0)
            {
                target = ((XAribute)(this.LeftAribute.Children[0])).GetOtherXAribute();
                ValueString += ((CodeBox)(target.ParentControl)).GetXAributeValue(target) + "." + rebute.Title;
            }
            else
            {
                ValueString += rebute.Title;
            }           
            ReturnValueName = ValueString;
        }
        #endregion
        #endregion
        #region 供编译C语言代码时候使用代码
        /// <summary>
        /// 获取作为C语言代码时候属性框的值（供编译使用）
        /// </summary>
        /// <returns></returns>
        public string GetCLanguagesCodeBoxValue()
        {
            string ValueString = "";
            string LineBack = "\r\n";
            XAribute target;
            ///数字
            int num = 0;
            switch (this.CodeBoxType)
            {
                #region 函数
                case XAType.XFunction:
                    ///输出数据的保存
                    foreach (XAribute bute in this.rightAribute.Children)
                    {
                        if (bute.SelectType != XAribute.XAttributeType.XExc)
                        {
                            if (bute.GetOtherXAribute() == null)
                            {
                                ValueString += bute.GetValueTextBox();
                            }
                            else
                            {
                                ValueString += bute.ExName + " " + bute.Title + " = ";
                            }
                            ReturnValueName = bute.Title;
                        }
                    }
                    ValueString += this.Title + "(";
                    ///输入数据的赋值                   
                    foreach (XAribute bute in this.lefAribute.Children)
                    {
                        if (bute.SelectType != XAribute.XAttributeType.XEnter)
                        {

                            if (num == 0)
                                ValueString += ((CodeBox)bute.GetOtherXAribute().ParentControl).GetCLanguagesXAributeValue(bute.GetOtherXAribute());
                            else
                                ValueString += "," + ((CodeBox)bute.GetOtherXAribute().ParentControl).GetCLanguagesXAributeValue(bute.GetOtherXAribute());
                            num++;
                        }
                    }
                    ValueString += ");" + LineBack;
                    break;
                #endregion
                #region get/set方法
                case XAType.get:
                    AnGetCLanguagesTypeValueString();
                    break;
                case XAType.set:
                    foreach (XAribute bute in this.lefAribute.Children)
                    {
                        if (bute.SelectType == XAribute.XAttributeType.XTarget)
                        {
                            ///获取该属性的对象
                            string st = ((CodeBox)(bute.GetOtherXAribute().ParentControl)).GetCLanguagesXAributeValue(bute.GetOtherXAribute()) + "." + ((XAribute)this.rightAribute.Children[1]).Title;
                            ValueString += st + " = ";
                            //复制返回值
                            ReturnValueName = st;
                        }
                        else if (bute.SelectType != XAribute.XAttributeType.XEnter)
                        {
                            if (ValueString == "")
                            {
                                ///如果是本对象的自己的属性 则加上this
                                ValueString = ((XAribute)this.rightAribute.Children[1]).Title + " = ";
                                ///赋值返回值
                                ReturnValueName = ((XAribute)this.rightAribute.Children[1]).Title;
                            }
                            string targetna;
                            if (bute.GetOtherXAribute() == null)
                            {
                                targetna = bute.GetValueTextBox();
                                ValueString += targetna + ";";
                            }
                            else
                            {
                                target = bute.GetOtherXAribute();
                                targetna = ((CodeBox)bute.GetOtherXAribute().ParentControl).GetCLanguagesXAributeValue(target);
                                ValueString += targetna + ";";
                            }
                        }
                    }
                    break;
                #endregion
                #region System
                case XAType.XSystem:
                    ///替换掉未声明的变量
                    string getstringl = GenerateRandom(15) + this.Id.ToString();
                    SystemCodeString = SystemCodeString.Replace(myvariables, getstringl);
                    ReturnValueName = ReturnValueName.Replace(myvariables, getstringl);
                    ValueString = GetCLanguagesSytemTypeValueString(SystemCodeString);
                    break;
                #endregion
                #region MyRegion
                case XAType.XFor:
                    ///替换掉未声明的变量(用于for循环的计数)
                    string forstring = GenerateRandom(10) + this.Id.ToString();
                    SystemCodeString = SystemCodeString.Replace(myvariables, forstring);
                    ReturnValueName = ReturnValueName.Replace(myvariables, forstring);
                    ValueString = GetSytemTypeValueString(SystemCodeString);
                    break;
                #endregion
                #region 获取IF语句
                case XAType.XIf:
                    ValueString = "(" + GetCLanguagesIFTypeValueString() + ")";
                    break;
                    #endregion
            }
            return ValueString;
        }
        /// <summary>
        /// 解析作为C语言时候的GET属性值
        /// </summary>
        protected void AnGetCLanguagesTypeValueString()
        {
            string ValueString = "";
            XAribute target;
            XAribute rebute = (XAribute)(this.rightAribute.Children[0]);
            if (this.LeftAribute.Children.Count > 0)
            {
                target = ((XAribute)(this.LeftAribute.Children[0])).GetOtherXAribute();
                ValueString += ((CodeBox)(target.ParentControl)).GetXAributeValue(target) + "." + rebute.Title;
            }
            else
            {
                ValueString += rebute.Title;
            }
            ReturnValueName = ValueString;
        }
        /// <summary>
        /// 获取当为Sytem类型时候的代码解析（C语言版的编译使用）
        /// </summary>
        /// <param name="StartSystemCodeString">要进行解析的初始代码</param>
        /// <returns>解析好的代码</returns>
        public string GetCLanguagesSytemTypeValueString(string StartSystemCodeString)
        {
            string ValueString = StartSystemCodeString;
            XAribute target;
            foreach (XAribute bute in this.LeftAribute.Children)
            {
                if (bute.SelectType != XAribute.XAttributeType.XEnter && bute.SelectType != XAribute.XAttributeType.XExc)
                {
                    string butestring = "";
                    target = bute.GetOtherXAribute();
                    if (bute.GetOtherXAribute() == null)
                    {
                        butestring = bute.GetValueTextBox();
                    }
                    else
                    {
                        butestring = ((CodeBox)target.ParentControl).GetXAributeValue(target);
                    }
                    ValueString = ValueString.Replace("<" + bute.Title + ">", butestring);
                }
            }
            return ValueString;
        }
        /// <summary>
        /// 获取是判断类型的代码块的解析(做为C语言编译的时候使用)
        /// </summary>
        /// <returns>返回结果</returns>
        protected string GetCLanguagesIFTypeValueString()
        {
            string ValueString = "";
            XAribute target;
            foreach (XAribute bute in this.lefAribute.Children)
            {
                if (bute.SelectType == XAribute.XAttributeType.XBool)
                {
                    target = bute.GetOtherXAribute();
                    if (target == null)
                    {
                        ValueString = bute.GetValueTextBox();
                    }
                    else
                    {
                        ValueString = ((CodeBox)target.ParentControl).GetXAributeValue(target);
                    }
                    break;
                }
            }
            return ValueString;
        }
        /// <summary>
        /// 获得该属性的值（供C语言编译使用）
        /// </summary>
        /// <param name="bute">属性对象</param>
        /// <returns></returns>
        public string GetCLanguagesXAributeValue(XAribute bute)
        {
            string ValueString = "";
            switch (this.CodeBoxType)
            {
                case XAType.XFunction:
                    ValueString += this.ReturnValueName;
                    break;
                case XAType.get:
                    AnGetCLanguagesTypeValueString();
                    ValueString += this.ReturnValueName;
                    break;
                case XAType.set:
                    ValueString += this.ReturnValueName;
                    break;
                case XAType.XSystem:
                    ///替换掉未声明的变量
                    string getstringl = GenerateRandom(15);
                    SystemCodeString = SystemCodeString.Replace(myvariables, getstringl);
                    ReturnValueName = ReturnValueName.Replace(myvariables, getstringl);
                    ///获取值
                    ValueString += GetCLanguagesSytemTypeValueString(this.ReturnValueName);
                    break;
                case XAType.XFor:
                    ValueString += this.ReturnValueName;
                    break;
                default:
                    break;
            }
            return ValueString;
        }
        #endregion
        /// <summary>
        /// 根据ID获取代码块中的XAribute
        /// </summary>
        /// <param name="ID">XAribute的ID</param>
        /// <returns>寻找到的XAribute没找到则为null</returns>
        public XAribute GetXAributeByID(int ID)
        {
            ///在左边寻找
            foreach(XAribute bute in LeftAribute.Children)
            {
                if(bute.Id == ID)
                {
                    return bute;
                }
            }
            ///在右边寻找
            foreach (XAribute bute in rightAribute.Children)
            {
                if (bute.Id == ID)
                {
                    return bute;
                }
            }
            return null;
        }
        #endregion
        #region 子控件事件回调处理
        /// <summary>
        /// 子控件事件回调处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ms"></param>
        protected override object ChileEventCallBack(Object sender, MouseState ms, XObjectData data = null)
        {           
            switch (ms)
            {
                case MouseState.XClick:
                    break;
                case MouseState.XDown:                  
                    break;
                case MouseState.XToChildControl:
                    AddChildControlMsg();
                    break;
                case MouseState.XToStopControl:
                    DelChildControlMsg();
                    break;
                case MouseState.XModifyLayout:
                    ///调整尺寸
                    AutoSize();
                    break;
            }
            base.ChileEventCallBack(sender, ms, data);
            return null;
        }
        #endregion
    }
    /// <summary>
    /// 作为按钮的点击识别
    /// </summary>
    class ButtonKey
    {
        /// <summary>
        /// 左边添加按钮
        /// </summary>
        public int LeftAdd = 0;
        /// <summary>
        /// 左边减少按钮
        /// </summary>
        public int LeftDes = 0;
        /// <summary>
        /// 右边添加按钮
        /// </summary>
        public int RightAdd = 0;
        /// <summary>
        /// 右边减少按钮
        /// </summary>
        public int RightDes = 0;
    }
}
