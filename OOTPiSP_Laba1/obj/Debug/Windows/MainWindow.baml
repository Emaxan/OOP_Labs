   M S B A M L   `   `   `  ����H  DOOTPiSP_Laba1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:OOTPiSP_Laba1.PropertiesOOTPiSP_Laba1.Properties  5      =#clr-namespace:OOTPiSP_Laba1.WindowsOOTPiSP_Laba1.Windows  5      &     OOTPiSP_Laba1.Windows.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      =
properties&clr-namespace:OOTPiSP_Laba1.Properties     5      7windows#clr-namespace:OOTPiSP_Laba1.Windows     5      -   5          �Name$  WMain��5   	    � Title$ 	FigureMax��5	      $	��768��6   $
��1366��6"    � WindowStartupLocation$ CenterScreen=�6/    � WindowState$ Normal=�5
      $	��300��5   9   $	��500��6I   .��5      � .+�5      �� $��Stretch=�6   $��0,50,200,30q�6)   $��Stretch=�6>   $��0q�6Z   
���   ���5      �� '   !OOTPiSP_Laba1.Properties.Settings   Default �� Source# Z  �� Path$ 
BgMidColor� �� Mode$ OneWay=� �� UpdateSourceTrigger$ PropertyChanged=�._�5      �� -   5        	 ��Name$	 CMain��5      5      �� 5      $��Stretch=�6�   $��50��6�   $	��Top=�5      ��5      �� # Z $ BgDarkColor�$ OneWay=�$ PropertyChanged=���6�   �� # Z $ BgDarkColor�$ OneWay=�$ PropertyChanged=�5      �� 5      $��Stretch=�6�   $��30��6�   $��Bottom=�5      ��5      �� # Z $ BgDarkColor�$ OneWay=�$ PropertyChanged=���6�   �� # Z $ BgDarkColor�$ OneWay=�$ PropertyChanged=�5      �� 5      $��Right=�6�   $��Stretch=�62  $	��200��5      $��	0,50,0,30q�6   ��5      �� # Z $ BgDarkColor�$ OneWay=�$ PropertyChanged=���6�   �� # Z $ BgDarkColor�$ OneWay=�$ PropertyChanged=�5      �� 5      -   5        
 ��Name$
 	LbObjects��5      $��Right=�6   $��Stretch=�6;   $	��200��6W   $��0q�6c   $��
0,50,0,120q�5       �� SelectionMode$ Extended=�6I   ��6b   �� # Z $ BgLightColor�$ OneWay=�$ PropertyChanged=���5      �� 5       �� 
TargetType" ��6   .�5      ��  �� Property � {� �� Value 6#   �� *IsSelectable+$ OneWay=�$ PropertyChanged=��� 5       �@��
IsSelected 6$   �� *IsSelectedProp+$ OneWay=�$ PropertyChanged=� �� Triggers 5      P� 5       P� Property � {� P� Value .  . �5   	   ��  � ��
 �   �6&   �� 5    	    � ��
 �����6&   5!   	   5#      5%      � 5&      $��Right=�6   $��90��6*   $��Bottom=�66   $	��200��6Q   $��0,0,0,30q�6]   
������ .+�5'      �� -   5         ��Name$ 
BSelectAll��5'      $��
Select all��6    $	��100��65      ButtonStyle1#���   6A   $��windows:MainWindow.Select��6h   �� 5(      $��Unselect all��6   $	��100��6#   #���   6/   $!��windows:MainWindow.Unselect��6V   �� 5)      $��Delete��6   $	��100��6   #���   6)   $��windows:MainWindow.Delete��6P   �� 5*      $
��Edit��6   $	��100��6   #���   6'   $!��windows:MainWindow.EditElem��6N   �� 5+      $
��Save��6   $	��100��6   #���   6'   $��windows:MainWindow.Save��6N   �� 5,      $
��Load��6   $	��100��6   #���   6'   $��windows:MainWindow.Open��6N   5-      o� 5.      $��Stretch=�6	   $��30��6'   $��Bottom=�63   
������ |�.   o� 
IsMainMenu .  .@�5/      �� -   5         ��Name$ LMouseCoords��5/      $�� ��6!   |�.  6,   50      o� 51      $��Stretch=�6	   $��20��6'   $	��Top=�63   
������   MyMenuStyle#���  6d   .@�52      m� 
�������$
��File��6!    � Cursor$ Arrow��6/     MyMenuItemControlTemplate#���  6>   .@�53      m� -   5         m�Name$ MenOpen��53      $��Open...��6      MenuItemControlTemplate1#���  61    m� Icon 54      �� 55      ��56   	   �� 57   
    �� 	UriSource$8 2/OOTPiSP_Laba1;component/Properties/Img/folder.png?�6   59   	   5;      m� 5<      -   5        $ MenSave��5<      $��Save...��6    #���  61    5=      �� 5>      ��5?   	   �� 5@   
   $6 0/OOTPiSP_Laba1;component/Properties/Img/Save.png?�6   5B   	   5D      �� 5E      ���	#FF5C5C5C���	#FF5C5C5C$��	-25,2,5,2q�6?   m� 5F      -   5        $ MenExit��5F      $
��Exit��6    #���  6.    5G      �� 5H      ��5I   	   �� 5J   
   $6 0/OOTPiSP_Laba1;component/Properties/Img/exit.png?�6   5L   	   5N      5O      m� 5P      
�������$��Select��6!   $ Arrow��61   #���  6@   .@�5Q      m� -	   5        $ 	MenSelect��5Q      $��
Select all��6"   #���  66    5R      �� 5S      ��5T   	   �� 5U   
   $8 2/OOTPiSP_Laba1;component/Properties/Img/select.png?�6   5W   	   5Y      m� 5Z      -
   5        $ MenUnselect��5Z      $��Unselect all��6$   #���  6:    5[      �� 5\      ��5]   	   �� 5^   
   $: 4/OOTPiSP_Laba1;component/Properties/Img/unselect.png?�6   5`   	   5b      �� 5c      ���	#FF5C5C5C���	#FF5C5C5C$��	-25,2,5,2q�6?   �� 5d      $��Selection mode��6   |�.  $	��100��67   $��	0,0,-40,0q�6C   	� 5e      -   5         	�Name$ MenSingleselect��5e      $��Single��6+   |�.  $	��100��6N   $��	0,0,-40,0q�6Z   $��20��6m   6�   	� 5f      -   5        $ MenMultiselect��5f      $��Multi��6*   |�.  $	��100��6L   $��	0,0,-40,0q�6X   $��20��6k   6�   	� 5g      -   5        $ MenExtendedselect��5g      $��Extended��6-   |�.  $	��100��6R   $��	0,0,-40,0q�6^   $��20��6q   6�   5h      m� 5i      
�������$��Elements��6!   $ Arrow��63   #���  6B   .@�5j      m� -   5        $ MenDeleteSel��5j      $��Delete selected��6%   #���  6>    5k      �� 5l      ��5m   	   �� 5n   
   $8 2/OOTPiSP_Laba1;component/Properties/Img/delete.png?�6   5p   	   5r      m� 5s      -   5        $ MenEdit��5s      $
��Edit��6    #���  6.    5t      �� 5u      ��5v   	   �� 5w   
   $6 0/OOTPiSP_Laba1;component/Properties/Img/draw.png?�6   5y   	   5{      5|      5}      ]� 5~      -   5         ]�Name$ DpTop��5~      $��Stretch=�6   ��.  $��0,20,0,0q�6Q   $	��Top=�6c   5�      5�      5�      