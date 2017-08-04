﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Runtime.Serialization;
using BattleCity.Elements;
using Microsoft.Win32;

namespace BattleCity
{
	/// <summary>
	/// Interaction logic for LevelEditor.xaml
	/// </summary>
	public partial class LevelEditor : Window
	{
		private Position position;
		private List<Field>  elementsToDraw = new List<Field>();

		public LevelEditor()
		{
			InitializeComponent();
			texturesListBox.ItemsSource = Enum.GetValues(typeof(Element));
			texturesListBox.SelectedItem = Element.Wall;
			positionListBox.ItemsSource = Enum.GetValues(typeof(Position));
			positionListBox.SelectedItem = Position.Full;
		}

		private void texturesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Element element = (Element) (sender as ListBox).SelectedItem;

			textureImage.Source = TexturesFactory.ReturnTextureOfElement(element);
		}

		private void positionListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			position = (Position)(sender as ListBox).SelectedItem;
			textureImage.Clip = TexturesFactory.TrimTexture(position);
		}

		private void editorCanvas_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			var clickPosition = e.GetPosition(sender as Canvas);
			Element element = (Element)texturesListBox.SelectedItem;
			var field = Field.createFieldFromElementAndPosition(clickPosition, element, position);
			if (!IsFieldForbidden(field))
			{
				elementsToDraw.Add(field);
			}
			RedrawEditorScreen();
		}

		private bool IsFieldForbidden(Field field)
		{
			if (field.row == 0)
			{
				switch (field.column)//enemies starting points must be empty
				{
					case 0:
					case 6:
					case 12:
						return true;
				}
			}
			if (field.row == 12)
			{
				switch (field.column)//players starting points must be empty, eagle must remain
				{
					case 4:
					case 6:
					case 8:
						return true;
				}
			}
			return false;
		}

		private void RedrawEditorScreen()
		{
			editorCanvas.Children.Clear();
			foreach (var element in elementsToDraw)
			{
				editorCanvas.Children.Add(element.draw());
			}
		}

		private void saveMenuItem_Click(object sender, RoutedEventArgs e)
		{
			SaveFileDialog saveFileDialog = PrepareSaveLevelDialog();
			var result = saveFileDialog.ShowDialog();

			if (result == true)
			{
				SerializeFields(saveFileDialog);
			}
		}

		private SaveFileDialog PrepareSaveLevelDialog()
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.FileName = "level";
			saveFileDialog.DefaultExt = "xml";
			return saveFileDialog;
		}

		private void SerializeFields(SaveFileDialog saveFileDialog)
		{
			using (FileStream fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create))
			{
				DataContractSerializer serializer = new DataContractSerializer(typeof(List<Field>), new List<Type> { typeof(Field) });
				serializer.WriteObject(fileStream, elementsToDraw);
				fileStream.Close();
			}
		}

		private void loadMenuItem_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			var result = openFileDialog.ShowDialog();

			if (result == true)
			{
				DeserializeLevel(openFileDialog);
			}
		}

		private void DeserializeLevel(OpenFileDialog openFileDialog)
		{
			var file = openFileDialog.FileName;
			using (FileStream fileStream = new FileStream(file, FileMode.Open))
			{
				var serializer = new DataContractSerializer(typeof(List<Field>), new List<Type> { typeof(Field) });
				elementsToDraw = serializer.ReadObject(fileStream) as List<Field>;
			}
			RedrawEditorScreen();
		}
	}
}
