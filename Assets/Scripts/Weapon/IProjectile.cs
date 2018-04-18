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

		float LiveTime { get; set; }

		Vector3 Velocity { get; set; }

		Vector3 Position { get; set; }
    }

	public class MonoProjectile : MonoBehaviour, IProjectile
	{
		[SerializeField]
		protected float damage;
		public virtual float Damage
		{
			get { return damage; }
			set { damage = value; }
		}

		[SerializeField]
		protected float liveTime;
		public virtual float LiveTime
		{
			get { return liveTime; }
			set { liveTime = value; }
		}

		[SerializeField]
		protected Vector3 velocity;
		public virtual Vector3 Velocity
		{
			get { return velocity; }
			set { velocity = value; }
		}

		[SerializeField]
		protected Vector3 position;
		public virtual Vector3 Position
		{
			get { return position; }
			set { position = value; }
		}

		protected float liveTimer;

		protected virtual void Start()
		{
			liveTimer = LiveTime;
		}

		protected virtual void Update()
		{
			RaycastHit hit;
			if (Moving(Velocity, out hit))
			{
				Hitting(hit.collider);
			}

			liveTimer -= Time.deltaTime;
			if (liveTimer <= 0)
			{
				Destroy(gameObject);
			}
		}

		/// <summary>
		/// Move this projectile by velocity.
		/// </summary>
		/// <param name="velocity"></param>
		/// <returns>If this projectile hits something, return true, otherwise, false.</returns>
		protected virtual bool Moving(Vector3 velocity, out RaycastHit hit)
		{
			Ray ray = new Ray(Position, velocity);
			Debug.DrawRay(position, velocity * Time.deltaTime);
			Physics.Raycast(ray, out hit, velocity.magnitude * Time.deltaTime);

			transform.position += velocity * Time.deltaTime;
			position = transform.position;

			return hit.collider;
		}

		protected virtual void Hitting(Collider collider)
		{
		}
	}
}
