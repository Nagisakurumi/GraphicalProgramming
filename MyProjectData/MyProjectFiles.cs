using System.Collections.Generic;
using System.ComponentModel;

namespace MyProjectData
{
    public class MyProjectFiles : INotifyPropertyChanged
    {
        #region 属性
        /// <summary>
        /// 文件图标
        /// </summary>
        private string _icon = "";
        /// <summary>
        /// 文件名称
        /// </summary>
        private string _name = "";
        /// <summary>
        /// 文件路径
        /// </summary>
        private string _path = "";
        /// <summary>
        /// 是否被选中
        /// </summary>
        private bool _isSelected = false;
        /// <summary>
        /// 是否展开
        /// </summary>
        private bool _isExpanded = false;
        #endregion
        #region 读取器
        /// <summary>
        /// 文件图标
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
        /// 文件名字
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
        /// 文件路径
        /// </summary>
        public string Path
        {
            get
            {
                return _path;
            }

            set
            {
                _path = value;
                OnPropertyChanged("Path");
            }
        }
        /// <summary>
        /// 子文件
        /// </summary>
        public List<MyProjectFiles> ChildrenItem { get; set; }
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
        /// 是否展开
        /// </summary>
        public bool IsExpanded
        {
            get
            {
                return _isExpanded;
            }

            set
            {
                _isExpanded = value;
                OnPropertyChanged("IsExpanded");
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public MyProjectFiles()
        {

        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Name">文件名字</param>
        /// <param name="Path">文件路径</param>
        public MyProjectFiles(string Name, string Path)
        {
            this.Name = Name;
            this.Path = Path;
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
    }
}
