using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

using static Aspire.Utils.ConvertHelper;

namespace Aspire.FormCreate.ElementUi
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class FormSelectAttribute : FormItemAttribute
    {
        public FormSelectAttribute(string title) : base(title, "select")
        {
        }

        public override Dictionary<string, object> GetFormRule(PropertyInfo property)
        {
            var dic = base.GetFormRule(property);
            var optionAttributes = property
                .GetCustomAttributes<FormSelectOptionAttribute>();

            var options = new List<object>();
            optionAttributes
                .Where(x => x.Enums != null)
                .ToList()
                .ForEach(x =>
                {
                    foreach (var item in Enum.GetValues(x.Enums))
                    {
                        // 形成一组属性
                        var tmp = new Dictionary<string, object>();
                        var desc = item
                            .GetType()
                            .GetField(item.ToString()!)!
                            .GetCustomAttributes<DescriptionAttribute>()
                            .FirstOrDefault();
                        tmp.Add(ToLowercaseFirstCharacter(nameof(FormSelectOptionAttribute.Label)),
                            desc != null ? desc.Description : item.ToString());
                        tmp.Add(ToLowercaseFirstCharacter(nameof(FormSelectOptionAttribute.Value)), item.GetHashCode());
                        options.Add(tmp);
                    }
                });

            optionAttributes
                .Where(x => !string.IsNullOrWhiteSpace(x.Label))
                .ToList()
                .ForEach(x =>
                {
                    options.Add(new Dictionary<string, object>
                    {
                        {ToLowercaseFirstCharacter(nameof(FormSelectOptionAttribute.Label)), x.Label},
                        {ToLowercaseFirstCharacter(nameof(FormSelectOptionAttribute.Value)), x.Value}
                    });
                });
            if (options.Any())
            {
                dic.Add("options", options);
            }

            var propsAttribute = property.GetCustomAttributes().FirstOrDefault(x => x.GetType() == typeof(FormSelectPropsAttribute));
            if (propsAttribute != null)
            {
                dic.Add("props", Util.FormRuleToJson(property, propsAttribute));
            }

            return dic;
        }
    }
}
