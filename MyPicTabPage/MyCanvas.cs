using MyXObject;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:MyCanvas/>
    ///
    /// </summary>
    public class MyCanvas : Canvas
    {
        static MyCanvas()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MyCanvas), new FrameworkPropertyMetadata(typeof(MyCanvas)));
        }
        #region 自定义属性
        /// <summary>
        /// 背景线条颜色
        /// </summary>
        Color _bkLineColor = Color.FromArgb(0, 0, 0, 0);
        /// <summary>
        /// 框选第一个点
        /// </summary>
        Point _mouseStartPoint = new Point();
        /// <summary>
        /// 框选的第二个点
        /// </summary>
        Point _mouseEndPoint = new Point();
        /// <summary>
        /// 框的矩形
        /// </summary>
        Rect inveateRec = new Rect();
        /// <summary>
        /// 是否准备画框选的框
        /// </summary>
        bool _isToSelect = false;
        #endregion
        #region 读取器
        /// <summary>
        /// 是否准备画框
        /// </summary>
        public bool IsToSelect
        {
            get
            {
                return _isToSelect;
            }

            set
            {
                _isToSelect = value;
                this.InvalidateVisual();
            }
        }
        /// <summary>
        /// 框的矩形
        /// </summary>
        public Rect InveateRec
        {
            get
            {
                return inveateRec;
            }

            set
            {
                inveateRec = value;
            }
        }
        /// <summary>
        /// 第二个点
        /// </summary>
        public Point MouseEndPoint
        {
            get
            {
                return _mouseEndPoint;
            }

            set
            {
                _mouseEndPoint = value;
                InveateRec = ToolHelp.PointToRectangle(_mouseStartPoint, _mouseEndPoint);
                this.InvalidateVisual();
            }
        }
        /// <summary>
        /// 第一个点
        /// </summary>
        public Point MouseStartPoint
        {
            get
            {
                return _mouseStartPoint;
            }

            set
            {
                IsToSelect = true;
                _mouseStartPoint = value;
            }
        }
        /// <summary>
        /// 背景线条颜色
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
        #endregion
        #region 继承函数
        /// <summary>
        /// 界面渲染绘制函数
        /// </summary>
        /// <param name="drawingContext"></param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            #region 用于绘制 背景
            //Brush MyBrush = new LinearGradientBrush(Color.FromArgb(200,255,255,255),Color.FromArgb(150,0,0,0),new Point(0,0),new Point(this.RenderSize.Width,RenderSize.Height));
            Pen MyPen = new Pen(this.Background, 2);
            drawingContext.DrawRectangle(this.Background, MyPen, new Rect(0, 0, RenderSize.Width, RenderSize.Height));
            //drawingContext.DrawRoundedRectangle(MyBrush, MyPen, new Rect(0, 0, RenderSize.Width, RenderSize.Height), 10, 10) 
            #endregion;
            #region 绘制背景上的线条
            //Pen MyLinePen = new Pen(new RadialGradientBrush(Color.FromArgb(255, 255, 255, 255), Color.FromArgb(255, 0, 0, 0)),2);
            Pen MyLinePen = new Pen(new SolidColorBrush(BkLineColor), 1);
            double curLinei = RenderSize.Width / 100;
            if (curLinei < 40)
                curLinei = 40;
            for (double i = 0; i < RenderSize.Width; i += curLinei)
            {
                drawingContext.DrawLine(MyLinePen, new Point(i, 0), new Point(i, RenderSize.Height));
            }
            for (double i = 0; i < RenderSize.Height; i += curLinei)
            {
                drawingContext.DrawLine(MyLinePen, new Point(0, i), new Point(RenderSize.Width, i));
            }
            #endregion
            #region 绘制框选
            if (IsToSelect)
            {
                Pen RecPen = new Pen(new SolidColorBrush(Color.FromArgb(255, 255, 211, 08)), 2);
                drawingContext.DrawRectangle(new SolidColorBrush(Color.FromArgb(0, 0, 0, 0)), RecPen, InveateRec);
            }
            #endregion
        }
        #endregion
    }
}
