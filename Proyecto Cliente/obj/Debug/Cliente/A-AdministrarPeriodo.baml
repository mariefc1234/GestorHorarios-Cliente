   M S B A M L   `   `   `  ????K  GProyecto Cliente, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:Proyecto_Cliente.ClienteProyecto_Cliente.Cliente  5      3    -Proyecto_Cliente.Cliente.A_AdministrarPeriodo   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      8local&clr-namespace:Proyecto_Cliente.Cliente     5      _ [MaterialDesignThemes.Wpf, Version=4.6.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5        ? Title$  A_AdministrarPeriodo??5	      $	??800??5
      $	??750??5       ? AllowsTransparency .    MaterialDesignBody#???   5       ? WindowStartupLocation$ CenterScreen=?5       ? WindowStyle$
 None=?5      ??5      Z? .??5      #  MaterialDesignThemes.Wpf.Card  $??25q?5   	   .  (MaterialDesignThemes.Wpf.ElevationAssist   	Elevation$	 Dp4=?5   	     MaterialDesignPaper#???  5   	      UniformCornerRadius$ 15Q?5   	   .??5   
   )  #MaterialDesignThemes.Wpf.DialogHost  -   5          Name$ 
DialogHost??5   $      CloseOnClickAway . .??5      ?? .+?5      ?? $??30??5      ???	#FFA4CAFF ?? CornerRadius$ 	15,15,0,0??5      ._?5      ?? $??Right=?6!   	 ?? Orientation$	 
Horizontal=?6=   .+?5      A? -   5        $??20??5      $??20??5       $??0,0,5,0q?5!      $??Center=?5"      ???#FF8E265$      A? 5%      -   5        $??20??5&      $??20??5'      $??0,0,10,0q?5(      $??Center=?5)      
???  ??5+      5,      5-      ?? 5.      $??	16,10,0,6q?5/      $??Right=?50      $	 
Horizontal=?51      .+?52      ?? $??Center=?6    $??	Dark Mode??6;   d? 53      -   5        
 d?Name$
 themeToggle??54      $??12,0,8,0q?55       ? Cursor$
 Hand??57       ? ToolTip$ Enable Dark Mode??59       d? 	IsChecked 58      ?? *IsDarkTheme+59      5:      ?? 5;      $??0,0,0,20q?5<      $??Center=?5=      $??28(?5>      $
??Bold"?5?      $??Administrar Periodo??5@      ?? 5A      $??	0,10,15,0q?5B      $??Right=?5C      $	 
Horizontal=?5D      .+?5E      ?? -   5        $	??100??5F      $??0,0,15,0q?5G      .??5H   7   	Modificar5J      ?? 5K      -   5        $	??100??5L      $??0,0,15,0q?5M      
???????.??5O   3   Crear5Q      ?? 5R      -   5        $	??100??5S      
???????.??5U   4   
Eliminar5W      5X      ?? 5Y      $	??380??6   $??
15,15,15,0q?6*   .+?5Z      &   System.Windows.Controls.DataGrid  -   5        $??
dgPeriodos??5[         AutoGenerateColumns .     
IsReadOnly .    Columns 5^      0  *System.Windows.Controls.DataGridTextColumn  5_      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ * 5`   !      Header$ Fecha Inicio??5b   !   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5a   !   ?? *fechaInicio+5b   !     5c      $ * 5d   !   $ Fecha Ordinario??5f   !    5e   !   ?? *fechaOrdinario+5f   !     5g      $ * 5h   !   $ Fecha Extraordinario??5j   !    5i   !   ?? *
fechaExtra+5j   !     5k      $ * 5l   !   $ 	Fecha Fin??5n   !    5m   !   ?? *
fechaFin+5n   !     5o      $ * 5p   !   $ Activo??5r   !    5q   !   ?? *	activo2+5r   !   5t      5u      ?? 5v      $??	0,10,15,0q?5w      $??Right=?5x      $	 
Horizontal=?5y      .+?5z      ?? -	   5        $	??150??5{      
???? ??.??5}   5   	Activar5      5?      ?? 5?      $??	0,50,15,0q?5?      $??Right=?5?      $	 
Horizontal=?5?      .+?5?      ?? -
   5        $	??150??5?      .??6F   
Regresar5?      5?      5?      5?      5?      5?      