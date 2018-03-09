using System.Windows.Media;

namespace MyXToolsPanel
{
    /// <summary>
    /// 确认操作的回调函数
    /// </summary>
    /// <param name="PropertyName">属性名称</param>
    /// <param name="Option">属性类型</param>
    /// <param name="Value">属性值</param>
    public delegate void SureOptionCall(string PropertyName, OptionType Option, object Value);
    /// <summary>
    /// 操作的属性类型枚举
    /// </summary>
    public enum OptionType
    {
        /// <summary>
        /// 字符串类型的属性
        /// </summary>
        XString = 0,
        /// <summary>
        /// 颜色类型的属性
        /// </summary>
        XColor = 1
    }
    /// <summary>
    /// 颜色管理
    /// </summary>
    public class ColorMan
    {
        /// <summary>
        /// 红色数值
        /// </summary>
        public int red = 0;
        /// <summary>
        /// 绿色数值
        /// </summary>
        public int green = 0;
        /// <summary>
        /// 蓝色
        /// </summary>
        public int blue = 0;
        /// <summary>
        /// 透明度
        /// </summary>
        public int alpha = 0;
        /// <summary>
        /// 画刷
        /// </summary>
        public SolidColorBrush brush = new SolidColorBrush();
        /// <summary>
        /// 转换为16进制的颜色代码
        /// </summary>
        /// <returns></returns>
        public string ToHexString()
        {
            string hex = "#";
            hex += red.ToString("X") + green.ToString("X") + blue.ToString("X");
            return hex;
        }
        /// <summary>
        /// 获取实体画刷
        /// </summary>
        /// <returns></returns>
        public SolidColorBrush GetColorBrush()
        {
            brush.Color = Color.FromArgb((byte)alpha, (byte)red, (byte)green, (byte)blue);
            return brush;
        }
        /// <summary>
        /// 转换为颜色
        /// </summary>
        /// <returns>返回颜色</returns>
        public Color ToColor()
        {
            return Color.FromArgb((byte)alpha, (byte)red, (byte)green, (byte)blue);
        }
    }
}
