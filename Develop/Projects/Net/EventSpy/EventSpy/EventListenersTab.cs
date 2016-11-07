using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms.Design;
using Savchin.EventSpy.Properties;

namespace Savchin.EventSpy
{
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class EventListenersTab : PropertyTab
    {
        private bool sunkEvent;

        /// <summary>
        /// Gets the bitmap that is displayed for the <see cref="T:System.Windows.Forms.Design.PropertyTab"/>.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// The <see cref="T:System.Drawing.Bitmap"/> to display for the <see cref="T:System.Windows.Forms.Design.PropertyTab"/>.
        /// </returns>
        /// <PermissionSet>
        /// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
        /// </PermissionSet>
        public override System.Drawing.Bitmap Bitmap
        {
            get
            {
                return Resources.Event;
            }
        }
                // Properties
        public override string HelpKeyword
        {
            get
            {
                return "Events";
            }
        }


        public override string TabName
        {
            get { return "Event Spy"; }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="EventListenersTab"/> class.
        /// </summary>
        public EventListenersTab()
        {
        }

        ///// <summary>
        ///// Initializes a new instance of the <see cref="EventListenersTab"/> class.
        ///// </summary>
        ///// <param name="sp">The sp.</param>
        //public EventListenersTab(IServiceProvider sp)
        //{
        //    this.sp = sp;
        //}

        public override bool CanExtend(object extendee)
        {
            return !Marshal.IsComObject(extendee);
        }

        public override PropertyDescriptor GetDefaultProperty(object obj)
        {
            //IEventBindingService eventPropertyService = this.GetEventPropertyService(obj, null);
            //if (eventPropertyService != null)
            //{
            //    EventDescriptor defaultEvent = TypeDescriptor.GetDefaultEvent(obj);
            //    if (defaultEvent != null)
            //    {
            //        return eventPropertyService.GetEventProperty(defaultEvent);
            //    }
            //}
            return null;
        }

        //private IEventBindingService GetEventPropertyService(object obj, ITypeDescriptorContext context)
        //{
        //    IEventBindingService service = null;
        //    if (!this.sunkEvent)
        //    {
        //  //      IDesignerEventService service2 = (IDesignerEventService)this.sp.GetService(typeof(IDesignerEventService));
        //        //if (service2 != null)
        //        //{
        //        //    service2.ActiveDesignerChanged += new ActiveDesignerEventHandler(this.OnActiveDesignerChanged);
        //        //}
        //        this.sunkEvent = true;
        //    }
        //    if ((service == null) && (this.currentHost != null))
        //    {
        //        service = (IEventBindingService)this.currentHost.GetService(typeof(IEventBindingService));
        //    }
        //    if ((service == null) && (obj is IComponent))
        //    {
        //        ISite site = ((IComponent)obj).Site;
        //        if (site != null)
        //        {
        //            service = (IEventBindingService)site.GetService(typeof(IEventBindingService));
        //        }
        //    }
        //    if ((service == null) && (context != null))
        //    {
        //        service = (IEventBindingService)context.GetService(typeof(IEventBindingService));
        //    }
        //    return service;
        //}

        public override PropertyDescriptorCollection GetProperties(object component, Attribute[] attributes)
        {
            return this.GetProperties(null, component, attributes);
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object component, Attribute[] attributes)
        {
            //IEventBindingService eventPropertyService = this.GetEventPropertyService(component, context);
            //if (eventPropertyService == null)
            //{
            //    return new PropertyDescriptorCollection(null);
            //}
            EventDescriptorCollection events = TypeDescriptor.GetEvents(component, attributes);
           // PropertyDescriptorCollection eventProperties = eventPropertyService.GetEventProperties(events);
            PropertyDescriptorCollection eventProperties = GetEventProperties(events);
            Attribute[] destinationArray = new Attribute[attributes.Length + 1];
            Array.Copy(attributes, 0, destinationArray, 0, attributes.Length);
            destinationArray[attributes.Length] = DesignerSerializationVisibilityAttribute.Content;
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(component, destinationArray);


            if (properties.Count > 0)
            {
                ArrayList list = null;
                for (int i = 0; i < properties.Count; i++)
                {
                    PropertyDescriptor oldPropertyDescriptor = properties[i];
                    if (oldPropertyDescriptor.Converter.GetPropertiesSupported() && (TypeDescriptor.GetEvents(oldPropertyDescriptor.GetValue(component), attributes).Count > 0))
                    {
                        if (list == null)
                        {
                            list = new ArrayList();
                        }
                        oldPropertyDescriptor = TypeDescriptor.CreateProperty(oldPropertyDescriptor.ComponentType, oldPropertyDescriptor, new Attribute[] { MergablePropertyAttribute.No });
                        list.Add(oldPropertyDescriptor);
                    }
                }
                if (list != null)
                {
                    PropertyDescriptor[] array = new PropertyDescriptor[list.Count];
                    list.CopyTo(array, 0);
                    PropertyDescriptor[] descriptorArray2 = new PropertyDescriptor[eventProperties.Count + array.Length];
                    eventProperties.CopyTo(descriptorArray2, 0);
                    Array.Copy(array, 0, descriptorArray2, eventProperties.Count, array.Length);
                    eventProperties = new PropertyDescriptorCollection(descriptorArray2);
                }
            }
            return eventProperties;
        }

        //private void OnActiveDesignerChanged(object sender, ActiveDesignerEventArgs adevent)
        //{
        //    this.currentHost = adevent.NewDesigner;
        //}
        private Hashtable _eventProperties;
        PropertyDescriptorCollection GetEventProperties(EventDescriptorCollection events)
        {
            if (events == null)
            {
                throw new ArgumentNullException("events");
            }
            List<PropertyDescriptor> list = new List<PropertyDescriptor>(events.Count);
            if (this._eventProperties == null)
            {
                this._eventProperties = new Hashtable();
            }
            for (int i = 0; i < events.Count; i++)
            {
                if (!this.HasGenericArgument(events[i]))
                {
                    object eventDescriptorHashCode = this.GetEventDescriptorHashCode(events[i]);
                    PropertyDescriptor item = (PropertyDescriptor)this._eventProperties[eventDescriptorHashCode];
                    if (item == null)
                    {
                        item = new EventPropertyDescriptor(events[i]);
                        this._eventProperties[eventDescriptorHashCode] = item;
                    }
                    list.Add(item);
                }
            }
            return new PropertyDescriptorCollection(list.ToArray());
        }
        private string GetEventDescriptorHashCode(EventDescriptor eventDesc)
        {
            StringBuilder builder = new StringBuilder(eventDesc.Name);
            builder.Append(eventDesc.EventType.GetHashCode().ToString(CultureInfo.InvariantCulture));
            foreach (Attribute attribute in eventDesc.Attributes)
            {
                builder.Append(attribute.GetHashCode().ToString(CultureInfo.InvariantCulture));
            }
            return builder.ToString();
        }

 

 

        private bool HasGenericArgument(EventDescriptor ed)
        {
            if ((ed != null) && (ed.ComponentType != null))
            {
                EventInfo info = ed.ComponentType.GetEvent(ed.Name);
                if ((info == null) || !info.EventHandlerType.IsGenericType)
                {
                    return false;
                }
                Type[] genericArguments = info.EventHandlerType.GetGenericArguments();
                if ((genericArguments != null) && (genericArguments.Length > 0))
                {
                    for (int i = 0; i < genericArguments.Length; i++)
                    {
                        if (genericArguments[i].IsGenericType)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }



 



    }




}
