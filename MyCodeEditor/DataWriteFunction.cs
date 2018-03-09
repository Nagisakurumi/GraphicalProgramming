using MyCodeBox;
using MyPicTabPage;
using MyXAribute;
using MyXObject;
using System.Windows;
using System.Xml;

namespace MyXCodeEditor
{
    public static class DataWriteFunction
    {
        #region 自定义方法
        /// <summary>
        /// 写入属性
        /// </summary>
        /// <param name="writer">XML写入对象</param>
        /// <param name="node">XAribute对象</param>
        public static void WriteXAribute(XmlTextWriter writer, XAribute node)
        {
            writer.WriteStartElement("XAribute");
            writer.WriteAttributeString("ID", node.Id.ToString());
            writer.WriteAttributeString("Title", node.Title);
            writer.WriteAttributeString("PointTypeitem", node.SelectType.ToString());
            writer.WriteAttributeString("ListTypeitem", node.SelectSpc.ToString());
            writer.WriteAttributeString("PositionTypeitem", node.SelectPositionStyle.ToString());
            writer.WriteAttributeString("LinkTypeitem", node.CanLinkNum.ToString());
            writer.WriteAttributeString("TipTypeitem", node.Hint);
            writer.WriteAttributeString("LastExTexteitem", node.ExName);
            writer.WriteAttributeString("OpenType", node.MyOpenType.ToString());
            if(ISXAributeToSaveValue(node))
            {
                writer.WriteElementString("Value", node.GetValueTextBox());
            }
            /// 关闭XAribute元素节点
            writer.WriteEndElement();
        }
        /// <summary>
        /// 将CodeBox信息写入XML文件
        /// </summary>
        /// <param name="writer">XML对象</param>
        /// <param name="box">CodeBox对象</param>
        public static void WriteCodeBoxAribute(XmlTextWriter writer,CodeBox box)
        {
            writer.WriteStartElement("CodeBox");
            writer.WriteAttributeString("Title", box.Title);
            writer.WriteAttributeString("HitText", box.Hint);
            writer.WriteAttributeString("PositionX", box.GetPosition().X.ToString());
            writer.WriteAttributeString("PositionY", box.GetPosition().Y.ToString());
            writer.WriteAttributeString("ID", box.Id.ToString());
            writer.WriteAttributeString("CodeBoxType", box.CodeBoxType.ToString());
            writer.WriteAttributeString("SystemCodeString", box.SystemCodeString);
            writer.WriteAttributeString("ReturnValueName", box.ReturnValueName);
            ///写入左边属性
            writer.WriteStartElement("LeftXAribute");
            foreach (XAribute node in box.LeftAribute.Children)
            {
                WriteXAribute(writer, node);
            }
            ///结束写入左边边属性
            writer.WriteEndElement();
            writer.WriteStartElement("RightXAribute");
            foreach (XAribute node in box.RightAribute.Children)
            {
                WriteXAribute(writer, node);
            }
            ///结束写入右边属性
            writer.WriteEndElement();
            writer.WriteEndElement();
        }
        /// <summary>
        /// 贝塞尔曲线资源转换为文件
        /// </summary>
        /// <param name="writer">xml对象</param>
        /// <param name="line">贝塞尔曲线对象</param>
        public static void WriteBezierLine(XmlTextWriter writer, BezierLine line)
        {
            writer.WriteStartElement("BezierLine");
            writer.WriteAttributeString("ID", line.Id.ToString());
            writer.WriteAttributeString("StrokeThickness", line.StrokeThickness.ToString());
            ///起始点位置
            writer.WriteStartElement("StartPoint");
            writer.WriteElementString("PositionType", line.StartPoint.positionType.ToString());
            writer.WriteElementString("LinkAributeFatherID", line.StartPoint.LinkAribute.ParentControl.Id.ToString());
            writer.WriteElementString("LinkAributeID", line.StartPoint.LinkAribute.Id.ToString());
            ///结束起始点的属性
            writer.WriteEndElement();
            ///终止点的属性
            writer.WriteStartElement("EndPoint");
            writer.WriteElementString("PositionType", line.EndPoint.positionType.ToString());
            writer.WriteElementString("LinkAributeFatherID", line.EndPoint.LinkAribute.ParentControl.Id.ToString());
            writer.WriteElementString("LinkAributeID", line.EndPoint.LinkAribute.Id.ToString());
            writer.WriteEndElement();
            ///结束贝塞尔曲线的存储
            writer.WriteEndElement();
        }
        /// <summary>
        /// 将函数写入文件
        /// </summary>
        /// <param name="writer">写入文件的对象</param>
        /// <param name="func">要写入的函数</param>
        public static void WriteFunction(XmlTextWriter writer, PicFunctionTabPage func)
        {
            ///写入标识
            writer.WriteStartElement("Function");
            writer.WriteAttributeString("Title", func.Title);
            writer.WriteAttributeString("ID", func.Id.ToString());
            writer.WriteAttributeString("OpenType", func.MyOpenType.ToString());
            writer.WriteAttributeString("OverrideType", func.MyOverride.ToString());
            ///循环所有代码块
            foreach (CodeBox box in func.ListCodeBoxChild.Values)
            {
                DataWriteFunction.WriteCodeBoxAribute(writer, box);
            }
            ///循环所有贝塞尔曲线
            foreach (BezierLine line in func.GetBezierLines.Values)
            {
                DataWriteFunction.WriteBezierLine(writer, line);
            }
            ///结束函数的存储的标识
            writer.WriteEndElement();
        }
        /// <summary>
        /// 更新Codebox的内容
        /// </summary>
        /// <param name="update">更新内容的节点</param>
        /// <param name="codeboxfather">父节点</param>
        /// <param name="box">代码块</param>
        public static void UpdateCodeBoxAribute(XmlDocument update, XmlElement codeboxfather, CodeBox box)
        {
            XmlElement codedata = update.CreateElement("CodeBox");
            codedata.SetAttribute("Title", box.Title);
            codedata.SetAttribute("HitText", box.Hint);
            codedata.SetAttribute("PositionX", box.GetPosition().X.ToString());
            codedata.SetAttribute("PositionY", box.GetPosition().Y.ToString());
            codedata.SetAttribute("ID", box.Id.ToString());
            codedata.SetAttribute("CodeBoxType", box.CodeBoxType.ToString());
            codedata.SetAttribute("SystemCodeString", box.SystemCodeString);
            codedata.SetAttribute("ReturnValueName", box.ReturnValueName);
            ///更新左边属性
            XmlElement LeftXAributedata = update.CreateElement("LeftXAribute");
            foreach (XAribute node in box.LeftAribute.Children)
            {
                UpdateXAribute(update,LeftXAributedata, node);               
            }
            codedata.AppendChild(LeftXAributedata);
            ///更新右边边属性
            XmlElement RightXAributedata = update.CreateElement("RightXAribute");
            foreach (XAribute node in box.RightAribute.Children)
            {
                UpdateXAribute(update, RightXAributedata, node);             
            }
            codedata.AppendChild(RightXAributedata);
            ///添加节点
            codeboxfather.AppendChild(codedata);
        }
        /// <summary>
        /// 更新属性节点
        /// </summary>
        /// <param name="update">可以创建节点的域</param>
        /// <param name="xaributefather">属性的父节点</param>
        /// <param name="node">属性内容</param>
        public static void UpdateXAribute(XmlDocument update, XmlElement xaributefather, XAribute node)
        {
            XmlElement xaribute = update.CreateElement("XAribute");
            xaribute.SetAttribute("ID", node.Id.ToString());
            xaribute.SetAttribute("Title", node.Title);
            xaribute.SetAttribute("PointTypeitem", node.SelectType.ToString());
            xaribute.SetAttribute("ListTypeitem", node.SelectSpc.ToString());
            xaribute.SetAttribute("PositionTypeitem", node.SelectPositionStyle.ToString());
            xaribute.SetAttribute("LinkTypeitem", node.CanLinkNum.ToString());
            xaribute.SetAttribute("TipTypeitem", node.Hint);
            xaribute.SetAttribute("LastExTexteitem", node.ExName);
            xaribute.SetAttribute("OpenType", node.MyOpenType.ToString());
            if (ISXAributeToSaveValue(node))
            {
                xaribute.SetAttribute("Value", node.GetValueTextBox());
            }
            xaributefather.AppendChild(xaribute);
        }
        /// <summary>
        /// 更新贝塞尔曲线内容
        /// </summary>
        /// <param name="update">更新节点</param>
        /// /// <param name="bezierfather">贝塞尔曲线父节点</param>
        /// <param name="line">贝塞尔曲线内容</param>
        public static void UpdateBezierLine(XmlDocument update,XmlElement bezierfather, BezierLine line)
        {
            ///如果有没有处理且为空的曲线则清空
            if (line.StartPoint.LinkAribute == null || line.EndPoint.LinkAribute == null)
                return;
            XmlElement bezier =  update.CreateElement("BezierLine");
            bezier.SetAttribute("ID", line.Id.ToString());
            bezier.SetAttribute("StrokeThickness", line.StrokeThickness.ToString());
            ///起始点位置
            XmlElement startpoint = update.CreateElement("StartPoint");

            XmlElement startpointPositionType = update.CreateElement("PositionType");
            startpointPositionType.InnerText = line.StartPoint.positionType.ToString();
            XmlElement startpointLinkAributeFatherID = update.CreateElement("LinkAributeFatherID");
            startpointLinkAributeFatherID.InnerText = line.StartPoint.LinkAribute.ParentControl.Id.ToString();
            XmlElement startpointLinkAributeID = update.CreateElement("LinkAributeID");
            startpointLinkAributeID.InnerText = line.StartPoint.LinkAribute.Id.ToString();
            ///加入子节点
            bezier.AppendChild(startpoint);
            startpoint.AppendChild(startpointPositionType);
            startpoint.AppendChild(startpointLinkAributeFatherID);
            startpoint.AppendChild(startpointLinkAributeID);          
            ///终止点的属性
            XmlElement endpoint = update.CreateElement("EndPoint");
            XmlElement endpointPositionType = update.CreateElement("PositionType");
            endpointPositionType.InnerText = line.EndPoint.positionType.ToString();
            XmlElement endpointLinkAributeFatherID = update.CreateElement("LinkAributeFatherID");
            endpointLinkAributeFatherID.InnerText = line.EndPoint.LinkAribute.ParentControl.Id.ToString();
            XmlElement endpointLinkAributeID = update.CreateElement("LinkAributeID");
            endpointLinkAributeID.InnerText = line.EndPoint.LinkAribute.Id.ToString();
            bezier.AppendChild(endpoint);
            endpoint.AppendChild(endpointPositionType);
            endpoint.AppendChild(endpointLinkAributeFatherID);
            endpoint.AppendChild(endpointLinkAributeID);
            ///加入子节点
            bezierfather.AppendChild(bezier);
        }
        /// <summary>
        /// 更新函数
        /// </summary>
        /// <param name="update">更新节点</param>
        /// <param name="Functionfather">要更新的函数父节点</param>
        /// <param name="Function">函数的内容</param>
        public static void UpdateFunction(XmlDocument update, XmlElement Functionfather, PicFunctionTabPage func)
        {
            ///写入标识
            XmlElement function = update.CreateElement("Function");
            function.SetAttribute("Title", func.Title);
            function.SetAttribute("ID", func.Id.ToString());
            function.SetAttribute("OpenType", func.MyOpenType.ToString());
            function.SetAttribute("OverrideType", func.MyOverride.ToString());
            ///循环所有代码块
            foreach (CodeBox box in func.ListCodeBoxChild.Values)
            {
                DataWriteFunction.UpdateCodeBoxAribute(update,function, box);
            }
            ///循环所有贝塞尔曲线
            foreach (BezierLine line in func.GetBezierLines.Values)
            {
                DataWriteFunction.UpdateBezierLine(update,function, line);
            }
            ///加入子节点
            Functionfather.AppendChild(function);
        }
        /// <summary>
        /// 将XML对象转换回CodeBox对象
        /// </summary>
        /// <param name="boxElement"></param>
        /// <returns></returns>
        public static void ReadCodeBoxObject(XmlNode boxnode, PicTabPage pic)
        {
            XmlElement boxElement = (XmlElement)boxnode;
            //CodeBox ReBox = new CodeBox();
            int Id =int.Parse(boxElement.GetAttribute("ID"));
            string Title = boxElement.GetAttribute("Title");
            string Hint = boxElement.GetAttribute("HitText");
            CodeBox.XAType CodeboxType = CodeBox.CodeBoxTypeMapping(boxElement.GetAttribute("CodeBoxType"));
            Point position = new Point();
            position.X = float.Parse(boxElement.GetAttribute("PositionX"));
            position.Y = float.Parse(boxElement.GetAttribute("PositionY"));
            string SystemCodeString = boxElement.GetAttribute("SystemCodeString");
            string ReturnValueName = boxElement.GetAttribute("ReturnValueName");
            ///加载一个CodeBox
            CodeBox ReBox = pic.LoadXCodeBox(Title, position, Id, CodeboxType);
            ReBox.SystemCodeString = SystemCodeString;
            ReBox.ReturnValueName = ReturnValueName;
            ///循环左边属性
            XmlNode LeftXAribute = boxnode.SelectSingleNode("LeftXAribute");
            foreach (XmlNode node in LeftXAribute.SelectNodes("XAribute"))
            {
                ReadXAributeObject(node, ReBox);
            }
            ///循环右边属性
            XmlNode RightXAribute = boxnode.SelectSingleNode("RightXAribute");
            foreach (XmlNode node in RightXAribute.SelectNodes("XAribute"))
            {
                ReadXAributeObject(node, ReBox);
            }
        }
        /// <summary>
        /// 读取XML中的属性内容
        /// </summary>
        /// <param name="aribute">包含属性的XML节点</param>
        /// <param name="box">属性所在的CodeBox对象</param>
        /// <returns></returns>
        public static void ReadXAributeObject(XmlNode node,CodeBox box)
        {
            XmlElement aributeElement = (XmlElement)node;
            int Id = int.Parse(aributeElement.GetAttribute("ID"));
            string Title = aributeElement.GetAttribute("Title");
            XAribute.XAttributeType SelectType = XAribute.XAttributeTypeMapping(aributeElement.GetAttribute("PointTypeitem"));
            XAribute.XAttributeSpec SelectSpc = XAribute.XAttributeSpecMapping(aributeElement.GetAttribute("ListTypeitem"));
            XAribute.XPositonStyle SelectPositionStyle = XAribute.XPositonStyleMapping(aributeElement.GetAttribute("PositionTypeitem"));
            XAribute.CanLinkType CanLinkNum = XAribute.CanLinkTypeMapping(aributeElement.GetAttribute("LinkTypeitem"));
            string Hint = aributeElement.GetAttribute("TipTypeitem");
            string ExName = aributeElement.GetAttribute("LastExTexteitem");
            XObject.OpenType opentype = XObject.OpenTypeMapping(aributeElement.GetAttribute("OpenType"));
            ///通过内置函数加载属性
            XAribute bute = box.LoadAttribute(SelectType, SelectSpc, SelectPositionStyle, Title, CanLinkNum, Hint, ExName, Id,opentype);
            if (bute != null && ISXAributeToSaveValue(bute))
            {
                bute.SetValueTextBox(aributeElement.GetAttribute("Value"));
            }
        }
        /// <summary>
        /// 读取XML中的属性内容
        /// </summary>
        /// <param name="aribute">包含属性的XML节点</param>
        /// <param name="box">属性所在的CodeBox对象</param>
        /// <returns></returns>
        public static void ReadXAributeObject(XmlNode node, PicTabPage pic)
        {
            XmlElement aributeElement = (XmlElement)node;
            int Id = int.Parse(aributeElement.GetAttribute("ID"));
            string Title = aributeElement.GetAttribute("Title");
            XAribute.XAttributeType SelectType = XAribute.XAttributeTypeMapping(aributeElement.GetAttribute("PointTypeitem"));
            XAribute.XAttributeSpec SelectSpc = XAribute.XAttributeSpecMapping(aributeElement.GetAttribute("ListTypeitem"));
            XAribute.XPositonStyle SelectPositionStyle = XAribute.XPositonStyleMapping(aributeElement.GetAttribute("PositionTypeitem"));
            XAribute.CanLinkType CanLinkNum = XAribute.CanLinkTypeMapping(aributeElement.GetAttribute("LinkTypeitem"));
            string Hint = aributeElement.GetAttribute("TipTypeitem");
            string ExName = aributeElement.GetAttribute("LastExTexteitem");
            XObject.OpenType opentype = XObject.OpenTypeMapping(aributeElement.GetAttribute("OpenType"));           
            ///通过内置函数加载属性
            XAribute bute = pic.LoadXAribute(SelectType, SelectSpc, SelectPositionStyle, Title, CanLinkNum, Hint, ExName, Id, opentype);          
        }
        /// <summary>
        /// 读取XML中的贝塞尔曲线内容
        /// </summary>
        /// <param name="beziernode"></param>
        /// <param name="pic"></param>
        public static void ReadBezierLine(XmlNode beziernode, PicTabPage pic)
        {           
            XmlElement bezierElement = (XmlElement)beziernode;
            int ID = int.Parse(bezierElement.GetAttribute("ID"));
            double StrokeThickness = double.Parse(bezierElement.GetAttribute("StrokeThickness"));
            ///读取起始点
            XmlElement startPoint = (XmlElement)beziernode.SelectSingleNode("StartPoint");
            BezierPoint readStartBezierPoint = new BezierPoint();
            int startID = -1;
            int startfatherID = -1;
            foreach (XmlNode node in startPoint.ChildNodes)
            {
                XmlElement nodeElenemt = (XmlElement)node;
                switch(nodeElenemt.Name)
                {
                    case "PositionType":
                        readStartBezierPoint.positionType = XAribute.XPositonStyleMapping(nodeElenemt.InnerText);
                        break;
                    case "LinkAributeID":
                        startID = int.Parse(nodeElenemt.InnerText);
                        break;
                    case "LinkAributeFatherID":
                        startfatherID = int.Parse(nodeElenemt.InnerText);
                        break;
                    default:
                        break;
                }
            }
            ///读取结束点
            XmlElement endPoint = (XmlElement)beziernode.SelectSingleNode("EndPoint");
            BezierPoint readEndBezierPoint = new BezierPoint();
            int endID = -1;
            int endfatherID = -1;
            foreach (XmlNode node in endPoint.ChildNodes)
            {
                XmlElement nodeElenemt = (XmlElement)node;
                switch (nodeElenemt.Name)
                {
                    case "PositionType":
                        readEndBezierPoint.positionType = XAribute.XPositonStyleMapping(nodeElenemt.InnerText);
                        break;
                    case "LinkAributeID":
                        endID = int.Parse(nodeElenemt.InnerText);
                        break;
                    case "LinkAributeFatherID":
                        endfatherID = int.Parse(nodeElenemt.InnerText);
                        break;
                    default:
                        break;
                }
            }
            BezierLine readBezierLine = new BezierLine(ID);
            readBezierLine.StartPoint = readStartBezierPoint;
            readBezierLine.EndPoint = readEndBezierPoint;
            pic.ReadCreateBezierLine(readBezierLine, startID, endID,startfatherID,endfatherID);
        }
        /// <summary>
        /// 读取函数内容
        /// </summary>
        /// <param name="picnode">代码图节点</param>
        /// <param name="pic">代码图(类)</param>
        public static void ReadFunction(XmlNode functionnode, PicTabPage pic)
        {         
            XmlElement functionElement = (XmlElement)functionnode;
            string Title = functionElement.GetAttribute("Title");
            int ID = int.Parse(functionElement.GetAttribute("ID"));
            XObject.OpenType myOpenType = XObject.OpenTypeMapping(functionElement.GetAttribute("OpenType"));
            OverrideType myOverrideType = XObject.OverrideTypeMapping(functionElement.GetAttribute("OverrideType"));

            PicFunctionTabPage functionpage = pic.LoadFunctionPage(Title, ID,myOpenType,myOverrideType);
            ///查找CodeBox节点
            foreach (XmlNode node in functionnode.SelectNodes("CodeBox"))
            {
                DataWriteFunction.ReadCodeBoxObject(node, functionpage);
            }
            ///查找BezierLine节点
            foreach (XmlNode node in functionnode.SelectNodes("BezierLine"))
            {
                DataWriteFunction.ReadBezierLine(node, functionpage);
            }
        }
        #endregion

        #region 辅助函数
        /// <summary>
        /// 判断一个属性的值是否需要保存
        /// </summary>
        /// <param name="bute">要确认的属性</param>
        /// <returns>是否需要</returns>
        private static bool ISXAributeToSaveValue(XAribute node)
        {
            if (node.SelectType == XAribute.XAttributeType.XString || node.SelectType == XAribute.XAttributeType.XInt || node.SelectType == XAribute.XAttributeType.XFloat ||
                node.SelectType == XAribute.XAttributeType.XDouble || node.SelectType == XAribute.XAttributeType.XChar)
            {
                if(node.GetMyBeziers.Count == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        #endregion
    }
}
