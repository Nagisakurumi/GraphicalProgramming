using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using MyXObject;

namespace MyCodeEditor
{
    public static class CodeDestoryClass
    {

        #region 自定义属性
        /// <summary>
        /// C语言编译器的路径
        /// </summary>
        public static string CLanguageEditorPath = "E:\\工具\\编程工具\\VS2015\\VS2015\\VC\\bin\\cl.exe";
        #endregion
        /// <summary>
        /// 编译代码
        /// </summary>
        /// <param name="CodeString">要编译的代码字符串</param>
        /// <param name="OutCode">编译代码时候生成的消息(报错消息和成功消息)</param>
        ///         /// <param name="OutPath">输出路径</param>
        /// <param name="DllPath">dll文件路径</param>
        /// <returns>是否编译成功</returns>
        public static bool AnalyTicalCode(string CodeString, out string OutCode, string OutPath, string[] DllPath)
        {
            string Output = "Out.exe";
            string CodeError = "";
            CodeDomProvider codeProvider = CodeDomProvider.CreateProvider("CSharp");
            CompilerParameters parameters = new CompilerParameters();
            //是否生成一个exe文件
            parameters.GenerateExecutable = true;
            ///输出exe的名称
            parameters.OutputAssembly = Output;
            ///加载dll文件
            parameters.ReferencedAssemblies.Add("System.Data.dll");
            //parameters.CompilerOptions = "csc.exe /R:System.Data.dll MyCode.cs";
            CompilerResults results = codeProvider.CompileAssemblyFromSource(parameters, CodeString);

            if (results.Errors.Count > 0)
            {
                //ErroBox.ForeColor = Color.Red;
                foreach (CompilerError CompErr in results.Errors)
                {
                    CodeError +=
                                "Line number " + CompErr.Line +
                                ", Error Number: " + CompErr.ErrorNumber +
                                ", '" + CompErr.ErrorText + ";" +
                                Environment.NewLine + Environment.NewLine;
                }
                OutCode = CodeError;
                return false;
            }
            else
            {
                //Successful Compile
                //ErroBox.ForeColor = Color.Blue;
                OutCode = "Success!";
                //If we clicked run then launch our EXE
                Process.Start(Output);
                return true;
            }
        }
        /// <summary>
        /// 编译代码
        /// </summary>
        /// <param name="CodeString">要编译的代码字符串</param>
        /// <param name="OutCode">编译代码时候生成的消息(报错消息和成功消息)</param>
        /// <param name="OutPath">输出路径</param>
        /// <param name="DllPath">dll文件路径</param>
        /// <returns>是否编译成功</returns>
        public static bool AnalyTicalCode(string []CodeString, out string OutCode,string OutPath,string []DllPath,string projectname)
        {
            string Output = OutPath + projectname + ".exe";
            string CodeError = "";
            CodeDomProvider codeProvider = CodeDomProvider.CreateProvider("CSharp");
            CompilerParameters parameters = new CompilerParameters();
            //是否生成一个exe文件
            parameters.GenerateExecutable = true;
            ///输出exe的名称
            parameters.OutputAssembly = Output;
            ///加载dll文件
            //parameters.ReferencedAssemblies.Add("System.Data.dll");
            foreach(string path in DllPath)
            {
                parameters.ReferencedAssemblies.Add(path);
            }
            //parameters.CompilerOptions = "csc.exe /R:System.Data.dll MyCode.cs";
            CompilerResults results = codeProvider.CompileAssemblyFromSource(parameters, CodeString);

            if (results.Errors.Count > 0)
            {
                //ErroBox.ForeColor = Color.Red;
                foreach (CompilerError CompErr in results.Errors)
                {
                    CodeError +=
                                "Line number " + CompErr.Line +
                                ", Error Number: " + CompErr.ErrorNumber +
                                ", '" + CompErr.ErrorText + ";" +
                                Environment.NewLine + Environment.NewLine;
                }
                OutCode = CodeError;
                return false;
            }
            else
            {
                //Successful Compile
                //ErroBox.ForeColor = Color.Blue;
                OutCode = "Success!";
                //If we clicked run then launch our EXE
                Process.Start(Output);
                return true;
            }
        }
        /// <summary>
        /// 编译代码(C语言)
        /// </summary>
        /// <param name="CodeString">要编译的代码字符串</param>
        /// <param name="OutCode">编译代码时候生成的消息(报错消息和成功消息)</param>
        /// <param name="OutPath">输出路径</param>
        /// <param name="projectName">项目名称</param>
        /// <returns>是否编译成功</returns>
        public static bool AnalyTicalCode(string CodeString, out string OutCode, string OutPath,string projectName)
        {
            try
            {
                ///可执行文件的进程
                Process exeprocess;
                string filename = projectName + ".cpp";
                ///写入文件
                LoggerHelp.WriteMessageToFile(CodeString, filename, OutPath);
                ///执行编译
                exeprocess = Process.Start(CLanguageEditorPath, OutPath + filename + " /Fe:" + OutPath + projectName + ".exe");
                ///等待进程结束
                exeprocess.WaitForExit();
                ///开启编译好的进程
                Process.Start(OutPath + projectName + ".exe");
                OutCode = "Success";
            }
            catch (Exception ex)
            {
                OutCode = ex.Message;
                ///写入错误日志
                LoggerHelp.WriteLogger(ex.ToString());
            }
            return true;
        }
    }
}
