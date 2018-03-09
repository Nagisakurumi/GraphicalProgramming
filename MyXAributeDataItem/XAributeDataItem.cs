using MyCodeBox;
using MyXAribute;
using System.Collections.Generic;
using System.ComponentModel;
using MyXObject;

namespace MyXAributeDataItem
{
    /// <summary>
    /// 内容项
    /// </summary>
    public class XAributeDataItem : INotifyPropertyChanged
    {

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public XAributeDataItem(string Icon, string Name)
        {
            this.Icon = Icon;
            this.Name = Name;
        }
        #endregion
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
        #region 属性
        /// <summary>
        /// 图标
        /// </summary>
        private string _icon;
        /// <summary>
        /// 名称
        /// </summary>
        private string _name;
        /// <summary>
        /// 是否被选中
        /// </summary>
        private bool _isSelected = false;
        /// <summary>
        /// 端口类型
        /// </summary>
        private KeyValuePair<XAribute.XAttributeType, string> _pointTypeitem = new KeyValuePair<XAribute.XAttributeType, string>(XAribute.XAttributeType.XInt, "Int");
        /// <summary>
        /// 集合类型
        /// </summary>
        private KeyValuePair<XAribute.XAttributeSpec, string> _listTypeitem = new KeyValuePair<XAribute.XAttributeSpec, string>(XAribute.XAttributeSpec.XNone, "XNone");
        /// <summary>
        /// 代码块的类型
        /// </summary>
        private KeyValuePair<CodeBox.XAType, string> _codeBoxType = new KeyValuePair<CodeBox.XAType, string>();
        /// <summary>
        /// 位置类型
        /// </summary>
        private KeyValuePair<XAribute.XPositonStyle, string> _positionTypeitem = new KeyValuePair<XAribute.XPositonStyle, string>(XAribute.XPositonStyle.Left, "Left");
        /// <summary>
        /// 允许的连接类型
        /// </summary>
        private KeyValuePair<XAribute.CanLinkType, string> _linkTypeitem = new KeyValuePair<XAribute.CanLinkType, string>(XAribute.CanLinkType.One, "One");
        /// <summary>
        /// 提示信息
        /// </summary>
        private string _tipTypeitem = "";
        /// <summary>
        /// 类型名
        /// </summary>
        private string _lastExTexteitem = "";
        #endregion

        #region 读取器
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon
        {
            get
            {
                return _icon;
            }

            set
            {
                _icon = value;
                OnPropertyChanged("Icon");
            }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        /// <summary>
        /// 端口类型
        /// </summary>
        public KeyValuePair<XAribute.XAttributeType, string> PointTypeitem
        {
            get
            {
                return _pointTypeitem;
            }

            set
            {
                _pointTypeitem = value;
                OnPropertyChanged("PointTypeitem");
            }
        }
        /// <summary>
        /// 集合类型
        /// </summary>
        public KeyValuePair<XAribute.XAttributeSpec, string> ListTypeitem
        {
            get
            {
                return _listTypeitem;
            }

            set
            {
                _listTypeitem = value;
                OnPropertyChanged("ListTypeitem");
            }
        }
        /// <summary>
        /// 位置类型
        /// </summary>
        public KeyValuePair<XAribute.XPositonStyle, string> PositionTypeitem
        {
            get
            {
                return _positionTypeitem;
            }

            set
            {
                _positionTypeitem = value;
                OnPropertyChanged("PositionTypeitem");
            }
        }
        /// <summary>
        /// 允许的连接类型
        /// </summary>
        public KeyValuePair<XAribute.CanLinkType, string> LinkTypeitem
        {
            get
            {
                return _linkTypeitem;
            }

            set
            {
                _linkTypeitem = value;
                OnPropertyChanged("LinkTypeitem");
            }
        }
        /// <summary>
        /// 提示信息
        /// </summary>
        public string TipTypeitem
        {
            get
            {
                return _tipTypeitem;
            }

            set
            {
                _tipTypeitem = value;
                OnPropertyChanged("TipTypeitem");
            }
        }
        /// <summary>
        /// 类型名
        /// </summary>
        public string LastExTexteitem
        {
            get
            {
                return _lastExTexteitem;
            }

            set
            {
                _lastExTexteitem = value;
                OnPropertyChanged("LastExTexteitem");
            }
        }
        /// <summary>
        /// 是否被选中
        /// </summary>
        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }

            set
            {
                _isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }
        /// <summary>
        /// 代码块的类型
        /// </summary>
        public KeyValuePair<CodeBox.XAType, string> CodeBoxType
        {
            get
            {
                return _codeBoxType;
            }

            set
            {
                _codeBoxType = value;
            }
        }
        #endregion

    }
}
