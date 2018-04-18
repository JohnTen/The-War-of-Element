using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ParabolaDrawer : MonoBehaviour
{
	LineRenderer renderer;

	public float SimTime;
	public int MaxSimSteps;
	public float Gravity;
	public Vector3 InitPoint;
	public Vector3 InitVelocity;
	public bool DrawingParabola;

	public void DrawParabola()
	{
		int steps = 0;
		RaycastHit hit = new RaycastHit();
		Vector3 currentVel = transform.TransformDirection(InitVelocity);
		List<Vector3> points = new List<Vector3>();
		points.Add(transform.position + InitPoint);

		while (!hit.collider && steps < MaxSimSteps)
		{
			var lastPoint = points[points.Count - 1];
			currentVel.y -= Gravity * SimTime;
			Debug.DrawRay(lastPoint, currentVel * SimTime, Color.red);
			Physics.Raycast(new Ray(lastPoint, currentVel), out hit, currentVel.magnitude * SimTime);

			if (hit.collider != null)
				points.Add(hit.point);
			else
				points.Add(lastPoint + currentVel * SimTime);
			steps++;
		}

		renderer.positionCount = points.Count;
		renderer.SetPositions(points.ToArray());
	}

	// Use this for initialization
	void Awake () {
		renderer = GetComponent<LineRenderer>();
	}

	// Update is called once per frame
	void Update () {
		if (DrawingParabola)
			DrawParabola();
	}
}
