using System;
using System.Collections.Generic;
using System.Reflection;

using Aspire.Application.AppServices;

namespace Aspire.FormCreate.ElementUi
{
    [AttributeUsage(AttributeTargets.Class)]
    public abstract class FormItemAttribute : Attribute, IAspireForm
    {
        public string Title { get; }
        public string Type { get; }

        protected FormItemAttribute(string title, string type)
        {
            Title = title;
            Type = type;
        }

        public virtual Dictionary<string, object> GetFormRule(PropertyInfo property)
        {
            return Util.FormRuleToJson(property, this);
        }
    }
}
