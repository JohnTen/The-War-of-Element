using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ElementWar.Weapon
{
	public interface IProjectile
	{
		float Damage { get; set; }

		Vector3 Velocity { get; set; }

		Vector3 Position { get; set; }
    }

	public class MonoProjectile : MonoBehaviour, IProjectile
	{
		protected float damage;
		public float Damage
		{
			get { return damage; }
			set { damage = value; }
		}

		protected Vector3 velocity;
		public virtual Vector3 Velocity
		{
			get { return velocity; }
			set { velocity = value; }
		}

		protected Vector3 position;
		public virtual Vector3 Position
		{
			get { return position; }
			set { position = value; }
		}

		protected virtual void Update()
		{
			RaycastHit hit;
			if (Moving(Velocity, out hit))
			{
				Hitting(hit.collider);
			}
        }

		/// <summary>
		/// Move this projectile by velocity.
		/// </summary>
		/// <param name="velocity"></param>
		/// <returns>If this projectile hits something, return true, otherwise, false.</returns>
		protected virtual bool Moving(Vector3 velocity, out RaycastHit hit)
		{
			Ray ray = new Ray(Position, velocity * Time.deltaTime);

			Physics.Raycast(ray, out hit);

			transform.position += velocity * Time.deltaTime;

			return hit.collider;
		}

		protected virtual void Hitting(Collider collider)
		{

		}
	}
}
