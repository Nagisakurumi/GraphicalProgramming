using System.Windows.Controls;
using System.Windows.Input;

namespace MyXToolsPanel
{
    /// <summary>
    /// StringTextControl.xaml 的交互逻辑
    /// </summary>
    public partial class StringTextControl : UserControl
    {
        public StringTextControl()
        {
            InitializeComponent();
        }
        #region 自定义属性
        /// <summary>
        /// 确认回调函数
        /// </summary>
        private SureOptionCall OptionCall;
        #endregion
        #region 自定义函数
        /// <summary>
        /// 设置初始化信息
        /// </summary>
        /// <param name="PropertyName">属性名称</param>
        /// <param name="value">属性值</param>
        /// <param name="option">确认回调函数</param>
        public void SetInitBaseInfo(string PropertyName, string value, SureOptionCall option)
        {
            ///设置属性名称
            NameBlock.Text = PropertyName;
            ///设置初始值
            Value.Text = value;
            ///设置回调函数
            this.OptionCall = option;
        }
        #endregion

        /// <summary>
        /// 回车按下的时候
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(Value.Text != "" && e.Key == Key.Enter)
            {
                if(OptionCall != null)
                {
                    ///回调属性值
                    OptionCall(NameBlock.Text, OptionType.XString, Value.Text);
                }
            }
        }
    }
}
