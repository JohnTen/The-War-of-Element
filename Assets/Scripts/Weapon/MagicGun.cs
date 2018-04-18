using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElementWar.Weapon
{
	public class MagicGun : MonoWeapon, IElementalObject
	{
		[SerializeField]
		Element element;

		[SerializeField]
		ElementMeter meter;

		[SerializeField]
		ParabolaDrawer drawer;

		public Element Element
		{
			get { return element; }
			set { element = value; }
		}

		public void SwitchElement()
		{
			if (Element == Element.Earth)
				Element = Element.Air;
			else
				Element = (Element)((int)(element) << 1);
		}

		protected override void InitializeProjectile(MonoProjectile projectile)
		{
			base.InitializeProjectile(projectile);
			var eleProjectile = projectile as MagicalProjectile;
			if (eleProjectile == null) return;
			eleProjectile.Element = Element;
		}

		protected virtual void Start()
		{
			meter.ShowElements(Element);
		}

		protected override void Update()
		{
			drawer.InitVelocity = Vector3.forward * parameter.LaunchForce;
			if (Input.GetKeyDown(KeyCode.F))
			{
				SwitchElement();
				if (meter != null)
					meter.ShowElements(Element);
			}
			base.Update();
		}
	}
}