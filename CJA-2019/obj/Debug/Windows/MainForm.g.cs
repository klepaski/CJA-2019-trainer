﻿#pragma checksum "..\..\..\Windows\MainForm.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "1749B41D642B5CB6B175179A32B02FD6"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using CJA_2019.Windows;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Transitions;
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


namespace CJA_2019.Windows {
    
    
    /// <summary>
    /// MainForm
    /// </summary>
    public partial class MainForm : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 19 "..\..\..\Windows\MainForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid gridTop;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\Windows\MainForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCloseWindow;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\Windows\MainForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid gridOwner;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\Windows\MainForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.ImageBrush image;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\Windows\MainForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnGoToProfile;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\Windows\MainForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnExitToProfile;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\..\Windows\MainForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txbLogin;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\..\Windows\MainForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView listPanel;
        
        #line default
        #line hidden
        
        
        #line 104 "..\..\..\Windows\MainForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Ellipse ellReminder;
        
        #line default
        #line hidden
        
        
        #line 109 "..\..\..\Windows\MainForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txbCountReminder;
        
        #line default
        #line hidden
        
        
        #line 137 "..\..\..\Windows\MainForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid gridBackGround;
        
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
            System.Uri resourceLocater = new System.Uri("/CJA-2019;component/windows/mainform.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Windows\MainForm.xaml"
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
            this.gridTop = ((System.Windows.Controls.Grid)(target));
            
            #line 19 "..\..\..\Windows\MainForm.xaml"
            this.gridTop.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.gridTop_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnCloseWindow = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\..\Windows\MainForm.xaml"
            this.btnCloseWindow.Click += new System.Windows.RoutedEventHandler(this.btnCloseWindow_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.gridOwner = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            this.image = ((System.Windows.Media.ImageBrush)(target));
            return;
            case 5:
            this.btnGoToProfile = ((System.Windows.Controls.Button)(target));
            
            #line 62 "..\..\..\Windows\MainForm.xaml"
            this.btnGoToProfile.Click += new System.Windows.RoutedEventHandler(this.btnGoToProfile_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnExitToProfile = ((System.Windows.Controls.Button)(target));
            
            #line 64 "..\..\..\Windows\MainForm.xaml"
            this.btnExitToProfile.Click += new System.Windows.RoutedEventHandler(this.btnExitToProfile_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.txbLogin = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            this.listPanel = ((System.Windows.Controls.ListView)(target));
            
            #line 69 "..\..\..\Windows\MainForm.xaml"
            this.listPanel.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ListView_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 9:
            this.ellReminder = ((System.Windows.Shapes.Ellipse)(target));
            return;
            case 10:
            this.txbCountReminder = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 11:
            this.gridBackGround = ((System.Windows.Controls.Grid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

