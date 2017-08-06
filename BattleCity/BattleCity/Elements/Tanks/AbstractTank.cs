using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace BattleCity.Elements.Tanks
{
    abstract class AbstractTank: IDrawable
    {
        public double PositionX { get; set; }
        public double PositionY { get; set; }
        public double Speed { get; set; }
        public double Resistance { get; set; }
        public BitmapImage Image { get; set; }

		internal AbstractTank(int positionY, int positionX, double speed, int resistance, BitmapImage image)
		{
			PositionY = positionY;
			PositionX = positionX;
			Speed = speed;
			Resistance = resistance;
			Image = image;
		}

		public Image Draw()
		{
			var image = new Image();
			image.Source = Image;
			image.Margin = new Thickness(PositionX - 16, PositionY - 16, 0, 0);
			return image;
		}
	}
}
