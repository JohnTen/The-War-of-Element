using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElementWar
{
	public class EnemySpawner : MonoBehaviour
	{
		[SerializeField]
		float timeScale;

		[SerializeField]
		float radius = 10;

		[SerializeField]
		AnimationCurve enemySpeedCurve;

		[SerializeField]
		AnimationCurve SpawnRateCurve;

		[SerializeField]
		Transform destination;

		[SerializeField]
		Enemy[] enemyPrefabs;

		float time;

		float spawnTimer;

		// Update is called once per frame
		void Update()
		{
			time += Time.deltaTime;
			spawnTimer -= Time.deltaTime;
			if (spawnTimer > 0) return;

			while (spawnTimer <= 0)
			{
				spawnTimer += 1 / SpawnRateCurve.Evaluate(time / timeScale);
				Vector3 radius = OnUnitCircle();
				radius.z = radius.y;
				radius.y = 0;
				var enemy = 
					Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)].gameObject, 
					transform.position + radius, 
					Quaternion.identity).
					GetComponent<Enemy>();

				enemy.transform.LookAt(transform);
				enemy.Destination = destination;
				enemy.MoveSpeed = enemySpeedCurve.Evaluate(time / timeScale);

				//var element = Random.Range(0, 4);
				//switch (element)
				//{
				//	case 0:
				//		enemy.Element = Element.Air;
				//		break;
				//	case 1:
				//		enemy.Element = Element.Fire;
				//		break;
				//	case 2:
				//		enemy.Element = Element.Earth;
				//		break;
				//	case 3:
				//		enemy.Element = Element.Water;
				//		break;
				//}
			}
		}

		Vector2 OnUnitCircle()
		{
			float angle = Random.Range(0, Mathf.PI * 2);

			Vector2 point;
			point.x = radius * Mathf.Cos(angle);
			point.y = radius * Mathf.Sin(angle);

			return point;
		}
	}
}

