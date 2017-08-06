using System.Windows.Media.Imaging;

namespace BattleCity.Elements
{
    abstract class AbstractTank
    {
        public double PositionX { get; set; }
        public double PositionY { get; set; }
        public double Speed { get; set; }
        public double Resistance { get; set; }
        public BitmapImage Image { get; set; }
    }
}
