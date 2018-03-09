using MyPicTabPage;
using MyProjectData;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MyXCodeDataOption;
using MyXTreeView;
using MyXCodeEditor;
using MyXAribute;
using System;
using MyXObject;
using MyCodeBox;

namespace MyProjectClassData
{
    public class ProjectClass
    {
        #region 枚举值
        /// <summary>
        /// 所用语言类型
        /// </summary>
        public enum LanguageType
        {
            /// <summary>
            /// C#类型
            /// </summary>
            CSharp = 0,
            /// <summary>
            /// C语言类型
            /// </summary>
            C = 1
        }
        #endregion
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public ProjectClass(string language)
        {
            ///设置语言
            this.Language = language;
            /////初始化数据
            //ModityPicTabPageFunction();
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public ProjectClass()
        {

        }
        #endregion
        #region 属性
        /// <summary>
        /// 项目中可以调用的类
        /// </summary>
        private Dictionary<string, string> _projectUseClassType = new Dictionary<string, string>();
        /// <summary>
        /// 项目类型
        /// </summary>
        private string _projectType = "";
        /// <summary>
        /// 版本号
        /// </summary>
        private static string _version = "1.0.0";
        /// <summary>
        /// 编译器名称
        /// </summary>
        private static string _editorname = "MyEdtior";
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
        /// 配置文件路径
        /// </summary>
        private string _configFilePath = "";
        /// <summary>
        /// 图标路径
        /// </summary>
        public string DllIcon = "";
        /// <summary>
        ///   项目Dll文件路径
        /// </summary>
        private ObservableCollection<DllFile> _myDllFiles = new ObservableCollection<DllFile>();
        /// <summary>
        /// 代码类的集合
        /// </summary>
        private Dictionary<string, PicTabPage> _myListPicPage = new Dictionary<string, PicTabPage>();
        /// <summary>
        /// 项目文件的集合
        /// </summary>
        private ObservableCollection<MyProjectFiles> _myListProjectFiles = new ObservableCollection<MyProjectFiles>();
        /// <summary>
        /// 项目名称
        /// </summary>
        private string _projectName = "";
        /// <summary>
        /// 语言类型
        /// </summary>
        private string _language = "";
        /// <summary>
        /// 创建数据源
        /// </summary>
        private ObservableCollection<MyXTreeItem> DataSource = new ObservableCollection<MyXTreeItem>();
        #endregion
        #region 读取器
        /// <summary>
        ///   项目Dll文件路径
        /// </summary>
        public ObservableCollection<DllFile> MyDllFiles
        {
            get
            {
                return _myDllFiles;
            }
        }
        /// <summary>
        /// 代码类的集合
        /// </summary>
        public Dictionary<string, PicTabPage> MyListPicPage
        {
            get
            {
                return _myListPicPage;
            }

            set
            {
                _myListPicPage = value;
            }
        }
        /// <summary>
        /// 项目文件集合
        /// </summary>
        public ObservableCollection<MyProjectFiles> MyListProjectFiles
        {
            get
            {
                return _myListProjectFiles;
            }

            set
            {
                _myListProjectFiles = value;
            }
        }
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
        /// 配置文件路径
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
        /// 项目类型
        /// </summary>
        public string ProjectType
        {
            get
            {
                return _projectType;
            }

            set
            {
                _projectType = value;
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
        /// <summary>
        /// 项目中可以调用的类
        /// </summary>
        public Dictionary<string, string> ProjectUseClassType
        {
            get
            {
                return _projectUseClassType;
            }

            set
            {
                _projectUseClassType = value;
            }
        }
        /// <summary>
        /// 所用语言
        /// </summary>
        public string Language
        {
            get
            {
                return _language;
            }

            set
            {
                _language = value;
                
            }
        }
        #endregion
        #region 自定义方法
        /// <summary>
        /// 删除一个dll文件
        /// </summary>
        /// <param name="path"></param>
        public void DeletDllFilePath(DllFile path)
        {
            MyDllFiles.Remove(path);
            ModityPicTabPageFunction();
        }
        /// <summary>
        /// 添加一个dll文件
        /// </summary>
        /// <param name="path"></param>
        public void AddDllFilePath(DllFile path)
        {
            MyDllFiles.Add(path);
            ModityPicTabPageFunction();
        }
        /// <summary>
        /// 添加一个代码类
        /// </summary>
        /// <param name="page"></param>
        public void AddPicTabPage(PicTabPage page)
        {
            while (true)
            {
                if (MyListPicPage.ContainsKey(page.Title))
                {
                    page.Title = page.Title + "1";                    
                }
                else
                {
                    break;
                }
            }
            MyListPicPage.Add(page.Title, page);
            ///设置回调函数
            page.MessageUpdateCall = this.ChileEventCallBack;
            MyProjectFiles file = new MyProjectFiles();
            file.Name = page.Title;
            file.Path = FilesPath + page.Title;
            MyListProjectFiles.Add(file);
            page.PopContentCode.MyData = DataSource;
            ///初始化数据
            ModityPicTabPageFunction();
            ///添加数据
            foreach (XAribute bute in page.ListXAributes)
            {
                ///添加类型
                bute.ProjectUseClassType = this.ProjectUseClassType;
            }

        }
        /// <summary>
        /// 获取dll文件路径
        /// </summary>
        /// <returns>返回path路径集合</returns>
        public string [] GetDllFilePathString()
        {
            string[] dllstring = new string[MyDllFiles.Count];
            for(int i = 0;i < MyDllFiles.Count; i ++)
            {
                dllstring[i] = MyDllFiles[i].Path;
            }
            return dllstring;
        }
        /// <summary>
        /// 修改类图里面的可调用方法
        /// </summary>
        public void ModityPicTabPageFunction()
        {
            ///添加CSharp基本类型
            AddBaseClassType();
            DataSource.Clear();
            ///初始化系统读取的数据
            XCodeDataOption dataoption = new XCodeDataOption();
            ///声明系统初始数据
            MyXTreeItem sysdataitem;
            if (Language == "C#")
            {
                sysdataitem = dataoption.LoadXSystemFile();
                DataSource.Add(sysdataitem);
                ///初始化Dll文件里面的数据
                foreach (DllFile dll in MyDllFiles)
                {
                    MyXTreeItem dllitem = DecomposeClass.GetDllResources(dll, ProjectUseClassType);
                    DataSource.Add(dllitem);
                }           
            }
            else if(Language == "C")
            {
                sysdataitem = dataoption.LoadCLanguageXSystemFile();
                DataSource.Add(sysdataitem);
            }
            foreach (PicTabPage page in MyListPicPage.Values)
            {
                page.PopContentCode.MyData = DataSource;
                foreach (XAribute bute in page.ListXAributes)
                {
                    ///添加类型
                    bute.ProjectUseClassType = ProjectUseClassType;
                }
            }
        }
        /// <summary>
        /// 某个类添加了一个函数更新通知
        /// </summary>
        ///  <param name="page">添加了函数的类</param>
        /// <param name="fun">被添加的函数</param>
        public void AddPicFunctionToPicData(PicTabPage page, PicFunctionTabPage fun)
        {
            ///新建项
            MyXTreeItem item = new MyXTreeItem();
            item.ChildrenItem = null;
            item.MyCodeBoxType = CodeBox.XAType.XFunction;
            item.MyHitText = fun.Hint;
            item.TypeImagePath = "";
            item.XName = fun.Title;
            List<XAributeItem> xaitem = new List<XAributeItem>();
            ///如果是C#则要加上函数的所属
            if (Language == LanguageType.CSharp.ToString())
            {
                XAributeItem buteitem = new XAributeItem();
                buteitem.MyXPositonStyle = XAribute.XPositonStyle.Left;
                buteitem.Parameter_name = "Target";
                buteitem.MyCanLinkType = XAribute.CanLinkType.One;
                buteitem.MyXAttributeSpec = XAribute.XAttributeSpec.XNone;
                buteitem.MyXAttributeType = XAribute.XAttributeType.XTarget;
                buteitem.MyHittext = "函数所属的类";
                buteitem.MyLastExText = page.Title;
                xaitem.Add(buteitem);
            }
            //else if (Language == LanguageType.C.ToString())
            //{

            //}
            ///循环参数
            foreach (XAribute bute in fun.FunctionEnterBox.RightAribute.Children)
            {
                if(bute.SelectType != XAribute.XAttributeType.XEnter)
                {
                    ///填装信息
                    XAributeItem buteitem = new XAributeItem(bute);
                    //buteitem.Parameter_name = bute.Title;
                    //buteitem.MyCanLinkType = bute.CanLinkNum;
                    //buteitem.MyXAttributeSpec = bute.SelectSpc;
                    //buteitem.MyXAttributeType = bute.SelectType;
                    buteitem.MyXPositonStyle = XAribute.XPositonStyle.Left;
                    //buteitem.MyHittext = bute.Hint;
                    //buteitem.MyLastExText = bute.ExName;
                    ///加入列表
                    xaitem.Add(buteitem);
                }
            }
            ///组成函数的右半边属性
            foreach(XAribute bute in fun.FunctionExcBox.LeftAribute.Children)
            {
                if(bute.SelectType != XAribute.XAttributeType.XExc)
                {
                    XAributeItem buteitem = new XAributeItem(bute);
                    buteitem.MyXPositonStyle = XAribute.XPositonStyle.right;
                    xaitem.Add(buteitem);
                }
            }
            item.MyXaributeChildren = xaitem;
            bool isExect = false;  ///是否已经存在类
            MyXTreeItem UserTreeItem = null;
            foreach (MyXTreeItem myitem in DataSource)
            {
                if (myitem.XName == page.Title)
                {
                    isExect = true;
                    myitem.ChildrenItem.Add(item);
                    return;
                }
            }
            if (!isExect && UserTreeItem == null)
            {
                UserTreeItem = new MyXTreeItem();
                UserTreeItem.XName = page.Title;
                UserTreeItem.ChildrenItem.Add(item);
                ///添加信息和修改信息
                DataSource.Add(UserTreeItem);
            }
            //ModityPicTabPageFunction();
        }
        /// <summary>
        /// 某个类更新了一个函数属性的通知
        /// </summary>
        /// <param name="page">类</param>
        /// <param name="fun">函数</param>
        public void UpdatePicFunctionToPicData(PicTabPage page, PicFunctionTabPage fun)
        {
            MyXTreeItem delpicitem = null;
            MyXTreeItem delfunitem = null;
            foreach(MyXTreeItem item in DataSource)
            {
                if(item.XName == page.Title)
                {
                    delpicitem = item;
                    foreach (MyXTreeItem funitem in item.ChildrenItem)
                    {
                        if(funitem.XName == fun.Title)
                        {
                            delfunitem = funitem;
                        }
                    }
                }
            }
            if(delpicitem == null || delfunitem == null)
            {
                return;
            }
            delpicitem.ChildrenItem.Remove(delfunitem);
            ///重新添加函数信息
            AddPicFunctionToPicData(page, fun);
        }
        /// <summary>
        /// 保存这个项目的所有文件
        /// </summary>
        public void SaveProject()
        {
            ///保存配置文件
            UpdateConfigFile.UpdateConfig(MyListProjectFiles, ConfigFilePath,ProjectName);
            ///保存如文件
            PicTransfromXmlFile writefile = new PicTransfromXmlFile();
            ///循环所有代码
            foreach (PicTabPage page in MyListPicPage.Values)
            {
                writefile.PicToXml(page, FilesPath);
            }
        }
        /// <summary>
        /// 返回第一个代码图
        /// </summary>
        /// <returns></returns>
        public PicTabPage GetFirstPicTabPage()
        {
            foreach(PicTabPage page in MyListPicPage.Values)
            {
                return page;
            }
            return null;
        }
        /// <summary>
        /// 添加一个类型
        /// </summary>
        /// <param name="FullName">带命名空间的全名</param>
        /// <param name="Name">类型名称</param>
        public void AddProjectClassType(string FullName, string Name)
        {
            ///如果不存在则添加
            if(!ProjectUseClassType.ContainsKey(FullName))
            {
                ProjectUseClassType.Add(FullName, Name);
            }
        }
        /// <summary>
        /// 清理所有类型
        /// </summary>
        public void ClearProjectClassType()
        {
            ProjectUseClassType.Clear();
        }
        /// <summary>
        /// 根据带命名空间的类型名称删除一个特定的节点
        /// </summary>
        /// <param name="FullName">带命名空间的字符串</param>
        public void DelProjectClassType(string FullName)
        {
            if (ProjectUseClassType.ContainsKey(FullName))
            {
                ProjectUseClassType.Remove(FullName);
            }
        }
        /// <summary>
        /// 添加基础类型
        /// </summary>
        protected void AddBaseClassType()
        {
            ProjectUseClassType.Clear();
            if (Language == "C#")
            {
                ProjectUseClassType.Add("System.Int32", "整形");
                ProjectUseClassType.Add("System.Single", "单精度浮点型");
                ProjectUseClassType.Add("System.Double", "双精度浮点型");
                ProjectUseClassType.Add("System.String", "字符串类型");
                ProjectUseClassType.Add("System.Boolean", "真假类型");
                ProjectUseClassType.Add("System.Byte", "8位无符号整数");
            }
            else if(Language == "C")
            {
                ProjectUseClassType.Add("System.Int32", "整形");
                ProjectUseClassType.Add("System.Single", "单精度浮点型");
                ProjectUseClassType.Add("System.Double", "双精度浮点型");
                ProjectUseClassType.Add("System.Boolean", "真假类型");
                ProjectUseClassType.Add("System.Char", "字符类型");
            }
        }
        #endregion
        #region 事件回调
        /// <summary>
        /// 子控件事件回调处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ms"></param>
        protected virtual object ChileEventCallBack(Object sender, MouseState ms, XObjectData data = null) 
        {
            switch(ms)
            {
                case MouseState.XUpdatePropertyData:
                    XAribute bute = sender as XAribute;
                    if(bute != null)
                    {
                        //更新数据
                        bute.ProjectUseClassType = ProjectUseClassType;
                    }
                    break;
                case MouseState.XUpdateTreeViewData:
                    PicTabPage page = sender as PicTabPage;
                    if(page != null && (data.data as PicFunctionTabPage) != null)
                    {
                        MessageOption option = XObject.MessageOptionTypeMapping(data.additional_Information.ToString());
                        if ( option == MessageOption.Add)
                            ///更新函数
                            AddPicFunctionToPicData(page, data.data as PicFunctionTabPage);
                        else if(option == MessageOption.Update)
                        {
                            UpdatePicFunctionToPicData(page, data.data as PicFunctionTabPage);
                        }
                    }
                    break;
            }
            return null;
        }
        #endregion
    }  
}
