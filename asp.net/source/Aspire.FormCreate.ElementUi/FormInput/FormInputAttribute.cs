using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Aspire.FormCreate.ElementUi
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FormInputAttribute : FormItemAttribute
    {
        public FormInputAttribute(string title) : base(title, "input")
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="type">类型：https://developer.mozilla.org/en-US/docs/Web/HTML/Element/input#Form_%3Cinput%3E_types</param>
        public FormInputAttribute(string title, string type) : base(title, type)
        {

        }

        public override Dictionary<string, object> GetFormRule(PropertyInfo property)
        {
            var dic = base.GetFormRule(property);
            var propsAttribute = property.GetCustomAttributes<FormInputPropsAttribute>().FirstOrDefault();
            if (propsAttribute != null)
            {
                dic.Add("props", Util.FormRuleToJson(property, propsAttribute));
            }
            return dic;
        }
    }
}
