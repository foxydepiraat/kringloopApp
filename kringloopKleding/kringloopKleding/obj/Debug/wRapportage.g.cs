﻿#pragma checksum "..\..\wRapportage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "7C1FE25E49A793F3EFF7B7F4F42F34F36E347E240AC6F0B68BEBE40378871FEB"
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
        
        
        #line 16 "..\..\wRapportage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtKaart;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\wRapportage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnKaartnummerSearch;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\wRapportage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtFirstName;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\wRapportage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker dpRapportDatum;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\wRapportage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem Afhaling;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\wRapportage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem klantenBeheer;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\wRapportage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem Rapportage;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\wRapportage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgFamily;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\wRapportage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgFamilyMembers;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\wRapportage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgAfhaling;
        
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
            this.txtKaart = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.btnKaartnummerSearch = ((System.Windows.Controls.Button)(target));
            
            #line 17 "..\..\wRapportage.xaml"
            this.btnKaartnummerSearch.Click += new System.Windows.RoutedEventHandler(this.btnKaartnummerSearch_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.txtFirstName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.dpRapportDatum = ((System.Windows.Controls.DatePicker)(target));
            
            #line 29 "..\..\wRapportage.xaml"
            this.dpRapportDatum.CalendarClosed += new System.Windows.RoutedEventHandler(this.dpRapportDatum_CalendarClosed);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Afhaling = ((System.Windows.Controls.MenuItem)(target));
            
            #line 37 "..\..\wRapportage.xaml"
            this.Afhaling.Click += new System.Windows.RoutedEventHandler(this.Afhaling_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.klantenBeheer = ((System.Windows.Controls.MenuItem)(target));
            
            #line 38 "..\..\wRapportage.xaml"
            this.klantenBeheer.Click += new System.Windows.RoutedEventHandler(this.klantenBeheer_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.Rapportage = ((System.Windows.Controls.MenuItem)(target));
            
            #line 39 "..\..\wRapportage.xaml"
            this.Rapportage.Click += new System.Windows.RoutedEventHandler(this.Rapportage_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.dgFamily = ((System.Windows.Controls.DataGrid)(target));
            
            #line 48 "..\..\wRapportage.xaml"
            this.dgFamily.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.dgFamily_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 9:
            this.dgFamilyMembers = ((System.Windows.Controls.DataGrid)(target));
            
            #line 52 "..\..\wRapportage.xaml"
            this.dgFamilyMembers.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.dgFamilyMembers_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 10:
            this.dgAfhaling = ((System.Windows.Controls.DataGrid)(target));
            
            #line 56 "..\..\wRapportage.xaml"
            this.dgAfhaling.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.dgAfhaling_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

