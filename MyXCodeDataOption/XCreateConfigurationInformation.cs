using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace MyXCodeDataOption
{
    public class XCreateConfigurationInformation
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public XCreateConfigurationInformation()
        {

        }
        #endregion

        #region 自定义属性
        /// <summary>
        /// 项目根路径
        /// </summary>
        private string _rootPath = "";
        /// <summary>
        /// 项目存放文件路径
        /// </summary>
        private string _filesPath = "";
        /// <summary>
        /// 项目输出路径
        /// </summary>
        private string _outPath = "";
        /// <summary>
        /// 创建项目之初的配置文件路径
        /// </summary>
        private string _configFilePath = "";
        /// <summary>
        /// 创建项目的时候解决方案的配置文件的路径
        /// </summary>
        public static string SolutionConfigPath = "";
        /// <summary>
        /// 版本号
        /// </summary>
        private static string _version = "1.0.0";
        /// <summary>
        /// 编译器名称
        /// </summary>
        private static string _editorname = "MyEdtior";
        /// <summary>
        /// 配置文件路径
        /// </summary>
        private static string _projectConfigPath = "";
        /// <summary>
        /// debug路径
        /// </summary>
        private static string _debugPath = "";
        #endregion
        #region 读取器
        /// <summary>
        /// 项目根路径
        /// </summary>
        public string RootPath
        {
            get
            {
                return _rootPath;
            }

            set
            {
                _rootPath = value;
            }
        }
        /// <summary>
        /// 项目存放文件路径
        /// </summary>
        public string FilesPath
        {
            get
            {
                return _filesPath;
            }

            set
            {
                _filesPath = value;
            }
        }
        /// <summary>
        /// 项目输出路径
        /// </summary>
        public string OutPath
        {
            get
            {
                return _outPath;
            }

            set
            {
                _outPath = value;
            }
        }
        /// <summary>
        /// 创建项目之初的配置文件路径
        /// </summary>
        public string ConfigFilePath
        {
            get
            {
                return _configFilePath;
            }

            set
            {
                _configFilePath = value;
            }
        }
        /// <summary>
        /// 版本号
        /// </summary>
        public static string Version
        {
            get
            {
                return _version;
            }

            set
            {
                _version = value;
            }
        }
        /// <summary>
        /// 编译器名称
        /// </summary>
        public static string Editorname
        {
            get
            {
                return _editorname;
            }

            set
            {
                _editorname = value;
            }
        }
        #endregion
        #region 自定义函数
        /// <summary>
        /// 创建初始化文件系统
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <param name="Extension">文件扩展名</param>
        /// <param name="DirectoryName">目录的名字</param>
        /// <returns></returns>
        public bool CreateOpenFile(string filepath,string Extension, string DirectoryName, string ProjectTypeName, string type = "Control")
        {
            string CreateFilePath = filepath + "//" + DirectoryName;
            ///保存项目根目录
            RootPath = filepath + "//";
            ///保存文件目录
            FilesPath = CreateFilePath + "//";
            SaveFileDialog sfd = new SaveFileDialog();      
            if (!Directory.Exists(CreateFilePath))
            {
                ///如果文件夹不存在则创建
                Directory.CreateDirectory(CreateFilePath);
            }
            else
            {
                MessageBox.Show("已经存在同名文件请核对后再操作");
                return false;
            }
            ///要被创建的文件的全名
            string ProjectTypeSavePath = CreateFilePath + "//" + DirectoryName + "." + ProjectTypeName;// 获得文件保存的路径；  
           ///保存配置文件路径
            ConfigFilePath = ProjectTypeSavePath;
            ///写入内置项目配置文件
            WriteProjectInitInformation(ProjectTypeSavePath, DirectoryName);

            ///要被创建的文件的全名
            string fileSavePath = CreateFilePath + "." + Extension;// 获得文件保存的路径；  
            ///如果文件已经存在                   
            if (CheckFile(fileSavePath))
            {
                MessageBox.Show("已经存在同名文件请核对后再操作");
                return false;
            }
            ///如果文件不存在
            else
            {
                WriteSolutionInformation(fileSavePath, DirectoryName + "." + ProjectTypeName, type);
                ///保存解决方案配置文件路径
                SolutionConfigPath = fileSavePath;

                ///创建必要文件夹
                Directory.CreateDirectory(CreateFilePath);
                ///创建deubug文件夹
                Directory.CreateDirectory(CreateFilePath + "//bin//Debug");
                ///保存输出目录
                OutPath = CreateFilePath + "//bin//Debug//";
                _debugPath = CreateFilePath + "//bin//Debug//";
                ///创建配置文件夹
                Directory.CreateDirectory(CreateFilePath + "//ProjectConfig");
                _projectConfigPath = CreateFilePath + "//ProjectConfig//";              
         
            }
            return true;
        }
        /// <summary>
        /// 检查文件是否存在
        /// </summary>
        /// <returns></returns>
        protected bool CheckFile(String path)
        {
            if (!File.Exists(path))
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 写入这程序集的配置信息
        /// </summary>
        /// <param name="path">文件路径</param>
        protected void WriteProjectInitInformation(string path,string projectname)
        {
            if (!CheckFile(path))
            {
                XmlTextWriter writer = new XmlTextWriter(path, Encoding.UTF8);
                writer.Formatting = Formatting.Indented;
                writer.WriteStartDocument();
                writer.WriteStartElement("Config");
                ///开始写入正常信息               
                writer.WriteAttributeString("ProjectName", projectname);
                ///写入项目文件
                writer.WriteStartElement("ProjectFile");
                writer.WriteAttributeString("Type", "MainFunction"); 
                writer.WriteAttributeString("FileName", "Program.xs");
                writer.WriteEndElement();

                writer.WriteEndElement();
                writer.Close();
            }
            else
            {

            }
        }
        /// <summary>
        /// 写入解决方案哦诶之文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="projectname"></param>
        protected void WriteSolutionInformation(string path, string projectname, string type = "Control")
        {
            if (!CheckFile(path))
            {
                XmlTextWriter writer = new XmlTextWriter(path, Encoding.UTF8);
                writer.Formatting = Formatting.Indented;
                writer.WriteStartDocument();
                writer.WriteStartElement("Config");
                writer.WriteAttributeString("ProjectLanguage", type);
                writer.WriteAttributeString("版本号", Version);
                writer.WriteAttributeString("编辑器", Editorname);
                ///写入项目信息
                writer.WriteStartElement("Projects");
                writer.WriteStartElement("Project");
                writer.WriteAttributeString("ProjectName", projectname);
                writer.WriteEndElement();

                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.Close();
            }
        }
        #endregion
    }
}
