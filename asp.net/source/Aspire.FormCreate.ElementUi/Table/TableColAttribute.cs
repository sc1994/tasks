using System;
using System.Collections.Generic;
using System.Reflection;

using Aspire.Application.AppServices;

using static Aspire.Utils.ConvertHelper;

namespace Aspire.FormCreate.ElementUi.Table
{
    [AttributeUsage(AttributeTargets.Property)]
    public class TableColAttribute : Attribute, IAspireTable
    {
        private readonly string _colName;

        public TableColAttribute(string colName)
        {
            _colName = colName;
        }

        public Dictionary<string, object> GetTableRule(PropertyInfo property)
        {
            return new Dictionary<string, object>
            {
                {"name", _colName},
                {"field", ToLowercaseFirstCharacter(property.Name)}
            };
        }
    }
}
