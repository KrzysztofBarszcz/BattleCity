using System.Runtime.Serialization;
using System.Windows.Controls;

namespace BattleCity.Elements
{
	[DataContract]
	public abstract class DrawableElement : IDrawable
	{
		public abstract Image draw();
	}
}
