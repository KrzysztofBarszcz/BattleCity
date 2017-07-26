using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace BattleCity
{
	/// <summary>
	/// Interaction logic for the main window
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void InitializeMainScreen()
		{
			Image background = new Image();
			Image pointer = new Image();
			background.Source = new BitmapImage(new Uri("Images/startScreen.bmp", UriKind.Relative));
			mainCanvas.Children.Add(background);
			pointer.Source = new BitmapImage(new Uri("Images/icon.bmp", UriKind.Relative));
			Thickness margin = pointer.Margin;
			margin.Top = 264;
			margin.Left = 130;
			pointer.Margin = margin;
			mainCanvas.Children.Add(pointer);
		}

		private void mainWindow_Loaded(object sender, RoutedEventArgs e)
		{
			InitializeMainScreen();
		}
	}
}
