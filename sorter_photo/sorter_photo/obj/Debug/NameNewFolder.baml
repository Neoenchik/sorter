   M S B A M L   `   `   `  ����G  Csorter_photo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:sorter_photosorter_photo  5   	        sorter_photo.NameNewFolder   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:sorter_photo     5   	     � Title$  Новое имя��5   	    � SizeToContent$ WidthAndHeight=�6    � WindowStartupLocation$ CenterScreen=�6:   .��5	      � $��10q�6    � ColumnDefinitions 5
   
   �� 5      $
��117* �6   �� 5      $
��133* �6    � RowDefinitions 5   
   �� 5      $��20 �6   �� 5      $��20 �6   �� 5      $
��auto �6   .+�5   
   �� $��2��6   Enter name new folder:6@   �� 5   
   -   5        $��
NameFolder��5      $��1��6$   $	��250��61   $��2��6@   .��6T   Name6Z   � 5   
   $��2��6   $
��Left=�6!   $��	105,0,0,0q�6<   $��2��6O   .+�5      �� -   5         �� 	IsDefault . $��60��5   ;   $��0,0,10,0q�6I   .��6[   OK6_   �� 5       �� IsCancel . $��60��6%   .��64   Cancel6<   5      5      5      