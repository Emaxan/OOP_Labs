   M S B A M L   `   `   `  ����H  DOOTPiSP_Laba1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:OOTPiSP_Laba1.WindowsOOTPiSP_Laba1.Windows  5      &     OOTPiSP_Laba1.Windows.EditWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      5local#clr-namespace:OOTPiSP_Laba1.Windows     5      -   5          � 
ResizeMode$  NoResize=�5       � WindowStyle$
 None=�6*    � AllowsTransparency . 
������  � WindowStartupLocation$ CenterOwner=�5       � SizeToContent$ Height=�6'    � Title$
 Edit��5	      $	��700��6   $	��350��6    .��5
      � .+�5      ��  �� CornerRadius$ 50��6   $��1q�6   
���   � �� Padding$
 0,10q�6E   ���	#FF555555._�5      �  � RowDefinitions 5      �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5      �� -   5        $��0��5      $��30��6   $ 0q�6&   ._�5      � .+�5      �� -   5        	 ��Name$	 TopText��5      $��Stretch=�6   $��Center=�6=   $
��Edit��6a   $��	0,-10,0,0q�5   	   $��25(�6   �� 5      -   5        $	 BClose��5      $��Right=�6   $��
0,-10,30,0q�6:   $��0q�6N   $��30(�6Z   $��×��6h   |�. 5   >   5      5      � 5      -   5        
 �Name$
 GLinks��5      $��1��6    � 5      -   5          �Name$ FMain��5      $��2��6     � NavigationUIVisibility$ Hidden=�6(   $��	0,10,0,10q�6H   ��.  6[   �� 5      -   5        $��3��5      $
��Left=�6   $��70��65   $��30��6@   $��1q�6L   
���   �$ 50��5      $��	50,10,0,0q�6    � Cursor$
 Hand��5       ._�5!      �� -   5        $��Center=�5!      $��20(�61   $��-1q�6?   $��1313 MockingbiRd Lane)�6K   .��5"   9   OK5$      5%      �� 5&      -	   5        $��3��5&      $��Right=�6   $��70��66   $��30��6A   $��1q�6M   
���   �$ 50��5'      $��	0,10,50,0q�6   $
 Hand��5(      ._�5)      �� -
   5        $��Center=�5)      $��20(�61   $��-1q�6?   $��1313 MockingbiRd Lane)�6K   .��5*   =   Cancel5,      5-      5.      5/      50      51      