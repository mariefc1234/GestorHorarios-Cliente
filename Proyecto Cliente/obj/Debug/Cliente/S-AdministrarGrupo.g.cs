﻿#pragma checksum "..\..\..\Cliente\S-AdministrarGrupo.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "8E79AFD04A46E51CC10709122D545939A43AB9C806B80C56A76BD77DD77942B3"
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
    /// S_AdministrarGrupo
    /// </summary>
    public partial class S_AdministrarGrupo : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 23 "..\..\..\Cliente\S-AdministrarGrupo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignThemes.Wpf.DialogHost DialogHost;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\Cliente\S-AdministrarGrupo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton themeToggle;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\..\Cliente\S-AdministrarGrupo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgGrupos;
        
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
            System.Uri resourceLocater = new System.Uri("/Proyecto Cliente;component/cliente/s-administrargrupo.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Cliente\S-AdministrarGrupo.xaml"
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
            
            #line 36 "..\..\..\Cliente\S-AdministrarGrupo.xaml"
            ((System.Windows.Shapes.Ellipse)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.minimizeWindow);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 43 "..\..\..\Cliente\S-AdministrarGrupo.xaml"
            ((System.Windows.Shapes.Ellipse)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.btnCloseWindow_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.themeToggle = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            
            #line 54 "..\..\..\Cliente\S-AdministrarGrupo.xaml"
            this.themeToggle.Click += new System.Windows.RoutedEventHandler(this.themeToggle_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.dgGrupos = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 6:
            
            #line 96 "..\..\..\Cliente\S-AdministrarGrupo.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_clicCrearClase);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 100 "..\..\..\Cliente\S-AdministrarGrupo.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_clicAsignar);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 107 "..\..\..\Cliente\S-AdministrarGrupo.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_clicCrear);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 114 "..\..\..\Cliente\S-AdministrarGrupo.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_clicEliminar);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 120 "..\..\..\Cliente\S-AdministrarGrupo.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_clicSalir);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

