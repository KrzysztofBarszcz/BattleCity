using System.Windows.Media.Imaging;

namespace BattleCity.Elements.Tanks
{
	class PlayerTank : AbstractTank
	{
		internal PlayerTank(int positionY, int positionX, double speed, int resistance, BitmapImage image):
			base(positionY, positionX, speed, resistance, image)
		{
		}

		public void StopTank()
		{
			IsMoving = false;
			Clock.Stop();
		}
	}
}
