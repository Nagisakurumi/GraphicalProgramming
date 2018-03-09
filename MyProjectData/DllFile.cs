using System.ComponentModel;

namespace MyProjectData
{
    public class DllFile : INotifyPropertyChanged
    {
        /// <summary>
        /// 图标路径
        /// </summary>
        private string _icon = "";
        /// <summary>
        /// 名称
        /// </summary>
        private string name = "";
        /// <summary>
        /// 路径
        /// </summary>
        private string path = "";
        /// <summary>
        /// 构造函数
        /// </summary>
        public DllFile()
        {

        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Name">名称</param>
        /// <param name="Path">路径</param>
        public DllFile(string Name, string Path)
        {
            this.Name = Name;
            this.Path = Path;
        }

        #region 读取器
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        /// <summary>
        /// 路径
        /// </summary>
        public string Path
        {
            get
            {
                return path;
            }

            set
            {
                path = value;
                OnPropertyChanged("Path");
            }
        }
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
