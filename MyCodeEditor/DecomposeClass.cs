using MyProjectData;
using MyXTreeView;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Resources;

namespace MyXCodeEditor
{
    /// <summary>
    /// 解析类
    /// </summary>
    public class DecomposeClass
    {
        /// <summary>
        /// 读取资源
        /// </summary>
        /// <returns>返回读取的资源信息（资源名称和资源内容）</returns>
        public string ReadAssest()
        {
            string ContentList = "";

            Assembly assm = GetType().Assembly;//Assembly.LoadFrom(程序集路径);
            foreach (string resName in assm.GetManifestResourceNames())
            {
                Stream stream = assm.GetManifestResourceStream(resName);  //获取资源流
                ResourceReader rr = new ResourceReader(stream);
                IDictionaryEnumerator enumerator = rr.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    //de.Key是资源名
                    //de.Value是资源内容
                    DictionaryEntry de = (DictionaryEntry)enumerator.Current;
                    ContentList += "AssestName:" + de.Key + "\r\n";
                    ContentList += "AssestContent:" + de.Value + "\r\n";
                }
            }
            return ContentList;
        }
        /// <summary>
        /// 解析类型中的Fields内容
        /// </summary>
        /// <param name="type">要解析的类型</param>
        /// <returns>返回解析好的内容</returns>
        public static string ListFields(Type type)
        {
            string ContentList = "";
            ContentList += "******** Fields: ********\r\n";
            foreach (FieldInfo item in type.GetFields())
            {
                if (item.IsPrivate)
                {
                    ContentList += "private ";
                }
                else
                {
                    ContentList += "public ";
                }
                if (item.IsStatic)
                {
                    ContentList += "static ";
                }
                if (item.IsFamilyAndAssembly)
                {
                    ContentList += "internal ";
                }
                ContentList += item.FieldType.Name + " ";
                ContentList += item.Name;
                //ContentList += item.DeclaringType..ToString();
                ContentList += "\r\n";
            }
            return ContentList;
        }
        /// <summary>
        /// 解析类型中的Methods内容
        /// </summary>
        /// <param name="type">要解析的类型</param>
        /// <returns>返回解析好的内容</returns>
        public static string ListMethods(Type type)
        {
            string ContentList = "";
            ContentList += "******** Methods: ********\r\n";
            //var methodInfo = type.GetMethods().Select(m => m.Name).Distinct();//筛选不重复的函数名
            var methodInfo = type.GetMethods();
            foreach (var item in methodInfo)
            {
                ContentList += "//\r\n";
                ContentList += "// 声明类型的配置信息:\r\n";
                ContentList += "// " + item.DeclaringType.AssemblyQualifiedName + "\r\n";
                if (item.IsPrivate)
                {
                    ContentList += "private ";
                }
                else
                {
                    ContentList += "public ";
                }
                if (item.IsStatic)
                {
                    ContentList += "static ";
                }
                if (item.IsVirtual)
                {
                    ContentList += "virtual ";
                }
                if (item.IsAbstract)
                {
                    ContentList += "abstract ";
                }
                if (item.IsFinal)
                {
                    ContentList += "finally ";
                }

                ContentList += item.ReturnType.Name + " ";
                ContentList += item.Name + "(";
                ParameterInfo[] paramets = item.GetParameters();
                if (paramets.Length > 0)
                {
                    foreach (ParameterInfo pi in paramets)
                    {
                        ContentList += pi.ParameterType.Name + " " + pi.Name + ", ";
                    }
                    ContentList = ContentList.Substring(0, ContentList.Length - 2);
                }
                ContentList += ")\r\n";
            }
            return ContentList;
        }
        /// <summary>
        /// 解析类型中Interfaces的内容
        /// </summary>
        /// <param name="type">要解析的类型</param>
        /// <returns>返回解析好的内容</returns>
        public static string ListInterfaces(Type type)
        {
            string ContentList = "";
            ContentList += "******** Interfaces: ********\r\n";
            foreach (var item in type.GetInterfaces())
            {
                ContentList += "//\r\n";
                ContentList += "// 声明类型的配置信息:\r\n";
                ContentList += "//" + item.AssemblyQualifiedName + "\r\n";
                if (item.IsAbstract)
                {
                    ContentList += "abstract ";
                }
                ContentList += "->" + item.Name + "\r\n";
            }
            return ContentList;
        }
        /// <summary>
        /// 返回改类型中的Properties内容
        /// </summary>
        /// <param name="type">要解析的内容</param>
        /// <returns>返回解析好的内容</returns>
        public static string ListProperties(Type type)
        {
            string ContentList = "";

            ContentList += "******** Properties: ********\r\n";
            foreach (var item in type.GetProperties())
            {
                ContentList += "->" + item.Name + "\r\n";
            }
            return ContentList;
        }
        /// <summary>
        /// 通过反射解析类
        /// </summary>
        /// <param name="typeStr">要解析的类名</param>
        /// <returns>返回解析后的内容</returns>
        public static string ReadDll(string typeStr)
        {
            string ContentList = "";
            try
            {
                Type type = Type.GetType(typeStr);
                ContentList += ListFields(type);
                ContentList += ListMethods(type);
                ContentList += ListInterfaces(type);
                ContentList += ListProperties(type);
                return ContentList;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #region 解析Dll文件
        /// <summary>
        /// 把dll文件转换为可以利用的资源
        /// </summary>
        /// <param name="dll">dll文件的路径</param>
        /// /// <param name="ProjectClassType">项目可选类型</param>
        /// <returns>返回可以利用的资源</returns>
        public static MyXTreeItem GetDllResources(DllFile dll, Dictionary<string, string> ProjectClassType = null)
        {
            Assembly assembly = Assembly.LoadFrom(dll.Path);
            MyXTreeItem ReData = new MyXTreeItem();
            ReData.XName = dll.Name;
            ReData.TypeImagePath = "";
            ///获取所有的类型
            Type []alltypes = assembly.GetTypes();
            ///循环所有类型
            foreach(Type item in alltypes)
            {
                ///如果传进来一个类型变量引用
                if(ProjectClassType != null)
                {
                    ///添加可选类型
                    ProjectClassType.Add(item.FullName, item.Name);
                }
                MyXTreeItem classdata = new MyXTreeItem();
                classdata.XName = item.Namespace + "." + item.Name;
                classdata.TypeImagePath = "";
                #region 添加Proper内容
                ///获取所有访问器属性
                PropertyInfo[] pros = item.GetProperties();
                ///循环所有访问器
                MyXTreeItem prosdata = new MyXTreeItem();
                prosdata.XName = "Properties";
                prosdata.TypeImagePath = "";
                foreach (PropertyInfo info in pros)
                {
                    if (info.GetGetMethod(false) != null)
                    {///具体的访问器属性(GET)
                        MyXTreeItem proindataGet = new MyXTreeItem();
                        proindataGet.XName = "Get" + info.Name;
                        proindataGet.MyCodeBoxType = MyCodeBox.CodeBox.XAType.get;
                        proindataGet.TypeImagePath = "";
                        proindataGet.MyHitText = "";
                        ///添加一个该属性的Get代码块(get方法)
                        SetPropGetValue(proindataGet, info);
                        prosdata.ChildrenItem.Add(proindataGet);
                    }

                    if (info.GetSetMethod(false) != null)
                    {///具体的访问器属性(SET)
                        MyXTreeItem proindataSet = new MyXTreeItem();
                        proindataSet.XName = "Set" + info.Name;
                        proindataSet.MyCodeBoxType = MyCodeBox.CodeBox.XAType.set;
                        proindataSet.TypeImagePath = "";
                        proindataSet.MyHitText = "";
                        ///添加一个该属性的Get代码块(get方法)
                        SetPropSetValue(proindataSet, info);
                        prosdata.ChildrenItem.Add(proindataSet);
                    }
                }
                classdata.ChildrenItem.Add(prosdata);
                #endregion
                #region 添加fieldinfos内容
                ///获取该类型中所有的字段
                FieldInfo[] fieldinfos = item.GetFields();
                MyXTreeItem fieldinfosdata = new MyXTreeItem();
                fieldinfosdata.XName = "Fieldinfos";
                fieldinfosdata.TypeImagePath = "";
                fieldinfosdata.MyHitText = "";
                foreach (FieldInfo info in fieldinfos)
                {
                    ///添加Get类型
                    MyXTreeItem fieldinfosdataGet = new MyXTreeItem();
                    fieldinfosdataGet.XName = "Get" + info.Name;
                    fieldinfosdataGet.MyCodeBoxType = MyCodeBox.CodeBox.XAType.get;
                    fieldinfosdataGet.TypeImagePath = "";
                    fieldinfosdataGet.MyHitText = "";
                    ///添加一个该属性的Get代码块(get方法)
                    SetPropGetValue(fieldinfosdataGet, info, classdata.XName);
                    fieldinfosdata.ChildrenItem.Add(fieldinfosdataGet);
                    
                    ///添加Set类型
                    MyXTreeItem fieldinfosdataSet = new MyXTreeItem();
                    fieldinfosdataSet.XName = "Set" + info.Name;
                    fieldinfosdataSet.MyCodeBoxType = MyCodeBox.CodeBox.XAType.set;
                    fieldinfosdataSet.TypeImagePath = "";
                    fieldinfosdataSet.MyHitText = "";
                    ///添加一个该属性的Get代码块(get方法)
                    SetPropSetValue(fieldinfosdataSet, info, classdata.XName);
                    fieldinfosdata.ChildrenItem.Add(fieldinfosdataSet);
                    
                }
                classdata.ChildrenItem.Add(fieldinfosdata);
                #endregion
                #region 添加方法
                MethodInfo [] methodlinfos = item.GetMethods();
                MyXTreeItem methodlinfodata = new MyXTreeItem();
                methodlinfodata.XName = "公共方法集合";
                methodlinfodata.TypeImagePath = "";
                methodlinfodata.MyHitText = "该类型的所有方法";
                foreach(MethodInfo info in methodlinfos)
                {
                    if (!((info.Name.Substring(0, 4) == "set_" || info.Name.Substring(0, 4) == "get_") && info.IsSpecialName))
                    {
                        ///添加一个方法数据
                        MyXTreeItem methodlnfoitem = new MyXTreeItem();
                        methodlnfoitem.XName = info.Name;
                        //methodlinfodata.MyCodeBoxType = MyCodeBox.CodeBox.XAType.XFunction;
                        methodlnfoitem.TypeImagePath = "";
                        methodlnfoitem.MyHitText = "";
                        SetMethodInfoXAribute(methodlnfoitem, info);
                        ///添加一个方法
                        methodlinfodata.ChildrenItem.Add(methodlnfoitem);
                    }
                }
                classdata.ChildrenItem.Add(methodlinfodata);
                #endregion
                ReData.ChildrenItem.Add(classdata);
            }
            return ReData;
        }

        #region 属性和字段的方法
        /// <summary>
        /// 设置获取属性(Get)
        /// </summary>
        /// <param name="item">属性容器</param>
        /// <param name="info">属性内容</param>
        /// /// <param name="TargetExname">该属性所属的类</param>
        public static void SetPropGetValue(MyXTreeItem item, PropertyInfo info)
        {
            ///添加一个正常的属性
            AddNormailzeXAribute(item, info, 1);
            ///添加目标所有者
            AddTargetXAribute(item, info.DeclaringType.Name, info.DeclaringType.Namespace);
        }
        /// <summary>
        /// 设置获取属性(Set)
        /// </summary>
        /// <param name="item">属性容器</param>
        /// <param name="info">属性内容</param>
        /// /// <param name="TargetExname">该属性所属的类</param>
        public static void SetPropSetValue(MyXTreeItem item, PropertyInfo info)
        {
            ///添加一个出口属性
            AddExcXAribute(item);
            ///添加一个入口函数
            AddEnterXAribute(item);
            ///添加一个正常的属性
            AddNormailzeXAribute(item, info, 1);
            ///添加一个正常的属性
            AddNormailzeXAribute(item, info, 0);
            ///添加目标所有者
            AddTargetXAribute(item, info.DeclaringType.Name,info.DeclaringType.Namespace);
        }
        /// <summary>
        /// 设置获取属性(Get)
        /// </summary>
        /// <param name="item">属性容器</param>
        /// <param name="info">属性内容</param>
        /// /// <param name="TargetExname">该属性所属的类</param>
        public static void SetPropGetValue(MyXTreeItem item, FieldInfo info, string TargetExname)
        {
            ///添加一个正常的属性
            AddNormailzeXAribute(item, info, 1);
            ///添加目标所有者
            AddTargetXAribute(item, info.DeclaringType.Name, info.DeclaringType.Namespace);
        }
        /// <summary>
        /// 设置获取属性(Set)
        /// </summary>
        /// <param name="item">属性容器</param>
        /// <param name="info">属性内容</param>
        /// /// <param name="TargetExname">该属性所属的类</param>
        public static void SetPropSetValue(MyXTreeItem item, FieldInfo info, string TargetExname)
        {
            ///添加一个出口属性
            AddExcXAribute(item);
            ///添加一个入口函数
            AddEnterXAribute(item);
            ///添加一个正常的属性
            AddNormailzeXAribute(item, info, 1);
            ///添加一个正常的属性
            AddNormailzeXAribute(item, info, 0);
            ///添加目标所有者
            AddTargetXAribute(item, info.DeclaringType.Name, info.DeclaringType.Namespace);
        }
        /// <summary>
        ///  添加一个出口属性节点
        /// </summary>
        /// <param name="item">要添加的节点</param>
        public static void AddExcXAribute(MyXTreeItem item)
        {
            XAributeItem xaitem = new XAributeItem("出口", MyXAribute.XAribute.CanLinkType.One, MyXAribute.XAribute.XAttributeSpec.XNone
                , MyXAribute.XAribute.XAttributeType.XExc, MyXAribute.XAribute.XPositonStyle.right, "出口节点", "");
            item.MyXaributeChildren.Add(xaitem);
        }
        /// <summary>
        /// 添加一个入口的属性节点
        /// </summary>
        /// <param name="item">要添加结点</param>
        public static void AddEnterXAribute(MyXTreeItem item)
        {
            XAributeItem xaitem = new XAributeItem("入口", MyXAribute.XAribute.CanLinkType.More, MyXAribute.XAribute.XAttributeSpec.XNone
                , MyXAribute.XAribute.XAttributeType.XEnter, MyXAribute.XAribute.XPositonStyle.Left, "入口", "");
            item.MyXaributeChildren.Add(xaitem);
        }
        /// <summary>
        /// 添加一个正常的属性
        /// </summary>
        /// <param name="item">要添加属性的结点</param>
        /// <param name="info">属性具体内容</param>
        /// /// <param name="type">作为输入还是输出(0 为输入 1 位输出)</param>
        public static void AddNormailzeXAribute(MyXTreeItem item, FieldInfo info, int type)
        {
            XAributeItem xaitem = new XAributeItem(info.Name, type == 0 ? MyXAribute.XAribute.CanLinkType.One : MyXAribute.XAribute.CanLinkType.More,
                MyXAribute.XAribute.XAttributeSpec.XNone, MyXAribute.XAribute.XAttributeType.XClass, type == 0 ? MyXAribute.XAribute.XPositonStyle.Left : MyXAribute.XAribute.XPositonStyle.right,
                "属性的类型：" + info.DeclaringType.Namespace + "." + info.DeclaringType.Name, info.DeclaringType.Namespace + "." + info.DeclaringType.Name);
            item.MyXaributeChildren.Add(xaitem);
        }
        /// <summary>
        /// 添加一个正常的属性
        /// </summary>
        /// <param name="item">要添加属性的结点</param>
        /// <param name="info">属性具体内容</param>
        /// /// <param name="type">作为输入还是输出(0 为输入 1 位输出)</param>
        public static void AddNormailzeXAribute(MyXTreeItem item, PropertyInfo info, int type)
        {
            XAributeItem xaitem = new XAributeItem(info.Name, type == 0 ? MyXAribute.XAribute.CanLinkType.One : MyXAribute.XAribute.CanLinkType.More,
                MyXAribute.XAribute.XAttributeSpec.XNone, MyXAribute.XAribute.XAttributeType.XClass, type == 0 ? MyXAribute.XAribute.XPositonStyle.Left : MyXAribute.XAribute.XPositonStyle.right,
                "属性类型：" + info.DeclaringType.Namespace + "." + info.DeclaringType.Name , info.DeclaringType.Namespace + "." + info.DeclaringType.Name);
            item.MyXaributeChildren.Add(xaitem);
        }
        /// <summary>
        /// 添加父类目标
        /// </summary>
        /// <param name="item">结点</param>
        /// <param name="exname">类名</param>
        /// /// <param name="nameSpace">类命名空间</param>
        public static void AddTargetXAribute(MyXTreeItem item, string exname, string nameSpace)
        {
            XAributeItem xaitem = new XAributeItem("Target", MyXAribute.XAribute.CanLinkType.One, MyXAribute.XAribute.XAttributeSpec.XNone
                , MyXAribute.XAribute.XAttributeType.XTarget, MyXAribute.XAribute.XPositonStyle.Left, "该成员的所有者类型为：" + nameSpace + "." + exname, nameSpace + exname);
            item.MyXaributeChildren.Add(xaitem);
        }
        #endregion

        #region 方法的方法
        /// <summary>
        /// 设置方法的代码块
        /// </summary>
        /// <param name="item">要设置属性的节点</param>
        /// <param name="methodlinfo">方法信息</param>
        public static void SetMethodInfoXAribute(MyXTreeItem item, MethodInfo methodlinfo)
        {
            if(methodlinfo.ReturnType.Name == "void" || methodlinfo.ReturnType.Name == "Void")
            {
                AddEnterXAribute(item);
                AddExcXAribute(item);
                item.MyCodeBoxType = MyCodeBox.CodeBox.XAType.XFunction;
            }
            else
            {
                AddReturnTypeXAribute(item, methodlinfo.ReturnType);
                item.MyCodeBoxType = MyCodeBox.CodeBox.XAType.GetValue;
            }
            ParameterInfo[] parameterinfos = methodlinfo.GetParameters();
            foreach(ParameterInfo parinfo in parameterinfos)
            {
                AddParameterXAribute(item, parinfo);
            }
            ///添加目标所有者
            AddTargetXAribute(item, methodlinfo.DeclaringType.Name, methodlinfo.DeclaringType.Namespace);
        }
        /// <summary>
        /// 添加一个参数
        /// </summary>
        /// <param name="item">要添加参数的节点</param>
        /// <param name="info">要添加的参数信息</param>
        public static void AddParameterXAribute(MyXTreeItem item, ParameterInfo info)
        {
            XAributeItem xaitem = new XAributeItem(info.Name, MyXAribute.XAribute.CanLinkType.One, MyXAribute.XAribute.XAttributeSpec.XNone
                , MyXAribute.XAribute.XAttributeType.XClass, MyXAribute.XAribute.XPositonStyle.Left, "参数的类型" +  info.ParameterType.Namespace + "." + info.ParameterType.Name ,
                info.ParameterType.Namespace + "." + info.ParameterType.Name);
            item.MyXaributeChildren.Add(xaitem);
        }
        /// <summary>
        /// 添加一个返回值参数
        /// </summary>
        /// <param name="item">要添加返回值的节点</param>
        /// <param name="type">返回值的类型</param>
        public static void AddReturnTypeXAribute(MyXTreeItem item, Type type)
        {
            XAributeItem xaitem = new XAributeItem(type.Name, MyXAribute.XAribute.CanLinkType.More, MyXAribute.XAribute.XAttributeSpec.XNone,
                 MyXAribute.XAribute.XAttributeType.XClass, MyXAribute.XAribute.XPositonStyle.right, "类型: " + type.Namespace + "." + type.Name,
                 type.Namespace + "." +type.Name);
            item.MyXaributeChildren.Add(xaitem);
        }
        #endregion
        #endregion
    }
}
