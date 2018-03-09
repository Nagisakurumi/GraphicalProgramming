using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using MyTabPage;
using MyXObject;
using MyCodeBox;
using MyXAribute;
using MyXTreeView;
using System.Windows.Controls.Primitives;
using System.Collections.ObjectModel;
using MyXToolsPanel;
using System.Windows.Controls;

namespace MyPicTabPage
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:MyPicTabPage"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:MyPicTabPage;assembly=MyPicTabPage"
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
    public class PicTabPage : TabPage
    {
        static PicTabPage()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PicTabPage), new FrameworkPropertyMetadata(typeof(PicTabPage)));
        }
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">控件的唯一标示符</param>
        /// <param name="mcf">回调函数</param>
        /// <param name="name">控件名字</param>
        public PicTabPage(int id, MouseCallFunction mcf,string Title)
        {
            this.Id = id;
            this.CallBackFunction = mcf;
            this.Title = Title;
            InitBaseInfo();
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public PicTabPage()
        {
            InitBaseInfo();
        }
        /// <summary>
        /// 初始化基本信息
        /// </summary>
        protected override void InitBaseInfo()
        {
            base.InitBaseInfo();
            this.Background = new SolidColorBrush(Color.FromArgb(200, 114, 109, 109));
            this.MoveDistance = 10;
            ///清空被选中的控件
            SelectIDList.Clear();
            ///初始化弹出框
            InitPopup();
            ///设置允许拖拽
            this.AllowDrop = true;
            ///设置内容面板
            MyPanel.Width = _maxSize;
            MyPanel.Height = _maxSize;
            ///设置线条颜色
            MyPanel.BkLineColor = _bkLineColor;
            ///设置背景颜色
            MyPanel.Background = this.Background;
            this.GetChildren().Add(MyPanel);
            ///设置起始位置
            LTransform.X = - CenterPoint.X + 200;
            LTransform.Y = - _centerPoint.Y + 200;
        }
        #endregion
        #region 定义属性
        /// <summary>
        /// 表示清除所有子控件的选中状态
        /// </summary>
        private static int CancelAllSelected = -1;
        /// <summary>
        /// 一个脚本最多的语句块数量
        /// </summary>
        private static int TheAllControlNum = 2000000;
        /// <summary>
        /// 最多的贝塞尔曲线数量
        /// </summary>
        private static int TheAllBezierNum = 20000000;
        /// <summary>
        /// 当前用于创建语句块的ID
        /// </summary>
        private int CurrentID = 0;
        /// <summary>
        /// 被选中的控件的id
        /// </summary>
        private List<int> SelectIDList = new List<int>();
        /// <summary>
        /// 语句块的存储
        /// </summary>
        private Dictionary<int, XObject> ListCodeBox = new Dictionary<int, XObject>();
        /// <summary>
        /// 背景线条的颜色
        /// </summary>
        protected Color _bkLineColor = Color.FromArgb(255,255, 255, 255);
        /// <summary>
        /// 贝塞尔曲线存储
        /// </summary>
        private Dictionary<int, BezierLine> ListBezierLine = new Dictionary<int, BezierLine>();
        /// <summary>
        /// 存储函数的集合
        /// </summary>
        private ObservableCollection<PicFunctionTabPage> _listFunction = new ObservableCollection<PicFunctionTabPage>();
        /// <summary>
        /// 属性列表
        /// </summary>
        private ObservableCollection< XAribute> _listXAributes = new ObservableCollection<XAribute>();
        /// <summary>
        /// 正在被控制的对象
        /// </summary>
        private XAribute _controlXAtribute = null;
        /// <summary>
        /// alt键是否被按着
        /// </summary>
        private bool _isPressingAlt = false;
        /// <summary>
        /// 弹出框(树形数据)
        /// </summary>
        private Popup _pop = new Popup();
        /// <summary>
        /// 属性数据方式选择弹出框
        /// </summary>
        private Popup _selectPop = new Popup();
        /// <summary>
        /// 弹出框里面的内容
        /// </summary>
        private XMTreeView _popContentCode;
        /// <summary>
        /// 属性方式选择弹出框
        /// </summary>
        private XAributeDropSelect _popXaributeDrop = new XAributeDropSelect();
        /// <summary>
        /// 创建 点位置
        /// </summary>
        private Point _createBoxPosition = new Point();
        /// <summary>
        /// 消息更新回调函数
        /// </summary>
        private MouseCallFunction _messageUpdateCall;
        /// <summary>
        /// 内容面板
        /// </summary>
        private MyCanvas _myPanel = new MyCanvas();
        /// <summary>
        /// 设置内容面板的最大尺寸（宽和高）
        /// </summary>
        private static int _maxSize = 3000;
        /// <summary>
        /// 内容面板中心点的位置
        /// </summary>
        private static Point _centerPoint = new Point(_maxSize / 2, _maxSize/ 2 );
        /// <summary>
        /// 缩放尺寸
        /// </summary>
        private double _scaleValue = 1;
        #endregion
        #region 读取器
        /// <summary>
        /// 背景颜色线条的读取器
        /// </summary>
        public Color BkLineColor
        {
            get
            {
                return _bkLineColor;
            }

            set
            {
                _bkLineColor = value;
            }
        }
        /// <summary>
        /// 代码块集合
        /// </summary>
        public Dictionary<int, XObject> ListCodeBoxChild
        {
            get
            {
                return ListCodeBox;
            }        
        }
        /// <summary>
        /// 贝塞尔曲线对象集合
        /// </summary>
        public Dictionary<int, BezierLine> GetBezierLines
        {
            get
            {
                return ListBezierLine;
            }
        }
        /// <summary>
        /// 用于绑定的数据
        /// </summary>
        public ObservableCollection<PicFunctionTabPage> ListFunction
        {
            get
            {
                return _listFunction;
            }

            set
            {
                _listFunction = value;
            }
        }
        /// <summary>
        /// 属性列表
        /// </summary>
        public ObservableCollection<XAribute> ListXAributes
        {
            get
            {
                return _listXAributes;
            }
        }
        /// <summary>
        /// 弹出的内容数据对象
        /// </summary>
        public XMTreeView PopContentCode
        {
            get
            {
                return _popContentCode;
            }

            set
            {
                _popContentCode = value;
            }
        }
        /// <summary>
        /// 消息更新回调函数
        /// </summary>
        public MouseCallFunction MessageUpdateCall
        {
            get
            {
                return _messageUpdateCall;
            }

            set
            {
                _messageUpdateCall = value;
            }
        }
        /// <summary>
        /// 子项集合
        /// </summary>
        public new UIElementCollection Children
        {
            get
            {
                return MyPanel.Children;
            }
        }
        /// <summary>
        /// 内容面板
        /// </summary>
        public MyCanvas MyPanel
        {
            get
            {
                return _myPanel;
            }
        }
        /// <summary>
        /// 缩放变换
        /// </summary>
        public ScaleTransform STransForm
        {
            get
            {
                ///先判断是否为空
                if(MyPanel.RenderTransform as TransformGroup == null)
                {
                    MyPanel.RenderTransform = new TransformGroup();
                    ScaleTransform scale = new ScaleTransform();
                    TranslateTransform translater = new TranslateTransform();
                    (MyPanel.RenderTransform as TransformGroup).Children.Add(scale);
                    (MyPanel.RenderTransform as TransformGroup).Children.Add(translater);
                    return scale;
                }
                ScaleTransform rescale = new ScaleTransform();
                ///寻找缩放变换
                foreach(Transform trans in (MyPanel.RenderTransform as TransformGroup).Children)
                {
                    if(trans as ScaleTransform != null)
                    {
                        rescale = trans as ScaleTransform;
                    }
                }
                return rescale;
            }
        }
        /// <summary>
        /// 位移变换
        /// </summary>
        public TranslateTransform LTransform
        {
            get
            {
                ///先判断是否为空
                if (MyPanel.RenderTransform as TransformGroup == null)
                {
                    MyPanel.RenderTransform = new TransformGroup();
                    ScaleTransform scale = new ScaleTransform();
                    TranslateTransform translater = new TranslateTransform();
                    (MyPanel.RenderTransform as TransformGroup).Children.Add(scale);
                    (MyPanel.RenderTransform as TransformGroup).Children.Add(translater);
                    return translater;
                }
                TranslateTransform rescale = new TranslateTransform();
                ///寻找缩放变换
                foreach (Transform trans in (MyPanel.RenderTransform as TransformGroup).Children)
                {
                    if (trans as TranslateTransform != null)
                    {
                        rescale = trans as TranslateTransform;
                    }
                }
                return rescale;
            }
        }
        /// <summary>
        /// 内容面板的中心点
        /// </summary>
        public static Point CenterPoint
        {
            get
            {
                return _centerPoint;
            }
        }
        /// <summary>
        /// 缩放尺寸的值
        /// </summary>
        public double ScaleValue
        {
            get
            {
                return _scaleValue;
            }
            set
            {
                _scaleValue = value;
            }
        }
        #endregion
        #region 继承函数
        /// <summary>
        /// 界面渲染绘制函数
        /// </summary>
        /// <param name="drawingContext"></param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            //#region 用于绘制 背景
            ////Brush MyBrush = new LinearGradientBrush(Color.FromArgb(200,255,255,255),Color.FromArgb(150,0,0,0),new Point(0,0),new Point(this.RenderSize.Width,RenderSize.Height));
            //Pen MyPen = new Pen(this.Background, 2);
            //drawingContext.DrawRectangle(this.Background, MyPen, new Rect(0, 0, RenderSize.Width, RenderSize.Height));
            ////drawingContext.DrawRoundedRectangle(MyBrush, MyPen, new Rect(0, 0, RenderSize.Width, RenderSize.Height), 10, 10) 
            //#endregion;
            //#region 绘制背景上的线条
            ////Pen MyLinePen = new Pen(new RadialGradientBrush(Color.FromArgb(255, 255, 255, 255), Color.FromArgb(255, 0, 0, 0)),2);
            //Pen MyLinePen = new Pen(new SolidColorBrush(_bkLineColor), 1);
            //double curLinei = RenderSize.Width / 100;
            //if (curLinei < 40)
            //    curLinei = 40;
            //for (double i = 0; i < RenderSize.Width; i += curLinei)
            //{
            //    drawingContext.DrawLine(MyLinePen, new Point(i, 0), new Point(i, RenderSize.Height));
            //}
            //for (double i = 0; i < RenderSize.Height; i += curLinei)
            //{
            //    drawingContext.DrawLine(MyLinePen, new Point(0, i), new Point(RenderSize.Width, i));
            //}
            //#endregion
            //#region 绘制框选
            //if(isToSelect)
            //{
            //    Pen RecPen = new Pen(new SolidColorBrush(Color.FromArgb(255, 255, 211, 08)), 2);
            //    drawingContext.DrawRectangle(new SolidColorBrush(Color.FromArgb(0, 0, 0, 0)), RecPen, inveateRec);
            //}
            //#endregion
        }
        #region 画贝塞尔曲线
        /// <summary>
        /// 是否开始画贝塞尔曲线
        /// </summary>
        private bool isDrawBezier = false;
        /// <summary>
        /// 是否悬停在属性子控件上面
        /// </summary>
        private bool isOverAribute = false;
        /// <summary>
        /// 贝塞尔曲线对象
        /// </summary>
        private BezierLine MyBezierLine;
        /// <summary>
        /// 贝塞尔曲线连接的第一个属性
        /// </summary>
        private XAribute FirstXa;
        /// <summary>
        /// 贝塞尔曲线连接的第二个曲线
        /// </summary>
        private XAribute SecondXa;
        #endregion
        /// <summary>
        /// 鼠标按下的时候
        /// </summary>
        /// <param name="e">鼠标信息</param>
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            ///按下的时候获取键盘焦点
            Focus();           
            if (HasChildControled())
            {
                ///捕获鼠标焦点
                this.CaptureMouse();
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    MyPanel.MouseStartPoint = e.GetPosition(MyPanel);
                    MyPanel.MouseEndPoint = e.GetPosition(MyPanel);
                }
            }
        }
        /// <summary>
        /// 当鼠标移动的时候
        /// </summary>
        /// <param name="e">鼠标信息</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            #region 选择框的拉取
            ///查看是否具备刷新框选的资格
            if (MyPanel.IsToSelect && ToolHelp.DisPoint(MyPanel.MouseStartPoint, e.GetPosition(MyPanel)) > MoveDistance)
            {
                MyPanel.MouseEndPoint = e.GetPosition(MyPanel);
                CaultRectangle(MyPanel.InveateRec);
            }
            #endregion
            #region 画贝塞尔曲线
            else if (isDrawBezier && !isOverAribute && MyBezierLine != null)
            {
                MyBezierLine.SetBezierLine(e.GetPosition(MyPanel),FirstXa.GetOrXPositonStyle());
            }
            #endregion
            #region 自身移动
            ///如果焦点在本控件并且准备好移动自身
            if(HasChildControled() && IsStartMove)
            {
                ///获取新的点
                EndDragPoint = e.GetPosition(MyPanel);
                if(ToolHelp.DisPoint(StartDragPoint, EndDragPoint) >= MoveDistance)
                {
                    HorMove(StartDragPoint.X - EndDragPoint.X);
                    VecMove(StartDragPoint.Y - EndDragPoint.Y);
                    IsMove = true;
                    StartDragPoint.X = EndDragPoint.X;
                    StartDragPoint.Y = EndDragPoint.Y;
                }
            }
            #endregion
        }
        /// <summary>
        /// 当鼠标离开的时候
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(MouseEventArgs e)
        {
            #region 对贝塞尔曲线的处理
            if (MyBezierLine != null)
            {
                ///删除失败的贝塞尔曲线
                DelBezierLine(MyBezierLine);
                #region 初始化信息
                FirstXa = null;
                SecondXa = null;
                isOverAribute = false;
                isDrawBezier = false;
                SecondXa = null;
                #endregion              
            } 
            #endregion
            #region 自身移动初始化
            IsMove = false;
            IsStartMove = false;
            #endregion
            #region 取消框选
            MyPanel.IsToSelect = false;
            #endregion
            #region 焦点控制
            ///释放鼠标焦点
            this.ReleaseMouseCapture();
            #endregion
        }
        /// <summary>
        /// 鼠标抬起的时候
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            ///释放鼠标焦点
            this.ReleaseMouseCapture();
            #region 取消框选
            MyPanel.IsToSelect = false;
            #endregion
            #region 对贝塞尔曲线的操作
            ///成功连接
            if (isDrawBezier && isOverAribute && SecondXa != null && SecondXa.IsCanLin(FirstXa))
            {
                #region 将属性和连线的关系赋值
                FirstXa.AddBezierLine(MyBezierLine);
                MyBezierLine.StartPoint.LinkAribute = FirstXa;
                SecondXa.AddBezierLine(MyBezierLine);
                MyBezierLine.EndPoint.LinkAribute = SecondXa;
                MyBezierLine.SetBezierLine(SecondXa.GetWorldPosition(), SecondXa.SelectPositionStyle);
                #endregion
            }
            ///失败连接
            else if (isDrawBezier)
            {
                ///删除失败的贝塞尔曲线
                DelBezierLine(MyBezierLine);       
            }
            ///清空贝塞尔曲线对象,防止对此的再次操作
            if (MyBezierLine != null)
            {
                MyBezierLine = null;
                #region 初始化信息
                FirstXa = null;
                SecondXa = null;
                isOverAribute = false;
                isDrawBezier = false;
                SecondXa = null;
                #endregion
            }
            #endregion
        }
        #region 对自身的拖动
        /// <summary>
        /// 拖动起始点
        /// </summary>
        Point StartDragPoint = new Point();
        /// <summary>
        /// 拖动结束点
        /// </summary>
        Point EndDragPoint = new Point();
        /// <summary>
        /// 对自身的拖动
        /// </summary>
        bool IsMove = false;
        /// <summary>
        /// 是否已经准备移动
        /// </summary>
        bool IsStartMove = false;
        #endregion
        /// <summary>
        /// 鼠标右键按下的时候
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseRightButtonDown(e);
            if (HasChildControled())
            {
                ///设置准备好可以移动自身
                IsStartMove = true;
                StartDragPoint = e.GetPosition(MyPanel);  
            }
        }
        /// <summary>
        /// 右击弹起来的时候
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseRightButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseRightButtonUp(e);
            if (HasChildControled())
            {
                ///如果已经被移动过则不再触发代码创建框
                if (!IsMove)
                {
                    ///显示弹出框
                    DirPopup();
                    _createBoxPosition = e.GetPosition(MyPanel);
                }
                else
                {
                    IsMove = false;
                    IsStartMove = false;
                }
            }
        }
        /// <summary>
        /// 鼠标左击按下的时候
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            //MessageBox.Show(e.GetPosition(MyPanel).ToString());
            if(HasChildControled())
            {
                SetChildControlState(-1);
            }
        }
        /// <summary>
        /// 键盘按下的时候
        /// </summary>
        /// <param name="e">消息内容</param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            ///处理键盘事件
            switch (e.Key)
            {
                case Key.Delete:
                    DelSelectControl();
                    break;
                case Key.System:
                    _isPressingAlt = true;
                    break;
                case Key.RightAlt:
                    //_isPressingAlt = true;
                    break;
            }
        }
        /// <summary>
        /// 键盘抬起的时候
        /// </summary>
        /// <param name="e">键盘消息</param>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            ///处理键盘事件
            switch (e.Key)
            {
                case Key.System:
                    _isPressingAlt = false;
                    break;
                case Key.RightAlt:
                    //_isPressingAlt = true;
                    break;
            }
        }
        /// <summary>
        /// 键盘松开
        /// </summary>
        /// <param name="e"></param>
        public override void OnMyKeyUp(KeyEventArgs e)
        {
            base.OnMyKeyUp(e);
            switch (e.Key)
            {
                case Key.System:
                    _isPressingAlt = false;
                    break;
                case Key.RightAlt:
                    //_isPressingAlt = false;
                    break;
            }
        }
        /// <summary>
        /// 当添加一个子控件控制时候
        /// </summary>
        protected override void AddChildControlMsg(Object sender)
        {
            base.AddChildControlMsg();
            if (sender.GetType().Name == "XAribute")
            {
                _controlXAtribute = (XAribute)sender;               
            }
        }
        /// <summary>
        /// 当一个子控件退出控制的时候
        /// </summary>
        /// <param name="sender"></param>
        protected override void DelChildControlMsg(object sender = null)
        {
            base.DelChildControlMsg(sender);
            _controlXAtribute = null;
        }
        /// <summary>
        /// 当其他控件拖动过来时候触发
        /// </summary>
        /// <param name="e">数据源</param>
        protected override void OnDrop(DragEventArgs e)
        {
            base.OnDrop(e);
            ///获取数据
            IDataObject data = new DataObject();
            data = e.Data;
            object obj = data.GetData(typeof(XAribute));
            if (obj != null)
            {
                //XAribute bute = obj as XAribute;
                ///显示调用属性的方式
                DirXAributeSelectPopup(obj);
                _createBoxPosition = e.GetPosition(MyPanel);
            }
        }
        /// <summary>
        /// 滚轮滚动的时候
        /// </summary>
        /// <param name="e">事件信息</param>
        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            if (e.Delta == 120)
            {
                AddScale();
            }
            else if (e.Delta == -120)
            {
                DesScale();
            }
        }
        #endregion
        #region 自定义函数
        /// <summary>
        /// 放大
        /// </summary>
        protected void AddScale()
        {
            if(ScaleValue > 0.5)
            {
                ScaleValue -= 0.1;
            }
            STransForm.ScaleX = ScaleValue;
            STransForm.ScaleY = ScaleValue;
        }
        /// <summary>
        /// 缩小
        /// </summary>
        protected void DesScale()
        {
            if (ScaleValue < 2)
            {
                ScaleValue += 0.1;
            }
            STransForm.ScaleX = ScaleValue;
            STransForm.ScaleY = ScaleValue;
        }
        /// <summary>
        /// 水平移动（value 为负则向左移动，为正则向右移动）
        /// </summary>
        /// <param name="value">移动向量</param>
        protected void HorMove(double value)
        {
            double xvalue = LTransform.X;

            if (xvalue > 0)
            {
                LTransform.X = 0;
                return;
            }
            else if (xvalue < MyPanel.Width - this.Width)
            {
                LTransform.X = MyPanel.Width - this.Width;
                return;
            }         
            else
            {
                LTransform.X = xvalue + value / 2;
            }
        }
        /// <summary>
        /// 垂直移动(value为正则向下移动，为负则向上移动)
        /// </summary>
        /// <param name="value">移动向量</param>
        protected void VecMove(double value)
        {
            double yvalue = LTransform.Y;
            if (yvalue > 0)
            {
                LTransform.Y = 0;
                return;
            }
            else if(yvalue < MyPanel.Height - this.Height)
            {
                LTransform.Y = MyPanel.Height - this.Height;
                return;
            }
            else
            {
                LTransform.Y = yvalue + value / 2;
            }
        }
        /// <summary>
        /// 创建一个代码块
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        public CodeBox CreateXCodeBox(string name, Point position,CodeBox.XAType CodeboxType = CodeBox.XAType.XFunction)
        {
            int id = CreadCodeBoxID();            
            CodeBox xa = new CodeBox(id,this, name, ChileEventCallBack, CodeboxType);
            xa.SetPosition(position);
            //xa.AddAttribute(XRadioButton.XAttributeType.XEnter, XRadioButton.XPositonStyle.Left);
            ListCodeBoxChild.Add(id, xa);
            this.Children.Add(xa);
            return xa;
        }
        /// <summary>
        /// 从文件中加载一个代码块
        /// </summary>
        /// <param name="name"></param>
        /// <param name="position"></param>
        /// <param name="id"></param>
        /// <param name="CodeboxType"></param>
        /// <returns></returns>
        public CodeBox LoadXCodeBox(string name, Point position, int id, CodeBox.XAType CodeboxType = CodeBox.XAType.XFunction)
        {
            CodeBox box = new CodeBox(id, this, name, ChileEventCallBack, CodeboxType);
            box.SetPosition(position);
            //xa.AddAttribute(XRadioButton.XAttributeType.XEnter, XRadioButton.XPositonStyle.Left);
            ListCodeBoxChild.Add(id, box);
            this.Children.Add(box);
            return box;
        }
        /// <summary>
        /// 创建一个可以利用的id只使用CodeBox
        /// </summary>
        private int CreadCodeBoxID()
        {
            int id = CurrentID > 2000000 ? -1 : CurrentID++ % TheAllControlNum;
            ///循环生成不重复的主键
            while (ListCodeBoxChild.ContainsKey(id))
            {
                id = CurrentID > 2000000 ? -1 : CurrentID++ % TheAllControlNum;
            }
            return id;
        }
        /// <summary>
        /// 创建一个可以利用的id只使用BezierLine
        /// </summary>
        /// <returns></returns>
        protected int CreateBezierID()
        {
            int id = CurrentID > 20000 ? -1 : CurrentID++ % TheAllBezierNum;
            ///循环生成不重复的主键
            while (ListBezierLine.ContainsKey(id))
            {
                id = CurrentID > 20000 ? -1 : CurrentID++ % TheAllBezierNum;
            }
            return id;
        }
        /// <summary>
        /// 删除被选中的控件
        /// </summary>
        protected void DelSelectControl()
        {
            foreach (int childid in SelectIDList)
            {
                   DelControl(childid);
            }
            SelectIDList.Clear();
        }
        /// <summary>
        /// 根据id删除一个内置的语句块
        /// </summary>
        /// <param name="id"></param>
        private void DelControl(int id)
        {
            try
            {
                CodeBox xb = ListCodeBoxChild[id] as CodeBox;
                ///不能删除主函数入口
                if (xb.CodeBoxType != CodeBox.XAType.XMain && xb.CodeBoxType != CodeBox.XAType.XFunctionEnter)
                {
                    ///删除代码块
                    xb.DelCodeBox();
                    ListCodeBoxChild.Remove(id);
                    Children.Remove(xb);
                }
            }
            catch (Exception ex)
            {
                LoggerHelp.WriteLogger(ex.ToString());
            }
        }
        /// <summary>
        /// 设置子控件的状态,当只有一个子控件被选中的时候 当id = CancelAllSelected表示清楚所有选中状态
        /// </summary>
        private void SetChildControlState(int id = -1)
        {
            if (SelectIDList.Count == 1 && SelectIDList[0] == id)
            {
                return;
            }
            for (int i = 0; i < SelectIDList.Count; i++)
            {
                //try
                //{
                    ListCodeBoxChild[SelectIDList[i]].CanelSelectState();
                //}
                //catch (Exception ex)
                //{

                //}
            }
            SelectIDList.Clear();
            if (id != CancelAllSelected)
                SetChildState(id);
        }
        /// <summary>
        /// 设置子控件的状态单个
        /// </summary>
        private void SetChildState(int id)
        {
            lock (this)
            {
                //try
                //{
                ListCodeBoxChild[id].SetSelectState();
                if (!SelectIDList.Contains(id))
                {
                    SelectIDList.Add(id);
                }
                    //}
                //catch (Exception ex)
                //{
                //}
            }
        }
        /// <summary>
        /// 检测区域内的控件被设置为选中状态
        /// </summary>
        /// <param name="rc"></param>
        private void CaultRectangle(Rect rc)
        {
            foreach (int key in ListCodeBoxChild.Keys)
            {
                if (rc.Contains(new Rect(ListCodeBoxChild[key].GetPosition(), ListCodeBoxChild[key].GetSize())))
                {
                    SetChildState(key);
                }
            }
        }
        /// <summary>
        /// 添加一个贝塞尔曲线
        /// </summary>
        /// <param name="bz"></param>
        protected void AddBezierLine(BezierLine bz)
        {
            GetBezierLines.Add(bz.Id, bz);
            this.Children.Add(bz.Bezier);
        }
        /// <summary>
        /// 删除一条贝塞尔曲线
        /// </summary>
        /// <param name="bz"></param>
        private void DelBezierLine(BezierLine bz)
        {
            if (GetBezierLines.ContainsValue(bz))
            {
                GetBezierLines.Remove(bz.Id);
                this.Children.Remove(bz.Bezier);
            }
        }
        /// <summary>
        /// 从文件读取的时候添加一个贝塞尔曲线
        /// </summary>
        /// <param name="bz">贝塞尔曲线对象</param>
        public bool ReadCreateBezierLine(BezierLine bz, int startID,int endID,int startfatherID, int endfatherID)
        {
            XAribute startPoint = GetXAributeByID(startID,startfatherID);
            XAribute endPoint = GetXAributeByID(endID,endfatherID);
            if(startPoint != null && endPoint != null)
            {
                startPoint.AddBezierLine(bz);
                endPoint.AddBezierLine(bz);
                ///设置贝塞尔曲线信息
                bz.DirectSetBezierLineTwoPoint(startPoint,endPoint);
                ///将贝塞尔曲线添加入显示
                AddBezierLine(bz);                            
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 从文件加载函数
        /// </summary>
        /// <param name="Title">函数名</param>
        /// <param name="ID">ID</param>
        public PicFunctionTabPage LoadFunctionPage(string Title, int ID, OpenType opentype, OverrideType overridetype)
        {
            foreach(PicFunctionTabPage fun in ListFunction)
            {
                if (fun.Title == Title)
                    return null;
            }
            PicFunctionTabPage function = new PicFunctionTabPage(ID, this.CallBackFunction, Title);
            function.MyOpenType = opentype;
            function.MyOverride = overridetype;
            ListFunction.Add(function);
            return function;
        }
        /// <summary>
        /// 从文件加载一个属性
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
        public XAribute LoadXAribute(XAribute.XAttributeType xt, XAribute.XAttributeSpec xs, XAribute.XPositonStyle xp, string paramterName, XAribute.CanLinkType clt, string tiptext, string LastExName, int id, OpenType opentype)
        {
            XAribute xb = new XAribute(id, paramterName, xt, xs, xp, this.ChileEventCallBack, clt, LastExName);
            xb.Hint = tiptext;
            xb.MyOpenType = opentype;
            ///绑定标题改变事件
            xb.TitleChange += (e, o, v) =>
            {
                ///修改Title
                return ModifyPropertyTitle(o, v);
            };
            xb.ExNameChange += (s, o, v) =>
            {
                ///修改ExName
                this.ModifyPropetyExName((s as XAribute).Title, v);
            };
            ///检测是否存在
            if (!ListXAributes.Contains(xb))
            {
                ListXAributes.Add(xb);
                return xb;
            }
            return null;
        }
        /// <summary>
        /// 根据ID获取代码块中的XAribute
        /// </summary>
        /// <param name="ID">XAribute的ID</param>
        /// <returns>寻找到的XAribute没找到则为null</returns>
        protected XAribute GetXAributeByID(int ID,int fatherID)
        {
            foreach (CodeBox box in ListCodeBoxChild.Values)
            {
                if (box.Id == fatherID)
                {
                    XAribute rebute = box.GetXAributeByID(ID);
                    if (rebute != null)
                    {
                        return rebute;
                    }
                }
            }
            return null;
        }       
        /// <summary>
        /// 移动所有被选择的控件
        /// </summary>
        /// <param name="toMovePoint">移动的向量</param>
        protected void MoveSelectControl(Point toMovePoint, CodeBox cd)
        {
            foreach (int key in ListCodeBoxChild.Keys)
            {
                if (ListCodeBoxChild[key].SelectState)
                {
                    Type sendtype = ListCodeBoxChild[key].GetType();
                    if (sendtype.Name == "CodeBox" && ((CodeBox)ListCodeBoxChild[key]) != cd)
                    {
                        ((CodeBox)ListCodeBoxChild[key]).SetVectorMove(toMovePoint);
                    }
                }
            }
        }
        /// <summary>
        /// 初始化弹出框
        /// </summary>
        protected void InitPopup()
        {
            ///树状数据弹出框的初始化
            PopContentCode = new XMTreeView(this.ChileEventCallBack);
            _pop.AllowsTransparency = true;
            _pop.PopupAnimation = PopupAnimation.Fade;
            _pop.StaysOpen = false;
            _pop.Placement = PlacementMode.Mouse;
            //_pop.Width = 450;
            _pop.Child = PopContentCode;



            ///属性选择方式弹出框初始化
            _popXaributeDrop.SetDataTarget(this.ChileEventCallBack);
            _selectPop.AllowsTransparency = true;
            _selectPop.PopupAnimation = PopupAnimation.Fade;
            _selectPop.StaysOpen = false;
            _selectPop.Placement = PlacementMode.Mouse;
            ///设置弹出框内容
            _selectPop.Child = _popXaributeDrop;

        }
        /// <summary>
        /// 显示弹出框
        /// </summary>
        protected void DirPopup()
        {
            _pop.IsOpen = true;
            //_pop.Child = PopContentCode;
        }
        /// <summary>
        /// 隐藏创建代码块的框
        /// </summary>
        protected void HiddenPopup()
        {
            _pop.IsOpen = false;
        }
        /// <summary>
        /// 显示属性选择方式弹出框
        /// </summary>
        /// <param name="data">要操作的属性数据源</param>
        protected void DirXAributeSelectPopup(object data)
        {
            ///设置数据
            _popXaributeDrop.PlayData(data);
            _selectPop.IsOpen = true;
        }
        /// <summary>
        /// 隐藏属性弹出框
        /// </summary>
        protected void HiddenXAributeSelectPopup()
        {
            _selectPop.IsOpen = false;
        }
        /// <summary>
        /// 创建一个函数
        /// </summary>
        /// <param name="Title">函数的名字</param>
        public bool CreatePicFunctionPage(string Title)
        {
            foreach(PicFunctionTabPage fun in ListFunction)
            {
                if (fun.Title == Title)
                    return false;
            }
            PicFunctionTabPage function = new PicFunctionTabPage(1,this.CallBackFunction,Title);
            ///给函数里面的函数代码创建数据
            ObservableCollection<MyXTreeItem> funcitondata = new ObservableCollection<MyXTreeItem>();        
            foreach(MyXTreeItem item in PopContentCode.MyData)
            {
                funcitondata.Add(item);
            }
            ///函数属性更新事件
            function.XAributeChangeMessage = (tcp) => 
            {
                XObjectData tcpmessagedata = new XObjectData(tcp);
                tcpmessagedata.additional_Information = MessageOption.Update.ToString();
                ///消息回调
                MessageUpdateCall(this, MouseState.XUpdateTreeViewData, tcpmessagedata); 
            };
            ///绑定数据
            function.PopContentCode.MyData = funcitondata;
            ListFunction.Add(function);
            ///通知更新数据
            MessageOption option = MessageOption.Add;
            XObjectData messagedata = new XObjectData(function);
            messagedata.additional_Information = option.ToString();
            MessageUpdateCall(this, MouseState.XUpdateTreeViewData, messagedata);
            return true;
        }
        /// <summary>
        /// 删除一个函数
        /// </summary>
        /// <param name="Title">函数的名字</param>
        public bool DelPicFunctionPage(PicFunctionTabPage function)
        {
            try
            {
                ///从函数列表删除一个函数
                ListFunction.Remove(function);
                return true;
            }catch(Exception ex)
            {
                LoggerHelp.WriteLogger(ex.ToString());
                MessageBox.Show("删除函数出错");
                return false;
            }
        }
        /// <summary>
        /// 创建一个类的属性
        /// </summary>
        /// <param name="Title">属性名称</param>
        /// <returns>是否成功创建</returns>
        public bool CreatePicXAribute(string Title)
        {
            foreach(XAribute xb in ListXAributes)
            {
                if (xb.Title == Title)
                    return false;
            }
            XAribute bute = new XAribute(1,Title, XAribute.XAttributeType.XBool, XAribute.XAttributeSpec.XNone,
                    XAribute.XPositonStyle.Left,this.ChileEventCallBack, XAribute.CanLinkType.One, "bool");
            ///将类设置为属性的父控件
            bute.ParentControl = this;
            ///当ExName改变的时候
            bute.ExNameChange = (s, o, v) =>
            {
                ///修改ExName
                ModifyPropetyExName((s as XAribute).Title, v);
            };
            ///绑定标题改变事件
            bute.TitleChange += (e, o, v) =>
            {
                ///修改Title
                return ModifyPropertyTitle(o, v); 
            };
            ListXAributes.Add(bute);
            ///回调
            if(MessageUpdateCall != null)
            {
                MessageUpdateCall(bute, MouseState.XUpdatePropertyData);
            }
            return true;
        }
        /// <summary>
        /// 删除一个类的属性
        /// </summary>
        /// <param name="butefile">要删除的数据对象</param>
        /// <returns>是否成功操作</returns>
        public bool DelXAribute(XAribute bute)
        {
            if (!ListXAributes.Contains(bute))
            {
                return false;
            }
            else
            {
                ListXAributes.Remove(bute);
                return true;
            }
        }
        /// <summary>
        /// 修改Title属性的值
        /// </summary>
        /// <param name="forntName">修改前的名字</param>
        /// <param name="nowName">修改后的名字</param>
        /// <returns></returns>
        public bool ModifyPropertyTitle(string forntName,string newName)
        {
            ///检测名称是否可以使用
            foreach(XAribute bute in ListXAributes)
            {
                if(newName == bute.Title)
                {
                    return false;
                }
            }
            ///修改代码块内部的属性名称
            foreach(CodeBox box in ListCodeBoxChild.Values)
            {
                ///如果是get类型
                if(box.CodeBoxType == CodeBox.XAType.get)
                {
                    ///获取get类型代码块右边属性的第一个
                    XAribute bute = box.RightAribute.Children[0] as XAribute;
                    ///如果该代码块的是改类的属性的话
                    if (bute != null && bute.Title == forntName)
                    {
                        bute.Title = newName;
                        box.Title = box.Title.Replace(forntName, newName);
                    }
                }
                ///如果是set类型
                else if(box.CodeBoxType == CodeBox.XAType.set)
                {
                    ///获取get类型代码块左边边属性的第一个
                    XAribute lefttbute = box.LeftAribute.Children[1] as XAribute;
                    ///获取get类型代码块右边属性的第一个
                    XAribute rightbute = box.RightAribute.Children[1] as XAribute;
                    ///如果是该类的属性的话
                    if(lefttbute != null && rightbute != null && lefttbute.Title == forntName )
                    {
                        ///修改属性的名称和值
                        lefttbute.Title = newName;
                        rightbute.Title = newName;
                        box.Title = box.Title.Replace(forntName, newName);
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// 修改属性ExName的值
        /// </summary>
        /// <param name="title">要被修改的属性的Title值</param>
        /// <param name="newVlaue">ExName的新值</param>
        /// <returns></returns>
        public bool ModifyPropetyExName(string title, string newVlaue)
        {
            ///循环所有代码块
            foreach(CodeBox box in ListCodeBoxChild.Values)
            {
                if(box.CodeBoxType == CodeBox.XAType.set)
                {
                    XAribute leftbute = box.LeftAribute.Children[1] as XAribute;
                    XAribute rightbute = box.RightAribute.Children[1] as XAribute;
                    if(leftbute != null && leftbute.Title == title)
                    {
                        leftbute.ExName = newVlaue;
                        rightbute.ExName = newVlaue;
                    }
                }
                else if(box.CodeBoxType == CodeBox.XAType.get && box.RightAribute.Children.Count > 0)
                {
                    XAribute rightbute = box.RightAribute.Children[0] as XAribute;
                    if(rightbute != null && rightbute.Title == title)
                    {
                        rightbute.ExName = newVlaue;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// 根据属性名称获取类的属性
        /// </summary>
        /// <param name="name">属性名称</param>
        /// <returns>返回的属性没有则为null</returns>
        public XAribute GetClassXAribute(string name)
        {
            foreach(XAribute bute in ListXAributes)
            {
                if (bute.Title == name)
                    return bute;
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
            base.ChileEventCallBack(sender, ms);
            Type sendtype = sender.GetType();
            switch (ms)
            {
                case MouseState.XClick:
                    if (SelectIDList.Count <= 1 && sendtype.Name == "CodeBox")
                    {
                        SetChildControlState(((XObject)sender).Id);  ///设置选中状态
                    }                  
                    break;
                case MouseState.XDown:                 
                    break;
                case MouseState.XToChildControl:
                    AddChildControlMsg(sender);
                    break;
                case MouseState.XToStopControl:
                    DelChildControlMsg();
                    break;
                case MouseState.XToDrawBezier:
                    isDrawBezier = true;
                    FirstXa = (XAribute)sender;    ///将发出信号的子控件 赋值
                    ///如果ALT键被按下则删除该节点上的曲线
                    if (_isPressingAlt)
                    {
                        FirstXa.ClearBezierLine();
                    }
                    if (FirstXa != null)
                    {
                        MyBezierLine = new BezierLine(CreateBezierID(), FirstXa.BorderColor, FirstXa.GetWorldPosition(), FirstXa.SelectPositionStyle);
                        AddBezierLine(MyBezierLine);
                    }
                    break;
                case MouseState.XMouseEnter:
                    if (isDrawBezier)
                    {                                          
                        if(sendtype.Name == "XAribute")
                        {
                            SecondXa = (XAribute)sender;    ///将发出信号的子控件 赋值
                            if(SecondXa != FirstXa)
                                isOverAribute = true;
                        }
                    }
                    break;
                case MouseState.XMouseLeave:
                    isOverAribute = false;
                    break;
                case MouseState.XMoveControl:
                    if (sendtype.Name == "CodeBox")
                    {
                        MoveSelectControl((Point)data.data,((CodeBox)sender));
                    }
                    break;
                case MouseState.XDelBezier:
                    if(sendtype.Name == "XAribute" && data != null)
                    {
                        DelBezierLine((BezierLine)data.data);
                    }
                    break;
                case MouseState.XCreateCodeBox:
                    #region 用户选择创建代码块
                    ///根据用户选择创建代码块
                    if (sendtype.Name == "XMTreeView")
                    {
                        MyXTreeItem mitem = (MyXTreeItem)data.data;
                        CodeBox box = CreateXCodeBox(mitem.XName, _createBoxPosition, mitem.MyCodeBoxType);
                        box.Hint = mitem.MyHitText;
                        box.SystemCodeString = mitem.SystemCodeString;
                        box.ReturnValueName = mitem.ReturnValue;
                        for (int i = 0; i < mitem.MyXaributeChildren.Count; i++)
                        {
                            XAributeItem aItem = mitem.MyXaributeChildren[i];
                            box.AddAttribute(aItem.MyXAttributeType, aItem.MyXAttributeSpec, aItem.MyXPositonStyle,
                                aItem.Parameter_name, aItem.MyCanLinkType, aItem.MyHittext, aItem.MyLastExText);
                        }
                        ///创建完成隐藏
                        HiddenPopup();
                    } 
                    #endregion
                    #region 属性方式选择框的回调事件
                    ///如果是属性方式选择框的回调事件
                    else if (sendtype.Name == "XAributeDropSelect")
                    {
                        XAribute bute = data.data as XAribute;
                        string type = data.state as string;
                        ///如果数据没有丢失
                        if (bute != null && type != null)
                        {
                            if (type == "set")
                            {
                                CodeBox box = CreateXCodeBox("设置:" + bute.Title + " 的值", _createBoxPosition, CodeBox.XAType.set);
                                box.Hint = "设置当前类中的这个属性的值，类型：" + bute.ExName;
                                box.AddAttribute(XAribute.XAttributeType.XEnter, XAribute.XAttributeSpec.XNone, XAribute.XPositonStyle.Left, "入口"
                                    , XAribute.CanLinkType.More, "执行入口", "");
                                box.AddAttribute(XAribute.XAttributeType.XExc, XAribute.XAttributeSpec.XNone, XAribute.XPositonStyle.right,
                                    "出口", XAribute.CanLinkType.One, "执行出口", "");
                                box.AddAttribute(bute.SelectType, bute.SelectSpc, XAribute.XPositonStyle.Left, bute.Title, XAribute.CanLinkType.One
                                    , bute.Hint, bute.ExName);
                                XAribute rightBute = box.AddAttribute(bute.SelectType, bute.SelectSpc, XAribute.XPositonStyle.right, bute.Title, XAribute.CanLinkType.More
                                    , bute.Hint, bute.ExName);
                                ///同样的属性不用绘制2次名称
                                rightBute.IsDirText = false;
                            }
                            else if (type == "get")
                            {
                                CodeBox box = CreateXCodeBox("获取:" + bute.Title + " 的值", _createBoxPosition, CodeBox.XAType.get);
                                box.Hint = "获取当前类中的这个属性的值，类型：" + bute.ExName;
                                XAribute rightbute = box.AddAttribute(bute.SelectType, bute.SelectSpc, XAribute.XPositonStyle.right, bute.Title, XAribute.CanLinkType.More
                                    , bute.Hint, bute.ExName);
                                
                            }
                        }
                        ///隐藏选择框
                        HiddenXAributeSelectPopup();
                    } 
                    #endregion
                    break;
            }
            return null;
        }
        #endregion
    }
}
