using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Controls;
namespace BattleCity.Elements
{
	[DataContract]
	class Field : DrawableElement
	{
		[DataMember]
		public Element element { get; protected set; }
		[DataMember]
		public Position position { get; protected set; }
		[DataMember]
		public int row { get; protected set; }
		[DataMember]
		public int column { get; protected set; }

		public Dictionary<Direction, int> state = new Dictionary<Direction, int>();

		private Field()
		{
			SetState(position);
		}

		private Field(Element element, Position position, int row, int column)
		{
			this.element = element;
			this.position = position;
			this.row = row;
			this.column = column;

			SetState(position);
		}

		public void SetState(int up, int down, int left, int right)
		{
			SetState(up, Direction.Up);
			SetState(down, Direction.Down);
			SetState(left, Direction.Left);
			SetState(right, Direction.Right);
		}

		private void SetState(int value, Direction direction)
		{
			if(state == null)
			{
				state = new Dictionary<Direction, int>();
			}
			if (state.ContainsKey(direction)){
				state[direction] = value;
			}
			else
			{
				state.Add(direction, value);
			}
		}

		private void SetState(Position position)
		{
			switch (position)
			{
				case Position.Full:
					SetState(4, 4, 4, 4);
					break;
				case Position.Up:
					SetState(4, 2, 4, 4);
					break;
				case Position.Down:
					SetState(2, 4, 4, 4);
					break;
				case Position.Left:
					SetState(4, 4, 4, 2);
					break;
				case Position.Right:
					SetState(4, 4, 2, 4);
					break;
				case Position.LeftUp:
					SetState(4, 2, 4, 2);
					break;
				case Position.RightUp:
					SetState(4, 2, 2, 4);
					break;
				case Position.LeftDown:
					SetState(2, 4, 4, 2);
					break;
				case Position.RightDown:
					SetState(2, 4, 2, 4);
					break;
			}
		}

		public void SetState()
		{
			SetState(position);
		}

		public static Field CreateFieldFromElementAndPosition(Point point, Element element, Position position)
		{
			int row = (int)point.Y / 32;
			int column = (int)point.X / 32;
			return CreateFieldFromElementAndPosition(row, column, element, position);
		}
		public static Field CreateFieldFromElementAndPosition(int row, int column, Element element, Position position)
		{
			return new Field(element, position, row, column);

		}

		public override Image Draw()
		{
			var image = new Image();
			image.Source = TexturesFactory.ReturnTextureOfElement(element);
			image.Margin = new Thickness(column*32, row * 32, 0, 0);
			image.Clip = TexturesFactory.TrimTexture(position);
			return image;
		}
	}
}
