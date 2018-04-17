using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ElementWar.Weapon;
using System;

namespace ElementWar
{
	public class Enemy : MonoBehaviour, IElementalTarget
	{
		[SerializeField]
		UnityEvent OnElementChanged;

		[SerializeField]
		Element element;
		public Element Element
		{
			get { return element; }
			set { element = value; }
		}

		[SerializeField]
		List<Element> elementList;

		[SerializeField]
		ElementMeter eMeter;

		// Use this for initialization
		private void Start()
		{
			elementList.Add(Element);
		}

		// Update is called once per frame
		private void Update()
		{

		}

		public Element[] GetElements()
		{
			return elementList.ToArray();
		}

		public void HitByElement(Element element)
		{
			if (element == Element.None) return;

			if (elementList.Last() == element)
				elementList.RemoveAt(elementList.Count - 1);
			else
				elementList.Add(element);

			if (elementList.Count <= 0)
			{
				Destroy(gameObject);
			}

			eMeter.ShowElements(elementList.ToArray());
			OnElementChanged.Invoke();
		}
	}
}
