   M S B A M L   `   `   `  ���� I  EGatewayBrowser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU/clr-namespace:GatewayBrowser.UserControls.IPBox!GatewayBrowser.UserControls.IPBox  9!clr-namespace:GatewayBrowser.HelpGatewayBrowser.Help  *    $GatewayBrowser.Views.SetIPConfigView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     AIPBox/clr-namespace:GatewayBrowser.UserControls.IPBox     0uc!clr-namespace:GatewayBrowser.Help     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006       � WindowStartupLocation$  CenterOwner=� � 
ResizeMode$ NoResize=� � Title$ IP Configuration��   GatewayImageSource � Icon# [  $	��450��$	��398��.��]� $��5q�.+��� $��Bottom=�  buttonPanel#��[ .+��� -   $��OK���� -   $��Close��� -    �Name$ 	grd_Port1��$	��Top=�.+���   lightBlueBrush#��[ ����   
greenBrush#��[   groupBoxHeader#��[ ._��� $��Port1��.���  � ColumnDefinitions �� $	��110 ��� $	��150 ��� $
��Auto � � RowDefinitions �� $
��Auto ��� $��30 ��� $��30 ��� $
��Auto ��� $
��Auto �.+��� $��Name:��$	��100��$��28��$	��Top=��� -    ��Name$ lbl_IfcName_p1��$��ifcname��$��1��$	��150��$��28��$	��Top=�$
��Left=��� $��1��$��IP Address:��$	��100��$��28��$	��Top=�8   2GatewayBrowser.UserControls.IPBox.IPTextBoxControl  -     Name$ txt_IPAddress_p1��$��1��$��1��$��2��$
��Left=�$	��Top=��� $��2��$��Network Mask:��$	��100��  -   $ txt_NetworkMask_p1��$��2��$��1��$
��Left=�$	��Top=��� $��3��$��MAC Address:��$	��100��$��28��$	��Top=��� -   $ 
txt_MAC_p1��$��00000000��$��3��$��1��$	��150��$��28��$	��Top=�$
��Left=�� -   $ 	grd_Port2��.+��� #��[ ���� #��[ #��[ ._��� $��Port2��.���  �� $	��110 ��� $	��150 ��� $
��Auto � �� $
��Auto ��� $��30 ��� $��30 ��� $
��Auto ��� $
��Auto �.+��� $��Name:��$	��100��$��28��$	��Top=��� -	   $ lbl_IfcName_p2��$��ifcname��$��1��$	��150��$��28��$	��Top=�$
��Left=��� $��1��$��IP Address:��$	��100��$��28��$	��Top=�  -
   $ txt_IPAddress_p2��$��1��$��1��$��2��$
��Left=�$	��Top=��� $��2��$��Network Mask:��$	��100��  -   $ txt_NetworkMask_p2��$��2��$��1��$
��Left=�$	��Top=��� $��3��$��MAC Address:��$	��100��$��28��$	��Top=��� -   $ 
txt_MAC_p2��$��00000000��$��3��$��1��$	��150��$��28��$	��Top=�$
��Left=�