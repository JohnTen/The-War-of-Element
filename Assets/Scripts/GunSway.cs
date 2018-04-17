using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElementWar
{
	public class GunSway : MonoBehaviour
	{
		[SerializeField]
		Transform gun;

		[SerializeField]
		float positionSmoothing;

		[SerializeField]
		float rotationSmoothing;

		Vector3 originalLocalPos;
		Quaternion originalLocalRot;

		[SerializeField]
		Vector3 lastFramePos;
		[SerializeField]
		Quaternion lastFrameRot;

		[SerializeField]
		Vector3 MaxPositionLimit;

		void Start()
		{
			originalLocalPos = transform.localPosition;
			originalLocalRot = transform.localRotation;
			lastFramePos = transform.position;
			lastFrameRot = transform.rotation;
		}

		void Update()
		{
			if (positionSmoothing > 0)
			{
				gun.position = Vector3.Lerp(lastFramePos, transform.position, positionSmoothing * Time.deltaTime);
				var gunPos = gun.localPosition;
				if (MaxPositionLimit.x > 0 && Mathf.Abs(gunPos.x) > MaxPositionLimit.x)
				{
					gunPos.x = Mathf.Sign(gunPos.x) * MaxPositionLimit.x;
				}
				if (MaxPositionLimit.y > 0 && Mathf.Abs(gunPos.y) > MaxPositionLimit.y)
				{
					gunPos.y = Mathf.Sign(gunPos.y) * MaxPositionLimit.y;
				}
				if (MaxPositionLimit.z > 0 && Mathf.Abs(gunPos.z) > MaxPositionLimit.z)
				{
					gunPos.z = Mathf.Sign(gunPos.z) * MaxPositionLimit.z;
				}
				gun.localPosition = gunPos;
			}
			else
			{
				gun.position = transform.position;
			}
			lastFramePos = gun.position;

			if (rotationSmoothing > 0)
			{
				gun.rotation = Quaternion.Lerp(lastFrameRot, transform.rotation, rotationSmoothing * Time.deltaTime);
			}
			else
			{
				gun.rotation = transform.rotation;
			}
			lastFrameRot = gun.rotation;
		}
	}
}

