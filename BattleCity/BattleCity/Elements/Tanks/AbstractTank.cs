using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BattleCity.Elements.Tanks
{
    abstract class AbstractTank: IDrawable
    {
		public bool IsMoving { get; set; }
		public double PositionX { get; set; }
        public double PositionY { get; set; }
        public double Speed { get; set; }
        public double Resistance { get; set; }
        public BitmapImage Image { get; set; }

		public Direction Direction { get; set; }
		public Stopwatch Clock { get; set; }
		
		internal AbstractTank(int positionY, int positionX, double speed, int resistance, BitmapImage image)
		{
			PositionY = positionY;
			PositionX = positionX;
			Speed = speed;
			Resistance = resistance;
			Image = image;
			Clock = new Stopwatch();
		}

		public Image Draw()
		{
			if (IsMoving)
			{
				Move();
			}
			var image = new Image();
			image.Source = Image;
			image.Margin = new Thickness(PositionX - 16, PositionY - 16, 0, 0);
			image.RenderTransform = RotateTank();
			return image;
		}

		private RotateTransform RotateTank()
		{
			
			switch (Direction)
			{
				case Direction.Up:
					return null;
				case Direction.Down:
					return new RotateTransform(180,16,16);
				case Direction.Left:
					return new RotateTransform(270, 16, 16);
				case Direction.Right:
					return new RotateTransform(90, 16, 16);
				default:
					return null;
			}
		}

		private void Move()
		{
			double delta = (double)Clock.ElapsedMilliseconds /1000;
			Clock.Restart();
			switch (Direction)
			{
				case Direction.Up:
					PositionY -= delta * Speed;
					PositionX = Trim(PositionX);
					break;
				case Direction.Down:
					PositionY += delta * Speed;
					PositionX = Trim(PositionX);
					break;
				case Direction.Left:
					PositionX -= delta * Speed;
					PositionY = Trim(PositionY);
					break;
				case Direction.Right:
					PositionX += delta * Speed;
					PositionY = Trim(PositionY);
					break;
			}
		}

		private int Trim(double position)
		{
			if (position % 16 >= 8)
				return ((int)position / 16 + 1) * 16;
			else return (int)position / 16 * 16;
		}
	}
}
