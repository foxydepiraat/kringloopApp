   M S B A M L   `   `   `  ����K  GkringloopKleding, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:kringloopKledingkringloopKleding  5   	   "    kringloopKleding.wRapportage   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:kringloopKleding     5   	     � WindowState$  	Maximized=�5   	    � WindowStartupLocation$ CenterScreen=�5   	    � Title$ 
Rapportage��5
   	   $	��800��6   $
��1400��6)   .��5      � .+�5   
   � $
��Left=�6   
�������$	��400��6B   .+�5      ��  �� Orientation$ Vertical=�6   $��Center=�60   $��Center=�6M   .+�5      �� $ 
Horizontal=�6   .+�5      �� $��woonplaats: ��6   $��30��63   $	��120��6?   $��16(�6K   �� 5      -   5         ��Name$ txtResidence��5      $	��150��65   $��30��6A   $��16(�6M   ��6[   �� *
woonplaats+5      �� 5      $ 
Horizontal=�6   $��0,10,0,0q�66   .+�5      �� $��reden:��6   $��30��6/   $	��120��6;   $��16(�6H   �� 5      -   5        $ 	txtReason��5      $��30��62   $	��150��6>   $��16(�6J   ��6X   �� *reden+5      �� 5      $ 
Horizontal=�6   $��Stretch=�66   $��Center=�6T   $��0,0,0,20q�6o   .+�5      �� $��Kies een Datum: ��6   $	��120��67   $��30��6C   $��16(�6O   (  "System.Windows.Controls.DatePicker  5      -   5          Name$ dpRapportDatum��5   !   $	��150��69   $��30��6E   $��16(�6Q   6_   5      �� 5      $ 
Horizontal=�6   $��0,10,0,0q�66   .+�5      �� $��Kaart actief:��6   $��30��64   $	��125��6@   $��16(�6L   �� 5      -   5         ��Name$ cbActiveCard��5      $��Center=�65   $��Center=�6P    J� RenderTransformOrigin$ 0.5,0.5&�6m   z�5      V� 5      .~�5   "   ��  �� ScaleX$	 1.5Q�61   	 �� ScaleY$		 1.5Q�6>   �� 5    "   �� 5!   "   T� 5"   "   5#      5%      5&      �� 5'      $ 
Horizontal=�6   $��0,10,0,0q�66   .+�5(      �� $��geizinslid actief:��6   $��30��69   $	��125��6E   $��16(�6Q   �� 5)      -   5        $ cbActiveFamilyMember��5)      $��Center=�6=   $��Center=�6X   $ 0.5,0.5&�6u   z�5*      V� 5+      .~�5,   "   �� $	 1.5Q�61   $		 1.5Q�6>   �� 5-   "   �� 5.   "   T� 5/   "   50      52      53      �� 54      $��Center=�6   $��Center=�68   $ 
Horizontal=�6V   .+�55      �� -   5        
 ��Name$
 btnMonth��55      $��maand sorteren��6/   $��16(�6_   $��30��6n   $	��125��6z   $��0,0,10,0q�6�   �� 56      -   5        $
 btnYear��56      $��jaar sorteren��6.   $��16(�6\   $��30��6j   $	��125��6v   57      58      59      � 5:   
   $��Stretch=�6   $	��Top=�6-   $��40��6E   ���	#FF32BE1E.+�5;      ]� $��Stretch=�6   $��Stretch=�66   .+�5<      o� $	��Top=�6   
����������	#FF32BE28���	#FF32BE28.@�5=      m� $��_Menu��6   $��25(�6.   
����������	#FF32BE28���	#FF32BE28.@�5>      m� -   5         m�Name$ Afhaling��5>   #   $��	_Afhaling��65   $��25(�6_   
����������	#FF32BE28���	#FF32BE286�   m� 5?      -	   5        $ klantenBeheer��5?   #   $��_Klanten beheer��6:   $��25(�6o   
����������	#FF32BE28���	#FF32BE286�   m� 5@      -
   5        $ 
Rapportage��5@   #   $��_rapportage��67   $��25(�6e   
����������	#FF32BE28���	#FF32BE286�   5A      5B      5C      5E      �� 5F   
   $ Vertical=�6   $��	400,0,0,0q�6-   $��Center=�6@   $��Center=�6]   .+�5G      �� $ Vertical=�6   $��0,0,0,10q�60   $
��Left=�6B   .+�5H      �� $��	gezinnen:��6   $��30��6,   $��16(�68   $��0,0,0,10q�6F   &   System.Windows.Controls.DataGrid  5I      -   5          Name$ dgFamily��5I      $	��190��6.   $	��Top=�6;   $��16(�6T      AutoGenerateColumns .  $	��650��6      Columns 5J      0  *System.Windows.Controls.DataGridTextColumn  5K      ,  &System.Windows.Controls.DataGridColumn   Header$ Kringloopkaartnummer��61      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 6O   �� *kringloopKaartnummer+6x     5L      $ 
woonplaats��61    .  6E   �� *
Woonplaats+6d     5M      $ reden��61    .  6@   �� *reden+6Z     5N      $ is kaart actief��61    .  6J   �� *actief+6e   5P      5Q      �� 5R      $ 
Horizontal=�6   .+�5S      �� $ Vertical=�6   $��0,0,0,10q�60   $
��Left=�6B   .+�5T      �� $��gezinsleden:��6   $��30��60   $��16(�6<   $��0,0,0,10q�6J     5U      -   5        $ dgFamilyMembers��5U      $	��190��64   $	��Top=�6A   $��16(�6Y    .  $	��400��6�    5V        5W      $ voornaam��6-    .  6?   �� *
voornaam+6\     5X      $ geboortejaar��6-    .  6C   �� *geboortejaar+6d     5Y      $ is gezinslid actief��6-    .  6J   �� *actief+6e   5[      5\      �� 5]      $ Vertical=�6   $��
10,0,30,10q�60   $
��Left=�6D   .+�5^      �� $��Datums Afhalingen:��6   $��Stretch=�65   $��30��6Q   $��16(�6]   $��0,0,0,10q�6k     5_      -   5        $ 
dgAfhaling��5_      $	��190��6/   $	��250��6<   $��16(�6H    .   5`        5a      $ datum��6-    6<   �� *datum+  5b      $ gezinslid_id��6-    6C   �� *gezinslid_id+5d      5e      5f      5g      5h      5i      