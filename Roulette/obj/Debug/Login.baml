   M S B A M L   `   `   `  ����G  CRoulette.Net, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:RouletteRoulette  5   	       Roulette.Login   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:Roulette     5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	     � Title$  Login��5	   	   $	��230��5
   	   $	��430��5   	   ���#2d2d2d � WindowStyle$
 None=�5   	    � WindowStartupLocation$ CenterScreen=�5   	    � DataContext 5         Roulette.ViewModel  5   
   .��5      �  � RowDefinitions 5   
   �� 5      $	��50* �6   �� 5      $	��50* �6    � ColumnDefinitions 5   
   �� 5      $	��40* �6   �� 5      $	��60* �6   .+�5   
   �� $��2��6   .+�5      �� $��10q�6   .+�5      �� $��25��6   $��Benutzername:��6$   
�������$��DemiBold"�6O   $��0,4,0,0q�6e   6x   �� 5      $��25��6   $��	Passwort:��6$   
�������$��DemiBold"�6K   $��0,4,0,0q�6a   6t   5      �� 5       $��Center=�6   .+�5!      �� $E��?Benutzername oder Password Falsch bitte versuchen sie es erneut��6   
���  ��$��DemiBold"�6s   ��5"      �� 5#       �� 
TargetType" ��6     �� Triggers 5$      �� 5%   "    �� Binding 6.   �� *LoginCorrect+	 �� Value		 False6O   .a�5&   &   �� 
 �� Property
 � y� �� Value$ Visible=�6C   5'   #   �� 5(   "    6.   �� *LoginCorrect+	 true6O   .a�5)   &   �� 
 � y�$ Hidden=�6C   5*   #   5,      5.      5/      50      �� 51   
   $��1��6   $��10q�6%   .+�52      �� $��1,1,1,0q�6   
������� �� CornerRadius$ 5,5,5,5��6C   $	��200��6Z    J� RenderTransformOrigin$ 0.5,0.5&�6f   z�53      �� 54       �� Angle$	 180Q�6&   ._�56      �� $��25��6   ���#2d2d2d
�������$��DemiBold"�6h   $��0,4,0,0q�6~   $��0q�6�   $��4,4,0,0q�6�   ��6&   �� *
Username+.��6�    � LayoutTransform 57      �� 58      $
 -180Q�6*   .��59   /   5:      5;      �� 5<      $��1,1,1,0q�6   
�������$ 5,5,5,5��6C   $	��200��6Z   $ 0.5,0.5&�6f   z�5=      �� 5>      $	 180Q�6&   ._�5E      G� 
������ $��0q�67   $��25��6K   $��DemiBold"�6W   $��0,4,0,0q�6m   $��4,4,0,0q�6~   
������� G� PasswordChar$ $��6�   .  (System.Windows.Interactivity.Interaction   	Behaviors 5F      )   #Roulette.PasswordBoxBindingBehavior  5G         Password 6;   �� *
Password+ 5I      �� 5J      $
 -180Q�6*   5L      5M      5N      �� 5O   
   $��1��6   $��2��6"   $��Center=�66   $��Center=�6S    �� Orientation$ 
Horizontal=�6n   .+�5P      �� 
�������$��1q�6)   $ 5��6=    �� Padding$ 4q�6N   $��5q�6Z   $��40��6e   ._�5Q      �� $��Login��6   
������ 
�������$��DemiBold"�6U   $��20(�6k   $��0q�6y   ��6�   �� *btnLoginClickCommand+6�   5R      �� 5S      .+�5T      �� 
�������$��1q�6-   $ 5��6A   $ 4q�6R   $��5q�6^   ._�5U      �� $��Zurücksetzen��6   
������ 
�������$��DemiBold"�6`   $��20(�6v   $��0q�6�   ��6�   �� *btnClearClickCommand+6�   5V      �� 5W      
�������$��1q�6-   $ 5��6A   $ 4q�6R   $��5q�6^   ._�5X      �� $��
Neuer User��6   
������ 
�������$��DemiBold"�6^   $��20(�6t   $��0q�6�   ��6�   �� *btnNewUserClickCommand+6�   5Y      5Z      �� 5[      
�������$��1q�6)   $ 5��6=   $ 4q�6N   $��5q�6Z   $��40��6e   ._�5\      �� $��
Schließen��6   
������ 
�������$��DemiBold"�6Y   $��20(�6o   $��0q�6}   ��6�   �� *btnCloseClickCommand+6�   5]      5^      5_      5`      