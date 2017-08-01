using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BattleCity.Elements
{
	static class TexturesFactory
	{
		private static String PATH_TO_ELEMENTS = "Images/LevelElements/";
		private static Dictionary<Element, BitmapImage> elements = PrepareTextures();

		public static BitmapImage ReturnTextureOfElement (Element element)
		{
			return elements[element];
		}

		private static Dictionary<Element, BitmapImage> PrepareTextures()
		{
			var elementsDictionary = new Dictionary<Element, BitmapImage>();
			foreach (Element e in Enum.GetValues(typeof(Element)))
			{				
				Uri uri = new Uri(PATH_TO_ELEMENTS + e + ".bmp", UriKind.Relative);
				var bitmap = new BitmapImage(uri);
				elementsDictionary.Add(e, bitmap);
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
