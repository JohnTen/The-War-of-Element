using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElementWar.Weapon
{
	public class MagicalProjectile : MonoProjectile, IElementalObject
	{
		[SerializeField]
		Element element;
		public Element Element
		{
			get { return element; }
			set { element = value; }
		}

		protected override void Hitting(Collider collider)
		{
			base.Hitting(collider);
			var target = collider.GetComponent<IElementalTarget>();
			if (target != null)
				target.HitByElement(Element);
			Destroy(this.gameObject);
		}
	}
}

