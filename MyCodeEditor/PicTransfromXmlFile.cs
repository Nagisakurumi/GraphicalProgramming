using MyCodeBox;
using MyPicTabPage;
using MyXAribute;
using System;
using System.IO;
using System.Text;
using System.Xml;

namespace MyXCodeEditor
{
    public class PicTransfromXmlFile
    {
        #region 自定义属性
        /// <summary>
        /// 根节点
        /// </summary>
        private string RootName = "PicTabPage";
        #endregion
        #region 自定义函数
        /// <summary>
        /// 将代码图转换为文件
        /// </summary>
        /// <param name="Page">代码图对象</param>
        /// <param name="path">文件存放路径</param>
        public void PicToXml(PicTabPage pic, string path)
        {
            string filePath = path + pic.Title;
            ///检测本地文件是否已经存在
            #region 如果文件不存在则创建
            if (!CheckFile(filePath))
            {
                XmlTextWriter writer = new XmlTextWriter(filePath, Encoding.UTF8);
                writer.Formatting = Formatting.Indented;
                writer.WriteStartDocument();
                writer.WriteStartElement(RootName);
                writer.WriteAttributeString("Title", pic.Title);
                writer.WriteAttributeString("ID", pic.Id.ToString());
                ///循环所有代码块
                writer.WriteStartElement("MainCodeBoxs");
                foreach (CodeBox box in pic.ListCodeBoxChild.Values)
                {                
                    DataWriteFunction.WriteCodeBoxAribute(writer, box);                 
                }
                writer.WriteEndElement();
                ///循环所有贝塞尔曲线
                writer.WriteStartElement("MainBezierLines");
                foreach (BezierLine line in pic.GetBezierLines.Values)
                {                    
                    DataWriteFunction.WriteBezierLine(writer, line);                    
                }
                writer.WriteEndElement();
                ///循环所有函数
                foreach (PicFunctionTabPage func in pic.ListFunction)
                {
                    DataWriteFunction.WriteFunction(writer, func);
                }
                ///循环所有属性
                foreach (XAribute bute in pic.ListXAributes)
                {
                    DataWriteFunction.WriteXAribute(writer, bute);
                }
                writer.WriteEndElement();
                writer.Close();
            }
            #endregion
            #region 如果存在则更新
            else
            {
                XmlDocument XmlLoad = new XmlDocument();
                XmlLoad.Load(path + pic.Title);
                XmlNode root = XmlLoad.SelectSingleNode(RootName);
                ///先清除所有然后在写入
                root.RemoveAll();
                XmlElement picroot = (XmlElement)root;             
                picroot.SetAttribute("Title", pic.Title);
                picroot.SetAttribute("ID", pic.Id.ToString());
                ///循环所有代码块
                ///创建存放代码块的主节点
                XmlElement MainCodeBoxs = XmlLoad.CreateElement("MainCodeBoxs");
                foreach (CodeBox box in pic.ListCodeBoxChild.Values)
                {
                    DataWriteFunction.UpdateCodeBoxAribute(XmlLoad, MainCodeBoxs, box);
                }
                picroot.AppendChild(MainCodeBoxs);
                ///循环所有贝塞尔曲线
                ///创建存放贝塞尔曲线的主节点
                XmlElement MainBeizierLines = XmlLoad.CreateElement("MainBezierLines");
                foreach (BezierLine line in pic.GetBezierLines.Values)
                {
                    DataWriteFunction.UpdateBezierLine(XmlLoad, MainBeizierLines, line);
                }
                ///添加节点
                picroot.AppendChild(MainBeizierLines);
                ///循环所有函数
                foreach (PicFunctionTabPage func in pic.ListFunction)
                {
                    DataWriteFunction.UpdateFunction(XmlLoad, picroot, func);
                }
                ///循环所有属性
                foreach (XAribute bute in pic.ListXAributes)
                {
                    DataWriteFunction.UpdateXAribute(XmlLoad, picroot, bute);
                }
                XmlLoad.Save(path + pic.Title);
            }
            #endregion
        }
        /// <summary>
        /// 将文件转换为PicTabPage对象
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        public PicTabPage XmlToPic(string path)
        {
            PicTabPage pic = new PicTabPage();
            XmlDocument XmlLoad = new XmlDocument();
            XmlLoad.Load(path);
            ///查询出根节点
            XmlNode root = XmlLoad.SelectSingleNode(RootName);
            XmlElement picroot = (XmlElement)root;
            pic.Id = int.Parse(picroot.GetAttribute("ID"));
            pic.Title = picroot.GetAttribute("Title");
            ///查找CodeBox节点
            XmlNode MainCodeBoxs = root.SelectSingleNode("MainCodeBoxs");
            foreach (XmlNode node in MainCodeBoxs.SelectNodes("CodeBox"))
            {
                DataWriteFunction.ReadCodeBoxObject(node, pic);
            }
            ///查找BezierLine节点
            XmlNode MainBezierLines = root.SelectSingleNode("MainBezierLines");
            foreach (XmlNode node in MainBezierLines.SelectNodes("BezierLine"))
            {
                DataWriteFunction.ReadBezierLine(node, pic);
            }
            ///查找函数节点
            foreach (XmlNode node in root.SelectNodes("Function"))
            {
                DataWriteFunction.ReadFunction(node, pic);
            }
            ///查找属性节点
            foreach (XmlNode node in root.SelectNodes("XAribute"))
            {
                DataWriteFunction.ReadXAributeObject(node, pic);
            }
            XmlLoad.Save(path);
            return pic;
        }
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
        #endregion
    }
}
