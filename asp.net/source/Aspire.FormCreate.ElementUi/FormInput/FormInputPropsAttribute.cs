﻿using System;

namespace Aspire.FormCreate.ElementUi
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class FormInputPropsAttribute : Attribute
    {
        /// <summary>
        /// 原生属性，最大输入长度
        /// <para>默认值: null</para>
        /// </summary>
        public int Maxlength { get; set; } = 0;
        /// <summary>
        /// 最小输入长度
        /// <para>默认值: null</para>
        /// </summary>
        public int Minlength { get; set; }
        /// <summary>
        /// 输入框占位文本
        /// <para>默认值: null</para>
        /// </summary>
        public string Placeholder { get; set; }
        /// <summary>
        /// 是否可清空
        /// <para>默认值: null</para>
        /// </summary>
        public bool Clearable { get; set; }
        /// <summary>
        /// 禁用
        /// <para>默认值: null</para>
        /// </summary>
        public bool Disabled { get; set; }
        /// <summary>
        /// 输入框尺寸，只在 type!="textarea" 时有效
        /// <para>默认值: null</para>
        /// </summary>
        public SizeEnum Size { get; set; }
        /// <summary>
        /// 输入框头部图标
        /// <para>默认值: null</para>
        /// </summary>
        public string PrefixIcon { get; set; }
        /// <summary>
        /// 输入框尾部图标
        /// <para>默认值: null</para>
        /// </summary>
        public string SuffixIcon { get; set; }
        /// <summary>
        /// 输入框行数，只对 Type="textarea" 有效
        /// <para>默认值: 2</para>
        /// </summary>
        public int Rows { get; set; }
        /// <summary>
        /// 自动补全
        /// <para>默认值: off</para>
        /// </summary>
        public OnOffEnum Autocomplete { get; set; }
        /// <summary>
        /// Name
        /// <para>默认值: null</para>
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 是否只读
        /// <para>默认值: null</para>
        /// </summary>
        public bool Readonly { get; set; }
        /// <summary>
        /// 设置最大值
        /// <para>默认值: null</para>
        /// </summary>
        public string Max { get; set; }
        /// <summary>
        /// 设置最小值
        /// <para>默认值: null</para>
        /// </summary>
        public string Min { get; set; }
        /// <summary>
        /// 设置输入字段的合法数字间隔
        /// <para>默认值: null</para>
        /// </summary>
        public string Step { get; set; }
        /// <summary>
        /// 控制是否能被用户缩放
        /// <para>默认值: null</para>
        /// <para>可选值: none / both / horizontal / vertical</para>
        /// </summary>
        public string Resize { get; set; }
        /// <summary>
        /// 自动获取焦点
        /// <para>默认值: false</para>
        /// </summary>
        public bool Autofocus { get; set; }
        /// <summary>
        /// 输入框关联的label文字
        /// <para>默认值: null</para>
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// tabindex
        /// <para>默认值: null</para>
        /// </summary>
        public string Tabindex { get; set; }
        /// <summary>
        /// 输入时是否触发表单的校验
        /// <para>默认值: true</para>
        /// </summary>
        public bool ValidateEvent { get; set; }
    }
}
