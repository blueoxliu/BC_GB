   M S B A M L   `   `   `  ����I  EGatewayBrowser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU/clr-namespace:GatewayBrowser.UserControls.IPBox!GatewayBrowser.UserControls.IPBox  5      9!clr-namespace:GatewayBrowser.HelpGatewayBrowser.Help  5      *    $GatewayBrowser.Views.SetIPConfigView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      AIPBox/clr-namespace:GatewayBrowser.UserControls.IPBox     5      0uc!clr-namespace:GatewayBrowser.Help     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6H     � WindowStartupLocation$  CenterOwner=�5       � 
ResizeMode$ NoResize=�5       � Title$ IP Configuration��5	         GatewayImageSource � Icon# [  5
      $	��450��5      $	��398��6   .��5      ]� $��5q�6   .+�5   
   �� $��Bottom=�6     buttonPanel#��[ 6-   .+�5      �� -   5        $��OK��5      6"   �� 5      -   5        $��Close��5      6%   5      � 5   
   -   5         �Name$ 	grd_Port1��5   $   $	��Top=�6   .+�5      ��   lightBlueBrush#��[ 6   ��5      �� 5        
greenBrush#��[ 6     groupBoxHeader#��[ 5      ._�5      �� $��Port1��6$   5      .��5      �  � ColumnDefinitions 5      �� 5      $	��110 �6+   �� 5      $	��150 �6+   �� 5      $
��Auto �6+    � RowDefinitions 5!      �� 5"      $
��Auto �6(   �� 5#      $��30 �6(   �� 5$      $��30 �6(   �� 5%      $
��Auto �6(   �� 5&      $
��Auto �6(   .+�5(      �� $��Name:��6   $	��100��6,   $��28��68   $	��Top=�6D   �� 5)      -   5         ��Name$ lbl_IfcName_p1��5)   .   $��ifcname��6   $��1��6F   $	��150��6V   $��28��6b   $	��Top=�6n   $
��Left=�6�   �� 5+      $��1��6   $��IP Address:��6)   $	��100��6?   $��28��6K   $	��Top=�6W   8   2GatewayBrowser.UserControls.IPBox.IPTextBoxControl  5,      -   5          Name$ txt_IPAddress_p1��5,   .   $��1��6H   $��1��6U   $��2��6e   $
��Left=�6y   $	��Top=�6�   �� 5.      $��2��6   $��Network Mask:��6)   $	��100��6A     5/      -   5        $ txt_NetworkMask_p1��5/   .   $��2��6J   $��1��6W   $
��Left=�6g   $	��Top=�6�   �� 51      $��3��6   $��MAC Address:��6)   $	��100��6@   $��28��6L   $	��Top=�6X   �� 52      -   5        $ 
txt_MAC_p1��52   /   $��00000000��6   $��3��6C   $��1��6P   $	��150��6`   $��28��6l   $	��Top=�6x   $
��Left=�6�   53      54      55      � 56   
   -   5        $ 	grd_Port2��56      .+�57      �� #��[ 6   ��58      �� 59      #��[ 6   #��[ 5:      ._�5;      �� $��Port2��6$   5<      .��5?      �  5@      �� 5A      $	��110 �6+   �� 5B      $	��150 �6+   �� 5C      $
��Auto �6+    5E      �� 5F      $
��Auto �6(   �� 5G      $��30 �6(   �� 5H      $��30 �6(   �� 5I      $
��Auto �6(   �� 5J      $
��Auto �6(   .+�5L      �� $��Name:��6   $	��100��6,   $��28��68   $	��Top=�6D   �� 5M      -	   5        $ lbl_IfcName_p2��5M   .   $��ifcname��6   $��1��6F   $	��150��6V   $��28��6b   $	��Top=�6n   $
��Left=�6�   �� 5O      $��1��6   $��IP Address:��6)   $	��100��6?   $��28��6K   $	��Top=�6W     5P      -
   5        $ txt_IPAddress_p2��5P   .   $��1��6H   $��1��6U   $��2��6e   $
��Left=�6y   $	��Top=�6�   �� 5R      $��2��6   $��Network Mask:��6)   $	��100��6A     5S      -   5        $ txt_NetworkMask_p2��5S   .   $��2��6J   $��1��6W   $
��Left=�6g   $	��Top=�6�   �� 5U      $��3��6   $��MAC Address:��6)   $	��100��6@   $��28��6L   $	��Top=�6X   �� 5V      -   5        $ 
txt_MAC_p2��5V   /   $��00000000��6   $��3��6C   $��1��6P   $	��150��6`   $��28��6l   $	��Top=�6x   $
��Left=�6�   5W      5X      5Y      5Z      5[      