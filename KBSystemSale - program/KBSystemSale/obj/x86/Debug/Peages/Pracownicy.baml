   M S B A M L   `   `   `  ����  KBSystemSale$    KBSystemSale.Peages.Pracownicy   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        N� Title$  
Pracownicy��5      .��5
      � .+�5   
   	� -   5        $��radioButton1��5   �    	� 	GroupName$ GrupaWyszukiwania��6   $��Id pracownika��64   $��16��6L   $
��Left=�6X   $��
241,63,0,0q�6s   $	��Top=�6�   6�   	� 5   
   -   5        $��radioButton2��5   ~   $ GrupaWyszukiwania��6   $
��Imie��64   $��16��6C   $
��Left=�6O   $��
361,63,0,0q�6j   $	��Top=�6�   6�   	� 5   
   -   5        $��radioButton3��5   �   $ GrupaWyszukiwania��6   $��Nazwisko��64   $��16��6G   $
��Left=�6S   $��
430,63,0,0q�6n   $	��Top=�6�   6�   	� 5   
   -   5        $��radioButton4��5   f   $ GrupaWyszukiwania��6   $��Pesel��64   $��16��6D   $��526,63,337,0q�6P   $	��Top=�6z   6�   	� 5   
   -   5        $��radioButton5��5   �   $ GrupaWyszukiwania��6   $��Pracuje��64   $��16��6F   $��Right=�6R   $��
0,63,258,0q�6n   $	��Top=�6�   6�   	� 5   
   -   5        $��radioButton6��5   �   $ GrupaWyszukiwania��6   $��Nie pracuje��64   $��16��6J   $��Right=�6V   $��
0,63,176,0q�6r   $	��Top=�6�   6�   �� 5   
   -   5        $��label2��5   `   $��
Wyszukaj :��6   $��28��6%   $
��Left=�61   $��
280,12,0,0q�6L   $	��Top=�6n   �� 5   
   -   5        $��poleWyszukiwania��5   M   $��23��6   $
��Left=�6   $��
362,14,0,0q�69   $	��Top=�6e   $	��120��6}   6�   �� 5   
   -	   5        $��
labelWynik��5   [   $��wynik��6   $��28��6    $
��Left=�6,   $��
784,14,0,0q�6G   $	��Top=�6m    �� Target 6�   �� �� 5   
   -
   5        $��	groupBox1��5   b   $��Menu główne��6   $	��618��6(   $
��Left=�65   $��12,8,0,0q�6P   $	��Top=�6s   $	��192��6�   .��5      � $	��594��6   .+�5      �� -   5        $��buttonModyfikuj��5   f   $��	Modyfikuj��6   $��34��6-   $
��Left=�69   $��6,46,0,0q�6T   $	��Top=�6}   $	��165��6�   {�.  6�   �� 5      -   5        $��button3��5   b   $��Dodaj��6   $��34��6)   $
��Left=�65   $��6,86,0,0q�6P   $	��Top=�6q   $	��165��6�   6�   �� 5      -   5        $��button4��5   g   $��Szczegóły��6   $��34��6-   $
��Left=�69   $��	6,126,0,0q�6T   $	��Top=�6v   $	��165��6�   {�.  6�   �� 5      -   5        $��button5��5   h   $��Zamówienia��6   $��34��6.   $
��Left=�6:   $��	6,166,0,0q�6U   $	��Top=�6w   $	��165��6�   {�.  6�   5      5      &   System.Windows.Controls.DataGrid  5   
   -   5        $��	dataGrid1��5   �      CanUserAddRows .     CanUserDeleteRows .     
IsReadOnly .    AutoGenerateColumns .  $	��508��6r   $
��Left=�6   $��221,111,0,0q�6�   $	��Top=�6�   $	��638��6�      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Id��6G   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 6%   �� *id_pracownika+6G     5       $ Imię��6>   	 6%   �� *imie+6>     5!      $ Nazwisko��6B   	 6%   �� *
nazwisko+6B     5"      $ PESEL��6?   	 6%   �� *pesel+6?     5#      $ Status��6A   	 6%   �� *	pracuje+6A   5%      5&      5'      