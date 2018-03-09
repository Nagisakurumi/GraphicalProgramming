using MyCodeBox;
using MyXAribute;
using MyXObject;
using System;
using System.Collections.Generic;

namespace MyXCodeEditor
{
    public class XPicCodeToCLanguagesCode : XPicCodeToCode
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ProjectName">项目名称</param>
        public XPicCodeToCLanguagesCode(string ProjectName)
        {
            this.ProjectName = ProjectName;
        }
        #endregion
        #region 自定义函数
        /// <summary>
        /// 代码解析
        /// </summary>
        /// <param name="pic">PicTabPage 对象</param>
        public override string XPicCodeEditor(MyPicTabPage.PicTabPage pic)
        {
            ///判断该要解析的类是否是主类
            bool isMainClass = false;

            string CodeString =
                "#include <stdio.h>" + LineBreaks +
                "#include <conio.h>" + LineBreaks +
            "#include <limits.h>" + LineBreaks +
            //"#include <math.h>" + LineBreaks;
                "#include <stdbool.h>" + LineBreaks;

            ///解析具体属性实现
            foreach (XAribute bute in pic.ListXAributes)
            {
                CodeString += AnalyticalXAribute(bute, isMainClass);
            }
            ///解析具体函数实现
            foreach (MyPicTabPage.PicFunctionTabPage functionpage in pic.ListFunction)
            {
                CodeString += AnalyticalFunction(functionpage);
            }
            ///解析确定是函数入口
            foreach (CodeBox box in pic.ListCodeBoxChild.Values)
            {
                if (box.CodeBoxType == CodeBox.XAType.XMain)
                {
                    CodeString += AnalyticalMain(box);
                    ///确认正在解析主类
                    isMainClass = true;
                }
                else if (box.CodeBoxType == CodeBox.XAType.XEvent)
                {

                }
            }                    
            return CodeString;
        }
        /// <summary>
        /// 解析主函数
        /// </summary>
        /// <param name="box">主函数的Box</param>
        protected string AnalyticalMain(CodeBox box)
        {
            string startCode =
                "void main()" + LineBreaks
                + "{" + LineBreaks;
            string codeString = "";
            codeString += startCode;
            try
            {
                ///提取出第一个正常节点
                if (((XAribute)box.RightAribute.Children[0]).SelectType == XAribute.XAttributeType.XExc)
                {
                    CodeBox noramlBox = (CodeBox)((XAribute)box.RightAribute.Children[0]).GetOtherXAribute().ParentControl;
                    codeString += AnalyticalNormal(noramlBox);
                }
            }
            catch (Exception ex)
            {
                LoggerHelp.WriteLogger(ex.ToString());
            }
            codeString += "printf(\"\\n请按任意键退出！\");" + LineBreaks + "getch();" + LineBreaks;
            codeString += "}" + LineBreaks;
            return codeString;
        }
        /// <summary>
        /// 解析普通代码块
        /// </summary>
        protected string AnalyticalNormal(CodeBox box)
        {
            string codeString = "";
            string boxcodestring = "";
            switch (box.CodeBoxType)
            {
                #region 分支结构
                case CodeBox.XAType.XIf:
                    string ifstring = box.GetCLanguagesCodeBoxValue();
                    codeString += "if ( " + ifstring + "== true )" + LineBreaks;
                    codeString += "{" + LineBreaks;
                    ///条件为真的出口
                    XAribute trueExc = (XAribute)box.RightAribute.Children[0];
                    ///如果有后续内容
                    if (trueExc.GetMyBeziers.Count > 0)
                    {
                        codeString += AnalyticalCenterTool(trueExc);
                    }
                    codeString += LineBreaks;
                    codeString += "}" + LineBreaks;
                    codeString += "else" + LineBreaks;
                    codeString += "{" + LineBreaks;
                    ///条件为假的出口
                    XAribute falseExc = (XAribute)box.RightAribute.Children[1];
                    ///如果有后续内容
                    if (falseExc.GetMyBeziers.Count > 0)
                    {
                        codeString += AnalyticalCenterTool(falseExc);
                    }
                    codeString += LineBreaks;
                    codeString += "}" + LineBreaks;
                    break;
                #endregion
                #region 循环结构
                case CodeBox.XAType.XFor:
                    boxcodestring = box.GetCLanguagesCodeBoxValue();
                    codeString += "for ( " + boxcodestring + " )" + LineBreaks;
                    codeString += "{" + LineBreaks;
                    List<XAribute> butes = box.GetRightExc();
                    ///如果有后续内容
                    if (butes[0].GetMyBeziers.Count > 0)
                    {
                        codeString += AnalyticalCenterTool(butes[0]);
                    }
                    codeString += "}" + LineBreaks;
                    if (butes[1].GetMyBeziers.Count > 0)
                    {
                        codeString += AnalyticalCenterTool(butes[1]);
                    }
                    break; 
                #endregion
                #region 顺序结构
                default:
                    codeString += box.GetCLanguagesCodeBoxValue() + LineBreaks;
                    foreach (XAribute bute in box.RightAribute.Children)
                    {
                        if (bute.SelectType == XAribute.XAttributeType.XExc)
                        {
                            if (bute.GetMyBeziers.Count > 0)
                            {
                                codeString += AnalyticalCenterTool(bute);
                            }
                        }
                    }
                    break;
                    #endregion
            }
            return codeString;
        }
        /// <summary>
        /// 解析前的一些判断
        /// </summary>
        /// <param name="bute">上一个代码块的出口节点</param>
        /// <returns></returns>
        protected string AnalyticalCenterTool(XAribute bute)
        {
            XAribute target = bute.GetOtherXAribute();
            CodeBox targetBox = (CodeBox)target.ParentControl;
            ///代码块入口
            XAribute targetBoxEnter = targetBox.GetLeftEnter();
            if (target.SelectType == XAribute.XAttributeType.XBreak)
            {
                if (targetBoxEnter != null && targetBoxEnter.GetMyBeziers.Count > 0)
                {
                    return "break;" + LineBreaks;
                }
                else
                {
                    return "";
                }
            }
            else if (target != null)
            {
                return AnalyticalNormal(targetBox);
            }
            return "";
        }
        /// <summary>
        /// 解析函数
        /// </summary>
        /// <param name="functionpage"></param>
        /// <returns></returns>
        protected string AnalyticalFunction(MyPicTabPage.PicFunctionTabPage functionpage)
        {
            string Ftype = "";
            string xaributstring = "";
            CodeBox enterBox = null;
            ///提取参数和返回值
            foreach (CodeBox box in functionpage.ListCodeBoxChild.Values)
            {
                if (box.CodeBoxType == CodeBox.XAType.XFunctionExc)
                {
                    Ftype = ((XAribute)box.LeftAribute.Children[1]).ExName + " ";
                }
                if (box.CodeBoxType == CodeBox.XAType.XFunctionEnter)
                {
                    enterBox = box;
                    foreach (XAribute bute in box.RightAribute.Children)
                    {
                        if (bute.SelectType != XAribute.XAttributeType.XExc && bute.SelectType != XAribute.XAttributeType.XEnter && bute.SelectType != XAribute.XAttributeType.XEnum)
                        {
                            xaributstring += bute.ExName + " " + bute.Title + ", ";
                        }
                    }
                }
            }
            if (xaributstring != "")
            {
                xaributstring = xaributstring.Substring(0, xaributstring.Length - 2);
            }
            string ValueString = functionpage.MyOpenType.ToString() + " " + Ftype + functionpage.Title + "(" + xaributstring + ")"
                + "{";
            if (enterBox != null)
                ValueString += AnalyticalNormal(enterBox);
            ValueString += "}";
            return ValueString;
        }
        /// <summary>
        /// 解析类的属性
        /// </summary>
        /// <param name="bute">属性</param>
        /// <returns>返回解析完的字符串</returns>
        protected string AnalyticalXAribute(MyXAribute.XAribute bute, bool isMain = false)
        {
            string codeString = "";
            codeString += bute.MappingExNameToCLanguages() + " " + bute.Title + ";" + LineBreaks;
            return codeString;
        }
        #endregion
    }
}
