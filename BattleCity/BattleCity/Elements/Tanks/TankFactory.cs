namespace BattleCity.Elements.Tanks
{
	static class TankFactory
	{
		public static PlayerTank CreatePlayerOneTank()
		{
			return new PlayerTank(12 * 32 + 16, 4 * 32 + 16, 64, 1, 
				TexturesFactory.ReturnTextureOfUniqueElement(TexturesFactory.UniqueElement.PlayerOne));
		}

		public static PlayerTank CreatePlayerTwoTank()
		{
			return new PlayerTank(12 * 32 + 16, 8 * 32 + 16, 64, 1,
				TexturesFactory.ReturnTextureOfUniqueElement(TexturesFactory.UniqueElement.PlayerTwo));
		}
	}
}
