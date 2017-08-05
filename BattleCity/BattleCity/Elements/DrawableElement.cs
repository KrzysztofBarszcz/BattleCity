using System.Runtime.Serialization;
using System.Windows.Controls;

namespace BattleCity.Elements
{
	[DataContract]
	abstract class DrawableElement : IDrawable
	{
		public abstract Image draw();
	}
}
