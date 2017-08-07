using System.Windows.Media;
using BattleCity.Elements.Tanks;

namespace BattleCity.Helpers
{
	class MovementHelper
	{
		private static MovementHelper instance;

		public static MovementHelper GetInstance()
		{
			if (instance == null)
			{
				instance = new MovementHelper();
			}
			return instance;
		}

		public void Move(AbstractTank tank)
		{
			double delta = (double)tank.Clock.ElapsedMilliseconds / 1000;
			tank.Clock.Restart();
			switch (tank.Direction)
			{
				case Direction.Up:
					tank.PositionY -= delta * tank.Speed;
					tank.PositionX = Trim(tank.PositionX);
					break;
				case Direction.Down:
					tank.PositionY += delta * tank.Speed;
					tank.PositionX = Trim(tank.PositionX);
					break;
				case Direction.Left:
					tank.PositionX -= delta * tank.Speed;
					tank.PositionY = Trim(tank.PositionY);
					break;
				case Direction.Right:
					tank.PositionX += delta * tank.Speed;
					tank.PositionY = Trim(tank.PositionY);
					break;
			}
		}

		private int Trim(double position)
		{
			if (position % 16 >= 8)
				return ((int)position / 16 + 1) * 16;
			else return (int)position / 16 * 16;
		}

		public RotateTransform RotateTank(AbstractTank tank)
		{
			switch (tank.Direction)
			{
				case Direction.Up:
					return null;
				case Direction.Down:
					return new RotateTransform(180, 16, 16);
				case Direction.Left:
					return new RotateTransform(270, 16, 16);
				case Direction.Right:
					return new RotateTransform(90, 16, 16);
				default:
					return null;
			}
		}
	}
}
