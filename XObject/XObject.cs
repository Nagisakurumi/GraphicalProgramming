using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace MyXObject
{
    /// <summary>
    /// 当前鼠标的状态
    /// </summary>
    public enum MouseState
    {
        /// <summary>
        /// 没有鼠标状态
        /// </summary>
        XNone = 0,
        /// <summary>
        /// 鼠标在控件上
        /// </summary>
        XOn = 1,
        /// <summary>
        /// 鼠标进入控件
        /// </summary>
        XEnter = 2,
        /// <summary>
        /// 鼠标退出控件
        /// </summary>
        XOut = 3,
        /// <summary>
        /// 鼠标按下
        /// </summary>
        XDown = 4,
        /// <summary>
        /// 鼠标弹起
        /// </summary>
        XUp = 5,
        /// <summary>
        /// 鼠标单击
        /// </summary>
        XClick = 6,
        /// <summary>
        /// 鼠标双击
        /// </summary>
        XDoubleClick = 7,
        /// <summary>
        /// 鼠标滚轮按下
        /// </summary>
        XWheel = 8,
        /// <summary>
        /// 鼠标悬停事件
        /// </summary>
        XHover = 9,
        /// <summary>
        /// 正在移动控件
        /// </summary>
        XMoveControl = 10,
        /// <summary>
        /// 子控件的点击事件
        /// </summary>
        XChilldClick = 11,
        /// <summary>
        /// 鼠标右击
        /// </summary>
        XRightClick = 12,
        /// <summary>
        /// Del键按下
        /// </summary>
        XDelDwon = 13,
        /// <summary>
        /// 已经停止移动控件
        /// </summary>
        XStopMoveControl = 14,
        /// <summary>
        /// 鼠标移动
        /// </summary>
        XMove = 15,
        /// <summary>
        /// 正在控制子控件
        /// </summary>
        XToChildControl = 16,
        /// <summary>
        /// 停止对子控件的操作(XToChildControl 和 XToStopControl一定要成对出现)
        /// </summary>
        XToStopControl = 17,
        /// <summary>
        /// 清楚所有子控件控制
        /// </summary>
        XClearControl = 18,
        /// <summary>
        /// 画贝塞尔
        /// </summary>
        XToDrawBezier = 19,
        /// <summary>
        /// 表示鼠标进入控件
        /// </summary>
        XMouseEnter = 20,
        /// <summary>
        /// 表示鼠标离开控件
        /// </summary>
        XMouseLeave = 21,
        /// <summary>
        /// 删除贝塞尔曲线的时候
        /// </summary>
        XDelBezier = 22,
        /// <summary>
        /// 创建语句块
        /// </summary>
        XCreateCodeBox = 23,
        /// <summary>
        /// 修改矫正布局
        /// </summary>
        XModifyLayout = 24,
        /// <summary>
        /// 选中目标去显示
        /// </summary>
        XSelectObjectToDir = 25,
        /// <summary>
        /// 当属性值改变的时候
        /// </summary>
        XPropertyValueChange = 26,
        /// <summary>
        /// 更新属性数据时候
        /// </summary>
        XUpdatePropertyData = 27,
        /// <summary>
        /// 当要打开函数页面的时候
        /// </summary>
        XOpenFunction = 28,
        /// <summary>
        /// 更新代码块数据源的数据
        /// </summary>
        XUpdateTreeViewData = 29,
        /// <summary>
        /// 自动修改尺寸
        /// </summary>
        XAutoSize = 30
    }
    /// <summary>
    /// 对消息的操作枚举
    /// </summary>
    public enum MessageOption
    {
        /// <summary>
        /// 删除操作
        /// </summary>
        Delete = 0,
        /// <summary>
        /// 更新
        /// </summary>
        Update = 3,
        /// <summary>
        /// 添加操作
        /// </summary>
        Add = 1,
        /// <summary>
        /// 错误消息
        /// </summary>
        None = 4
    }
    /// <summary>
    /// 继承类型
    /// </summary>
    public enum OverrideType
    {
        /// <summary>
        /// 虚类型
        /// </summary>
        Virture = 0,
        /// <summary>
        /// 继承函数
        /// </summary>
        Override = 1, 
        /// <summary>
        /// 没有类型
        /// </summary>
        None = 2    
    }
    /// <summary>
    /// 子控件鼠标操作后的回调函数类型声明
    /// </summary>
    /// <param name="sender">信息发送者</param>
    /// <param name="mState">发送的具体状态</param>
    public delegate object MouseCallFunction(Object sender, MouseState mState, XObjectData data = null);
    /// <summary>
    /// Titlt文本修改时间
    /// </summary>
    /// <param name="sender">发送信息的控件</param>
    /// <param name="ChangeValue">修改后的文本</param>
    public delegate bool TitleTextChange(object sender, string OldValue,  string NewValue);
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:XObject"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:XObject;assembly=XObject"
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
    public class XObject : ItemsControl,INotifyPropertyChanged
    {
        /// <summary>
        /// 初始化
        /// </summary>
        static XObject()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(XObject), new FrameworkPropertyMetadata(typeof(XObject)));           
        }
        #region 定义属性
        /// <summary>
        /// 控件的标示符
        /// </summary>
        private int _id;
        /// <summary>
        /// 表水透明度0-1之间
        /// </summary>
        private double _alpha = 0;
        /// <summary>
        /// 当前控件的是否被选中
        /// </summary>
        private bool _selectState = false;
        /// <summary>
        /// 提供给父控件使用的回调函数
        /// </summary>
        private MouseCallFunction MCallFc;
        /// <summary>
        /// 字体
        /// </summary>
        //private string _myFont = "华文彩云";
        private string _myFont = "宋体";
        /// <summary>
        /// 字体颜色
        /// </summary>
        private Color _fontColor = Color.FromArgb(255,255,255,255);
        /// <summary>
        /// 边框颜色
        /// </summary>
        private Color _borderColor = Color.FromArgb(200, 2, 247, 142);
        /// <summary>
        /// 标题
        /// </summary>
        private string _title = "默认名称";
        /// <summary>
        /// 边框宽度
        /// </summary>
        private int _borderWidth = 4;
        /// <summary>
        /// 圆角半径
        /// </summary>
        private int _radius = 20;
        /// <summary>
        /// 没有被选中的默认状态
        /// </summary>
        private Color _defultColor = Color.FromArgb(255, 79, 79, 79);
        /// <summary>
        /// 被选中状态
        /// </summary>
        private Color _selectColor = Color.FromArgb(255, 255, 255, 255);
        /// <summary>
        /// 默认边框粗细
        /// </summary>
        private int _defuleBorderWidth = 5;
        /// <summary>
        /// 被选种边框粗细
        /// </summary>
        private int _selectBorderWidth = 8;
        /// <summary>
        /// 背景颜色
        /// </summary>
        protected Color _bkColor = Color.FromArgb(255, 114, 109, 109);
        /// <summary>
        /// 是否有子控件正在被控制(为0 的时候为没有子控件被控制)
        /// </summary>
        private int IsChildControled = 0;
        /// <summary>
        /// 提示（注释）
        /// </summary>
        private string _hint = "";
        /// <summary>
        /// 显示提示信息的标签
        /// </summary>
        private TextBlock _hintText = new TextBlock();
        /// <summary>
        /// 提示信息的弹出框
        /// </summary>
        private ToolTip _hintTip = new ToolTip();
        /// <summary>
        /// 是否可以显示提示
        /// </summary>
        private bool _isDirTip = false;
        /// <summary>
        /// 上上级父控件
        /// </summary>
        private XObject _parentControl;
        /// <summary>
        /// 公开类型
        /// </summary>
        public enum OpenType
        {
            /// <summary>
            /// 共有类型
            /// </summary>
            Xpublic = 0,
            /// <summary>
            /// 私有类型
            /// </summary>
            Xprivate = 1,
            /// <summary>
            /// 保护类型
            /// </summary>
            Xprotected = 2,
            /// <summary>
            /// 抽象类型
            /// </summary>
            Xabstract = 3
        }
        /// <summary>
        /// 函数的类型
        /// </summary>
        private OpenType _myOpenType = OpenType.Xpublic;
        /// <summary>
        /// 文本改变事件
        /// </summary>
        private TitleTextChange _titleChange;
        #endregion
        #region 读取器
        /// <summary>
        /// 唯一标示符
        /// </summary>
        public int Id
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
            }
        }
        /// <summary>
        /// 表示透明度0-100之前0表示不透明100为完全透明(只接受0-100之间的数值)
        /// </summary>
        public double Alpha
        {
            get
            {            
                return (byte)((this._alpha / 100) * 255);
            }
            set
            {
                if (value > 100)
                    _alpha = 100;
                else if (value < 0)
                    _alpha = 0;
                else
                    _alpha = value;
                this.Opacity = _alpha;
                OnPropertyChanged("Alpha");
                InvalidateVisual();
            }
        }
        /// <summary>
        /// 获取当前对象的一个父控件回调函数
        /// </summary>
        public MouseCallFunction CallBackFunction
        {
            get
            {
                return MCallFc;
            }
            set
            {
                MCallFc = value;
            }
        }
        /// <summary>
        /// 当鼠标移动量超过这个值的时候才能拖动控件这个值越高越能减少控件闪烁
        /// </summary>
        public int MoveDistance = 10;
        /// <summary>
        /// 控件边框颜色
        /// </summary>
        public Color BorderColor
        {
            get { return _borderColor; }
            set
            {
                _borderColor = value;
                //_defultColor = value;
                OnPropertyChanged("BorderColor");
                InvalidateVisual();
            }
        }
        /// <summary>
        /// 设置默认颜色
        /// </summary>
        public Color DefultColor
        {
            get
            {
                return _defultColor;
            }
            set
            {
                _defultColor = value;
                OnPropertyChanged("DefultColor");
            }
        }
        /// <summary>
        /// 圆角弧度
        /// </summary>
        public int Radius
        {
            get { return _radius; }
            set
            {
                _radius = value;
                InvalidateVisual();
                OnPropertyChanged("Radius");
            }
        }
        /// <summary>
        /// 获取或设置控件的边框被选中时候边框宽度。
        /// </summary>
        public int SelectBorderWidth
        {
            get
            {
                return _selectBorderWidth;
            }
            set
            {
                _selectBorderWidth = value;
                InvalidateVisual();
            }
        }
        /// <summary>
        /// 获取或设置控件的边框被选中时候的颜色。
        /// </summary>
        public Color SelectColor
        {
            get
            {
                return _selectColor;
            }
            set
            {
                _selectColor = value;
            }
        }
        /// <summary>
        /// 获取或设置控件的边框宽度。
        /// </summary>
        public int BorderWidth
        {
            get
            {
                return _borderWidth;
            }
            set
            {
                _borderWidth = value;
                _defuleBorderWidth = value;
                InvalidateVisual();
            }
        }
        /// <summary>
        /// 名称读取器
        /// </summary>
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                string old = _title;           
                if (TitleChange != null)
                {
                    ///调用文本改变事件
                    bool isToChange = TitleChange(this, old, value);
                    if(isToChange)
                    {
                        _title = value;
                        OnPropertyChanged("Title");
                    }
                }    
                else
                {
                    _title = value;
                    OnPropertyChanged("Title");
                }           
            }
        }
        /// <summary>
        /// 字体读取器
        /// </summary>
        public string MyFont
        {
            get
            {
                return _myFont;
            }
            set
            {
                _myFont = value;
                OnPropertyChanged("MyFont");
                InvalidateVisual();
            }
        }
        /// <summary>
        /// 字体颜色读取器
        /// </summary>
        public Color FontColor
        {
            get
            {
                return _fontColor;
            }
            set
            {
                _fontColor = value;
                InvalidateVisual();
            }
        }
        /// <summary>
        /// 获得当前控件的状态
        /// </summary>
        public bool SelectState
        {
            get
            {
                return _selectState;
            }
        }
        /// <summary>
        /// 背景颜色读取器
        /// </summary>
        public Color BkColor
        {
            get
            {
                return _bkColor;
            }

            set
            {
                _bkColor = value;
                OnPropertyChanged("BkColor");
                InvalidateVisual();
            }
        }
        /// <summary>
        /// 提示（注释）
        /// </summary>
        public string Hint
        {
            get
            {
                return _hint;
            }

            set
            {
                OnPropertyChanged("Hint");
                _hint = value;
            }
        }
        /// <summary>
        /// 是否允许显示提示信息
        /// </summary>
        public bool IsDirTip
        {
            get
            {
                return _isDirTip;
            }

            set
            {
                _isDirTip = value;
                
            }
        }
        /// <summary>
        /// 函数类型
        /// </summary>
        public OpenType MyOpenType
        {
            get
            {
                return _myOpenType;
            }

            set
            {
                _myOpenType = value;
                OnPropertyChanged("MyOpenType");
            }
        }
        /// <summary>
        /// 父控件
        /// </summary>
        public XObject ParentControl
        {
            get
            {
                return _parentControl;
            }

            set
            {
                _parentControl = value;
            }
        }
        /// <summary>
        /// 旋转变换
        /// </summary>
        public TranslateTransform TransForm
        {
            get
            {
                if(this.RenderTransform as TranslateTransform == null)
                {
                    this.RenderTransform = new TranslateTransform(0,0);
                }
                return this.RenderTransform as TranslateTransform;
            }
            set
            {
                this.RenderTransform = value;
            }
        }
        /// <summary>
        /// 子项
        /// </summary>
        public ItemCollection Children
        {
            get
            {
                return this.Items;
            }
        }
        /// <summary>
        /// 文本改变事件
        /// </summary>
        public TitleTextChange TitleChange
        {
            get
            {
                return _titleChange;
            }

            set
            {
                _titleChange = value;
            }
        }
        #endregion
        #region 依赖项

        #endregion
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public XObject()
        {

        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="title">标题</param>
        public XObject(int id, string title)
        {
            this.Id = id;
            this._title = title;
        }
        /// <summary>
        /// 初始化基本信息
        /// </summary>
        protected virtual void InitBaseInfo()
        {
            FontSize = 25;
        } 
        #endregion
        #region 自定义函数      
        /// <summary>
        /// 设置当前控件为被选中状态
        /// </summary>
        public virtual void SetSelectState()
        {
            _selectState = true;
            _borderColor = _selectColor;
            _borderWidth = _selectBorderWidth;
            InvalidateVisual();
        }
        /// <summary>
        /// 取消当前控件被选中状态
        /// </summary>
        public virtual void CanelSelectState()
        {
            _selectState = false;
            _borderColor = _defultColor;
            _borderWidth = _defuleBorderWidth;
            InvalidateVisual();
        }
        /// <summary>
        /// 自己的键盘按下事件
        /// </summary>
        /// <param name="e">按下的键盘信息</param>
        public virtual void OnMyKeyDown(KeyEventArgs e)
        {

        }
        /// <summary>
        /// 自己的键盘松开事件
        /// </summary>
        /// <param name="e"></param>
        public virtual void OnMyKeyUp(KeyEventArgs e)
        {

        }
        /// <summary>
        /// 获取控件当前尺寸
        /// </summary>
        /// <returns></returns>
        public Size GetSize()
        {
            return new Size(this.RenderSize.Width,this.RenderSize.Height);
        }
        /// <summary>
        /// 向父控件回调事件
        /// </summary>
        /// <param name="ms"></param>
        protected object ToCallBackParent(MouseState ms, XObjectData data = null)
        {
            if(CallBackFunction != null)
            {
                return CallBackFunction(this, ms,data);
            }
            return null;
        }
        /// <summary>
        /// 向父控件回调事件
        /// </summary>
        /// <param name="sender">信息发送者</param>
        /// <param name="ms">消息类型</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        protected object ToCallBackParent(object sender, MouseState ms, XObjectData data = null)
        {
            if (CallBackFunction != null)
            {
                return CallBackFunction(sender, ms, data);
            }
            return null;
        }
        /// <summary>
        /// 发送属性值改变事件(如果是其他属性值的改变则frontValue 则填写要被改变的属性的Title值)
        /// </summary>
        /// <param name="PropertyName">属性值变化的属性名称</param>
        /// <param name="frontValue">属性目前的值</param>
        /// <param name="newValue">属性要改变的值</param>
        /// <returns></returns>
        protected object ToSenderPropertyValueChangeEvent(string PropertyName, object frontValue, object newValue)
        {
            ///声明数据
            XObjectData data = new XObjectData(newValue);
            ///要被修改的属性名称
            data.additional_Information = PropertyName;
            ///要被修改的属性目前的值
            data.state = frontValue;
            ///接受返回数据
            object redata = ToCallBackParent(MouseState.XPropertyValueChange, data);
            ///返回结果
            return redata;
        }
        /// <summary>
        /// 添加一个子控件被控制的消息
        /// </summary>
        protected virtual void AddChildControlMsg(Object sender = null)
        {
            IsChildControled++;
        }
        /// <summary>
        /// 去掉一个子控件被控制的消息
        /// </summary>
        protected virtual void DelChildControlMsg(Object sender = null)
        {
            IsChildControled = IsChildControled -- < 0 ? 0 : IsChildControled;
        }
        /// <summary>
        /// 是否有子控件被控制着(true表示没有子控件被控制，false表示有)
        /// </summary>
        /// <returns></returns>
        protected bool HasChildControled()
        {
            return IsChildControled == 0 ? true : false;
        }
        /// <summary>
        /// 清空所有子控件控制
        /// </summary>
        protected void ClearChildControled()
        {
            IsChildControled = 0;
        }
        /// <summary>
        /// 显示提示信息
        /// </summary>
        protected void DirTipHint()
        {
            _hintText.Text = _hint;
            _hintTip.Content = _hintText;
            _hintTip.Width = _hintText.Width;
            _hintTip.Height = _hintText.Height;
            _hintTip.IsOpen = true;
        }
        /// <summary>
        /// 隐藏提示
        /// </summary>
        protected void HiddenTipHint()
        {
            _hintTip.IsOpen = false;
        }
        /// <summary>
        /// 去检测是否要显示提示信息
        /// </summary>
        /// <returns></returns>
        protected void ToCheckDirTip()
        {           
            if (IsDirTip)
            {
                DirTipHint();               
            }
        }
        /// <summary>
        /// 映射OpenType类型
        /// </summary>
        /// <param name="openString">要映射的字符串</param>
        /// <returns></returns>
        public static OpenType OpenTypeMapping(string openString)
        {
            foreach (OpenType item in Enum.GetValues(typeof(OpenType)))
            {
                if (openString == item.ToString())
                {
                    return item;
                }
            }
            return OpenType.Xpublic;
        }
        /// <summary>
        /// 映射OverrideType
        /// </summary>
        /// <param name="overrideString"></param>
        /// <returns></returns>
        public static OverrideType OverrideTypeMapping(string overrideString)
        {
            foreach (OverrideType item in Enum.GetValues(typeof(OverrideType)))
            {
                if (overrideString == item.ToString())
                {
                    return item;
                }
            }
            return OverrideType.None;
        }
        /// <summary>
        /// 映射MessageOption
        /// </summary>
        /// <param name="messageOption"></param>
        /// <returns></returns>
        public static MessageOption MessageOptionTypeMapping(string messageOption)
        {
            foreach (MessageOption item in Enum.GetValues(typeof(MessageOption)))
            {
                if (messageOption == item.ToString())
                {
                    return item;
                }
            }
            return MessageOption.None;
        }
        /// <summary>
        /// 返回OpenType类型的代码字符串
        /// </summary>
        /// <param name="opentype">OpenType类型</param>
        /// <returns>代码字符串</returns>
        public static string GetOpenTypeCodeString(OpenType opentype)
        {
            string CodeString = "";
            //foreach(OpenType item in Enum.GetValues(typeof(OpenType)))
            //{
            //    if(item == opentype)
            //    {
            //        CodeString = (item)
            //    }
            //}
            CodeString = (opentype.ToString()).Substring(1, opentype.ToString().Length - 1);
            return CodeString;
        }
        /// <summary>
        /// 返回子元素集合
        /// </summary>
        /// <returns></returns>
        protected ItemCollection GetChildren()
        {
            return Children;
        }
        /// <summary>
        /// 自动修改尺寸
        /// </summary>
        public virtual void AutoSize()
        {
            ///通知去修改尺寸
            ToCallBackParent(MouseState.XModifyLayout);
        }
        #region 旋转变换信息函数
        /// <summary>
        /// 获取X的坐标
        /// </summary>
        /// <returns></returns>
        public double GetLeft()
        {
            return TransForm.X;
        }
        /// <summary>
        /// 获取Y的坐标
        /// </summary>
        /// <returns></returns>
        public double GetTop()
        {
            return TransForm.Y;
        }
        /// <summary>
        /// 设置子控件的X坐标
        /// </summary>
        /// <param name="control">子控件</param>
        /// <param name="X">X坐标值</param>
        public void SetLeft(Control control, double X)
        {
            ///判断是否包含此子控件
            if(Children.Contains(control))
            {
                ///判断是否已经实例化此子控件的旋转变换
                if(control.RenderTransform as TranslateTransform == null)
                {
                    ///实例化变换旋转
                    control.RenderTransform = new TranslateTransform();
                }
                ///赋值
                (control.RenderTransform as TranslateTransform).X = X;
            }
        }
        /// <summary>
        /// 设置子控件的Y坐标
        /// </summary>
        /// <param name="control">子控件</param>
        /// <param name="Y">Y新坐标的值</param>
        public void SetTop(Control control, double Y)
        {
            ///判断是否包含此子控件
            if (Children.Contains(control))
            {
                ///判断是否已经实例化此子控件的旋转变换
                if (control.RenderTransform as TranslateTransform == null)
                {
                    ///实例化变换旋转
                    control.RenderTransform = new TranslateTransform();
                }
                ///赋值
                (control.RenderTransform as TranslateTransform).Y = Y;
            }
        }
        /// <summary>
        /// 获取控件当前坐标
        /// </summary>
        /// <returns></returns>
        public Point GetPosition()
        {
            return new Point(TransForm.X, TransForm.Y);
        }
        /// <summary>
        /// 设置移动的偏移量
        /// </summary>
        /// <param name="offectPoint">偏移量的值</param>
        public void SetMoveOffect(Point offectPoint)
        {
            ///添加上偏移量
            this.TransForm.X += offectPoint.X;
            this.TransForm.Y += offectPoint.Y;
        }
        /// <summary>
        /// 设置子控件的偏移量
        /// </summary>
        /// <param name="control">子控件</param>
        /// <param name="x">X的偏移量的值</param>
        /// <param name="y">Y的偏移量的值</param>
        public void SetMoveOffect(Control control, double x, double y)
        {
            if(control.RenderTransform as TranslateTransform == null)
            {
                control.RenderTransform = new TranslateTransform(0, 0);
            }
            TranslateTransform trans = control.RenderTransform as TranslateTransform;
            trans.X += x;
            trans.Y += y;
        }
        /// <summary>
        /// 设置坐标
        /// </summary>
        /// <param name="position">坐标点</param>
        public void SetPosition(Point position)
        {
            ///设置X和Y的值
            TransForm.X = position.X;
            TransForm.Y = position.Y;
        }
        /// <summary>
        /// 设置子控件的坐标
        /// </summary>
        /// <param name="control">子控件</param>
        /// <param name="position">坐标点</param>
        public void SetPosition(Control control, Point position)
        {
            if (control.RenderTransform as TranslateTransform == null)
            {
                control.RenderTransform = new TranslateTransform(0, 0);
            }
            TranslateTransform trans = control.RenderTransform as TranslateTransform;
            trans.X = position.X;
            trans.Y = position.Y;
        }
        /// <summary>
        /// 获取子元素相对本元素的位置
        /// </summary>
        /// <param name="child">要查询的子元素</param>
        public virtual Point GetChildWorldPosition(XObject child)
        {
            return new Point();
        }
        #endregion
        #endregion
        #region 继承函数
        /// <summary>
        /// 鼠标按下
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            ToCallBackParent(MouseState.XDown);
        }
        /// <summary>
        /// 鼠标移动
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (HasChildControled())
            {
                ToCheckDirTip();
            }
            ToCallBackParent(MouseState.XMove);
        }
        /// <summary>
        /// 鼠标抬起
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            ToCallBackParent(MouseState.XUp);
        }
        /// <summary>
        /// 当鼠标滚轮键按下的时候
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            ToCallBackParent(MouseState.XWheel);
        }
        /// <summary>
        /// 鼠标左击按下的时候
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            ToCallBackParent(MouseState.XClick);           
        }
        /// <summary>
        /// 鼠标左击抬起的时候
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
        }
        /// <summary>
        /// 鼠标离开的时候
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(MouseEventArgs e)
        {
            //base.OnMouseLeave(e);
            ///鼠标离开控件的时候隐藏
            HiddenTipHint();
            ToCallBackParent(MouseState.XToStopControl);
        }
        /// <summary>
        /// 鼠标进入的时候
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseEnter(MouseEventArgs e)
        {
            ToCallBackParent(MouseState.XToChildControl);
        }   
        /// <summary>
        /// 设置当前控件状态(仅供内部使用外部无效)
        /// </summary>
        /// <param name="state"></param>
        protected void SetState(bool state)
        {
            _selectState = state;
        }
        #endregion
        #region 子控件事件回调处理
        /// <summary>
        /// 子控件事件回调处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ms"></param>
        protected virtual object ChileEventCallBack(Object sender, MouseState ms, XObjectData data = null)
        {
            ToCallBackParent(sender,ms, data);
            if(ms == MouseState.XClearControl)
            {
                ClearChildControled();
            }
            return null;
        }
        /// <summary>
        /// 传递子控件键盘事件
        /// </summary>
        /// <param name="e"></param>
        protected virtual void ChildKeyEvent(KeyEventArgs e)
        {

        }
        #endregion
        #region 实现的接口函数
        /// <summary>
        /// 通知接口
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// 具体通知事件的实现
        /// </summary>
        /// <param name="propertyName">数据改变的属性名字</param>
        protected internal virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
    /// <summary>
    /// 用于传递的数据
    /// </summary>
    public class XObjectData
    {
        /// <summary>
        /// 数据
        /// </summary>
        public object data;
        /// <summary>
        /// 附加信息
        /// </summary>
        public object additional_Information;
        /// <summary>
        /// 信息状态
        /// </summary>
        public object state;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="data"></param>
        public XObjectData(Object data)
        {
            this.data = data;
        }
    }
    /// <summary>
    /// 写出错误日志
    /// </summary>
    public static class LoggerHelp
    {
        /// <summary>
        /// 写入错误信息
        /// </summary>
        /// <param name="LoggerMsg">错误信息</param>
        public static void WriteLogger(String LoggerMsg)
        {
            LoggerMsg = LoggerMsg + DateTime.Now.ToString() + "\r\n" + "以上信息---------------------------------------------------分割线" + "\r\n\r\n\r\n\r\n";
            String Extension_name = ".txt";
            //if (sfd.ShowDialog(this) == DialogResult.OK)
            //{// 在上边的 sfd.ShowDialog（） 的括号里边一定要加上 this 否则就不会弹出 另存为 的对话框，而弹出的是本类的其他窗口，，这个一定要注意！！！【解释：加了this的sfd.ShowDialog(this)，“另存为”窗口的指针才能被SaveFileDialog的对象调用，若不加thisSaveFileDialog 的对象调用的是本类的其他窗口了，当然不弹出“另存为”窗口。】
            String path = AppDomain.CurrentDomain.BaseDirectory + "LoggerHelp//";
            if (!Directory.Exists(path))
            {
                // Create the directory it does not exist.
                Directory.CreateDirectory(path);
            }
            string fileSavePath = path + DateTime.Now.ToString("d").ToString().Replace(":", "_").Replace("-", "_").Replace("/", "_") + Extension_name;// 获得文件保存的路径；  
            // 创建文件流，然后根据路径创建文件；           
            byte[] arrMsg = System.Text.Encoding.UTF8.GetBytes(LoggerMsg);
            if (CheckFile(fileSavePath))
            {
                StreamWriter sw = File.AppendText(fileSavePath);
                sw.Write(LoggerMsg);
                sw.Close();
            }
            else
            {
                using (FileStream fs = new FileStream(fileSavePath, FileMode.Create))
                {
                    fs.Write(arrMsg, 0, arrMsg.Length);
                }
            }
        }
        /// <summary>
        /// 检查文件是否存在
        /// </summary>
        /// <returns></returns>
        public static bool CheckFile(String path)
        {
            if (!File.Exists(path))
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 写入内容到文件
        /// </summary>
        /// <param name="message">要写入的内容</param>
        /// <param name="FileName">文件的名称包括扩展名</param>
        /// <param name="outpath">要写入的文件的输出目录</param>
        /// <returns>返回操作消息</returns>
        public static string WriteMessageToFile(string message,string FileName, string outpath)
        {
            if (!Directory.Exists(outpath))
            {
                // 创建输出目录
                Directory.CreateDirectory(outpath);
            }
            try
            {
                string fileSavePath = outpath + FileName;// 获得文件保存的路径；  
                // 创建文件流，然后根据路径创建文件；           
                byte[] arrMsg = System.Text.Encoding.UTF8.GetBytes(message);
                ///写入内容
                File.WriteAllText(fileSavePath, message, System.Text.Encoding.UTF8);
                return "成功写入文件";
            }catch(Exception ex)
            {
                WriteLogger(ex.ToString());
                return ex.Message;
            }
        }
    }
    ///工具类
    public static class ToolHelp
    {
        /// <summary>
        /// 设置绑定高度
        /// </summary>
        /// <param name="bind">要绑定的对象</param>
        /// <param name="targetbind">要绑定到的对象</param>
        public static void SetBindingHeight(FrameworkElement bind, FrameworkElement targetbind)
        {
            ///创建绑定对象
            Binding widBinding = new Binding();
            ///设置绑定源
            widBinding.Source = targetbind;
            ///设置要绑定的对象的属性
            widBinding.Path = new PropertyPath("Height");
            ///绑定该对象的属性
            bind.SetBinding(FrameworkElement.HeightProperty, widBinding);
        }
        /// <summary>
        /// 设置绑定高度
        /// </summary>
        /// <param name="bind">要绑定的对象</param>
        /// <param name="targetbind">要绑定到的对象</param>
        public static void SetBindingWidth(FrameworkElement bind, FrameworkElement targetbind)
        {
            ///创建绑定对象
            Binding widBinding = new Binding();
            ///设置绑定源
            widBinding.Source = targetbind;
            ///设置要绑定的对象的属性
            widBinding.Path = new PropertyPath("Width");
            ///绑定该对象的属性
            bind.SetBinding(FrameworkElement.WidthProperty, widBinding);
        }
        /// <summary>
        /// 设置将宽度绑定到高度
        /// </summary>
        /// <param name="bind">要绑定的对象</param>
        /// <param name="targetbind">要绑定到的对象</param>
        public static void SetBindingWidthToHeigth(FrameworkElement bind, FrameworkElement targetbind)
        {
            ///创建绑定对象
            Binding widBinding = new Binding();
            ///设置绑定源
            widBinding.Source = targetbind;
            ///设置要绑定的对象的属性
            widBinding.Path = new PropertyPath("Height");
            ///绑定该对象的属性
            bind.SetBinding(FrameworkElement.WidthProperty, widBinding);
        }
        /// <summary>
        /// 用于计算2个点的距离
        /// </summary>
        /// <param name="StartP"></param>
        /// <param name="EndP"></param>
        public static double DisPoint(Point StartP, Point EndP)
        {
            double dis = 0;
            dis = Math.Sqrt(Math.Pow(Math.Abs(StartP.X - EndP.X), 2) + Math.Pow(Math.Abs(StartP.Y - EndP.Y), 2));
            return dis;
        }
        /// <summary>
        /// 根据2个点计算返回Size
        /// </summary>
        /// <param name="StartP"></param>
        /// <param name="EndP"></param>
        /// <returns></returns>
        public static Size PointToSize(Point StartP, Point EndP)
        {
            try
            {
                return new Size(Math.Abs(StartP.X - EndP.X), Math.Abs(StartP.Y - EndP.Y));
            }
            catch (Exception ex)
            {
                LoggerHelp.WriteLogger(ex.ToString());
                return new Size(0, 0);
            }
        }
        /// <summary>
        /// 计算2个点想减
        /// </summary>
        /// <param name="StartP">第一个点</param>
        /// <param name="EndP">第二个点</param>
        /// <returns>返回点</returns>
        public static Point PointSecPoint(Point StartP, Point EndP)
        {
            return new Point(EndP.X - StartP.X, EndP.Y - StartP.Y);
        }
        /// <summary>
        /// 根据2个矩形获取2个矩形最大的限度的矩形
        /// </summary>
        /// <param name="rct1">矩形1</param>
        /// <param name="rct2">矩形2</param>
        /// <returns>返回一个矩形</returns>
        public static Rect GetOutRectangle(Rect rct1, Rect rct2)
        {
            try
            {
                Rect rct = new Rect();
                rct.X = rct1.X < rct2.X ? rct1.X : rct2.X;
                rct.Y = rct1.Y < rct2.Y ? rct1.Y : rct2.Y;
                rct.Width = rct1.Right > rct2.Right ? rct1.Right - rct.X : rct2.Right - rct.X;
                rct.Height = rct1.Bottom > rct2.Bottom ? rct1.Bottom - rct.Y : rct2.Bottom - rct.Y;
                return rct;
            }
            catch (Exception ex)
            {
                LoggerHelp.WriteLogger(ex.ToString());
                return new Rect();
            }
        }
        /// <summary>
        /// 根据2个点获取一个矩形
        /// </summary>
        /// <param name="left">点1</param>
        /// <param name="right">点2</param>
        /// <returns></returns>
        public static Rect PointToRectangle(Point left, Point right)
        {
            try
            {
                Point RecLeft = new Point(left.X < right.X ? left.X : right.X, left.Y < right.Y ? left.Y : right.Y);
                return new Rect(RecLeft, PointToSize(left, right));
            }
            catch (Exception ex)
            {
                LoggerHelp.WriteLogger(ex.ToString());
                return new Rect();
            }
        }
    }
}
