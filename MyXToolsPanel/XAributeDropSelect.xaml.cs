using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MyXObject;

namespace MyXToolsPanel
{
    /// <summary>
    /// XAributeDropSelect.xaml 的交互逻辑
    /// </summary>
    public partial class XAributeDropSelect : UserControl
    {
        public XAributeDropSelect()
        {
            InitializeComponent();
        }
        #region 自定义属性
        /// <summary>
        /// 事件冒泡回调
        /// </summary>
        private MouseCallFunction _callFunction;
        /// <summary>
        /// 数据
        /// </summary>
        XObjectData _data;
        #endregion
        #region 自定义函数
        /// <summary>
        /// 设置初始化值
        /// </summary>
        /// <param name="callFuntion"></param>
        /// <param name="obj"></param>
        public void SetDataTarget(MouseCallFunction callFuntion)
        {
            ///绑定回调函数
            _callFunction = callFuntion;         
        }
        /// <summary>
        /// 更换数据源
        /// </summary>
        /// <param name="obj"></param>
        public void PlayData(object obj)
        {
            ///清理数据
            if(_data != null)
            {
                _data = null;
            }
            ///绑定新数据
            _data = new XObjectData(obj);
        }
        #endregion
        #region 控件事件
        /// <summary>
        /// 获取该属性的值 的按钮按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Get_MouseClick(object sender, RoutedEventArgs e)
        {
            if(_callFunction != null)
            {
                ///设置数据的方式的获取
                _data.state = "get";
                _callFunction(this, MouseState.XCreateCodeBox, _data);
            }
        }
        /// <summary>
        /// 设置该属性的值 的按钮按下
        /// </summary> 
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Set_MouseClick(object sender, RoutedEventArgs e)
        {
            if (_callFunction != null)
            {
                ///设置数据的方式为设置
                _data.state = "set";
                _callFunction(this, MouseState.XCreateCodeBox, _data);
            }
        }
        #endregion
    }
}
