   M S B A M L   `   `   `  ����=  9TT, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:TT.Controls.FriendTT.Controls.Friend  5      ,    &TT.Controls.Friend.ChatListItemControl   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:TT.Controls.Friend     5        � 	Resources  5	         ContentStyle �� 
TargetType �� Property �� Value �� 
TargetType ��Name J� InputBindings d� MouseAction �� Command	 �Name
 � ColumnDefinitions �� Padding �� RelativeSource �� CornerRadius  ForegroundLightBrush  FontSizeLarge  LatoRegular �� Padding  WordBlueBrush  ForegroundDarkBrush �� Triggers �� Value �� Binding �� 
TargetName  WordVeryVeryLightBlueBrush :� RoutedEvent  WordVeryVeryLightBlue	 �� To g� Duration �� 
TargetName �� TargetProperty P� Property P� Value��%Y  &	        7  7  7  7  7  7  7  7  7  7  �� 5
   
   " ��5      .�5      ��  � �� 5      �� 5      " ��5      .R�5      �� $ 
background��5       5      d� 5   "   $ 	LeftClicke�5   %    5   %   �� *OpenMessageCommand+._�5      � $	 	container��5   !   
������ 
 5    "   �� 5#   &   $
��Auto �5$   )   �� 5&   &   $��* �5'   )   .+�5+   "   �� 
���  ��$��4��5-   %   $
��Left=�5.   %   �� 52   "   $��0��53   %   $
 8 15q�54   %   ._�56   &   �� $��40��57   )   ��58   )   �� *ActualHeight+ �� *Self+ 59   )   �� *ActualHeight+ �� *Self+._�5;   *   �� $��Center=�5=   -   $��Center=�5>   -   8��  8�� 8�� ��5<   -   �� *
Initials+5A   -   5B   '   5C   #   �� 5F   "   $��1��5G   %   $ 0 0 15 0q�5H   %   ._�5I   &   �� $��Center=�5J   )   .+�5L   *   �� $ 0 0 0 2q�5N   -   8�� $��CharacterEllipsis=�5P   -   8�� ��5M   -   �� *Name+5Q   -   �� 5S   *   $ 0 2 0 0q�5U   -   8�� $��CharacterEllipsis=�5W   -   8�� ��5T   -   �� *	Message+5X   -   5Z   '   5[   #   5]      5^       5a      �� 5c      $
 True��5e   !    5d   !   �� *
IsSelected+.a�5f   "   �� $ 
background��5h   %   8   � ��5i   %   5j      :� 5l      $ 
MouseEnter��5m   !   .[�5n   "   �� .��5o   &   �� .��5p   *   �� 8  $ 0:0:0.3D�5r   -   $ 	container��5s   -   $ Background.Color�5t   -   5u   '   5v   #   5w      :� 5x      $ 
MouseLeave��5y   !   .[�5z   "   �� .��5{   &   �� .��5|   *   �� $ TransparentX�5}   -   $ 0:0:0.3D�5~   -   $ 	container��5   -   $ Background.Color�5�   -   5�   '   5�   #   5�      P� 5�       � {� .  . �5�   "   �� $ 	container��5�   %   8 	  � ��5�   %   5�      5�      5�      5�      5�      .��5�      �� #��[  5�   	   5�      