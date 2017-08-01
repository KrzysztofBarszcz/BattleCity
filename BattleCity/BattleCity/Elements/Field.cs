using System.Windows;
using System.Windows.Controls;
namespace BattleCity.Elements
{
	class Field : IDrawable
	{
		public Element element { get; }
		public Position position { get; }
		public int row { get; }
		public int column { get; }

		private Field(Element element, Position position, int row, int column)
		{
			this.element = element;
			this.position = position;
			this.row = row;
			this.column = column;
		}

		public static Field createFieldFromElementAndPosition(Point point, Element element, Position position)
		{
			int row = (int)point.Y / 32;
			int column = (int)point.X / 32;
			return new Field(element, position, row, column);
		}

		public Image draw()
		{
			var image = new Image();
			image.Source = TexturesFactory.ReturnTextureOfElement(element);
			image.Margin = new Thickness(column*32, row * 32, 0, 0);
			image.Clip = TexturesFactory.TrimTexture(position);
			return image;
		}
	}
}
