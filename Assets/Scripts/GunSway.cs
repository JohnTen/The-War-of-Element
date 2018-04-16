using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
