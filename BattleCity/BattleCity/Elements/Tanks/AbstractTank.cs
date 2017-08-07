using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using BattleCity.Helpers;

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
				MovementHelper.GetInstance().Move(this);
			}
			var image = new Image();
			image.Source = Image;
			image.Margin = new Thickness(PositionX - 16, PositionY - 16, 0, 0);
			image.RenderTransform = MovementHelper.GetInstance().RotateTank(this);
			return image;
		}
	}
}
