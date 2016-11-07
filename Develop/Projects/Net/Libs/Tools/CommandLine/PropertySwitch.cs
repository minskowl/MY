using System;
using System.Reflection;

namespace Savchin.CommandLine
{
    /// <summary>
    /// PropertySwitch
    /// </summary>
    internal class PropertySwitch : BaseSwitch
    {
        private bool _isSetted;
        public PropertySwitch(SwitchAttribute data, PropertyInfo property, object o)
            : base(data, property, o)
        {
        }

        protected override void SetValue(string value)
        {
            if (_isSetted)
                throw new InvalidOperationException(string.Format("Switch {0} is duplicated.", Data.Names[0]));
            Property.SetValue(Object, GetValue(value), null);

            _isSetted = true;
        }

        private object GetValue(string value)
        {
            return Property.PropertyType.Equals(typeof(bool)) ?
                true : Convert.ChangeType(value, Property.PropertyType);
        }

        public override void  Validate()
        {
            if(!_isSetted && Data.Required)
                throw new InvalidOperationException(string.Format("Switch {0} is required.", Data.Names[0]));
        }
    }
}
