﻿#pragma checksum "..\..\..\Cliente\PrincipalAdministrador.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "0AF6FF8C5578705E2152DAD11CC62EC0C26C917879E5321E9B928C6111913331"
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
    /// PrincipalAdministrador
    /// </summary>
    public partial class PrincipalAdministrador : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 23 "..\..\..\Cliente\PrincipalAdministrador.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignThemes.Wpf.DialogHost DialogHost;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\Cliente\PrincipalAdministrador.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton themeToggle;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\..\Cliente\PrincipalAdministrador.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button administrarSalones;
        
        #line default
        #line hidden
        
        
        #line 84 "..\..\..\Cliente\PrincipalAdministrador.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button administrarMateria;
        
        #line default
        #line hidden
        
        
        #line 103 "..\..\..\Cliente\PrincipalAdministrador.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button administrarEdificio;
        
        #line default
        #line hidden
        
        
        #line 118 "..\..\..\Cliente\PrincipalAdministrador.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button administrarArea;
        
        #line default
        #line hidden
        
        
        #line 137 "..\..\..\Cliente\PrincipalAdministrador.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button registrarSecretarios;
        
        #line default
        #line hidden
        
        
        #line 152 "..\..\..\Cliente\PrincipalAdministrador.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button administrarPeriodo;
        
        #line default
        #line hidden
        
        
        #line 172 "..\..\..\Cliente\PrincipalAdministrador.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button cerrarSesion;
        
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
            System.Uri resourceLocater = new System.Uri("/Proyecto Cliente;component/cliente/principaladministrador.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Cliente\PrincipalAdministrador.xaml"
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
            
            #line 36 "..\..\..\Cliente\PrincipalAdministrador.xaml"
            ((System.Windows.Shapes.Ellipse)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.minimizeWindow);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 43 "..\..\..\Cliente\PrincipalAdministrador.xaml"
            ((System.Windows.Shapes.Ellipse)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.btnCloseWindow_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.themeToggle = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            
            #line 54 "..\..\..\Cliente\PrincipalAdministrador.xaml"
            this.themeToggle.Click += new System.Windows.RoutedEventHandler(this.themeToggle_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.administrarSalones = ((System.Windows.Controls.Button)(target));
            
            #line 75 "..\..\..\Cliente\PrincipalAdministrador.xaml"
            this.administrarSalones.Click += new System.Windows.RoutedEventHandler(this.administrarSalones_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.administrarMateria = ((System.Windows.Controls.Button)(target));
            
            #line 89 "..\..\..\Cliente\PrincipalAdministrador.xaml"
            this.administrarMateria.Click += new System.Windows.RoutedEventHandler(this.administrarMateria_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.administrarEdificio = ((System.Windows.Controls.Button)(target));
            
            #line 108 "..\..\..\Cliente\PrincipalAdministrador.xaml"
            this.administrarEdificio.Click += new System.Windows.RoutedEventHandler(this.administrarEdificio_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.administrarArea = ((System.Windows.Controls.Button)(target));
            
            #line 123 "..\..\..\Cliente\PrincipalAdministrador.xaml"
            this.administrarArea.Click += new System.Windows.RoutedEventHandler(this.administrarArea_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.registrarSecretarios = ((System.Windows.Controls.Button)(target));
            
            #line 142 "..\..\..\Cliente\PrincipalAdministrador.xaml"
            this.registrarSecretarios.Click += new System.Windows.RoutedEventHandler(this.registrarSecretarios_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.administrarPeriodo = ((System.Windows.Controls.Button)(target));
            
            #line 157 "..\..\..\Cliente\PrincipalAdministrador.xaml"
            this.administrarPeriodo.Click += new System.Windows.RoutedEventHandler(this.AdministrarPeriodo_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.cerrarSesion = ((System.Windows.Controls.Button)(target));
            
            #line 177 "..\..\..\Cliente\PrincipalAdministrador.xaml"
            this.cerrarSesion.Click += new System.Windows.RoutedEventHandler(this.cerrarSesion_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

