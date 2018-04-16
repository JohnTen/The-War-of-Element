using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElementWar.Weapon
{
	[System.Flags]
	public enum Element
	{
		None = 0,
		Air = 1,
        Fire = 2,
		Water = 4,
		Earth = 8,
	}

	public class MagicalProjectile : MonoProjectile
	{
		public Element Element { get; set; }
	}
}

