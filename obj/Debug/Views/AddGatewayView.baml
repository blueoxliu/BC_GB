   M S B A M L   `   `   `  ����I  EGatewayBrowser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:GatewayBrowser.HelpGatewayBrowser.Help  5      )    #GatewayBrowser.Views.AddGatewayView   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      0uc!clr-namespace:GatewayBrowser.Help     5        � Title$  Add Gateway��5       � WindowStartupLocation$ CenterOwner=�5       � 
ResizeMode$ NoResize=�5         GatewayImageSource � Icon# [  5      $	��450��5	      $	��218��6   .��5
      ]� $��5q�6   .+�5   
   �� $��Bottom=�6     buttonPanel#��[ 6-   .+�5      �� -   5        $
��Save��5      6$   �� 5      -   5        $��Close��5      6%   5      � 5   
   .+�5      ��   lightBlueBrush#��[ 6   ��5      �� 5        
greenBrush#��[ 6     groupBoxHeader#��[ 5      ._�5      �� $��Add Offline Gateway��6$   5      .��5      �  � ColumnDefinitions 5      �� 5      $
��Auto �6+   �� 5      $	��150 �6+   �� 5      $
��Auto �6+   $��89��68   �� 5      $��78 �6+    � RowDefinitions 5       �� 5!      $
��Auto �6(   �� 5"      $
��Auto �6(   �� 5#      $
��Auto �6(   �� 5$      $
��Auto �6(   �� 5%      $	��19* �6(   �� 5&      $	��46* �6(   .+�5)      �� $��Device Type:��6   $��81��63   �� 5*      -   5         ��Name$ cmb_DeviceType��5*      $��1��6d   $��2��6t   ��67   -   'GatewayBrowser.Help.EnumValuesExtension  *uc:DeviceTypes+6t   �� 5+      $��1��6   $��Description:��6)   $��0,1,0,36q�6n   $��4��6�   $��78��6�    �� Target 6@   ��  �� ElementName$ txt_MACAddress��6�   �� 5,      -   5        	 ��Name$	 txt_Description��5,      $��60��67   $	��232��6C   $��1��6O   $��1��6\   $��2��6l   
 �� TextWrapping$

 Wrap=�6�    �� AcceptsReturn . 6�   5-      5.      5/      50      51      