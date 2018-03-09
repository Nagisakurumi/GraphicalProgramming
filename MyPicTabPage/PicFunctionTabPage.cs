using MyXObject;
using System.Windows;
using MyCodeBox;
using MyXAribute;

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
    ///     <MyNamespace:PicFunctionTabPage/>
    ///
    /// </summary>
    public class PicFunctionTabPage : PicTabPage
    {
        /// <summary>
        /// 属性改变
        /// </summary>
        public delegate void XAributeChange(PicFunctionTabPage tcp);
        #region 构造函数
        static PicFunctionTabPage()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PicFunctionTabPage), new FrameworkPropertyMetadata(typeof(PicFunctionTabPage)));
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public PicFunctionTabPage()
        {
            InitBaseInfo();
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">控件的唯一标示符</param>
        /// <param name="mcf">回调函数</param>
        /// <param name="name">控件名字</param>
        public PicFunctionTabPage(int id, MouseCallFunction mcf, string Title)
        {
            this.Id = id;
            this.CallBackFunction = mcf;
            this.Title = Title;           
        }
        /// <summary>
        /// 初始化基本信息
        /// </summary>
        protected override void InitBaseInfo()
        {
            base.InitBaseInfo();
            ///创建函数入口代码块
            FunctionEnterBox = CreateXCodeBox("方法入口", CenterPoint, CodeBox.XAType.XFunctionEnter);
            FunctionEnterBox.OpenButton = 2;
            FunctionEnterBox.AddXExcXAribute();
            FunctionEnterBox.XAributeChangeMessage = () => { XAributeChangeMessage(this); };///属性消息变更通知
            ///创建函数出口代码块
            CodeBox functionExc = CreateXCodeBox("方法出口", new Point(CenterPoint.X + 250, CenterPoint.Y), CodeBox.XAType.XFunctionExc);
            functionExc.OpenButton = 1;
            functionExc.AddXEnterXAribute();
            functionExc.XAributeChangeMessage = () => { XAributeChangeMessage(this); };///属性消息变更通知
            ///保存函数出口代码块的地址
            FunctionExcBox = functionExc;

            ///添加初始连线
            XAribute FirstXa = FunctionEnterBox.GetRightExc()[0];
            XAribute SecondXa = functionExc.GetLeftEnter();
            BezierLine MyBezierLine = new BezierLine(CreateBezierID(), FirstXa.BorderColor, FirstXa.GetWorldPosition(), FirstXa.SelectPositionStyle);
            AddBezierLine(MyBezierLine);
            FirstXa.AddBezierLine(MyBezierLine);
            MyBezierLine.StartPoint.LinkAribute = FirstXa;
            SecondXa.AddBezierLine(MyBezierLine);
            MyBezierLine.EndPoint.LinkAribute = SecondXa;
            MyBezierLine.SetBezierLine(SecondXa.GetWorldPosition(), SecondXa.SelectPositionStyle);
        }
        #endregion
        #region 自定义属性
        /// <summary>
        /// 函数入口地址
        /// </summary>
        private CodeBox _functionEnterBox;
        /// <summary>
        /// 函数出口地址
        /// </summary>
        private CodeBox _functionExcBox;
        /// <summary>
        /// 继承类型
        /// </summary>
        private OverrideType _myOverride = OverrideType.None;
        #endregion
        #region 读取器
        /// <summary>
        /// 函数入口地址
        /// </summary>
        public CodeBox FunctionEnterBox
        {
            get
            {
                return _functionEnterBox;
            }

            set
            {
                _functionEnterBox = value;
            }
        }
        /// <summary>
        /// 继承类型
        /// </summary>
        public OverrideType MyOverride
        {
            get
            {
                return _myOverride;
            }

            set
            {
                _myOverride = value;
            }
        }
        /// <summary>
        /// 函数出口地址
        /// </summary>
        public CodeBox FunctionExcBox
        {
            get
            {
                return _functionExcBox;
            }

            set
            {
                _functionExcBox = value;
            }
        }
        /// <summary>
        /// 属性变更通知
        /// </summary>
        public XAributeChange XAributeChangeMessage { get; set; }
        #endregion
        #region 自定义函数

        #endregion
    }
}
