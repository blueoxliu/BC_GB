   M S B A M L   `   `   `  ����R  NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R8clr-namespace:System.ComponentModel;assembly=WindowsBaseSystem.ComponentModel  5       GatewayBrowser)   #GatewayBrowser.UserControls.SideBar   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml      5      Gcm8clr-namespace:System.ComponentModel;assembly=WindowsBase      5        � 	Resources  5         gatewaySourceonline �� Source �� SortDescriptions+   %System.ComponentModel.SortDescription   PropertyName  gatewaySourceoffline��%  &	        &	 |     �� 5   
   -   5         5   ;   �� *CurrentGateways+ 5        5      $ 
LookupName��6%   5
      �� 5   
   -   5         5   <   �� *CurrentGateways+ 5        5      $ 
LookupName��6%   5      5      .��5      �� $
��Left=�6   $	��Top=�6,   $	��250��6D   .+�5   
   �� $��Online��6   $
��Bold"�6"   67   �� 5   
   -   5        $	��250��5      $��Stretch=�6#   $��0q�6?   ��6S   ��  �� Source# [  ��5      �� 5      .R�5      � $��1q�6   .+�5      �� $��2 2 0 0q�6!    �� CornerRadius$ 4��62   
������� J� Opacity$ .5Q�6V   �� 5      $��1q�6!   $ 4��65   
�������$��0 0 2 2q�6Y    �� Padding$ 0q�6j   ._�5      �  � ColumnDefinitions 5   "   �� 5   &   $��20 �67   6D   �� 5   &   $	��180 �67   6E   �� 5   &   69   .+�5!   "   �� $��0��6(   $��15��6U   $��15��6`   ��68   �� *	ImagePath+6o   �� 5"   "   $
��Bold"�6,   $��1��6Z   ��6>   �� *
LookupName+	 � ContextMenu	 5#   &   �� 5$   *   $��contextmenu��66   .@�5%   .   m� 
 m�Name$
 ContextMenu_ChangeTempIP��67   -   5        $��Change Temporary IP��5%   Y   6v   m� 5&   .   $
 ContextMenu_RemoveTempIP��67   -   5        $��Remove Temporary IP��5&   Y   6v   m� 5'   .   $
 ContextMenu_SetIP��67   -   5        $��Set Device IP��5'   R   6i   m� 5(   .   $
 ContextMenu_RebootModule��67   -   5        $��Reboot Module��5(   Y   6p   m� 5)   .   $
 ContextMenu_delete��67   -   5        $��Delete��5)   S   6c   5*   +   5,   #   �� 5-   "    ��Name$ configbutton��6f   $��2��6)    
 Config#��[ 69   $��30��6[     
openButton#��[ 6|   5.      5/      50      51      53      �� 54   
   $��Offine��6   $
��Bold"�6"   66   �� 55   
   -	   5        $	��250��55      $��Stretch=�6#   $��0q�6?   ��6S   �� # [ ��56      �� 57      .R�58      � $��1q�6   .+�59      �� $��2 2 0 0q�6!   $ 4��62   
�������$ .5Q�6V   �� 5:      $��1q�6!   $ 4��65   
�������$��0 0 2 2q�6Y   $ 2q�6j   ._�5;      �  5<   "   �� 5=   &   $��20 �67   6D   �� 5>   &   $	��180 �67   6E   �� 5?   &   69   .+�5A   "   �� $��0��6(   $��15��6U   $��15��6`   $��Center=�6l   $��Center=�6�   ��68   �� *	ImagePath+6�   �� 5B   "   $
��Bold"�6,   $��1��6Z   $��Center=�6j   $
��Left=�6�   ��6>   �� *
LookupName+	 5C   &   �� 5D   *   $��contextmenu��66   .@�5I   .   m� $
 ContextMenu_delete��67   -
   5        $��Delete��5I   S   6c   5J   +   5L   #   �� 5M   "   $��2��6)   $��25��69     ConfigLarge#��[ 6E   #��[ 6m   5N      5O      5P      5Q      5S      5U      5V      