using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Controls;
using MyXObject;

namespace MyXToolsPanel
{
    /// <summary>
    /// SelectTypeEnum.xaml 的交互逻辑
    /// </summary>
    public partial class SelectTypeEnum : UserControl
    {
        #region 初始化
        public SelectTypeEnum()
        {
            InitializeComponent();
        }
        #endregion
        #region 自定义属性
        /// <summary>
        /// 该属性的目标对象
        /// </summary>
        private object TargetObject;
        /// <summary>
        /// 要被使用的属性
        /// </summary>
        private PropertyInfo Property;
        #endregion
        #region 自定义函数
        /// <summary>
        /// 设置目标数据源
        /// </summary>
        /// <param name="targetobj">目标对象</param>
        /// <param name="info">对象的具体属性</param>
        public void SetTargetDataObject(object targetobj, PropertyInfo info)
        {
            this.TargetObject = targetobj;
            this.Property = info;
            Play();
        }
        /// <summary>
        /// 显示具体内容
        /// </summary>
        protected void Play()
        {
            try
            {
                ///数据
                Dictionary<string, string> data;
                ///获取属性的值转换为字符串
                string value = Property.GetValue(TargetObject).ToString();
                #region 如果要展示的是属性的类型的话
                ///如果要展示的是属性的类型的话
                if (Property.Name == "ExName" && TargetObject != null)
                {
                    ///获取解析类
                    Type targetType = TargetObject.GetType();
                    PropertyInfo info = targetType.GetProperty("ProjectUseClassType");
                    ///转换为需要的数据
                    data = info.GetValue(TargetObject) as Dictionary<string, string>;
                    if (data != null)
                    {
                        ///绑定数据
                        DataComBox.ItemsSource = data;
                        DataComBox.SelectedValuePath = "Key";
                        DataComBox.DisplayMemberPath = "Value";
                    }
                }
                #endregion
                #region 如果是普通枚举值
                else
                {
                    ///获取属性的类型
                    Type PropertyType = Property.PropertyType;

                    ///反射枚举为数据
                    data = AnyEnumGetData(PropertyType);
                    if (data != null)
                    {
                        ///绑定数据
                        DataComBox.ItemsSource = data;
                        DataComBox.SelectedValuePath = "Key";
                        DataComBox.DisplayMemberPath = "Value";
                    }
                }
                #endregion
                ///设置属性名称
                PropertyName.Text = Property.Name;
                ///设置默认值
                foreach (string key in data.Keys)
                {
                    if (value == key)
                    {
                        //DataComBox.SelectedItem = (KeyValuePair<string,string>
                        ///设置默认值
                        DataComBox.SelectedValue = key;
                        break;
                    }
                }
            }catch(Exception ex)
            {
                ///写入错误信息
                LoggerHelp.WriteLogger(ex.ToString());
            }
        }
        /// <summary>
        /// 解析枚举获取所有枚举情况
        /// </summary>
        /// <param name="enumtype">要解析的枚举类型</param>
        /// <returns></returns>
        protected Dictionary<string, string> AnyEnumGetData(Type enumtype)
        {
            ///声明数据
            Dictionary<string, string> enumData = new Dictionary<string, string>();
            ///反射枚举 遍历枚举值
            foreach (var item in Enum.GetValues(enumtype))
            {
                enumData.Add(item.ToString(), item.ToString());
            }
            return enumData;
        }
        #endregion
        #region 控件函数
        /// <summary>
        /// 属性值选项改变时候
        /// </summary>
        /// <param name="sender">信息发送者</param>
        /// <param name="e">信息</param>
        private void DataComBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ///检测是否为空
            if(TargetObject != null && DataComBox.SelectedIndex != -1)
            {
                ///选择的值
                KeyValuePair<string, string> value = (KeyValuePair<string, string>)DataComBox.SelectedItem; 
                if(value.Key.ToString() != Property.GetValue(TargetObject) as string)
                {
                    if (Property.Name == "ExName")
                    {
                        ///修改属性的值
                        Property.SetValue(TargetObject, value.Key);
                    }
                    else
                    {
                        ///反射枚举 遍历枚举值
                        foreach (var item in Enum.GetValues(Property.PropertyType))
                        {
                            ///检测具体值
                            if(item.ToString() == value.Key)
                            {
                                ///修改属性的值
                                Property.SetValue(TargetObject, item);
                            }
                        }                      
                    }
                }
            }
        }
        #endregion
    }
}
