   M S B A M L   `   `   `  ����K  GCalculadoraTinta, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:CalculadoraTintaCalculadoraTinta  5   	   !    CalculadoraTinta.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:CalculadoraTinta     5   	     � Title$  Calculadora��5   	   $	��800��6   $	��600��6*   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� -   5        $��textBox��5       �� TextWrapping$ WrapWithOverflow=�6"   
�������$��80(�5      $��Console)�6!   
���   �$��Center=�6I   {�.  .��5   %   5      �� 5   
   -   5        $��canvasTinta��5      $��1��6'   66   �� 5   
   -   5        $��Borrar��5      $��2��6   .��6A   Borrar último6P   � 5   
   $��3��6    � ColumnDefinitions 5      �� 5      $
��Auto �6#   62   �� 5      $
��Auto �6#   62   �� 5       6%   .+�5#      	� -   5        $��Modo1��5#      $��0��6'    d� 	IsChecked$
 TrueY�67   $��2q�6`   .��6k   Modo 16s   	� 5$      -   5        $��Modo2��5$      $��1��6'   $��2q�6O   .��6Z   Modo 26b   �� 5%      -   5        $��FeedbackText��5%      $��2��6*   $��2q�6:   $��Right=�6E   5&      5(      5)      