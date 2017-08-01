using System.Windows;

namespace BattleCity.Elements
{
	class Field
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
	}
}
