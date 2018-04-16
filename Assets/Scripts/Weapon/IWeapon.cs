using System;
using System.Collections.Generic;
using UnityEngine;

namespace ElementWar.Weapon
{
	[Serializable]
	public struct WeaponParameter
	{
		public MonoProjectile Projectile;
		public float FireRate;
		public float Damage;
		public float LaunchForce;
	}

	public interface IWeapon
	{
		WeaponParameter Parameter { get; set; }
		bool Launch();
	}

	public abstract class MonoWeapon : MonoBehaviour, IWeapon
	{
		[SerializeField]
		Transform firePoint;

		[SerializeField]
		protected WeaponParameter parameter;
		public virtual WeaponParameter Parameter
		{
			get
			{
				return parameter;
            }

			set
			{
				parameter = value;
            }
		}
		[SerializeField]
		float fireTimer;

		protected virtual void Update()
		{
			if (fireTimer > 0) fireTimer -= Time.deltaTime;
        }

		public virtual bool Launch()
		{
			if (fireTimer > 0) return false;
			fireTimer = Parameter.FireRate;

			var projectile = Instantiate(parameter.Projectile);
			InitializeProjectile(projectile);

			return true;
        }

		protected virtual void InitializeProjectile(MonoProjectile projectile)
		{
			projectile.transform.position = firePoint.position;
			projectile.transform.rotation = firePoint.rotation;
			projectile.Velocity = firePoint.forward * parameter.LaunchForce;
		}
	}
}
