   M S B A M L   `   `   `  ����G  CRoulette.Net, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:RouletteRoulette  5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity 5   	       Roulette.NewUser   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:Roulette     5   	   aintrPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     5   	     � Title$  Neuer Benutzer��5	   	   $	��230��5
   	   $	��430��5   	    � WindowStartupLocation$ CenterScreen=�5   	    � WindowStyle$
 None=�5   	    � 
ResizeMode$ NoResize=�5   	   ���#2d2d2d � DataContext 5         Roulette.ViewModel  5   
   .  (System.Windows.Interactivity.Interaction   Triggers 5      /  )System.Windows.Interactivity.EventTrigger  5   
      	EventName$ Loaded��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6'   �� *newUserLoadedCommand+6R   5      .��5      � 	 � RowDefinitions	 5   
   �� 5      $	��60* �6   �� 5      $	��40* �6   
 � ColumnDefinitions
 5   
   �� 5      $	��40* �6   �� 5      $	��60* �6   .+�5"   
   �� $��10q�6   .+�5#      �� $��25��6   $��User ID:��6    
�������$��DemiBold"�6F   $��0,4,0,0q�6\   6o   �� 5$      $��25��6   $��Benutzername:��6    
�������$��DemiBold"�6K   $��0,4,0,0q�6a   6t   �� 5%      $��25��6   $��	Passwort:��6    
�������$��DemiBold"�6G   $��0,4,0,0q�6]   6p   5&      �� 5'   
   $��1��6   $��10q�6%   .+�5(      �� $��1,1,1,0q�6   
������� �� CornerRadius$ 5,5,5,5��6C   $	��200��6Z    J� RenderTransformOrigin$ 0.5,0.5&�6f   z�5)      �� 5*       �� Angle$	 180Q�6&   ._�5,      �� $��25��6   ���#2d2d2d
�������$��DemiBold"�6i   $��0,4,0,0q�6   $��0q�6�   $��4,4,0,0q�6�   {�.  ��6&   �� *	NewUserId+.��6�    � LayoutTransform 5-      �� 5.      $
 -180Q�6*   .��5/   /   50      51      �� 52      $��1,1,1,0q�6   
�������$ 5,5,5,5��6C   $	��200��6Z   $ 0.5,0.5&�6f   z�53      �� 54      $	 180Q�6&   ._�56      �� $��25��6   ���#2d2d2d
�������$��DemiBold"�6o   $��0,4,0,0q�6�   $��0q�6�   $��4,4,0,0q�6�   ��6&   �� *NewUserUsername+.��6�    57      �� 58      $
 -180Q�6*   .��59   /   5:      5;      �� 5<      $��1,1,1,0q�6   
�������$ 5,5,5,5��6C   $	��200��6Z   $ 0.5,0.5&�6f   z�5=      �� 5>      $	 180Q�6&   ._�5E      G� 
������ $��0q�67   $��25��6K   $��DemiBold"�6W   $��-1,4,1,0q�6m   $��4,4,0,0q�6   
������� G� PasswordChar$ $��6�      	Behaviors 5F      )   #Roulette.PasswordBoxBindingBehavior  5G         Password 6;   �� *NewUserPassword+ 5I      �� 5J      $
 -180Q�6*   5L      5M      5N      �� 5O   
   $��1��6   $��2��6"    �� Orientation$ 
Horizontal=�66   $��Center=�6O   .+�5P      �� 
�������$��1q�6)   $ 5��6=    �� Padding$ 4q�6N   $��5q�6Z   $��40��6e   ._�5Q      �� $��	Erstellen��6   
������ 
�������$��DemiBold"�6Y   $��20(�6o   $��0q�6}   ��6�   �� *btnCreateNewUserClickCommand+6�   5R      �� 5S      
�������$��1q�6)   $ 5��6=   $ 4q�6N   $��5q�6Z   $��40��6e   ._�5T      �� $��Zurücksetzen��6   
������ 
�������$��DemiBold"�6\   $��20(�6r   $��0q�6�   ��6�   �� *btnClearClickCommand+6�   5U      �� 5V      
�������$��1q�6)   $ 5��6=   $ 4q�6N   $��5q�6Z   $��40��6e   ._�5W      �� $��	Abbrechen��6   
������ 
�������$��DemiBold"�6Y   $��20(�6o   $��0q�6}   ��6�   �� *btnCancelClickCommand+6�   5X      5Y      5[      5\      