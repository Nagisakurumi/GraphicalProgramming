using System.ComponentModel;

namespace MyProjectData
{
    public class XProjectDataItem : INotifyPropertyChanged
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public XProjectDataItem(string Icon, string ProjectTypeName)
        {
            this.Icon = Icon;
            this.ProjectTypeName = ProjectTypeName;
        }
        #endregion
        #region 接口实现
        /// <summary>
        /// 事件声明接口
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
        #endregion
        #region 属性
        /// <summary>
        /// 是否被选中
        /// </summary>
        private bool _isSelected = false;
        /// <summary>
        /// 图标
        /// </summary>
        private string _icon = "";
        /// <summary>
        /// 项目名称
        /// </summary>
        private string _projectTypeName = "";
        /// <summary>
        /// 项目配置文件
        /// </summary>
        private string _projectFileExtension = "csxproject";
        /// <summary>
        /// 总文件打开方式扩展名
        /// </summary>
        private string _openFileExtension = "xpl";
        /// <summary>
        /// 项目编译后的语言
        /// </summary>
        private string _projectLanguages = "C#";
        #endregion
        #region 读取器
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
        /// 图标路劲
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
        /// 创建的项目类型
        /// </summary>
        public string ProjectTypeName
        {
            get
            {
                return _projectTypeName;
            }
            set
            {
                _projectTypeName = value;
                OnPropertyChanged("ProjectTypeName");
            }
        }
        /// <summary>
        /// 项目配置文件名
        /// </summary>
        public string ProjectFileExtension
        {
            get
            {
                return _projectFileExtension;
            }

            set
            {
                _projectFileExtension = value;
            }
        }
        /// <summary>
        /// 总文件打开方式扩展名
        /// </summary>
        public string OpenFileExtension
        {
            get
            {
                return _openFileExtension;
            }

            set
            {
                _openFileExtension = value;
            }
        }
        /// <summary>
        /// 项目编译后的语言
        /// </summary>
        public string ProjectLanguages
        {
            get
            {
                return _projectLanguages;
            }

            set
            {
                _projectLanguages = value;
            }
        }
        #endregion
    }
}
