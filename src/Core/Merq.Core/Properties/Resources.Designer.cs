﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Merq {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Merq.Core.Properties.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to The command handler &apos;{0}&apos; cannot be used because an existing one is already registered for the command type &apos;{1}&apos;..
        /// </summary>
        internal static string CommandBus_DuplicateHandler {
            get {
                return ResourceManager.GetString("CommandBus_DuplicateHandler", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Command handler &apos;{0}&apos; does not implement ICommandHandler or IAsyncCommandHandler generic interfaces..
        /// </summary>
        internal static string CommandBus_InvalidHandler {
            get {
                return ResourceManager.GetString("CommandBus_InvalidHandler", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No command handler is registered for command type {0}..
        /// </summary>
        internal static string CommandBus_NoHandler {
            get {
                return ResourceManager.GetString("CommandBus_NoHandler", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Published events must be public types..
        /// </summary>
        internal static string EventStream_PublishedEventNotPublic {
            get {
                return ResourceManager.GetString("EventStream_PublishedEventNotPublic", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Subscribed events must be public types..
        /// </summary>
        internal static string EventStream_SubscribedEventNotPublic {
            get {
                return ResourceManager.GetString("EventStream_SubscribedEventNotPublic", resourceCulture);
            }
        }
    }
}
