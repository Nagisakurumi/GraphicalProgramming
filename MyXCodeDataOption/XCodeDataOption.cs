using MyCodeBox;
using MyXAribute;
using MyXAributeDataItem;
using MyXObject;
using MyXTreeView;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;


namespace MyXCodeDataOption
{
    public class XCodeDataOption
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public XCodeDataOption()
        {

        }
        #endregion
        #region 自定义属性        
        /// <summary>
        /// 系统文件夹路径
        /// </summary>
        private string path = Application.StartupPath + "//XCodeBoxXml";
        /// <summary>
        /// 系统文件名称
        /// </summary>
        private string XSystemFileName = "//XSystem";
        /// <summary>
        /// 作为C语言的话系统文件名称
        /// </summary>
        private string XCSystemFileName = "//XCLanguageSystem";
        /// <summary>
        /// 存放这类文件的目录名称
        /// </summary>
        private string DictionaryName = "//XCodeBoxXml";
        /// <summary>
        /// 用户文件名
        /// </summary>
        private string XUserFileName = "//UserFile";
        /// <summary>
        /// 系统文件根节点的 名称
        /// </summary>
        private string RootName = "XSystem";
        #endregion
        #region 自定义方法
        /// <summary>
        /// 检查文件是否存在
        /// </summary>
        /// <returns></returns>
        public static bool CheckFile(String path)
        {
            if (!File.Exists(path))
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 当写入XML
        /// </summary>
        /// <param name="node">要写入的节点</param>
        /// <param name="write">XML操作对象</param>
        protected void XMLWriteFile(XAributeDataItem node, XmlTextWriter writer)
        {
            writer.WriteStartElement("XAribute");
            writer.WriteAttributeString("Name", node.Name);
            writer.WriteElementString("Icon", node.Icon);
            writer.WriteElementString("PointTypeitem", node.PointTypeitem.Key.ToString());
            //writer.WriteAttributeString("PointTypeitem_key", node.PointTypeitem.Key.ToString());
            writer.WriteElementString("ListTypeitem", node.ListTypeitem.Key.ToString());
            //writer.WriteAttributeString("ListTypeitem_key", node.ListTypeitem.Key.ToString());
            writer.WriteElementString("PositionTypeitem", node.PositionTypeitem.Key.ToString());
            //writer.WriteAttributeString("PositionTypeitem_key", node.PositionTypeitem.Key.ToString());
            writer.WriteElementString("LinkTypeitem", node.LinkTypeitem.Key.ToString());
            //writer.WriteAttributeString("LinkTypeitem_key", node.LinkTypeitem.Key.ToString());
            writer.WriteElementString("TipTypeitem", node.TipTypeitem);
            writer.WriteElementString("LastExTexteitem", node.LastExTexteitem);
            /// 关闭XAribute元素节点
            writer.WriteEndElement();
        }
        /// <summary>
        /// 向文档追加内容时候
        /// </summary>
        /// <param name="node">内容节点</param>
        /// <param name="writer">操作对象</param>
        /// <param name="goalNode">子内容要追到的节点</param>
        protected void XMLAppendFile(XAributeDataItem node, XmlDocument XmlAppend,XmlElement goalNode)
        {
            XmlElement aribute = XmlAppend.CreateElement("XAribute");
            aribute.SetAttribute("Name", node.Name);
            XmlElement Icon = XmlAppend.CreateElement("Icon");
            Icon.InnerText = node.Icon;
            aribute.AppendChild(Icon);
            XmlElement PointTypeitem = XmlAppend.CreateElement("PointTypeitem");
            //PointTypeitem.SetAttribute("PointTypeitem_key", node.PointTypeitem.Key.ToString());
            PointTypeitem.InnerText = node.PointTypeitem.Key.ToString();
            aribute.AppendChild(PointTypeitem);
            XmlElement ListTypeitem = XmlAppend.CreateElement("ListTypeitem");
            //ListTypeitem.SetAttribute("ListTypeitem_key", node.ListTypeitem.Key.ToString());
            ListTypeitem.InnerText = node.ListTypeitem.Key.ToString();
            aribute.AppendChild(ListTypeitem);
            XmlElement PositionTypeitem = XmlAppend.CreateElement("PositionTypeitem");
            //PositionTypeitem.SetAttribute("PositionTypeitem_key", node.PositionTypeitem.Key.ToString());
            PositionTypeitem.InnerText = node.PositionTypeitem.Key.ToString();
            aribute.AppendChild(PositionTypeitem);
            XmlElement LinkTypeitem = XmlAppend.CreateElement("LinkTypeitem");
            //LinkTypeitem.SetAttribute("LinkTypeitem_key", node.LinkTypeitem.Key.ToString());
            LinkTypeitem.InnerText = node.LinkTypeitem.Key.ToString();
            aribute.AppendChild(LinkTypeitem);
            XmlElement TipTypeitem = XmlAppend.CreateElement("TipTypeitem");
            TipTypeitem.InnerText = node.TipTypeitem;
            aribute.AppendChild(TipTypeitem);
            XmlElement LastExTexteitem = XmlAppend.CreateElement("LastExTexteitem");
            LastExTexteitem.InnerText = node.LastExTexteitem;
            aribute.AppendChild(LastExTexteitem);
            goalNode.AppendChild(aribute);
        }
        /// <summary>
        /// 写入代码块到系统文件中
        /// </summary>
        /// <param name="dataclass">要写入的数据结构</param>
        /// <returns>是否成功写入</returns>
        public bool WriteXSystemFile(XCodeDataOptionDataStructClass dataclass)
        {
            ///检测文件路劲是否存在
            if (!Directory.Exists(path))
            {
                /// 如果不存在就创建
                Directory.CreateDirectory(path);
            }

            if(!CheckFile(path + XSystemFileName))
            {
                XmlTextWriter writer = new XmlTextWriter(path + XSystemFileName, Encoding.UTF8);
                writer.Formatting = Formatting.Indented;
                writer.WriteStartDocument();
                writer.WriteStartElement(RootName);
                ///开始写入
                writer.WriteStartElement("CodeBox");
                writer.WriteAttributeString("Name", dataclass.CodeboxName);
                writer.WriteAttributeString("HitText", dataclass.CodeBoxHitText);
                writer.WriteAttributeString("CodeBoxType", dataclass.CodeBoxType);
                writer.WriteAttributeString("SystemCodeString", dataclass.CodeBoxSystemCodeString);
                writer.WriteAttributeString("ReturnValue", dataclass.ReturnValue);
                for (int i = 0;i < dataclass.nodeList.Count; i++)
                {
                    ///写入子节点
                    XMLWriteFile(dataclass.nodeList[i], writer);
                }
                writer.WriteEndElement();
                writer.WriteEndElement();
                ///关闭
                writer.Close();
                return true;
            }
            else
            {
                /// <summary>
                /// 读取XML的类
                /// </summary>
                XmlDocument XmlAppend = new XmlDocument();
                XmlAppend.Load(path + XSystemFileName);               
                ///查询出根节点
                XmlNode root = XmlAppend.SelectSingleNode(RootName);
                ///检查是否已经存在
                foreach (XmlNode node in root.SelectNodes("CodeBox"))
                {
                    XmlElement xet = (XmlElement)node;
                    if (xet.GetAttribute("Name") == dataclass.CodeboxName)
                    {
                        return false;
                    }
                }
                ///创建一个CodeBox节点
                XmlElement codebox = XmlAppend.CreateElement("CodeBox");
                ///设置属性
                codebox.SetAttribute("Name", dataclass.CodeboxName);
                codebox.SetAttribute("HitText", dataclass.CodeBoxHitText);
                codebox.SetAttribute("CodeBoxType", dataclass.CodeBoxType);
                codebox.SetAttribute("SystemCodeString", dataclass.CodeBoxSystemCodeString);
                codebox.SetAttribute("ReturnValue", dataclass.ReturnValue);
                ///开始创建子节点
                for (int i = 0; i < dataclass.nodeList.Count; i++)
                {
                    ///向子节点追加内容
                    XMLAppendFile(dataclass.nodeList[i], XmlAppend, codebox);
                }
                ///放在根节点下
                root.AppendChild(codebox);
                ///保存文件
                XmlAppend.Save(path + XSystemFileName);
                return true;
            }
        }
        /// <summary>
        /// 写入代码块到系统文件中(C语言时候)
        /// </summary>
        /// <param name="dataclass">要写入的数据结构</param>
        /// <returns>是否成功写入</returns>
        public bool WriteXCLanguageSystemFile(XCodeDataOptionDataStructClass dataclass)
        {
            ///检测文件路劲是否存在
            if (!Directory.Exists(path))
            {
                /// 如果不存在就创建
                Directory.CreateDirectory(path);
            }

            if (!CheckFile(path + XCSystemFileName))
            {
                XmlTextWriter writer = new XmlTextWriter(path + XCSystemFileName, Encoding.UTF8);
                writer.Formatting = Formatting.Indented;
                writer.WriteStartDocument();
                writer.WriteStartElement(RootName);
                ///开始写入
                writer.WriteStartElement("CodeBox");
                writer.WriteAttributeString("Name", dataclass.CodeboxName);
                writer.WriteAttributeString("HitText", dataclass.CodeBoxHitText);
                writer.WriteAttributeString("CodeBoxType", dataclass.CodeBoxType);
                writer.WriteAttributeString("SystemCodeString", dataclass.CodeBoxSystemCodeString);
                writer.WriteAttributeString("ReturnValue", dataclass.ReturnValue);
                for (int i = 0; i < dataclass.nodeList.Count; i++)
                {
                    ///写入子节点
                    XMLWriteFile(dataclass.nodeList[i], writer);
                }
                writer.WriteEndElement();
                writer.WriteEndElement();
                ///关闭
                writer.Close();
                return true;
            }
            else
            {
                /// <summary>
                /// 读取XML的类
                /// </summary>
                XmlDocument XmlAppend = new XmlDocument();
                XmlAppend.Load(path + XCSystemFileName);
                ///查询出根节点
                XmlNode root = XmlAppend.SelectSingleNode(RootName);
                ///检查是否已经存在
                foreach (XmlNode node in root.SelectNodes("CodeBox"))
                {
                    XmlElement xet = (XmlElement)node;
                    if (xet.GetAttribute("Name") == dataclass.CodeboxName)
                    {
                        return false;
                    }
                }
                ///创建一个CodeBox节点
                XmlElement codebox = XmlAppend.CreateElement("CodeBox");
                ///设置属性
                codebox.SetAttribute("Name", dataclass.CodeboxName);
                codebox.SetAttribute("HitText", dataclass.CodeBoxHitText);
                codebox.SetAttribute("CodeBoxType", dataclass.CodeBoxType);
                codebox.SetAttribute("SystemCodeString", dataclass.CodeBoxSystemCodeString);
                codebox.SetAttribute("ReturnValue", dataclass.ReturnValue);
                ///开始创建子节点
                for (int i = 0; i < dataclass.nodeList.Count; i++)
                {
                    ///向子节点追加内容
                    XMLAppendFile(dataclass.nodeList[i], XmlAppend, codebox);
                }
                ///放在根节点下
                root.AppendChild(codebox);
                ///保存文件
                XmlAppend.Save(path + XCSystemFileName);
                return true;
            }
        }
        /// <summary>
        /// 写入用户自定义文件
        /// </summary>
        /// <param name="nodeList"></param>
        /// <param name="CodboxName"></param>
        public bool WriteUserFile(List<XAributeDataItem> nodeList, string CodeboxName, string CodeBoxHitText)
        {
            ///检测文件路劲是否存在
            if (!Directory.Exists(path))
            {
                /// 如果不存在就创建
                Directory.CreateDirectory(path);
            }

            if (!CheckFile(path + XUserFileName))
            {
                XmlTextWriter writer = new XmlTextWriter(DictionaryName + XUserFileName, Encoding.UTF8);
                writer.Formatting = Formatting.Indented;
                writer.WriteStartDocument();
                writer.WriteStartElement(RootName);
                ///开始写入
                writer.WriteStartElement("CodeBox");
                writer.WriteAttributeString("Name", CodeboxName);
                writer.WriteAttributeString("HitText", CodeBoxHitText);
                for (int i = 0; i < nodeList.Count; i++)
                {
                    ///写入子节点
                    XMLWriteFile(nodeList[i], writer);
                }
                writer.WriteEndElement();
                writer.WriteEndElement();
                ///关闭
                writer.Close();
                return true;
            }
            else
            {
                /// <summary>
                /// 读取XML的类
                /// </summary>
                XmlDocument XmlAppend = new XmlDocument();
                XmlAppend.Load(path + XUserFileName);                
                ///查询出根节点
                XmlNode root = XmlAppend.SelectSingleNode(RootName);
                ///检查是否已经存在
                foreach (XmlNode node in root.SelectNodes("CodeBox"))
                {
                    XmlElement xet = (XmlElement)node;
                    if (xet.GetAttribute("Name") == CodeboxName)
                    {
                        return false;
                    }
                }
                ///创建一个CodeBox节点
                XmlElement codebox = XmlAppend.CreateElement("CodeBox");
                ///设置属性
                codebox.SetAttribute("Name", CodeboxName);
                codebox.SetAttribute("HitText", CodeBoxHitText);
                ///开始创建子节点
                for (int i = 0; i < nodeList.Count; i++)
                {
                    ///向子节点追加内容
                    XMLAppendFile(nodeList[i], XmlAppend, codebox);
                }
                ///放在根节点下
                root.AppendChild(codebox);
                ///保存文件
                XmlAppend.Save(path + XSystemFileName);
                return true;
            }
        }
        /// <summary>
        /// 组装读取的数据
        /// </summary>
        /// <param name="RootTreeItem">根节点数据</param>
        /// <param name="root">根节点XML</param>
        protected void LoadData(MyXTreeItem RootTreeItem, XmlNode root)
        {
            ///检查是否已经存在
            foreach (XmlNode node in root.SelectNodes("CodeBox"))
            {
                XmlElement codeNode = (XmlElement)node;
                MyXTreeItem RetreeItem = new MyXTreeItem();
                RetreeItem.IsSelected = false;
                RetreeItem.IsExpanded = false;
                RetreeItem.XName = codeNode.GetAttribute("Name");
                RetreeItem.MyHitText = codeNode.GetAttribute("HitText");
                RetreeItem.MyCodeBoxType = CodeBox.CodeBoxTypeMapping(codeNode.GetAttribute("CodeBoxType"));
                RetreeItem.SystemCodeString = codeNode.GetAttribute("SystemCodeString");
                RetreeItem.ReturnValue = codeNode.GetAttribute("ReturnValue");
                foreach (XmlNode xNode in codeNode.SelectNodes("XAribute"))
                {
                    ///MyXTreeItem的子项
                    XAributeItem xaItem = new XAributeItem();
                    ///将内容提出填装
                    XmlElement xaNode = (XmlElement)xNode;
                    xaItem.Parameter_name = xaNode.GetAttribute("Name");
                    xaItem.MyXAttributeType = XAribute.XAttributeTypeMapping(((XmlElement)xaNode.SelectSingleNode("PointTypeitem")).InnerText);
                    xaItem.MyXAttributeSpec = XAribute.XAttributeSpecMapping(((XmlElement)xaNode.SelectSingleNode("ListTypeitem")).InnerText);
                    xaItem.MyXPositonStyle = XAribute.XPositonStyleMapping(((XmlElement)xaNode.SelectSingleNode("PositionTypeitem")).InnerText);
                    xaItem.MyCanLinkType = XAribute.CanLinkTypeMapping(((XmlElement)xaNode.SelectSingleNode("LinkTypeitem")).InnerText);
                    xaItem.MyHittext = ((XmlElement)xaNode.SelectSingleNode("TipTypeitem")).InnerText;
                    xaItem.MyLastExText = ((XmlElement)xaNode.SelectSingleNode("LastExTexteitem")).InnerText;
                    ///添加到子项中
                    RetreeItem.MyXaributeChildren.Add(xaItem);
                }
                ///添加子节点
                RootTreeItem.ChildrenItem.Add(RetreeItem);
            }
        }
        /// <summary>
        /// 读取系统配置文件里面的内容
        /// </summary>
        /// <returns>返回所有系统配置代码</returns>
        public MyXTreeItem LoadXSystemFile()
        {
            ///检测文件路劲是否存在
            if (!Directory.Exists(path))
            {
                ///如果不存在
                return null;
            }
            try
            {
                /// <summary>
                /// 读取XML的类
                /// </summary>
                XmlDocument XmlAppend = new XmlDocument();
                XmlAppend.Load(path + XSystemFileName);
                ///查询出根节点
                XmlNode root = XmlAppend.SelectSingleNode(RootName);
                ///树状数据的系统代码块的头结点
                MyXTreeItem SystemTreeItem = new MyXTreeItem();
                SystemTreeItem.XName = "系统代码";
                ///组装数据
                LoadData(SystemTreeItem, root);
                ///保存关闭文件
                XmlAppend.Save(path + XSystemFileName);
                return SystemTreeItem;
            }catch(Exception ex)
            {
                LoggerHelp.WriteLogger(ex.ToString());
                return null;
            }
        }
        /// <summary>
        /// 读取系统配置文件里面的内容
        /// </summary>
        /// <returns>返回所有系统配置代码</returns>
        public MyXTreeItem LoadCLanguageXSystemFile()
        {
            ///检测文件路劲是否存在
            if (!Directory.Exists(path))
            {
                ///如果不存在
                return null;
            }
            try
            {
                /// <summary>
                /// 读取XML的类
                /// </summary>
                XmlDocument XmlAppend = new XmlDocument();
                XmlAppend.Load(path + XCSystemFileName);
                ///查询出根节点
                XmlNode root = XmlAppend.SelectSingleNode(RootName);
                ///树状数据的系统代码块的头结点
                MyXTreeItem SystemTreeItem = new MyXTreeItem();
                SystemTreeItem.XName = "C语言版系统代码";
                ///组装数据
                LoadData(SystemTreeItem, root);
                ///保存关闭文件
                XmlAppend.Save(path + XCSystemFileName);
                return SystemTreeItem;
            }
            catch (Exception ex)
            {
                LoggerHelp.WriteLogger(ex.ToString());
                return null;
            }
        }
        /// <summary>
        /// 加载用户配置
        /// </summary>
        /// <returns></returns>
        public MyXTreeItem LoadXUserFile()
        {
            ///检测文件路劲是否存在
            if (!Directory.Exists(path))
            {
                ///如果不存在
                return null;
            }
            /// <summary>
            /// 读取XML的类
            /// </summary>
            XmlDocument XmlAppend = new XmlDocument();
            XmlAppend.Load(path + XUserFileName);
            ///查询出根节点
            XmlNode root = XmlAppend.SelectSingleNode(RootName);
            ///树状数据的系统代码块的头结点
            MyXTreeItem UserTreeItem = new MyXTreeItem();
            UserTreeItem.XName = "用户代码";
            ///组装数据
            LoadData(UserTreeItem, root);
            ///保存关闭文件
            XmlAppend.Save(path + XSystemFileName);
            return UserTreeItem;
        }       
        #endregion
    }
}
