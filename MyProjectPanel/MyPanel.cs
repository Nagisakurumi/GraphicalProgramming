using System.Windows.Controls;
using MyXObject;
namespace MyProjectPanel
{
    public class MyPanel : UserControl
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public MyPanel()
        {

        }
        #endregion
        #region 自定义属性
        /// <summary>
        /// 回调函数
        /// </summary>
        private MouseCallFunction _callFunction;
        #endregion

        #region 自定义函数
        /// <summary>
        /// 设置回调函数
        /// </summary>
        /// <param name="callfunction">回调函数</param>
        public void SetCallFunction(MouseCallFunction callfunction)
        {
            this._callFunction = callfunction;
        }
        /// <summary>
        /// 向父控件回调事件
        /// </summary>
        /// <param name="state">事件类型</param>
        /// <param name="data">数据</param>
        protected void ToCallFunctionParentControl(MouseState state, XObjectData data)
        {
            if(_callFunction != null)
            {
                _callFunction(this, state, data);
            }
        }
        #endregion
    }
}
