﻿#pragma checksum "..\..\wRapportage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "605DD2496D686983D16F2246DAEB7C5BF5A8FAA3D4BD24D0D20E50D563693E63"
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
using kringloopKleding;


namespace kringloopKleding {
    
    
    /// <summary>
    /// wRapportage
    /// </summary>
    public partial class wRapportage : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 17 "..\..\wRapportage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem Afhaling;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\wRapportage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem klantenBeheer;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\wRapportage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem Rapportage;
        
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
            System.Uri resourceLocater = new System.Uri("/kringloopKleding;component/wrapportage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\wRapportage.xaml"
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
            this.Afhaling = ((System.Windows.Controls.MenuItem)(target));
            
            #line 17 "..\..\wRapportage.xaml"
            this.Afhaling.Click += new System.Windows.RoutedEventHandler(this.Afhaling_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.klantenBeheer = ((System.Windows.Controls.MenuItem)(target));
            
            #line 18 "..\..\wRapportage.xaml"
            this.klantenBeheer.Click += new System.Windows.RoutedEventHandler(this.klantenBeheer_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Rapportage = ((System.Windows.Controls.MenuItem)(target));
            
            #line 19 "..\..\wRapportage.xaml"
            this.Rapportage.Click += new System.Windows.RoutedEventHandler(this.Rapportage_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

