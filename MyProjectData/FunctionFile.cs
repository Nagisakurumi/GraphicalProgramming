using System.ComponentModel;

namespace MyProjectData
{
    public class FunctionFile : INotifyPropertyChanged
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
        /// 修改前的名字
        /// </summary>
        private string _forntName = "";
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
        /// 修改前的名字 
        /// </summary>
        public string ForntName
        {
            get
            {
                return _forntName;
            }

            set
            {
                _forntName = value;
            }
        }
        #endregion
        /// <summary>
        /// 构造函数
        /// </summary>
        public FunctionFile()
        {

        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Name">名称</param>
        public FunctionFile(string Name)
        {
            this.Name = Name;
            this.ForntName = Name;
        }
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
