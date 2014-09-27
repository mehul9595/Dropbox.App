﻿#pragma checksum "..\..\..\View\DropBoxView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D2498146483251F621A3B4F77D0F589F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Dropbox.App.Converters;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Dropbox.App.View {
    
    
    /// <summary>
    /// DropBoxView
    /// </summary>
    public partial class DropBoxView : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 20 "..\..\..\View\DropBoxView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Browse;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\View\DropBoxView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Upload;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\View\DropBoxView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Authorize;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\View\DropBoxView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox DropBox;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\View\DropBoxView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock StatusIndicator;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\View\DropBoxView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar ProgressBar;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Dropbox.App;component/view/dropboxview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\DropBoxView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Browse = ((System.Windows.Controls.Button)(target));
            return;
            case 2:
            this.Upload = ((System.Windows.Controls.Button)(target));
            return;
            case 3:
            this.Authorize = ((System.Windows.Controls.Button)(target));
            return;
            case 4:
            this.DropBox = ((System.Windows.Controls.ListBox)(target));
            
            #line 32 "..\..\..\View\DropBoxView.xaml"
            this.DropBox.Drop += new System.Windows.DragEventHandler(this.DropBox_Drop);
            
            #line default
            #line hidden
            
            #line 32 "..\..\..\View\DropBoxView.xaml"
            this.DropBox.DragOver += new System.Windows.DragEventHandler(this.DropBox_DragOver);
            
            #line default
            #line hidden
            
            #line 32 "..\..\..\View\DropBoxView.xaml"
            this.DropBox.DragLeave += new System.Windows.DragEventHandler(this.DropBox_DragLeave);
            
            #line default
            #line hidden
            return;
            case 5:
            this.StatusIndicator = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.ProgressBar = ((System.Windows.Controls.ProgressBar)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
