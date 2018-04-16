using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ElementWar.Weapon
{
	[RequireComponent(typeof(Rigidbody))]
	public class PhysicalProjectile : MonoProjectile
	{
		protected Rigidbody rigid;
		public override Vector3 Velocity
		{
			get
			{
				return rigid.velocity;
			}

			set
			{
				rigid.velocity = value;
			}
		}

		protected virtual void Awake()
		{
			rigid = GetComponent<Rigidbody>();
		}

		protected override bool Moving(Vector3 velocity, out RaycastHit hit)
		{
			Ray ray = new Ray(Position, velocity * Time.deltaTime);

			Physics.Raycast(ray, out hit);

			return hit.collider;
		}
	}
}
