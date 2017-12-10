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

			double nextPositionX =0, nextPositionY =0;

			switch (tank.Direction)
			{
				case Direction.Up:
					nextPositionX = Trim(tank.PositionX);
					nextPositionY = tank.PositionY - delta * tank.Speed;
					break;
				case Direction.Down:
					nextPositionX = Trim(tank.PositionX);
					nextPositionY = tank.PositionY + delta * tank.Speed;
					break;
				case Direction.Left:
					nextPositionY = Trim(tank.PositionY);
					nextPositionX = tank.PositionX - delta * tank.Speed;
					  break;
				case Direction.Right:
					nextPositionY = Trim(tank.PositionY);
					nextPositionX = tank.PositionX + delta * tank.Speed;
					break;
			}
			if (!Collision(tank, nextPositionX, nextPositionY))
			{
				tank.PositionX = nextPositionX;
				tank.PositionY = nextPositionY;
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

		private bool Collision(AbstractTank tank, double posX, double posY)
		{
			int tankColumn = (int)(posX) / 32;
			int tankRow = (int)(posY) / 32;
			return CollisionWithField(tank, tankColumn, tankRow) || CollisionWithBounds(posX, posY);
		}

		private bool CollisionWithField(AbstractTank tank, int tankColumn, int tankRow)
		{
			if (tank.Direction == Direction.Up || tank.Direction == Direction.Down)
			{
				bool halfColumn = Trim(tank.PositionY) % 2 == 0;
				for (int i = 0; i < 13; i++)
				{
					if (CheckCollision(tank, levelElements[i, tankColumn]))
						return true;
				}
			}
			if (tank.Direction == Direction.Left || tank.Direction == Direction.Right)
			{
				for (int i = 0; i < 13; i++)
				{
					if (CheckCollision(tank, levelElements[tankRow, i]))
					{
						return true;
					}
				}
			}
			return false;
		}

		private bool CheckCollision(AbstractTank tank, Field field)
		{
			if (field == null)
				return false;
			int up = ReturnValueFromState(field, Direction.Up);
			int down = ReturnValueFromState(field, Direction.Down);
			int left = ReturnValueFromState(field, Direction.Left);
			int right = ReturnValueFromState(field, Direction.Right);
			
			if (tank.Direction == Direction.Up && Math.Abs(tank.PositionY - (field.row * 32 + 16 - down)) < 32
				&& tank.PositionY > field.row * 32 + 16)
			{
				return true;
			}
			else if (tank.Direction == Direction.Down && Math.Abs(tank.PositionY - (field.row * 32 + 16 + up)) < 32
				&& tank.PositionY < field.row*32+16)
			{
				return true;
			}
			else if (tank.Direction == Direction.Left && Math.Abs(tank.PositionX - (field.column*32 + 16 - right))<32
				&& tank.PositionX > (field.column * 32 + 16))
			{
				return true;
			}
			else if( tank.Direction == Direction.Right && Math.Abs(tank.PositionX - (field.column*32 +16 + left)) < 32
				&& tank.PositionX < (field.column * 32 + 16))
			{
				return true;
			}
			return false;
		}

		private int ReturnValueFromState(Field field, Direction direction)
		{
			if (field != null)
				return field.state[direction] <= 2 ? 16 : 0;
			else return 0;
		} 

		private bool CollisionWithBounds(double posX, double posY)
		{
			if (posX < 16 || posX > 400 || posY < 16 || posY > 400)
			{
				return true;
			}
			else return false;
		}
	}
}
