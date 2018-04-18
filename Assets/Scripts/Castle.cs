using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElementWar
{
	public class Castle : MonoBehaviour
	{
		private void OnTriggerEnter(Collider collider)
		{
			print(collider.name);
			if (collider.transform.parent == null) return;
			var enemy = collider.transform.parent.GetComponent<Enemy>();
			if (enemy == null) return;
			GameManager.CastleTakeDamage(1);
		}
	}
}

