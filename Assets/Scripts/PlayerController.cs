using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElementWar.Weapon;

namespace ElementWar
{
	public class PlayerController : MonoBehaviour
	{
		[SerializeField, Range(0, 10)]
		float moveSpeed;

		[SerializeField]
		float noddingFreq;

		[SerializeField]
		float noddingSpeed;

		[SerializeField]
		MagicGun gun;

		// Update is called once per frame
		void Update ()
		{
			MovePlayer();
			FireWeapon();
        }

		void MovePlayer()
		{
			Vector3 spaceMovement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

			if (spaceMovement.sqrMagnitude > 1)
				spaceMovement.Normalize();

			transform.Translate(spaceMovement * moveSpeed * Time.deltaTime);
		}

		void FireWeapon()
		{
			if (Input.GetButton("Fire1"))
			{
				gun.Launch();
            }
		}
	}
}

