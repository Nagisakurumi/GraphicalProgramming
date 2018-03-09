using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace MyXToolsPanel
{
    /// <summary>
    /// ColorSelectorControl.xaml 的交互逻辑
    /// </summary>
    public partial class ColorSelectorControl : UserControl
    {
        #region 自定义属性
        /// <summary>
        /// 定义弹出窗
        /// </summary>
        private Popup _pop = new Popup();
        /// <summary>
        /// 声明一个颜色值
        /// </summary>
        private ColorMan _colorValue = new ColorMan();
        /// <summary>
        /// 颜色选择器
        /// </summary>
        private ColorSelector _cSelector = new ColorSelector();
        /// <summary>
        /// 确认操作回调函数
        /// </summary>
        private SureOptionCall OptionCall;
        #endregion
        #region 读取器
        /// <summary>
        /// 声明一个颜色值
        /// </summary>
        public ColorMan ColorValue
        {
            get
            {
                return _colorValue;
            }

            set
            {
                _colorValue = value;
            }
        }
        #endregion      
        #region 初始化
        public ColorSelectorControl()
        {
            InitializeComponent();
            ///设置对象
            _cSelector.SetColorValue(this);
            ColorText.IsEnabled = false;
            

            #region 初始化颜色弹出框
            _pop.AllowsTransparency = true;
            _pop.PopupAnimation = PopupAnimation.Fade;
            _pop.StaysOpen = false;
            _pop.Placement = PlacementMode.RelativePoint;
            _pop.Child = _cSelector;
            _pop.PlacementTarget = ColorText;
            #endregion
        }
        #endregion
        #region 函数
        /// <summary>
        /// 设置初始化信息
        /// </summary>
        /// <param name="PropertyName">属性名称</param>
        /// <param name="value">初始值</param>
        /// <param name="option">确认操作回调函数</param>
        public void SetInitBaseInfo(string PropertyName, Color value, SureOptionCall option)
        {
            this.OptionCall = option;
            ///设置属性名称
            NameBlock.Text = PropertyName;
            ///设置颜色初始值
            ColorValue.red = value.R;
            ColorValue.green = value.G;
            ColorValue.blue = value.B;
            ColorValue.alpha = value.A;
            ///显示颜色
            ColorText.Text = ColorValue.ToHexString();
            ColorRec.Fill = ColorValue.GetColorBrush();
        }
        /// <summary>
        /// 颜色选择按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _pop.IsOpen = true;          
        }
        #region 自定义函数
        /// <summary>
        /// 取消弹出框
        /// </summary>
        /// <param name="issure">是否更新颜色</param>
        public void CancelPopup(bool issure)
        {
            _pop.IsOpen = false;
            if(issure)
            {
                ColorText.Text = ColorValue.ToHexString();
                ColorRec.Fill = ColorValue.GetColorBrush();
                ///回调信息
                if(OptionCall != null)
                    OptionCall(NameBlock.Text, OptionType.XColor, ColorValue);
            }
        }
        #endregion
        #endregion
    }
}
