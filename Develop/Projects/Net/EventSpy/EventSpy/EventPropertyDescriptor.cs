using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Savchin.EventSpy.Core;

namespace Savchin.EventSpy
{

    public class EventPropertyDescriptor : PropertyDescriptor
    {
        // Fields
        private TypeConverter _converter;
        private EventDescriptor _eventDesc;
        // private EventBindingService _eventSvc;

        // Methods
        internal EventPropertyDescriptor(EventDescriptor eventDesc)
            : base(eventDesc, null)
        {
            this._eventDesc = eventDesc;

        }

        public override bool CanResetValue(object component)
        {
            return (this.GetValue(component) != null);
        }

        public override object GetValue(object component)
        {
            return EventSpyCore.EventListenerManager.HasListener((Control)component, _eventDesc.Name).ToString();
        }

        public override void ResetValue(object component)
        {
            this.SetValue(component, null);
        }

        public override void SetValue(object component, object value)
        {
            if (this.IsReadOnly)
            {
                Exception exception = new InvalidOperationException("EventBindingServiceEventReadOnly");
                exception.HelpLink = "EventBindingServiceEventReadOnly";
                throw exception;
            }
            if (value == null)
                return;
            if (!(value is string))
            {
                Exception exception2 = new ArgumentException("EventBindingServiceBadArgType");
                exception2.HelpLink = "EventBindingServiceBadArgType";
                throw exception2;
            }
            bool hasListener = EventSpyCore.EventListenerManager.HasListener((Control)component, _eventDesc.Name);
            bool setListener = bool.Parse(value.ToString());
            if (setListener == hasListener)
            {
                return;
            }
            EventInfo eventInfo = component.GetType().GetEvent(_eventDesc.Name);
            var listener = EventSpyCore.EventListenerManager.GetOrCreateListener((Control)component, _eventDesc.Name);

            if (setListener)
            {
                MulticastDelegate recorder = listener.CreateDelegate(eventInfo);
                if (recorder != null)
                    eventInfo.AddEventHandler(component, recorder);
                else
                    EventSpyCore.EventListenerManager.RemoveListener((Control)component, _eventDesc.Name);

            }
            else
            {
                listener = EventSpyCore.EventListenerManager.GetOrCreateListener((Control)component, _eventDesc.Name);
                eventInfo.RemoveEventHandler(component, listener.Listener);
                EventSpyCore.EventListenerManager.RemoveListener((Control)component, _eventDesc.Name);
            }


        
        }

      
        public override bool ShouldSerializeValue(object component)
        {
            return this.CanResetValue(component);
        }

        // Properties
        public override Type ComponentType
        {
            get
            {
                return this._eventDesc.ComponentType;
            }
        }

        public override TypeConverter Converter
        {
            get
            {
                if (this._converter == null)
                {
                    this._converter = new EventConverter(this._eventDesc);
                }
                return this._converter;
            }
        }

        internal EventDescriptor Event
        {
            get
            {
                return this._eventDesc;
            }
        }

        public override bool IsReadOnly
        {
            get
            {
                return this.Attributes[typeof(ReadOnlyAttribute)].Equals(ReadOnlyAttribute.Yes);
            }
        }

        public override Type PropertyType
        {
            get
            {
                return this._eventDesc.EventType;
            }
        }

        // Nested Types
        private class EventConverter : TypeConverter
        {
            // Fields
            private EventDescriptor _evt;

            // Methods
            internal EventConverter(EventDescriptor evt)
            {
                this._evt = evt;
            }

            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                return ((sourceType == typeof(string)) || base.CanConvertFrom(context, sourceType));
            }

            public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            {
                return ((destinationType == typeof(string)) || base.CanConvertTo(context, destinationType));
            }

            public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
            {
                if (value != null)
                {
                    if (!(value is string))
                    {
                        return base.ConvertFrom(context, culture, value);
                    }
                    if (((string)value).Length == 0)
                    {
                        return null;
                    }
                }
                return value;
            }

            public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
            {
                if (destinationType != typeof(string))
                {
                    return base.ConvertTo(context, culture, value, destinationType);
                }
                if (value != null)
                {
                    return value;
                }
                return string.Empty;
            }

            public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
            {
                string[] values = new[] { "True", "False" };

                return new StandardValuesCollection(values);
            }

            public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
            {
                return false;
            }

            public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
            {
                return true;
            }
        }

        private class ReferenceEventClosure
        {
            // Fields
            private EventPropertyDescriptor propertyDescriptor;
            private object reference;

            // Methods
            public ReferenceEventClosure(object reference, EventPropertyDescriptor prop)
            {
                this.reference = reference;
                this.propertyDescriptor = prop;
            }

            public override bool Equals(object otherClosure)
            {
                if (!(otherClosure is ReferenceEventClosure))
                {
                    return false;
                }
                ReferenceEventClosure closure = (ReferenceEventClosure)otherClosure;
                return ((closure.reference == this.reference) && closure.propertyDescriptor.Equals(this.propertyDescriptor));
            }

            public override int GetHashCode()
            {
                return (this.reference.GetHashCode() * this.propertyDescriptor.GetHashCode());
            }
        }
    }

}
