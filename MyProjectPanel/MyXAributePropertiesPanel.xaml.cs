using MyXAribute;
using MyXObject;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace MyProjectPanel
{
    /// <summary>
    /// MyXAributePropertiesPanel.xaml 的交互逻辑
    /// </summary>
    public partial class MyXAributePropertiesPanel : UserControl
    {
        #region 构造函数
        public MyXAributePropertiesPanel()
        {
            InitializeComponent();
        }
        #endregion
        #region 自定义属性
        /// <summary>
        /// 绑定的数据
        /// </summary>
        private XAribute BindingData;
        #endregion

        #region 自定义函数
        /// <summary>
        /// 更换绑定数据
        /// </summary>
        /// <param name="bute">要更换的绑定数据</param>
        /// <param name="data">所有类型数据</param>
        public void ChangeBindingData(XAribute bute, List<string> data)
        {
            BindingData = bute;
            #region 初始化信息
            InitOpenType();
            InitProType(data);
            InitListType();
            InitHintText();
            #endregion
        }
        /// <summary>
        /// 初始化公开类型
        /// </summary>
        protected void InitOpenType()
        {
            List<string> opens = new List<string>();
            foreach(XObject.OpenType type in Enum.GetValues(typeof(XObject.OpenType)))
            {
                opens.Add(type.ToString());
            }
            OpenType.ItemsSource = opens;

            if (BindingData != null)
                OpenType.SelectedItem = BindingData.MyOpenType.ToString();
        }
        /// <summary>
        /// 初始化字段类型
        /// </summary>
        public void InitProType(List<string> data)
        {
            ProType.ItemsSource = data;
            if (BindingData != null)
                ProType.SelectedItem = BindingData.ExName;
        }
        /// <summary>
        /// 初始化数组类型
        /// </summary>
        protected void InitListType()
        {
            List<string> lists = new List<string>();

            foreach(XAribute.XAttributeSpec spec in Enum.GetValues(typeof(XAribute.XAttributeSpec)))
            {
                lists.Add(spec.ToString());
            }
            ListType.ItemsSource = lists;

            if (BindingData != null)
                ListType.SelectedItem = BindingData.SelectSpc.ToString();
        }
        /// <summary>
        /// 初始化提示信息
        /// </summary>
        protected void InitHintText()
        {
            if(BindingData != null)
                HintText.Text = BindingData.Hint;
        }
        #endregion
    }
}
