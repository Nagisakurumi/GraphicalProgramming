using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Media;

namespace MyXToolsPanel
{
    /// <summary>
    /// MyPropertiesToolPanel.xaml 的交互逻辑
    /// </summary>
    public partial class MyPropertiesToolPanel :UserControl
    {
        #region 初始化
        public MyPropertiesToolPanel()
        {
            InitializeComponent();
        }
        #endregion
        #region 自定义属性
        /// <summary>
        /// 目标对象
        /// </summary>
        private object _targetObj;
        /// <summary>
        /// 目标解析类型
        /// </summary>
        private Type _targetType;
        /// <summary>
        /// 不显示的属性列表
        /// </summary>
        private List<string> _notDirList = new List<string>();
        #endregion
        #region 读取器
        /// <summary>
        /// 目标对象
        /// </summary>
        public object TargetObj
        {
            get
            {
                return _targetObj;
            }

            set
            {
                _targetObj = value;
            }
        }
        /// <summary>
        /// 目标解析类型
        /// </summary>
        public Type TargetType
        {
            get
            {
                return _targetType;
            }

            set
            {
                _targetType = value;
            }
        }
        #endregion
        #region 自定义函数
        /// <summary>
        /// 设置目标解析
        /// </summary>
        /// <param name="obj">目标属性</param>
        /// <param name="type">已经反射过的类型</param>
        public void SetTargetObj(object obj)
        {
            ///赋值目标对象
            TargetObj = obj;
            ///赋值解析对象
            TargetType = obj.GetType();        
        }
        /// <summary>
        /// 添加一个不需要显示的属性
        /// </summary>
        /// <param name="infos">属性名称列表</param>
        public void AddNotDirProperties(string name)
        {
            if(!this._notDirList.Contains(name))
            {
                this._notDirList.Add(name);
            }
        }
        /// <summary>
        /// 设置不需要显示的属性名称列表
        /// </summary>
        /// <param name="names">列表内容</param>
        public void AddNotDirProperties(List<string> names)
        {
            this._notDirList = names;
        }
        /// <summary>
        /// 将绑定好的数据去面板显示
        /// </summary>
        public void Play()
        {       
            ///如果还没有设置目标解析则直接退出
            if (TargetType == null)
                return;
            ///先清空面板
            ContentList.Items.Clear();
            ///获取所有属性
            PropertyInfo[] myPropers = TargetType.GetProperties();
            foreach (PropertyInfo info in myPropers)
            {
                if(CheckIsToDir(info))
                {
                    ///显示属性
                    DirProperty(info);
                }
            }
        }
        /// <summary>
        /// 检测该属性是否要显示
        /// </summary>
        /// <param name="info">要检测的属性信息</param>
        /// <returns>是否可以显示</returns>
        protected bool CheckIsToDir(PropertyInfo info)
        {
            ///至少有一个设置器和访问器且不存在在不显示列表中
            if (info.GetSetMethod() != null && info.GetGetMethod() != null && !_notDirList.Contains(info.Name))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 显示属性
        /// </summary>
        /// <param name="info">要显示的属性</param>
        protected  void DirProperty(PropertyInfo info)
        {

            ///解析该属性的类型
            switch (info.PropertyType.FullName)
            {
                ///如果是字符串类型
                case "System.String":
                    if (IsExName(info))
                    {
                        ///添加一个展示枚举的属性控件
                        AddEnumPropertyControl(info);
                    }
                    else if(info.GetValue(TargetObj) != null)
                    {
                        ///添加一个字符串的属性控件
                        AddStringPropertyControl(info.Name, (info.GetValue(TargetObj)).ToString());
                    }
                    break;
                case "System.Int32":
                    AddStringPropertyControl(info.Name, (info.GetValue(TargetObj)).ToString());
                    break;
                case "System.Single":
                    AddStringPropertyControl(info.Name, (info.GetValue(TargetObj)).ToString());
                    break;
                case "System.Double":
                    AddStringPropertyControl(info.Name, (info.GetValue(TargetObj)).ToString());
                    break;
                default:
                    Type t = info.PropertyType.GetType();
                    AnalyticalOtherType(info);
                    break;
            }
        }
        /// <summary>
        /// 添加一个可以转换为string类型显示的属性框
        /// </summary>
        /// <param name="name">属性名称</param>
        /// <param name="value">属性值</param>
        protected void AddStringPropertyControl(string name,string value)
        {
            StringTextControl stringtext = new StringTextControl();
            stringtext.SetInitBaseInfo(name, value, this.ChildInfoCallBack);
            //stringtext.Width = this.Width;
            //stringtext.Height = 50;
            ///添加控件
            this.ContentList.Items.Add(stringtext);
        }
        /// <summary>
        /// 添加一个属于颜色的属性框
        /// </summary>
        /// <param name="name">属性名称</param>
        /// <param name="value">属性值</param>
        protected void AddColorPropertyControl(string name, Color value)
        {
            ///创建一个颜色选择器添加
            ColorSelectorControl colorcontrol = new ColorSelectorControl();
            colorcontrol.SetInitBaseInfo(name, value, this.ChildInfoCallBack);
            ///添加颜色框
            this.ContentList.Items.Add(colorcontrol);
        }
        /// <summary>
        /// 添加一个展示枚举的的属性的控件
        /// </summary>
        /// <param name="info">要展示的属性</param>
        protected void AddEnumPropertyControl(PropertyInfo info)
        {
            SelectTypeEnum enumControl = new SelectTypeEnum();
            ///设置数据属性(初始化)
            enumControl.SetTargetDataObject(TargetObj, info);
            ///添加一个映射枚举属性的控件
            this.ContentList.Items.Add(enumControl);
        }
        /// <summary>
        /// 解析其他类型的数据
        /// </summary>
        /// <param name="info">要解析的属性</param>
        protected void AnalyticalOtherType(PropertyInfo info)
        {
            if(IsColor(info))
            {
                ///添加一个颜色框
                AddColorPropertyControl(info.Name, (Color)info.GetValue(TargetObj));
            }
            ///如果是枚举类型
            else if(info.PropertyType.IsEnum)
            {
                ///添加一个显示枚举类型的控件
                AddEnumPropertyControl(info);
            }          
        }
        /// <summary>
        /// 是否属性颜色这一类
        /// </summary>
        /// <param name="info">属性</param>
        /// <returns></returns>
        protected bool IsColor(PropertyInfo info)
        {
            Type nowtype = info.PropertyType;
            while(nowtype != null)
            {
                ///如果属于颜色
                if(nowtype.Name == "Color")
                {
                    return true;
                }
                nowtype = nowtype.BaseType;
            }
            return false;
        }
        /// <summary>
        /// 是否是ExName属性
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected bool IsExName(PropertyInfo info)
        {
            if (info.Name == "ExName")
                return true;
            return false;
        }
        #endregion
        #region 回调函数
        /// <summary>
        /// 子控件事件回调函数
        /// </summary>
        /// <param name="PropertyName">属性名称</param>
        /// <param name="Option">属性类型</param>
        /// <param name="Value">属性值</param>
        public void ChildInfoCallBack(string PropertyName, OptionType Option, object Value)
        {
            switch(Option)
            {
                ///解析颜色类型
                case OptionType.XColor:
                    AnalyticalColorProperty(PropertyName, (ColorMan)Value);
                    break;
                ///解析字符串类型的属性
                case OptionType.XString:
                    AnalyticalStringProperty(PropertyName, (string)Value);
                    break;
            }
        }
        /// <summary>
        /// 解析当子控件返回数据时候给对象赋值
        /// </summary>
        /// <param name="name">属性名称</param>
        /// <param name="value">属性值</param>
        protected void AnalyticalStringProperty(string name, string value)
        {
            ///获取当前属性
            PropertyInfo info = TargetType.GetProperty(name);
            if (info == null)
                return;
            info.SetValue(TargetObj, value);
            //if (info.PropertyType.Namespace == "System.String")
            //{
            //    info.SetValue(TargetObj, value);
            //}
            //else if(info.PropertyType.Namespace == "System.Int32")
            //{
            //    info.SetValue(TargetObj, int.Parse(value));
            //}
        }
        /// <summary>
        /// 解析当子控件返回数据的时候给对象属性赋值
        /// </summary>
        /// <param name="name">属性名称</param>
        /// <param name="Value">返回的数据</param>
        protected void AnalyticalColorProperty(string name, ColorMan value)
        {
            ///获取当前属性
            PropertyInfo info = TargetType.GetProperty(name);
            if (info == null)
                return;
            info.SetValue(TargetObj, value.ToColor());
        }
        #endregion
    }
}
