using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using BattleCity.Elements.Tanks;

namespace BattleCity.Elements
{
	static class TexturesFactory
	{
		private const String PATH_TO_ELEMENTS = "Images/LevelElements/";
		private const String PATH_TO_UNIQUE_ELEMENTS = "Images/UniqueElements/";
		private static Dictionary<Element, BitmapImage> elements = PrepareTextures<Element>(PATH_TO_ELEMENTS);
		private static Dictionary<UniqueElement, BitmapImage> uniqueElements = PrepareTextures<UniqueElement>(PATH_TO_UNIQUE_ELEMENTS);

		public enum UniqueElement{
			Eagle,
			PlayerOne,
			PlayerTwo
		}

		public static BitmapImage ReturnTextureOfElement (Element element)
		{
			return elements[element];
		}

		public static BitmapImage ReturnTextureOfUniqueElement(UniqueElement element)
		{
			return uniqueElements[element];
		}

		public static Image DrawEagle()
		{
			var image = new Image();
			image.Source = ReturnTextureOfUniqueElement(UniqueElement.Eagle);
			image.Margin = new Thickness(6 * 32, 12 * 32, 0, 0);
			return image;
		}

		private static Dictionary<T, BitmapImage> PrepareTextures<T>(String path)
		{
			var elementsDictionary = new Dictionary<T, BitmapImage>();
			foreach (T element in Enum.GetValues(typeof(T)))
			{
				Uri uri = new Uri(path + element + ".bmp", UriKind.Relative);
				var bitmap = new BitmapImage(uri);
				elementsDictionary.Add(element, bitmap);
			}
			return elementsDictionary;
		}
		
		public static Geometry TrimTexture(Position position)
		{
			switch (position)
			{
				case Position.Full:
					return null;
				case Position.Left:
					return CreateGeometry(0, 0, 16, 32);
				case Position.Right:
					return CreateGeometry(16, 0, 16, 32);
				case Position.Up:
					return CreateGeometry(0, 0, 32, 16);
				case Position.Down:
					return CreateGeometry(0, 16, 32, 16);
				case Position.LeftDown:
					return CreateGeometry(0, 16, 16, 16);
				case Position.RightDown:
					return CreateGeometry(16, 16, 16, 16);
				case Position.LeftUp:
					return CreateGeometry(0, 0, 16, 16);
				case Position.RightUp:
					return CreateGeometry(16, 0, 16, 16);
				default:
					return null;
			}
		}

		private static Geometry CreateGeometry(int x, int y, int width, int height)
		{
			return new RectangleGeometry(new Rect(x, y, width, height));
		}
	}
}
