﻿#pragma checksum "..\..\..\..\Windows\Pages\MySquarePage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D8F78BA9D2B459F0F5160F15ED6020A4"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using OOTPiSP_Laba1.Windows.Pages;
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


namespace OOTPiSP_Laba1.Windows.Pages {
    
    
    /// <summary>
    /// MySquarePage
    /// </summary>
    public partial class MySquarePage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\..\..\Windows\Pages\MySquarePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid GMain;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\..\Windows\Pages\MySquarePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TbName;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\..\Windows\Pages\MySquarePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TbThickness;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\..\Windows\Pages\MySquarePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border BColor;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\..\Windows\Pages\MySquarePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border BgColor;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\..\Windows\Pages\MySquarePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TbAngle;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\..\Windows\Pages\MySquarePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TbPosX;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\Windows\Pages\MySquarePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TbPosY;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\..\Windows\Pages\MySquarePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TbLength;
        
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
            System.Uri resourceLocater = new System.Uri("/OOTPiSP_Laba1;component/windows/pages/mysquarepage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Windows\Pages\MySquarePage.xaml"
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
            this.GMain = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.TbName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.TbThickness = ((System.Windows.Controls.TextBox)(target));
            
            #line 36 "..\..\..\..\Windows\Pages\MySquarePage.xaml"
            this.TbThickness.KeyDown += new System.Windows.Input.KeyEventHandler(this.Numbers_KeyDown);
            
            #line default
            #line hidden
            
            #line 36 "..\..\..\..\Windows\Pages\MySquarePage.xaml"
            this.TbThickness.LostKeyboardFocus += new System.Windows.Input.KeyboardFocusChangedEventHandler(this.Numbers_LostKeyboardFocus);
            
            #line default
            #line hidden
            
            #line 36 "..\..\..\..\Windows\Pages\MySquarePage.xaml"
            this.TbThickness.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.Numbers_PreviewKeyDown);
            
            #line default
            #line hidden
            return;
            case 4:
            this.BColor = ((System.Windows.Controls.Border)(target));
            
            #line 37 "..\..\..\..\Windows\Pages\MySquarePage.xaml"
            this.BColor.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.Color_MouseLeftButtonUp);
            
            #line default
            #line hidden
            return;
            case 5:
            this.BgColor = ((System.Windows.Controls.Border)(target));
            
            #line 38 "..\..\..\..\Windows\Pages\MySquarePage.xaml"
            this.BgColor.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.Color_MouseLeftButtonUp);
            
            #line default
            #line hidden
            return;
            case 6:
            this.TbAngle = ((System.Windows.Controls.TextBox)(target));
            
            #line 39 "..\..\..\..\Windows\Pages\MySquarePage.xaml"
            this.TbAngle.KeyDown += new System.Windows.Input.KeyEventHandler(this.Numbers_KeyDown);
            
            #line default
            #line hidden
            
            #line 39 "..\..\..\..\Windows\Pages\MySquarePage.xaml"
            this.TbAngle.LostKeyboardFocus += new System.Windows.Input.KeyboardFocusChangedEventHandler(this.Numbers_LostKeyboardFocus);
            
            #line default
            #line hidden
            
            #line 39 "..\..\..\..\Windows\Pages\MySquarePage.xaml"
            this.TbAngle.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.Numbers_PreviewKeyDown);
            
            #line default
            #line hidden
            return;
            case 7:
            this.TbPosX = ((System.Windows.Controls.TextBox)(target));
            
            #line 40 "..\..\..\..\Windows\Pages\MySquarePage.xaml"
            this.TbPosX.KeyDown += new System.Windows.Input.KeyEventHandler(this.Numbers_KeyDown);
            
            #line default
            #line hidden
            
            #line 40 "..\..\..\..\Windows\Pages\MySquarePage.xaml"
            this.TbPosX.LostKeyboardFocus += new System.Windows.Input.KeyboardFocusChangedEventHandler(this.Numbers_LostKeyboardFocus);
            
            #line default
            #line hidden
            
            #line 40 "..\..\..\..\Windows\Pages\MySquarePage.xaml"
            this.TbPosX.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.Numbers_PreviewKeyDown);
            
            #line default
            #line hidden
            return;
            case 8:
            this.TbPosY = ((System.Windows.Controls.TextBox)(target));
            
            #line 41 "..\..\..\..\Windows\Pages\MySquarePage.xaml"
            this.TbPosY.KeyDown += new System.Windows.Input.KeyEventHandler(this.Numbers_KeyDown);
            
            #line default
            #line hidden
            
            #line 41 "..\..\..\..\Windows\Pages\MySquarePage.xaml"
            this.TbPosY.LostKeyboardFocus += new System.Windows.Input.KeyboardFocusChangedEventHandler(this.Numbers_LostKeyboardFocus);
            
            #line default
            #line hidden
            
            #line 41 "..\..\..\..\Windows\Pages\MySquarePage.xaml"
            this.TbPosY.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.Numbers_PreviewKeyDown);
            
            #line default
            #line hidden
            return;
            case 9:
            this.TbLength = ((System.Windows.Controls.TextBox)(target));
            
            #line 42 "..\..\..\..\Windows\Pages\MySquarePage.xaml"
            this.TbLength.KeyDown += new System.Windows.Input.KeyEventHandler(this.Numbers_KeyDown);
            
            #line default
            #line hidden
            
            #line 42 "..\..\..\..\Windows\Pages\MySquarePage.xaml"
            this.TbLength.LostKeyboardFocus += new System.Windows.Input.KeyboardFocusChangedEventHandler(this.Numbers_LostKeyboardFocus);
            
            #line default
            #line hidden
            
            #line 42 "..\..\..\..\Windows\Pages\MySquarePage.xaml"
            this.TbLength.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.Numbers_PreviewKeyDown);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

