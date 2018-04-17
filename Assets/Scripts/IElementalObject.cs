using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElementWar
{
	[Flags]
	public enum Element
	{
		None = 0,
		Air = 1,
		Fire = 2,
		Water = 4,
		Earth = 8,
	}

	public interface IElementalObject
	{
		Element Element { get; set; }
	}

	public interface IElementalTarget : IElementalObject
	{
		Element[] GetElements();
		void HitByElement(Element element);
	}
}
