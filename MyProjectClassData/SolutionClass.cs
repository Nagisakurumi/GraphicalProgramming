using MyCodeEditor;
using MyPicTabPage;
using MyProjectData;
using MyXCodeEditor;
using MyXObject;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Xml;

namespace MyProjectClassData
{
    public class SolutionClass
    {
        #region 自定义属性
        /// <summary>
        /// 解决方案集合
        /// </summary>
        private List<ProjectClass> _mySolution = new List<ProjectClass>();
        /// <summary>
        /// 解决方案里面所有项目的集合
        /// </summary>
        private ObservableCollection<MyProjectFiles> _myProjectClassFile = new ObservableCollection<MyProjectFiles>();
        /// <summary>
        /// 更目录路径
        /// </summary>
        private static string _rootPath = "";
        /// <summary>
        /// 扩展名
        /// </summary>
        private static string _exname = ".xpl";
        /// <summary>
        /// 解决方案配置文件路径
        /// </summary>
        private string _solutionConfigPath = "";
        /// <summary>
        /// 项目语言
        /// </summary>
        private string _projectLanguage = "";
        /// <summary>
        /// 保存代码文件路劲
        /// </summary>
        private string _codeFilePath = "CodeFile/";
        #endregion
        #region 读取器
        /// <summary>
        /// 解决方案集合
        /// </summary>
        public List<ProjectClass> MySolution
        {
            get
            {
                return _mySolution;
            }

            set
            {
                _mySolution = value;
            }
        }
        /// <summary>
        /// 更目录路径
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
        /// 解决方案里面所有项目的集合
        /// </summary>
        public ObservableCollection<MyProjectFiles> MyProjectClassFile
        {
            get
            {
                return _myProjectClassFile;
            }

            set
            {
                _myProjectClassFile = value;
            }
        }
        /// <summary>
        /// 解决方案配置文件路径
        /// </summary>
        public string SolutionConfigPath
        {
            get
            {
                return _solutionConfigPath;
            }

            set
            {
                _solutionConfigPath = value;
            }
        }
        /// <summary>
        /// 配置文件扩展名
        /// </summary>
        public static string Exname
        {
            get
            {
                return _exname;
            }

            set
            {
                _exname = value;
            }
        }
        /// <summary>
        /// 项目语言
        /// </summary>
        public string ProjectLanguage
        {
            get
            {
                return _projectLanguage;
            }

            set
            {
                _projectLanguage = value;
            }
        }
        #endregion
        #region 自定义函数
        /// <summary>
        /// 获取第一个项目类
        /// </summary>
        /// <returns></returns>
        public ProjectClass GetFirstProject()
        {
            if(MySolution.Count > 0)
            {
                return MySolution[0];
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 添加一个项目文件
        /// </summary>
        /// <param name="pro">要添加的项目类</param>
        public void AddProjectClass(ProjectClass pro)
        {
            if (!MySolution.Contains(pro))
            {
                MySolution.Add(pro);
                MyProjectFiles file = new MyProjectFiles();
                file.Name = pro.ProjectName;
                file.Path = pro.FilesPath;
                MyProjectClassFile.Add(file);
            }
        }
        /// <summary>
        /// 删除一个项目
        /// </summary>
        /// <param name="pro">要删除的项目</param>
        public void DelProjectClass(ProjectClass pro)
        {
            if(MySolution.Contains(pro))
            {
                MySolution.Remove(pro);
                foreach(MyProjectFiles file in MyProjectClassFile)
                {
                    if(file.Name == pro.ProjectName)
                    {
                        MyProjectClassFile.Remove(file);
                    }
                }
            }
        }
        /// <summary>
        /// 保存解决方案
        /// </summary>
        public void SaveSolution()
        {
            ///更新解决方案配置文件
            UpdateConfigFile.UpdateSolutionConfig(MyProjectClassFile, SolutionConfigPath, ProjectClass.Version, ProjectClass.Editorname, ProjectLanguage);
            ///保存所有项目
            foreach(ProjectClass pro in MySolution)
            {
                pro.SaveProject();
            }
        }
        /// <summary>
        /// 加载项目
        /// </summary>
        /// <param name="path">文件完整路径</param>
        /// /// <param name="name">文件名字</param>
        public void LoadSolution(string path)
        {
            string FileDictionary = path.Substring(0,path.Length - Exname.Length) + "//";
            _rootPath = FileDictionary;
            XmlDocument XmlLoad = new XmlDocument();
            XmlLoad.Load(path);
            XmlElement rootElenemt = (XmlElement)XmlLoad.SelectSingleNode("Config");
            ProjectLanguage = rootElenemt.GetAttribute("ProjectLanguage");
            XmlElement projects = (XmlElement)rootElenemt.SelectSingleNode("Projects");
            foreach(XmlElement node in projects.SelectNodes("Project"))
            {
                AddProjectClass(GetProjectClass(node, FileDictionary));
            }
            XmlLoad.Save(path);
        }
        /// <summary>
        /// 获取一个项目类
        /// </summary>
        /// <param name="projectnode">xml项目节点</param>
        ///  /// <param name="dictionary">上级目录</param>
        /// <returns></returns>
        protected ProjectClass GetProjectClass(XmlElement projectnode,string dictionary)
        {
            string name = projectnode.GetAttribute("ProjectName");
            ProjectClass pro = new ProjectClass(this.ProjectLanguage);
            pro.RootPath = dictionary;
            pro.OutPath = dictionary + "bin//Debug//";
            pro.FilesPath = dictionary;
            pro.ConfigFilePath = dictionary + name;
            pro.ProjectName = name;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(pro.ConfigFilePath);
            XmlElement rootElenemts = (XmlElement)xmlDoc.SelectSingleNode("Config");
            //pro.ProjectName = rootElenemts.GetAttribute("ProjectName");
            XmlNodeList ProjectFileNodelist = rootElenemts.SelectNodes("ProjectFile");
            foreach (XmlElement enemt in ProjectFileNodelist)
            {
                ///获取文件名
                string filename = enemt.GetAttribute("FileName");
                PicTransfromXmlFile transf = new PicTransfromXmlFile();
                pro.AddPicTabPage(transf.XmlToPic(dictionary + filename));
            }
            ///保存关闭文件
            xmlDoc.Save(pro.ConfigFilePath);
            ///设置项目语言
            pro.Language = this.ProjectLanguage;
            return pro;
        }
        /// <summary>
        /// 编译代码
        /// </summary>
        /// <returns></returns>
        public string Editor()
        {
            string EditorMessage = "";
            ///如果是C#代码
            if (ProjectLanguage == "C#")
            {
                if (MyProjectClassFile.Count == 0)
                {
                    MessageBox.Show("还没有创建任何项目!");
                    return EditorMessage = "error";
                }
                ///编译所有的项目
                for (int nowcount = 0; nowcount < MySolution.Count; nowcount++)
                {
                    ProjectClass proclass = MySolution[nowcount];
                    string[] CodeString = new string[proclass.MyListPicPage.Count];
                    int i = 0;
                    XPicCodeToCSharpCode codeEditor = new XPicCodeToCSharpCode(proclass.ProjectName);
                    foreach (PicTabPage page in proclass.MyListPicPage.Values)
                    {
                        CodeString[i] = codeEditor.XPicCodeEditor(page);
                    }
                    string ErrorString = "";
                    CodeDestoryClass.AnalyTicalCode(CodeString, out ErrorString, proclass.OutPath, proclass.GetDllFilePathString(), proclass.ProjectName);
                    EditorMessage += ErrorString + "\r\n";
                }
            }
            else if(ProjectLanguage == "C")
            {
                if (MyProjectClassFile.Count == 0)
                {
                    MessageBox.Show("还没有创建任何项目!");
                    return EditorMessage = "error";
                }

                ProjectClass proclass = GetFirstProject();
                string CodeString = "";
                XPicCodeToCLanguagesCode codeEditor = new XPicCodeToCLanguagesCode(proclass.ProjectName);
               CodeString = codeEditor.XPicCodeEditor(proclass.GetFirstPicTabPage());
                string ErrorString = "";
                CodeDestoryClass.AnalyTicalCode(CodeString, out ErrorString, proclass.OutPath, proclass.ProjectName);
                EditorMessage += ErrorString + "\r\n";
                
            }
            return EditorMessage;
        }
        /// <summary>
        /// 获取翻译后的代码
        /// </summary>
        /// <returns>返回翻译后的代码</returns>
        private string[] GetCode()
        {
            ///如果是C#代码
            if (ProjectLanguage == "C#")
            {
                if (MyProjectClassFile.Count == 0)
                {
                    MessageBox.Show("还没有创建任何项目!");
                    return null;
                }
                ///编译所有的项目
                for (int nowcount = 0; nowcount < MySolution.Count; nowcount++)
                {
                    ProjectClass proclass = MySolution[nowcount];
                    string[] CodeString = new string[proclass.MyListPicPage.Count];
                    int i = 0;
                    XPicCodeToCSharpCode codeEditor = new XPicCodeToCSharpCode(proclass.ProjectName);
                    foreach (PicTabPage page in proclass.MyListPicPage.Values)
                    {
                        CodeString[i] = codeEditor.XPicCodeEditor(page);
                    }
                    return CodeString;
                }
                return null;
            }
            else if (ProjectLanguage == "C")
            {
                if (MyProjectClassFile.Count == 0)
                {
                    MessageBox.Show("还没有创建任何项目!");
                    return null;
                }

                ProjectClass proclass = GetFirstProject();
                string []CodeString = new string[1];
                XPicCodeToCLanguagesCode codeEditor = new XPicCodeToCLanguagesCode(proclass.ProjectName);
                CodeString[0] = codeEditor.XPicCodeEditor(proclass.GetFirstPicTabPage());
                return CodeString;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 保存生成的代码
        /// </summary>
        public void SaveCode()
        {
            //获取代码
            string[] codes = GetCode();

            if(codes == null || codes.Length <= 0)
            {
                return;
            }
            else
            {
                int i = 0;
                //循环保存代码
                foreach(string code in codes)
                {
                    //写入文件
                    LoggerHelp.WriteMessageToFile(code, "Code" + i++ + ".code", RootPath + _codeFilePath);
                }
            }
        }
        #endregion
    }
}
