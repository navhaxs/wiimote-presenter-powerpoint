﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WiimoteAddin.Properties {
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("WiimoteAddin.Properties.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot; ?&gt;
        ///&lt;!-- loadImage=&quot;GetImage&quot;--&gt;
        ///&lt;customUI xmlns=&quot;http://schemas.microsoft.com/office/2006/01/customui&quot; loadImage=&quot;OnGetImage&quot; onLoad=&quot;Ribbon_Load&quot;&gt;
        ///  &lt;ribbon startFromScratch=&quot;false&quot;&gt;
        ///    &lt;tabs&gt;
        ///      &lt;tab idMso=&quot;TabSlideShow&quot;&gt;
        ///        &lt;group idMso=&quot;GroupFont&quot; visible=&quot;false&quot; /&gt;
        ///        &lt;group id=&quot;WiimotePP_group&quot; label=&quot;Wii Remote&quot; getImage=&quot;ReturnImage&quot;&gt;
        ///          &lt;button id=&quot;ButtonA12&quot; label=&quot;Wiimote Setup&quot; size=&quot;large&quot; getImage =&quot;ReturnImage&quot; keytip=&quot;W1&quot; supe [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string CustomUI {
            get {
                return ResourceManager.GetString("CustomUI", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap ribbonButtonLogo {
            get {
                object obj = ResourceManager.GetObject("ribbonButtonLogo", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
    }
}
