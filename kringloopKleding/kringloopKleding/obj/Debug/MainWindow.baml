   M S B A M L   `   `   `  ����K  GkringloopKleding, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:kringloopKledingkringloopKleding  5   	   !    kringloopKleding.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:kringloopKleding     5   	     � WindowState$  	Maximized=�5   	    � WindowStartupLocation$ CenterScreen=�5   	    � Title$ Afhaling��5
   	   $	��800��6   $
��1400��6'   .��5      � .+�5   
   � $
��Left=�6   $��Stretch=�6*   $	��400��6F   
�������6R   � 5   
   $��Stretch=�6   $	��Top=�6-   $��40��6E   ���	#FF32BE1E.+�5      ]� $��Stretch=�6   $��Stretch=�66   .+�5      o� $	��Top=�6   
�������
���   ����	#FF32BE28.@�5      m� $��_Menu��6   $��25(�6.   
�������
���   ����	#FF32BE28.@�5      m� -   5         m�Name$ Afhaling��5   #   $��	_Afhaling��65   $��25(�6_   
����������	#FF32BE28���	#FF32BE286�   m� 5      -   5        $ klantenBeheer��5   #   $��_Klanten beheer��6:   $��25(�6o   
����������	#FF32BE28���	#FF32BE286�   m� 5      -   5        $ 
Rapportage��5   #   $��_rapportage��67   $��25(�6e   
����������	#FF32BE28���	#FF32BE286�   5      5      5      5      �� 5   
    �� Orientation$ Vertical=�6   $��Stretch=�6,   $��Center=�6J   .+�5      �� $
��Left=�6   $ Vertical=�64   .+�5      �� $ 
Horizontal=�6   $��0,10,0,0q�66   $��Center=�6H   $��Center=�6c   .+�5      �� $��Kaartnummer:��6   $��30��63   $	��130��6?   $��18(�6K   �� 5      -   5         ��Name$ txtCard��5      $��30��6/   $	��100��6;    �� 	MaxLength$ 6��6G   $��18(�6U   6c   �� 5      -   5         ��Name$ btnKaartnummerSearch��5      $�� ��6;   $
��Left=�6F   $��30��6a   $	��Top=�6m   $��50��6�   $��0,0,0,0q�6�    � Cursor$  ��6�   
���   ���5      �� 5       	 j� Stretch$	 Uniform=�6)   
 j� TileMode$

 None=�6<    �� ImageSource$ /vergrootglas.png��6L   5"      5#      �� 5$      $ 
Horizontal=�6   $��0,10,0,0q�66   $��Center=�6H   $��Center=�6e   .+�5%      �� $��Achternaam:��6   $	��130��62   $��30��6>   $��18(�6J   �� 5&      -   5        $ txtLastname��5&      $	��150��63   $��30��6?   $��Center=�6K   $��Center=�6f   $��18(�6�   5'      �� 5(      $ 
Horizontal=�6   $��0,10,0,0q�66   $��Center=�6H   $��Center=�6e   .+�5)      �� $��	Voornaam:��6   $	��130��60   $��30��6<   $��18(�6H   �� 5*      -   5        $ txtFirstname��5*      $	��150��64   $��30��6@   $��Center=�6L   $��Center=�6g   $��18(�6�   5+      �� 5,      $ 
Horizontal=�6   $��Right=�66   $��Center=�6R   $��	0,20,50,0q�6m   .+�5-      �� -   5        $ btnAfhaling��5-      $��Opslaan��62   $	��100��6D   $��30��6P   $��18(�6\   6j   5.      5/      50      �� 51   
   $ 
Horizontal=�6   $��Stretch=�6.   $��Center=�6L   $��	450,0,0,0q�6g   .+�52      &   System.Windows.Controls.DataGrid  -	   5          Name$ dgFamilymember��52      $��Center=�6/   $	��250��6L   $	��Top=�6Y   $	��400��6q   $��16(�6�   $��0,0,50,0q�6�      AutoGenerateColumns .     
IsReadOnly .    Columns 53      0  *System.Windows.Controls.DataGridTextColumn  54      ,  &System.Windows.Controls.DataGridColumn   Header$ voornaam��6)      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 6;   �� *
voornaam+6X     55      $ geboortejaar��6)    .  6?   �� *geboortejaar+6`   57        58      -
   5        $ dgPickUp��58      $��Center=�6)   $	��250��6F   $	��Top=�6S   $	��275��6k   $��16(�6x    .   .  59        5:      $ datum��6)    .  68   �� *datum+6R     5;      $ gezinslid_id��6)    .  6?   �� *gezinslid_id+6`   5=      5>      5?      5@      