   M S B A M L   `   `   `  ����  KBSystemSale     KBSystemSale.Peages.Towary   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        N� Title$  Towary��5      .��5
      �  � ColumnDefinitions 5   
   �� 5      $
��204* �6   �� 5      $
��501* �6    � RowDefinitions 5   
   �� 5      $	��100 �6   �� 5      $��* �6   .+�5   
   �� $��1��6   .+�5      �� -   5        $��label1��5   N   $��Towary��6   $��28��6%   $��Center=�61   $	��Top=�6\   $��1��6t   �� 5      -   5        $��	searchtbx��5      $	��400��6'   63   �� 5       �� Orientation$ 
Horizontal=�6   $��Center=�62   .+�5      	� -   5        $��
nazwaradio��5   B    	� 	GroupName$ searchg��6   $��Nazwa��62   6T   	� 5      -   5        $��	prodradio��5   F   $ searchg��6   $��	Producent��62   6W   	� 5      -   5        $��katradio��5   F   $ searchg��6   $��	Kategoria��62   6V   5      5      �� 5   
   -   5        $��	groupBox1��5   b   $��Menu główne��6   $	��618��6(   $
��Left=�65   $��12,8,0,0q�6P   $	��Top=�6s   $	��192��6�   $��2��6�   .��5      � $	��594��6   .+�5      �� -   5        $��buttonModyfikuj��5   f   $��	Modyfikuj��6   $��34��6-   $
��Left=�69   $��6,46,0,0q�6T   $	��Top=�6}   $	��165��6�   {�.  6�   �� 5       -   5        $��button3��5    b   $��Dodaj��6   $��34��6)   $
��Left=�65   $��6,86,0,0q�6P   $	��Top=�6q   $	��165��6�   6�   �� 5"      -	   5        $��button5��5"   h   $��Zamówienia��6   $��34��6.   $
��Left=�6:   $��	6,166,0,0q�6U   $	��Top=�6w   $	��165��6�   5#      5$      &   System.Windows.Controls.DataGrid  5%   
   -
   5        $��	dataGrid1��5%   L   $��2��6   $��1��6       AutoGenerateColumns .     CanUserAddRows .     CanUserDeleteRows .     
IsReadOnly . 	   Columns	 5&      0  *System.Windows.Controls.DataGridTextColumn  5'      ,  &System.Windows.Controls.DataGridColumn
   Header$
 Nazwa��6?   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   �� *model+6?     5(      $
 	Producent��6Z    6%   ��  �� Path$! producent.kontrahenci.Nazwa�6Z     5)      $
 Ilość na magazynie��6I    6%   �� *stan_magazynowy+6I     5*      $

 Cena��6I    6%   �� *cena_katalogowa+6I      RowDetailsTemplate 5,      �� 5-      .R�5.      �  5/      �� 50      �� 51       53      �� 54      �� 55      �� 56      �� 57      �� 58      �� 59      �� 5:      �� 5;      .+�5=      �� 
Producent:60   �� 5>      $��1��6$   Model:69   �� 5?      $��2��6$   Cena:68   �� 5@      $��3��6$   Stan magazynowy:6C   �� 5A      $��4��6$   
Kategoria:6=   �� 5B      $��5��6$   PN:66   �� 5C      $��6��6$   
Aktywny:6;   �� 5D      $��7��6$   opis:68   �� 5F      $��1��6$   ��64   �� $! producent.kontrahenci.Nazwa��� 5G      $��1��6$   $��1��64   ��6A   �� *model+�� 5H      $��1��6$   $��2��64   ��6A   �� *cena_katalogowa+�� 5I      $��1��6$   $��3��64   ��6A   �� *stan_magazynowy+�� 5J      $��1��6$   $��4��64   ��6A   �� $ kategoria.nazwa��� 5K      $��1��6$   $��5��64   ��6A   �� *PN+�� 5L      $��1��6#   $��6��63   {�.   d� 	IsChecked 6@   �� *	aktywne+6^   �� 5M      $��1��6$   $��7��64   ��6A   �� *opis+5N      5O      5Q      5R      5S      