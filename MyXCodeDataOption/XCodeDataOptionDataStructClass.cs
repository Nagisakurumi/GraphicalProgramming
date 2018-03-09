using MyXAributeDataItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyXCodeDataOption
{
    public class XCodeDataOptionDataStructClass
    {
        /// <summary>
        /// 属性数据项
        /// </summary>
        public List<XAributeDataItem> nodeList { get; set; }
        /// <summary>
        /// 代码块的标题
        /// </summary>
        public string CodeboxName { get; set; }
        /// <summary>
        /// 代码块的提示信息
        /// </summary>
        public string CodeBoxHitText { get; set; }
        /// <summary>
        /// 代码块类型
        /// </summary>
        public string CodeBoxType { get; set; }
        /// <summary>
        /// 系统代码块的编译代码
        /// </summary>
        public string CodeBoxSystemCodeString { get; set; }
        /// <summary>
        /// 代码块的输出值
        /// </summary>
        public string ReturnValue { get; set; }
    }
}
