using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Controls;
namespace BattleCity.Elements
{
	[DataContract]
	class Field : IDrawable
	{
		[DataMember]
		public Element element { get; protected set; }
		[DataMember]
		public Position position { get; protected set; }
		[DataMember]
		public int row { get; protected set; }
		[DataMember]
		public int column { get; protected set; }

		private Field()
		{

		}

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
