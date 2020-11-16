using System;

namespace Aspire.FormCreate.ElementUi
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class FormSelectOptionAttribute : FormItemOptionAttribute
    {
        public FormSelectOptionAttribute(string label) : base(label)
        {
        }

        public FormSelectOptionAttribute(string label, object value) : base(label, value)
        {
        }

        public Type Enums { get; }
        public FormSelectOptionAttribute(Type enums)
        {
            Enums = enums;
        }

        public Type Type { get; }
        public string Method { get; }
        public FormSelectOptionAttribute(Type type, string method)
        {
            Type = type;
            Method = method;
        }
    }
}
