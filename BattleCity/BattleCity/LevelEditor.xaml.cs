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

		private Dictionary<Element, BitmapImage> textures = new Dictionary<Element, BitmapImage>();
	
		private String pathToTextures = "Images/LevelElements/";
		private Position position;
		private Element element;
		private Field[,] fields = new Field[13, 13];

		public LevelEditor()
		{
			PrepareTextures();
			InitializeComponent();
			texturesListBox.ItemsSource = textures.Keys;
			positionListBox.ItemsSource = Enum.GetValues(typeof(Position));
		}

		private void PrepareTextures()
		{ 
			foreach (Element e in Enum.GetValues(typeof (Element))){
				Uri uri = new Uri(pathToTextures + e + ".bmp", UriKind.Relative);
				var bitmap = new BitmapImage(uri);
				textures.Add( e, bitmap);
			}
		}

		private void texturesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Element key = (Element) (sender as ListBox).SelectedItem;
			textureImage.Source = textures[key];
		}

		private void positionListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			position = (Position) (sender as ListBox).SelectedItem;
		}

		private void editorCanvas_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			var clickPosition = e.GetPosition(sender as Canvas);
			Element element = (Element)texturesListBox.SelectedItem;
			var field = Field.createFieldFromElementAndPosition(clickPosition, element, position);
			fields[field.row, field.column] = field;

			RedrawEditorScreen();
		}

		private void RedrawEditorScreen()
		{
			editorCanvas.Children.Clear();
			for (int i = 0; i < 13; i++)
			{
				for (int j = 0; j < 13; j++)
				{
					if (fields[i, j] != null)
					{
						var image = new Image();
						image.Source = textures[fields[i, j].element];
						image.Margin = new Thickness(j * 32, i * 32, 0, 0);
						editorCanvas.Children.Add(image);
					}
				}
			}
		}
	}
}
