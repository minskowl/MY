﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Savchin.Html {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class strings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal strings() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Savchin.Html.strings", typeof(strings).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unknown attribute.
        /// </summary>
        internal static string badAttr {
            get {
                return ResourceManager.GetString("badAttr", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Spaces in a closing tag.
        /// </summary>
        internal static string etHasSpaces {
            get {
                return ResourceManager.GetString("etHasSpaces", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unnamed HTML tag.
        /// </summary>
        internal static string noTagName {
            get {
                return ResourceManager.GetString("noTagName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ending tag cannot be a closed (stand-alone).
        /// </summary>
        internal static string orphanET {
            get {
                return ResourceManager.GetString("orphanET", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unmatched HTML tag {0} at line {1}.
        /// </summary>
        internal static string tagMismatch {
            get {
                return ResourceManager.GetString("tagMismatch", resourceCulture);
            }
        }
    }
}
