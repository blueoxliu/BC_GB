   M S B A M L   `   `   `  ���� I  EGatewayBrowser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:GatewayBrowser.HelpGatewayBrowser.Help  )    #GatewayBrowser.Views.AddGatewayView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     0uc!clr-namespace:GatewayBrowser.Help       � Title$  Add Gateway�� � WindowStartupLocation$ CenterOwner=� � 
ResizeMode$ NoResize=�   GatewayImageSource � Icon# [  $	��450��$	��218��.��]� $��5q�.+��� $��Bottom=�  buttonPanel#��[ .+��� -   $
��Save���� -   $��Close��� .+���   lightBlueBrush#��[ ����   
greenBrush#��[   groupBoxHeader#��[ ._��� $��Add Offline Gateway��.���  � ColumnDefinitions �� $
��Auto ��� $	��150 ��� $
��Auto �$��89���� $��78 � � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $	��19* ��� $	��46* �.+��� $��Device Type:��$��81���� -    ��Name$ cmb_DeviceType��$��1��$��2����-   'GatewayBrowser.Help.EnumValuesExtension  *uc:DeviceTypes+�� $��1��$��Description:��$��0,1,0,36q�$��4��$��78�� �� Target ��  �� ElementName$ txt_MACAddress���� -   	 ��Name$	 txt_Description��$��60��$	��232��$��1��$��1��$��2��
 �� TextWrapping$

 Wrap=� �� AcceptsReturn . 