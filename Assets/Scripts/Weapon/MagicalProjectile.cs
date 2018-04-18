using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElementWar.Weapon
{
	public class MagicalProjectile : MonoProjectile, IElementalObject
	{
		[SerializeField]
		float gravityScale = 1;

		[SerializeField]
		Element element;
		public Element Element
		{
			get { return element; }
			set { element = value; }
		}

		protected override void Update()
		{
			velocity.y -= gravityScale * 9.81f * Time.deltaTime;
			base.Update();
		}

		protected override void Hitting(Collider collider)
		{
			base.Hitting(collider);
			var target = collider.GetComponent<IElementalTarget>();

			if (target == null && collider.transform.parent != null)
				target = collider.transform.parent.GetComponent<IElementalTarget>();

			if (target != null)
				target.HitByElement(Element);
			Destroy(this.gameObject);
		}
	}
}

