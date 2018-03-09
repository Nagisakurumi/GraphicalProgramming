namespace MyXCodeEditor
{
    public abstract class XPicCodeToCode
    {
        #region 自定义属性
        /// <summary>
        /// 项目名称
        /// </summary>
        private string _projectName = "";
        /// <summary>
        /// 换行符
        /// </summary>
        private string _lineBreaks = "\r\n";
        #endregion
        #region 读取器
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName
        {
            get
            {
                return _projectName;
            }

            set
            {
                _projectName = value;
            }
        }
        /// <summary>
        /// 换行符
        /// </summary>
        public string LineBreaks
        {
            get
            {
                return _lineBreaks;
            }

            set
            {
                _lineBreaks = value;
            }
        }
        #endregion
        #region 
        /// <summary>
        /// 代码转换
        /// </summary>
        /// <returns></returns>
        public virtual string XPicCodeEditor(MyPicTabPage.PicTabPage pic)
        {
            return "";
        }
        #endregion
    }
}
