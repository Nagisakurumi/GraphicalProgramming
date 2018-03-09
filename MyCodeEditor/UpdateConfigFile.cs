using MyProjectData;
using System.Collections.ObjectModel;
using System.Xml;

namespace MyXCodeEditor
{
    public static class UpdateConfigFile
    {
        #region 自定义方法
        /// <summary>
        /// 更新本项目文件配置
        /// </summary>
        /// <param name="MyListProjectFiles">文件集合</param>
        /// <param name="configpath">配置文件路径</param>
        /// /// <param name="configpath">项目名称</param>
        public static void UpdateConfig(ObservableCollection<MyProjectFiles> MyListProjectFiles,string configpath,string projectname)
        {
            XmlDocument XmlLoad = new XmlDocument();
            XmlLoad.Load(configpath);
            XmlElement configroot = (XmlElement)XmlLoad.SelectSingleNode("Config");
            configroot.RemoveAll();
            configroot.SetAttribute("ProjectName", projectname);
            foreach (MyProjectFiles file in MyListProjectFiles)
            {
                XmlElement filenode = XmlLoad.CreateElement("ProjectFile");
                filenode.SetAttribute("Type", "MainFunction");
                filenode.SetAttribute("FileName", file.Name);
                configroot.AppendChild(filenode);
            }
            ///保存文件
            XmlLoad.Save(configpath);
        }
        /// <summary>
        /// 更新解决方案配置
        /// </summary>
        /// <param name="MyProjectClassFiles">解决方案里面的项目集合</param>
        /// <param name="configpath">解决方案的配置文件路径</param>
        /// <param name="Version">版本号</param>
        /// <param name="Editorname">编译器名称</param>
        /// <param name="language">所用语言</param>
        public static void UpdateSolutionConfig(ObservableCollection<MyProjectFiles> MyProjectClassFiles, string configpath,string Version, string Editorname, string language)
        {
            XmlDocument XmlLoad = new XmlDocument();
            XmlLoad.Load(configpath);
            XmlElement configroot = (XmlElement)XmlLoad.SelectSingleNode("Config");
            configroot.RemoveAll();
            configroot.SetAttribute("ProjectLanguage", language);
            configroot.SetAttribute("版本号", Version);
            configroot.SetAttribute("编辑器", Editorname);
            XmlElement projects = XmlLoad.CreateElement("Projects");
            foreach (MyProjectFiles file in MyProjectClassFiles)
            {
                XmlElement filenode = XmlLoad.CreateElement("Project");
                filenode.SetAttribute("ProjectName", file.Name);
                projects.AppendChild(filenode);
            }
            configroot.AppendChild(projects);
            XmlLoad.Save(configpath);
        }
        #endregion
    }
}
