using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using BattleCity.Elements;

namespace BattleCity
{
	/// <summary>
	/// Interaction logic for LevelEditor.xaml
	/// </summary>
	public partial class LevelEditor : Window
	{
		private enum Element {
			Concrete,
			Fast,
			Tree,
			Wall,
			Water
		}
		
		private Dictionary<String, BitmapImage> textures = new Dictionary<String, BitmapImage>();
	
		private String pathToTextures = "Images/LevelElements/";
		private Position position;

		public LevelEditor()
		{
			PrepareTextures();
			InitializeComponent();
			texturesListBox.ItemsSource = textures.Keys;
			positionListBox.ItemsSource = Enum.GetNames(typeof (Position));
		}

		private void PrepareTextures()
		{
			foreach (var e in Enum.GetNames(typeof (Element))){
				Uri uri = new Uri(pathToTextures + e + ".bmp", UriKind.Relative);
				var bitmap = new BitmapImage(uri);
				textures.Add(e, bitmap);
			}
		}

		private void texturesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var listBox = sender as ListBox;
			var key = listBox.SelectedItem as String;
			textureImage.Source = textures[key];
		}

		private void positionListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var item = (sender as ListBox).SelectedItem as String;
			position = (Position)Enum.Parse(typeof (Position), item);
		}
	}
}
