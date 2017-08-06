using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Input;

namespace BattleCity
{
	/// <summary>
	/// Interaction logic for the main window
	/// </summary>
	public partial class MainWindow : Window
	{
		private Image background = new Image();
		private Image pointer = new Image();
		private Options selectedOption;

		enum Options { ONE_PLAYER, TWO_PLAYERS, EDITOR}
		public MainWindow()
		{
			InitializeComponent();
			background.Source = new BitmapImage(new Uri("Images/startScreen.bmp", UriKind.Relative));
			pointer.Source = new BitmapImage(new Uri("Images/icon.bmp", UriKind.Relative));
			selectedOption = Options.ONE_PLAYER;
		}

		private void InitializeMainScreen()
		{
			mainCanvas.Children.Add(background);
			Thickness margin = pointer.Margin;
			margin.Top = 250;
			margin.Left = 100;
			pointer.Margin = margin;
			mainCanvas.Children.Add(pointer);
		}

		private void mainWindow_Loaded(object sender, RoutedEventArgs e)
		{
			InitializeMainScreen();
		}

		private void mainWindow_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Down)
			{
				if (pointer.Margin.Top < 314)
				{
					MoveDown(32);
					selectedOption = selectedOption == Options.ONE_PLAYER ? Options.TWO_PLAYERS : Options.EDITOR;
				}
			}
			if (e.Key == Key.Up)
			{
				if (pointer.Margin.Top > 250)
				{
					MoveDown(-32);
					selectedOption = selectedOption == Options.EDITOR ? Options.TWO_PLAYERS : Options.ONE_PLAYER;
				}
			}
			if (e.Key == Key.Space)
			{
				switch (selectedOption)
				{
					case Options.ONE_PLAYER:
						break;
					case Options.TWO_PLAYERS:
						break;
					case Options.EDITOR:
						LevelEditor levelEditor = new LevelEditor();
						levelEditor.Show();
						Close();
						break;
				}
			}
		}

		private void MoveDown(int pixels)
		{
			mainCanvas.Children.Clear();
			mainCanvas.Children.Add(background);
			Thickness margin = pointer.Margin;
			pointer.Margin = new Thickness(100, margin.Top + pixels, 0, 0);
			mainCanvas.Children.Add(pointer);
		}
	}
}
