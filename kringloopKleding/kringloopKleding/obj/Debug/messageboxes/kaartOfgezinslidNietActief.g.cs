﻿#pragma checksum "..\..\..\messageboxes\kaartOfgezinslidNietActief.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "EA1553C491E17C29ADB03A35B878C365ACBF51901C2AAD189B9ED18784D3A3B0"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
using kringloopKleding.messageboxes;


namespace kringloopKleding.messageboxes {
    
    
    /// <summary>
    /// kaartOfgezinslidNietActief
    /// </summary>
    public partial class kaartOfgezinslidNietActief : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\..\messageboxes\kaartOfgezinslidNietActief.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblTitle;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\messageboxes\kaartOfgezinslidNietActief.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblText;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\messageboxes\kaartOfgezinslidNietActief.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnKaartAdd;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\messageboxes\kaartOfgezinslidNietActief.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnOk;
        
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
            System.Uri resourceLocater = new System.Uri("/kringloopKleding;component/messageboxes/kaartofgezinslidnietactief.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\messageboxes\kaartOfgezinslidNietActief.xaml"
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
            this.lblTitle = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.lblText = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.btnKaartAdd = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\..\messageboxes\kaartOfgezinslidNietActief.xaml"
            this.btnKaartAdd.Click += new System.Windows.RoutedEventHandler(this.btnKaartAdd_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnOk = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\..\messageboxes\kaartOfgezinslidNietActief.xaml"
            this.btnOk.Click += new System.Windows.RoutedEventHandler(this.btnOk_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

