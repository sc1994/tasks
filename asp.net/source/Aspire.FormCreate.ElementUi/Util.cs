using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using static Aspire.Utils.ConvertHelper;

namespace Aspire.FormCreate.ElementUi
{
    internal class Util
    {
        public static Dictionary<string, object> FormRuleToJson(PropertyInfo property, object ruleInstance)
        {
            var typeThis = ruleInstance.GetType();
            var param = typeThis
                .GetConstructors()
                .OrderBy(x => x.GetParameters().Length)
                .First()
                .GetParameters()
                .Select(x => (object)null)
                .ToArray();
            var defaultThis = param.Any()
                ? Activator.CreateInstance(typeThis, param)
                : Activator.CreateInstance(typeThis);

            // 验证规则
            var validateAttributes = property.GetCustomAttributes<FormValidateAttribute>();
            IEnumerable<Dictionary<string, object>> validate = new List<Dictionary<string, object>>();
            if (validateAttributes.Any())
            {
                var defaultValidate = Activator.CreateInstance(typeof(FormValidateAttribute));
                validate = validateAttributes.Select(
                   item => item.GetType().GetProperties()
                       .Where(x => x.Name != "TypeId")
                       .Where(x =>
                       {
                           var v = x.GetValue(item);
                           if (v == null) return false;
                           if (x.Name == "Type") return true;
                           return !v.Equals(x.GetValue(defaultValidate));
                       })
                       .ToDictionary(x => ToLowercaseFirstCharacter(x.Name), x => x.GetValue(item)));
            }

            var defaultDto = Activator.CreateInstance(property.DeclaringType!);

            return typeThis.GetProperties()
                .Where(x => x.Name != "TypeId")
                .Where(x =>
                {
                    var v = x.GetValue(ruleInstance);
                    if (v == null) return false;
                    if (x.Name == "Type") return true;
                    return !v.Equals(x.GetValue(defaultThis));
                })
                .ToDictionary(
                    x => ToLowercaseFirstCharacter(x.Name),
                    x => x.GetValue(ruleInstance))
                .Append(new KeyValuePair<string, object>("field", ToLowercaseFirstCharacter(property.Name)))
                .Append(new KeyValuePair<string, object>("value", property.GetValue(defaultDto)))
                .Append(new KeyValuePair<string, object>("validate", validate))
                .ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
