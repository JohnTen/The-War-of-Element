using UnityEngine;

namespace ElementWar
{
	public enum FloatingMethod
	{
		Absolute,
		Relative
	}

	public class ObjectFloater : MonoBehaviour
	{
		#region Fields

		[SerializeField]
		protected FloaterParameter floatParameter;

		[SerializeField]
		bool returnToOrigin;

		[SerializeField]
		protected FloatingMethod method;
		
		protected FloatingSolver Floater { get; set; }

		#endregion

		#region Public properties

		/// <summary>
		/// The original local position of the floating object.
		/// </summary>
		public Vector3 OriginalLocalPosition { get; set; }

		/// <summary>
		/// The original local rotation of the floating object.
		/// </summary>
		public Quaternion OriginalLocalRotation { get; set; }
		
		/// <summary>
		/// Make object return to it's original position and rotation.
		/// </summary>
		public bool ReturnToOrigin
		{
			get { return returnToOrigin; }
			set
			{
				//if (returnToOrigin == value) return;
				returnToOrigin = value;
				Floater.ReturnToOrigin = value;
			}
		}

		/// <summary>
		/// The parameters of floater
		/// </summary>
		public FloaterParameter FloatParameter
		{
			get { return floatParameter; }
			set
			{
				floatParameter = value;
				Floater.FloatParm = value;
			}
		}

		public FloatingMethod Method
		{
			get { return method; }
			set { method = value; }
		}

		#endregion

		#region Methods

		protected void Awake()
		{
			Floater = new FloatingSolver();
			Floater.FloatParm = FloatParameter;
			OriginalLocalPosition = transform.localPosition;
			OriginalLocalRotation = transform.localRotation;
		}

		protected virtual void Update()
		{
			ReturnToOrigin = returnToOrigin;
		
			switch (method)
			{
				case FloatingMethod.Absolute: AbsoluteMoveObject(); break;
				case FloatingMethod.Relative: RelativeMoveObject(); break;
			}
		}

		/// <summary>
		/// Moving object by setting absolute movement.
		/// </summary>
		public void AbsoluteMoveObject()
		{
			Vector3 posMovement;
			Quaternion rotMovement;

			Floater.GetAbsoluteOffsets(out posMovement, out rotMovement);

			transform.localPosition = OriginalLocalPosition + posMovement;
			transform.localRotation = OriginalLocalRotation * rotMovement;
		}

		/// <summary>
		/// Moving object by adding relative movement.
		/// </summary>
		public void RelativeMoveObject()
		{
			Vector3 posMovement;
			Quaternion rotMovement;

			Floater.GetRelativeOffsets(out posMovement, out rotMovement);

			//transform.Translate(posMovement);
			//transform.Rotate(rotMovement.eulerAngles);
			transform.localPosition += posMovement;
			transform.localRotation *= rotMovement;
		}

		/// <summary>
		/// Immediately return to origin point
		/// </summary>
		public void ReturnImmediately()
		{
			Floater.Reset();
			transform.localPosition = OriginalLocalPosition;
			transform.localRotation = OriginalLocalRotation;
		}

		#endregion

		#region Floating solver

		/// <summary>
		/// Generate the floating movement.
		/// </summary>
		protected class FloatingSolver
		{
			#region Fields

			/// <summary>
			/// The random position generater timer for <see cref="GetPosTarget"/> 
			/// </summary>
			float posTimer;

			/// <summary>
			/// The random rotation generater timer for <see cref="GetRotTarget"/> 
			/// </summary>
			float rotTimer;

			/// <summary>
			/// Used to prevent running multiple times one frame.
			/// </summary>
			int lastFrame;

			/// <summary>
			/// Backing field for <see cref="ReturnToOrigin"/>
			/// </summary>
			bool returnOrigin;

			/// <summary>
			/// Backing field for <see cref="ReturnSpeed"/>
			/// </summary>
			float returnSpeed = 8;

			/// <summary>
			/// Backing field for <see cref="FloatParm"/>
			/// </summary>
			FloaterParameter floatParm;
			
			float idealDist;

			Vector3 curPosOffset;	// Current position offset.
			Vector3 lstPosOffset;	// Last position offset.
			Quaternion curRotOffset = Quaternion.identity;  // Current rotation offset.
			Quaternion lstRotOffset = Quaternion.identity;  // Last rotation offset.

			#endregion

			#region Public properties

			/// <summary>
			/// Is this floater returning to original position/rotation.
			/// </summary>
			public bool ReturnToOrigin
			{
				get { return returnOrigin; }
				set
				{
					if (returnOrigin == value) return;
					if (value)
					{
						ResetBerp();
						posTimer = 0;
						rotTimer = 0;
					}
					returnOrigin = value;
				}
			}

			/// <summary>
			/// The parameters for generate random position and rotation.
			/// </summary>
			public FloaterParameter FloatParm
			{
				get { return floatParm; }
				set
				{
					floatParm = value;
					if (floatParm != null)
						idealDist = Mathf.Max(floatParm.PosRange.x, floatParm.PosRange.y, floatParm.PosRange.z);
				}
			}

			/// <summary>
			/// The speed when returning to origin. Will be 8 in default.
			/// </summary>
			public float ReturnSpeed
			{
				get { return returnSpeed; }
				set { returnSpeed = value; }
			}

			#endregion

			#region Public methods

			/// <summary>
			/// Get the absolute offset of position and rotation.
			/// </summary>
			/// <param name="posOffset">Position offset.</param>
			/// <param name="rotOffset">Rotation offset.</param>
			public void GetAbsoluteOffsets(out Vector3 posOffset, out Quaternion rotOffset)
			{
				if (lastFrame != Time.frameCount)
				{
					lastFrame = Time.frameCount;

					lstPosOffset = curPosOffset;
					lstRotOffset = curRotOffset;

					if (ReturnToOrigin)
					{
						curPosOffset = Berp(curPosOffset, Vector3.zero, Time.deltaTime * ReturnSpeed);
						curRotOffset = Berp(curRotOffset, Quaternion.identity, Time.deltaTime * ReturnSpeed);
					}
					else
					{
						curPosOffset = Berp(curPosOffset, GetPosTarget(), Time.deltaTime * FloatParm.PosChangeSpeed);
						curRotOffset = Berp(curRotOffset, GetRotTarget(), Time.deltaTime * FloatParm.RotChangeSpeed);
					}
				}
				
				posOffset = curPosOffset;
				rotOffset = curRotOffset;
			}

			/// <summary>
			/// Get the relative offset of position and rotation.
			/// </summary>
			/// <param name="posOffset">Position offset.</param>
			/// <param name="rotOffset">Rotation offset.</param>
			public void GetRelativeOffsets(out Vector3 posOffset, out Quaternion rotOffset)
			{
				if (lastFrame != Time.frameCount)
				{
					GetAbsoluteOffsets(out curPosOffset, out curRotOffset);
				}

				posOffset = curPosOffset - lstPosOffset;
				rotOffset = Quaternion.Inverse(lstRotOffset) * curRotOffset;
			}

			/// <summary>
			/// Reset floater.
			/// </summary>
			public void Reset()
			{
				posTimer = 0;
				rotTimer = 0;
				curPosOffset = Vector3.zero;
				curRotOffset = Quaternion.identity;
				ResetBerp();
			}

			#endregion

			#region Private methods

			/// <summary>
			/// Generate a random point in local space.
			/// </summary>
			/// <returns>A random point</returns>
			private Vector3 GetRandomPoint()
			{
				Vector3 point = Random.insideUnitSphere;
				point.Scale(FloatParm.PosRange);
				return point;
			}

			/// <summary>
			/// Generate a random rotation in local space.
			/// </summary>
			/// <returns>A random rotation</returns>
			private Quaternion GetRandomRotation()
			{
				float x = Random.Range(-FloatParm.RotRange.x, FloatParm.RotRange.x);
				float y = Random.Range(-FloatParm.RotRange.y, FloatParm.RotRange.y);
				float z = Random.Range(-FloatParm.RotRange.z, FloatParm.RotRange.z);
				return Quaternion.Euler(x, y, z);
			}

			Vector3 posTarget;
			private Vector3 GetPosTarget()
			{
				posTimer -= Time.deltaTime;
				if (posTimer > 0) return posTarget;
				
				for (int i = 0; i < 5; i++)
				{
					posTarget = GetRandomPoint();
					float sqrDist = (posTarget - ctrlPos).sqrMagnitude;
					if (Vector3.Dot(posTarget, ctrlPos) <= 0
						&& sqrDist <= idealDist)
						break;
				}
				posTimer = FloatParm.PosChangeFreq;

				return posTarget;
			}

			Quaternion rotTarget = Quaternion.identity;
			private Quaternion GetRotTarget()
			{
				rotTimer -= Time.deltaTime;
				if (rotTimer > 0) return rotTarget;

				rotTarget = GetRandomRotation();
				rotTimer = FloatParm.RotChangeFreq;

				return rotTarget;
			}

			#endregion

			/// Kind of a modified version of bezier interpolation.
			/// It's time sensitive due to the self-maintain control node.
			/// So it should be called once and only once per frame.
			#region Bezier interpolation

			Vector3 ctrlPos;
			/// <summary>
			/// Bezier interpolation of <see cref="Vector3"/>
			/// </summary>
			/// <param name="curPos">Current position</param>
			/// <param name="towardPos">Toward position</param>
			/// <returns></returns>
			private Vector3 Berp(Vector3 curPos, Vector3 towardPos, float t)
			{
				ctrlPos = Vector3.Lerp(ctrlPos, towardPos, t);
				curPos = Vector3.Lerp(curPos, ctrlPos, t);
				return curPos;
			}

			Quaternion ctrlRot = Quaternion.identity;
			/// <summary>
			/// Bezier interpolation of <see cref="Quaternion"/>
			/// </summary>
			/// <param name="curRot">Current rotation</param>
			/// <param name="towardRot">Toward rotation</param>
			/// <returns></returns>
			private Quaternion Berp(Quaternion curRot, Quaternion towardRot, float t)
			{
				ctrlRot = Quaternion.Lerp(ctrlRot, towardRot, t);
				curRot = Quaternion.Lerp(curRot, ctrlRot, t);
				
				return curRot;
			}

			/// <summary>
			/// Reset bezier interpolation.
			/// </summary>
			private void ResetBerp()
			{
				ctrlPos = Vector3.zero;
				ctrlRot = Quaternion.identity;
			}
			#endregion
		}
		#endregion
	}
}
