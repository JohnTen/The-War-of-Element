using System;
using UnityEngine;

namespace ElementWar
{
	/// <summary>
	/// Store the parameters of <see cref="WeaponFloater"/>
	/// </summary>
	[Serializable]
	public class FloaterParameter
	{
		/// <summary>
		/// Positional random movement range.
		/// </summary>
		[Tooltip("Positional random movement range.")]
		public Vector3 PosRange;

		/// <summary>
		/// The changing frequency of positional random movement.
		/// </summary>
		[Tooltip("The changing frequency of positional random movement.")]
		public float PosChangeFreq;

		/// <summary>
		/// Speed of positional random movement.
		/// </summary>
		[Tooltip("Speed of positional random movement.")]
		public float PosChangeSpeed;

		/// <summary>
		/// Rotational random movement range.
		/// </summary>
		[Tooltip("Rotational random movement range.")]
		public Vector3 RotRange;

		/// <summary>
		/// The changing frequency of rotational random movement.
		/// </summary>
		[Tooltip("The changing frequency of rotational random movement.")]
		public float RotChangeFreq;

		/// <summary>
		/// Speed of rotational random movement.
		/// </summary>
		[Tooltip("Speed of rotational random movement.")]
		public float RotChangeSpeed;

		public FloaterParameter() { }

		public FloaterParameter(FloaterParameter param)
		{
			PosRange = param.PosRange;
			PosChangeFreq = param.PosChangeFreq;
			PosChangeSpeed = param.PosChangeSpeed;

			RotRange = param.RotRange;
			RotChangeFreq = param.RotChangeFreq;
			RotChangeSpeed = param.RotChangeSpeed;
		}

		public FloaterParameter(Vector3 posRange, float posFreq, float posSpeed, Vector3 rotRange, float rotFreq, float rotSpeed)
		{
			this.PosRange = posRange;
			PosChangeFreq = posFreq;
			PosChangeSpeed = posSpeed;

			this.RotRange = rotRange;
			RotChangeFreq = rotFreq;
			RotChangeSpeed = rotSpeed;
		}
	}

}

