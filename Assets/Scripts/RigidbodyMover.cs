using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodyMover : MonoBehaviour
{
	[SerializeField]
	float moveSpeed;

	[SerializeField]
	float jumpPower;

	[SerializeField]
	float destinationReachedDistance;

	new Rigidbody rigidbody;

	Transform destination;

	public bool IsAirborne { get; protected set; }

	public void SetDestination(Vector3 destination)
	{
		if (this.destination == null)
			this.destination = new GameObject("Dest").transform;
		this.destination.SetParent(null);
		this.destination.position = destination;
	}

	public void SetDestination(Transform destination)
	{
		if (this.destination == null)
			this.destination = new GameObject("Dest").transform;
		this.destination.SetParent(destination);
		this.destination.localPosition = Vector3.zero;
	}

	public void ClearDestination()
	{
		destination = null;
	}

	public void Translate(Vector3 translation)
	{
		rigidbody.MovePosition(transform.position += translation);
	}

	public void Translate(Vector3 translation, bool relativeToSelf)
	{
		if (relativeToSelf)
		{
			translation = transform.TransformDirection(translation);
		}
		rigidbody.MovePosition(transform.position += translation);
	}

	public void Jump()
	{
		rigidbody.AddForce(Vector3.up * jumpPower);
	}

	void Awake ()
	{
		rigidbody = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate ()
	{
		if (destination == null) return;

		
 	}
}
