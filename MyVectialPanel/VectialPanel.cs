using MyXAribute;
using MyXObject;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MyVectialPanel
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:MyVectialPanel"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:MyVectialPanel;assembly=MyVectialPanel"
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
    public class VectialPanel : XObject
    {
        static VectialPanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(VectialPanel), new FrameworkPropertyMetadata(typeof(VectialPanel)));
        }
        #region 自定义属性
        /// <summary>
        /// 显示位置信息
        /// </summary>
        public enum PositonType
        {
            /// <summary>
            /// 左边
            /// </summary>
            Left = 1,
            /// <summary>
            /// 右边
            /// </summary>
            Right = 2
        }
        /// <summary>
        /// 设置为左边垂直 还是 右边垂直
        /// </summary>
        private PositonType _position;
        /// <summary>
        /// 上下间隙
        /// </summary>
        private double _disHeigth = 1;
        /// <summary>
        /// 主面板
        /// </summary>
        private StackPanel _myPanel;
        #endregion
        #region 读取器
        /// <summary>
        /// 控件之间的间隙
        /// </summary>
        public double DisHeigth
        {
            get
            {
                return _disHeigth;
            }

            set
            {
                _disHeigth = value;
            }
        }
        /// <summary>
        /// 设置为左边垂直 还是 右边垂直
        /// </summary>
        public PositonType Position
        {
            get
            {
                return _position;
            }

            set
            {
                _position = value;
            }
        }
        /// <summary>
        /// 主面板
        /// </summary>
        public StackPanel MyPanel
        {
            get
            {
                return _myPanel;
            }

            set
            {
                _myPanel = value;
            }
        }
        /// <summary>
        /// 子项
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
        /// 构造函数
        /// </summary>
        /// <param name="dir">确认方向</param>
        public VectialPanel(PositonType dir)
        {
            Position = dir;
            InitBaseInfo();
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public VectialPanel()
        {
            Position = PositonType.Left;
            InitBaseInfo();
        }
        /// <summary>
        /// 初始化信息
        /// </summary>
        protected override void InitBaseInfo()
        {
            ///设置初始宽度
            this.Width = 80;
            ///设置自己的Panel
            MyPanel = new StackPanel();
            ///设置为垂直布局
            MyPanel.Orientation = Orientation.Vertical;
            ///设定布局流向
            if(Position == PositonType.Left)
            {
                MyPanel.FlowDirection = FlowDirection.LeftToRight;
            }
            else
            {
                MyPanel.FlowDirection = FlowDirection.RightToLeft;
            }
            ///加入控件作为主布局
            GetChildren().Add(MyPanel);
            ///绑定高度
            ToolHelp.SetBindingHeight(MyPanel, this);
            ///绑定宽度
            ToolHelp.SetBindingWidth(MyPanel, this);          
        }
        #endregion
        #region 自定义函数
        /// <summary>
        /// 添加一个子控件
        /// </summary>
        /// <param name="addchild"></param>
        public void AddXAbutrite(XAribute addchild)
        {
            Children.Add(addchild);
            ///由于设置了布局流向所以方向相反
            if (this.Position == PositonType.Left)
            {
                addchild.HorizontalAlignment = HorizontalAlignment.Left;
            }
            else
            {
                addchild.HorizontalAlignment = HorizontalAlignment.Left;
            }
        }
        /// <summary>
        /// 删除一个子控件
        /// </summary>
        /// <param name="addchild"></param>
        public void DelXAbutrite(XAribute addchild)
        {
            try
            {
                MyPanel.Children.Remove(addchild);
                ///修改尺寸
                AutoSize();
            }
            catch(Exception ex)
            {
                LoggerHelp.WriteLogger(ex.ToString());
            }
        }
        /// <summary>
        /// 修改尺寸和调整个控件的位置
        /// </summary>
        public override void AutoSize()
        {
            double hei = 0;
            double wid = 0;
            for (int i = 0; i < MyPanel.Children.Count; i++)
            {
                XAribute xa = ((XAribute)MyPanel.Children[i]);
                hei += xa.Height + DisHeigth;
                double centerWidth = 0;
                centerWidth = xa.Width;
                wid = wid < centerWidth ? centerWidth : wid;
            }
            this.Height = hei;
            this.Width = wid;              
        }              
        /// <summary>
        /// 获取子元素相对本元素的位置
        /// </summary>
        /// <param name="child">要查询的子元素</param>
        public override Point GetChildWorldPosition(XObject child)
        {
            ///查看是否包含
            if (MyPanel.Children.Contains(child))
            {
                ///获取索引
                int index = MyPanel.Children.IndexOf(child);
                double wid = child.RenderSize.Width == 0 ? child.Width : child.RenderSize.Width;
                double hei = child.Height;
                Point position = new Point();
                if (this.Position == PositonType.Right)
                    position = new Point(this.Width - wid, index * hei);
                else
                    position = new Point(0, index * hei);
                return position;
            }
            ///如果不存在则返回一个负位置
            return new Point(-1, -1);
        }
        #endregion
    }
}
