   M S B A M L   `   `   `  ����I  EGatewayBrowser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:GatewayBrowser.UserControlsGatewayBrowser.UserControls  5   	       GatewayBrowser.Shell   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   8uc)clr-namespace:GatewayBrowser.UserControls     5   	     � Title$  Gateway Browser��5   	      GatewayImageSource � Icon# [  5   	   $	��600��5   	   $	��800��5   	    � WindowState$ 	Maximized=�6   .��5	      ]� .+�5
   
   o� $	��Top=�6   .@�5      m� $
��Scan��6   .@�5      m� $
��Scan��6;     Scan m� Icon# [ 6I    m� InputGestureText$ Ctrl+S��6f    m� Command 6   �� *ScanCommand+6f   �� 5      m� 5      $��Add Offline Gateway��6B     Add# [ 6_   $ Ctrl+M��6{    6   �� *ManuallyAddCommand+6{   5      m� 5      $
��Help��6   .@�5      m� $��About��6<    	 About# [ 6K   $ F1��6i    6   �� *AboutCommand+6i   5      5      �� 5   
   $��Bottom=�6   .@�5      �� .��5      �� ��6   �� *
StatusText+5      5      9� 5   
   $
��Left=�6    9� ExpandDirection$ Right=�5       9� 
IsExpanded . $��0 1 1 1q�5      
�������$��0 2 0 0q�5       $��2q�5!      ��5"      �� 5#      -   5         ��Name$ TextBlock_GateWays��5#      $��GateWays��68   $��14(�6H   $
��Bold"�6V   	 � LayoutTransform	 5$      �� 5%      
 �� Angle$
 90Q�6&   5'      .��5)      )   #GatewayBrowser.UserControls.SideBar  -   5          Name$ sd_sdnav��5)      5*      �� 5,   
   -   5         ��Name$
 tabs��5,      $��2��6#   $	��5 0q�63   5-      5/      50      