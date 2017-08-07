using System;
using System.Windows.Media;
using BattleCity.Elements;
using BattleCity.Elements.Tanks;

namespace BattleCity.Helpers
{
	class MovementHelper
	{
		private static MovementHelper instance = new MovementHelper();
		public Field[,] levelElements { get; set; }

		public static MovementHelper GetInstance()
		{
			return instance;
		}

		public void Move(AbstractTank tank)
		{
			double delta = (double)tank.Clock.ElapsedMilliseconds / 1000;
			tank.Clock.Restart();
			switch (tank.Direction)
			{
				case Direction.Up:
					tank.PositionX = Trim(tank.PositionX);
					if (!Collision(tank))
					{
						tank.PositionY -= delta * tank.Speed;
					}
					break;
				case Direction.Down:
					tank.PositionX = Trim(tank.PositionX);
					if (!Collision(tank))
					{
						tank.PositionY += delta * tank.Speed;
					}
					break;
				case Direction.Left:
					tank.PositionY = Trim(tank.PositionY);
					if (!Collision(tank))
					{
						tank.PositionX -= delta * tank.Speed;
					}
					break;
				case Direction.Right:
					tank.PositionY = Trim(tank.PositionY);
					if (!Collision(tank))
					{
						tank.PositionX += delta * tank.Speed;
					}
					break;
			}
			BoundaryCollision(tank);
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

		private bool Collision(AbstractTank tank)
		{
			int tankRow = (int)Math.Round((tank.PositionY - 16) / 32);
			int tankColumn = (int)Math.Round((tank.PositionX - 16) / 32);
			switch (tank.Direction)
			{
				case Direction.Up:
					if (tankRow > 0 && levelElements[tankRow - 1, tankColumn] != null && tank.PositionY <= tankRow * 32 + 16)
					{
						return true;
					}
					else return false;
				case Direction.Down:
					if (tankRow < 12 && levelElements[tankRow + 1, tankColumn] != null && tank.PositionY >= tankRow * 32 + 16)
					{
						return true;
					}
					else return false;
				case Direction.Left:
					if (tankColumn > 0 && levelElements[tankRow, tankColumn - 1] != null && tank.PositionX <= tankColumn * 32 + 16)
					{
						return true;
					}
					else return false;
				case Direction.Right:
					if (tankColumn < 12 && levelElements[tankRow, tankColumn + 1] != null && tank.PositionX >= tankColumn * 32 + 1)
					{
						return true;
					}
					else return false;
				default: return false;
			}
		}

		private void BoundaryCollision(AbstractTank tank)
		{
			if (tank.PositionX < 16)
			{
				tank.PositionX = 16;
			}
			else if (tank.PositionX > 400)
			{
				tank.PositionX = 400;
			}
			if (tank.PositionY < 16)
			{
				tank.PositionY = 16;
			}
			else if (tank.PositionY > 400)
			{
				tank.PositionY = 400;
			}
		}
	}
}
