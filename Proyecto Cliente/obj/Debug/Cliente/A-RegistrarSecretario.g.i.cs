﻿#pragma checksum "..\..\..\Cliente\A-RegistrarSecretario.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "DB8124DB25A3C5FCF7DEA58319113954D9B8744625B3D0BEBC9A9FDABD0813B3"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using Proyecto_Cliente.Cliente;
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


namespace Proyecto_Cliente.Cliente {
    
    
    /// <summary>
    /// A_RegistrarSecretario
    /// </summary>
    public partial class A_RegistrarSecretario : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 23 "..\..\..\Cliente\A-RegistrarSecretario.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignThemes.Wpf.DialogHost DialogHost;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\Cliente\A-RegistrarSecretario.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton themeToggle;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\..\Cliente\A-RegistrarSecretario.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbCorreoElectronico;
        
        #line default
        #line hidden
        
        
        #line 82 "..\..\..\Cliente\A-RegistrarSecretario.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbContraseña;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\..\Cliente\A-RegistrarSecretario.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbClaveEmpleado;
        
        #line default
        #line hidden
        
        
        #line 97 "..\..\..\Cliente\A-RegistrarSecretario.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbNombre;
        
        #line default
        #line hidden
        
        
        #line 103 "..\..\..\Cliente\A-RegistrarSecretario.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbApellidos;
        
        #line default
        #line hidden
        
        
        #line 107 "..\..\..\Cliente\A-RegistrarSecretario.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker dpFechaNacimiento;
        
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
            System.Uri resourceLocater = new System.Uri("/Proyecto Cliente;component/cliente/a-registrarsecretario.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Cliente\A-RegistrarSecretario.xaml"
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
            this.DialogHost = ((MaterialDesignThemes.Wpf.DialogHost)(target));
            return;
            case 2:
            
            #line 36 "..\..\..\Cliente\A-RegistrarSecretario.xaml"
            ((System.Windows.Shapes.Ellipse)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.minimizeWindow);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 43 "..\..\..\Cliente\A-RegistrarSecretario.xaml"
            ((System.Windows.Shapes.Ellipse)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.btnCloseWindow_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.themeToggle = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            
            #line 54 "..\..\..\Cliente\A-RegistrarSecretario.xaml"
            this.themeToggle.Click += new System.Windows.RoutedEventHandler(this.themeToggle_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.tbCorreoElectronico = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.tbContraseña = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.tbClaveEmpleado = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.tbNombre = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.tbApellidos = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            this.dpFechaNacimiento = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 11:
            
            #line 117 "..\..\..\Cliente\A-RegistrarSecretario.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_ClicRegresar);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 120 "..\..\..\Cliente\A-RegistrarSecretario.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_ClicGuardar);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

