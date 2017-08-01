using System;
using System.Collections.Generic;
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
	}
}
