   M S B A M L   `   `   `  ????K  GProyecto Cliente, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:Proyecto_Cliente.ClienteProyecto_Cliente.Cliente  5   	   -    'Proyecto_Cliente.Cliente.S_AgregarGrupo   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:Proyecto_Cliente.Cliente     5   	     ? Title$  S_AgregarGrupo??5   	   $	??450??6    $	??800??6-   .??5	      ? $	??450??6   $	??800??6   .+?5   
   ?? $??25(?6   $??310,10,310,396q?6   .??66   Agregar Grupo6E   ?? 5   
   $??20(?6   $??80,185,654,230q?6   .??66   Area:6=   ?? 5   
   $??20(?6   $??80,69,641,346q?6   .??65   	Bloque:6>   ?? 5   
   $??20(?6   $??80,69,641,346q?6   .??65   	Bloque:6>   ?? 5   
   -   5         ??Name$ tbBloque??5      $??175,69,245,346q?6$   6>   ?? 5   
   -   5        $??615,73,58,345q?5      .??6D   	Guardar6M   ?? 5   
   -   5        
???   ?
???????$??615,340,58,78q?5   T   .??6k   Salir6r   &   System.Windows.Controls.DataGrid  5   
   -   5        $??dgArea??5      $??0??6!      AutoGenerateColumns .     
IsReadOnly . $??80,225,245,65q?6_      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ * 5   !      Header$ Nombre??5   !   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5   !   ?? *nombre+5   !     5      $ * 5   !   $ Semestre??5   !    5   !   ?? *
semestre+5   !   5"      5#      5$      